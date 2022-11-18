//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ArmyAegisRep
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyAegisRep";
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
        sCore.SkipOptions
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
        if (Farm.FactionRank("Aegis") >= 10)
            return;

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(4900, 4910, 4914); //Kick Some Can 4900, The Best You Can Buy 4910, Testing My Metal 4914
        Farm.ToggleBoost(BoostType.Reputation);
        Army.SmartAggroMonStart("skytower", "Seraphic Assassin", "Virtuous Warrior", "Seraphic Assassin", "Virtuous Warrior");
        while (!Bot.ShouldExit && Farm.FactionRank("Aegis") < 10)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Reputation, false);
        Core.CancelRegisteredQuests();
    }
}