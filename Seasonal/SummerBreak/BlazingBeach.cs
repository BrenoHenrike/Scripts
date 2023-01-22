/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class BlazingBeachStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (!Core.isSeasonalMapActive("blazingbeach"))
            return;

        if (Core.isCompletedBefore(8708))
            return;

        Story.PreLoad(this);

        // (Volca)No Trespassing
        Story.KillQuest(8702, "blazingbeach", "Burning Bombadier");

        // Piracy for Pyromancers
        Story.KillQuest(8703, "blazingbeach", new[] { "Burning Bombadier", "Magma Pirate", "Red-Hot Raider" });

        // Canned Heat
        Story.KillQuest(8704, "blazingbeach", "Burning Bombadier");
        Story.MapItemQuest(8704, "blazingbeach", 10252);

        // Dau Go
        if (!Story.QuestProgression(8705))
        {
            Core.EnsureAccept(8705);
            Core.HuntMonster("blazingbeach", "Dao Treeant", "Cavern Wood", 12, log: false);
            Core.KillMonster("blazingbeach", "r2", "Right", "Burning Bombadier", "Redistributed Loot", 12, log: false);
            Core.HuntMonster("burningbeach", "Water Goblin", "Goblin Canteen", 5, log: false);
            Core.EnsureComplete(8705);
        }
        // Bãi Cháy
        if (!Story.QuestProgression(8706))
        {
            Core.EnsureAccept(8706);
            Core.HuntMonster("burningbeach", "Lavazard", "Lizard Lava", 5, log: false);
            Core.HuntMonster("burningbeach", "Lava Guardian", "Mage Magma", 5, log: false);
            Core.HuntMonster("blazingbeach", "Ruby Golem", "Flame Ruby", 3, log: false);

            Core.EnsureComplete(8706);
        }

        // Sung Sot
        Story.MapItemQuest(8707, "blazingbeach", 10253);
        Story.KillQuest(8707, "burningbeach", new[] { "Maladrite", "Shark" });

        // Ha Long   
        Story.KillQuest(8708, "blazingbeach", "Magma Blazebeard");
    }
}
