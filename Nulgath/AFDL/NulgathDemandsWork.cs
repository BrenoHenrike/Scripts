//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Nulgath/AFDL/WillpowerExtraction.cs
using RBot;

public class NulgathDemandsWork
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNulgath Nulgath = new CoreNulgath();

    public WillpowerExtraction WillpowerExtraction = new WillpowerExtraction();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nulgath.bagDrops);
        Core.BankingBlackList.AddRange(new[] {"Unidentified 35", "Archfiend Essence Fragment", "Unidentified 27", "Unidentified 26",
            "Golden Hanzo Void", "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction"});
        Core.SetOptions();

        Unidentified35();

        Core.SetOptions(false);
    }

    public void Unidentified35()
    {
        if (Core.CheckInventory(new[] { "Unidentified 35", "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction" }))
            return;

        Core.AddDrop(Nulgath.bagDrops);
        Core.AddDrop("Unidentified 35", "Archfiend Essence Fragment", "Unidentified 27", "Unidentified 26",
            "Golden Hanzo Void", "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction");

        int i = 0;
        while (!Core.CheckInventory(new[] { "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction", "Unidentified 35" }, toInv: false))
        {
            if (Core.CheckInventory("Archfiend Essence Fragment", 9)
                && Core.CheckInventory(new[] { "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction" }))
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
                Bot.Player.Pickup("Unidentified 27");
                Core.Logger("Uni 27 acquired");
            }

            if (!Core.CheckInventory("Golden Hanzo Void"))
            {
                Nulgath.ApprovalAndFavor(50, 200);
                Farm.BattleGroundE(100000);
                Core.BuyItem("evilwarnul", 456, "Golden Hanzo Void");
                Core.Logger("Golden Hanzo Void bought");
            }

            Bot.Player.Pickup(Bot.Drops.Pickup.ToArray());

            Core.EnsureCompleteChoose(5259, new[] { "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction" });

            Core.Logger($"Completed x{i}");
            i++;
        }

        if (Core.CheckInventory("Archfiend Essence Fragment", 9) && !Core.CheckInventory("Unidentified 35"))
        {
            Core.JoinTercessuinotlim();
            Bot.Player.Jump("Swindle", "Left");
            Core.BuyItem("tercessuinotlim", 1951, 35770);
        }
    }
}