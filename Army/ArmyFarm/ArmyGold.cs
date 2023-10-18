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
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
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
    public CoreStory Story = new();
    private CoreArmyLite Army = new();
    private DarkWarLegionandNation DWLN = new();
    public SevenCircles SC = new();
    private CoreSoW SoW = new();

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

        Setup(Bot.Config!.Get<Method>("mapname"));

        Core.SetOptions(false);
    }

    public void Setup(Method mapname)
    {
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Core.EquipClass(ClassType.Farm);
        //Adv.BestGear(GenericGearBoost.gold);
        Farm.ToggleBoost(BoostType.Gold);
        Bot.Lite.ReacceptQuest = true;

        if (((int)mapname == 0) || ((int)mapname == 1))
            BGE(Bot.Config!.Get<Method>("mapname"));
        else if (((int)mapname == 2))
            DWL();
        else if (((int)mapname == 3))
            DWN();
        else if (((int)mapname == 4))
            SCW();
        else if (((int)mapname == 5))
            StreamWar();
        Bot.Lite.ReacceptQuest = false;
    }

    public void BGE(Method mapname)
    {
        // 1 = bge, 0 = HH
        if ((int)mapname == 0 && Bot.Player.Level <= 60)
            Core.Logger("Minimum level 61 required for this map", messageBox: true, stopBot: true);

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.RegisterQuests(Core.IsMember ? new[] { 3991, 3992, 3993 } : new[] { 3991, 3992 });

        Army.AggroMonMIDs(1, 2, 3, 4, 5, 6);
        Army.AggroMonStart("battlegrounde");
        Army.DivideOnCells("r4", "r3", "r2", "r1");


        while (!Bot.ShouldExit && Bot.Player.Gold < 100000000)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
        Army.waitForParty("whitemap");
    }

    public void DWL()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        DWLN.DarkWarLegion();

        Army.AggroMonMIDs(1, 2, 3, 4, 5, 6, 7, 8);
        Army.AggroMonStart("darkwarlegion");
        Army.DivideOnCells("Enter", "r2", "r3");

        Core.RegisterQuests(8584, 8585, 8586, 8587); //Nation Badges 8584, Mega Nation Badges 8585, A Nation Defeated 8586, ManSlayer? More Like ManSLAIN 8587
        // Army.SmartAggroMonStart("darkwarlegion", "Bloodfiend", "Dreadfiend", "Infernal Fiend", "Manslayer Fiend", "Void Fiend");
        while (!Bot.ShouldExit && Bot.Player.Gold < 100000000)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
        Army.waitForParty("whitemap");
    }

    public void DWN()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        DWLN.DarkWarNation();

        Core.RegisterQuests(8578, 8579, 8580, 8581); //Legion Badges, Mega Legion Badges, Doomed Legion Warriors, Undead Legion Dread

        Army.AggroMonMIDs(1, 2, 3, 4, 5, 6, 7, 8);
        Army.AggroMonStart("darkwarnation");
        Army.DivideOnCells("Enter", "r2", "r3");

        // Army.SmartAggroMonStart("darkwarnation", "High Legion Inquisitor", "Legion Doomknight", "Legion Dread Knight");
        while (!Bot.ShouldExit && Bot.Player.Gold < 100000000)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
        Army.waitForParty("whitemap");
    }

    public void SCW()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        SC.CirclesWar(true);

        Core.RegisterQuests(7979, 7980, 7981);

        Army.AggroMonMIDs(1, 2, 3, 4, 5, 6);
        Army.AggroMonStart("sevencircleswar");
        Army.DivideOnCells("Enter", "r2", "r3");

        // Army.SmartAggroMonStart("sevencircleswar", "Wrath Guard", "Heresy Guard", "Violence Guard", "Treachery Guard");
        while (!Bot.ShouldExit && Bot.Player.Gold < 100000000)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
        Army.waitForParty("whitemap");
    }

    public void StreamWar()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.AddDrop("Prismatic Seams");

        SoW.TimestreamWar();

        Army.AggroMonMIDs(8, 9, 10, 11, 12, 13);
        Army.AggroMonStart("streamwar");
        Army.DivideOnCells("r3a");

        Core.RegisterQuests(8814, 8815);
        while (!Bot.ShouldExit && Bot.Player.Gold < 100000000)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();

        Core.ToBank("Prismatic Seams");
        Army.waitForParty("whitemap");
    }


    private void ShadowBattleon()
    {
        RequiredQuest("shadowbattleon", 9426);
        Core.EquipClass(ClassType.Farm);
        Core.AddDrop("Wisper");
        Core.RegisterQuests(Core.isCompletedBefore(9426) ? new[] { 9421, 9422, 9426 } : new[] { 9421, 9422 });

        Army.waitForParty("shadowbattleon");

        Army.AggroMonCells("r11", "r12");
        Army.AggroMonStart("shadowbattleon");
        Army.DivideOnCells("r11", "r12");

        while (!Bot.ShouldExit && Bot.Player.Gold < 100000000)
            Bot.Combat.Attack("*");


        Army.AggroMonStop(true);
        Core.JumpWait();
        Core.ToBank("Wisper");
        Army.waitForParty("whitemap");
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
    }

    public enum Method
    {
        BattleGroundE = 0,
        HonorHall = 1,
        DarkWarLegion = 2,
        DarkWarNation = 3,
        SevenCirclesWar = 4,
        StreamWar = 5
    }
}
