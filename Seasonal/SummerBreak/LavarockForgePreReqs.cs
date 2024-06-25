/*
name: LavarockForge
description: This script will farm the pre reqs for the Lavarock Forge Set.
tags: burning sword of doom, blazing light of destiny, blaze of awe, pre reqs, lavarock forge, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Farm/BuyScrolls.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;


public class LavarockForge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private BuyScrolls Scroll = new();

    public string OptionsStorage = "LavarockForge";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("FarmAwe", "Farm Blaze of Awe Pre Reqs?","true/false" ,false),
        new Option<bool>("FarmBLOD", "Farm Blazing Light of Destiny Pre Reqs?","true/false", false),
        new Option<bool>("FarmBSOD", "Farm Burning Sword of Doom Pre Reqs?", "true/false", false),
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        PreReqs(Bot.Config!.Get<bool>("FarmAwe"), Bot.Config!.Get<bool>("FarmBLOD"), Bot.Config!.Get<bool>("FarmBSOD"));
        Core.SetOptions(false);
    }

    public void PreReqs(bool FarmAwe = false, bool FarmBLOD = false, bool FarmBSOD = false)
    {
        if (FarmAwe)
        {
            Core.Logger("Farming Blaze of Awe Pre Reqs.");
            Scroll.BuyScroll(Scrolls.ScorchedSteel, 99);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("shadowstrike", "Sepulchuroth", "Akriloth's Scale", 500, false);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("battleundere", "Lava Guard", "Molten Core", 50, isTemp: false);
        }

        if (FarmBLOD)
        {
            Core.Logger("Farming Blazing Light of Destiny Pre Reqs.");
            Scroll.BuyScroll(Scrolls.ScorchedSteel, 99);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("shadowstrike", "Sepulchuroth", "Akriloth's Scale", 1000, false);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("battleundere", "Lava Guard", "Molten Core", 100, false);
        }

        if (FarmBSOD)
        {
            Core.Logger("Farming Burning Sword of Doom Pre Reqs.");
            Scroll.BuyScroll(Scrolls.ScorchedSteel, 99);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("shadowstrike", "Sepulchuroth", "Akriloth's Scale", 1500, false);
            Core.HuntMonster("ashfallcamp", "Smoldur", "Flame Heart", isTemp: false);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("battleundere", "Lava Guard", "Molten Core", 150, false);
        }
    }
}
