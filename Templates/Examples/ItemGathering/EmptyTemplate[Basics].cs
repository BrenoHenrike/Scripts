/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Quests;

public class DefaultTemplate
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    private CoreAdvanced Adv = new();
    private CoreFarms Farm = new();
    private CoreStory Story = new();
    private CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "item1", "Item2", "Etc" });
        Core.SetOptions();

        Example();

        Core.SetOptions(false);
    }

    public void Example(bool TestMode = false)
    {

        //Test Push
        if (TestMode)
        {
            if (Core.CheckInventory("item", 1))
                return;

            #region General Item Farming with quest included
            Core.RegisterQuests(000);
            while (!Bot.ShouldExit && Core.CheckInventory("item", 1))
            {
                Core.HuntMonster("map", "mob", "item", 1, isTemp: false, log: false);
                Core.HuntMonsterMapID("map", 1, "item", 1, isTemp: false, log: false);
                Core.KillMonster("map", "cell", "pad", "mob", "item", 1, isTemp: false, log: false);
            }
            Core.CancelRegisteredQuests();
            #endregion

            #region Story Mode:
            Story.KillQuest(000, "mapname", "MonsterName");
            Story.KillQuest(000, "mapname", new[] { "Monstername", "Monstername" });
            Story.MapItemQuest(000, "mapname", 1, 1);
            Story.MapItemQuest(000, "mapname", new[] { 000, 000, 000, });
            Story.ChainQuest(000);
            #endregion
        }
    }
}



