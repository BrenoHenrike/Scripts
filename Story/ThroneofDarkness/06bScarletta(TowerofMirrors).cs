//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ThroneofDarkness/06aScarletta(ShatterGlassMaze).cs
using RBot;
public class TowerofMirrors
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public HedgeMaze HM = new HedgeMaze();
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        TowerofMirrorsSaga();

        Core.SetOptions(false);
    }

    public void TowerofMirrorsSaga()
    {
        if (Story.isCompletedBefore(5332))
            return;

        HM.HedgeMaze_Questline();

        // Drink Me
        Story.KillQuest(5314, "towerofmirrors", new[] { "Glassgoyle|Glass Serpent", "Glass Serpent" });
        // The Key To Success
        Story.KillQuest(5315, "towerofmirrors", "Silver Elemental");
        Story.MapItemQuest(5315, "towerofmirrors", 4691, 1);
        Story.MapItemQuest(5315, "towerofmirrors", 4692, 1);
        // Phanatics
        Story.KillQuest(5316, "towerofmirrors", new[] { "Phans", "Phans" });
        // But I Have A Backstage Pass!
        Story.KillQuest(5317, "towerofmirrors", "Lothahnos the Reversed");
        // True Love
        Story.KillQuest(5318, "towerofmirrors", "Silver Elemental");
        Story.MapItemQuest(5318, "towerofmirrors", 4687, 1);
        Story.MapItemQuest(5318, "towerofmirrors", 4693, 1);
        // Turn to the Left
        Story.KillQuest(5319, "towerofmirrors", new[] { "Runway Wraith", "Runway Wraith", "Runway Wraith", "Runway Wraith" });
        // Now Turn to the Right
        Story.KillQuest(5320, "towerofmirrors", "Lukcrisio the Buffed");
        // Or Maybe THIS Is True Love
        Story.KillQuest(5321, "towerofmirrors", "Silver Elemental");
        Story.MapItemQuest(5321, "towerofmirrors", 4688, 1);
        Story.MapItemQuest(5321, "towerofmirrors", 4694, 1);
        // Those Harpies!
        Story.KillQuest(5322, "towerofmirrors", new[] { "Pageant Mom", "Pageant Mom" });
        // Drink your Go-Go Juice
        Story.KillQuest(5323, "towerofmirrors", "Medeskar the Smudged");
        // Oh Sure, Why Not
        Story.KillQuest(5324, "towerofmirrors", "Silver Elemental");
        Story.MapItemQuest(5324, "towerofmirrors", 4689, 1);
        Story.MapItemQuest(5324, "towerofmirrors", 4695, 1);
        // Behind the Scenes
        Story.KillQuest(5325, "towerofmirrors", new[] { "Stage Tech", "Stage Tech" });
        // In the Spotlight
        Story.KillQuest(5326, "towerofmirrors", "Atticus the Warped");
        // Oh, I Give Up
        Story.KillQuest(5327, "towerofmirrors", "Silver Elemental");
        Story.MapItemQuest(5327, "towerofmirrors", 4690, 1);
        Story.MapItemQuest(5327, "towerofmirrors", 4696, 1);
        // We Gotta Wendi-GO
        Story.KillQuest(5328, "towerofmirrors", new[] { "Sasquatch", "Sasquatch" });
        // Kick His- Wait, Did We Already Use That Pun?
        Story.KillQuest(5329, "towerofmirrors", "Leofire the Shattered");
        // Fiend Zoned
        Story.KillQuest(5330, "towerofmirrors", "Fervent Suitor");
        // Find Scarletta
        Story.MapItemQuest(5331, "towerofmirrors", 4697, 1);
        // Defeat ... Wait. What?
        Story.KillQuest(5332, "towerofmirrors", "Scarletta");
    }
}
