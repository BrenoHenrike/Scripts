//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class DarkAlly_Story
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DarkAlly_Questline();

        Core.SetOptions(false);
    }

    public void DarkAlly_Questline()
    {
        if (Core.isCompletedBefore(7428))
            return;

        Story.PreLoad();

        Story.MapItemQuest(7419, "darkally", 7179, 6);
        Story.KillQuest(7419, "darkally", "Shadow");

        Story.KillQuest(7420, "darkally", new[] { "Underworld Golem", "Underworld Golem" });

        Story.MapItemQuest(7421, "darkally", 7180, 1);
        Story.MapItemQuest(7421, "darkally", 7181, 8);

        Story.KillQuest(7422, "darkally", "Dark Makai");

        Story.KillQuest(7423, "darkally", "Shadow|Creeping Shadow");

        Story.KillQuest(7424, "darkally", new[] { "Legion Defector", "Legion Defector" });

        Story.KillQuest(7425, "darkally", "Frozen Pyromancer");

        Story.KillQuest(7426, "darkally", "Underworld Golem");

        Story.MapItemQuest(7427, "darkally", 7182, 1);

        Story.KillQuest(7428, "darkally", "Underfiend");

    }

}
