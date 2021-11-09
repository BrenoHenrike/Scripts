using RBot;
using RBot.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public class CoreBots
{
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
	/// Whether the player is Member
	/// </summary>
	public bool IsMember => ScriptInterface.Instance.Player.IsMember;

	/// <summary>
	/// Set commom bot options to desired value
	/// </summary>
	/// <param name="changeTo">Value the options will be changed to</param>
	public void SetOptions(bool changeTo = true)
	{
		Bot.Options.SafeTimings = changeTo;
		Bot.Options.RestPackets = changeTo;
		Bot.Options.InfiniteRange = changeTo;
		Bot.Options.ExitCombatBeforeQuest = changeTo;
		Bot.Options.SkipCutscenes = changeTo;
		Bot.Drops.RejectElse = changeTo;
		//Bot.Options.LagKiller = changeTo;
		Bot.Lite.UntargetDead = changeTo;
		Bot.Lite.UntargetSelf = changeTo;
		Bot.Lite.Set("bReaccept", false);

		Bot.Drops.Interval = 500;

		//Uncomment to use AutoRelogin to restart the script when the player goes AFK, it should not be necessary
		//Bot.Options.AutoRelogin = changeTo;
		//void AFKHandler(ScriptInterface b)
		//{
		//	Logger("Player AFK, triggering logout");
		//	b.Events.PlayerAFK -= AFKHandler;
		//	b.Player.Logout();
		//}
		//Bot.Events.PlayerAFK += AFKHandler;

		if (changeTo)
			Bot.Player.LoadBank();

		//Bot.Skills.LoadSkills("Skills/---.xml");
		if (changeTo)
			Bot.Skills.StartTimer();
		else
			Bot.Skills.StopTimer();
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
			Bot.Sleep(ActionDelay);
		}
	}

	/// <summary>
	/// Buys a item till it have the desired quantity
	/// </summary>
	/// <param name="map">Map of the shop</param>
	/// <param name="shopID">ID of the shop</param>
	/// <param name="itemName">Name of the item</param>
	/// <param name="quant">Desired quantity</param>
	public void BuyItem(string map, int shopID, string itemName, int quant = 1)
	{
		if (CheckInventory(itemName, quant))
			return;
		Bot.Player.Join(map);
		int quantB = quant - Bot.Inventory.GetQuantity(itemName);
		quant = quantB < 0 ? quant : quantB;
		for (int i = 0; i < quant; i++)
		{
			Bot.Shops.BuyItem(shopID, itemName);
			Bot.Sleep(ActionDelay);
		}
	}

	/// <summary>
	/// Buys a item till it have the desired quantity
	/// </summary>
	/// <param name="map">Map of the shop</param>
	/// <param name="shopID">ID of the shop</param>
	/// <param name="itemID">ID of the item</param>
	/// <param name="quant">Desired quantity</param>
	public void BuyItem(string map, int shopID, int itemID, int quant = 1)
	{
		if (CheckInventory(itemID, quant))
			return;
		InventoryItem item = Bot.Inventory.Items.Find(i => i.ID == itemID);
		Bot.Player.Join(map);
		int quantB = quant - Bot.Inventory.GetQuantity(item == null ? "any" : item.Name);
		quant = quantB < 0 ? quant : quantB;
		for (int i = 0; i < quant; i++)
		{
			Bot.Shops.BuyItem(shopID, itemID);
			Bot.Sleep(ActionDelay);
		}
	}

	#region Inventory
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
			if (Bot.Quests.IsInProgress(quest))
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
			Bot.Player.Kill(monster);
		else
			Bot.Player.KillForItem(monster, item, quant, isTemp);
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
		if (item == null)
			Bot.Player.Kill(Bot.Monsters.CurrentMonsters.Find(m => m.ID == monsterID));
		else
		{
			if(isTemp)
				while (Bot.Inventory.ContainsTempItem(item, quant))
				{
					if (Bot.Inventory.ContainsTempItem(item, quant))
						break;
					Bot.Player.Kill(Bot.Monsters.CurrentMonsters.Find(m => m.ID == 2908));
					Bot.Sleep(ActionDelay);
				}
			else
				while (Bot.Inventory.Contains(item, quant))
				{
					if (Bot.Inventory.Contains(item, quant))
						break;
					Bot.Player.Kill(Bot.Monsters.CurrentMonsters.Find(m => m.ID == 2908));
					Bot.Sleep(ActionDelay);
					if(Bot.Player.DropExists(item))
						Bot.Player.Pickup(item);
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
			Bot.Player.Hunt(monster);
		else
			Bot.Player.HuntForItem(monster, item, quant, isTemp);
	}

	/// <summary>
	/// Kill Escherion for the desired item
	/// </summary>
	/// <param name="item">Item name</param>
	/// <param name="quant">Desired quantity</param>
	/// <param name="isTemp">Whether the item is temporary</param>
	/// <param name="removeHandler">Whether remove the handler for Staff of Inversion after finished</param>
	public void KillEscherion(string item, int quant = 1, bool isTemp = false, bool removeHandler = true)
	{
		if(!Bot.Handlers.Where(h => h.Name == "escherion").Any())
		{
			Bot.RegisterHandler(5, b =>
			{
				if (b.Monsters.CurrentMonsters.Where(m => m.Alive).Count() > 1)
					b.Player.Kill("Staff of Inversion");
			}, "escherion");
		}
		KillMonster("escherion", "Boss", "Left", "Escherion", item, quant, isTemp);
		if(removeHandler)
			Bot.Handlers.RemoveAll(h => h.Name == "escherion");
	}
	#endregion

	/// <summary>
	/// Logs a line of text to the script log with time, method from where it's called and a message
	/// </summary>
	public void Logger(string message = "", [CallerMemberName] string caller = null) => Bot.Log($"[{DateTime.Now:HH:mm:ss}] ({caller})  {message}");
	
	/// <summary>
	/// Sends a getMapItem packet for the specified item
	/// </summary>
	/// <param name="itemID">ID of the item</param>
	/// <param name="quant">Desired quantity of the item</param>
	public void GetMapItem(int itemID, int quant = 1)
	{
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
		Bot.Player.SetSpawnPoint();
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
}