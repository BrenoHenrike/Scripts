
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class ArmyPrinceDarkonsPoleaxeMats
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyPrinceDarkonsPoleaxeMats";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        new Option<string>(
            "ClassToUse",
            "your class",
            "class to use",
            "classsss"
        ),
        new Option<string>(
            "SafeClass",
            "your safe class",
            "any class that not used in this bot",
            "classsss"
        ),
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {

        Core.SetOptions(disableClassSwap: false);

        Army.initArmy();
        Army.setLogName(OptionsStorage);
        ArmyHunt("arcangrove", new[] { "Right", "LeftBack" }, "Darkon's Receipt", 7324, 22);
        ArmyHunt("astravia", new[] { "r6", "r7", "r8" }, "La's Gratitude", 8001, 22);
        ArmyHunt("eridani", new[] { "r4", "r5", "r3", "r8" }, "Teeth", 7780, 22);
        ArmyHunt("astraviacastle", new[] { "r3", "r6", "r11" }, "Astravian Medal", 8257, 22);
        ArmyHunt("astraviajudge", new[] { "r2", "r3", "r11" }, "A Melody", 8396, 22);

        Core.SetOptions(false);
    }


    void ArmyHunt(string map, string[] cells, string item, int questId, int quant = 1)
    {
        string? safeClass = Bot.Config!.Get<string>("SafeClass");

        if (!string.IsNullOrEmpty(safeClass))
            Core.Equip(safeClass);
        else
            Core.Logger("SafeClass configuration is missing or empty.");

        Army.registerMessage(item);
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.BankingBlackList.Add(item);
        Core.AddDrop(item);
        if (map.ToLower() == "eridani")
        {
            Core.AddDrop("Tooth");
            Core.AddDrop("Wisdom Tooth");
        }

        Bot.Sleep(1000);
        string? classToUse = Bot.Config.Get<string>("ClassToUse");

        if (!string.IsNullOrEmpty(classToUse))
            Core.Equip(classToUse);
        else
            Core.Logger("ClassToUse configuration is missing or empty.");

        //Core.EquipClass(classType);
        Core.Join(map);
        //Army.waitForPartyCell("Enter", "Spawn");
        Core.RegisterQuests(questId);
        Army.waitForSignal("imready");

        Army.DivideOnCellsPriority(cells, priorityCell: "", setAggro: true);
        Army.registerMessage(item, false);

        // Army.AggroMonCells(cells);
        // Army.AggroMonStart(map);
        // Army.DivideOnCells(cells);

        Core.FarmingLogger(item, quant);

        Core.Logger($"army: starting {quant} {item}");
        Army.AggroMonStart();
        Army.StartFarm(item, quant);

        Core.CancelRegisteredQuests();
        Army.AggroMonStop(true);
        Core.Jump(Bot.Player.Cell, Bot.Player.Pad);
        Core.ToBank(item);
        Core.Logger($"everyone has finished {quant} {item}");
    }
}
