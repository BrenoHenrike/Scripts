/*
name: Elders Blood (Army)
description: One-client version of the Elders' Blood daily
tags: daily, dailies, army, elders, blood, vhl, void highlord, oneclient
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmyEldersBlood
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private CoreDailies Dailies = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "ArmyEldersBlood";
    public List<IOption> Options = new()
    {
        new Option<bool>("randomServers", "Random Servers", "Should the bot use a random server each for each account.", true),
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        EldersBlood(Bot.Config!.Get<bool>("randomServers"));

        Core.SetOptions(false);
    }

    public void EldersBlood(bool randomServers)
    {
        while (!Bot.ShouldExit && Army.doForAll(randomServers))
            Dailies.EldersBlood();
    }
}
