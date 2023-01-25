/*
name: Army All Rep
description: Farm reputation with your army. Faction: all reps in order.
tags: army, reputation, all reputation
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Seasonal/HarvestDay/CoreHarvestDay.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyAegisRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyArcangroveRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyBaconCatRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyBrethwrenRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyChaosMilitiaRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyChaosRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyChronoSpanRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyCraggleRockRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyDoomWoodRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyDreadfireRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyDreadrockRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyDruidGroveRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyDwarfholdRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyElementalMasterRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyEmberseaRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyEternalRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyEtherstormRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyGoodEvilRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyHollowbornRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyInfernalArmyRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyLoremasterRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyLycanRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyMonsterHunterRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyMysteriousDungeonRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyMythsongRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyNecroCryptRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyNorthpoienteRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyRavenlossRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmySandseaRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmySomniaRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmySwordhavenRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyTreasureHunterRep.cs
//cs_include Scripts/Army/ArmyFarm/Rep/ArmyTrollRep.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmyAllRep
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    private ArmyAegisRep ArmyAegisRep = new();
    private ArmyArcangroveRep ArmyArcangroveRep = new();
    private ArmyBaconCatRep ArmyBaconCatRep = new();
    private ArmyBrethwrenRep ArmyBrethwrenRep = new();
    private ArmyChaosMilitiaRep ArmyChaosMilitiaRep = new();
    private ArmyChaosRep ArmyChaosRep = new();
    private ArmyChronoSpanRep ArmyChronoSpanRep = new();
    private ArmyCraggleRockRep ArmyCraggleRockRep = new();
    private ArmyDoomWoodRep ArmyDoomWoodRep = new();
    private ArmyDreadfireRep ArmyDreadfireRep = new();
    private ArmyDreadrockRep ArmyDreadrockRep = new();
    private ArmyDruidGroveRep ArmyDruidGroveRep = new();
    private ArmyDwarfholdRep ArmyDwarfholdRep = new();
    private ArmyElementalMasterRep ArmyElementalMasterRep = new();
    private ArmyEmberseaRep ArmyEmberseaRep = new();
    private ArmyEternalRep ArmyEternalRep = new();
    private ArmyEtherstormRep ArmyEtherstormRep = new();
    private ArmyGoodEvilREP ArmyGoodEvilRep = new();
    private ArmyHollowbornRep ArmyHollowbornRep = new();
    private ArmyInfernalArmyRep ArmyInfernalArmyRep = new();
    private ArmyLoremasterRep ArmyLoremasterRep = new();
    private ArmyLycanRep ArmyLycanRep = new();
    private ArmyMonsterHunterRep ArmyMonsterHunterRep = new();
    private ArmyMysteriousDungeonRep ArmyMysteriousDungeonRep = new();
    private ArmyMythsongRep ArmyMythsongRep = new();
    private ArmyNecroCryptRep ArmyNecroCryptRep = new();
    private ArmyNorthpointeRep ArmyNorthpointeRep = new();
    private ArmyRavenlossRep ArmyRavenlossRep = new();
    private ArmySandseaRep ArmySandseaRep = new();
    private ArmySomniaRep ArmySomniaRep = new();
    private ArmySwordhavenRep ArmySwordhavenRep = new();
    private ArmyTreasureHunterRep ArmyTreasureHunterRep = new();
    private ArmyTrollRep ArmyTrollRep = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyAllRep";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
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

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Setup();

        Core.SetOptions(false);
    }

    public void Setup()
    {
        ArmyAegisRep.Setup();
        ArmyArcangroveRep.Setup();
        ArmyBaconCatRep.Setup();
        ArmyBrethwrenRep.Setup();
        ArmyChaosMilitiaRep.Setup();
        ArmyChaosRep.Setup();
        ArmyChronoSpanRep.Setup();
        ArmyCraggleRockRep.Setup();
        ArmyDoomWoodRep.Setup();
        ArmyDreadfireRep.Setup();
        ArmyDreadrockRep.Setup();
        ArmyDruidGroveRep.Setup();
        ArmyDwarfholdRep.Setup();
        ArmyElementalMasterRep.Setup();
        ArmyEmberseaRep.Setup();
        ArmyEternalRep.Setup();
        ArmyEtherstormRep.Setup();
        ArmyGoodEvilRep.Setup();
        ArmyHollowbornRep.Setup();
        ArmyInfernalArmyRep.Setup();
        ArmyLoremasterRep.Setup();
        ArmyLycanRep.Setup();
        ArmyMonsterHunterRep.Setup();
        ArmyMysteriousDungeonRep.Setup();
        ArmyMythsongRep.Setup();
        ArmyNecroCryptRep.Setup();
        ArmyNorthpointeRep.Setup();
        ArmyRavenlossRep.Setup();
        ArmySandseaRep.Setup();
        ArmySomniaRep.Setup();
        ArmySwordhavenRep.Setup();
        ArmyTreasureHunterRep.Setup();
        ArmyTrollRep.Setup();
    }
}
