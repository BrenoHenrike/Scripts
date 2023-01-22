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
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nation/CoreNation.cs

using Skua.Core.Interfaces;

public class HollowbornOblivionBlade
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreHollowborn HB = new CoreHollowborn();
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetBlade();

        Core.SetOptions(false);
    }

    public void GetBlade()
    {
        if (Core.CheckInventory("Hollowborn Oblivion Blade"))
            return;

        Core.AddDrop("Hollowborn Oblivion Blade", "4th Dimension Gem");

        Core.EnsureAccept(7294);
        Farm.Experience(80);
        Nation.FarmVoucher(false);
        ArchFiendEnchantedOrbs();
        Nation.FarmUni13();
        Nation.DiamondEvilWar(500);
        if (!Core.CheckInventory("Unidentified 25"))
        {
            Farm.Gold(15000000);
            Core.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
            Bot.Wait.ForPickup("Unmoulded Fiend Essence");
            Core.BuyItem("tercessuinotlim", 1951, "Unidentified 25");
        }
        Nation.ApprovalAndFavor(0, 1000);
        Nation.FarmBloodGem(50);
        Core.HuntMonster("lostruinswar", "Diabolical Warlord", "Diabolical Minion's Seed", isTemp: false);
        if (!Core.CheckInventory("4th Dimension Gem"))
        {
            Core.EnsureAccept(5163);
            Core.HuntMonster("blackholesun", "Black Light Elemental", "Black Light", 5);
            Core.EnsureComplete(5163);
            Bot.Wait.ForPickup("4th Dimension Gem");
        }
        Core.HuntMonster("goldenarena", "Blessed Dragon", "Celestial Seal", isTemp: false);
        Core.HuntMonster("shadowattack", "Death", "Death's Power", isTemp: false);
        HB.FreshSouls(1, 350);
        Core.EnsureComplete(7294);
        Bot.Wait.ForPickup("Hollowborn Oblivion Blade");
        Adv.EnhanceItem("Hollowborn Oblivion Blade", EnhancementType.Lucky, CapeSpecial.None, HelmSpecial.None, WeaponSpecial.Spiral_Carve);
    }

    public void ArchFiendEnchantedOrbs()
    {
        if (Core.CheckInventory("ArchFiend Enchanted Orbs"))
            return;

        HB.FreshSouls(1, 100);
        if (!Core.CheckInventory("Unidentified 25"))
        {
            Farm.Gold(15000000);
            Core.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
            Core.BuyItem("tercessuinotlim", 1951, "Unidentified 25");
        }
        Nation.FarmUni13();
        Nation.DiamondEvilWar(150);
        Nation.FarmBloodGem(10);
        Nation.FarmVoucher(false);
        Core.BuyItem("tercessuinotlim", 1820, "ArchFiend Enchanted Orbs");
        Bot.Wait.ForPickup("ArchFiend Enchanted Orbs");
    }
}
