/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Guru
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if ((Core.IsMember && Core.isCompletedBefore(53)) || (!Core.IsMember && Core.isCompletedBefore(52)))
            return;

        Story.PreLoad(this);

        //Golden Shrooms 47
        Story.MapItemQuest(47, "Guru", 20, 10);

        //Blue Feathers 48
        Story.KillQuest(48, "Guru", "Trobble");

        //Flying Tails 49
        Story.KillQuest(49, "Guru", "Leatherwing");

        //Mixing Pot 50
        Story.KillQuest(50, "Guru", "Guru Chest");

        //Hungry for a Recipe 51
        Story.KillQuest(51, "River", "River Fishman");

        //Missing Ingredient 52
        Story.KillQuest(52, "Guru", "Wisteria");

        if (Core.IsMember)
        {
            //Delicious Ingredient 53
            Story.KillQuest(53, "Guru", "Wisteria");

        }
    }
}
