/*
name: Ice Plane Story
description: This will finish the Ice Plane Story.
tags: story, quest, ice plane, iceplane, ice, plane, glace, quetzal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class IcePlane
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
        if (Core.isCompletedBefore(5783))
            return;

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // Find the Core (5777)
        Story.KillQuest(5777, "iceplane", "Spirit of Ice");

        // Make a Bridge (5778)
        Story.KillQuest(5778, "iceplane", "Frozen Jellyfish");
        Story.MapItemQuest(5778, "iceplane", 5218);

        // Don't Fall (5779)
        Story.MapItemQuest(5779, "iceplane", new[] { 5219, 5220 });

        // Defeat the Shade (5780)
        if (!Core.isCompletedBefore(5780))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(5780, "iceplane", "Crystalline Shade");
            Story.MapItemQuest(5780, "iceplane", 5221);
            Core.EquipClass(ClassType.Farm);
        }

        // Ice is Sharp (5781)
        Story.KillQuest(5781, "iceplane", "Frostblade");

        // Release the Energy (5782)
        Story.KillQuest(5782, "iceplane", "Animus of Ice");

        // Defeat the Enfield (5783)
        if (!Core.isCompletedBefore(5783))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(5783, "iceplane", "Enfield");
            Core.EquipClass(ClassType.Farm);
        }

    }
}
