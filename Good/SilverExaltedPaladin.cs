//cs_include Scripts/CoreBots.cs

using RBot;

public class SEP
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        SilverExaltedPaladin();

        Core.SetOptions(false);
    }

    public void SilverExaltedPaladin()
    {
        Core.AddDrop("Silver Exalted Paladin");
        if(Core.CheckInventory("Silver Exalted Paladin"))
            return;
        if (!Bot.Quests.IsUnlocked(7581))
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
        if(!Bot.Quests.IsUnlocked(7582))
        {
            Core.EnsureAccept(7581);
            Core.KillMonster("ectocave", "r4", "Left", "Ichor Dracolich", "Sticky Paladin Helm");
            Core.EnsureCompleteChoose(7581);
        }
        if(!Bot.Quests.IsUnlocked(7583))
        {
            Core.EnsureAccept(7582);
            Core.KillMonster("frozenruins", "r11a", "Right", "*", "Paladin Helmet Wings");
            Core.EnsureCompleteChoose(7582);
        }
        if(!Bot.Quests.IsUnlocked(7584))
        {
            Core.EnsureAccept(7583);
            Core.HuntMonster("thirdspell", "Great Solar Elemental", "Wings Found");
            Core.EnsureComplete(7583);
        }
        if(!Bot.Quests.IsUnlocked(7585))
        {
            Core.EnsureAccept(7584);
            Core.KillMonster("table", "r6", "Left", "*", "Paladin Polearm Found");
            Core.EnsureCompleteChoose(7584);
        }
        if(!Bot.Quests.IsUnlocked(7586))
        {
            Core.EnsureAccept(7585);
            Core.KillMonster("dracocon", "r16", "Bottom", "Singer", "Paladin Weapon Found");
            Core.EnsureCompleteChoose(7585);
        }
        Core.EnsureAccept(7586);
        Core.KillMonster("warhorc", "r6", "Left", "*", "Paladin Armor Found");
        Core.EnsureComplete(7586);
    }
}