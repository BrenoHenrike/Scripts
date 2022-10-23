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
    public CoreNation Nation = new();
    public GoldenHanzoVoid GHV = new();
    public WillpowerExtraction WillpowerExtraction = new();

    public string[] NDWItems =
    {   "DoomLord's War Mask",
        "ShadowFiend Cloak",
        "Locks of the DoomLord",
        "Doomblade of Destruction",
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.AddRange(NDWItems);
        Core.BankingBlackList.AddRange(new[] { "Archfiend Essence Fragment", "Unidentified 35" });
        Core.SetOptions();

        NDWQuest(new[] { "Unidentified 35" });
        NDWQuest(NDWItems);

        Core.SetOptions(false);
    }



    /// <summary>
    /// Complets "Nulgath Demands Work" until the Desired Items are gotten. 
    /// <param name="string[] items">The List of items to Get from the Quest</param>
    /// <param name="quant">Amount of the "item" [Mostly the Archfiend Ess and Uni 35]</param>
    /// </summary>
    public void NDWQuest(string[] items = null, int quant = 1)
    {
        if (items == null)
            items = NDWItems;

        if (Core.CheckInventory(items, quant))
            return;

        var rewards = Core.EnsureLoad(5259).Rewards;

        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop(rewards.Select(x => x.Name).ToArray());

        foreach (string item in items)
        {
            if (Core.CheckInventory(item, quant))
                continue;

            int itemID = 0;
            try
            {
                itemID = rewards.First(x => x.Name.ToLower() == item.ToLower()).ID;
            }
            catch
            {
                continue;
            }

            Core.FarmingLogger(item, quant);

            int i = 0;
            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
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

                if (item == "Unidentified 35")
                {
                    if (Core.CheckInventory("Archfiend Essence Fragment", 9))
                        Core.BuyItem("tercessuinotlim", 1951, 35770);
                    else Core.EnsureComplete(5259);
                }
                Core.EnsureComplete(5259, itemID);
                Core.ToBank(item);

                Core.Logger($"Completed x{i}");
                i++;
            }
        }
    }

    public void Uni27(int quant = 1)
    {
        if (Core.CheckInventory("Unidentified 27"))
            return;

        Core.AddDrop("Unidentified 27");
        Nation.Supplies("Unidentified 26", 1, true);
        Core.EnsureAccept(584);
        Core.HuntMonster("evilmarsh", "Dark Makai", "Dark Makai Sigil");
        Core.EnsureComplete(584);
        Bot.Wait.ForPickup("Unidentified 27");
        Core.Logger("Uni 27 acquired");

    }
}
