using RBot;
using RBot.Items;
using RBot.Monsters;
using RBot.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

public class CoreBots
{
	private static CoreBots _instance;
	public static CoreBots Instance => _instance ?? (_instance = new CoreBots());
	public ScriptInterface Bot => ScriptInterface.Instance;
	/// <summary>
	/// Delay between commom actions, 700 is the safe number
	/// </summary>
	public int ActionDelay { get; set; } = 700;
	/// <summary>
	/// Delay used to get out of combat, 1500 is the safe number
	/// </summary>
	public int ExitCombatDelay { get; set; } = 1600;

	/// <summary>
	/// If true will wait till the player is fully rested after killing monsters
	/// </summary>
	public bool ShouldRest { get; set; } = false;

	public int HeroAlignment { get; set; } = (int)Alignment.Evil;

	/// <summary>
	/// Whether the player is Member
	/// </summary>
	public bool IsMember => ScriptInterface.Instance.Player.IsMember;

	public List<ItemBase> CurrentRequirements = new List<ItemBase>();

	/// <summary>
	/// Set commom bot options to desired value
	/// </summary>
	/// <param name="changeTo">Value the options will be changed to</param>
	public void SetOptions(bool changeTo = true)
	{
		// Common Options
		Bot.Options.LagKiller = changeTo;
		Bot.Options.SafeTimings = changeTo;
		Bot.Options.RestPackets = changeTo;
		Bot.Options.AutoRelogin = changeTo;
		Bot.Options.PrivateRooms = changeTo;
		Bot.Options.InfiniteRange = changeTo;
		Bot.Options.SkipCutscenes = changeTo;
		Bot.Options.ExitCombatBeforeQuest = changeTo;
		Bot.Drops.RejectElse = changeTo;
		Bot.Drops.Interval = 500;
		Bot.Lite.UntargetDead = changeTo;
		Bot.Lite.UntargetSelf = changeTo;
		Bot.Lite.Set("bReaccept", false);

		// Anti-lag option
		Bot.SetGameObject("stage.frameRate", 10);
		if (!Bot.GetGameObject<bool>("ui.monsterIcon.redX.visible"))
			Bot.CallGameFunction("world.toggleMonsters");

		//Uncomment to use AutoRelogin to restart the script when the player goes AFK, it should not be necessary
		//Bot.Options.AutoRelogin = changeTo;
		//void AFKHandler(ScriptInterface b)
		//{
		//	Logger("Player AFK, triggering logout");
		//	b.Events.PlayerAFK -= AFKHandler;
		//	b.Player.Logout();
		//}
		//Bot.Events.PlayerAFK += AFKHandler;

		// Uncomment and use any skill set you want, by default it will use Generic.xml
		//Bot.Skills.LoadSkills("Skills/---.xml");

		if (changeTo)
		{
			Logger("Bot Started");
			Bot.Player.LoadBank();
			Bot.Runtime.BankLoaded = true;
			Bot.Skills.StartTimer();
			Bot.RegisterHandler(2, b => {
				if (b.ShouldExit())
				{
					b.SetGameObject("stage.frameRate", 30);
					b.Options.LagKiller = false;
					b.Options.LagKiller = true;
					b.Options.LagKiller = false;
					if (b.GetGameObject<bool>("ui.monsterIcon.redX.visible"))
						b.CallGameFunction("world.toggleMonsters");
				}
			});
		}
		else
		{
			Bot.Skills.StopTimer();
			Bot.SetGameObject("stage.frameRate", 30);
			Bot.Options.LagKiller = false;
			Bot.Options.LagKiller = true;
			Bot.Options.LagKiller = false;
			if (Bot.GetGameObject<bool>("ui.monsterIcon.redX.visible"))
				Bot.CallGameFunction("world.toggleMonsters");
			Logger("Bot Stopped", messageBox: true);
		}
	}

