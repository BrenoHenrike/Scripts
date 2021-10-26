//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;
using System.Collections.Generic;

public class VoidHighlordsChallenge
{
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core = new CoreBots();
	public CoreNulgath Nulgath = new CoreNulgath();
	public CoreDailys Dailys = new CoreDailys();
	public CoreFarms Farm = new CoreFarms();

	List<string> drops = new List<string>();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		drops.AddRange(Nulgath.bagDrops);
		drops.AddRange(Nulgath.VHLDrops);

		Core.AddDrop(drops.ToArray());

		if(!Core.CheckInventory("Hadean Onyx of Nulgath"))
		{
			Nulgath.JoinTercessuinotlim();
			Core.Jump("r13", "Right");
			Bot.Player.KillForItem("Shadow of Nulgath", "Hadean Onyx of Nulgath", 1);
		}

		if (!Core.CheckInventory("Voucher of Nulgath (non-mem)"))
		{
			if (Core.CheckInventory("Bounty Hunter's Drone Pet") || Core.CheckInventory("Nulgath's Birthday Gift"))
				Nulgath.NewWorldsNewOpportunities("Voucher of Nulgath (non-mem)");
			else
				Nulgath.Supplies("Voucher of Nulgath (non-mem)");
		}

		Core.EnsureAccept(5660);

		BlackKnightOrb();
		
		if (!Core.CheckInventory("Dwakel Decoder"))
		{
			bot.Player.Join("crashsite");
			bot.Map.GetMapItem(106); 
		}
		
		if(!Core.CheckInventory("Nulgath Shaped Chocolate"))
		{
			if (bot.Player.Gold < 2000000)
				Farm.BattleGroundE(2000000);
			bot.Player.Join("citadel");
			bot.Shops.BuyItem(44, 38316);
			bot.Wait.ForItemBuy();
		}
		
		if (!Core.CheckInventory("The Secret 1"))
		{
			Core.EnsureAccept(623);
			Core.HuntMonster("willowcreek", "Hidden Spy", "The Secret 1", 1, false);
		}
		
		Dailys.EldersBlood();

		if (!Core.CheckInventory("Aelita's Emerald"))
		{
			bot.Player.Join("yulgar");
			bot.Shops.Load(16);
			bot.Wait.ForActionCooldown(ScriptWait.GameActions.LoadShop);
			bot.SendPacket("%xt%zm%buyItem%150738%40660%16%23790%");
			bot.Sleep(1500);
		}

		Nulgath.FarmUni13();

		if (!Core.CheckInventory("Elemental Ink", 10))
		{
			if(!Core.CheckInventory("Mystic Quills", 4))
				Core.HuntMonster("mobius", "Slugfit", "Mystic Quills", 4, false);
			bot.Player.Join("dragonrune");
			bot.Shops.BuyItem(549, 13284);
			bot.Wait.ForItemBuy();
			if(!bot.Inventory.Contains("Elemental Ink", 10))
				bot.Shops.BuyItem(549, 13284);
		}

		if (!Core.CheckInventory("Gem of Nulgath", 20))
		{
			Nulgath.VoucherItemTotemofNulgath(ChooseReward.GemofNulgath);
			if (!bot.Inventory.Contains("Gem of Nulgath", 20))
				Nulgath.VoucherItemTotemofNulgath(ChooseReward.GemofNulgath);
		}

		if (!Core.CheckInventory("Bone Dust", 20))
			Core.KillMonster("battleunderb", "Enter", "Spawn", "*", "Bone Dust", 20, false);

		Nulgath.EmblemofNulgath(19);

		Nulgath.EssenceofNulgath(50);

		Nulgath.SwindleBulk(100);

		Nulgath.ApprovalAndFavor(300, 300);
		
		bot.Sleep(Core.ActionDelay);
		if (Bot.Quests.CanComplete(5660))
			Core.EnsureComplete(5660);
		else
			Core.Logger("Couldn't complete the quest");

		Core.SetOptions(false);
	}

	public void BlackKnightOrb()
	{
		if (Core.CheckInventory("Black Knight Orb"))
			return;
		Core.EnsureAccept(318);
		Core.HuntMonster("well", "Gell Oh No", "Black Knight Leg Piece");
		Core.HuntMonster("greendragon", "Greenguard Dragon", "Black Knight Chest Piece");
		Core.HuntMonster("deathgazer", "DeathGazer", "Black Knight Shoulder Piece");
		Core.HuntMonster("trunk", "Greenguard Basilisk", "Black Knight Arm Piece");
		Core.EnsureComplete(318);
	}
}