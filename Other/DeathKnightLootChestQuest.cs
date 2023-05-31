/*
name: DeathknightLootChestQuest
description: This script farms all the rewards from deathknight loot chest quest
tags: deathknight, loot, chest, quest, fallen, deathknight, accoutrements
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/AranxQuests.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class DeathKnightLootChest
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    private CoreStory Story = new();
    private AranxQuests AR = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DeathKnightLoot();
        Core.SetOptions(false);
    }

    public void DeathKnightLoot()
    {
    
        string[] rewards = {
            "Fallen DeathKnight",
            "Fallen DeathKnight Helm",
            "Fallen DeathKnight Wings",
            "Fallen DeathKnight Helm + Scarf",
            "Fallen DeathKnight Accoutrements",
        };

        if (Core.CheckInventory(rewards))
        {
            AR.StoryLine();
            Core.Logger("You already have all the rewards.");
            return;
        }

        Core.AddDrop(rewards);

        Core.EquipClass(ClassType.Solo);

        Bot.Quests.UpdateQuest(6509);

        foreach (string Reward in rewards)
        {
            if (Core.CheckInventory(Reward))
            {
                Core.ToBank(Reward);
                continue;
            }
            Core.FarmingLogger(Reward, 1);
            Core.EnsureAccept(6509);
            Core.HuntMonster("ivoliss", "ivoliss", "Loot Key", 6, true, false);
            Core.EnsureComplete(6509);
            Core.ToBank(Reward);
        }      
    }
}
