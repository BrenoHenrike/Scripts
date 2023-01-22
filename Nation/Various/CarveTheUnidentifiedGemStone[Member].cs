/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class CarveTheUnidentifiedGemStone
{

    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();


    public string OptionsStorage = "CarveTheUnidentifiedGemStone";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<ChooseReward>("ChooseReward", "Choose Your Reward", "Pick A Reward", ChooseReward.All),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        
DoCarveTheUnidentifiedGemStone();

        Core.SetOptions(false);
    }

    public void DoCarveTheUnidentifiedGemStone()
    {
        if (Bot.Config.Get<ChooseReward>("ChooseReward") == ChooseReward.All)
            Nation.CarveUniGemStone();
        else Nation.CarveUniGemStone(Bot.Config.Get<ChooseReward>("ChooseReward").ToString());
        
    }

    private enum ChooseReward
    {
        Dark_Crystal_Shard,
        Diamond_of_Nulgath,
        Gem_of_Nulgath,
        Blood_Gem_of_the_Archfiend,
        All,
    }
}
