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
            Bot.Wait.ForPickup("Ada's Overcharged Scythe");
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Amethyst_Pickaxe || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory("Amethyst Pickaxe"))
        {
            Core.HuntMonster("djinn", "Tibicenas", "Amethyst Pickaxe", isTemp: false);
            Bot.Wait.ForPickup("Amethyst Pickaxe");
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Ancient_Frying_Pan || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory("Ancient Frying Pan"))
        {
            Core.HuntMonster("timevoid", "Unending Avatar", "Ancient Frying Pan", isTemp: false);
            Bot.Wait.ForPickup("Ancient Frying Pan");
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Blood_of_the_Void_Daggers || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory("Blood of the Void Daggers"))
        {
            Core.HuntMonster("voidbattle", "Jir'abin Challenge", "Blood of the Void Daggers", isTemp: false);
            Bot.Wait.ForPickup("Blood of the Void Daggers");
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Blood_of_the_Void_Blade || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory("Blood of the Void Blade"))
        {
            Core.HuntMonster("voidbattle", "Jir'abin Challenge", "Blood of the Void Blade", isTemp: false);
            Bot.Wait.ForPickup("Blood of the Void Blade");
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Burning_Blade || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory(31058))
        {
            Core.HuntMonster("lostruinswar", "Diabolical Warlord", "Burning Blade");
            Bot.Wait.ForPickup("Burning Blade");
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Deaths_Bright_Blade || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory("Death's Bright Blade"))
        {
            Core.HuntMonster("tercessuinotlim", "Death's Head", "Death's Bright Blade", isTemp: false);
            Bot.Wait.ForPickup("Death's Bright Blade");
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Deaths_Scythe || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory(25117))
        {
            Core.HuntMonster("shadowattack", "Death", "Death's Scythe", isTemp: false);
            Bot.Wait.ForPickup("Death's Scythe");
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Bone_Claws_ofTurmoil || Bot.Config.Get<Weapons>("Weapons") == Weapons.All  && !Core.CheckInventory("Bone Claws of Turmoil"))
        {
            Core.HuntMonster("cloister", "Acornment", "Bone Claws of Turmoil", isTemp: false);
            Bot.Wait.ForPickup("Bone Claws of Turmoil");
        }

        if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Diamonds_Of_Time || Bot.Config.Get<Weapons>("Weapons") == Weapons.All && !Core.CheckInventory("Diamonds Of Time"))
        {
            Core.HuntMonster("cloister", "Acornment", "Diamonds Of Time", isTemp: false);
            Bot.Wait.ForPickup("Diamonds Of Time");
        }

        // if (Bot.Config.Get<Weapons>("Weapons") == Weapons.Insert && !Core.CheckInventory("Insert"))
        // {
        //     Core.HuntMonster("Map", "Mob", "Item", isTemp: false);
        //     Bot.Wait.ForPickup("Item");
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
    Bone_Claws_ofTurmoil,
    All,
    None
}
