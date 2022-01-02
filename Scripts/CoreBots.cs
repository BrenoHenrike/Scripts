using RBot;
using RBot.Items;
using RBot.Monsters;
using RBot.Quests;
using RBot.Skills;
using RBot.Flash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

public class CoreBots
{
	// [Can Change] Delay between commom actions, 700 is the safe number
	public int ActionDelay { get; set; } = 700;
	// [Can Change] Delay used to get out of combat, 1600 is the safe number
	public int ExitCombatDelay { get; set; } = 1600;
	// [Can Change] Delay between jumping rooms after hunting a monster, increase if you think it is jumping too much
	public int HuntDelay { get; set; } = 1000;
	// [Can Change] How many tries to accept/complete the quest will be sent
	public int AcceptandCompleteTries { get; set; } = 20;
	// [Can Change] Whether the bots will use private rooms
	public bool PrivateRooms { get; set; } = true;
	// [Can Change] Whether the player should rest after killing a monster
	public bool ShouldRest { get; set; } = false;
	// [Can Change] Whether you want anti lag features (lag killer, invisible monsters, set to 10 FPS)
	public bool AntiLag { get; set; } = true;
	// [Can Change] The interval, in milliseconds, at which to use skills, if they are available.
	public int SkillTimer { get; set; } = 100;
	// [Can Change] Name of your soloing class
	public string SoloClass { get; set; } = "Generic";
	// [Can Change] (Use the Core Skill plugin) Skill sequence string
	public string SoloClassSkills { get; set; } = "1 | 2 | 3 | 4 | Mode Optimistic";
	// [Can Change] (Use the Core Skill plugin if unsure) SkillTimeout of the soloing class
	public int SoloClassSkillTimeout { get; set; } = 1;
	// [Can Change] Name of your farming class
	public string FarmClass { get; set; } = "Generic";
	// [Can Change] (Use the Core Skill plugin) Skill sequence string
	public string FarmClassSkills { get; set; } = "1 | 2 | 3 | 4 | Mode Optimistic";
	// [Can Change] (Use the Core Skill plugin if unsure) SkillTimeout of the farming class
	public int FarmClassSkillTimeout { get; set; } = 1;
	// [Can Change] Some Sagas use the hero alignment to give extra reputation, change to your desired rep (Alignment.Evil or Alignment.Good).
	public int HeroAlignment { get; set; } = (int)Alignment.Evil;


	// Whether the player is Member
	public bool IsMember => ScriptInterface.Instance.Player.IsMember;

	private static CoreBots _instance;
	public static CoreBots Instance => _instance ?? (_instance = new CoreBots());
	public ScriptInterface Bot => ScriptInterface.Instance;

	public List<ItemBase> CurrentRequirements = new List<ItemBase>();

	/// <summary>
	/// Set commom bot options to desired value
	/// </summary>
	/// <param name="changeTo">Value the options will be changed to</param>
	public void SetOptions(bool changeTo = true)
	{
		// Common Options
		Bot.Options.PrivateRooms = PrivateRooms;
		Bot.Options.SafeTimings = changeTo;
		Bot.Options.RestPackets = changeTo;
		Bot.Options.AutoRelogin = changeTo;
		Bot.Options.InfiniteRange = changeTo;
		Bot.Options.ExitCombatBeforeQuest = changeTo;
		Bot.Drops.RejectElse = changeTo;
		Bot.Lite.UntargetDead = changeTo;
		Bot.Lite.UntargetSelf = changeTo;
        Bot.Lite.ReacceptQuest = false;

		if (changeTo)
		{
			Logger("Bot Started");

			Bot.Options.HuntDelay = HuntDelay;

			Bot.RegisterHandler(2, b =>
			{
				if (b.ShouldExit())
					StopBot();
			}, "Stop Handler");

			Bot.RegisterHandler(15, b =>
			{
				if (b.Player.Cell.Contains("Cut"))
				{
					FlashUtil.Call("skipCutscenes");
					b.Sleep(1500);
					b.Map.Reload();
				}
			}, "Skip Cutscenes");

			Bot.Player.LoadBank();
			Bot.Runtime.BankLoaded = true;
			usingSoloGeneric = SoloClass.ToLower() == "generic";
			usingFarmGeneric = FarmClass.ToLower() == "generic";
			EquipClass(ClassType.Solo);
			// Anti-lag option
			if (AntiLag)
			{
				Bot.Options.LagKiller = true;
				Bot.SetGameObject("stage.frameRate", 10);
				if (!Bot.GetGameObject<bool>("ui.monsterIcon.redX.visible"))
					Bot.CallGameFunction("world.toggleMonsters");
			}

			Logger("Bot Configured");
		}
		else
			StopBot(true);
	}

