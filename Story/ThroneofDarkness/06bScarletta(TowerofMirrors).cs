//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Story/ThroneofDarkness/06aScarletta(ShatterGlassMaze).cs
using RBot;
public class TowerofMirrors
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public HedgeMaze HM = new HedgeMaze();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        TowerofMirrorsSaga();

        Core.SetOptions(false);
    }

    public void TowerofMirrorsSaga()
    {
        if (Core.isCompletedBefore(5332))
            return;

        Core.EquipClass(ClassType.Solo);

        HM.HedgeMaze_Questline();

        // Drink Me
        Core.KillQuest(5314, "towerofmirrors", new[] { "Glassgoyle|Glass Serpent", "Glass Serpent" });
        // The Key To Success
        Core.KillQuest(5315, "towerofmirrors", "Silver Elemental");
        Core.MapItemQuest(5315, "towerofmirrors", 4691, 1);
        Core.MapItemQuest(5315, "towerofmirrors", 4692, 1);
        // Phanatics
        Core.KillQuest(5316, "towerofmirrors", new[] { "Phans", "Phans" });
        // But I Have A Backstage Pass!
        Core.KillQuest(5317, "towerofmirrors", "Lothahnos the Reversed");
        // True Love
        Core.KillQuest(5318, "towerofmirrors", "Silver Elemental");
        Core.MapItemQuest(5318, "towerofmirrors", 4687, 1);
        Core.MapItemQuest(5318, "towerofmirrors", 4693, 1);
        // Turn to the Left
        Core.KillQuest(5319, "towerofmirrors", new[] { "Runway Wraith", "Runway Wraith", "Runway Wraith", "Runway Wraith" });
        // Now Turn to the Right
        Core.KillQuest(5320, "towerofmirrors", "Lukcrisio the Buffed");
        // Or Maybe THIS Is True Love
        Core.KillQuest(5321, "towerofmirrors", "Silver Elemental");
        Core.MapItemQuest(5321, "towerofmirrors", 4688, 1);
        Core.MapItemQuest(5321, "towerofmirrors", 4694, 1);
        // Those Harpies!
        Core.KillQuest(5322, "towerofmirrors", new[] { "Pageant Mom", "Pageant Mom" });
        // Drink your Go-Go Juice
        Core.KillQuest(5323, "towerofmirrors", "Medeskar the Smudged");
        // Oh Sure, Why Not
        Core.KillQuest(5324, "towerofmirrors", "Silver Elemental");
        Core.MapItemQuest(5324, "towerofmirrors", 4689, 1);
        Core.MapItemQuest(5324, "towerofmirrors", 4695, 1);
        // Behind the Scenes
        Core.KillQuest(5325, "towerofmirrors", new[] { "Stage Tech", "Stage Tech" });
        // In the Spotlight
        Core.KillQuest(5326, "towerofmirrors", "Atticus the Warped");
        // Oh, I Give Up
        Core.KillQuest(5327, "towerofmirrors", "Silver Elemental");
        Core.MapItemQuest(5327, "towerofmirrors", 4690, 1);
        Core.MapItemQuest(5327, "towerofmirrors", 4696, 1);
        // We Gotta Wendi-GO
        Core.KillQuest(5328, "towerofmirrors", new[] { "Sasquatch", "Sasquatch" });
        // Kick His- Wait, Did We Already Use That Pun?
        Core.KillQuest(5329, "towerofmirrors", "Leofire the Shattered");
        // Fiend Zoned
        Core.KillQuest(5330, "towerofmirrors", "Fervent Suitor");
        // Find Scarletta
        Core.MapItemQuest(5331, "towerofmirrors", 4697, 1);
        // Defeat ... Wait. What?
        Core.KillQuest(5332, "towerofmirrors", "Scarletta");
    }
}
