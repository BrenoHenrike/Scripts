/*
name: LowDRHelmets
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class LowDRHelmets
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();


    public string OptionsStorage = "1%Helmets";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<Helmets>("Helmets", "Choose Your Helmets", "Extra Helmets can be added as long as they are 1% or lower drop chance.", Helmets.None),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetItem();

        Core.SetOptions(false);
    }

    public void GetItem()
    {
        Helmets? helmetConfig = Bot.Config?.Get<Helmets>("Helmets");

        if (helmetConfig == null || helmetConfig == Helmets.None)
        {
            Core.Logger($"\"None\" Selected, Stopping.");
            return;
        }

        Core.FarmingLogger($"{helmetConfig.ToString()}", 1);

        if (helmetConfig == Helmets.Sekts_Mummys_Hat || helmetConfig == Helmets.All && !Core.CheckInventory("Sekt's Mummy's Hat"))
        {
            Core.HuntMonster("fourdpyramid", "Sekt's Mummy", "Sekt's Mummy's Hat", isTemp: false);
        }

        if (helmetConfig == Helmets.Dracolich_Destroyer_Mask || helmetConfig == Helmets.All && !Core.CheckInventory("Dracolich Destroyer Mask"))
        {
            Core.HuntMonster("dragonheart", "Avatar of Desolich", "Dracolich Destroyer Mask", isTemp: false);
        }

        if (helmetConfig == Helmets.Asherion_Helmet || helmetConfig == Helmets.All && !Core.CheckInventory("Asherion Helmet"))
        {
            Core.HuntMonster("stonewooddeep", "Sir Kut", "Asherion Helmet", isTemp: false);
        }

        if (helmetConfig == Helmets.Drakath_Mask || helmetConfig == Helmets.All && !Core.CheckInventory("Drakath Mask"))
        {
            Core.HuntMonster("finalbattle", "drakath", "Drakath Mask", isTemp: false);
        }

        if (helmetConfig == Helmets.Monster_Queens_Locks || helmetConfig == Helmets.All && !Core.CheckInventory("Monster Queen's Locks"))
        {
            Core.HuntMonster("deadlines", "Eternal Dragon", "Monster Queen's Locks", isTemp: false);
        }

        if (helmetConfig == Helmets.Monster_Queens_Malicious_Morph || helmetConfig == Helmets.All && !Core.CheckInventory("Monster Queen's Malicious Morph"))
        {
            Core.HuntMonster("deadlines", "Eternal Dragon", "Monster Queen's Malicious Morph", isTemp: false);
        }

        if (helmetConfig == Helmets.Reapers_Morph || helmetConfig == Helmets.All && !Core.CheckInventory("Reaper's Morph"))
        {
            Core.HuntMonster("thevoid", "Reaper", "Reaper's Morph", isTemp: false);
        }

        if (helmetConfig == Helmets.Reapers_Scream || helmetConfig == Helmets.All && !Core.CheckInventory("Reaper's Scream"))
        {
            Core.HuntMonster("thevoid", "Reaper", "Reaper's Scream", isTemp: false);
        }

        if (helmetConfig == Helmets.Reapers_Visage || helmetConfig == Helmets.All && !Core.CheckInventory("Reaper's Visage"))
        {
            Core.HuntMonster("thevoid", "Reaper", "Reaper's Visage", isTemp: false);
        }

        // if (Bot.Config.Get<Helmets>("Helmets") == Helmets.Insert || Bot.Config.Get<Helmets>("Helmets") == Helmets.All && !Core.CheckInventory("Insert"))
        // {
        //     Core.HuntMonster("Map", "Mob", "Item", isTemp: false);
        // }

    }
}

public enum Helmets
{
    Sekts_Mummys_Hat,
    Dracolich_Destroyer_Mask,
    Asherion_Helmet,
    Drakath_Mask,
    Monster_Queens_Locks,
    Monster_Queens_Malicious_Morph,
    Reapers_Morph,
    Reapers_Scream,
    Reapers_Visage,
    All,
    None
}