	#region Inventory, Bank and Shop
	/// <summary>
	/// Check the Bank and Inventory for the item
	/// </summary>
	/// <param name="item">Name of the item</param>
	/// <param name="quant">Desired quantity</param>
	/// <param name="toInv">Whether or not send the item to Inventory</param>
	/// <returns>Returns whether the item exists in the desired quantity in the bank and inventory</returns>
	public bool CheckInventory(string item, int quant = 1, bool toInv = true)
	{
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
	/// <returns>Returns whether the item exists in the desired quantity in the bank and inventory</returns>
	public bool CheckInventory(int itemID, int quant = 1, bool toInv = true)
	{
		InventoryItem item = Bot.Bank.BankItems.Find(i => i.ID == itemID);
		if (item == null)
			return false;
		if (Bot.Bank.Contains(item.Name))
		{
			if (!toInv)
				return true;
			Unbank(item.Name);
		}
		if (Bot.Inventory.Contains(item.Name, quant))
			return true;
		return false;
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
			Bot.Sleep(ActionDelay);
			Bot.Runtime.Require(item);
			if (Bot.Bank.Contains(item))
			{
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
		Bot.Player.Join(map);
		Bot.Shops.Load(shopID);
		RBot.Shops.ShopItem item = Bot.Shops.ShopItems.First(shopitem => shopitem.Name == itemName);
		if(quant > 1)
		{
			if(Bot.Inventory.GetQuantity(itemName) + shopQuant > item.MaxStack)
                return;
            int quantB = quant - Bot.Inventory.GetQuantity(itemName);
			if(quantB < 0)
                return;
			decimal quantF = (decimal)quantB / (decimal)shopQuant;
            quant = (int)Math.Ceiling(quantF);
		}
		if(shopItemID == 0)
        	for (int i = 0; i < quant; i++)
                Bot.Shops.BuyItem(shopID, itemName);
		else
		{
            SendPackets($"%xt%zm%buyItem%{Bot.Map.RoomID}%{item.ID}%{shopID}%{shopItemID}%", quant);
            if(!Bot.Options.AutoRelogin)
                Bot.Options.AutoRelogin = true;
            Bot.Sleep(2000);
			Logger($"Triggering Auto Relogin to prevent ghost buy");
            Bot.Player.Logout();
        }
        Logger($"Bought {quant} {itemName}");
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
        Bot.Player.Join(map);
        Bot.Shops.Load(shopID);
        RBot.Shops.ShopItem item = Bot.Shops.ShopItems.First(shopitem => shopitem.ID == itemID);
        if(quant > 1)
		{
			int quantB = quant - Bot.Inventory.GetQuantity(item.Name);
            if(quantB < 0)
                return;
            decimal quantF = (decimal)quantB / (decimal)shopQuant;
            quant = (int)Math.Ceiling(quantF);
        }
		if(shopItemID == 0)
        	for (int i = 0; i < quant; i++)
            	Bot.Shops.BuyItem(shopID, itemID);
		else
		{
			SendPackets($"%xt%zm%buyItem%{Bot.Map.RoomID}%{item.ID}%{shopID}%{shopItemID}%", quant);
            if(!Bot.Options.AutoRelogin)
                Bot.Options.AutoRelogin = true;
			Bot.Sleep(2000);
            Logger($"Triggering Auto Relogin to prevent ghost buy");
            Bot.Player.Logout();
        }
        Logger($"Bought {quant} {item.Name}");
    }
	#endregion

	#region Drops

	/// <summary>
	/// Adds drops to the pickup list and restart the Drop Grabber
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
		return Bot.Quests.EnsureAccept(questID, 20);
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
			Bot.Quests.EnsureAccept(quest, 20);
		}
	}

