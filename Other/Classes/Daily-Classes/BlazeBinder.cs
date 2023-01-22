/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class BlazeBinder
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreDailies Dailies = new();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetClass();

        Core.SetOptions(false);
    }

    public void GetClass()
    {
        if (Core.CheckInventory("Blaze Binder"))
            return;

        if (!Core.CheckInventory("Pyromancer"))
        {
            Core.Logger($"{Bot.Inventory.GetQuantity("Shurpu Blaze Token")} / 84");
            if (!Core.CheckInventory("Shurpu Blaze Token", 84))
                Dailies.Pyromancer();
            else Adv.BuyItem("xancave", 447, "Pyromancer", shopItemID: 12812);
        }
        else
        {
            InventoryItem itemInv = Bot.Inventory.Items.First(i => i.Name.ToLower() == ("Pyromancer").ToLower() && i.Category == ItemCategory.Class);
            if (itemInv.Quantity < 1)
            {
                Adv.GearStore();
                Core.Equip("Pyromancer");
                Core.Logger("Getting *1* point in Pyro for Blaze Binder");
                Core.Join("Noobshire");
                Bot.Kill.Monster("*");
                Adv.GearStore(true);
            }
            Adv.BuyItem("fireforge", 1142, "Darkness Sigil");
            Adv.BuyItem("fireforge", 1142, "Flame Sigil");
            Adv.BuyItem("fireforge", 1140, "Blaze Binder");
            Bot.Sleep(Core.ActionDelay);
            Adv.rankUpClass("Blaze Binder");
        }
    }
}
