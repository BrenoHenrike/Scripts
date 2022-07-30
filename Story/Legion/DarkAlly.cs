//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DarkAlly_Story
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
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

        if (!Story.QuestProgression(7419))
        {
            Story.MapItemQuest(7419, "darkally", 7179, 6);
            Core.KillMonster("darkally", "r2", "Left", "Shadow", "Shadow Destroyed", 10);
            Core.EnsureComplete(7419);
        }

        Story.KillQuest(7420, "darkally", new[] { "Underworld Golem", "Underworld Golem" });

        Story.MapItemQuest(7421, "darkally", 7180, 1);
        Story.MapItemQuest(7421, "darkally", 7181, 8);

        if (!Story.QuestProgression(7422))
        {
            Core.EnsureAccept(7422);
            Core.Join("Darkally", "r2", "Left");
            while (!Bot.ShouldExit && !Core.CheckInventory(53855, 10))
                Bot.Kill.Monster("Dark Makai");
            Core.EnsureComplete(7422);
        }

        if (!Story.QuestProgression(7423))
        {
            Core.EnsureAccept(7423);
            Core.KillMonster("Darkally", "r2", "Left", 4452, "Shredded Shadow", 9);
            Core.EnsureComplete(7423);
        }

        Story.KillQuest(7424, "darkally", new[] { "Legion Defector", "Legion Defector" });

        Story.KillQuest(7425, "darkally", "Frozen Pyromancer");

        Story.KillQuest(7426, "darkally", "Underworld Golem");

        Story.MapItemQuest(7427, "darkally", 7182, 1);

        Story.KillQuest(7428, "darkally", "Underfiend");

    }

}
