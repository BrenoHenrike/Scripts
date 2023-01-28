/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class LowDRArmors
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public string OptionsStorage = "1%Armors";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<Armors>("Armors", "Choose Your Armors", "Extra Armors can be added as long as they are 1% or lower drop chance.", Armors.None),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetItem();

        Core.SetOptions(false);
    }

    public void GetItem()
    {
        if (Bot.Config.Get<Armors>("Armors") == Armors.None)
        {
            Core.Logger($"\"None\" Selected, Stopping.");
            return;
        }

        Core.FarmingLogger($"{Bot.Config.Get<Armors>("Armors").ToString()}", 1);

        if (Bot.Config.Get<Armors>("Armors") == Armors.Dark_FrostSpawn_Mage || Bot.Config.Get<Armors>("Armors") == Armors.All && !Core.CheckInventory("Dark FrostSpawn Mage"))
        {
            Core.HuntMonster("northstar", "Karok the Fallen", "Dark FrostSpawn Mage", isTemp: false);
        }

        if (Bot.Config.Get<Armors>("Armors") == Armors.Feral_DoomKnight || Bot.Config.Get<Armors>("Armors") == Armors.All && !Core.CheckInventory("Feral DoomKnight"))
        {
            Core.HuntMonster("stonewooddeep", "Sir Kut", "Feral DoomKnight", isTemp: false);
        }

        if (Bot.Config.Get<Armors>("Armors") == Armors.Alteons_Royal_Armor || Bot.Config.Get<Armors>("Armors") == Armors.All && !Core.CheckInventory("Alteon's Royal Armor"))
        {
            Core.HuntMonster("swordhavenfalls", "Chaos Lord Alteon", "Alteon's Royal Armor", isTemp: false);
        }

        if (Bot.Config.Get<Armors>("Armors") == Armors.Dracolich_Destroyer || Bot.Config.Get<Armors>("Armors") == Armors.All && !Core.CheckInventory("Dracolich Destroyer"))
        {
            Core.HuntMonster("dragonheart", "Avatar of Desolich", "Dracolich Destroyer", isTemp: false);
        }

        if (Bot.Config.Get<Armors>("Armors") == Armors.Asherion_Armor || Bot.Config.Get<Armors>("Armors") == Armors.All && !Core.CheckInventory("Asherion Armor"))
        {
            Core.HuntMonster("stonewooddeep", "Sir Kut", "Asherion Armor", isTemp: false);
        }

        if (Bot.Config.Get<Armors>("Armors") == Armors.Lore_Golem || Bot.Config.Get<Armors>("Armors") == Armors.All && !Core.CheckInventory("Lore Golem"))
        {
            Core.HuntMonster("twistedcavern", "Lore Golem", "Lore Golem", isTemp: false);
        }

        // if (Bot.Config.Get<Armors>("Armors") == Armors.Insert || Bot.Config.Get<Armors>("Armors") == Armors.All && !Core.CheckInventory("Insert"))
        // {
        //     Core.HuntMonster("Map", "Mob", "Item", isTemp: false);
        // }

        // case "item":
        // Code goes here
        // break;

    }
}

public enum Armors
{
    Dark_FrostSpawn_Mage,
    Feral_DoomKnight,
    Alteons_Royal_Armor,
    Dracolich_Destroyer,
    Asherion_Armor,
    Lore_Golem,
    All,
    None
}
