//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmyBattlegroundE
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    public string OptionsStorage = "ArmyGoldExp";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        new Option<string>("player1", "Account #1", "Name of one of your accounts.", ""),
        new Option<string>("player2", "Account #2", "Name of one of your accounts.", ""),
        new Option<string>("player3", "Account #3", "Name of one of your accounts.", ""),
        new Option<string>("player4", "Account #4", "Name of one of your accounts.", ""),
        new Option<Method>("MapName", "BattleGroundE or HonorHall?", "BattleGroundE or HonorHall?", Method.BattleGroundE),
        new Option<int>("PacketDelay", "Delay for Packet Spam", "Sets the delay for the Packet Spam \n" +
        "Increase if spamming too much - Decrease if missing kills\n" +
        "Recommended setting: 500 or 1000)", 500),
        new Option<bool>("skipSetup", "Skip this window next time?", "You will be able to return to this screen via [Scripts] -> [Edit Script Options] if you wish to change anything.", false),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        if (!Bot.Config.Get<bool>("skipSetup"))
            Bot.Config.Configure();

        Core.SetOptions(disableClassSwap: true);
        bot.Options.RestPackets = false;

        Setup(Bot.Config.Get<Method>("MapName"));

        Core.SetOptions(false);
    }

    public void Setup(Method mapname)
    {
        Core.EquipClass(ClassType.Farm);
        if (mapname.ToString() == "BattleGroundE")
        {
            if (Bot.Player.Level <= 60)
                Core.Logger("Minimum level 61 required for this map", messageBox: true, stopBot: true);
            Core.Join("battlegrounde");
            battleground();
        }
        else
        {
            Core.Join("honorhall");
            honorhall();
        }


        void battleground()
        {
            if ((Bot.Player.Username == Bot.Config.Get<string>("player1").ToLower()))
                Core.Jump("r2", "Center");
            else if ((Bot.Player.Username == Bot.Config.Get<string>("player2").ToLower()))
                Core.Jump("r3", "Left");
            else if ((Bot.Player.Username == Bot.Config.Get<string>("player3").ToLower()))
                Core.Jump("r4", "Left");
            else if ((Bot.Player.Username == Bot.Config.Get<string>("player4").ToLower()))
                Core.Jump("r5", "Left");
            else
                Core.Jump("r4", "Left");
            Core.RegisterQuests(3992);
            Army();
        }

        void honorhall()
        {
            if ((Bot.Player.Username == Bot.Config.Get<string>("player1").ToLower()))
                Core.Jump("r1", "Left");
            else if ((Bot.Player.Username == Bot.Config.Get<string>("player2").ToLower()))
                Core.Jump("r2", "Left");
            else if ((Bot.Player.Username == Bot.Config.Get<string>("player3").ToLower()))
                Core.Jump("r3", "Left");
            else if ((Bot.Player.Username == Bot.Config.Get<string>("player4").ToLower()))
                Core.Jump("r4", "Left");
            else
                Core.Jump("r1", "Left");
            Core.RegisterQuests(3993);
            Army();
        }

    }

    public void Army()
    {
        var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%1%2%3%4%5%6%", "String", Bot.Config.Get<int>("PacketDelay"), CancellationToken.None);
        while (!Bot.ShouldExit)
            Bot.Combat.Attack("*");
    }

    public enum Method
    {
        BattleGroundE = 0,
        HonorHall = 1
    }
}