	#region Inventory, Bank and Shop
	/// <summary>
	/// Check the Bank, Inventory and Temp Inventory for the item
	/// </summary>
	/// <param name="item">Name of the item</param>
	/// <param name="quant">Desired quantity</param>
	/// <param name="toInv">Whether or not send the item to Inventory</param>
	/// <returns>Returns whether the item exists in the desired quantity in the bank and inventory</returns>
	public bool CheckInventory(string item, int quant = 1, bool toInv = true)
	{
		if (Bot.Inventory.ContainsTempItem(item, quant))
			return true;
		if (Bot.Bank.Contains(item))
		{
			if (!toInv)
				return true;
			Unbank(item);
		}
		if (Bot.Inventory.Contains(item, quant))
			return true;
		return false;
	}

	/// <summary>
	/// Checks the Bank and Inventory for the item with it's ID
	/// </summary>
	/// <param name="itemID">ID of the item to verify</param>
	/// <param name="quant">Desired quantity</param>
	/// <param name="toInv">Whether or not send the item to Inventory</param>
	/// <returns>Returns whether the item exists in the desired quantity in the Bank and Inventory</returns>
	public bool CheckInventory(int itemID, int quant = 1, bool toInv = true)
	{
		InventoryItem itemBank = Bot.Bank.BankItems.Find(i => i.ID == itemID);
		InventoryItem itemInv = Bot.Inventory.Items.Find(i => i.ID == itemID);
		ItemBase itemTempInv = Bot.Inventory.TempItems.Find(i => i.ID == itemID);
		if (itemBank == null && itemInv == null && itemTempInv == null)
			return false;
		if (itemTempInv != null && Bot.Inventory.ContainsTempItem(itemTempInv.Name, quant))
			return true;
		if (itemBank != null && Bot.Bank.Contains(itemBank.Name))
		{
			if (!toInv)
				return true;
			Unbank(itemBank.Name);
		}
		if (itemInv != null && Bot.Inventory.Contains(itemInv.Name, quant))
			return true;
		
		return false;
	}

	/// <summary>
	/// Check if the Bank/Inventory has atleast 1 of all listed items
	/// </summary>
	/// <param name="itemNames">Array of names of the items to be check</param>
	/// <param name="toInv">Whether or not send the item to Inventory</param>
	/// <param name="any">If any of the items exist, returns true</param>
	/// <returns>Returns whether all the items exist in the Bank or Inventory</returns>
	public bool CheckInventory(string[] itemNames, bool any = false, bool toInv = true)
	{
		foreach (string name in itemNames)
		{
			if (Bot.Bank.Contains(name))
			{
				if (!toInv && !any)
					continue;
                if (!toInv && any)
                    return true;
                Unbank(name);
			}
			if (Bot.Inventory.Contains(name) && any)
				return true;
			else if (Bot.Inventory.Contains(name) && !any)
				continue;
			else if (!any)
				return false;
		}
		return true;
	}

	/// <summary>
	/// Move items from bank to inventory
	/// </summary>
	/// <param name="items">Items to move</param>
	public void Unbank(params string[] items)
	{
		JumpWait();
		Bot.Player.OpenBank();
		foreach (string item in items)
		{
			if (Bot.Bank.Contains(item))
			{
				Bot.Sleep(ActionDelay);
				while (!Bot.Inventory.Contains(item))
				{
					Bot.Bank.ToInventory(item);
					Bot.Wait.ForBankToInventory(item);
					Bot.Sleep(ActionDelay); 
				}
				Logger($"{item} moved from bank");
			}
		}
	}

	/// <summary>
	/// Move items from inventory to bank
	/// </summary>
	/// <param name="items">Items to move</param>
	public void ToBank(params string[] items)
	{
		JumpWait();
		Bot.Player.OpenBank();
		foreach (string item in items)
		{
			if (Bot.Inventory.Contains(item))
			{
				Bot.Sleep(ActionDelay);
				while (!Bot.Bank.Contains(item))
				{
					Bot.Inventory.ToBank(item);
					Bot.Wait.ForInventoryToBank(item);
					Bot.Sleep(ActionDelay);
				}
				Logger($"{item} moved to bank"); 
			}
		}
	}

