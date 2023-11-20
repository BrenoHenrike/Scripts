/*
name: ScarletSorceress
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Other/Classes/BloodSorceress.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class ScarletSorceress
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm => new();
    public CoreAdvanced Adv = new();
    public CoreToD TOD = new();
    public BloodSorceress BS = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetSSorc();

        Core.SetOptions(false);
    }

    public void GetSSorc(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Scarlet Sorceress"))
        {
            if (rankUpClass)
                Adv.RankUpClass("Scarlet Sorceress");
            return;
        }

        Core.AddDrop("Scarlet Sorceress");

        TOD.TowerofMirrors();
        BS.GetBSorc(false);

        //checking if BS has atleast 1 classpoint.
        Adv.GearStore();
        InventoryItem? BloodSorceress = Bot.Inventory.Items.Find(i => i.Name.ToLower().Trim() == "Blood Sorceress".ToLower().Trim() && i.Category == ItemCategory.Class);

        Core.JumpWait();
        while (!Bot.ShouldExit && BloodSorceress!.Quantity < 1)
        {
            if (Core.CheckInventory("Blood Sorceress") && !Bot.Inventory.IsEquipped("Blood Sorceress"))
            {
                Core.Equip("Blood Sorceress");
                Core.Sleep(Core.ActionDelay);
            }
            else if (Bot.Inventory.IsEquipped("Blood Sorceress"))
                Core.KillMonster("battleontown", "Enter", "Spawn", "*");
        }
        Adv.GearStore(true);

        Farm.Experience(50);

        Core.ChainComplete(6236);
        Bot.Wait.ForPickup("Scarlet Sorceress");
        Core.Logger("Quest Classes are received unenhanced until relog...\n" +
        "GOOD LUCK ON THE RELOG WORKING");

        Core.Relogin();

        while (!Bot.ShouldExit && Bot.Map.Name != "battleon")
        {
            Core.Sleep(Core.ActionDelay);
            Bot.Wait.ForMapLoad("battleon");
        }

        Core.Logger("Holy F**k it worked, congrats!");

        if (rankUpClass)
            Adv.RankUpClass("Scarlet Sorceress");
    }
}
