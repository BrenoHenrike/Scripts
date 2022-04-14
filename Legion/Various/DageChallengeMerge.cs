
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Legion/CoreLegion.cs
using RBot;
using RBot.Shops;
using RBot.Items;
using System.Collections.Generic;

public class DageChallengeMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreFarms Farm = new CoreFarms();
    public CoreLegion Legion = new CoreLegion();
    public SevenCircles Circles = new SevenCircles();

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

    public void ScriptMain(ScriptInterface bot)
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
        DageQuests(); //to unlock mergeshop
        Merge("all"); //if specific, just put the itemname in the "" 's
    }

    public void DageQuests()
    {
        if (Core.isCompletedBefore(8546))
            return;

        Core.AddDrop("Underworld Medal", "Underworld Laurel", "Underworld Accolade");

        if (!Story.QuestProgression(8544))
        {
            Core.EquipClass(ClassType.Solo);
            //Training with Dage
            Core.EnsureAccept(8544);
            Core.HuntMonster("Dage", "Dage the Evil", "Dage Dueled");
            Core.EnsureComplete(8544);
            Bot.Wait.ForPickup("Underworld Laurel");
        }

        if (!Story.QuestProgression(8545))
        {
            Core.AddDrop("Underworld Medal", "Souls of Heresy", "Dage's Favor");
            Core.EquipClass(ClassType.Farm);

            //Darkness for Darkness'Sake
            Core.EnsureAccept(8545);
            if (!Core.CheckInventory("Dage's Favor", 200))
                Core.HuntMonster("underworld", "Legion Fenrir", "Dage's Favor", 200, isTemp: false);
            Legion.ObsidianRock(10);
            SoH();
            Core.EnsureComplete(8545);
            Bot.Wait.ForPickup("Underworld Medal");
        }

        if (!Story.QuestProgression(8546))
        {
            //Power of the Undead Legion
            Core.EnsureAccept(8546);
            Core.HuntMonster("legionarena", "legion fiend rider", "Fiend Rider's Approval");
            Core.HuntMonster("frozenlair", "lich lord", "Lich Lord's Approval");
            Core.HuntMonster("dagefortress", "Grrrberus", "Grrrberus's Grr Grrr");
            Core.EnsureComplete(8546);
            Bot.Wait.ForPickup("Underworld Accolade");
        }
    }

    public void SoH(int quant = 30)
    {
        int z = 1;

        if (Core.CheckInventory("Souls of Heresy", quant))
            return;

        Core.AddDrop("Souls of Heresy");
        Core.SendPackets($"%xt%zm%getQuests%{Bot.Map.RoomID}%7983%%7980%%7981%");
        Core.EquipClass(ClassType.Farm);

        while (!Core.CheckInventory("Souls of Heresy", quant))
        {
            Core.Logger($"Farming Souls of Heresy, {quant - Bot.Inventory.GetQuantity("Souls of Heresy")}/{quant}");
            Core.EnsureAccept(7980, 7981, 7983);
            while (Core.CheckInventory("Mega War Medal", 3) || Core.CheckInventory("War Medal", 5) && !Core.CheckInventory("Souls of Heresy", quant))
            {
                while (Core.CheckInventory("War Medal", 5))
                {
                    Bot.Sleep(1500);
                    Core.EnsureComplete(7980);
                }
                while (Core.CheckInventory("Mega War Medal", 3))
                {
                    Bot.Sleep(1500);
                    Core.EnsureComplete(7981);
                }
            }
            Core.KillMonster("sevencircleswar", "r5", "Left", "Heresy Guard", "Heresy Guards Defeated", 12);
            Core.EnsureComplete(7983);
            Core.Logger($"Quest Completed x{z++} times");
        }
    }

    public void MergeMats(int quantLaurel, int quantMedal, int quantAccolade)
    {
        if (Core.CheckInventory("Underworld Laurel", quantLaurel) && Core.CheckInventory("Underworld Medal", quantMedal) && Core.CheckInventory("Underworld Accolade", quantAccolade))
            return;

        Core.AddDrop("Underworld Laurel", "Underworld Medal", "Underworld Accolade");

        if (!Core.CheckInventory("Underworld Laurel", quantLaurel))
            Core.Logger($"Farming \"Underworld Laurel\" x{quantLaurel}");
        while (!Core.CheckInventory("Underworld Laurel", quantLaurel))
        {
            Core.EnsureAccept(8544);
            Core.HuntMonster("Dage", "Dage the Evil", "Dage Dueled", publicRoom: true);
            Core.EnsureComplete(8544);
            Bot.Wait.ForPickup("Underworld Laurel");
        }

        if (!Core.CheckInventory("Underworld Medal", quantMedal))
            Core.Logger($"Farming \"Underworld Medal\" x{quantMedal}");
        while (!Core.CheckInventory("Underworld Medal", quantMedal))
        {
            Core.EquipClass(ClassType.Farm);

            Core.EnsureAccept(8545);
            Legion.ApprovalAndFavor(0, 200);
            Legion.ObsidianRock(10);
            SoH(30);
            Core.EnsureComplete(8545);
            Bot.Wait.ForPickup("Underworld Medal");
        }

        if (!Core.CheckInventory("Underworld Accolade", quantAccolade))
            Core.Logger($"Farming \"Underworld Accolade\" x{quantAccolade}))");
        while (!Core.CheckInventory("Underworld Accolade", quantAccolade))
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
            List<ShopItem> shopdata = Bot.Shops.ShopItems;

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
            List<ShopItem> shopdata = Bot.Shops.ShopItems;

            foreach (string MergeItem in MergeItems)
            {
                if (!Core.CheckInventory(MergeItem, 1, toInv: false))
                {
                    Core.Logger($"Started farming for {MergeItem}");
                    if (MergeItem == "Avarice of the Legion's Helm")
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("dage", "Dage the Evil", "Avarice of the Legion's Skull", publicRoom: true);
                        Core.HuntMonster("dage", "Dage the Evil", "Avarice of the Legion's Hood", publicRoom: true);
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