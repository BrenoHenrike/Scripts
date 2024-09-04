/*
name: For The Crater Good Quest
description: This will finish the For The Crater Good Quest.
tags: quest, crater, big crater, summer-break, the crater good, rock bottom
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/SummerBreak/CraterHouseMerge.cs
using Skua.Core.Interfaces;

public class ForTheCraterGood
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private CraterHouseMerge CHM = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoQuest();

        Core.SetOptions(false);
    }

    public void DoQuest()
    {
        if (Core.CheckInventory(Core.QuestRewards(9782), toInv: false) || !Core.isSeasonalMapActive("summerbreak"))
            return;

        if (!Core.CheckInventory("Big Crater"))
        {
            Core.Logger("Big Crater is missing. Buying it now.");
            CHM.BuyAllMerge("Big Crater");
        }

        Core.AddDrop(Core.QuestRewards(9782));
        Core.EnsureAccept(9782);

        // Space Helm
        if (!Core.CheckInventory("Space Helm"))
        {
            Core.EquipClass(ClassType.Farm);
            Core.AddDrop("Space Helm");
            Core.EnsureAccept(2855);
            Core.HuntMonster("j6moon", "Space Raptor", "Note from J6");
            Core.EnsureComplete(2855);
        }

        // Ultimate Security Bot Defeated
        if (!Core.CheckInventory("Ultimate Security Bot Defeated", 600))
        {
            Core.EquipClass(ClassType.Farm);
            Core.EnsureAccept(1176);
            Core.HuntMonster("moonyardb", "Robo Guard", "Ultimate Security Bot Defeated", 600, false);
            Bot.Quests.UnregisterQuests(1176);
        }

        // Kua-Toa Dagger
        if (!Core.CheckInventory("Kua-Toa Dagger"))
        {
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("uppercity", "Drow Assassin", "Kua-Toa Dagger", 1, false);
        }

        // Enchanted Titan Helm +15
        if (!Core.CheckInventory("Enchanted Titan Helm +15"))
        {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("darkheart", "Gaiazor", "Enchanted Titan Helm +15", 1, false);
        }

        // Living Tree Titan
        if (!Core.CheckInventory(30203))
        {
            Core.EquipClass(ClassType.Solo);
            Bot.Quests.UpdateQuest(4361);
            Core.FarmingLogger("Living Tree Titan");
            while (!Core.CheckInventory(30203))
                Core.HuntMonster("treetitanbattle", "Dakka the Dire Dragon", log: false);
        }

        Core.EnsureComplete(9782);
    }
}
