//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;

public class CoreOblivionBladeofNulgath
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreNation Nation = new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void OblivionBladeofFighting()
    {
        if (Core.CheckInventory("Oblivion Wrath Blade Of Nulgath"))
            return;

        PetCheck(1122, 1109);

        Core.RegisterQuests(Core.CheckInventory("Oblivion Blade of Nulgath Pet") ? 1122 : 1109);
        if (!Core.CheckInventory("Oblivion Wrath Blade Of Nulgath"))
            Nation.FarmGemofNulgath(10);
        Core.HuntMonster("tercessuinotlim", "Taro Blademaster", "Blade Master Rune");
        Core.CancelRegisteredQuests();
    }

    public void OblivionNulgath(string item, int quant)
    {
        if (Core.CheckInventory(item, quant))
            return;

        Quest QuestData = Core.EnsureLoad(Core.CheckInventory("Oblivion Blade of Nulgath Pet") ? 2557 : 868);
        ItemBase[] RequiredItems = QuestData.Requirements.ToArray();
        ItemBase[] QuestReward = QuestData.Rewards.ToArray();
        foreach (ItemBase Item in QuestReward)
            Core.AddDrop(Item.Name);
        Core.AddDrop("Mana Energy for Nulgath");

        PetCheck(2557, 868);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonsterMapID("elemental", 7, "Mana Energy for Nulgath", isTemp: false, log: false);
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("elemental", "r3", "Down", "*", "Charged Mana Energy for Nulgath", 5, log: false);
            Bot.Drops.Pickup(item);
            if (Bot.Inventory.IsMaxStack(item))
                Core.Logger("Max Stack Hit.");
            else Core.Logger($"{item}: {Bot.Inventory.GetQuantity(item)}/{quant}");
        }
        Core.CancelRegisteredQuests();
    }


    public void PhoenixBladeofNulgath()
    {
        if (Core.CheckInventory("Phoenix Blade of Nulgath"))
            return;

        PetCheck(2558, 588);

        Nation.FarmDiamondofNulgath(10);
        Nation.FarmDarkCrystalShard(5);
        Nation.SwindleBulk(5);
        Nation.FarmUni13();
        Core.HuntMonster("underworld", "Undead Bruiser", "Undead Bruiser Sigil");

        Core.CancelRegisteredQuests();
    }


    public void TheWeaponParasite()
    {
        if (Core.CheckInventory("Sinister Leech Blade of Nulgath"))
            return;

        PetCheck(2559, 589);

        Nation.Supplies("Unidentified 6");
        Nation.FarmDarkCrystalShard(10);
        Nation.SwindleBulk(10);
        Core.HuntMonster("underworld", "Skull Warrior", "Skull Warrior Rune");

        Core.CancelRegisteredQuests();
    }


    public void DoomWormCreepers()
    {
        if (Core.CheckInventory("Dread Heads of Nulgath"))
            return;

        PetCheck(2560, 591);

        Nation.SwindleBulk(10);
        Adv.BuyItem("Necropolis", 407, "Doom Worm Creepers");
        Core.HuntMonster("underworld", "Legion Fenrir", "Legion Fenrir Rune");

        Core.CancelRegisteredQuests();
    }


    public void TheDarkDeal(int quant)
    {
        if (Core.CheckInventory("Relic of Chaos", quant))
            return;
        Bot.Drops.Add("Relic of Chaos");
        PetCheck(2561, 599);
        while (!Bot.ShouldExit && !Core.CheckInventory("Relic of Chaos", quant))
            Core.HuntMonster("evilmarsh", "Tainted Elemental", "Tainted Soul");

        Core.CancelRegisteredQuests();
    }


    public void VoucheritemChampionofNulgath()
    {
        if (!Core.CheckInventory("Warlord"))
            return;

        if (Core.CheckInventory("Champion Blade of Nulgath"))
            return;


        PetCheck(601, 2562);

        Adv.rankUpClass("Warlord");
        Core.AddDrop("Champion Blade of Nulgath");

        Nation.Supplies("Voucher of Nulgath", voucherNeeded: true);
        Nation.Supplies("Voucher of Nulgath (non-mem)", voucherNeeded: true);
        Nation.FarmUni13();
        Core.HuntMonster("Evilmarsh", "Tainte Elemental", "Tainted Rune of Evil");
        Bot.Wait.ForPickup("Champion Blade of Nulgath");

        Core.CancelRegisteredQuests();
    }

    /// <summary>
    /// Turns in Voucher of Nulgath(mem) for Diamonds of Nulgath
    /// </summary>
    /// <param name="quant">Quanity of Gems of Nulgath to Get</param>
    public void DiamondsofNulgathSale(int quant)
    {
        if (Core.CheckInventory("item"))
            return;

        PetCheck(2563, 603);

        while (!Bot.ShouldExit && !Core.CheckInventory("Diamonds of Nulgath", quant))
            Nation.FarmVoucher(true);

        Core.CancelRegisteredQuests();
    }

    public void ArchfiendCloak()
    {
        if (Core.CheckInventory("Archfiend Cloak of Nulgath"))
            return;

        PetCheck(2564, 607);

        Adv.BuyItem("tercessuinotlim", 68, "Fiend Cloak of Nulgath");
        Nation.FarmVoucher(true);
        Nation.SwindleBulk(7);
        Nation.FarmDarkCrystalShard(7);
        Nation.FarmDiamondofNulgath(3);
        Core.HuntMonster("underworld", "Undead Bruiser", "Undead Bruiser Rune");

        Core.CancelRegisteredQuests();
    }

    public void TheMarks()
    {
        if (Core.CheckInventory("Unholy Reaper of Nulgath"))
            return;

        PetCheck(2565, 610);

        Core.AddDrop("The Secret 1", "The Secret 2");
        Core.EnsureAccept(623);
        Core.HuntMonster("willowcreek", "Hidden Spy", "The Secret 1", isTemp: false);
        Core.HuntMonster("Tercessuinotlim", "Ninja Spy", "The Secret 2", isTemp: false);
        EmpoweringStuff();
        Nation.FarmDiamondofNulgath(13);
        Nation.FarmUni13();

        Core.CancelRegisteredQuests();
    }

    private void EmpoweringStuff()
    {
        if (Core.CheckInventory("Death Scythe of Nulgath"))
            return;

        Core.AddDrop("Death Scythe of Nulgath");

        Core.EnsureAccept(558);
        Nation.FarmUni13();
        Nation.FarmDiamondofNulgath(10);
        Core.HuntMonster("underworld", "Undead Bruiser", "Undead Bruiser Sigil");
        Core.EnsureComplete(558);
        Bot.Wait.ForPickup("Death Scythe of Nulgath");
    }

    void PetCheck(int OblivionID, int OblivionRareID)
    {
        if (Core.IsMember && Core.CheckInventory("Oblivion Blade of Nulgath Pet"))
            Core.RegisterQuests(OblivionID);
        if (Core.CheckInventory("Oblivion Blade of Nulgath Pet (Rare)"))
            Core.RegisterQuests(OblivionRareID);
        else
        {
            Core.Logger("You Dont Own Either of The Required Pets.");
            return;
        }
    }
}
