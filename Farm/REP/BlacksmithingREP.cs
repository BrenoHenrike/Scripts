/*
name: Blacksmithing REP
description: This script will farm Blacksmithing reputation to rank 10.
tags: blacksmith, rep, reputation, rank, farm
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class BlacksmithingREP
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "BlackSmithRepGold";
    public List<IOption> Options = new()
    {
        new Option<bool>("UseGold", "Use Gold To Get Rep?", "Will Farm the Quest \"Intrepid Investing\" which costs 500k/ turnin, if you dont have the gold the bot will farm it.", false),
        new Option<bool>("BulkFarmGold", "Pre-Farm Gold(100m)", "Bulk Turnin after farming 100m Gold.", false),
        CoreBots.Instance.SkipOptions,
    };


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        //Adv.BestGear(GenericGearBoost.dmgAll);
        //Adv.BestGear(GenericGearBoost.rep);
        Farm.BlacksmithingREP(10, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));

        Core.SetOptions(false);
    }
}
