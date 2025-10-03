/*
name: Death Knight Lord Post-update farming script
description: Death Knight Lord, post-update farming script, deathknight lord, golden deathknight lord, silver deathknight lord, dkl, member, class
tags: death knight lord, member, class
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs

using Skua.Core.Interfaces;

public class DeathKnightLordFarmer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public static CoreFarms Farm { get => _Farm ??= new CoreFarms(); set => _Farm = value; }
    private static CoreFarms _Farm;
    public static CoreStory Story { get => _Story ??= new CoreStory(); set => _Story = value; }
    private static CoreStory _Story;
    public static CoreToD ToD { get => _ToD ??= new CoreToD(); set => _ToD = value; }
    private static CoreToD _ToD;
    public static CoreAdvanced Adv { get => _Adv ??= new CoreAdvanced(); set => _Adv = value; }
    private static CoreAdvanced _Adv;


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DKL();

        Core.SetOptions(false);
    }

    public void DKL(bool rankUpClass = true)
    {
        // Check if DeathKnight Lord is already owned
        if (Core.CheckInventory("DeathKnight Lord"))
        {
            Core.Logger("DeathKnight Lord class is already owned");
            if (rankUpClass)
                Adv.RankUpClass("DeathKnight Lord");
            return;
        }

        // Member check
        if (!Core.IsMember)
        {
            Core.Logger("The DeathKnight Lord is a member-only item. You will not be able to obtain it otherwise.");
            return;
        }

        ToD.CompleteToD();

        // Silver and gold pieces
        string[] silverPieces =
        {
            "Silver DeathKnight Lord Boots",
            "Silver DeathKnight Lord Chest Plate",
            "Silver DeathKnight Lord Gauntlets",
            "Silver DeathKnight Lord Greaves",
            "Silver DeathKnight Lord Hauberk"
        };

        string[] goldPieces =
        {
            "Golden DeathKnight Lord Gauntlets",
            "Golden DeathKnight Lord Greaves",
            "Golden DeathKnight Lord Chest Plate",
            "Golden DeathKnight Lord Hauberk",
            "Golden DeathKnight Lord Boots"
        };

        string[] importantItems = { "SilverSkull Amulet", "GoldSkull Amulet", "Shadow Skull", "Bonecastle Amulet" };

        // Add all important items to drop list
        Core.AddDrop(importantItems.Concat(silverPieces).Concat(goldPieces).ToArray());

        // Ensure unbanking of important items
        foreach (var item in importantItems)
        {
            if (Core.CheckInventory(item, toInv: false)) // in bank
                Core.Unbank(item);
        }

        // Farm Silver DeathKnight Lord pieces
        Core.Logger("Farming Flester the Silver for Silver items...");
        foreach (string item in silverPieces)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item))
            {
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("towersilver", "Flester the Silver", item, isTemp: false);
            }
        }

        Core.FarmingLogger("SilverSkull Amulet", 25);
        Core.EquipClass(ClassType.Farm);
        while (!Bot.ShouldExit && !Core.CheckInventory("SilverSkull Amulet", 25))
            Core.HuntMonsterQuest(5010, mapName: "towersilver", monsterName: "Bloody Scary");

        // Merge Silver DeathKnight Lord
        if (!Core.CheckInventory("Silver DeathKnight Lord") &&
            Core.CheckInventory(silverPieces) &&
            Core.CheckInventory("SilverSkull Amulet", 25))
        {
            Core.Logger("Merging Silver DeathKnight Lord...");
            Core.BuyItem("towersilver", 1243, "Silver DeathKnight Lord");
        }

        // Farm Golden DeathKnight Lord pieces
        Core.Logger("Farming Yurrod the Gold for Golden items...");
        foreach (string item in goldPieces)
        {
            Core.EquipClass(ClassType.Solo);
            while (!Bot.ShouldExit && !Core.CheckInventory(item))
                Core.HuntMonster("towergold", "Yurrod the Gold", item, isTemp: false);
        }

        // Farm GoldSkull Amulet (quest 5024) until 30x
        Core.FarmingLogger("GoldSkull Amulet", 30);
        Core.EquipClass(ClassType.Farm);
        while (!Bot.ShouldExit && !Core.CheckInventory("GoldSkull Amulet", 30))
            Core.HuntMonsterQuest(5024, mapName: "towergold", monsterName: "Bone Widow");

        // Merge Golden DeathKnight Lord
        if (!Core.CheckInventory("Golden DeathKnight Lord") &&
            Core.CheckInventory(goldPieces, toInv: true) &&
            Core.CheckInventory("GoldSkull Amulet", 30))
        {
            Core.Logger("Merging Golden DeathKnight Lord...");
            Core.BuyItem("towergold", 1243, "Golden DeathKnight Lord");
        }

        // Repeat quest 4993 for Shadow Skull and Bonecastle Amulet
        Core.FarmingLogger("Shadow Skull", 30);
        while (!Bot.ShouldExit && !Core.CheckInventory("Shadow Skull", 30))
        {
            if (!Core.CheckInventory("Silver DeathKnight Lord"))
                Core.BuyItem("towersilver", 1243, "Silver DeathKnight Lord");
            if (!Core.CheckInventory("Golden DeathKnight Lord"))
                Core.BuyItem("towergold", 1243, "Golden DeathKnight Lord");

            Core.EnsureAccept(4993);

            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("bonecastle", "Green Rat", "Gamey Rat Meat", 3, true);
            Core.HuntMonster("bonecastle", "Undead Waiter", "Waiter's Notepad", 1, true);
            Core.HuntMonster("bonecastle", "Turtle", "Turtle's Eggs", 6, true);
            Core.HuntMonster("bonecastle", "Ghoul", "Ghoul \"Vinegar\"", 6, true);
            Core.HuntMonster("bonecastle", "Grateful Undead", "Spices", 2, true);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("bonecastle", "The Butcher", "Bag of Bone Flour", 1, true);

            Core.EnsureComplete(4993);
        }

        // Buy final class
        if (Core.CheckInventory("Shadow Skull", 30))
        {
            Core.BuyItem("towersilver", 1243, "DeathKnight Lord");
        }

        if (rankUpClass)
            Adv.RankUpClass("DeathKnight Lord");

    }
}
