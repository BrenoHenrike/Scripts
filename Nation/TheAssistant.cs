/*
name: TheAssistant
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class TheAssistant
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public string OptionsStorage = "TheAssistant";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<SwindlesReturnReward>("ChooseReward", "Choose Your Quest Reward", "if `returnPolicyDuringSupplies` is enabled in CoreBot Options, Choose the Reward here", (int)SwindlesReturnReward.None),
    };


    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Nation.TheAssistant(Reward: Bot.Config?.Get<SwindlesReturnReward>("ChooseReward") ?? default);

        Core.SetOptions(false);
    }
}
