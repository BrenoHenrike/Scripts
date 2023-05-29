/*
name: DarkDungeon
description: This script finish all dark dungeon quests storyline in /darkdungeon
tags: dark, dungeon, story, line, mirror, drakath
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DarkDungeon
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();


    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        storyline();
        Core.SetOptions(false);
    }

    public void storyline()
    {
        if (Core.isCompletedBefore(3545))
            return;

        Story.PreLoad(this);
        //Bright Idea 2909
        Story.MapItemQuest(2909, "battleoff", 17375);

        //Spare Parts 2910
        Story.MapItemQuest(2910, "battleoff", 1780, 8);

        //Power It Up 2911
        Story.KillQuest(2911, "battleoff", "Evil Moglin");

        //Filthy Creatures 2912
        Story.KillQuest(2912, "battleoff", "Evil Moglin");

        //Quiz the Locals 3538
        Story.MapItemQuest(3538, "darkdungeon", new[] { 2671, 2672, 2673 });

        //Slay the Minions 3539
        Story.KillQuest(3539, "darkdungeon", "Dungeon Minion");

        //Free the Fallen 3540
        Story.KillQuest(3540, "darkdungeon", "Dungeon Paladin");

        //Break the Doors Down 3542
        Story.MapItemQuest(3542, "darkdungeon", 2679, 5);

        //The Best vs The Beast 3543
        Story.KillQuest(3543, "darkdungeon", "Cockatrice");

        //Go To Sleep 3545
        Story.KillQuest(3545, "darkdungeon", new[] { "Dungeon Minion", "Dungeon Paladin" });
    }
}
