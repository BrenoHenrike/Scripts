//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Nulgath/AFDL/WillpowerExtraction.cs
using RBot;

public class NulgathDemandsWork
{
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core = new CoreBots();
	public CoreFarms Farm = new CoreFarms();
	public CoreNulgath Nulgath = new CoreNulgath();

	public WillpowerExtraction WillpowerExtraction = new WillpowerExtraction();
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Unidentified35();

		Core.SetOptions(false);
	}

	public void Unidentified35()
	{
		if (Core.CheckInventory("Unidentified 35"))
			return;

		Core.AddDrop(Nulgath.bagDrops);
		Core.AddDrop("Unidentified 35", "Archfiend Essence Fragment", "Unidentified 27", "Unidentified 26",
			"Golden Hanzo Void", "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction");

		int i = 0;
		while (!Core.CheckInventory("DoomLord's War Mask", toInv: false) && !Core.CheckInventory("ShadowFiend Cloak", toInv: false)
			&& !Core.CheckInventory("Locks of the DoomLord", toInv: false) && !Core.CheckInventory("Doomblade of Destruction", toInv: false)
			&& !Core.CheckInventory("Unidentified 35"))
		{
			if (Core.CheckInventory("Archfiend Essence Fragment", 9))
				break;

			WillpowerExtraction.Unidentified34(10);

			Nulgath.FarmUni13(2);

			Core.EnsureAccept(5259);

			Nulgath.FarmBloodGem(2);

			Nulgath.FarmDiamondofNulgath(60);

			Nulgath.FarmDarkCrystalShard(45);

			Nulgath.FarmVoucher(false);

			Nulgath.FarmGemofNulgath(15);

			Nulgath.SwindleBulk(50);

			if (!Core.CheckInventory("Unidentified 27"))
			{
				Nulgath.Supplies("Unidentified 26");
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

			if (!Bot.Quests.CanComplete(5259))
				Bot.Player.Logout();
			else if (!Core.CheckInventory("DoomLord's War Mask", toInv: false))
				Core.EnsureComplete(5259, 35399);
			else if (!Core.CheckInventory("ShadowFiend Cloak", toInv: false))
				Core.EnsureComplete(5259, 35400);
			else if (!Core.CheckInventory("Locks of the DoomLord", toInv: false))
				Core.EnsureComplete(5259, 35401);
			else if (!Core.CheckInventory("Doomblade of Destruction", toInv: false))
				Core.EnsureComplete(5259, 35398);
			else
				Core.EnsureComplete(5259, 35399);
			Bot.Player.Pickup("DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction", "Unidentified 35", "Archfiend Essence Fragment");

			Core.Logger($"Completed x{i}");
			i++;
		}

		if(Core.CheckInventory("Archfiend Essence Fragment", 9))
		{
			Nulgath.JoinTercessuinotlim();
			Bot.Player.Jump("Swindle", "Left");
			Bot.Shops.Load(1951);
			Bot.Sleep(1000);
			Bot.SendPacket("%xt%zm%buyItem%119981%35770%1951%7912%");
		}
		
	}
}