/*
name: Army leveling
description: Uses your army, and different methods, to level up
tags: army, icestormarena, icestormunder, icewing, aggro
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/Legion/DarkWarLegionandNation.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models.Monsters;
using System.Collections.Generic;
using Skua.Core.Models.Items;

public class ArmyLeveling
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    public CoreArmyLite Army = new();
    public SevenCircles SC = new();
    private CoreSoW SoW = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "ArmyLeveling";
    public List<IOption> Options = new List<IOption>
    {
        new Option<Method>("LevelMethod", "Map selection", "Which map to farm Experience?", Method.IceStormArena),
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

    public int level = 75;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        Bot.Lite.ReacceptQuest = true;
        Level();
        Bot.Lite.ReacceptQuest = false;
        Core.SetOptions(false);
    }

    public void Level(int level = 100)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Farm.ToggleBoost(BoostType.Experience);
        Farm.ToggleBoost(BoostType.Gold);

        // Get the selected method from the configuration
        Method selectedMethod = Bot.Config!.Get<Method>("LevelMethod");

        // Execute the selected method
        switch (selectedMethod)
        {
            case Method.IceStormArena:
                Core.EquipClass(ClassType.Farm);
                Army.waitForParty("icestormarena");
                Army.AggroMonCells("r22");
                Army.AggroMonStart("icestormarena");
                Army.DivideOnCells("r22");
                Core.RegisterQuests();
                while (!Bot.ShouldExit && Bot.Player.Level < level)
                    Bot.Combat.Attack("*");
                Army.AggroMonStop(true);
                Core.JumpWait();
                Army.waitForParty("whitemap");
                break;

            case Method.IceStormUnder:
                if (Bot.Player.Level < 75)
                    Core.Logger("Player is below lvl 75, which is\n" +
                    "required for the map. --stopping", stopBot: true);
                Core.EquipClass(ClassType.Farm);
                Army.waitForParty("icestormunder");
                Army.AggroMonCells("r2");
                Army.AggroMonStart("icestormunder");
                Army.DivideOnCells("r2");
                while (!Bot.ShouldExit && Bot.Player.Level < level)
                    Bot.Combat.Attack("Frost Spirit");
                Army.AggroMonStop(true);
                Core.JumpWait();
                Army.waitForParty("whitemap");
                break;

            case Method.IceWing:
                if (Bot.Player.Level < 75)
                    Core.Logger("Player is below lvl 75, required for\n" +
                    "the map --stopping", stopBot: true);
                Core.EquipClass(ClassType.Solo);
                Army.waitForParty("icewing");
                Army.AggroMonCells('Enter');
                Army.AggroMonStart("icewing");
                Army.DivideOnCells("Enter");
                Core.RegisterQuests(Core.IsMember ? 6635 : 6632);
                while (!Bot.ShouldExit && Bot.Player.Level < level)
                    Bot.Combat.Attack("*");
                Army.AggroMonStop(true);
                Core.JumpWait();
                Army.waitForParty("whitemap");
                break;

            case Method.SevenCirclesWar:
                SC.CirclesWar(true);
                Core.EquipClass(ClassType.Farm);
                Army.waitForParty("sevencircleswar");
                Army.AggroMonCells("Enter", "r1", "r2", "r3");
                Army.AggroMonStart("sevencircleswar");
                Army.DivideOnCells("Enter", "r1", "r2", "r3");
                Core.RegisterQuests(7979, 7980, 7981);
                while (!Bot.ShouldExit && Bot.Player.Level < level)
                    Bot.Combat.Attack("*");
                Army.AggroMonStop(true);
                Core.JumpWait();
                Army.waitForParty("whitemap");
                break;

            case Method.Streamwar:
                SoW.TimestreamWar();
                Core.EquipClass(ClassType.Farm);
                Core.AddDrop("Prismatic Seams");
                Army.waitForParty("streamwar");
                Army.AggroMonCells("r3a");
                Army.AggroMonStart("streamwar");
                Army.DivideOnCells("r3a");
                Core.RegisterQuests(8814, 8815);
                while (!Bot.ShouldExit && Bot.Player.Level < level)
                    Bot.Combat.Attack("*");
                Army.AggroMonStop(true);
                Core.JumpWait();
                Army.waitForParty("whitemap");
                break;

                //add more cases

                /*
                case Method.Method:
                Core.EquipClass(ClassType.ClassType);
                Army.waitForParty("map");
                Army.AggroMonCells(cells);
                Army.AggroMonStart("map");
                Army.DivideOnCells("cell");
                Core.RegisterQuests(questIDs);
                while (!Bot.ShouldExit && Bot.Player.Level < level)
                    Bot.Combat.Attack("*");
                Army.AggroMonStop(true);
                Core.JumpWait();
                Army.waitForParty("whitemap");
                break;

                */
        }
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Experience, false);
        Core.CancelRegisteredQuests();
        Core.JumpWait();
    }

    public enum Method
    {
        IceStormArena = 1,
        IceStormUnder = 2,
        Streamwar = 3,
        SevenCirclesWar = 4,
        IceWing = 5

    }

}
