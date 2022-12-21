//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Army/Rep/ArmyAegisRep.cs
//cs_include Army/Rep/ArmyArcangroveRep.cs
//cs_include Army/Rep/ArmyBaconCatRep.cs
//cs_include Army/Rep/ArmyBrethwrenRep.cs
//cs_include Army/Rep/ArmyChaosMilitiaRep.cs
//cs_include Army/Rep/ArmyChaosRep.cs
//cs_include Army/Rep/ArmyChronoSpanRep.cs
//cs_include Army/Rep/ArmyCraggleRockRep.cs
//cs_include Army/Rep/ArmyDoomWoodRep.cs
//cs_include Army/Rep/ArmyDreadfireRep.cs
//cs_include Army/Rep/ArmyDreadrockRep.cs
//cs_include Army/Rep/ArmyDruidGroveRep.cs
//cs_include Army/Rep/ArmyDwarfholdRep.cs
//cs_include Army/Rep/ArmyElementalMasterRep.cs
//cs_include Army/Rep/ArmyEmberseaRep.cs
//cs_include Army/Rep/ArmyEternalRep.cs
//cs_include Army/Rep/ArmyEtherstormRep.cs
//cs_include Army/Rep/ArmyGoodEvilRep.cs
//cs_include Army/Rep/ArmyHollowbornRep.cs
//cs_include Army/Rep/ArmyInfernalArmyRep.cs
//cs_include Army/Rep/ArmyLoremasterRep.cs
//cs_include Army/Rep/ArmyLycanRep.cs
//cs_include Army/Rep/ArmyMonsterHunterRep.cs
//cs_include Army/Rep/ArmyMysteriousDungeonRep.cs
//cs_include Army/Rep/ArmyMythsongRep.cs
//cs_include Army/Rep/ArmyNecroCryptRep.cs
//cs_include Army/Rep/ArmyNorthpoienteRep.cs
//cs_include Army/Rep/ArmyRavenlossRep.cs
//cs_include Army/Rep/ArmySandseaRep.cs
//cs_include Army/Rep/ArmySomniaRep.cs
//cs_include Army/Rep/ArmySwordhavenRep.cs
//cs_include Army/Rep/ArmyTreasureHunterRep.cs
//cs_include Army/Rep/ArmyTrollRep.cs
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
        bot.Options.RestPackets = false;

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