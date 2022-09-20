//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class MountOtzi
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        MountOtziQuests();

        Core.SetOptions(false);
    }

    public void MountOtziQuests()
    {
        if (Core.isCompletedBefore(8444))
            return;

        Story.PreLoad(this);

        // Light Midnight
        Story.MapItemQuest(8434, "MountOtzi", 9437, 7);

        // Actaeon Stew
        Story.KillQuest(8435, "MountOtzi", "Stitched Stag");

        // Vain Howl
        Story.KillQuest(8436, "MountOtzi", "Gauden Hound");

        // Holle's Meal
        if (!Story.QuestProgression(8437))
        {
            Story.MapItemQuest(8437, "MountOtzi", 9388);
            Story.MapItemQuest(8437, "MountOtzi", 9387, 6);
            Story.KillQuest(8437, "MountOtzi", "Stitched Stag");
        }

        // The Hidden One
        if (!Story.QuestProgression(8438))
        {
            Story.MapItemQuest(8438, "MountOtzi", 9389);
            Story.KillQuest(8438, "MountOtzi", "Gauden Hound");
        }

        //MountOtzi's Stones
        if (!Story.QuestProgression(8439))
        {
            Story.MapItemQuest(8439, "MountOtzi", 9390, 7);
            Story.KillQuest(8439, "MountOtzi", new[] { "Gauden Hound", "Mangled Stag" });
        }

        //Faceless Hunters
        if (!Story.QuestProgression(8440))
        {
            Story.KillQuest(8440, "MountOtzi", "Sluagh Warrior");
            Story.MapItemQuest(8440, "MountOtzi", 9391);
        }

        //Stitch Work
        if (!Story.QuestProgression(8441))
        {
            Story.KillQuest(8441, "MountOtzi", "Mangled Stag");
            Story.MapItemQuest(8441, "MountOtzi", 9392);
        }

        //Killer Promotion
        if (!Story.QuestProgression(8442))
        {
            Story.KillQuest(8442, "MountOtzi", "Sluagh Warrior");
            Story.MapItemQuest(8442, "MountOtzi", 9393, 7);
        }

        //Cold Pleasures
        if (!Story.QuestProgression(8443))
        {
            Story.KillQuest(8443, "MountOtzi", "Sluagh Warrior");
            Story.MapItemQuest(8443, "MountOtzi", 9394);
        }

        //Corvus Mellori
        Story.KillQuest(8444, "MountOtzi", "Sluagh Mellori", AutoCompleteQuest: false);
    }
}
