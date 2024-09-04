/*
name: Yguasu Falls Story
description: This will complete the Yguasu Falls story.
tags: story, quest, hero, heart, yguasu falls, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class YguasuFalls
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoStory();

        Core.SetOptions(false);
    }

    public void DoStory()
    {
        if (!Core.isSeasonalMapActive("yguasu") || Core.isCompletedBefore(9587))
            return;

        Story.PreLoad(this);

        // Designer Scales 9580
        Story.KillQuest(9580, "yguasu", "Boiuna");

        // Harpy-Pecked 9581
        Story.KillQuest(9581, "yguasu", "Harpy Dancer");

        // Dancer Incognito 9582
        Story.MapItemQuest(9582, "yguasu", 12688);
        Story.KillQuest(9582, "yguasu", "Lovely Mask");

        // Costume Check 9583
        Story.KillQuest(9583, "yguasu", new[] { "Boiuna", "Harpy Dancer" });

        // Transformative Medicine 9584
        Story.KillQuest(9584, "yguasu", "Lobisomen");

        // Rocked My Life 9585
        Story.KillQuest(9585, "yguasu", "Boiuna");

        Story.MapItemQuest(9585, "yguasu", 12689);

        // Reaching Palm Fronds 9586
        Story.MapItemQuest(9586, "yguasu", 12690);
        Story.KillQuest(9586, "yguasu", "Lobisomen");

        // That's M'Boi 9587
        Story.KillQuest(9587, "yguasu", "M'Boi");
    }
}
