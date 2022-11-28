//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmyLowTideMergeReqs
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    public CoreArmyLite Army = new();

    public static CoreBots sCore = new();
    public static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyLowTideMergeReqs";
    public bool DontPreconfigure = true;
    CancellationTokenSource cts = new();
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
        Core.BankingBlackList.AddRange(Loot);

        Core.SetOptions();
        bot.Options.RestPackets = false;

        Setup();

        Core.SetOptions(false);
    }

    public void Setup()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        Army.AggroMonPacketDelay = Bot.Config.Get<int>("PacketDelay");

        Core.AddDrop(Loot);
        Core.EquipClass(ClassType.Farm);

        Army.AggroMonMIDs(5, 6, 7, 8, 9, 10);
        Army.AggroMonStart("lowtide");
        Army.DivideOnCells("r4", "r5", "r6");

        Core.RegisterQuests(8846);
        while (!Bot.ShouldExit)
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }

    private string[] Loot = { "Evidence Tag" };
}