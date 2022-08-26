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

        DeadmoorQuests();

        Core.SetOptions(false);
    }

    public void DeadmoorQuests()
    {
        if (Core.isCompletedBefore(4307))
            return;

        Story.PreLoad();

        //A Walking Nightmare 4296
        Story.MapItemQuest(4296, "Deadmoor", 3457);

        //A Ritual is Required 4297
        Story.KillQuest(4297, "Deadmoor", "Deadmoor Wraith");

        //A Spider's Finger 4298
        Story.KillQuest(4298, "Deadmoor", "Toxic Souleater");

        //Well Done 4299
        Story.MapItemQuest(4299, "Deadmoor", 3448);

        //Fighting A Horse 4300
        Story.KillQuest(4300, "Deadmoor", "Nightmare");

        //Wake the Dead 4301
        Story.MapItemQuest(4301, "Deadmoor", 3459);

        //Bone Breaking Bonds 4302
        Story.MapItemQuest(4302, "Deadmoor", new int[] { 3449, 3450, 3451, 3452, 3453, 3454 });

        //Last Shreds of Humanity 4303
        Story.KillQuest(4303, "Deadmoor", "Deadmoor Wraith|Toxic Souleater");

        //Consecrated Ground 4304
        Story.MapItemQuest(4304, "Deadmoor", 3455, 7);

        //Geist 4305
        Story.KillQuest(4305, "Deadmoor", "Geist");

        //The Last Caretaker 4306
        Story.MapItemQuest( 4306, "Deadmoor", new int[] { 3458, 3460, 3461, 3462, 3462, 3463, 3464, 3465 } );

        //The Confrontation 4307
        Story.KillQuest(4307, "Deadmoor", "Banshee Mallora");

    }
}
