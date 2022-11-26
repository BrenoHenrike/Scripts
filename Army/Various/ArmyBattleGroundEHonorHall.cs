//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ArmyBattlegroundE
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyGoldExp";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        new Option<Method>("mapname", "BGE or Honour Hall?", "Farm BGE or Honourhall(member)?", Method.BattleGroundE),
        sCore.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions(disableClassSwap: true);
        bot.Options.RestPackets = false;

        Setup(Bot.Config.Get<Method>("mapname"));

        Core.SetOptions(false);
    }

    public void Setup(Method mapname)
    {
        if ((int)mapname == 0 && Bot.Player.Level <= 60)
            Core.Logger("Minimum level 61 required for this map", messageBox: true, stopBot: true);

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.EquipClass(ClassType.Farm);
        Farm.ToggleBoost(BoostType.Gold);
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

    public enum Method
    {
        BattleGroundE = 0,
        HonorHall = 1
    }
}