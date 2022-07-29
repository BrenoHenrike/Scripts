//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs
//cs_include Scripts/Nation/Various/GoldenHanzoVoid.cs
using RBot;

public class NulgathDemandsWork
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();
    public GoldenHanzoVoid GHV = new();

    public WillpowerExtraction WillpowerExtraction = new WillpowerExtraction();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
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

        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop("Unidentified 35", "Archfiend Essence Fragment", "Unidentified 27", "Unidentified 26",
            "Golden Hanzo Void", "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction");

        int i = 0;
        while (!Bot.ShouldExit() && !Core.CheckInventory(new[] { "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction", "Unidentified 35" }, toInv: false))
        {
            if (Core.CheckInventory("Archfiend Essence Fragment", 9)
                && Core.CheckInventory(new[] { "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction" }))
                break;

            WillpowerExtraction.Unidentified34(10);

            Nation.FarmUni13(2);

            Core.EnsureAccept(5259);

            Nation.FarmBloodGem(2);

            Nation.FarmDiamondofNulgath(60);

            Nation.FarmDarkCrystalShard(45);

            if (!Core.CheckInventory("Unidentified 27"))
            {
                Nation.Supplies("Unidentified 26", 1, true);
                Core.EnsureAccept(584);
                Core.HuntMonster("evilmarsh", "Dark Makai", "Dark Makai Sigil", 1);
                Core.EnsureComplete(584);
                Bot.Player.Pickup("Unidentified 27");
                Core.Logger("Uni 27 acquired");
            }

            Nation.FarmVoucher(false);

            Nation.FarmGemofNulgath(15);

            Nation.SwindleBulk(50);


            GHV.GetGHV();

            Bot.Player.Pickup(Bot.Drops.Pickup.ToArray());

            Core.EnsureCompleteChoose(5259, new[] { "DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction" });
            if (Bot.Quests.IsInProgress(5259))
            {
                Core.Logger("Looks like the quest is still in progress. Turning it in now");
                Core.EnsureComplete(5259);
            }

            Core.Logger($"Completed x{i}");
            i++;
        }

        if (Core.CheckInventory("Archfiend Essence Fragment", 9) && !Core.CheckInventory("Unidentified 35"))
        {
            Core.BuyItem("tercessuinotlim", 1951, 35770);
        }
    }
}