/*
name: Treasures of Aotearoa
description: This script will farm all the rewards of "Treasures of Aotearoa" quest.
tags: treasures,of,aotearoa,seasonal,summer,break,valencia,dandelion,kiwi,cookie
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class TreasuresOfAotearoa
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    int questID = 9279;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Rewards();

        Core.SetOptions(true);
    }

    public void Rewards()
    {
        Core.AddDrop(Core.QuestRewards(questID));

        foreach (string Item in Core.QuestRewards(questID)[4..9])
        {
            var rewards = Core.EnsureLoad(questID).Rewards;
            ItemBase? item = rewards.Find(x => x.Name == Item);

            Core.EnsureAccept(questID);

            Core.HuntMonster("burningbeach", "Water Goblin", "Stolen Egg", 5, log: false);

            Core.EnsureComplete(questID, item.ID);
        }

        Core.RegisterQuests(questID);
        while (!Bot.ShouldExit && !Core.CheckInventory(Core.QuestRewards(questID)[2..3]))
            Core.HuntMonster("burningbeach", "Water Goblin", "Stolen Egg", 5, log: false);
        Core.CancelRegisteredQuests();
    }
}