/*
name: Future Legion Story
description: This will complete the Future Legion story.
tags: story, quest, future, legion, legion, staff, birthday
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class FutureLegion
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoStory();
        Core.SetOptions(false);
    }

    public void DoStory()
    {
        if (Core.isCompletedBefore(5736))
            return;
        if (!Core.isSeasonalMapActive("futurelegion"))
            return;

        Story.PreLoad(this);

        //Examine the Area 5724
        Story.MapItemQuest(5724, "futurelegion", new[] { 5162, 5163, 5164 });
        Story.KillQuest(5724, "futurelegion", "UW3017 Gunner");

        //Get the Key 5725
        Story.KillQuest(5725, "futurelegion", "UW3017 Gunner");

        //Obtain Agravh's Soul 5726
        Story.MapItemQuest(5726, "futurelegion", 5165);
        Story.KillQuest(5726, "futurelegion", "Commander Agravh");

        //Obtain Uslaw's Soul 5727
        Story.MapItemQuest(5727, "futurelegion", 5166);
        Story.KillQuest(5727, "futurelegion", "Commander Uslaw");

        //Access the Control Room 5728
        Story.MapItemQuest(5728, "futurelegion", 5167);

        //Destory the Force Field 5729
        Story.KillQuest(5729, "futurelegion", "UW3017 Blaster");
        Story.MapItemQuest(5729, "futurelegion", 5168);

        //Obtain Ozar's Soul 5730
        Story.KillQuest(5730, "futurelegion", "Commander Ozar");

        //Obtain Pavon's Soul 5731
        Story.KillQuest(5731, "futurelegion", "Commander Pavon");

        //Activate the Teleporter 5732
        Story.MapItemQuest(5732, "futurelegion", 5169);

        //Keep It Grounded 5733
        Story.MapItemQuest(5733, "futurelegion", 5170, 7);
        Story.KillQuest(5733, "futurelegion", "SF3017 Gunner");

        //Get the Code 5734
        Story.KillQuest(5734, "futurelegion", new[] { "SF3017 Gunner", "SF3017 Blade" });

        //Open the Door 5735
        Story.MapItemQuest(5735, "futurelegion", 5171);

        //Take out the Legionator 5736
        Story.KillQuest(5736, "futurelegion", "Legionator");
    }
}
