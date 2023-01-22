/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class DualWield
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        WeaponReflection();

        Core.SetOptions(false);
    }


    public void WeaponReflection(int quant = 200)
    {
        if (Core.CheckInventory("Weapon Reflection", quant))
            return;

        Core.Logger("Checking if Your Acc is 8 Years Old");

        Core.BuyItem(Bot.Map.Name, 1317, "Golden 8th Birthday Candle");
        if (!Core.CheckInventory("Golden 8th Birthday Candle"))
        {
            Core.Logger("your acc isn't old enough.");
            return;
        }

        Core.AddDrop("Weapon Reflection");
        while (!Bot.ShouldExit && (!Core.CheckInventory("Weapon Reflection", quant)))
        {
            Core.EnsureAccept(5518);
            Core.HuntMonster("nostalgiaquest", "Skeletal Viking", "Reflected Glory", 5);
            Core.HuntMonster("nostalgiaquest", "Skeletal Warrior", "Divided Light", 5);
            Core.EnsureComplete(5518);
            Bot.Wait.ForPickup("Weapon Reflection");
        }
    }
}
