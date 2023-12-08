/*
name: Tinsel's Gifts
description: This will finish all of Tinsel's quest and obtain all of the quest rewards.
tags: tinsels-gifts, seasonal, frostvale
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class TinselGifts
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        TinselWeapon();
        TinselHelm();
        TinselArmor();
        TinselCape();
    }

    public void TinselWeapon() => ProcessSeasonalMap("frostdeep", 914, "Ancient Maggot", "Tinsel's Sword Bow");
    public void TinselHelm() => ProcessSeasonalMap("icevolcano", 915, "Ice Symbiote", "Tinsel's Helm Bow");
    public void TinselArmor() => ProcessSeasonalMap("goldenruins", 1517, "Golden Warrior", "Tinsel's Armor Bow");
    public void TinselCape() => ProcessSeasonalMap("icerise", 2554, "Arctic Direwolf", "Tinsel's Cape Bow");

    private void ProcessSeasonalMap(string mapName, int questId, string monsterName, string itemBow)
    {
        if (!Core.isSeasonalMapActive(mapName))
            return;

        string[] rewards = Core.QuestRewards(questId);

        if (Core.CheckInventory(rewards))
            return;

        Core.AddDrop(rewards);

        while (!Bot.ShouldExit && !(Core.CheckInventory(rewards)))
        {
            Core.EnsureAccept(questId);
            Core.HuntMonster(mapName, monsterName, itemBow, log: false);
            Core.EnsureCompleteChoose(questId);
        }
        Core.ToBank(rewards);
    }

}
