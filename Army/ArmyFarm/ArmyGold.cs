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
//cs_include Scripts/Story/DragonsOfYokai/CoreDOY.cs
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
    private CoreDOY CoreDOY = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "Army_Gold";
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

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.EquipClass(ClassType.Farm);
        //Adv.BestGear(GenericGearBoost.gold);
        Farm.ToggleBoost(BoostType.Gold);
        Bot.Lite.ReacceptQuest = true;

        switch ((int)mapname)
        {
            case 0:
            case 1:
                BGE(Bot.Config!.Get<Method>("mapname"));
                break;
            case 2:
                DWL();
                break;
            case 3:
                DWN();
                break;
            case 4:
                SCW();
                break;
            case 5:
                StreamWar();
                break;
            case 6:
            case 7:
            case 8:
                ShadowBattleon();
                break;
            case 9:
                HakuWar();
                break;

            case 10:
                PirateBloodWar();
                break;
        }

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



        while (!Bot.ShouldExit && Bot.Player.Gold < 999999999)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
    }

    public void DWL()
    {
        DWLN.DarkWarLegion();

        Army.AggroMonMIDs(1, 2, 3, 4, 5, 6, 7, 8);
        Army.AggroMonStart("darkwarlegion");
        Army.DivideOnCells("Enter", "r2", "r3");



        Core.RegisterQuests(8584, 8585, 8586, 8587); //Nation Badges 8584, Mega Nation Badges 8585, A Nation Defeated 8586, ManSlayer? More Like ManSLAIN 8587
                                                     // Army.SmartAggroMonStart("darkwarlegion", "Bloodfiend", "Dreadfiend", "Infernal Fiend", "Manslayer Fiend", "Void Fiend");



        while (!Bot.ShouldExit && Bot.Player.Gold < 999999999)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
    }

    public void DWN()
    {
        DWLN.DarkWarNation();

        Core.RegisterQuests(8578, 8579, 8580, 8581); //Legion Badges, Mega Legion Badges, Doomed Legion Warriors, Undead Legion Dread

        Army.AggroMonMIDs(1, 2, 3, 4, 5, 6, 7, 8);
        Army.AggroMonStart("darkwarnation");
        Army.DivideOnCells("Enter", "r2", "r3");



        // Army.SmartAggroMonStart("darkwarnation", "High Legion Inquisitor", "Legion Doomknight", "Legion Dread Knight");
        while (!Bot.ShouldExit && Bot.Player.Gold < 999999999)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
    }

    public void SCW()
    {
        SC.CirclesWar(true);

        Core.RegisterQuests(7979, 7980, 7981);

        Army.AggroMonMIDs(1, 2, 3, 4, 5, 6);
        Army.AggroMonStart("sevencircleswar");
        Army.DivideOnCells("Enter", "r2", "r3");



        // Army.SmartAggroMonStart("sevencircleswar", "Wrath Guard", "Heresy Guard", "Violence Guard", "Treachery Guard");
        while (!Bot.ShouldExit && Bot.Player.Gold < 999999999)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
    }

    public void StreamWar()
    {
        Core.AddDrop("Prismatic Seams");

        SoW.TimestreamWar();

        Army.AggroMonMIDs(8, 9, 10, 11, 12, 13);
        Army.AggroMonStart("streamwar");
        Army.DivideOnCells("r3a");

        Core.RegisterQuests(8814, 8815);



        while (!Bot.ShouldExit && Bot.Player.Gold < 999999999)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();

        Core.ToBank("Prismatic Seams");
    }

    public void ShadowBattleon()
    {
        RequiredQuest("shadowbattleon", 9426);

        Bot.Lite.ReacceptQuest = true;
        Core.EquipClass(ClassType.Farm);
        Core.AddDrop("Wisper");
        Core.RegisterQuests(9421, 9422, 9426);

        if (Bot.Config!.Get<Method>("mapname") == Method.ShadowBattleon_Baby_Mode)
            Core.RegisterQuests(9421, 9422, 9423);
        else
            Core.RegisterQuests(9421, 9422, 9426);

        Core.Logger($"Mode Selected: {Bot.Config!.Get<Method>("mapname")}");

        if (Bot.Config!.Get<Method>("mapname") == Method.ShadowBattleon_High_Levels)
        {
            Army.AggroMonCells("r11", "r12");
            Army.AggroMonStart("shadowbattleon");
            Army.DivideOnCells("r11", "r12");
        }
        if (Bot.Config!.Get<Method>("mapname") == Method.ShadowBattleon_Lower_Levels)
        {
            Army.AggroMonCells("r11");
            Army.AggroMonStart("shadowbattleon");
            Army.DivideOnCells("r11");
        }
        else if (Bot.Config!.Get<Method>("mapname") == Method.ShadowBattleon_Baby_Mode)
        {
            Army.AggroMonCells("Enter");
            Army.AggroMonStart("shadowbattleon");
            Army.DivideOnCells("Enter");
        }

        Core.Logger("This method is insane atm.. if the rate is ever complete sh*t please use SCW");



        while (!Bot.ShouldExit && Bot.Player.Gold < 999999999)
            Bot.Combat.Attack("*");

        Bot.Options.AttackWithoutTarget = false;
        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.JumpWait();
    }

    public void HakuWar()
    {
        Quest Quest = Core.EnsureLoad(9601);
        if (Quest.XP < 6000)
        {
            Core.Logger("XP rates have been nerfed, swapping to method: SCW (its better)");
            SCW();
        }
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        CoreDOY.DoAll();
        Core.RegisterQuests(9601, 9602, 9603, 9605, 9606);

        Core.EquipClass(ClassType.Farm);
        Army.AggroMonMIDs(4, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 25, 26, 27);
        // Army.AggroMonCells("r2", "r4", "r5", "r6", "r7", "r9");
        Army.AggroMonStart("hakuwar");
        Army.DivideOnCells("r2", "r4", "r5", "r6", "r7", "r9");



        while (!Bot.ShouldExit && Bot.Player.Gold < 999999999)
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
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

            case "hakuwar":
                CoreDOY.DoAll();
                break;

            case "Default":
                //Example Case
                break;
        }
        Core.JumpWait();
    }

    void PirateBloodWar()
    {
        Quest? WarQuest = Bot.Quests.EnsureLoad(9873);

        if (WarQuest != null)
        {
            if (WarQuest.Gold < 6000 || !Core.isSeasonalMapActive("piratebloodhub"))
                SCW();
        }
        else
        {
            Core.Logger("Failed to load quest 9873.");
        }
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.RegisterQuests(9872, 9873);

        Army.AggroMonMIDs(Core.FromTo(1, 6));
        Army.AggroMonStart("piratelycan");
        Army.DivideOnCells("r2", "r3");

        while (!Bot.ShouldExit && Bot.Player.Gold < 999999999)
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
        SevenCirclesWar = 4,
        StreamWar = 5,
        ShadowBattleon_Baby_Mode = 6,
        ShadowBattleon_Lower_Levels = 7,
        ShadowBattleon_High_Levels = 8,
        Haku_War = 9,
        Pirate_Blood_War = 10

    }
}