	/// <summary>
	/// Buys a item till it have the desired quantity
	/// </summary>
	/// <param name="map">Map of the shop</param>
	/// <param name="shopID">ID of the shop</param>
	/// <param name="itemName">Name of the item</param>
	/// <param name="quant">Desired quantity</param>
	/// <param name="shopQuant">How many items you get for 1 buy</param>
	/// <param name="shopItemID">Use this for Merge shops that has 2 or more of the item with the same name and you need the second/third/etc., be aware that it will relog you after to prevent ghost buy. To get the ShopItemID use the built in loader of RBot</param>
	public void BuyItem(string map, int shopID, string itemName, int quant = 1, int shopQuant = 1, int shopItemID = 0)
	{
		if (CheckInventory(itemName, quant))
			return;
		JumpWait();
		Bot.Player.Join(map);
		Bot.Shops.Load(shopID);
		RBot.Shops.ShopItem item = Bot.Shops.ShopItems.First(shopitem => shopitem.Name == itemName);
		quant = _CalcBuyQuantity(item, quant, shopQuant);
		if(quant <= 0)
			return;
		_BuyItem(shopID, item, quant, shopQuant, shopItemID);
	}

	/// <summary>
	/// Buys a item till it have the desired quantity
	/// </summary>
	/// <param name="map">Map of the shop</param>
	/// <param name="shopID">ID of the shop</param>
	/// <param name="itemID">ID of the item</param>
	/// <param name="quant">Desired quantity</param>
	/// <param name="shopQuant">How many items you get for 1 buy</param>
	/// <param name="shopItemID">Use this for Merge shops that has 2 or more of the item with the same name and you need the second/third/etc., be aware that it will relog you after to prevent ghost buy. To get the ShopItemID use the built in loader of RBot</param>
	public void BuyItem(string map, int shopID, int itemID, int quant = 1, int shopQuant = 1, int shopItemID = 0)
	{
		if (CheckInventory(itemID, quant))
			return;
		JumpWait();
		Bot.Player.Join(map);
		Bot.Shops.Load(shopID);
		RBot.Shops.ShopItem item = Bot.Shops.ShopItems.First(shopitem => shopitem.ID == itemID);
		quant = _CalcBuyQuantity(item, quant, shopQuant);
		if(quant <= 0)
			return;
		_BuyItem(shopID, item, quant, shopQuant, shopItemID);
	}

	private void _BuyItem(int shopID, RBot.Shops.ShopItem item, int quant, int shopQuant, int shopItemID)
	{
		if (shopItemID == 0)
			for (int i = 0; i < quant; i++)
				Bot.Shops.BuyItem(shopID, item.Name);
		else
		{
			SendPackets($"%xt%zm%buyItem%{Bot.Map.RoomID}%{item.ID}%{shopID}%{shopItemID}%", quant);
			Logger("Relogin to prevent ghost buy");
			Relogin();
		}
		Logger($"Bought {quant}x{shopQuant} {item.Name}");
	}

	private int _CalcBuyQuantity(RBot.Shops.ShopItem item, int quant, int shopQuant)
	{
		if (Bot.Inventory.GetQuantity(item.Name) + shopQuant > item.MaxStack)
		{
			Logger("Can't buy merge item past its max stack, skipping");
			return 0;
		}
		int quantB = quant - Bot.Inventory.GetQuantity(item.Name);
		if (quantB < 0)
			return 0;
		decimal quantF = (decimal)quantB / (decimal)shopQuant;
		return (int)Math.Ceiling(quantF);
	}

	public void SellItem(string itemName, int quant = 0, bool all = false)
	{
		if(!CheckInventory(itemName))
			return;
		JumpWait();
		if(!all)
			for (int i = 0; i < quant; i++)
			{
				Bot.Shops.SellItem(itemName);
				Bot.Sleep(ActionDelay);
			}
		else
			while(Bot.Inventory.GetQuantity(itemName) != 0)
			{
				Bot.Shops.SellItem(itemName);
				Bot.Sleep(ActionDelay);
			}

		Logger($"{(all ? "" : quant.ToString())} {itemName} sold");
	}
	#endregion

	#region Drops

