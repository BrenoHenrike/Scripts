/*
name: Arcadia
description: This will complete the Arcadia story quest.
tags: arcadia, seasonal, heros-heart-day, heart, hero, story
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Arcadia
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        CompleteStory();
        Core.SetOptions(false);
    }

    public void CompleteStory()
    {
        if (Core.isCompletedBefore(8520))
            return;

        if (!Core.isSeasonalMapActive("Arcadia"))
            return;

        Story.PreLoad(this);
        Core.EquipClass(ClassType.Farm);

        // Cotton Candy Sky 8507
        Story.KillQuest(8507, "Arcadia", "Spirit Butterfly");

        // Sticking Your Neck Out 8508
        Story.MapItemQuest(8508, "Arcadia", 9625);

        // Melty Blood 8509
        Story.KillQuest(8509, "Arcadia", "Shadowfall Warrior");
        Story.MapItemQuest(8509, "Arcadia", 9626);

        // Poincare Recurrence 8510
        Story.MapItemQuest(8510, "Arcadia", 9630);
        Story.KillQuest(8510, "Arcadia", "Spirit Butterfly");

        // Princess Carry 8511
        Story.MapItemQuest(8511, "Arcadia", 9631);
        Story.KillQuest(8511, "Arcadia", "Spirit Butterfly");

        // Flowers for a Crown  8512
        Story.MapItemQuest(8512, "Arcadia", 9632, 4);

        // Tea Diffusion  8513
        Story.KillQuest(8513, "Arcadia", "Phantasm");
        Story.MapItemQuest(8513, "Arcadia", 9627);

        // Road Not Taken  8514
        Story.MapItemQuest(8514, "Arcadia", 9633, 4);
        Story.KillQuest(8514, "Arcadia", "Spirit Butterfly");

        // Squeaky Toy 8515
        Story.MapItemQuest(8515, "Arcadia", 9634);
        Story.KillQuest(8515, "Arcadia", "Phantasm");

        // Vantablack 8516
        Story.MapItemQuest(8516, "Arcadia", 9635, 3);
        Story.KillQuest(8516, "Arcadia", "Lightguard Wraith");

        // Salt Bin 8517
        Story.MapItemQuest(8517, "Arcadia", 9628);

        // Four Princesses  8518
        Story.MapItemQuest(8518, "Arcadia", 9636);
        Story.KillQuest(8518, "Arcadia", "Lightguard Wraith");

        // Unconditional 8519
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(8519, "Arcadia", "Agape");

        // Separation Anxiety 8520
        Story.KillQuest(8520, "Arcadia", "Agape");
        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(8520, "Arcadia", new[] {"Spirit Butterfly", "Lightguard Wraith" });

    }
}
