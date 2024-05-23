/*
name: Senlin-Mas's Quests
description: Completes Senlin-Mas's Quests in akibacny.
tags: seasonal, yokai, akibacny, akiba new year, story, senlin-mas
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class SenlinMas
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();
    private CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(3335) || !Core.isSeasonalMapActive("akibacny"))
            return;

        Story.PreLoad(this);

        Farm.Experience(50);
        Core.EquipClass(ClassType.Farm);

        // Prove Yourself  3321
        Story.KillQuest(3321, "yokairiver", "Kappa Ninja");

        // Culling the Corruption 3322
        Story.KillQuest(3322, "akibacny", new[] { "Xingzhi", "Lingzhi" });

        // The Core of the Problem 3323
        Story.KillQuest(3323, "akibacny", "Lingzhi");

        // Essentially Pure 3324
        Story.KillQuest(3324, "pines", "Red Shell Turtle");

        // Salve-ation 3325
        Story.MapItemQuest(3325, "akibacny", 2448, 6);

        // Not So Much 3326
        Story.KillQuest(3326, "pines", "Pine Troll");

        // Berry Sturdy 3327
        Story.KillQuest(3327, "farm", "Treeant");

        // Oil of... what? 3328
        Story.KillQuest(3328, "faerie", "Chainsaw Sneevil");

        // No One Likes Taking Medicine 3329
        Story.KillQuest(3329, "akibacny", "Lingzhi");

        // Nip it in the Bud 3330
        Story.KillQuest(3330, "akibacny", "Lingzhi");

        // Finding Yaomo 3331
        Story.MapItemQuest(3331, "akibacny", 2449, 1);

        // Sort of Like a Keycard 3332
        Story.KillQuest(3332, "akibacny", "Lingzhi");

        // Defeat Yaomo 3333
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(3333, "akibacny", "Yaomo");
        Core.EquipClass(ClassType.Farm);

        // Mushroom Compost 3334
        Story.KillQuest(3334, "akibacny", "Lingzhi");

        // Tainted Qi 3335
        Story.KillQuest(3335, "akibacny", "Xingzhi");


        /*
        these are unlocked by default and dont need the rest
        // // Shushunned 3336
        // Story.KillQuest(3336, "akibacny", "Shushen");

        // // Tainted Hearts 3337
        // Story.KillQuest(3337, "akibacny", "Hoku");

        // // Corrupt Collection 3338
        // Story.KillQuest(3338, "akibacny", new[] { "Lingzhi", "Xingzhi", "Hoku", "Shushen" });

        // // Cute Lil Devils 3339
        // Story.KillQuest(3339, "akibacny", "Mogui");

        // // Little Ghosts 3340
        // Story.KillQuest(3340, "akibacny", new[] { "Shushen", "Mogui" });

        // // Defeat Ultra Yaomo 3341
        // Story.KillQuest(3341, "akibacny", "Ultra Yaomo");
        */
    }
}
