/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
using Skua.Core.Interfaces;

public class EnhancedNulgathNationHouse
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailies Daily = new();
    public CoreNation Nation = new();
    public CoreBLOD BLOD = new CoreBLOD();
    public CoreStory Story = new CoreStory();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.SetOptions();

        GetENNH();

        Core.SetOptions(false);
    }

    public void GetENNH()
    {
        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop("Nulgath Nation House", "Enchanted Nulgath Nation House", "Cemaros' Amethyst", "Aluminum");

        if (!Core.CheckInventory("Nulgath Nation House"))
        {
            Nation.FarmUni10(400);
            Nation.FarmUni13();
            Nation.FarmVoucher(false);
            Nation.FarmDiamondofNulgath(300);
            Nation.FarmDarkCrystalShard(250);
            Nation.FarmTotemofNulgath(30);
            Nation.FarmGemofNulgath(150);
            Nation.SwindleBulk(200);
            Nation.FarmBloodGem(100);
            Nation.ApprovalAndFavor(1000, 0);

            Farm.ChaosREP(2);
            Core.BuyItem("mountdoomskull", 776, "Cemaros' Amethyst");

            BLOD.UnlockMineCrafting();
            Daily.MineCrafting(new[] { "Aluminum" });

            Farm.DoomWoodREP();
            Farm.Gold(999);
            Core.BuyItem("lightguard", 277, "NUE Necronomicon");

            Core.EnsureAccept(4779);
            if (!Core.EnsureComplete(4779))
            {
                Core.Logger("Could not complete the quest, stopping bot", messageBox: true);
                return;
            }
            Bot.Drops.Pickup("Nulgath Nation House");
        }

        Core.HuntMonster("guru", "Guru Chest", "Pink Star Diamond of Nulgath", 1, false);
        Core.HuntMonster("timelibrary", "Ancient Chest", "Musgravite of Nulgath", 2, false);
        Core.BuyItem("archportal", 1211, "Enchanted Nulgath Nation House");
    }
}
