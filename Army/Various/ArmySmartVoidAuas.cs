/*
name: Smart Void Auras (Army)
description: Farms Void Auras with the best method available using your army.
tags: army, void, aura, smart, VA, NSOD, necrotic, sword, doom
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Army/CoreArmyLite.cs

using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Models.Monsters;

public class ArmySmartVoidAuras
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "RVAArmy";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<bool>("sellToSync", "Sell to Sync", "Sell items to make sure the army stays syncronized.\nIf off, there is a higher chance your army might desyncornize", false),
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        //limit to 4 due to /dragonchallenge
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public string[] VA =
    {
        "Void Aura",
        "Astral Ephemerite Essence",
        "Belrot the Fiend Essence",
        "Black Knight Essence",
        "Tiger Leech Essence",
        "Carnax Essence",
        "Chaos Vordred Essence",
        "Dai Tengu Essence",
        "Unending Avatar Essence",
        "Void Dragon Essence",
        "Creature Creation Essence"
    };

    public string[] VASDKA =
    {
        "Void Aura",
        "Empowered Essence",
        "Malignant Essence"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        if (Core.CheckInventory(14474))
            Core.BankingBlackList.AddRange(VA);
        else Core.BankingBlackList.AddRange(VASDKA);

        Core.SetOptions();

        Setup();

        Core.SetOptions(false);
    }

    public void Setup()
    {
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Bot.Events.PlayerAFK += PlayerAFK;
        if (Core.CheckInventory(14474))
            CommandingShadowEssences(7500);
        else
        {
            Farm.EvilREP();
            RetrieveVoidAurasArmy(7500);
        }
        Bot.Events.PlayerAFK -= PlayerAFK;
    }

    public void CommandingShadowEssences(int Quantity)
    {
        if (Core.CheckInventory("Void Aura", Quantity))
            return;

        Core.AddDrop(VASDKA);
        Core.Logger($"Farming Void Aura's with SDKA Method");
        Core.FarmingLogger($"Void Aura", Quantity);

        while (!Bot.ShouldExit && !Core.CheckInventory("Void Aura", Quantity))
        {
            Core.EnsureAccept(4439);
            ArmyHunt("shadowrealmpast", new[] { "Pure Shadowscythe", "Shadow Guardian", "Shadow Warrior" }, "Empowered Essence", ClassType.Farm, isTemp: false, 50);
            ArmyHunt("shadowrealmpast", new[] { "Shadow Lord" }, "Malignant Essence", ClassType.Solo, isTemp: false, 3);
            Core.EnsureComplete(4439);
        }
    }

    public void RetrieveVoidAurasArmy(int Quantity)
    {
        if (Core.CheckInventory("Void Aura", Quantity))
            return;

        Core.AddDrop(VA);
        Core.Logger($"Farming Void Aura's with Non-SDKA Method");
        Core.FarmingLogger($"Void Aura", Quantity);

        while (!Bot.ShouldExit && !Core.CheckInventory("Void Aura", Quantity))
        {
            Core.EnsureAccept(4432);
            ArmyHunt("timespace", new[] { "Astral Ephemerite" }, "Astral Ephemerite Essence", ClassType.Farm, isTemp: false, 100);
            ArmyHunt("citadel", new[] { "Belrot the Fiend" }, "Belrot the Fiend Essence", ClassType.Farm, isTemp: false, 100);
            ArmyHunt("greenguardwest", new[] { "Black Knight" }, "Black Knight Essence", ClassType.Solo, isTemp: false, 100);
            ArmyHunt("mudluk", new[] { "Tiger Leech" }, "Tiger Leech Essence", ClassType.Solo, isTemp: false, 100);
            ArmyHunt("aqlesson", new[] { "Carnax" }, "Carnax Essence", ClassType.Solo, isTemp: false, 100);
            ArmyHunt("necrocavern", new[] { "Chaos Vordred" }, "Chaos Vordred Essence", ClassType.Solo, isTemp: false, 100);
            ArmyHunt("hachiko", new[] { "Dai Tengu" }, "Dai Tengu Essence", ClassType.Solo, isTemp: false, 100);
            ArmyHunt("timevoid", new[] { "Unending Avatar" }, "Unending Avatar Essence", ClassType.Solo, isTemp: false, 100);
            ArmyHunt("dragonchallenge", new[] { "Void Dragon" }, "Void Dragon Essence", ClassType.Solo, isTemp: false, 100);
            ArmyHunt("maul", new[] { "Creature Creation" }, "Creature Creation Essence", ClassType.Solo, isTemp: false, 100);
            Core.EnsureCompleteMulti(4432);
        }
        Core.ConfigureAggro(false);
    }

    void ArmyHunt(string map, string[] monsters, string item, ClassType classType, bool isTemp = false, int quant = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Bot.Config!.Get<bool>("sellToSync"))
            Army.SellToSync(item, quant);

        Core.AddDrop(item);

        Core.EquipClass(classType);
        //Army.waitForParty(map, item);
        Core.FarmingLogger(item, quant);

        Army.SmartAggroMonStart(map, monsters);

        

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
    }

    public void PlayerAFK()
    {
        Core.Logger("Anti-AFK engaged");
        Core.Sleep(1500);
        Bot.Send.Packet("%xt%zm%afk%1%false%");
    }
}
