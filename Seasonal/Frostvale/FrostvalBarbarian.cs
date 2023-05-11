/*
name: Frostval Barbarian (Class)
description: This will finish the required quest and farms the required materials in order to get the Frostval Barbarian (Class).
tags: frostval-barbarian, class, seasonal, frostvale
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Frostvale/Frostvale.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class FrostvalBarbarian
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public Frostvale Frostvale = new Frostvale();
    public GlaceraStory Glacera = new GlaceraStory();
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetFB();

        Core.SetOptions(false);
    }

    public void GetFB(bool rankUpClass = true)
    {

        if (Core.CheckInventory("Frostval Barbarian"))
            return;
        if (!Core.isSeasonalMapActive("frostvale"))
            return;

        Glacera.DoAll();
        Frostvale.DoAll();

        if (!Core.CheckInventory("Infernal Ice Heart") && !Core.CheckInventory("Crypto Token", 5))
        {
            Daily.CryptoToken();
            if (!Core.CheckInventory("Crypto Token", 5))
                Core.Logger($"Please do the Crypto Token Daily {5 - Bot.Inventory.GetQuantity("Crypto Token")} more times before continueing the farm", messageBox: true, stopBot: true);
        }

        Core.AddDrop("Frostval Barbarian", "Frosty Barbarian's Helm", "Frosty Barbarian's Horns", "Bearded Barbarian Helm", "Frostval Barbarian Cape", "Frostval Barbarian Sword");
        Core.EnsureAccept(6649);
        Core.BuyItem("frostdeep", 520, "Sword Of Hope");
        if (!Core.CheckInventory("Sassafras' War Helm"))
        {
            Core.AddDrop("Sassafras' War Helm");
            while (!Bot.ShouldExit && !Core.CheckInventory("Sassafras' War Helm"))
            {
                Core.EnsureAccept(2570);
                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("newbie", "Slime", "Potent Dried Slime", 6);
                Core.EnsureComplete(2570);
            }
        }
        if (!Core.CheckInventory("Fur Tuft"))
        {
            Core.AddDrop("Fur Tuft");
            Core.EnsureAccept(1513);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("alpine", "Wendigo", "Woebegone Wendigo");
            Core.EnsureComplete(1513);
        }
        if (!Core.CheckInventory("Icy Holly"))
        {
            Core.AddDrop("Icy Holly");
            Core.EnsureAccept(6132);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("coldwindvalley", "Snow Golem", "Elemental Ice", 5);
            Core.GetMapItem(5557, 8, "coldwindvalley");
            Core.EnsureComplete(6132);
        }
        if (!Core.CheckInventory("Glaceran Key"))
        {
            Core.AddDrop("Glaceran Key");
            Core.EnsureAccept(3971);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("northstar", "Karok the Fallen", "Karok defeated", 1);
            Core.EnsureComplete(3971);
        }
        if (!Core.CheckInventory("Infernal Ice Heart"))
        {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("frostvalfuture", "Wargoth the Frozen", "Frozen Orb", 5, false);
            Core.BuyItem("curio", 1539, "Infernal Ice Heart");
        }
        Core.EnsureComplete(6649);

        Bot.Wait.ForPickup("Frostval Barbarian");
        if (rankUpClass)
            Adv.rankUpClass("Frostval Barbarian");
    }
}
