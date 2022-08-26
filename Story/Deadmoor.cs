//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Deadmoor
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
        if (Core.isCompletedBefore(4307))
            return;

        Story.PreLoad();

        // 4296 A Walking Nightmare
        Story.MapItemQuest(4296, "deadmoor", 3457);
        
        // 4297 A Ritual is Required
        Story.KillQuest(4297, "deadmoor", "Deadmoor Wraith");

        // 4298 A Spider's Finger
        Story.KillQuest(4298, "deadmoor", "Toxic Souleater");

        // 4299 Well Done
        Story.MapItemQuest(4299, "deadmoor", 3448);

        // 4300 Fighting A Horse
        Story.KillQuest(4300, "deadmoor", "Nightmare");

        // 4301 Wake the Dead
        Story.MapItemQuest(4301, "deadmoor", 3459);

        // 4302 Bone Breaking Bonds
        Story.MapItemQuest(4302, "deadmoor", 3449);
        Story.MapItemQuest(4302, "deadmoor", 3450);
        Story.MapItemQuest(4302, "deadmoor", 3451);
        Story.MapItemQuest(4302, "deadmoor", 3452);
        Story.MapItemQuest(4302, "deadmoor", 3453);
        Story.MapItemQuest(4302, "deadmoor", 3454);

        // 4303 Last Shreds of Humanity
        Story.KillQuest(4303, "deadmoor", "Toxic Souleater");

        // 4304 Consecrated Ground
        Story.MapItemQuest(4304, "deadmoor", 3455, 7);

        // 4305 Geist
        Story.KillQuest(4305, "deadmoor", "Geist");

        // 4306 The Last Caretaker
        Story.MapItemQuest(4306, "deadmoor", 3458);
        Story.MapItemQuest(4306, "deadmoor", 3460);
        Story.MapItemQuest(4306, "deadmoor", 3461);
        Story.MapItemQuest(4306, "deadmoor", 3462);
        Story.MapItemQuest(4306, "deadmoor", 3463);
        Story.MapItemQuest(4306, "deadmoor", 3464);
        Story.MapItemQuest(4306, "deadmoor", 3465);

        // 4307 The Confrontation
        Story.KillQuest(4307, "deadmoor", "Banshee Mallora");
    }
}