/*
name: 1% Drop Rate Capes
description: Farms all 1% drop rate capes.
tags: 1%, cape, drop-rate
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class LowDRCapes
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();


    public string OptionsStorage = "1%Capes";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<Capes>("Capes", "Choose Your Capes", "Extra Capes can be added as long as they are 1% or lower drop chance.", Capes.None),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetItem();

        Core.SetOptions(false);
    }

    public void GetItem()
    {
        Capes? capeConfig = Bot.Config?.Get<Capes>("Capes");

        if (capeConfig == null || capeConfig == Capes.None)
        {
            Core.Logger($"\"None\" Selected, Stopping.");
            return;
        }

        Core.FarmingLogger($"{capeConfig.ToString()}", 1);

        if (capeConfig == Capes.Infinity_Shield_Cape || capeConfig == Capes.All && !Core.CheckInventory("Infinity Shield Cape"))
        {
            Core.HuntMonster("whitehole", "Mehensi Serpent", "Infinity Shield Cape", isTemp: false);
        }

        if (capeConfig == Capes.Drakath_Wings || capeConfig == Capes.All && !Core.CheckInventory("Drakath Wings"))
        {
            Core.HuntMonster("finalbattle", "Drakath", "Drakath Wings", isTemp: false);
        }

        if (capeConfig == Capes.Chaotic_Champion_Wings || capeConfig == Capes.All && !Core.CheckInventory("Chaotic Champion Wings"))
        {
            Core.HuntMonster("finalbattle", "Drakath", "Chaotic Champion Wings", isTemp: false);
        }

        if (capeConfig == Capes.Wings_Of_Destruction || capeConfig == Capes.All && !Core.CheckInventory("Wings Of Destruction"))
        {
            Core.HuntMonster("infernalspire", "Malxas", "Wings Of Destruction", isTemp: false);
        }

        if (capeConfig == Capes.ShadowScythe_Warlocks_Demonic_Flames || capeConfig == Capes.All && !Core.CheckInventory("ShadowScythe Warlock's Demonic Flames"))
        {
            Core.HuntMonster("innershadows", "Krahen", "ShadowScythe Warlock's Demonic Flames", isTemp: false);
        }

        if (capeConfig == Capes.ShadowScythe_Warlocks_Flames || capeConfig == Capes.All && !Core.CheckInventory("ShadowScythe Warlock's Flames"))
        {
            Core.HuntMonster("innershadows", "Krahen", "ShadowScythe Warlock's Flames", isTemp: false);
        }

        if (capeConfig == Capes.Skull_Pauldrons_of_Vordred || capeConfig == Capes.All && !Core.CheckInventory("Skull Pauldrons of Vordred"))
        {
            Core.HuntMonster("vordredboss", "Vordred", "Skull Pauldrons of Vordred", isTemp: false);
        }

        // if (Bot.Config.Get<Capes>("Capes") == Capes.Insert || Bot.Config.Get<Capes>("Capes") == Capes.All && !Core.CheckInventory("Insert"))
        // {
        //     Core.HuntMonster("Map", "Mob", "Item", isTemp: false);
        // }

    }
}

public enum Capes
{
    Infinity_Shield_Cape,
    Drakath_Wings,
    Wings_Of_Destruction,
    Chaotic_Champion_Wings,
    ShadowScythe_Warlocks_Demonic_Flames,
    ShadowScythe_Warlocks_Flames,
    Skull_Pauldrons_of_Vordred,
    All,
    None
}
