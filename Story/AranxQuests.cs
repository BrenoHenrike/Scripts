/*
name: Aranx Quests
description: This script will do all quests of aranx in /darkdimension, /icedimension, /Ivoliss, /Pocketdimension and /sanddimension
tags: aranx, darkdimension, icedimension, ivoliss, pocketdimension, sanddimension
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class AranxQuests
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        StoryLine();
        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(6508))
            return;

        Story.PreLoad(this);
        //Find the Resonance 6496
        Story.KillQuest(6496, "pocketdimension", "Nothing");

        //Tune it up! 6497
        Story.MapItemQuest(6497, "pocketdimension", 5988, 3);

        //Explore the Ice Dimension 6498
        Story.MapItemQuest(6498, "icedimension", 5989);
        Story.KillQuest(6498, "icedimension", "Ice Spitter");

        //Open the Next Dimension 6499
        Story.MapItemQuest(6499, "icedimension", 5990, 5);
        Story.KillQuest(6499, "icedimension", "Ice Elemental");

        //Explore the Sand Dimension 6500
        Story.MapItemQuest(6500, "sanddimension", 5991);
        Story.KillQuest(6500, "sanddimension", "Lotus Spider");

        //Open the 3rd Dimension 6501
        Story.MapItemQuest(6501, "sanddimension", 5992, 5);
        Story.KillQuest(6501, "sanddimension", "Lotus Spider");

        //Explore the Dark Dimension 6502
        Story.MapItemQuest(6502, "darkdimension", 5993);
        Story.KillQuest(6502, "darkdimension", "Void Phoenix");

        //Open the 4th Dimension 6503
        Story.MapItemQuest(6503, "darkdimension", 5994, 5);
        Story.KillQuest(6503, "darkdimension", "Void Phoenix");

        //Find Arthelyn 6504
        Story.MapItemQuest(6504, "ivoliss", 5995);

        //Get the Key 6505
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6505, "ivoliss", "ivoliss");

        //Free Arthelyn 6506
        Story.MapItemQuest(6506, "ivoliss", 5996);
        Story.MapItemQuest(6506, "ivoliss", 5998);

        //Reach Arthelyn 6507
        Story.MapItemQuest(6507, "ivoliss", 5997);

        //Defeat Arthelyn 6508
        Story.KillQuest(6508, "ivoliss", "Arthelyn");

    }
}