	/// <summary>
	/// Adds drops to the pickup list, unbank the items and restart the Drop Grabber
	/// </summary>
	/// <param name="items">Items to add</param>
	public void AddDrop(params string[] items)
	{
		Bot.Drops.Stop();
		Unbank(items);
		foreach (string item in items)
			Bot.Drops.Add(item);
		Bot.Drops.Start();
	}
	
	/// <summary>
	/// Removes drops from the pickup list and restart the Drop Grabber
	/// </summary>
	/// <param name="items">Items to remove</param>
	public void RemoveDrop(params string[] items)
	{
		Bot.Drops.Stop();
		foreach (string item in items)
			Bot.Drops.Remove(item);
		Bot.Drops.Start();
	}
	#endregion

	#region Quest
	
	/// <summary>
	/// Ensures you are out of combat before accepting the quest
	/// </summary>
	/// <param name="questID">ID of the quest to accept</param>
	public bool EnsureAccept(int questID)
	{
		if (Bot.Quests.IsInProgress(questID))
			return true;
		if(questID <= 0)
			return false;
		JumpWait();
		Bot.Sleep(ActionDelay);
		return Bot.Quests.EnsureAccept(questID, tries: AcceptandCompleteTries);
	}

	/// <summary>
	/// Accepts all de quests given
	/// </summary>
	/// <param name="questIDs">IDs of the quests</param>
	public void EnsureAccept(params int[] questIDs)
	{
		JumpWait();
		foreach (int quest in questIDs)
		{
			if (Bot.Quests.IsInProgress(quest) || quest <= 0)
				continue;
			Bot.Sleep(ActionDelay);
			Bot.Quests.EnsureAccept(quest, tries: AcceptandCompleteTries);
		}
	}

	/// <summary>
	/// Ensures you are out of combat before completing the quest
	/// </summary>
	/// <param name="questID">ID of the quest to complete</param>
	/// <param name="itemID">ID of the choosable reward item</param>
	public bool EnsureComplete(int questID, int itemID = -1)
	{
		if (questID <= 0)
			return false;
		JumpWait();
		Bot.Sleep(ActionDelay);
		return Bot.Quests.EnsureComplete(questID, itemID, tries: AcceptandCompleteTries);
	}

	/// <summary>
	/// Completes a quest and choose any item from it that you don't have (automatically accepts the drop)
	/// </summary>
	/// <param name="questID">ID of the quest</param>
	/// <param name="itemList">List of the items to get, if you want all just let it be null</param>
	public bool EnsureCompleteChoose(int questID, string[] itemList = null)
	{
		if (questID <= 0)
			return false;
		JumpWait();
		Bot.Sleep(ActionDelay);
		if(Bot.Quests.TryGetQuest(questID, out Quest quest))
			foreach (ItemBase item in quest.Rewards)
			{
				if(!CheckInventory(item.Name, toInv: false)
					&& (itemList == null || (itemList != null && itemList.Contains(item.Name))))
				{
					Logger($"Completed [{quest.Name}] for {item.Name}");
					bool completed = Bot.Quests.EnsureComplete(questID, item.ID, tries: AcceptandCompleteTries);
					Bot.Player.Pickup(item.Name);
					return completed;
				}
			}
		Logger($"Could not complete the quest {questID}. Maybe all items are already in your inv");
		return false;
	}

	/// <summary>
	/// Completes all the quests given but doesn't support quests with choosable rewards
	/// </summary>
	/// <param name="questIDs">IDs of the quests</param>
	public void EnsureComplete(params int[] questIDs)
	{
		JumpWait();
		foreach (int quest in questIDs)
		{
			if (quest <= 0)
				continue;
			Bot.Sleep(ActionDelay);
			Bot.Quests.EnsureComplete(quest, tries: AcceptandCompleteTries);
		}
	}

	/// <summary>
	/// Accepts and then completes the quest, used inside a loop
	/// </summary>
	/// <param name="questID">ID of the quest</param>
	/// <param name="itemID">ID of the choosable reward item</param>
	public void ChainComplete(int questID, int itemID = -1)
	{
		JumpWait();
		Bot.Quests.EnsureAccept(questID, tries: AcceptandCompleteTries);
		Bot.Sleep(ActionDelay);
		Bot.Quests.EnsureComplete(questID, itemID, tries: AcceptandCompleteTries);
	}
	#endregion

	#region Kill

