//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Tricktown
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
        if (Core.isCompletedBefore(8435))
            return;

        Story.PreLoad(this);

        // Punchergeist 8926
        Story.MapItemQuest(8926, "tricktown", 10813);
        Story.KillQuest(8926, "tricktown", "Playful Ghost");

        // Party Town 8927
        Story.MapItemQuest(8927, "tricktown", 10804, 3);
        Story.KillQuest(8927, "tricktown", "Decay Spirit");

        // Sweet Mucus 8928
        Story.MapItemQuest(8928, "tricktown", 10805, 5);
        Story.KillQuest(8928, "tricktown", "Playful Ghost");

        //Door to Door 8929
        if (!Story.QuestProgression(8929))
        {
            Core.EnsureAccept(8929);
            while (!Bot.ShouldExit && !Core.CheckInventory(73465, 300))
            {
                Core.AddDrop("Treats");
                Core.Join("tricktown");
                Core.KillMonster("trickortreat", "Enter", "Spawn", "Trick or Treater");
                Bot.Wait.ForPickup("Treats");
            }
            Core.EnsureComplete(8929);
        }

        // Crusted Compost 8930
        Story.MapItemQuest(8930, "tricktown", 10806, 3);
        Story.KillQuest(8930, "tricktown", "Rotting Pumpkin");

        // A Spot of Sludge 8931
        Story.MapItemQuest(8931, "tricktown", 10807, 4);
        Story.KillQuest(8931, "tricktown", "Decay Spirit");

        // Messy Unrest 8932
        Story.KillQuest(8932, "tricktown", new[] { "Decay Spirit", "Playful Ghost" });

        // Plain Monster 8933
        Story.MapItemQuest(8933, "tricktown", 10808);
        Story.KillQuest(8933, "tricktown", "Rotting Mound");

        // Study Group 8934
        Story.MapItemQuest(8934, "tricktown", 10809, 5);
        Story.KillQuest(8934, "tricktown", "Rotting Mound");

        // The Fruits of Labor 8435
        Story.KillQuest(8435, "tricktown", "Madam Ester");
    }
}
