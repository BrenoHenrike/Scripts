//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class ParadoxPortal
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        ParadoxPortalSaga();

        Core.SetOptions(false);
    }

    public void ParadoxPortalSaga()
    {
        if (Core.isCompletedBefore(5050))
            return;

        // Through the Portal!
        Story.KillQuest(5034, "portalmaze", "Time Wraith");
        // Red Alert
        Story.KillQuest(5035, "portalmaze", "Heavy Lab Guard");
        // Bugs... They Give Me Hives
        Story.KillQuest(5036, "portalmaze", "Hatchling|Pillbug");
        // Through the Delta V Portal!
        Story.KillQuest(5037, "portalmaze", "Time Wraith");
        // Hwoounga? Unf!
        Story.KillQuest(5038, "portalmaze", "Jurassic Monkey");
        // They're Firin' Their LAZORS
        Story.KillQuest(5039, "portalmaze", "Lazor Dino");
        // Through the Jurassic Portal!
        Story.KillQuest(5040, "portalmaze", "Time Wraith");
        // Where's Twilly?!?
        Story.MapItemQuest(5041, "portalmaze", 4409, 1);
        Story.MapItemQuest(5041, "portalmaze", 4408, 1);
        Story.MapItemQuest(5041, "portalmaze", 4410, 1);
        // Through the Yulgar Portal!
        Story.KillQuest(5042, "portalmaze", "Time Wraith");
        // Ode to the Walkers
        Story.KillQuest(5043, "portalmaze", "Bucket Zombie|Dancing Zombie|Tunneling Zombie");
        // Sonnet of the Undead
        Story.KillQuest(5044, "portalmaze", new[] { "Bucket Zombie", "Dancing Zombie", "Tunneling Zombie" });
        // Through the Zombie Portal!
        Story.KillQuest(5045, "portalmaze", "Time Wraith");
        // Your Lips Are Sealed
        Story.KillQuest(5046, "portalmaze", "Pactagonal Knight");
        // Through the Swordhaven Portal!
        Story.KillQuest(5047, "portalmaze", "Time Wraith");
        // Escape from the ChronoLords
        Story.KillQuest(5048, "portalmaze", "ChronoLord");
        // Vorefax
        Story.KillQuest(5049, "portalmaze", "Vorefax");
        // The Death of Time
        Story.KillQuest(5050, "portalmaze", "Mors Temporis");
    }
}