	/// <summary>
	/// Kills the specified monsters in the map for the quest requirements
	/// </summary>
	/// <param name="questID">ID of the quest</param>
	/// <param name="map">Map where the <paramref name="monsters"/> are</param>
	/// <param name="monsters">Array of the monsters to kill</param>
	/// <param name="iterations">How many times it should kill the monster until going to the next</param>
	/// <param name="completeQuest">Whether complete the quest after killing all monsters</param>
	public void SmartKillMonster(int questID, string map, string[] monsters, int iterations = 20, bool completeQuest = false)
	{
		EnsureAccept(questID);
		_AddRequirement(questID);
		Bot.Player.Join(map);
		foreach (string monster in monsters)
			_SmartKill(monster, iterations);
		if (completeQuest)
			EnsureComplete(questID);
		CurrentRequirements.Clear();
	}

	/// <summary>
	/// Kills the specified monsters in the map for the quest requirements
	/// </summary>
	/// <param name="questID">ID of the quest</param>
	/// <param name="map">Map where the <paramref name="monster"/> is</param>
	/// <param name="monster">Monster to kill</param>
	/// <param name="iterations">How many times it should kill the monster until exiting</param>
	/// <param name="completeQuest">Whether complete the quest after killing all monsters</param>
	public void SmartKillMonster(int questID, string map, string monster, int iterations = 20, bool completeQuest = false)
	{
		EnsureAccept(questID);
		_AddRequirement(questID);
		Bot.Player.Join(map);
		_SmartKill(monster, iterations);
		if (completeQuest)
			EnsureComplete(questID);
		CurrentRequirements.Clear();
	}

	private void _SmartKill(string monster, int iterations = 20)
	{
		bool repeat = true;
		for(int j = 0; j < iterations; j++)
		{
			if(CurrentRequirements.Count == 0)
				break;
			if (CurrentRequirements.Count == 1)
			{
				if(_RepeatCheck(ref repeat, 0))
					break;
				_MonsterHunt(ref repeat, monster, CurrentRequirements[0].Name, CurrentRequirements[0].Quantity, CurrentRequirements[0].Temp, 0);
				break;
			}
			else
			{
				for (int i = CurrentRequirements.Count - 1; i >= 0; i--)
				{
					if(j == 0 && (CheckInventory(CurrentRequirements[i].Name, CurrentRequirements[i].Quantity)))
					{
						CurrentRequirements.RemoveAt(i);
						continue;
					}
					if (j != 0 && CheckInventory(CurrentRequirements[i].Name))
					{
						if(_RepeatCheck(ref repeat, i))
							break;
						_MonsterHunt(ref repeat, monster, CurrentRequirements[i].Name, CurrentRequirements[i].Quantity, CurrentRequirements[i].Temp, i);
						break;
					}
				}
			}
			if (!repeat)
				break;

			Bot.Player.Hunt(monster);
			Bot.Player.Pickup(CurrentRequirements.Where(item => !item.Temp).Select(item => item.Name).ToArray());
			Bot.Sleep(ActionDelay);
		}
	}

	private void _MonsterHunt(ref bool shouldRepeat, string monster, string itemName, int quantity, bool isTemp, int index)
	{
		_HuntForItem(monster, itemName, quantity, isTemp);
		CurrentRequirements.RemoveAt(index);
		shouldRepeat = false;
	}

	private bool _RepeatCheck(ref bool shouldRepeat, int index)
	{
		if(CheckInventory(CurrentRequirements[index].Name, CurrentRequirements[index].Quantity))
		{
			CurrentRequirements.RemoveAt(index);
			shouldRepeat = false;
			return true;
		}
		return false;
	}

	/// <summary>
	/// Joins a map, jump & set the spawn point and kills the specified monster
	/// </summary>
	/// <param name="map">Map to join</param>
	/// <param name="cell">Cell to jump to</param>
	/// <param name="pad">Pad to jump to</param>
	/// <param name="monster">Name of the monster to kill</param>
	/// <param name="item">Item to kill the monster for, if null will just kill the monster 1 time</param>
	/// <param name="quant">Desired quantity of the item</param>
	/// <param name="isTemp">Whether the item is temporary</param>
	/// <param name="log">Whether it will log that it is killing the monster</param>
	public void KillMonster(string map, string cell, string pad, string monster, string item = null, int quant = 1, bool isTemp = true, bool log = true)
	{
		if (item != null && CheckInventory(item, quant))
			return;
		Bot.Player.Join(map);
		Jump(cell, pad);
		if (item == null)
		{
			if(log)
				Logger($"Killing {monster}");
			Bot.Player.Kill(monster);
			Rest();
		}
		else
			_KillForItem(monster, item, quant, isTemp, log: log);
	}

