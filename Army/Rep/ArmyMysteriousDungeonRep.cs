//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ArmyMysteriousDungeonRep
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyMysteriousDungeonRep";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6, //adjust if needed, check maps limit on wiki
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
        if (Farm.FactionRank("Mysterious Dungeon") >= 10)
            return;

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.EquipClass(ClassType.Farm);
        if (!Bot.Quests.IsAvailable(5429))
        {
            Core.Join("cursedshop");
            Core.EnsureAccept(5428);
            Bot.Map.GetMapItem(4803);
            Bot.Sleep(2500);
            if (Bot.Quests.CanComplete(5428))
                Core.EnsureComplete(5428);
            Bot.Map.Jump("Enter", "Spawn");
        }
        Core.RegisterQuests(5429, 5430, 5431, 5432); //Lamps, Paintings and Chairs, oh my! 5429, The (Un)Dresser 5430, Ghost Stories 5431, You Can't Tell  Time 5432
        Farm.ToggleBoost(BoostType.Reputation);
        Army.SmartAggroMonStart("cursedshop", "Antique Chair", "UnDresser", "Writing Desk", "Grandfather Clock");
        while (!Bot.ShouldExit && Farm.FactionRank("Mysterious Dungeon") < 10)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Reputation, false);
        Core.CancelRegisteredQuests();
    }
}