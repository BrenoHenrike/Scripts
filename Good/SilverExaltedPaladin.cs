//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs

using Skua.Core.Interfaces;

public class SEP
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SilverExaltedPaladin();

        Core.SetOptions(false);
    }

    public void SilverExaltedPaladin()
    {
        if (Core.CheckInventory("Silver Exalted Paladin"))
            return;

        Story.PreLoad(this);

        Core.AddDrop("Silver Exalted Paladin");

        if (!Core.isCompletedBefore(7586))
        {
            if (!Story.QuestProgression(7580))
            {
                Core.EnsureAccept(7580);
                Core.KillMonster("dragonheart", "r3", "Left", "*", "Ancient Paladin Chest 1");
                Core.KillMonster("dragonheart", "r5", "Left", "*", "Ancient Paladin Chest 2");
                Core.KillMonster("dragonheart", "r7", "Left", "*", "Ancient Paladin Chest 3");
                Core.KillMonster("dragonheart", "r9", "Left", "*", "Ancient Paladin Chest 4");
                Core.KillMonster("dragonheart", "r10", "Left", "Tempest Dracolich", "Ancient Paladin Chest 5");
                Core.KillMonster("dragonheart", "r11", "Left", "Granite Dracolich", "Ancient Paladin Chest 6");
                Core.KillMonster("dragonheart", "r12", "Left", "Avatar of Desolich", "Ancient Paladin Chest 7");
                Core.EnsureComplete(7580);
            }
            Story.KillQuest(7581, "ectocave", "Ichor Dracolich", AutoCompleteQuest: false);
            Core.EnsureCompleteChoose(7581);
            Story.KillQuest(7582, "frozenruins", "Frostdeep Dweller", AutoCompleteQuest: false);
            Core.EnsureCompleteChoose(7582);
            Story.KillQuest(7583, "thirdspell", "Great Solar Elemental", GetReward: false);
            Story.KillQuest(7584, "table", "Roach", AutoCompleteQuest: false);
            Core.EnsureCompleteChoose(7584);
            Story.KillQuest(7585, "dracocon", "Singer", AutoCompleteQuest: false);
            Core.EnsureCompleteChoose(7585);
        }
        Core.EnsureAccept(7586);
        Core.HuntMonster("warhorc", "General Drox", "Paladin Armor Found");
        Core.EnsureComplete(7586);
    }
}