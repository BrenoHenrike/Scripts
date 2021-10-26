//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarm.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class WillpowerExtraction
{
	public ScriptInterface Bot = ScriptInterface.Instance;
	public CoreBots Core = new CoreBots();
	public CoreFarm Farm = new CoreFarm();
	public CoreNulgath Nulgath = new CoreNulgath();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Core.AddDrop(Nulgath.bagDrops);
		Core.AddDrop(Nulgath.tercessBags);
		Core.AddDrop("Unidentified 34", "Unidentified 19", "Necrot", "Chaoroot", "Doomatter",
			"Mortality Cape of Revontheus", "Facebreaker of Nulgath", "SightBlinder Axe of Nulgath", "Mystic Tribal Sword", "King Klunk's Crown");
		int i = 1;
		while (!bot.Inventory.Contains("Unidentified 34", 90))
		{
			Unidentified34();
			Core.Logger($"Completed x{i}");
			i++;
		}

		Core.SetOptions(false);
	}

	public void Unidentified34()
	{
		Core.EnsureAccept(5258);

		if (!Core.CheckInventory("Shadow Lich"))
		{
			if (Bot.Player.Gold < 50000)
				Farm.BattleGroundE(60000);
			Bot.Player.Join("Shadowfall");
			Bot.Shops.BuyItem(89, "Shadow Lich");
		}

		if (!Core.CheckInventory("Mystic Tribal Sword"))
		{
			Bot.Player.Join("arcangrove");
			Bot.Shops.BuyItem(214, "Mystic Tribal Sword");
		}

		if (Core.CheckInventory(Nulgath.CragName))
			Nulgath.BambloozevsDrudgen("Unidentified 19");
		else
			Nulgath.Supplies("Unidentified 19");

		if (!Core.CheckInventory("Necrot", 5))
			Core.KillMonster("deathsrealm", "Frame2", "Left", "Skeleton Fighter", "Necrot", 5, false);

		if (!Core.CheckInventory("Chaoroot", 5))
			Core.KillMonster("hydra", "Boss", "Left", "Hydra Head", "Chaoroot", 5, false);

		if (!Core.CheckInventory("Doomatter", 5))
			Core.KillMonster("maul", "r3", "Down", "Creature Creation", "Doomatter", 5, false);

		if (!Core.CheckInventory("King Klunk's Crown"))
			Core.KillMonster("evilwarnul", "r15", "Left", "Laken", "King Klunk's Crown", 1, false);

		Nulgath.ApprovalAndFavor(0, 90);

		Nulgath.EssenceofNulgath(70);

		if (!Core.CheckInventory("Totem of Nulgath"))
			Nulgath.VoucherItemTotemofNulgath(ChooseReward.TotemofNulgath);

		if (!Core.CheckInventory("Mortality Cape of Revontheus"))
		{
			Nulgath.ApprovalAndFavor(35);
			Bot.Player.Join("evilwarnul");
			Bot.Player.Jump("Enter", "Spawn");
			Bot.Shops.BuyItem(452, "Mortality Cape of Revontheus");
			Bot.Wait.ForItemBuy();
		}

		if (!Core.CheckInventory("Facebreaker of Nulgath"))
		{
			while (!Bot.Inventory.Contains("Facebreaker of Nulgath"))
			{
				Core.EnsureAccept(3046);
				Core.HuntMonster("citadel", "Grand Inquisitor", "Golden Shadow Breaker", 1, false);
				Core.HuntMonster("battleundera", "Bone Terror", "Shadow Terror Axe", 1, false);
				Nulgath.FarmUni13();
				Nulgath.SwindleBulk(5);
				Nulgath.EssenceofDefeatReagent(5);
				//Nulgath.Supplies("Dark Crystal Shard", 5);
				Nulgath.DiamondEvilWar(1);
				Core.EnsureComplete(3046);
				Bot.Player.Pickup("Facebreaker of Nulgath", "SightBlinder Axe of Nulgath");
				Bot.Sleep(Core.ActionDelay);
			}
		}

		Core.EnsureComplete(5258);
		Bot.Player.Pickup("Unidentified 34");
	}
}