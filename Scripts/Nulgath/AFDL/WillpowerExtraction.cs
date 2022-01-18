//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class WillpowerExtraction
{
	public ScriptInterface Bot = ScriptInterface.Instance;
	public CoreBots Core => CoreBots.Instance;
	public CoreFarms Farm = new CoreFarms();
	public CoreNulgath Nulgath = new CoreNulgath();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();
		
		Unidentified34(90);

		Core.SetOptions(false);
	}

	public void Unidentified34(int quant)
	{
		if (Core.CheckInventory("Unidentified 34", quant))
			return;

		Core.AddDrop(Nulgath.bagDrops);
		Core.AddDrop(Nulgath.tercessBags);
		Core.AddDrop("Unidentified 34", "Unidentified 19", "Necrot", "Chaoroot", "Doomatter",
			"Mortality Cape of Revontheus", "Facebreakers of Nulgath", "SightBlinder Axes of Nulgath", "Mystic Tribal Sword",
			"King Klunk's Crown", "Golden Shadow Breaker", "Shadow Terror Axe");

		int i = 1;
		while (!Core.CheckInventory("Unidentified 34", quant))
		{
			Core.EnsureAccept(5258);

			if (!Core.CheckInventory("Shadow Lich"))
			{
				if (Bot.Player.Gold < 50000)
					Farm.BattleGroundE(60000);
				Core.BuyItem("shadowfall", 89, "Shadow Lich");
			}

			if (!Core.CheckInventory("Mystic Tribal Sword"))
				Core.BuyItem("arcangrove", 214, "Mystic Tribal Sword");

			Nulgath.Supplies("Unidentified 19");
			Core.EquipClass(ClassType.Farm);
			if (!Core.CheckInventory("Necrot", 5))
				Core.KillMonster("deathsrealm", "Frame2", "Left", "Skeleton Fighter", "Necrot", 5, false);

			if (!Core.CheckInventory("Chaoroot", 5))
				Core.KillMonster("hydra", "Boss", "Left", "Hydra Head", "Chaoroot", 5, false);
			Core.EquipClass(ClassType.Solo);
			if (!Core.CheckInventory("Doomatter", 5))
				Core.KillMonster("maul", "r3", "Down", "Creature Creation", "Doomatter", 5, false);

			if (!Core.CheckInventory("King Klunk's Crown"))
				Core.KillMonster("evilwarnul", "r15", "Left", "Laken", "King Klunk's Crown", 1, false);

			Nulgath.ApprovalAndFavor(0, 90);

			Nulgath.FarmTotemofNulgath(1);

			Nulgath.EssenceofNulgath(10);

			if (!Core.CheckInventory("Mortality Cape of Revontheus"))
			{
				Nulgath.ApprovalAndFavor(0, 35);
                Core.BuyItem("evilwarnul", 452, "Mortality Cape of Revontheus");
			}

			if (!Core.CheckInventory("Facebreakers of Nulgath"))
			{
				while (!Bot.Inventory.Contains("Facebreakers of Nulgath"))
				{
					Core.EnsureAccept(3046);
					Core.EquipClass(ClassType.Solo);
					Core.HuntMonster("citadel", "Grand Inquisitor", "Golden Shadow Breaker", 1, false);
					Core.HuntMonster("battleundera", "Bone Terror", "Shadow Terror Axe", 1, false);
					Nulgath.FarmUni13(2);
					Nulgath.FarmDarkCrystalShard(5);
					Nulgath.SwindleBulk(5);
					Nulgath.FarmDiamondofNulgath(1);
					Core.EnsureComplete(3046);
					Bot.Player.Pickup("Facebreakers of Nulgath", "SightBlinder Axes of Nulgath");
					Bot.Sleep(Core.ActionDelay);
				}
			}
			Nulgath.FarmUni13();

			if (!Bot.Quests.CanComplete(5258))
				Bot.Player.Logout();
			Core.EnsureComplete(5258);
			Bot.Player.Pickup("Unidentified 34");

			Core.Logger($"Completed x{i}");
			i++;
		}
	}
}