/*
name: Army Aegis Rep
description: Farm reputation with your army. Faction: Aegis
tags: army, reputation, aegis
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Story/RavenlossSaga.cs
//cs_include Scripts/Story/PockeymogsStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Options;

public class CoreArmyRep
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    public Core13LoC LOC => new();
    public RavenlossSaga RavenlossSaga => new();
    public CoreDailies Dailies => new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "CoreArmyRep";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };


    public void ScriptMain(IScriptInterface Bot)
    {
        Core.RunCore();
    }

    public void ArmyAegisRep() => RunArmyRep("Aegis", "skytower", new[] { "r4", "r6", "r7" }, new[] { "r4", "r6", "r7" }, new[] { 4900, 4910, 4914 });
    public void ArmyArcangroveRep() => RunArmyRep("Arcangrove", "arcangrove", new[] { "Left", "Back", "Right", "LeftBack" }, new[] { "Left", "Back", "Right", "LeftBack" }, new[] { 794, 795, 796, 797, 798, 799, 800, 801 });
    public void ArmyBaconCatRep() => RunArmyRep("BaconCat", "baconcatlair", new[] { "r4" }, new[] { "r4" }, new[] { 5112, 5120 });
    public void ArmyDoomWoodRep() => RunArmyRep("DoomWood", "shadowfallwar", new[] { "Garden2", "Garden1", "Bonus" }, new[] { "Garden2", "Garden1", "Bonus" }, new[] { 1151, 1152, 1153 });
    public void ArmyCraggleRockRep() => RunArmyRep("CraggleRock", "wanders", new[] { "r2", "r3", "r5", "r7" }, new[] { "r2", "r3", "r5", "r7" }, new[] { 7277 });
    public void ArmyChaosMilitiaRep() => RunArmyRep("Chaos Militia", "citadel", new[] { "m1", "m5", "m9" }, new[] { "m1", "m5", "m9" }, new[] { 5775 });
    public void ArmyChaosRep() => RunArmyRep("Chaos", "mountdoomskull", new[] { "b1", "b2" }, new[] { "b1", "b2" }, new[] { 3594 });
    public void ArmyDreadfireRep() => RunArmyRep("Dreadfire", "dreadfire", new[] { "r7", "r13" }, new[] { "r7", "r13" }, new[] { 5695, 5696, 5697 });
    public void ArmyDreadrockRep() => RunArmyRep("Dreadrock", "dreadrock", new[] { "r3", "r8a" }, new[] { "r3", "r8a" }, new[] { 4863, 4862, 4865, 4868 });
    public void ArmyChronoSpanRep() => RunArmyRep("ChronoSpan", "thespan", new[] { "r4", "r3", "r2" }, new[] { "r4", "r3", "r2" }, new[] { 2204, 2205 });
    public void ArmyDruidGroveRep() => RunArmyRep("Druid Grove", "bloodtusk", new string[] { "r8", "r3", "r18" }, new string[] { "r8", "r3", "r18" }, new int[] { 3049 });
    public void ArmyDwarfholdRep() => RunArmyRep("Dwarfhold", "pines", new string[] { "Enter", "Mountain" }, new string[] { "Enter", "Mountain" }, new int[] { 320, 321 });
    public void ArmyElementalMasterRep() => RunArmyRep("Elemental Master", "gilead", new[] { "r8", "r3", "r4" }, new string[] { "r8", "r3", "r4" }, new int[] { 3050, 3298 });
    public void ArmyEtherstormRep() => RunArmyRep("Etherstorm", "etherwardes", new string[] { "Enter", "r3", "r2" }, new string[] { "Enter", "r3", "r2" }, new int[] { 1721 });
    public void ArmyEmberseaRep() => RunArmyRep("Embersea", "fireforge", new string[] { "r5", "r8", "r7" }, new string[] { "r5", "r8", "r7" }, new int[] { 4227, 4228, 4229 });
    public void ArmyEternalRep() => RunArmyRep("Eternal", "fourdpyramid", new[] { "r10", "r11" }, new[] { "r10", "r11" }, new int[] { 5198, 5208 });
    public void ArmyGoodEvilRep() { int goodRank = FactionRank("Good"); int evilRank = FactionRank("Evil"); string repname = "Good"; string AggroMonStart = goodRank < 4 || evilRank < 4 ? "castleundead" : "swordhavenbridge"; string[] Cells = goodRank < 4 || evilRank < 4 ? new[] { "Bridge" } : new string[] { "Enter", "Bright", "Hall" }; int[] quests = goodRank < 4 || evilRank < 4 ? new[] { 364, 369 } : new int[] { 367, 372 }; RunArmyRep(repname, AggroMonStart, Cells, Cells, quests); }
    public void ArmyHollowbornRep() => RunArmyRep("Hollowborn", "shadowrealm", new[] { "r2", "r4", "r6" }, new[] { "r2", "r4", "r6" }, new int[] { 7553, 7555 });
    public void ArmyInfernalArmyRep() => RunArmyRep("Infernal Army", "dreadfire", new[] { "r10", "r10a", "r10b" }, new[] { "r10", "r10a", "r10b" }, new int[] { 5707, 5708, 5709 });
    public void ArmyMythsongRep() => RunArmyRep("Mythsong", "beehive", new[] { "r1", "r2", "r3", "r4" }, new string[] { "r1", "r2", "r3", "r4" }, new int[] { 4829 });
    public void ArmyMysteriousDungeonRep() => RunArmyRep("Mysterious Dungeon", "wanders", new[] { "r2", "r3", "r5", "r7" }, new[] { "r2", "r3", "r5", "r7" }, new[] { 5429, 5430, 5431, 5432 });
    public void ArmyNecroCryptRep() => RunArmyRep("Necro Crypt", "castleundead", new[] { "Enter", "Bright", "Hall" }, new[] { "Enter", "Bright", "Hall" }, new[] { 3048 });
    public void ArmyNorthpointeRep() => RunArmyRep("Northpointe", "Northpointe", new[] { "r13", "r11", "r8" }, new[] { "r13", "r11", "r8" }, new[] { 4027, 4026 });
    public void ArmyRavenlossRep() => RunArmyRep("RavenLoss", "twilightedge", new[] { "r2", "r3", "r4" }, new[] { "r2", "r3", "r4" }, new[] { 3445 });
    public void ArmySandseaRep() => RunArmyRep("Sandsea", "Sandsea", new[] { "Enter", "r6", "r7", "r8" }, new[] { "Enter", "r6", "r7", "r8" }, new[] { 916, 917, 919, 921, 922 });
    public void ArmySomniaRep() => RunArmyRep("Somnia", "Somnia", new[] { "r8", "r4", "r7" }, new[] { "r8", "r4", "r7" }, new[] { 7665, 7666, 7669 });
    public void ArmySwordhavenRep() => RunArmyRep("Swordhaven", "Swordhaven", new[] { "r4", "r3", "r5" }, new[] { "r4", "r3", "r5" }, new[] { 3065, 3066, 3067, 3070, 3085, 3086, 3087 });
    public void ArmyTreasureHunterRep() => RunArmyRep("TreasureHunter", "stalagbite", new[] { "Enter", "r1" }, new[] { "Enter", "r1" }, new[] { 6593 });
    public void ArmyTrollRep() => RunArmyRep("Troll", "bloodtuskwar", new[] { "r6", "r3", "r2" }, new[] { "r6", "r3", "r2" }, new[] { 1263 });
    public void ArmyLoremasterRep() => RunArmyRep("Loremaster", Core.IsMember ? "druids" : "wardwarf", new[] { Core.IsMember ? "r5" : "r2", Core.IsMember ? "r5" : "r4" }, new[] { Core.IsMember ? "r5" : "r2", Core.IsMember ? "r5" : "r4" }, Core.IsMember ? new[] { 3032 } : new[] { 7505 });
    public void ArmyLycanRep() => RunArmyRep("Lycan", "Lycan", new[] { "r4", "r5" }, new[] { "r4", "r5" }, new[] { 537 });
    public void ArmyMonsterHunterRep() => RunArmyRep("Monster Hunter", "pilgrimage", new[] { "r5", "r7", "r8", "r9" }, new[] { "r5", "r7", "r8", "r9" }, new[] { 5849, 5850 });
    public void ArmySkyeRep() => RunArmyRep("Skye", "balemorale", new[] { "r2", "r4", "r6", "r7", "r9", "r10" }, new[] { "r2", "r4", "r6", "r7", "r9", "r10" }, new[] { 9709, 9710, 9711, 9717 });
    #region Time of year restricted
    public void ArmyBrethwrenRep()
    {
        Core.Logger("Currently waiting for the mapto be avaible to fix this");
    } // redo this when the map is available.
      // public void ArmyBrethwrenRep() => RunArmyRep("Brethwren", "birdswithharms", new[] { 1 }, new[] { "Turkonian" }, new[] { 8989 }); // redo this when the map is available.
    #endregion Time of year restricted


    public void DoAll()
    {
        ArmyAegisRep();
        ArmyArcangroveRep();
        ArmyBaconCatRep();
        ArmyBrethwrenRep();
        ArmyChaosMilitiaRep();
        ArmyChaosRep();
        ArmyChronoSpanRep();
        ArmyCraggleRockRep();
        ArmyDoomWoodRep();
        ArmyDreadfireRep();
        ArmyDreadrockRep();
        ArmyDruidGroveRep();
        ArmyDwarfholdRep();
        ArmyElementalMasterRep();
        ArmyEmberseaRep();
        ArmyEternalRep();
        ArmyEtherstormRep();
        ArmyGoodEvilRep();
        ArmyHollowbornRep();
        ArmyInfernalArmyRep();
        ArmyLoremasterRep();
        ArmyLycanRep();
        ArmyMonsterHunterRep();
        ArmyMysteriousDungeonRep();
        ArmyMythsongRep();
        ArmyNecroCryptRep();
        ArmyNorthpointeRep();
        ArmyRavenlossRep();
        ArmySandseaRep();
        ArmySomniaRep();
        ArmySwordhavenRep();
        ArmyTreasureHunterRep();
        ArmyTrollRep();
        ArmySkyeRep();
    }
    void RunArmyRep(string repname, string AggroMonStart, string[] AggroMonCells, string[] DivideOnCells, int[] RegisterQuests)
    {
        Core.DL_Enable();
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        switch (repname)
        {
            case "Mythsong":
                LOC.Kimberly();
                break;

            case "RavenLoss":
                RavenlossSaga.TwilightEdge();
                break;

            case "Monster Hunter":
                if (!Bot.Quests.IsAvailable(5850))
                {
                    Core.EnsureAccept(5849);
                    Core.KillMonster("pilgrimage", "r5", "Left", "SpiderWing", "Spiderwing Captured", 4, log: false);
                    Core.KillMonster("pilgrimage", "r5", "Left", "Urstrix", "Urstrix Captured", 4, log: false);
                    Core.EnsureComplete(5849);
                }
                break;

            case "Skye":
                //story goes here .. vv then the dailies
                if (!Core.isCompletedBefore(9125))
                {
                    Core.Logger("Quest \"Your Hero [9125]\" Not complete (you have to do this yourself), cannot continue the rep", stopBot: true);
                    return;
                }

                foreach (int Q in new[] { 9713, 9714 })
                    if (!Dailies.CheckDailyv2(Q))
                        Core.EnsureAccept(Q);
                break;

            // Add more cases for other faction-specific actions as needed.
            default:
                // Handle the default case if needed.
                break;
        }

        Farm.ToggleBoost(BoostType.Reputation);
        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(RegisterQuests);

        Army.AggroMonCells(AggroMonCells);
        Army.AggroMonStart(AggroMonStart);
        Army.DivideOnCells(DivideOnCells);

        (string, string) Dividedcell = (Bot.Player.Cell, Bot.Player.Pad);
        Bot.Player.SetSpawnPoint();

        while (!Bot.ShouldExit && FactionRank(repname) < 10)
        {
            while (!Bot.ShouldExit && !Bot.Player.Alive)
                Bot.Wait.ForTrue(() => Bot.Player.Alive, 20);

            while (!Bot.ShouldExit && Bot.Player.Cell != Dividedcell.Item1)
            {
                Core.Jump(Dividedcell.Item1, Dividedcell.Item2);
                Bot.Wait.ForCellChange(Dividedcell.Item1);
                Core.Sleep();
            }

            Bot.Combat.Attack("*");
            Core.Sleep();
        }

        // Clean up
        Army.AggroMonStop(true);
        Core.JumpWait();
        Core.CancelRegisteredQuests();
        Farm.ToggleBoost(BoostType.Reputation, false);

        // Wait for the party
        //Army.waitForParty("whitemap");
    }
    public int FactionRank(string faction) => Bot.Reputation.GetRank(faction);
}
