//cs_include Scripts/CoreBots.cs
using RBot;

public class Originul_Story
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

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

        // Inquisite the Inquisitors
        Core.KillQuest(7881, "Originul", "Inquisitor Guard");
        // Captains Capped
        Core.KillQuest(7882, "Originul", "Inquisitor Captain");
        // Grand Defeat
        Core.KillQuest(7883, "Originul", "Grand Inquisitor");
        // Portal Unlocked
        Core.KillQuest(7884, "Originul", new[] { "Inquisitor Guard", "Inquisitor Captain", "Grand Inquisitor" });
        // Fiend Training
        Core.KillQuest(7885, "Originul", "Bloodfiend");
        // Failed Fiend Shards
        Core.KillQuest(7886, "Originul", "Bloodfiend|Dreadfiend");
        // Executed Tasks
        Core.KillQuest(7887, "Originul", "Dreadfiend");
        // Champion Usurper
        Core.KillQuest(7888, "Originul", "Fiend Champion");
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
