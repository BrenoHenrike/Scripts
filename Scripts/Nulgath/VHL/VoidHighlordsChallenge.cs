//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class VoidHighlordsChallenge
{
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core => CoreBots.Instance;
	public CoreNulgath Nulgath = new CoreNulgath();
	public CoreDailys Dailys = new CoreDailys();
	public CoreFarms Farm = new CoreFarms();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Core.AddDrop(Nulgath.bagDrops);
		Core.AddDrop(Nulgath.VHLDrops);

		if(!Core.CheckInventory("Hadean Onyx of Nulgath"))
		{
			Nulgath.JoinTercessuinotlim();
			Core.Jump("r13", "Right");
			Bot.Player.KillForItem("Shadow of Nulgath", "Hadean Onyx of Nulgath", 1);
		}

		Nulgath.FarmVoucher(false);

		Core.EnsureAccept(5660);

		BlackKnightOrb();
		
		if (!Core.CheckInventory("Dwakel Decoder"))
            Core.GetMapItem(106, map: "crashsite");
		
		if(!Core.CheckInventory("Nulgath Shaped Chocolate"))
		{
			if (bot.Player.Gold < 2000000)
				Farm.BattleGroundE(2000000);
            Core.BuyItem("citadel", 44, 38316);
		}
		
		if (!Core.CheckInventory("The Secret 1"))
		{
			Core.EnsureAccept(623);
			Core.HuntMonster("willowcreek", "Hidden Spy", "The Secret 1", isTemp: false);
		}
		
		Dailys.EldersBlood();

        Core.BuyItem("yulgar", 16, "Aelita's Emerald");

		Nulgath.FarmUni13();

		if (!Core.CheckInventory("Elemental Ink", 10))
		{
			if(!Core.CheckInventory("Mystic Quills", 4))
				Core.HuntMonster("mobius", "Slugfit", "Mystic Quills", 4, false);
            Core.BuyItem("dragonrune", 549, 13284, 10, 5);
		}

		Nulgath.FarmGemofNulgath(20);

		if (!Core.CheckInventory("Bone Dust", 20))
			Core.KillMonster("battleunderb", "Enter", "Spawn", "*", "Bone Dust", 20, false);

		Nulgath.EmblemofNulgath(20);

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