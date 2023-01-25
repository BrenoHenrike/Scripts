/*
name: Army Gold
description: Uses different methods with your army to farm gold.
tags: army, gold, battle ground e, dark war legion, dark war nation, seven circles war
*/
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/Legion/DarkWarLegionandNation.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class ArmyGold
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    private DarkWarLegionandNation DWLN = new();
    public SevenCircles SC = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyGold";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<Method>("mapname", "Map selection", "Which map to farm gold?", Method.BattleGroundE),
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.player7,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions(disableClassSwap: true);

        Setup(Bot.Config.Get<Method>("mapname"));

        Core.SetOptions(false);
    }

    public void Setup(Method mapname)
    {
        Core.EquipClass(ClassType.Farm);
        Adv.BestGear(GearBoost.gold);
        Farm.ToggleBoost(BoostType.Gold);

        if (((int)mapname == 0) || ((int)mapname == 1))
            BGE(Bot.Config.Get<Method>("mapname"));
        else if (((int)mapname == 2))
            DWL();
        else if (((int)mapname == 3))
            DWN();
        else if (((int)mapname == 4))
            SCW();
    }

    public void BGE(Method mapname)
    {
        if ((int)mapname == 0 && Bot.Player.Level <= 60)
            Core.Logger("Minimum level 61 required for this map", messageBox: true, stopBot: true);

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (((int)mapname == 0 && Core.IsMember))
            Core.RegisterQuests(3991, 3992, 3993);
        else if (((int)mapname == 1))
            Core.RegisterQuests(3992, 3993);
        else
            Core.RegisterQuests(3991, 3992);
        Army.AggroMonMIDs(1, 2, 3, 4, 5, 6);
        Army.AggroMonStart(mapname.ToString());

        if ((int)mapname == 0)
            Army.DivideOnCells("r5", "r4", "r3", "r2");
        else Army.DivideOnCells("r4", "r3", "r2", "r1");

        while (!Bot.ShouldExit && Bot.Player.Gold < 100000000)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
    }

    public void DWL()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        DWLN.DarkWarLegion();

        Core.RegisterQuests(8584, 8585, 8586, 8587); //Nation Badges 8584, Mega Nation Badges 8585, A Nation Defeated 8586, ManSlayer? More Like ManSLAIN 8587
        Army.SmartAggroMonStart("darkwarlegion", "Bloodfiend", "Dreadfiend", "Infernal Fiend", "Manslayer Fiend", "Void Fiend");
        while (!Bot.ShouldExit && Bot.Player.Gold < 100000000)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
    }

    public void DWN()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        DWLN.DarkWarNation();

        Core.RegisterQuests(8578, 8579, 8580, 8581); //Legion Badges, Mega Legion Badges, Doomed Legion Warriors, Undead Legion Dread
        Army.SmartAggroMonStart("darkwarnation", "High Legion Inquisitor", "Legion Doomknight", "Legion Dread Knight", "Legion Dreadmarch", "Legion Fiend Rider");
        while (!Bot.ShouldExit && Bot.Player.Gold < 100000000)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
    }

    public void SCW()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        SC.CirclesWar();

        Core.RegisterQuests(7979, 7980, 7981);
        Army.SmartAggroMonStart("sevencircleswar", "Wrath Guard", "Heresy Guard", "Violence Guard", "Treachery Guard");
        while (!Bot.ShouldExit && Bot.Player.Gold < 100000000)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
    }

    public enum Method
    {
        BattleGroundE = 0,
        HonorHall = 1,
        DarkWarLegion = 2,
        DarkWarNation = 3,
        SevenCirclesWar = 4
    }
}
