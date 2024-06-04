/*
name: Hanzo Orb Quests
description: Does the quests from either the Astral Orb Pet, or the Crimson Orb Pet
tags: astral, crimson, orb, pet, quests
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class HanzoOrbQuest
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        HanzoOrb();
    }


    public void HanzoOrb(string Reward = "Any", int quant = 1, bool AnyReward = false)
    {
        int questID = 0;
        if (Core.CheckInventory("Astral Orb Pet"))
        {
            Core.Logger("Using \"Astral Orb Pet Quest\"");
            questID = 4020;
        }
        else if (Core.CheckInventory("Crimson Orb Pet"))
        {
            Core.Logger("Using \"Crimson Orb Pet Quest\"");
            questID = 4019;
        }
        else
        {
            Core.Logger("Neither orb owned, stopping");
            return;
        }

        var rewards = Core.QuestRewards(questID);
        Core.AddDrop(rewards);

        Core.RegisterQuests(questID);
        if (AnyReward)
        {
            foreach (string item in rewards)
            {
                while (!Bot.ShouldExit && (!Core.CheckInventory(item) || !Bot.Inventory.IsMaxStack(item)))
                {
                    Core.HuntMonster("graveyard", "Big Jack Sprat", "Jacked Eye", 5, log: false);
                    Core.HuntMonster("marsh", "Dreadspider", "Dreadspider Silk", log: false);
                    Core.KillMonster("tercessuinotlim", "m2", "Left", "*", "Makai Fang", 5, log: false);
                    Core.HuntMonster("bludrut", "Rattlebones", "Rattle Bones", 3, log: false);
                }
            }
        }
        else
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(Reward, quant))
            {
                Core.HuntMonster("graveyard", "Big Jack Sprat", "Jacked Eye", 5, log: false);
                Core.HuntMonster("marsh", "Dreadspider", "Dreadspider Silk", log: false);
                Core.KillMonster("tercessuinotlim", "m2", "Left", "*", "Makai Fang", 5, log: false);
                Core.HuntMonster("bludrut", "Rattlebones", "Rattle Bones", 3, log: false);
            }
        }
        Core.CancelRegisteredQuests();
    }
}
