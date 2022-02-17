//cs_include Scripts/CoreBots.cs
using RBot;

public class DarkAlly_Story
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

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

        Core.MapItemQuest(7419, "darkally", 7179, 6);
        Core.KillQuest(7419, "darkally", "Shadow");

        Core.KillQuest(7420, "darkally", new[] { "Underworld Golem", "Underworld Golem" });

        Core.MapItemQuest(7421, "darkally", 7180, 1);
        Core.MapItemQuest(7421, "darkally", 7181, 8);

        Core.KillQuest(7422, "darkally", "Dark Makai");

        Core.KillQuest(7423, "darkally", "Shadow|Creeping Shadow");

        Core.KillQuest(7424, "darkally", new[] { "Legion Defector", "Legion Defector" });

        Core.KillQuest(7425, "darkally", "Frozen Pyromancer");

        Core.KillQuest(7426, "darkally", "Underworld Golem");

        Core.MapItemQuest(7427, "darkally", 7182, 1);

        Core.KillQuest(7428, "darkally", "Underfiend");

    }

}