	/// <summary>
	/// Ensures you are out of combat before completing the quest
	/// </summary>
	/// <param name="questID">ID of the quest to complete</param>
	/// <param name="itemID">ID of the choosable reward item</param>
	public bool EnsureComplete(int questID, int itemID = -1)
	{
		if (!Bot.Quests.CanComplete(questID) || questID <= 0)
			return false;
		JumpWait();
		Bot.Sleep(ActionDelay);
		return Bot.Quests.EnsureComplete(questID, itemID, tries: 20);
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
			if (!Bot.Quests.CanComplete(quest) || quest <= 0)
				continue;
			Bot.Sleep(ActionDelay);
			Bot.Quests.EnsureComplete(quest, tries: 20);
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
		Bot.Quests.EnsureAccept(questID, 20);
		Bot.Quests.EnsureComplete(questID, itemID, tries: 20);
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
		AddRequirements(questID);
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
		AddRequirements(questID);
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
					if(j == 0 && (Bot.Inventory.Contains(CurrentRequirements[i].Name, CurrentRequirements[i].Quantity) || Bot.Inventory.ContainsTempItem(CurrentRequirements[i].Name, CurrentRequirements[i].Quantity)))
					{
						CurrentRequirements.RemoveAt(i);
						continue;
					}
					if (j != 0 && (Bot.Inventory.Contains(CurrentRequirements[i].Name) || Bot.Inventory.ContainsTempItem(CurrentRequirements[i].Name)))
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
        Logger($"Hunting {monster} for {itemName} ({quantity})[{isTemp}]");
		Bot.Player.HuntForItem(monster, itemName, quantity, isTemp);
		CurrentRequirements.RemoveAt(index);
		shouldRepeat = false;
    }

