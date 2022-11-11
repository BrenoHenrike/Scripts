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

        if (Bot.Config.Get<Armors>("Armors") == Armors.Dark_FrostSpawn_Mage && !Core.CheckInventory("Dark FrostSpawn Mage"))
        {
            Core.HuntMonster("northstar", "Karok the Fallen", "Dark FrostSpawn Mage", isTemp: false, publicRoom: true, log: false);
            Bot.Wait.ForPickup("Dark FrostSpawn Mage");
        }

        // if (Bot.Config.Get<Armors>("Armors") == Armors.Insert && !Core.CheckInventory("Insert"))
        // {
        //     Core.HuntMonster("Map", "Mob", "Item", isTemp: false, publicRoom: true);
        //     Bot.Wait.ForPickup("Item");
        // }

        // case "item":
        // Code goes here
        // break;

    }
}

public enum Armors
{
    Dark_FrostSpawn_Mage,
    None
}