	/// <summary>
	/// Kills a monster using it's ID
	/// </summary>
	/// <param name="map">Map to join</param>
	/// <param name="cell">Cell to jump to</param>
	/// <param name="pad">Pad to jump to</param>
	/// <param name="monsterID">ID of the monster</param>
	/// <param name="item">Item to kill the monster for, if null will just kill the monster 1 time</param>
	/// <param name="quant">Desired quantity of the item</param>
	/// <param name="isTemp">Whether the item is temporary</param>
	/// <param name="log">Whether it will log that it is killing the monster</param>
	public void KillMonster(string map, string cell, string pad, int monsterID, string item = null, int quant = 1, bool isTemp = true, bool log = true)
	{
		if (item != null && CheckInventory(item, quant))
			return;
		Bot.Player.Join(map);
        Bot.Wait.ForMapLoad(map);
		Jump(cell, pad);
		Monster monster = Bot.Monsters.CurrentMonsters.Find(m => m.ID == monsterID);
		if (item == null)
		{
			if (log)
				Logger($"Killing {monster}");
			Bot.Player.Kill(monster);
			Rest();
		}
		else
		{
			if (log)
				Logger($"Killing {monster} for {item} ({quant}) [Temp = {isTemp}]");
			_KillForItem(monster.Name, item, quant, isTemp);
		}
	}

	/// <summary>
	/// Joins a map and hunt for the monster
	/// </summary>
	/// <param name="map">Map to join</param>
	/// <param name="monster">Name of the monster to kill</param>
	/// <param name="item">Item to hunt the monster for, if null will just hunt & kill the monster 1 time</param>
	/// <param name="quant">Desired quantity of the item</param>
	/// <param name="isTemp">Whether the item is temporary</param>
	public void HuntMonster(string map, string monster, string item = null, int quant = 1, bool isTemp = true)
	{
		if (item != null && CheckInventory(item, quant))
			return;
		Bot.Player.Join(map);
        Bot.Wait.ForMapLoad(map);
        if (item == null)
		{
			Logger($"Hunting {monster}");
            Bot.Player.Hunt(monster);
            Rest();
		}
		else
			_HuntForItem(monster, item, quant, isTemp);
	}

	/// <summary>
	/// Kill Escherion for the desired item
	/// </summary>
	/// <param name="item">Item name</param>
	/// <param name="quant">Desired quantity</param>
	/// <param name="isTemp">Whether the item is temporary</param>
	public void KillEscherion(string item = null, int quant = 1, bool isTemp = false)
	{
		if (item != null && CheckInventory(item, quant))
			return;
		Bot.Player.Join("escherion");
		Jump("Boss", "Left");
		if (item == null)
		{
			Logger("Killing Escherion");
			while(Bot.Monsters.CurrentMonsters.Find(m => m.Name == "Escherion").Alive)
			{
				if(Bot.Monsters.CurrentMonsters.Find(m => m.Name == "Staff of Inversion").Alive)
					Bot.Player.Kill("Staff of Inversion");
				Bot.Player.Attack("Escherion");
				Bot.Sleep(1000);
			}
		}
		else
		{
			Logger($"Killing Escherion for {item} ({quant}) [Temp = {isTemp}]");
			while(!CheckInventory(item, quant))
			{
				if(Bot.Monsters.CurrentMonsters.Find(m => m.Name == "Staff of Inversion").Alive)
					Bot.Player.Kill("Staff of Inversion");
				Bot.Player.Attack("Escherion");
				Bot.Sleep(1000);
				Bot.Player.Pickup(item);
			}
		}
	}
	#endregion

	#region Utils
	
	/// <summary>
	/// Logs a line of text to the script log with time, method from where it's called and a message
	/// </summary>
	public void Logger(string message = "", [CallerMemberName] string caller = null, bool messageBox = false, bool stopBot = false)
	{
		Bot.Log($"[{DateTime.Now:HH:mm:ss}] ({caller})  {message}");
		if (messageBox)
			Message(message, caller);
		if (stopBot)
			StopBot(true);
	}