    private bool _RepeatCheck(ref bool shouldRepeat, int index)
	{
		if(Bot.Inventory.Contains(CurrentRequirements[index].Name, CurrentRequirements[index].Quantity) 
			|| Bot.Inventory.ContainsTempItem(CurrentRequirements[index].Name, CurrentRequirements[index].Quantity))
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
	public void KillMonster(string map, string cell, string pad, string monster, string item = null, int quant = 1, bool isTemp = true)
	{
		if (item != null && isTemp && Bot.Inventory.ContainsTempItem(item, quant))
			return;
		if (item != null && !isTemp && Bot.Inventory.Contains(item, quant))
			return;
		Bot.Player.Join(map);
		Jump(cell, pad);
		if (item == null)
		{
			Logger($"Killing {monster}");
			Bot.Player.Kill(monster);
		}
		else
		{
			Logger($"Killing {monster} for {item} ({quant}) [Temp = {isTemp}]");
			_KillForItem(monster, item, quant, isTemp);
		}
	}

	/// <summary>
	/// Kills a monster using it's name
	/// </summary>
	/// <param name="map">Map to join</param>
	/// <param name="cell">Cell to jump to</param>
	/// <param name="pad">Pad to jump to</param>
	/// <param name="monsterID">ID of the monster</param>
	/// <param name="item">Item to kill the monster for, if null will just kill the monster 1 time</param>
	/// <param name="quant">Desired quantity of the item</param>
	/// <param name="isTemp">Whether the item is temporary</param>
	public void KillMonster(string map, string cell, string pad, int monsterID, string item = null, int quant = 1, bool isTemp = true)
	{
		if (item != null && isTemp && Bot.Inventory.ContainsTempItem(item, quant))
			return;
		if (item != null && !isTemp && Bot.Inventory.Contains(item, quant))
			return;
		Bot.Player.Join(map);
		Jump(cell, pad);
		Monster monster = Bot.Monsters.CurrentMonsters.Find(m => m.ID == monsterID);
		if (item == null)
			Bot.Player.Kill(monster);
		else
		{
			while (!Bot.ShouldExit()
				&& (isTemp || !Bot.Inventory.Contains(item, quant))
				&& (!isTemp || !Bot.Inventory.ContainsTempItem(item, quant)))
			{
				Bot.Player.Kill(monster);
				Bot.Player.Pickup(item);
				Bot.Sleep(ActionDelay);
			}
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
		if (item != null && isTemp && Bot.Inventory.ContainsTempItem(item, quant))
			return;
		if (item != null && !isTemp && Bot.Inventory.Contains(item, quant))
			return;
		Bot.Player.Join(map);
		if (item == null)
		{
			Logger("Hunting {monster}");
			Bot.Player.Hunt(monster);
		}
		else
		{
			Logger("Hunting {monster} for {item} ({quant}) [Temp = {isTemp}]");
			Bot.Player.HuntForItem(monster, item, quant, isTemp);
		}
	}

	/// <summary>
	/// Kill Escherion for the desired item
	/// </summary>
	/// <param name="item">Item name</param>
	/// <param name="quant">Desired quantity</param>
	/// <param name="isTemp">Whether the item is temporary</param>
	/// <param name="removeHandler">Whether remove the handler for Staff of Inversion after finished</param>
	public void KillEscherion(string item = null, int quant = 1, bool isTemp = false, bool removeHandler = true)
	{
		if(!Bot.Handlers.Where(h => h.Name == "escherion").Any())
		{
			Bot.RegisterHandler(5, b =>
			{
				if (b.Monsters.CurrentMonsters.Where(m => m.Alive).Count() > 1)
					b.Player.Kill("Staff of Inversion");
			}, "escherion");
		}
		if (item == null)
		{
			Logger("Killing Escherion");
			Bot.Player.Kill("Escherion");
		}
		else
		{
			Logger("Killing Escherion for {item} ({quant}) [Temp = {isTemp}]");
			KillMonster("escherion", "Boss", "Left", "Escherion", item, quant, isTemp);
		}
		if(removeHandler)
			Bot.Handlers.RemoveAll(h => h.Name == "escherion");
	}
	#endregion

	/// <summary>
    /// Will add any requirements from the specified quest to the CurrentRequirements list. Be sure to clear the list after completing the quest if you are not using the SmartKillMonster method
    /// </summary>
    /// <param name="questID">ID of the quest</param>
	public void AddRequirements(int questID)
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
                    reqItems.Add(item.Name);
					CurrentRequirements.Add(item);
				}
			});
            AddDrop(reqItems.ToArray());
		}
	}

	/// <summary>
	/// Logs a line of text to the script log with time, method from where it's called and a message
	/// </summary>
	public void Logger(string message = "", [CallerMemberName] string caller = null, bool messageBox = false, bool stopBot = false)
	{
		Bot.Log($"[{DateTime.Now:HH:mm:ss}] ({caller})  {message}");
		if (messageBox)
			Message(message, caller);
		if (stopBot)
			ScriptManager.StopScript();
	}

	/// <summary>
	/// Creates a Message Box with the desired text and caption
	/// </summary>
	/// <param name="message">Message to display</param>
	/// <param name="caption">Title of the box</param>
	public void Message(string message, string caption) => MessageBox.Show(message, caption);

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
	/// Sends a getMapItem packet for the specified item
	/// </summary>
	/// <param name="itemID">ID of the item</param>
	/// <param name="quant">Desired quantity of the item</param>
	/// <param name="map">Map where the item is</param>
	public void GetMapItem(int itemID, int quant = 1, string map = null)
	{
		if (map != null)
			Bot.Player.Join(map);
		for (int i = 0; i < quant; i++)
		{
			Bot.Map.GetMapItem(itemID);
			Bot.Sleep(ActionDelay);
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
		if (Bot.Player.Cell == cell)
			return;
		Bot.Player.Jump(cell, pad);
		Bot.Player.SetSpawnPoint(cell, pad);
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

	private void _KillForItem(string name, string item, int quantity, bool tempItem = false, bool rejectElse = false)
	{
		while (!Bot.ShouldExit()
			&& (tempItem || !Bot.Inventory.Contains(item, quantity))
			&& (!tempItem || !Bot.Inventory.ContainsTempItem(item, quantity)))
		{
			Bot.Player.Kill(name);
			Bot.Player.Pickup(item);
			Rest();
		}
	}

	/// <summary>
    /// Rest the player until full if ShouldRest = true
    /// </summary>
	public void Rest()
	{
		if (Bot.Player.Mana < 30 && ShouldRest)
		{
			Bot.Player.Rest(true);
			while (Bot.Player.Mana < 90)
				Bot.Sleep(2000);
		}
	}
}

public enum Alignment
{
	Good,
	Evil,
	Chaos
}