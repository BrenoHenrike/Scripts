//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DwarvesVsGiants
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(2784))
            return;

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Solo);

        // Defeat Drram 2778
        Story.KillQuest(2778, "dvg", "Draam");

        // Take On Tergum 2779
        Story.KillQuest(2779, "dvg", "Tergum");

        // Smack Down Slork 2780
        Story.KillQuest(2780, "dvg", "Slork");

        // Crush Krashh 2781
        Story.KillQuest(2781, "dvg", "Krashh");

        // Win The Exhibition Match 2782
        Story.KillQuest(2782, "dvg", new[] { "Meatball", "Blixx" });

        // Mow Down Munthor 2783
        Story.KillQuest(2783, "dvg", "Munthor");

        if (!Core.IsMember)
        {
            Core.Logger("You must be a Member to complete DvG Challenge quests.");
            return;
        }

        // Challenge Meatball 2784
        Story.KillQuest(2784, "dvgchallenge", "Meatball");

        // Challenge Blixx 2784
        Story.KillQuest(2784, "dvgchallenge", "Blixx");
    }
}