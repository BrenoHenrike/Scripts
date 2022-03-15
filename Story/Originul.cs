//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class Originul_Story
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Originul_Questline();

        Core.SetOptions(false);
    }

    public void Originul_Questline()
    {
        if (Core.isCompletedBefore(7889))
            return;

        Story.PreLoad();

        // Inquisite the Inquisitors
        Story.KillQuest(7881, "Originul", "Inquisitor Guard");
        // Captains Capped
        Story.KillQuest(7882, "Originul", "Inquisitor Captain");
        // Grand Defeat
        Story.KillQuest(7883, "Originul", "Grand Inquisitor");
        // Portal Unlocked
        Story.KillQuest(7884, "Originul", new[] { "Inquisitor Guard", "Inquisitor Captain", "Grand Inquisitor" });
        // Fiend Training
        Story.KillQuest(7885, "Originul", "Bloodfiend");
        // Failed Fiend Shards
        Story.KillQuest(7886, "Originul", "Bloodfiend|Dreadfiend");
        // Executed Tasks
        Story.KillQuest(7887, "Originul", "Dreadfiend");
        // Champion Usurper
        Story.KillQuest(7888, "Originul", "Fiend Champion");
        // Break their Muti-kneecaps
        Core.EnsureAccept(7889);
        Core.Join("Originul", "r10", "Top");
        while (!Bot.Quests.CanComplete(7889))
        {
            Bot.Player.Kill("Bloodfiend");
            Bot.Player.Kill("Dreadfiend");
        }
        Core.EnsureComplete(7889);

        Core.Logger("Questline completed.");
    }

}
