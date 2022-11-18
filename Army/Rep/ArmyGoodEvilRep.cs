//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ArmyGoodEvilREP
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyGoodEvilREP";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions,
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        Bot.Options.RestPackets = false;

        Setup();

        Core.SetOptions(false);
    }

    public void Setup(int rank = 10)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.EquipClass(ClassType.Farm);
        Farm.ToggleBoost(BoostType.Reputation);

        while (!Bot.ShouldExit && Farm.FactionRank("Evil") < 4)
            rank4();

        while (!Bot.ShouldExit && Farm.FactionRank("Good") < 4)
            rank4();
        
        while (!Bot.ShouldExit && Farm.FactionRank("Good") < rank)
            rankMAX();

        while (!Bot.ShouldExit && Farm.FactionRank("Evil") < rank)
            rankMAX();
            
        Farm.ToggleBoost(BoostType.Reputation, false);
    }

    public void rank4()
    {
        Core.RegisterQuests(364, 369); //Youthanize 364, That Hero Who Chases Slimes 369
        Army.SmartAggroMonStart("swordhavenbridge", "Slimes");
            Bot.Combat.Attack("*");
    
        Army.AggroMonClear();
        Core.CancelRegisteredQuests();
    }

    public void rankMAX()
    {
        Core.RegisterQuests(367, 372); //Bone-afide 367, Tomb with a View 372
        Army.SmartAggroMonStart("castleundead", "Skeletal Viking", "Skeletal Warrior");
        while (!Bot.ShouldExit && (Farm.FactionRank("Good") < 10 && Farm.FactionRank("Evil") < 10))    
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }
}