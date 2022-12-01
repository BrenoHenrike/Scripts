//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class LowDRWeapons
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();


    public string OptionsStorage = "1%Weapons";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<Weapons>("Weapons", "Choose Your Weapons", "Extra Weapons can be added as long as they are 1% or lower drop chance.", Weapons.None),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetItem();

        Core.SetOptions(false);
    }

    public void GetItem(string item = null)
    {
        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.None)
        {
            Core.Logger($"\"None\" Selected, Stopping.");
            return;
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Adas_Overcharged_Scythe || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory("Ada's Overcharged Scythe"))
        {
            Core.HuntMonster("laken", "Ada", "Ada's Overcharged Scythe", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Amethyst_Pickaxe || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory("Amethyst Pickaxe"))
        {
            Core.HuntMonster("djinn", "Tibicenas", "Amethyst Pickaxe", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Ancient_Frying_Pan || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory("Ancient Frying Pan"))
        {
            Core.HuntMonster("timevoid", "Unending Avatar", "Ancient Frying Pan", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Blood_of_the_Void_Daggers || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory("Blood of the Void Daggers"))
        {
            Core.HuntMonster("voidbattle", "Jir'abin Challenge", "Blood of the Void Daggers", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Blood_of_the_Void_Blade || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory("Blood of the Void Blade"))
        {
            Core.HuntMonster("voidbattle", "Jir'abin Challenge", "Blood of the Void Blade", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Purified_Void_Blade || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory("Purified Void Blade"))
        {
            Core.HuntMonster("voidbattle", "Jir'abin Challenge", "Purified Void Blade", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Purified_Void_Daggers || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory("Purified Void Daggers"))
        {
            Core.HuntMonster("voidbattle", "Jir'abin Challenge", "Purified Void Daggers", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Burning_Blade || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory(31058))
        {
            Core.HuntMonster("lostruinswar", "Diabolical Warlord", "Burning Blade");
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Deaths_Bright_Blade || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory("Death's Bright Blade"))
        {
            Core.HuntMonster("tercessuinotlim", "Death's Head", "Death's Bright Blade", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Deaths_Scythe || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory(25117))
        {
            Core.HuntMonster("shadowattack", "Death", "Death's Scythe", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Bone_Claws_of_Turmoil || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory("Bone Claws of Turmoil"))
        {
            Core.HuntMonster("cloister", "Acornment", "Bone Claws of Turmoil", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Diamonds_Of_Time || Bot.Config.Get<Weapons>("Weapons") == Weapons.All && !Core.CheckInventory("Diamonds Of Time"))
        {
            Core.HuntMonster("cloister", "Acornment", "Diamonds Of Time", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Underworldly_Dark_Wand || Bot.Config.Get<Weapons>("Weapons") == Weapons.All && !Core.CheckInventory("Underworldly Dark Wand"))
        {
            Core.HuntMonster("legionarena", "Exalted Legion Champion", "Underworldly Dark Wand", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Legion_Chain_Whip || Bot.Config.Get<Weapons>("Weapons") == Weapons.All && !Core.CheckInventory("Legion Chain Whip"))
        {
            Core.HuntMonster("legionarena", "Exalted Legion Champion", "Legion Chain Whip", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Blood_Scythe_Of_Destruction || Bot.Config.Get<Weapons>("Weapons") == Weapons.All && !Core.CheckInventory("Blood Scythe Of Destruction"))
        {
            Core.HuntMonster("infernalspire", "Helzekiel", "Blood Scythe Of Destruction", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Sanctified_Guardian_Blade || Bot.Config.Get<Weapons>("Weapons") == Weapons.All && !Core.CheckInventory("Sanctified Guardian Blade"))
        {
            Core.HuntMonster("aqlesson", "Carnax", "Sanctified Guardian Blade", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Feral_Blade_of_Doom || Bot.Config.Get<Weapons>("Weapons") == Weapons.All && !Core.CheckInventory("Feral Blade of Doom"))
        {
            Core.HuntMonster("stonewooddeep", "Sir Kut", "Feral Blade of Doom", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Feral_DoomBlade || Bot.Config.Get<Weapons>("Weapons") == Weapons.All && !Core.CheckInventory("Feral DoomBlade"))
        {
            Core.HuntMonster("stonewooddeep", "Sir Kut", "Feral DoomBlade", isTemp: false);
        }
        
        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Ruby_Pickaxe || Bot.Config.Get<Weapons>("Weapons") == Weapons.All && !Core.CheckInventory("Ruby Pickaxe"))
        {
            Core.HuntMonster("banished", "Desterrat Moya", "Ruby Pickaxe", isTemp: false);
        }
        
        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Duel_Swords_of_Vindication || Bot.Config.Get<Weapons>("Weapons") == Weapons.All && !Core.CheckInventory("Duel Swords of Vindication"))
        {
            Core.HuntMonster("xancave", "Shurpu Ring Guardian", "Duel Swords of Vindication", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Apocryphal_Blade_Of_The_Truth || Bot.Config.Get<Weapons>("Weapons") == Weapons.All && !Core.CheckInventory("Apocryphal Blade Of The Truth"))
        {
            Core.HuntMonster("banished", "Desterrat Moya", "Apocryphal Blade Of The Truth", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Evolved_Agony_Chain || Bot.Config.Get<Weapons>("Weapons") == Weapons.All && !Core.CheckInventory("Evolved Agony Chain"))
        {
            Core.HuntMonster("lust", "Lascivia", "Evolved Agony Chain", isTemp: false);
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.TOO_Big_100K || Bot.Config.Get<Weapons>("Weapons") == Weapons.All && !Core.CheckInventory("TOO Big 100K"))
        {
            Core.HuntMonster("lair", "Red Dragon", "TOO Big 100K", isTemp: false);
        }


        // if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Insert || Bot.Config.Get<Weapons>("Weapons") == Weapons.All && !Core.CheckInventory("Insert"))
        // {
        //     Core.HuntMonster("Map", "Mob", "Item", isTemp: false);
        // }

    }
}

public enum Weapons
{
    Adas_Overcharged_Scythe,
    Amethyst_Pickaxe,
    Ancient_Frying_Pan,
    Blood_of_the_Void_Blade,
    Blood_of_the_Void_Daggers,
    Burning_Blade,
    Deaths_Bright_Blade,
    Deaths_Scythe,
    Diamonds_Of_Time,
    Bone_Claws_of_Turmoil,
    Underworldly_Dark_Wand,
    Legion_Chain_Whip,
    Blood_Scythe_Of_Destruction,
    Sanctified_Guardian_Blade,
    Feral_Blade_of_Doom,
    Feral_DoomBlade,
    Ruby_Pickaxe,
    Duel_Swords_of_Vindication,
    Apocryphal_Blade_Of_The_Truth,
    Evolved_Agony_Chain,
    Purified_Void_Blade,
    Purified_Void_Daggers,
    TOO_Big_100K,
    All,
    None
}
