/*
name: Yokai Hunt
description: Completes the quests in yokaihunt.
tags: yokai, seasonal, akiba-new-year, yokaihunt, story, baoyu lin, ai no miko, yue huang
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class YokaiHunt
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoAll();
        Core.SetOptions(false);
    }

    public void DoAll()
    {
        AiNoMiko();
        YueHuang();
        BaoyuLin();

    }

    public void AiNoMiko()
    {
        if (Core.isCompletedBefore(7941) || !Core.isSeasonalMapActive("yokaihunt"))
            return;

        Story.PreLoad(this);

        //Nope-perabo (7936)
        Story.KillQuest(7936, "yokaihunt", "Ox Nopperabo");

        //Intel Processor (7937)
        Story.KillQuest(7937, "yokaihunt", "Golden Ox Guard");

        //Scout Out (7938)
        Story.MapItemQuest(7938, "yokaihunt", 8133);
        Story.KillQuest(7938, "yokaihunt", "Golden Ox Guard");

        //Re-info-source-ments (7939)
        Story.KillQuest(7939, "yokaihunt", "Golden Ox Guard");

        //Yokai Ox Spirit (7940)
        Story.KillQuest(7940, "yokaihunt", "Ox Yokai Spirit");

        //Etokoun Captured (7941)
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(7941, "yokaihunt", "Etokoun");
    }

    public void YueHuang()
    {
        if (Core.isCompletedBefore(9098) || !Core.isSeasonalMapActive("yokaihunt"))
            return;

        AiNoMiko();

        Story.PreLoad(this);

        //Vitamoon (9092)
        Story.KillQuest(9092, "natatorium", new[] { "Anglerfish", "Merdraconian" });

        //Fill'er Up (9093)
        if (!Story.QuestProgression(9093))
        {
            Core.EnsureAccept(9093);
            Core.KillMonster("wanders", "r5", "Left", "Lotus Spider", "Lotus Seeds", 10, log: false);
            Core.HuntMonster("battlefowl", "ChickenCow", "Chickencow Egg", 3, log: false);
            Core.EnsureComplete(9093);
        }

        //Mirror Flowers (9094)
        Story.KillQuest(9094, "guardiantree", new[] { "Blossoming Treeant", "Seed Spitter" });

        //Moon's Reflection (9095)
        Story.KillQuest(9095, "beachparty", new[] { "Water Elemental", "Boiling Elemental" });

        //Shifting Faces (9096)
        Story.KillQuest(9096, "safiria", new[] { "Albino Bat", "Chaos Lycan" });

        //Eto... Bleh (9097)
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(9097, "yokaihunt", "Etokoun");

        //Elixir of the Moon (9098)
        Story.KillQuest(9098, "yokaihunt", "Elixir Etokoun");
    }

    public void BaoyuLin()
    {
        if (Core.isCompletedBefore(9575) || !Core.isSeasonalMapActive("yokaihunt"))
            return;

        YueHuang();

        Story.PreLoad(this);

        // Art of Resilience (9571)
        Story.KillQuest(9571, "zhu", "Plum Treeant");

        // Lucky Red (9572)
        Story.KillQuest(9572, "shipwreck", new[] { "Gilded Merdraconian", "Lobthulhu" });

        // Palm Soot (9573)
        Story.KillQuest(9573, "burningbeach", "Lava Guardian");

        // Hong of the West (9574)
        Story.KillQuest(9574, "ashfallcamp", "Smoldur");

        // Baihong Guan Ri (9575)
        Story.KillQuest(9575, "yokaihunt", "Mutou Hong");
    }
}
