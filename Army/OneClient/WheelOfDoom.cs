/*
name: Army All Dailies
description: One-client version of All Dailies
tags: alldailies, army, thefamily
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmyWheelofDoom
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private CoreDailies Dailies = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "ArmyWheelOfDoom";
    public List<IOption> Options = new()
    {
        new Option<bool>("randomServers", "Random Servers", "Should the bot use a random server each for each account.", true),
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        WheelOfDoom(Bot.Config.Get<bool>("randomServers"));

        Core.SetOptions(false);
    }

    public void WheelOfDoom(bool randomServers)
    {
        while (Army.doForAll(randomServers))
            Dailies.WheelofDoom();
    }
}
