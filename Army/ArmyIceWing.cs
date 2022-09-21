//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class IceWingLevelingArmy
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreArmyLite Army = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "ArmyIceWing";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<int>("armysize","Players", "Input the minimum of players to wait for", 1),
        new Option<bool>("skipSetup", "Skip this window next time?", "You will be able to return to this screen via [Scripts] -> [Edit Script Options] if you wish to change anything.", false),
    };

    public int level = 75;

    public void ScriptMain(IScriptInterface bot)
    {
        if (!Bot.Config.Get<bool>("skipSetup"))
            Bot.Config.Configure();

        Core.SetOptions();

        ArmyIceWing();

        Core.SetOptions(false);
    }

    public void ArmyIceWing()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.RegisterQuests(Core.IsMember ? 6635 : 6632);
        while (!Bot.ShouldExit)
            KillIceWing("icestormarena", "r23", "Left", "*");
        Core.CancelRegisteredQuests();
    }

    public void KillIceWing(string map, string cell, string pad, string monster)
    {
        Core.Join(map, cell, pad);
        if (Bot.Player.Cell != cell)
        {
            if (Bot.Player.Level < level)
                Bot.Send.ClientPacket("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"levelUp\",\"intExpToLevel\":\"0\",\"intLevel\":100}}}", "json");
            Bot.Sleep(500);
            Core.Jump(cell, pad);
        }
        //while (!Bot.Map.PlayerExists((Bot.Config.Get<string>("playerName"))) || Bot.Map.GetPlayer((Bot.Config.Get<string>("playerName"))).Cell != Bot.Player.Cell)
        while ((cell != null && Bot.Map.CellPlayers.Count() > 0 ? Bot.Map.CellPlayers.Count() : Bot.Map.PlayerCount) < Bot.Config.Get<int>("armysize"))
        {
            Core.Logger($"Waiting for the squad. [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
            Bot.Sleep(2000);
        }
        Bot.Kill.Monster(monster);
    }
}
