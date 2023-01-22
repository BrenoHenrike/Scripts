/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class VoidDestroyer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();

    public readonly string[] Rewards =
    {
        "Void Destroyer",
        "Void Destruction Blade",
        "Void Spear of War",
        "Horned Void War Helm",
        "Crested Void War Helm",
        "Wrap of the Void",
        "Tainted Destruction Blade",
        "Toxic Void Katana",
        "Dual Toxic Void Katanas"
    };
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetDestroyer();

        Core.SetOptions(false);
    }

    public void GetDestroyer()
    {
        int i = 1;
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards, toInv: false))
        {
            Nation.Supplies("Unidentified 4");
            Nation.SwindleBulk(1);
            Nation.FarmDarkCrystalShard(1);
            Nation.EssenceofNulgath(1);
            Nation.FarmGemofNulgath(1);

            Core.ChainComplete(5661);
            Bot.Drops.Pickup(Rewards);
            Core.Logger($"Completed x{i++}");
        }
    }
}
