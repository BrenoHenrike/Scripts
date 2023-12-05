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
using Skua.Core.Models.Quests;

public class ArmyLeveling
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    public CoreArmyLite Army = new();
    public SevenCircles SC = new();
    private CoreSoW SoW = new();
    public CoreStory Story = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "Army Leveling";
    public List<IOption> Options = new List<IOption>
    {
        new Option<MethodV2>("LevelMethod", "Map selection", "Which map to farm Experience?", MethodV2.IceStormArena),
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
        Level();
        Core.SetOptions(false);
    }

    public void Level(int level = 100)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Farm.ToggleBoost(BoostType.Experience);
        Farm.ToggleBoost(BoostType.Gold);

        // Get the selected method from the configuration
        MethodV2 selectedMethod = Bot.Config!.Get<MethodV2>("LevelMethod");

        // Execute the selected method
        switch (selectedMethod)
        {
            case MethodV2.IceStormArena:
                Core.EquipClass(ClassType.Farm);
                Army.waitForParty("whitemap");
                Army.AggroMonCells("r22");
                Army.AggroMonStart("icestormarena");
                Army.DivideOnCells("r22");
                Core.RegisterQuests();
                if (Bot.Player.CurrentClass!.Name == "ArchMage")
                    Bot.Options.AttackWithoutTarget = true;
                while (!Bot.ShouldExit && Bot.Player.Level < level)
                    Bot.Combat.Attack("*");
                Army.AggroMonStop(true);
                Core.JumpWait();
                Farm.ToggleBoost(BoostType.Experience, false);
                Farm.ToggleBoost(BoostType.Gold, false);
                Army.waitForParty("whitemap");
                break;

            case MethodV2.IceStormUnder:
                if (Bot.Player.Level < 75)
                    Core.Logger("Player is below lvl 75, which is\n" +
                    "required for the map. --stopping", stopBot: true);
                Core.EquipClass(ClassType.Farm);
                Army.waitForParty("whitemap");
                Army.AggroMonCells("r2");
                Army.AggroMonStart("icestormunder");
                Army.DivideOnCells("r2");
                if (Bot.Player.CurrentClass!.Name == "ArchMage")
                    Bot.Options.AttackWithoutTarget = true;
                while (!Bot.ShouldExit && Bot.Player.Level < level)
                    Bot.Combat.Attack("Frost Spirit");
                Army.AggroMonStop(true);
                Core.JumpWait();
                Farm.ToggleBoost(BoostType.Experience, false);
                Farm.ToggleBoost(BoostType.Gold, false);
                Army.waitForParty("whitemap");
                break;

            case MethodV2.IceWing:
                if (Bot.Player.Level < 75)
                    Core.Logger("Player is below lvl 75, required for\n" +
                    "the map --stopping", stopBot: true);
                Core.EquipClass(ClassType.Solo);
                Army.waitForParty("whitemap");
                Army.AggroMonCells("Enter");
                Army.AggroMonStart("icewing");
                Army.DivideOnCells("Enter");
                Core.RegisterQuests(Core.IsMember ? 6635 : 6632);
                if (Bot.Player.CurrentClass!.Name == "ArchMage")
                    Bot.Options.AttackWithoutTarget = true;
                while (!Bot.ShouldExit && Bot.Player.Level < level)
                    Bot.Combat.Attack("*");
                Army.AggroMonStop(true);
                Core.JumpWait();
                Farm.ToggleBoost(BoostType.Experience, false);
                Farm.ToggleBoost(BoostType.Gold, false);
                Army.waitForParty("whitemap");
                break;

            case MethodV2.SevenCirclesWar:
                SC.CirclesWar(true);
                Core.EquipClass(ClassType.Farm);
                Army.waitForParty("whitemap");
                Army.AggroMonCells("Enter", "r2", "r3");
                Army.AggroMonStart("sevencircleswar");
                Army.DivideOnCells("Enter", "r2", "r3");
                Core.RegisterQuests(7979, 7980, 7981);
                if (Bot.Player.CurrentClass!.Name == "ArchMage")
                    Bot.Options.AttackWithoutTarget = true;
                while (!Bot.ShouldExit && Bot.Player.Level < level)
                    Bot.Combat.Attack("*");
                Army.AggroMonStop(true);
                Core.JumpWait();
                Farm.ToggleBoost(BoostType.Experience, false);
                Farm.ToggleBoost(BoostType.Gold, false);
                Army.waitForParty("whitemap");
                break;

            case MethodV2.Streamwar:
                SoW.TimestreamWar();
                Core.EquipClass(ClassType.Farm);
                Core.AddDrop("Prismatic Seams");
                Army.waitForParty("whitemap");
                Army.AggroMonCells("r3a");
                Army.AggroMonStart("streamwar");
                Army.DivideOnCells("r3a");
                Core.RegisterQuests(8814, 8815);
                if (Bot.Player.CurrentClass!.Name == "ArchMage")
                    Bot.Options.AttackWithoutTarget = true;
                while (!Bot.ShouldExit && Bot.Player.Level < level)
                    Bot.Combat.Attack("*");
                Army.AggroMonStop(true);
                Core.JumpWait();
                Farm.ToggleBoost(BoostType.Experience, false);
                Farm.ToggleBoost(BoostType.Gold, false);
                Army.waitForParty("whitemap");
                break;


            case MethodV2.ShadowBattleon_High_Levels:
            case MethodV2.ShadowBattleon_Lower_Levels:
            case MethodV2.ShadowBattleon_Baby_Mode:
                RequiredQuest("shadowbattleon", 9426);
                Core.EquipClass(ClassType.Farm);
                Core.AddDrop("Wisper");

                if (selectedMethod == MethodV2.ShadowBattleon_Baby_Mode)
                    Core.RegisterQuests(9421, 9422, 9423);
                else
                    Core.RegisterQuests(9421, 9422, 9426);

                Core.Logger($"Mode Selected: {selectedMethod}");

                if (selectedMethod == MethodV2.ShadowBattleon_High_Levels)
                {
                    Army.AggroMonCells("r11", "r12");
                    Army.AggroMonStart("shadowbattleon");
                    Army.DivideOnCells("r11", "r12");
                }
                else if (selectedMethod == MethodV2.ShadowBattleon_Lower_Levels)
                {
                    Army.AggroMonCells("r11");
                    Army.AggroMonStart("shadowbattleon");
                    Army.DivideOnCells("r11");
                }
                else if (selectedMethod == MethodV2.ShadowBattleon_Baby_Mode)
                {
                    Army.AggroMonCells("Enter");
                    Army.AggroMonStart("shadowbattleon");
                    Army.DivideOnCells("Enter");
                }

                Core.Logger("This method is optimized. If the rate is ever poor, please use SCW.");

                if (Bot.Player.CurrentClass!.Name == "ArchMage")
                    Bot.Options.AttackWithoutTarget = true;

                while (!Bot.ShouldExit && Bot.Player.Level < level)
                    Bot.Combat.Attack("*");

                Army.AggroMonStop(true);
                Core.JumpWait();
                Farm.ToggleBoost(BoostType.Experience, false);
                Farm.ToggleBoost(BoostType.Gold, false);
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
        Core.CancelRegisteredQuests();
        Core.JumpWait();
    }

    public enum MethodV2
    {
        IceStormArena = 1,
        IceStormUnder = 2,
        Streamwar = 3,
        SevenCirclesWar = 4,
        IceWing = 5,
        ShadowBattleon_Baby_Mode = 6,
        ShadowBattleon_Lower_Levels = 7,
        ShadowBattleon_High_Levels = 8,
    }


    void RequiredQuest(string map, int Quest)
    {
        Quest QuestData = Core.EnsureLoad(Quest);
        if (Core.isCompletedBefore(Quest))
        {
            Core.Logger($"{QuestData.Name} [ {QuestData.ID}] Already unlocked! onto the gains.");
            return;
        }

        Bot.Lite.ReacceptQuest = false;
        Core.Logger($"Unlocking {QuestData.Name} [ {QuestData.ID}]");
        switch (map)
        {
            case "shadowbattleon":

                Core.EquipClass(ClassType.Solo);

                // Mega Shadow Hunt Medal
                Story.KillQuest(9422, "shadowbattleon", "Doomed Beast");
                // Early Autopsy
                Story.KillQuest(9423, "shadowbattleon", "Doomed Beast");
                // Given Life and Purpose
                Story.KillQuest(9424, "shadowbattleon", "Possessed Armor");
                // Adult Hatchling
                Story.KillQuest(9425, "shadowbattleon", "Ouro Spawn");
                // Solidified Light
                Story.KillQuest(9426, "shadowbattleon", "Tainted Wraith");
                Core.Logger($"{QuestData.Name} [ {QuestData.ID}] Unlocked! Onto the gains.");
                break;

            case "Default":
                //Example Case
                break;
        }
        Core.JumpWait();
        Army.waitForParty("Whitemap");
    }

}
