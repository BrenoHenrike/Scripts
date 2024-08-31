/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class CoreAwe
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void GetAweRelic(string Item, int LegendQuest, int FragmentAmount, int ShardAmount, string Map, string Monster)
    {
        string relicName = $"{Item} Relic";
        if (Core.CheckInventory(relicName))
            return;

        int questID;
        if (Bot.Flash.GetGameObject<int>("world.myAvatar.objData.intAQ") > 0)
        {
            Farm.BladeofAweREP(5, false);
            Farm.Experience(35);
            Core.BuyItem("tower", 53, 29403, shopItemID: 18569);
            questID = LegendQuest + 1;
        }
        else
        {
            Farm.BladeofAweREP(10, false);
            Farm.Experience(55);

            Core.BuyItem("museum", 1130, Core.IsMember ? 29402 : 29404, shopItemID: Core.IsMember ? 18580 : 18579);
            questID = Core.IsMember ? LegendQuest : LegendQuest + 2;
        }

        Core.EquipClass(ClassType.Solo);
        Core.AddDrop($"{Item} Shard", $"{Item} Fragment");
        Core.FarmingLogger($"{Item} Fragment", FragmentAmount);

        while (!Bot.ShouldExit && !Core.CheckInventory($"{Item} Fragment", FragmentAmount))
        {
            Core.EnsureAccept(questID);
            if (Map.ToLower() == "doomvault" || Map.ToLower() == "doomvaultb")
                Core.KillMonster(Map, Map.ToLower().EndsWith('b') ? "r26" : "r5", "Left", Monster, $"{Item} Shard", ShardAmount, false);
            else
                Core.HuntMonster(Map, Monster, $"{Item} Shard", ShardAmount, false);
            Core.EnsureComplete(questID);
            Bot.Wait.ForPickup($"{Item} Fragment");
        }

        Core.BuyItem("museum", 1129, $"{Item} Relic");
    }
}