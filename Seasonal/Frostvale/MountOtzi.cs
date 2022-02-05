//cs_include Scripts/CoreBots.cs
using RBot;
public class MountOtzi
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        MountOtziQuests();

        Core.SetOptions(false);
    }

    public void MountOtziQuests()
    {
        if (Bot.Quests.IsUnlocked(8444))
            return;

        // Light Midnight
        Core.MapItemQuest(8434, "MountOtzi", 9437, 7);

        // Actaeon Stew
        Core.KillQuest(8435, "MountOtzi", "Stitched Stag");

        // Vain Howl
        Core.KillQuest(8436, "MountOtzi", "Gauden Hound");

        // Holle's Meal
        if (!Core.QuestProgression(8437)) {
            Core.MapItemQuest(8437, "MountOtzi", 9388);
            Core.MapItemQuest(8437, "MountOtzi", 9387, 6);
            Core.KillQuest(8437, "MountOtzi", "Stitched Stag");
        }

        // The Hidden One
        if (!Core.QuestProgression(8438)) {
            Core.MapItemQuest(8438, "MountOtzi", 9389);
            Core.KillQuest(8438, "MountOtzi", "Gauden Hound");
        }

        //MountOtzi's Stones
        if (!Core.QuestProgression(8439)) {
            Core.MapItemQuest(8439, "MountOtzi", 9390, 7);
            Core.KillQuest(8439, "MountOtzi", new[] {"Gauden Hound", "Mangled Stag"});
        }

        //Faceless Hunters
        if (!Core.QuestProgression(8440)) {
            Core.KillQuest(8440, "MountOtzi", "Sluagh Warrior");
            Core.MapItemQuest(8440, "MountOtzi", 9391);
        }

        //Stitch Work
        if (!Core.QuestProgression(8441)) {
            Core.KillQuest(8441, "MountOtzi", "Mangled Stag");
            Core.MapItemQuest(8441, "MountOtzi", 9392);
        }

        //Killer Promotion
        if (!Core.QuestProgression(8442)) {
            Core.KillQuest(8442, "MountOtzi", "Sluagh Warrior");
            Core.MapItemQuest(8442, "MountOtzi", 9393, 7);
        }

        //Cold Pleasures
        if (!Core.QuestProgression(8443)) {
            Core.KillQuest(8443, "MountOtzi", "Sluagh Warrior");
            Core.MapItemQuest(8443, "MountOtzi", 9394);
        }

        //Corvus Mellori
        Core.KillQuest(8444, "MountOtzi", "Sluagh Mellori", hasFollowup: false, AutoCompleteQuest: false);
    }
}
