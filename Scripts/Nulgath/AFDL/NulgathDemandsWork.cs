//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class NulgathDemandsWork
{
	public CoreBots Core = new CoreBots();
	public CoreNulgath Nulgath = new CoreNulgath();
	public CoreFarms Farm = new CoreFarms();
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Core.AddDrop(Nulgath.bagDrops);
		Core.AddDrop("Unidentified 35", "Archfiend Essence Fragment", "Unidentified 27", "Unidentified 26", 
			"Golden Hanzo Void", "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction");
		int i = 1;
		while (bot.Inventory.GetQuantity("Unidentified 34") >= 10)
		{
			Nulgath.FarmUni13(2);

			Core.EnsureAccept(5259);

			Nulgath.FarmBloodGem(2);

			Nulgath.FarmDiamondofNulgath(60);

			Nulgath.FarmDarkCrystalShard(45);

			Nulgath.FarmVoucher(false);

			Nulgath.FarmGemofNulgath(15);

			Nulgath.SwindleBulk(50);

			Nulgath.Supplies("Unidentified 26");
			Core.Logger("All miscs collected");

			if (!Core.CheckInventory("Unidentified 27"))
			{
				Core.EnsureAccept(584);
				Core.HuntMonster("evilmarsh", "Dark Makai", "Dark Makai Sigil", 1);
				Core.EnsureComplete(584);
				Core.Logger("Uni 27 acquired");
			}

			if (!Core.CheckInventory("Golden Hanzo Void"))
			{
				Nulgath.ApprovalAndFavor(50, 200);
				Farm.BattleGroundE(100000);
				Core.BuyItem("evilwarnul", 456, "Golden Hanzo Void");
				Core.Logger("Golden Hanzo Void bought");
			}

			if (!Core.CheckInventory("DoomLord's War Mask", toInv: false))
				Core.EnsureComplete(5259, 35399);
			else if (!Core.CheckInventory("ShadowFiend Cloak", toInv: false))
				Core.EnsureComplete(5259, 35400);
			else if (!Core.CheckInventory("Locks of the DoomLord", toInv: false))
				Core.EnsureComplete(5259, 35401);
			else if (!Core.CheckInventory("Doomblade of Destruction", toInv: false))
				Core.EnsureComplete(5259, 35398);
			else
				Core.EnsureComplete(5259, 35399);
			bot.Player.Pickup("DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction", "Unidentified 35", "Archfiend Essence Fragment");

			Core.Logger($"Completed x{i}");
			i++;
		}

		Core.SetOptions(false);
	}
}