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
using Skua.Core.Models.Items;
using Skua.Core.Models.Skills;

public class CoreSoWMats
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    private CoreSoW SoW = new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void Acquiescence(int Quantity = 1000)
    {
        SoW.ShadowFlame();
        Core.EquipClass(ClassType.Solo);
        FarmReq(() => {
            Core.HuntMonster("worldscore", "Elemental Attempt", "Cracked Elemental Stone", 8);
            Core.HuntMonster("worldscore", "Crystalized Mana", "Crystalized Tooth", 8);
            Core.HuntMonster("worldscore", "Mask of Tranquility", "Creator's Favor", 1);
        }, "Acquiescence", Quantity, 8966);
    }

    public void DragonsTear(int Quantity = 1)
    {
        if (Core.CheckInventory("Dragon's Tear"))
            return;

        Willpower(10);
        GarishRemnant(30);
        PrismaticSeams(100);
        UnboundThread(30);
        Acquiescence(25);
        ElementalCore(40);   
        Adv.BuyItem("manacradle", 2242, "Dragon's Tear");
    }
    public void ElementalCore(int Quantity = 1000)
    {
        SoW.ManaCradle();
        if (Core.CheckInventory("Yami no Ronin") || Core.CheckInventory("Dragon of Time"))
        {
            Bot.Skills.StartAdvanced(Core.CheckInventory("Yami no Ronin") ? "Yami no Ronin" : "Dragon of Time", true, ClassUseMode.Solo);
        }
        else Core.EquipClass(ClassType.Solo);

        FarmReq(() => {
            Core.HuntMonster("manacradle", "Dark Tainted Mana", "Elemental Tear", 8);
            Core.HuntMonster("manacradle", "Malgor", "Weathered Armor Shard");
            Core.HuntMonster("manacradle", "The Mainyu", "Licorice Scale");
        }, "Elemental Core", Quantity, 9126);
    }
    public void GarishRemnant(int Quantity = 1000)
    {
        SoW.Timekeep();
        FarmReq(() => {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("Timekeep", "Mal-formed Gar", "Gar's Resignation Letter");
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("Timekeep", "Mumbler", "Mumbler Drool", 8);
            Core.HuntMonster("Timekeep", "Decaying Locust", "Locust Wings", 8);
        }, "Garish Remnant", Quantity, 8813);
    }
    public void PrismaticSeams(int Quantity = 2000)
    {
        SoW.TimestreamWar();
        Core.EquipClass(ClassType.Farm);
        FarmReq(() =>{
            Core.KillMonster("Streamwar", "r3a", "Left", "*", log: false);
        }, "Prismatic Seams", Quantity, 8814, 8815);
    }
    public void UnboundThread(int Quantity = 1000)
    {
        SoW.DeadLines();
        Core.EquipClass(ClassType.Solo);
        FarmReq(() =>{
            Core.HuntMonster("DeadLines", "Shadowfall Warrior", "Armor Scrap", 8);
            Core.HuntMonster("DeadLines", "Frenzied Mana", "Captured Mana", 8);
            Core.HuntMonster("DeadLines", "Eternal Dragon", "Eternal Dragon Scale");
        }, "Unbound Thread", Quantity, 8869);
    }
    public void Willpower(int Quantity = 1000)
    {
        SoW.RuinedCrown();
        FarmReq(() =>{
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster($"ruinedcrown", "Calamitous Warlic", "Warlic’s Favor");
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("ruinedcrown", "Frenzied Mana", "Mana Residue", 8);
            Core.HuntMonster($"ruinedcrown", "Mana-Burdened Mage", "Mage’s Blood Sample", 8);
        }, "Willpower", Quantity, 8788);
    }

    // UTILITIES
    public void FarmReq(Action Kill, string Material, int Quantity, params int[] questIDs)
    {
        if (Core.CheckInventory(Material, Quantity))
            return;

        Core.FarmingLogger(Material, Quantity);
        Core.AddDrop(Material);
        Core.RegisterQuests(questIDs);
        while (!Bot.ShouldExit && !Core.CheckInventory(Material, Quantity))
        {
            Kill();
            Bot.Wait.ForPickup(Material);
        }
        Core.RemoveDrop(Material);
        Core.CancelRegisteredQuests();
    }
}
