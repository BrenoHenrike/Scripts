/*
name: Legion Castle Quest
description: Farms the "Legion Castle Quest" untill all drops are obtained. Requires "Legion Castle". Will also farm Legion Tokens
tags: legion, castle, quest, token, loyal
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class LegionCastleQuest
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Example();
        Core.SetOptions(false);
    }

    public void Example()
    {
        var rewards = Core.QuestRewards(6822);

        if (Core.CheckInventory(rewards, toInv: false))
            return;
        if (!Core.CheckInventory("Legion Castle"))
        {
            Core.Logger("You do not own \"Legion Castle\". Stopping the bot");
            return;
        }

        Core.AddDrop(rewards);

        Core.RegisterQuests(6822, 6742, 6743);
        while (!Bot.ShouldExit && !Core.CheckInventory(rewards))
        {
            Core.KillMonster("legionarena", "r2", "Left", "*", "Challenger Slain", 12, publicRoom: true);
            Core.KillMonster("legionarena", "Boss", "Left", "Legion Fiend Rider", "Legion Fiend Rider Slain", 1, publicRoom: true);
        }
        Core.CancelRegisteredQuests();
    }
}
