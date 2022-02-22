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
       Core.AddDrop("Floozer", "Ice Diamond", "Dark Bloodstone", "Songstone", "Butterfly Sapphire",
         "Understone", "Rainbow Moonstone");
        Story.KillQuest(7277, "wanders", "Kalestri Worshiper", GetReward: false);
        if (!Story.isCompletedBefore(7280))
        {
            Core.EnsureAccept(7278);
            if (!Core.CheckInventory("Ice Diamond"))
                Core.SmartKillMonster(7279, "kingcoal", "Snow Golem", completeQuest: true);
            Core.EnsureComplete(7278);
        }
        if(!Story.isCompletedBefore(7282))
        {
            Core.EnsureAccept(7280);
            if(!Core.CheckInventory("Dark Bloodstone"))
                Core.SmartKillMonster(7281, "safiria", "Blood Maggot", completeQuest: true);
            Core.EnsureComplete(7280);
        }
        Story.KillQuest(7282, "brightfall", "Painadin Overlord", GetReward: false);
        Story.KillQuest(7283, "timevoid", "Unending Avatar", GetReward: false);
        Story.MapItemQuest(7284, "downward", 6908, GetReward: false);
        if(!Story.isCompletedBefore(7286))
        {
            Core.EnsureAccept(7285);
            if (!Core.CheckInventory("Songstone"))
            {
                Core.EnsureAccept(7297);
                Core.GetMapItem(6909, 15, "mythsong");
                Core.EnsureComplete(7297);
            }
            Core.EnsureComplete(7285);
        }
        if(!Story.isCompletedBefore(7288))
        {
            Core.EnsureAccept(7286);
            if(!Core.CheckInventory("Butterfly Sapphire"))
                Core.SmartKillMonster(7287, "bloodtusk", "Trollola Plant", completeQuest: true);
            Core.EnsureComplete(7286);
        }
        if(!Story.isCompletedBefore(7290))
        {
            Core.EnsureAccept(7288);
            if(!Core.CheckInventory("Understone"))
            {
                Under.BattleUnderB();
                Under.Understone(1);
            }
            Core.EnsureComplete(7288);
        }
        if (!Story.isCompletedBefore(7292))
        {
            Core.EnsureAccept(7290);
            if (!Core.CheckInventory("Rainbow Moonstone"))
            {
                Core.EnsureAccept(7291);
                Core.KillMonster("earthstorm", "r7", "Left", "Diamond Golem", "Chip of Diamond");
                Core.KillMonster("earthstorm", "r9", "Left", "Emerald Golem", "Chip of Emerald");
                Core.KillMonster("earthstorm", "r3", "Left", "Ruby Golem", "Chip of Ruby");
                Core.KillMonster("earthstorm", "r5", "Left", "Sapphire Golem", "Chip of Sapphire");
                Core.EnsureComplete(7291);
            }
            Core.EnsureComplete(7290);
        }
        Story.MapItemQuest(7292, "thespan", 6910, GetReward: false);
    }
}