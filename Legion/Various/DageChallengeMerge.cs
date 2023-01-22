/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Legion/HeadOfTheLegionBeast.cs
//cs_include Scripts/Story/Legion/DageChallengeStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Shops;
using Skua.Core.Models.Items;

public class DageChallengeMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreFarms Farm = new();
    public CoreLegion Legion = new();
    public SevenCircles Circles = new();
    public HeadoftheLegionBeast HOTLB = new();
    public DageChallengeStory DageChallenge = new();

    string[] MergeItems = {
        "Avarice of the Legion's Scythe",
        "Virgil of the Legion's Staff",
        "Avarice of the Legion",
        "Luxuria of the Legion",
        "Virgil of the Legion",
        "Avarice of the Legion's Helm",
        "Virgil of the Legion's Helm",
        "Avarice of the Legion's Scarf",
        "Eye of Luxuria Runes",
        "Virgil of the Legion's Cape",
        "Underworld Blade of DOOM",
        "Wrath of the Legion",
        "Wrath of the Legion's Cloak"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        MergePrep();

        Core.SetOptions(false);
    }

    public void MergePrep()
    {
        Adv.BestGear(GearBoost.Undead, true);
        Legion.SoulForgeHammer();
        Circles.CirclesWar(); //Required to turnin & accept the SoH quests
        DageChallenge.DageChallengeQuests(); //to unlock mergeshop
        Merge("all"); //if specific, just put the itemname in the "" 's
    }

    public void MergeMats(int quantLaurel, int quantMedal, int quantAccolade)
    {
        if (Core.CheckInventory("Underworld Laurel", quantLaurel) && Core.CheckInventory("Underworld Medal", quantMedal) && Core.CheckInventory("Underworld Accolade", quantAccolade))
            return;

        Core.AddDrop("Underworld Laurel", "Underworld Medal", "Underworld Accolade");

        if (!Core.CheckInventory("Underworld Laurel", quantLaurel))
            Core.Logger($"Farming \"Underworld Laurel\" x{quantLaurel}");
        while (!Bot.ShouldExit && !Core.CheckInventory("Underworld Laurel", quantLaurel))
        {
            Core.EnsureAccept(8544);
            Core.HuntMonster("Dage", "Dage the Evil", "Dage Dueled", publicRoom: true);
            Core.EnsureComplete(8544);
            Bot.Wait.ForPickup("Underworld Laurel");
        }

        if (!Core.CheckInventory("Underworld Medal", quantMedal))
            Core.Logger($"Farming \"Underworld Medal\" x{quantMedal}");
        while (!Bot.ShouldExit && !Core.CheckInventory("Underworld Medal", quantMedal))
        {
            Core.EquipClass(ClassType.Farm);

            Core.EnsureAccept(8545);
            Legion.ApprovalAndFavor(0, 200);
            Legion.ObsidianRock();
            HOTLB.SoulsHeresy(30);
            Core.EnsureComplete(8545);
            Bot.Wait.ForPickup("Underworld Medal");
        }

        if (!Core.CheckInventory("Underworld Accolade", quantAccolade))
            Core.Logger($"Farming \"Underworld Accolade\" x{quantAccolade}))");
        while (!Bot.ShouldExit && !Core.CheckInventory("Underworld Accolade", quantAccolade))
        {
            Core.EnsureAccept(8546);
            Core.HuntMonster("legionarena", "legion fiend rider", "Fiend Rider's Approval");
            Core.HuntMonster("frozenlair", "lich lord", "Lich Lord's Approval");
            Core.HuntMonster("dagefortress", "Grrrberus", "Grrrberus's Grr Grrr");
            Core.EnsureComplete(8546);
            Bot.Wait.ForPickup("Underworld Accolade");
        }
    }

    public void Merge(string item = "all")
    {
        if (item == "all" && Core.CheckInventory(MergeItems))
            return;

        if (item != "all" && Core.CheckInventory(item))
            return;

        if (item != "all" && !Core.CheckInventory(item))
        {
            Core.Join("dage");
            Bot.Shops.Load(2118);
            List<ShopItem> shopdata = Bot.Shops.Items;

            List<ItemBase> Requirements = shopdata.First(i => i.Name == item).Requirements;
            int Laurel = Requirements.First(i => i.Name == "Underworld Laurel").Quantity;
            int Medal = Requirements.First(i => i.Name == "Underworld Medal").Quantity;
            int Accolade = Requirements.First(i => i.Name == "Underworld Accolade").Quantity;

            MergeMats(Laurel, Medal, Accolade);
            Core.BuyItem("dage", 2118, item);
            Core.ToBank(item);
        }

        if (item == "all" && !Core.CheckInventory(MergeItems))
        {
            Core.Join("dage");
            Bot.Shops.Load(2118);
            List<ShopItem> shopdata = Bot.Shops.Items;

            foreach (string MergeItem in MergeItems)
            {
                if (!Core.CheckInventory(MergeItem, 1, toInv: false))
                {
                    Core.Logger($"Started farming for {MergeItem}");
                    if (MergeItem == "Avarice of the Legion's Helm")
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("dage", "Dage the Evil", "Avarice of the Legion's Skull", isTemp: false, publicRoom: true);
                        Core.HuntMonster("dage", "Dage the Evil", "Avarice of the Legion's Hood", isTemp: false, publicRoom: true);
                    }
                    List<ItemBase> Requirements = shopdata.First(i => i.Name == MergeItem).Requirements;
                    int Laurel = Requirements.First(i => i.Name == "Underworld Laurel").Quantity;
                    int Medal = Requirements.First(i => i.Name == "Underworld Medal").Quantity;
                    int Accolade = Requirements.First(i => i.Name == "Underworld Accolade").Quantity;

                    MergeMats(Laurel, Medal, Accolade);
                    Core.BuyItem("dage", 2118, MergeItem);
                    Core.ToBank(MergeItem);
                }
            }
        }
    }
}
