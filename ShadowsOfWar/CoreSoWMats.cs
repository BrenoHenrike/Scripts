/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Skills;

public class CoreSoWMats
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();
    private CoreSoW SoW = new();

    public void ScriptMain(IScriptInterface Bot) => Core.RunCore();

    public void DragonsTear()
    {
        if (Core.CheckInventory("Dragon's Tear"))
            return;

        Core.FarmingLogger("Dragon's Tear", 1);

        Willpower(10);
        GarishRemnant(30);
        PrismaticSeams(100);
        UnboundThread(30);
        Acquiescence(25);
        ElementalCore(40);
        Adv.BuyItem("manacradle", 2242, "Dragon's Tear");
    }

    public void Acquiescence(int Quantity = 1000)
    {
        if (Core.CheckInventory("Acquiescence", Quantity))
            return;

        SoW.ShadowFlame();

        Core.FarmingLogger("Acquiescence", Quantity);
        Core.AddDrop("Acquiescence");
        Core.EquipClass(ClassType.Solo);

        Core.RegisterQuests(8966);
        while (!Bot.ShouldExit && !Core.CheckInventory("Acquiescence", Quantity))
        {
            Core.HuntMonster("worldscore", "Elemental Attempt", "Cracked Elemental Stone", 14, log: false);
            Core.HuntMonster("worldscore", "Crystalized Mana", "Crystalized Tooth", 14, log: false);
            Core.HuntMonster("worldscore", "Mask of Tranquility", "Creator's Favor", 1, log: false);
        }
        Core.CancelRegisteredQuests();
    }

    public void ElementalCore(int Quantity = 1000)
    {
        if (Core.CheckInventory("Elemental Core", Quantity))
            return;

        SoW.ManaCradle();

        if (!Core.isCompletedBefore(9126))
        {
            Core.Logger("Once Upon Another Time 9126, missing quest progress (Group Boss - skua no can do)");
            return;
        }

        Core.FarmingLogger("Elemental Core", Quantity);
        Core.AddDrop("Elemental Core");

        Adv.GearStore();
        Core.BossClass();

        Core.RegisterQuests(9126);
        while (!Bot.ShouldExit && !Core.CheckInventory("Elemental Core", Quantity))
        {
            Core.HuntMonster("manacradle", "Dark Tainted Mana", "Elemental Tear", 20, log: false);
            Core.HuntMonster("manacradle", "Malgor", "Weathered Armor Shard", log: false);
            Core.HuntMonster("manacradle", "The Mainyu", "Licorice Scale", log: false);
        }
        Core.CancelRegisteredQuests();
        Adv.GearStore(true);
    }

    public void GarishRemnant(int Quantity = 1000)
    {
        if (Core.CheckInventory("Garish Remnant", Quantity))
            return;

        SoW.Timekeep();

        Core.FarmingLogger("Garish Remnant", Quantity);
        Core.AddDrop("Garish Remnant");

        Core.RegisterQuests(8813);
        while (!Bot.ShouldExit && !Core.CheckInventory("Garish Remnant", Quantity))
        {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("Timekeep", "Mal-formed Gar", "Gar's Resignation Letter", log: false);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("Timekeep", "Mumbler", "Mumbler Drool", 8, log: false);
            Core.HuntMonster("Timekeep", "Decaying Locust", "Locust Wings", 8, log: false);
        }
        Core.CancelRegisteredQuests();
    }

    public void PrismaticSeams(int Quantity = 2000)
    {
        if (Core.CheckInventory("Prismatic Seams", Quantity))
            return;

        SoW.TimestreamWar();

        Core.FarmingLogger("Prismatic Seams", Quantity);
        Core.AddDrop("Prismatic Seams");
        Core.EquipClass(ClassType.Farm);

        Core.RegisterQuests(8814, 8815);
        while (!Bot.ShouldExit && !Core.CheckInventory("Prismatic Seams", Quantity))
            Core.KillMonster("Streamwar", "r3a", "Left", "*", log: false);
        Core.CancelRegisteredQuests();
    }

    public void UnboundThread(int Quantity = 1000)
    {
        if (Core.CheckInventory("Unbound Thread", Quantity))
            return;

        SoW.DeadLines();

        Core.FarmingLogger("Unbound Thread", Quantity);
        Core.AddDrop("Unbound Thread");
        Core.EquipClass(ClassType.Solo);

        Core.RegisterQuests(8869);
        while (!Bot.ShouldExit && !Core.CheckInventory("Unbound Thread", Quantity))
        {
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("DeadLines", "Frenzied Mana", "Captured Mana", 8, log: false);
            Core.HuntMonster("DeadLines", "Shadowfall Warrior", "Armor Scrap", 8, log: false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("DeadLines", "Eternal Dragon", "Eternal Dragon Scale", log: false);
        }
        Core.CancelRegisteredQuests();
    }

    public void Willpower(int Quantity = 1000)
    {
        if (Core.CheckInventory("Willpower", Quantity))
            return;

        SoW.RuinedCrown();

        Core.FarmingLogger("Willpower", Quantity);
        Core.AddDrop("Willpower");

        Core.RegisterQuests(8788);
        while (!Bot.ShouldExit && !Core.CheckInventory("Willpower", Quantity))
        {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("ruinedcrown", "Calamitous Warlic", "Warlic's Favor", log: false);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("ruinedcrown", "Frenzied Mana", "Mana Residue", 8, log: false);
            Core.HuntMonster("ruinedcrown", "Mana-Burdened Mage", "Mage's Blood Sample", 8, log: false);
        }
        Core.CancelRegisteredQuests();
    }
}