    /// <summary>
    /// Creates a Message Box with the desired text and caption
    /// </summary>
    /// <param name="message">Message to display</param>
    /// <param name="caption">Title of the box</param>
    public void Message(string message, string caption)
    {
        if(DialogResult.OK == MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000))
            return;
    }

    /// <summary>
    /// Send a packet to the server the desired amount of times
    /// </summary>
    /// <param name="packet">Packet to send</param>
    /// <param name="times">How many times to send</param>
    public void SendPackets(string packet, int times = 1)
	{
		for (int i = 0; i < times; i++)
		{
			Bot.SendPacket(packet);
			Bot.Sleep(ActionDelay*2);
		}
	}

	/// <summary>
	/// Rest the player until full if ShouldRest = true
	/// </summary>
	public void Rest()
	{
		if (ShouldRest)
            Bot.Player.Rest(true);
	}

	public void Relogin()
	{
		bool autoRelogSwitch = Bot.Options.AutoRelogin;
		Bot.Options.AutoRelogin = false;
		Bot.Sleep(2000);
		Logger("Relogin started");
		Bot.Player.Logout();
		Bot.Sleep(5000);
		RBot.Servers.Server server = Bot.Options.AutoReloginAny 
				? RBot.Servers.ServerList.Servers.Find(x => x.IP != RBot.Servers.ServerList.LastServerIP) 
				: RBot.Servers.ServerList.Servers.Find(s => s.IP == RBot.Servers.ServerList.LastServerIP) ?? RBot.Servers.ServerList.Servers[0];
		Bot.Player.Login(Bot.Player.Username, Bot.Player.Password);
		Bot.Player.Connect(server);
		while(!Bot.Player.LoggedIn)
			Bot.Sleep(500);
		Bot.Sleep(5000);
		Logger("Relogin finished");
		Bot.Options.AutoRelogin = autoRelogSwitch;
	}

	ClassType currentClass = ClassType.None;
	bool usingSoloGeneric = false;
	bool usingFarmGeneric = false;
	public void EquipClass(ClassType classToUse)
	{
		if(currentClass == classToUse)
			return;
		switch (classToUse)
		{
			case ClassType.Farm:
				if(usingFarmGeneric && !usingSoloGeneric)
				{
					EquipClass(ClassType.Solo);
					return;
				}
				_EquipClass(FarmClass);
				if(currentClass != ClassType.Farm)
                    Bot.Skills.StartAdvanced(FarmClassSkills, FarmClassSkillTimeout);
				break;
			default:
				if(usingSoloGeneric && !usingFarmGeneric)
				{
					EquipClass(ClassType.Farm);
					return;
				}
				_EquipClass(SoloClass);
				if(currentClass != ClassType.Solo)
                    Bot.Skills.StartAdvanced(SoloClassSkills, SoloClassSkillTimeout);
				break;
		}
		currentClass = classToUse;
	}

	private void _KillForItem(string name, string item, int quantity, bool tempItem = false, bool rejectElse = false, bool log = true)
	{
        if(log)
            Logger($"Killing {name} for {item} ({quantity}) [Temp = {tempItem}]");
		while (!Bot.ShouldExit() && !CheckInventory(item, quantity))
		{
            Bot.Player.Kill(name);
			if (!tempItem && !CheckInventory(item))
			{
                Bot.Sleep(ActionDelay);
				Bot.Player.Pickup(item);
				if (rejectElse)
					Bot.Player.RejectExcept(item);
			}
			Rest();
        }
	}

	private void _HuntForItem(string name, string item, int quantity, bool tempItem = false, bool rejectElse = false)
	{
        Logger($"Hunting {name} for {item} ({quantity}) [Temp = {tempItem}]");
		while (!Bot.ShouldExit() && !CheckInventory(item, quantity))
		{
            if (CheckInventory(item, quantity))
                return;
			Bot.Player.HuntWithPriority(name, Bot.Options.HuntPriority);
			if (!tempItem && !CheckInventory(item))
			{
                Bot.Sleep(ActionDelay);
				Bot.Player.Pickup(item);
				if (rejectElse)
					Bot.Player.RejectExcept(item);
			}
			Rest();
		}
	}

	private void _AddRequirement(int questID)
	{
		if (questID > 0)
		{
			Quest quest = Bot.Quests.EnsureLoad(questID);
			if (quest == null)
				Logger($"Quest [{questID}] doesn't exist", messageBox: true, stopBot: true);
			List<string> reqItems = new List<string>();
			quest.AcceptRequirements.ForEach(item => reqItems.Add(item.Name));
			quest.Requirements.ForEach(item =>
			{
				if (!CurrentRequirements.Where(i => i.Name == item.Name).Any())
				{
                    if(!item.Temp)
					    reqItems.Add(item.Name);
					CurrentRequirements.Add(item);
				}
			});
			AddDrop(reqItems.ToArray());
		}
	}

	private void _EquipClass(string className)
	{
		if(className.ToLower() != "generic" 
			&& Bot.Inventory.CurrentClass.Name.ToLower() != className.ToLower())
		{
			JumpWait();
			while(!Bot.Inventory.IsEquipped(className))
			{
				Bot.Player.EquipItem(className);
				Bot.Sleep(ActionDelay);
			}
			Logger($"{className} equipped");
		}
	}

	public void StopBot(bool removeStopHandler = false)
	{
		if(removeStopHandler)
			Bot.Handlers.RemoveAll(handler => handler.Name == "Stop Handler");
		Bot.Player.Join("battleon");
		if (AntiLag)
        {
            Bot.SetGameObject("stage.frameRate", 60);
            if (Bot.GetGameObject<bool>("ui.monsterIcon.redX.visible"))
                Bot.CallGameFunction("world.toggleMonsters");
        }
		Bot.Options.PrivateRooms = false;
		Bot.Options.AutoRelogin = false;
		Bot.Options.LagKiller = false;
		Bot.Options.LagKiller = true;
		Bot.Options.LagKiller = false;
		Logger("Bot Stopped Successfully", messageBox: true);
		ScriptManager.StopScript();
	}
	#endregion

	#region Map
	/// <summary>
	/// Sends a getMapItem packet for the specified item
	/// </summary>
	/// <param name="itemID">ID of the item</param>
	/// <param name="quant">Desired quantity of the item</param>
	/// <param name="map">Map where the item is</param>
	public void GetMapItem(int itemID, int quant = 1, string map = null)
	{
		if (map != null)
			Bot.Player.Join(map);
		Bot.Sleep(ActionDelay);
		for (int i = 0; i < quant; i++)
		{
			Bot.Map.GetMapItem(itemID);
			Bot.Sleep(1000);
		}
		Logger($"Map item {itemID}({quant}) acquired");
	}
	
	/// <summary>
	/// Jumps to the desired cell and set spawn point
	/// </summary>
	/// <param name="cell">Cell to jump to</param>
	/// <param name="pad">Pad to jump to</param>
	public void Jump(string cell = "Enter", string pad = "Spawn")
	{
		Bot.Player.SetSpawnPoint(cell, pad);
		if (Bot.Player.Cell == cell)
			return;
		Bot.Player.Jump(cell, pad);
	}

	/// <summary>
	/// Jump to wait room and wait <see cref="ExitCombatDelay"/>
	/// </summary>
	public void JumpWait()
	{
		if (Bot.Player.Cell != "Wait")
		{
			Bot.Player.Jump("Wait", "Spawn");
			Bot.Sleep(ExitCombatDelay);
		}
	}

	/// <summary>
	/// Joins Tercessuinotlim
	/// </summary>
	public void JoinTercessuinotlim()
	{
		if (Bot.Map.Name == "tercessuinotlim")
			return;
		Bot.Player.Join("citadel", "m22", "Left");
		if (Bot.Player.Cell != "m22")
			Bot.Player.Jump("m22", "Left");
		Bot.Player.Join("tercessuinotlim");
		Bot.Wait.ForMapLoad("tercessuinotlim");
	}

	/// <summary>
	/// This method is used to move between Bludrut Brawl rooms
	/// </summary>
	/// <param name="mtcid">Last number of the mtcid packet</param>
	/// <param name="cell">Cell you want to be</param>
	/// <param name="moveX">X position of the door</param>
	/// <param name="moveY">Y position of the door</param>
	public void BludrutMove(int mtcid, string cell, int moveX = 828, int moveY = 276)
	{
		while (Bot.Player.Cell != cell)
		{
			Bot.SendPacket($"%xt%zm%mv%{Bot.Map.RoomID}%{moveX}%{moveY}%8%");
			Bot.Sleep(1500);
			Bot.SendPacket($"%xt%zm%mtcid%{Bot.Map.RoomID}%{mtcid}%");
		}
	}
	#endregion
}

public enum Alignment
{
	Good,
	Evil,
	Chaos
}

public enum ClassType
{
	Solo,
	Farm,
	None
}