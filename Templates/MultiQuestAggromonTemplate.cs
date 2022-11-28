//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class MultiQuestAggromonTemplate  //<-- replace
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "RenameME"; //<-- replace
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        new Option<int>("armysize", "Players", "Input the minimum of players to wait for 1-6", 1),
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
        Core.BankingBlackList.AddRange(new[]
        {"all the item names here, to prevent them from beign banked."}); //<-- fill in stuffs

        Core.SetOptions();
        bot.Options.RestPackets = false;

        RenameME();

        Core.SetOptions(false);
    }

    public void RenameME()
    {
        Core.EquipClass(ClassType.Farm);
        Core.AddDrop("item", "item");

        while (!Bot.ShouldExit && !Core.CheckInventory(new[] { "item", "item", "etc" })) //<-- fill in stuffs
        {
            if (!Core.CheckInventory("item"))
                ArmyThing(0000, "map", new[] { "monster" }, "drop", isTemp: false); //<-- fill in stuffs
            if (!Core.CheckInventory("item"))                                       //<--               |
                ArmyThing(0000, "map", new[] { "monster" }, "drop", isTemp: false); //<--               |
            if (!Core.CheckInventory("item"))                                       //<--               |
                ArmyThing(0000, "map", new[] { "monster" }, "drop", isTemp: false); //<-- fill in stuffs
        }
    }

    void ArmyThing(int questID, string map = null, string[] monsters = null, string item = null, bool isTemp = false, int quant = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory(item, quant))
            return;

        if (item == null)
            return;

        Bot.Drops.Add(item);

        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger(item, quant);

        Core.Join(map);
        WaitCheck();
        Core.EnsureAccept(questID);
        Armyshit(map);


        foreach (string monster in monsters)
            Army.SmartAggroMonStart(map, monsters);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
        Bot.Wait.ForPickup(item);
        Core.EnsureComplete(questID);
    }

    void WaitCheck()
    {
        while (Bot.Map.PlayerCount < Bot.Config.Get<int>("armysize"))
        {
            Core.Logger($"Waiting for the squad. [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
            Bot.Sleep(5000);
        }
        Core.Logger($"Squad All Gathered [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
    }

    void Armyshit(string map = null)
    {
        if (Bot.Map.Name == null)
            return;

        if (Bot.Map.Name == "MapName") //<-- fill in stuffs
        {
            Army.AggroMonCells("r1", "r2", "r3"); //<-- replace the cells with the cells mobs are in.
            Army.AggroMonStart("MapName");
            Army.DivideOnCells("r1", "r2", "r3"); //<-- replace the cells with the cells mobs are in.
        }
    }
}