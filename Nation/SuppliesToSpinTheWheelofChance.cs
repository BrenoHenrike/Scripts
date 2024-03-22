/*
name: SuppliesToSpinTheWheelofChance
description: Does "Supplies to Spin the Wheel" [*or* swindles bilk quests if u have it avaible.]
tags: swindles return policy, supplies to spin the wheel, swindles bilk, the assistant, nulgath, nation, supplies, Ultra Alteon, escherion

*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class SuppliesToSpinTheWheelofChance
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public string OptionsStorage = "SuppliesOptions";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<bool>("AssistantDuring", "Do: \"The Assistant\" during?", "Do the quest: [The Assistant], (requires alota gold, that you will get from the vouchers of nulgath (mem)) during this.", false),
        new Option<bool>("UltraAlteon", "Kill \"UltraAlteon\"", "Instead of \"Escherion\" or bamboozle, do \"Ultra Alteon\"?", false),
        CoreBots.Instance.SkipOptions,
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.SuppliesRewards);
        Core.SetOptions();

        Nation.Supplies(UltraAlteon: bot.Config!.Get<bool>("UltraAlteon"), AssistantDuring: bot.Config!.Get<bool>("AssistantDuring"));

        Core.SetOptions(false);
    }
}
