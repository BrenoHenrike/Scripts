//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs
//cs_include Scripts/Nation/Various/GoldenHanzoVoid.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

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
        Core.BankingBlackList.AddRange(NDWItems);
        Core.BankingBlackList.Add("Archfiend Essence Fragment");
        Core.SetOptions();

        NDWQuest(new[] { "Unidentified 35" });
        NDWQuest(NDWItems);

        Core.SetOptions(false);
    }

    public void NDWQuest(string[] items = null, int quant = 1)
    {
        if (Core.CheckInventory(items, quant))
            return;

        if (items == null)
            items = NDWItems;


        Core.AddDrop(NDWItems);
        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop("unidentified 27");
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(5259).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);
            
        foreach (ItemBase item in RewardOptions)
        {            
            if (Core.CheckInventory(item.Name, quant))
                return;
            Core.FarmingLogger(item.Name, quant);

            int i = 0;
            while (!Bot.ShouldExit && !Core.CheckInventory(item.Name, quant))
            {
                Core.EnsureAccept(5259);

                WillpowerExtraction.Unidentified34(10);
                Nation.FarmUni13(2);
                Nation.FarmBloodGem(2);
                Nation.FarmDiamondofNulgath(60);
                Nation.FarmDarkCrystalShard(45);
                Uni27();
                Nation.FarmVoucher(true);
                Nation.FarmGemofNulgath(15);
                Nation.SwindleBulk(50);
                GHV.GetGHV();
                if (item.Name == "Unidentified 35" && !Core.CheckInventory("Unidentified 35", quant) && Core.CheckInventory("Archfiend Essence Fragment", 9))
                    Core.BuyItem("tercessuinotlim", 1951, 35770);
                else Core.EnsureComplete(5259, item.ID);
                Core.ToBank(item.Name);

                Core.Logger($"Completed x{i}");
                i++;
            }
        }
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
