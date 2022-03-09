//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/BattleUnder.cs
using RBot;

public class Bamboozle
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreStory Story = new CoreStory();
    public BattleUnder Under = new BattleUnder();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        BamboozleQuest();

        Core.SetOptions(false);
    }

    public void BamboozleQuest()
    {
        if (Core.isCompletedBefore(7292))
            return;
        Core.AddDrop("Floozer", "Ice Diamond", "Dark Bloodstone", "Songstone", "Butterfly Sapphire",
         "Understone", "Rainbow Moonstone");
        Story.KillQuest(7277, "wanders", "Kalestri Worshiper", GetReward: false);
        if (!Story.QuestProgression(7278))
        {
            Core.EnsureAccept(7278);
            if (!Core.CheckInventory("Ice Diamond"))
                Story.KillQuest(7279, "kingcoal", "Snow Golem");
            Core.EnsureComplete(7278);
        }
        if (!Story.QuestProgression(7280))
        {
            Core.EnsureAccept(7280);
            if (!Core.CheckInventory("Dark Bloodstone"))
                Story.KillQuest(7281, "safiria", "Blood Maggot");
            Core.EnsureComplete(7280);
        }
        Story.KillQuest(7282, "brightfall", "Painadin Overlord", GetReward: false);
        Story.KillQuest(7283, "timevoid", "Unending Avatar", GetReward: false);
        Story.MapItemQuest(7284, "downward", 6908, GetReward: false);
        if (!Story.QuestProgression(7285))
        {
            Core.EnsureAccept(7285);
            if (!Core.CheckInventory("Songstone"))
                Story.MapItemQuest(7297, "mythsong", 6909, 15);
            Core.EnsureComplete(7285);
        }
        if (!Story.QuestProgression(7286))
        {
            Core.EnsureAccept(7286);
            if (!Core.CheckInventory("Butterfly Sapphire"))
                Story.KillQuest(7287, "bloodtusk", "Trollola Plant");
            Core.EnsureComplete(7286);
        }
        if (!Story.QuestProgression(7288))
        {
            Core.EnsureAccept(7288);
            if (!Core.CheckInventory("Understone"))
            {
                Under.BattleUnderB();
                Under.Understone();
            }
            Core.EnsureComplete(7288);
        }
        if (!Story.QuestProgression(7290))
        {
            Core.EnsureAccept(7290);
            if (!Core.CheckInventory("Rainbow Moonstone"))
                Story.KillQuest(7291, "earthstorm", new[] { "Diamond Golem", "Emerald Golem", "Ruby Golem", "Sapphire Golem" });
            Core.EnsureComplete(7290);
        }
        Story.MapItemQuest(7292, "thespan", 6910, GetReward: false);
    }
}