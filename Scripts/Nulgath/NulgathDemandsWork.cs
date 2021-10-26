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
			Core.EnsureAccept(5259);

			Nulgath.ContractExchange(ChooseReward.BloodGemoftheArchfiend);
			Nulgath.ContractExchange(ChooseReward.BloodGemoftheArchfiend);

			Nulgath.FarmUni13(2);

			if (Core.CheckInventory(Nulgath.CragName))
			{
				Nulgath.BambloozevsDrudgen("Diamond of Nulgath", 60);
				Nulgath.BambloozevsDrudgen("Tainted Gem", 50);
				Nulgath.BambloozevsDrudgen("Dark Crystal Shard", 45);
				Nulgath.BambloozevsDrudgen("Voucher of Nulgath (non-mem)");
				Nulgath.BambloozevsDrudgen("Gem of Nulgath", 15);
			}
			else if (Core.CheckInventory("Nulgath's Birthday Gift") || Core.CheckInventory("Bounty Hunter's Drone Pet"))
			{
				Nulgath.NewWorldsNewOpportunities("Diamond of Nulgath", 60);
				Nulgath.NewWorldsNewOpportunities("Tainted Gem", 50);
				Nulgath.NewWorldsNewOpportunities("Dark Crystal Shard", 45);
				Nulgath.NewWorldsNewOpportunities("Voucher of Nulgath (non-mem)");
				Nulgath.NewWorldsNewOpportunities("Gem of Nulgath", 15);
				Nulgath.NewWorldsNewOpportunities("Blood Gem of the Archfiend", 2);
			}
			else
			{
				Nulgath.DiamondEvilWar(60);
				Nulgath.SwindleBulk(50);
				Nulgath.EssenceofDefeatReagent(45);
				Nulgath.Supplies("Voucher of Nulgath (non-mem)");
				if(Core.CheckInventory("Gem of Nulgath", 15))
					Nulgath.VoucherItemTotemofNulgath(ChooseReward.GemofNulgath);
				if (Core.CheckInventory("Gem of Nulgath", 15))
					Nulgath.VoucherItemTotemofNulgath(ChooseReward.GemofNulgath);
				Nulgath.KisstheVoid(2);
			}

			Nulgath.Supplies("Unidentified 26");

			if (Core.CheckInventory("Unidentified 27"))
			{
				Core.EnsureAccept(584);
				Core.HuntMonster("evilmarsh", "Dark Makai", "Dark Makai Sigil", 1);
				Core.EnsureComplete(584);
			}

			Nulgath.ApprovalAndFavor(50, 200);
			Farm.BattleGroundE(100000);
			bot.Player.Join("evilwarnul");
			bot.Player.Jump("Enter", "Spawn");
			bot.Shops.BuyItem(456, "Golden Hanzo Void");

			if (!Core.CheckInventory("DoomLord's War Mask"))
			{
				Core.EnsureComplete(5259, 35399);
				bot.Player.Pickup("DoomLord's War Mask", "Unidentified 35", "Archfiend Essence Fragment");
			}
			else if (!Core.CheckInventory("ShadowFiend Cloak"))
			{
				Core.EnsureComplete(5259, 35400);
				bot.Player.Pickup("ShadowFiend Cloak", "Unidentified 35", "Archfiend Essence Fragment");
			}
			else if (!Core.CheckInventory("Locks of the DoomLord"))
			{
				Core.EnsureComplete(5259, 35401);
				bot.Player.Pickup("Locks of the DoomLord", "Unidentified 35", "Archfiend Essence Fragment");
			}
			else if (!Core.CheckInventory("Doomblade of Destruction"))
			{
				Core.EnsureComplete(5259, 35398);
				bot.Player.Pickup("Doomblade of Destruction", "Unidentified 35", "Archfiend Essence Fragment");
			}
			else
				Core.EnsureComplete(5259, 35399);
			Core.Logger($"Completed x{i}");
			i++;
		}

		Core.SetOptions(false);
	}
}