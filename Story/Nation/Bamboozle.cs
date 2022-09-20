//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/BattleUnder.cs
using Skua.Core.Interfaces;

public class Bamboozle
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public BattleUnder Under = new BattleUnder();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BamboozleQuest();

        Core.SetOptions(false);
    }

    public void BamboozleQuest()
    {
        if (Core.isCompletedBefore(7292))
            return;

        Story.PreLoad(this);

        Core.AddDrop("Floozer", "Ice Diamond", "Dark Bloodstone", "Songstone", "Butterfly Sapphire", "Understone", "Rainbow Moonstone", "Time Quartz");

        //Star of the Sandsea
        if (!Story.QuestProgression(7277))
        {
            Bot.Quests.UpdateQuest(976);
            Core.EnsureAccept(7277);
            Core.HuntMonster("wanders", "Kalestri Worshiper", "Star of the Sandsea");
            Core.EnsureComplete(7277);
        }

        //Ice Diamond
        if (!Story.QuestProgression(7278))
        {
            if (!Core.CheckInventory("Ice Diamond"))
            {
                Core.EnsureAccept(7278, 7279);
                Core.HuntMonster("kingcoal", "Snow Golem", "Frozen Coal", 10);
                Core.EnsureComplete(7279);
                Bot.Wait.ForPickup("Ice Diamond");
            }
            Core.EnsureComplete(7278);
        }

        //Dark Bloodstone
        if (!Story.QuestProgression(7280))
        {
            if (!Core.CheckInventory("Dark Bloodstone"))
            {
                Core.EnsureAccept(7280, 7281);
                Core.HuntMonster("safiria", "Blood Maggot", "Blood Gem", 10);
                Core.EnsureComplete(7281);
                Bot.Wait.ForPickup("Dark Bloodstone");
            }
            Core.EnsureComplete(7280);
        }

        //Doomstone
        Story.KillQuest(7282, "brightfall", "Painadin Overlord", GetReward: false);

        //Void Opal
        Story.KillQuest(7283, "timevoid", "Unending Avatar", GetReward: false);

        //Mana Crystal
        Story.MapItemQuest(7284, "downward", 6908, GetReward: false);

        //Songstone
        if (!Story.QuestProgression(7285))
        {
            if (!Core.CheckInventory("Songstone"))
            {
                Core.EnsureAccept(7285, 7297);
                Core.GetMapItem(6909, 15, "mythsong");
                Core.EnsureComplete(7297);
                Bot.Wait.ForPickup("Songstone");
            }
            Core.EnsureComplete(7285);
        }

        //Butterfly Sapphire
        if (!Story.QuestProgression(7286))
        {
            Core.EnsureAccept(7286);
            if (!Core.CheckInventory("Butterfly Sapphire"))
            {

                Core.EnsureAccept(7287);
                Core.HuntMonster("bloodtusk", "Trollola Plant", "Butterfly Bloom", 15);
                Core.EnsureComplete(7287);
                Bot.Wait.ForPickup("Butterfly Sapphire");
            }
            Core.EnsureComplete(7286);

        }

        //Understone
        if (!Story.QuestProgression(7288))
        {
            if (!Core.CheckInventory("Understone"))
            {
                Bot.Quests.UpdateQuest(935);
                Under.Understone();
                Bot.Wait.ForPickup("Understone");
                Core.ChainComplete(7288);
            }
        }

        //Rainbow Moonstone
        if (!Story.QuestProgression(7290) || !Core.CheckInventory("Floozer"))
        {
            Core.EnsureAccept(7290);
            if (!Core.CheckInventory("Rainbow Moonstone"))
            {
                Core.EnsureAccept(7291);
                Core.HuntMonster("earthstorm", "Sapphire Golem", "Chip of Sapphire");
                Core.HuntMonster("earthstorm", "Ruby Golem", "Chip of Ruby");
                Core.HuntMonster("earthstorm", "Emerald Golem", "Chip of Emerald");
                Core.HuntMonster("earthstorm", "Diamond Golem", "Chip of Diamond");
                Core.EnsureComplete(7291);
                Bot.Wait.ForPickup("Rainbow Moonstone");
            }
            Core.EnsureComplete(7290);
            Bot.Wait.ForPickup("Floozer");
        }

        //Time Quartz
        if (!Story.QuestProgression(7292))
        {
            Core.EnsureAccept(7292);
            Core.GetMapItem(6910, 1, "Thespan");
            Bot.Wait.ForPickup("Time Quartz");
            Core.EnsureComplete(7292);
        }
    }
}