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

public class SwindlesReturnPolicy
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();
    

    public string OptionsStorage = "SwindlesReturnPolicy";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<SwindleReturnItems>("ItemChoice", "Item Choice", "Choose your Desired Item from the list.", SwindleReturnItems.Tainted_Gem),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        
        DoSwindlesReturnPolicy();

        Core.SetOptions(false);
    }

    public void DoSwindlesReturnPolicy()
    {
        if (Bot.Config.Get<SwindleReturnItems>("ItemChoice") != SwindleReturnItems.All)
            Nation.SwindleReturn(Bot.Config.Get<SwindleReturnItems>("ItemChoice").ToString());
        else
        {
            Nation.SwindleReturn("Dark Crystal Shard");
            Nation.SwindleReturn("Diamond of Nulgath");
            Nation.SwindleReturn("Gem of Nulgath");
            Nation.SwindleReturn("Blood Gem of the Archfiend");
            Nation.SwindleReturn("Tainted Gem");
        }        
    }

    private enum SwindleReturnItems
    {
        Dark_Crystal_Shard = 4770,
        Diamond_of_Nulgath = 4771,
        Gem_of_Nulgath = 6136,
        Blood_Gem_of_the_Archfiend = 22332,
        Tainted_Gem = 4769,
        All
    };
}
