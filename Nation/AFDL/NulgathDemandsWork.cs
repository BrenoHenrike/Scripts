//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs
//cs_include Scripts/Nation/Various/GoldenHanzoVoid.cs
using Skua.Core.Interfaces;

public class NulgathDemandsWork
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();
    public GoldenHanzoVoid GHV = new();
    public WillpowerExtraction WillpowerExtraction = new WillpowerExtraction();

    public string[] NDWItems =
    {   "Unidentified 35",
        "Unidentified 27",
        "Unidentified 26",
        "Golden Hanzo Void",
        "DoomLord's War Mask",
        "ShadowFiend Cloak",
        "Locks of the DoomLord",
        "Doomblade of Destruction",
        "Archfiend Essence Fragment"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.Add("Archfiend Essence Fragment");
        Core.SetOptions();

        Uni35(1);

        Core.SetOptions(false);
    }

    public void NDWQuest(string[] items = null, int quant = 1)
    {
        if (Core.CheckInventory(items, quant))
            return;

        if (items == null)
        {
            Core.Logger("No Item Input");
            return;
        }

        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop(NDWItems);
        Core.AddDrop("unidentified 27");
        
        foreach (string item in items)
        {
            if (Core.CheckInventory(items, quant))
                break;
            else Core.FarmingLogger(item, quant);

            int i = 0;
            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            {
                Core.EnsureAccept(5259);

                WillpowerExtraction.Unidentified34(10);
                Nation.FarmUni13(2);
                Core.EnsureAccept(5259);
                Nation.FarmBloodGem(2);
                Nation.FarmDiamondofNulgath(60);
                Nation.FarmDarkCrystalShard(45);
                Uni27();
                Nation.FarmVoucher(true);
                Nation.FarmGemofNulgath(15);
                Nation.SwindleBulk(50);
                GHV.GetGHV();
                Core.EnsureCompleteChoose(5259, items);
                Core.ToBank(item);

                if (item == "Unidentified 35" && !Core.CheckInventory("Unidentified 35", quant) && Core.CheckInventory("Archfiend Essence Fragment", 9))
                    Core.BuyItem("tercessuinotlim", 1951, 35770);

                Core.Logger($"Completed x{i}");
                i++;
            }
        }
    }

    public void Uni35(int quant = 1)
    {
        if (Core.CheckInventory("Unidentified 35", quant))
            return;

        NDWQuest(new[] { "Unidentified 35" });
    }

    public void Uni27(int quant = 1)
    {
        if (Core.CheckInventory("Unidentified 27"))
            return;

        Nation.Supplies("Unidentified 26", 1, true);
        Core.EnsureAccept(584);
        Core.HuntMonster("evilmarsh", "Dark Makai", "Dark Makai Sigil");
        Core.EnsureComplete(584);
        Bot.Wait.ForPickup("Unidentified 27");
        Core.Logger("Uni 27 acquired");

    }
}
