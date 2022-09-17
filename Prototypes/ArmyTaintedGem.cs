//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmyTaintedGem
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    CancellationTokenSource cts = new();
    public bool DontPreconfigure = true;
    public string OptionsStorage = "ArmyTaintedGem";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<string>("player1", "Account #1", "Name of one of your accounts.", ""),
        new Option<string>("player2", "Account #2", "Name of one of your accounts.", ""),
        new Option<string>("player3", "Account #3", "Name of one of your accounts.", ""),
        new Option<string>("player4", "Account #4", "Name of one of your accounts.", ""),
        new Option<int>("PacketDelay", "Delay for Packet Spam", "Sets the delay for the Packet Spam \n" +
        "Increase if spamming too much (disconnect) - Decrease if missing kills\n" +
        "Recommended setting: 100 to 500)", 100),
        new Option<bool>("skipSetup", "Skip this window next time?", "You will be able to return to this screen via [Scripts] -> [Edit Script Options] if you wish to change anything.", false)
    };

    public string[] Loot =
    {
        "Cubes",
        "Tainted Gem",
        "Receipt of Swindle"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        if (!Bot.Config.Get<bool>("skipSetup"))
            Bot.Config.Configure();

        Bot.Events.PlayerAFK += PlayerAFK;
        Core.BankingBlackList.AddRange(Loot);

        Core.SetOptions();

        TaintedGem();

        Core.SetOptions(false);
    }

    public void TaintedGem(int quant = 1000)
    {
        if (Core.CheckInventory("Tainted Gem", quant))
            return;

        Core.AddDrop(Loot);
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger($"Tainted Gem", quant);
        while (!Bot.ShouldExit && !Core.CheckInventory("Tainted Gem", quant))
        {
            Core.EnsureAccept(7817);
            boxes();
            mountfrost();
            Bot.Sleep(1500);
            Core.EnsureComplete(7817);
        }
    }
    public void Setup()
    {
        if (Bot.Map.Name == "boxes")
        {
            if (Bot.Player.Username == Bot.Config.Get<string>("player1").ToLower())
                Core.Jump("Fort2", "Center");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player2").ToLower())
                Core.Jump("Closet", "Center");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player3").ToLower())
                Core.Jump("Fort1", "Center");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player4").ToLower())
                Core.Jump("Boss", "Center");
            else
                Core.Jump("Boss", "Center");
        }
        else if (Bot.Map.Name == "mountfrost")
        {
            if (Bot.Player.Username == Bot.Config.Get<string>("player1").ToLower())
                Core.Jump("War", "Left");
            else
                Core.Jump("War", "Left");
        }
        else
            Core.Logger("Current map not found", messageBox: true, stopBot: true);
    }

    public void boxes()
    {
        Core.Join("boxes");
        Setup();
        var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%118891%4%5%8%2%3%1%11%12%13%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Cubes", 500)))
            Bot.Combat.Attack("*");
        cts.Cancel();
        Core.JumpWait();
        Bot.Sleep(2000);
    }

    public void mountfrost()
    {
        Core.Join("mountfrost");
        Setup();
        var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%119011%1%2%3%4%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Ice Cubes", 6)))
            Bot.Combat.Attack("*");
        cts.Cancel();
        Core.JumpWait();
        Bot.Sleep(2000);
    }

    public void PlayerAFK()
    {
        Core.Logger("Anti-AFK engaged");
        Bot.Sleep(1500);
        Bot.Send.Packet("%xt%zm%afk%1%false%");
    }
}