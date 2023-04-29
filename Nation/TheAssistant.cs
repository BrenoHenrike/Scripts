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
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<SwindlesReturnReward>("ChooseReward", "Choose Your Quest Reward", "if `returnPolicyDuringSupplies` is enabled in CoreBot Options, Choose the Reward here", (int)SwindlesReturnReward.None),
    };


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Nation.TheAssistant(Reward: bot.Config.Get<SwindlesReturnReward>("ChooseReward"));

        Core.SetOptions(false);
    }
}
