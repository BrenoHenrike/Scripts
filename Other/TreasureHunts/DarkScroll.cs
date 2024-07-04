/*
name: Dark Scroll Treasure Hunt
description: such a treasure hunt condensed to (by lucK) 2 minutes of your time...
tags: dark scroll, amethyst gem, lilbro dell'inferno, amethyst inferno tome, inferno tome, burning seal, deciphered scroll, the point, the point sword
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class DarkScroll
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Example();

        Core.SetOptions(false);
    }

    public void Example()
    {
        if (Core.CheckInventory(new[] { "The Point", "The Point Sword" }))
            return;

        if (!Core.CheckInventory("Deciphered Scroll"))
        {
            Core.Logger($"Step 1 Dark Scroll");
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("tercessuinotlim", "m2", "Left", "*", "Dark Scroll", isTemp: false);

            if (!Core.CheckInventory("Amethyst Inferno Tome"))
            {
                Core.Logger($"Step 2 Libro dell'Inferno");
                if (!Core.CheckInventory("Libro dell'Inferno"))
                    Core.BuyItem("zazul", 336, "Libro dell'Inferno");

                Core.EquipClass(ClassType.Solo);
                Adv.GearStore();
                Core.Logger($"Step 3 Amethyst Gem");
                Core.HuntMonster("onslaughttower", "Maximillian Lionfang", "Amethyst Gem", isTemp: false, log: false);
                Adv.GearStore(true);

                //Amethyst Inferno Tome
                Core.Logger($"Step 4 Amethyst Inferno Tome");
                Core.BuyItem("yokaiwar", 1370, "Amethyst Inferno Tome");
            }

            //Burning Seal
            Core.Logger($"Step 5 Burning Seal");
            Core.HuntMonster("phoenixrise", "Cinderclaw", "Burning Seal", isTemp: false, log: false);

            //Blazing Catalyst
            Core.Logger($"Step 6 Blazing Catalyst");
            if (!Core.CheckInventory(77148))
            {
                Bot.Drops.Add(77148);
                Core.GetMapItem(11464, 1, "feverfew");
                Bot.Wait.ForDrop(77148, 20);
                Bot.Wait.ForPickup(77148);
            }

            //Deciphered Scroll
            Core.Logger($"Step 7 Deciphered Scroll");
            Core.ChainComplete(5027);
            Core.BuyItem("Icarus", 590, "Deciphered Scroll");
        }

        //Rewards
        Core.Logger($"Step 8 Rewards");
        Core.AddDrop(38805, 41969);
        if (!Core.CheckInventory(38805))
        {
            Core.EnsureAccept(3003);
            Core.GetMapItem(11475, 1, "junkheap");
            Core.EnsureComplete(3003, 38805);
        }
        if (!Core.CheckInventory(41969))
        {
            Core.EnsureAccept(3003);
            Core.GetMapItem(11475, 1, "junkheap");
            Core.EnsureComplete(3003, 41969);
        }
        Core.Logger("All rewards gotten, congrats.");

    }
}
