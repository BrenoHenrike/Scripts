//cs_include Scripts/CoreBots.cs
using RBot;
public class ParadoxPortal
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        ParadoxPortalSaga();

        Core.SetOptions(false);
    }

    public void ParadoxPortalSaga()
    {
        // Through the Portal!
        Core.KillQuest(5034, "portalmaze", "Time Wraith");
        // Red Alert
        Core.KillQuest(5035, "portalmaze", "Heavy Lab Guard");
        // Bugs... They Give Me Hives
        Core.KillQuest(5036, "portalmaze", "Hatchling|Pillbug");
        // Through the Delta V Portal!
        Core.KillQuest(5037, "portalmaze", "Time Wraith");
        // Hwoounga? Unf!
        Core.KillQuest(5038, "portalmaze", "Jurassic Monkey");
        // They're Firin' Their LAZORS
        Core.KillQuest(5039, "portalmaze", "Lazor Dino");
        // Through the Jurassic Portal!
        Core.KillQuest(5040, "portalmaze", "Time Wraith");
        // Where's Twilly?!?
        Core.MapItemQuest(5041, "portalmaze", 4409, 1);
        Core.MapItemQuest(5041, "portalmaze", 4408, 1);
        Core.MapItemQuest(5041, "portalmaze", 4410, 1);
        // Through the Yulgar Portal!
        Core.KillQuest(5042, "portalmaze", "Time Wraith");
        // Ode to the Walkers
        Core.KillQuest(5043, "portalmaze", "Bucket Zombie|Dancing Zombie|Tunneling Zombie");
        // Sonnet of the Undead
        Core.KillQuest(5044, "portalmaze", new[] { "Bucket Zombie", "Dancing Zombie", "Tunneling Zombie" });
        // Through the Zombie Portal!
        Core.KillQuest(5045, "portalmaze", "Time Wraith");
        // Your Lips Are Sealed
        Core.KillQuest(5046, "portalmaze", "Pactagonal Knight");
        // Through the Swordhaven Portal!
        Core.KillQuest(5047, "portalmaze", "Time Wraith");
        // Escape from the ChronoLords
        Core.KillQuest(5048, "portalmaze", "ChronoLord");
        // Vorefax
        Core.KillQuest(5049, "portalmaze", "Vorefax");
        // The Death of Time
        Core.KillQuest(5050, "portalmaze", "Mors Temporis");
    }
}
