/*
name: Trading and Stuff (Single)
description: This script will complete "Trading and Stuff (Single)" quest.
tags: trading and stuff, single, hollowborn oblivion blade, hollowborn, oblivion, blade
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nation/CoreNation.cs

using Skua.Core.Interfaces;

public class TradingandStuffSingle
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreHollowborn HB = new();
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetBlade();

        Core.SetOptions(false);
    }

    public void GetBlade()
    {
        if (Core.CheckInventory("Hollowborn Oblivion Blade") || !Core.CheckInventory("Oblivion Blade of Nulgath"))
            return;

        Core.AddDrop("Hollowborn Oblivion Blade");

        Core.EnsureAccept(7295);
        Farm.Experience(80);
        Nation.FarmVoucher(false);
        ArchFiendEnchantedOrbs();
        Core.HuntMonster("shadowattack", "Death", "Death's Power", isTemp: false);
        if (!Core.CheckInventory("Unidentified 25"))
        {
            Farm.Gold(15000000);
            Core.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
            Bot.Wait.ForPickup("Unmoulded Fiend Essence");
            Core.BuyItem("tercessuinotlim", 1951, "Unidentified 25");
        }
        HB.FreshSouls(1, 100);
        Core.EnsureComplete(7295);
        Bot.Wait.ForPickup("Hollowborn Oblivion Blade");
        Adv.EnhanceItem("Hollowborn Oblivion Blade", EnhancementType.Lucky, CapeSpecial.None, HelmSpecial.None, WeaponSpecial.Spiral_Carve);
    }

    public void ArchFiendEnchantedOrbs()
    {
        if (Core.CheckInventory("ArchFiend Enchanted Orbs"))
            return;

        Core.Logger("Farming ArchFiend Enchanted Orbs.");

        HB.FreshSouls(1, 100);
        if (!Core.CheckInventory("Unidentified 25"))
        {
            Farm.Gold(15000000);
            Core.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
            Core.BuyItem("tercessuinotlim", 1951, "Unidentified 25");
        }
        Nation.FarmUni13(1);
        Nation.DiamondEvilWar(150);
        Nation.FarmBloodGem(10);
        Nation.FarmVoucher(false);
        Core.BuyItem("tercessuinotlim", 1820, "ArchFiend Enchanted Orbs");
        Bot.Wait.ForPickup("ArchFiend Enchanted Orbs");
    }
}
