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
    public List<IOption> Options = new List<IOption>()
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
        if (Bot.Config.Get<Helmets>("Helmets") == Helmets.None)
        {
            Core.Logger($"\"None\" Selected, Stopping.");
            return;
        }
        
        Core.FarmingLogger($"{Bot.Config.Get<Helmets>("Helmets").ToString()}", 1);

        if (Bot.Config.Get<Helmets>("Helmets") == Helmets.Sekts_Mummys_Hat || Bot.Config.Get<Helmets>("Helmets") == Helmets.All && !Core.CheckInventory("Sekt's Mummy's Hat"))
        {
            Core.HuntMonster("fourdpyramid", "Sekt's Mummy", "Sekt's Mummy's Hat", isTemp: false);
            Bot.Wait.ForPickup("Sekt's Mummy's Hat");
        }

        if (Bot.Config.Get<Helmets>("Helmets") == Helmets.Dracolich_Destroyer_Mask || Bot.Config.Get<Helmets>("Helmets") == Helmets.All && !Core.CheckInventory("Dracolich Destroyer Mask"))
        {
            Core.HuntMonster("dragonheart", "Avatar of Desolich", "Dracolich Destroyer Mask", isTemp: false);
            Bot.Wait.ForPickup("Dracolich Destroyer Mask");
        }

        // if (Bot.Config.Get<Helmets>("Helmets") == Helmets.Insert || Bot.Config.Get<Helmets>("Helmets") == Helmets.All && !Core.CheckInventory("Insert"))
        // {
        //     Core.HuntMonster("Map", "Mob", "Item", isTemp: false);
        //     Bot.Wait.ForPickup("Item");
        // }

    }
}

public enum Helmets
{
    Sekts_Mummys_Hat,
    Dracolich_Destroyer_Mask,
    All,
    None
}
