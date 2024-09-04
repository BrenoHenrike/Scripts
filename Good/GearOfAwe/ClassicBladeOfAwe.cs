/*
name: Classic Blade of Awe
description: This bot will farm Classic Blade of Awe.
tags: classic, blade, blade of awe, awe, good, weapon, original treasure chest, mysterious chest, unlocking the chest
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/DracoCon.cs
//cs_include Scripts/Other/TreasureHuntQuestRewards.cs
using Skua.Core.Interfaces;

public class ClassicBladeOfAwe
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    private TreasureHuntQuest THQ = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetCBoA();

        Core.SetOptions(false);
    }

    public void GetCBoA()
    {
        if (Core.CheckInventory("Classic Blade of Awe", toInv: false))
            return;

        GetChest();

        Core.Logger("Getting Classic Blade of Awe");
        Core.AddDrop("Classic Blade of Awe");

        Core.EnsureAccept(9705); // Unlocking the Chest
        if (!Core.CheckInventory("Pearl Dragon Blade"))
            Adv.BuyItem("vendorbooths", 730, "Pearl Dragon Blade");
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("aqlesson", "Akriloth", "Serpent Tongue", isTemp: false);
        if (!Core.CheckInventory("Big 100K"))
            Adv.BuyItem("swordhaven", 3, "Big 100K");
        Core.HuntMonster("aqlesson", "Carnax", "Carnax Essence", 100, isTemp: false);
        Core.EnsureComplete(9705);
    }

    public void GetChest()
    {
        if (Core.CheckInventory("Original Treasure Chest"))
            return;

        Core.Logger("Getting Original Treasure Chest");
        Core.AddDrop("Original Treasure Chest");

        Core.EquipClass(ClassType.Solo);
        if (!Bot.Quests.IsUnlocked(9704))
        {
            Core.EnsureAccept(8757); // Awe Enhancements At Home
            Core.HuntMonster("banished", "Desterrat Moya", "Apocryphal Blade Of The Truth", isTemp: false);
            Core.EnsureComplete(8757);
        }
        Core.EnsureAccept(9704); // Mysterious Chest
        Core.HuntMonster("summon", "Blood minion", "Protector of Lore", isTemp: false);
        THQ.DoQuest(true);
        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("marsh2", "Thrax Ironhide", "Tyrant Blade", isTemp: false);
        Core.EnsureComplete(9704);
    }
}
