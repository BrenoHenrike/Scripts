//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmyApprovalFavour
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    public string OptionsStorage = "ArmyApprovalFavour";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        new Option<string>("player1", "Account #1", "Name of one of your accounts.", ""),
        new Option<string>("player2", "Account #2", "Name of one of your accounts.", ""),
        new Option<string>("player3", "Account #3", "Name of one of your accounts.", ""),
        new Option<string>("player4", "Account #4", "Name of one of your accounts.", ""),
        new Option<string>("player5", "Account #5", "Name of one of your accounts.", ""),
        new Option<string>("player6", "Account #6", "Name of one of your accounts.", ""),
        new Option<bool>("skipSetup", "Skip this window next time?", "You will be able to return to this screen via [Scripts] -> [Edit Script Options] if you wish to change anything.", false),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        if (!Bot.Config.Get<bool>("skipSetup"))
            Bot.Config.Configure();

        Core.BankingBlackList.AddRange(Loot);

        Core.SetOptions();
        bot.Options.RestPackets = false;

        Setup();

        Core.SetOptions(false);
    }

    public string[] Loot = { "Archfiend's Favor", "Nulgath's Approval" };

    public void Setup()
    {
        Core.AddDrop(Loot);
        Core.EquipClass(ClassType.Farm);
        Core.Join("evilwarnul");
        if ((Bot.Player.Username == Bot.Config.Get<string>("player1").ToLower()))
            Core.Jump("r2", "Down");
        else if ((Bot.Player.Username == Bot.Config.Get<string>("player2").ToLower()))
            Core.Jump("r3", "Right");
        else if ((Bot.Player.Username == Bot.Config.Get<string>("player3").ToLower()))
            Core.Jump("r4", "Right");
        else if ((Bot.Player.Username == Bot.Config.Get<string>("player4").ToLower()))
            Core.Jump("r5", "Right");
        else if ((Bot.Player.Username == Bot.Config.Get<string>("player5").ToLower()) || (Bot.Player.Username == Bot.Config.Get<string>("player6").ToLower()))
            Core.Jump("r6", "Left");
        else
            Core.Jump("r2", "Down");
        Army();
    }

    public void Army()
    {
        while (!Bot.ShouldExit)
        {
            Bot.Combat.Attack("*");
            Packet();
        }
    }
    public void Packet()
    {
        if (Bot.Map.PlayerCount <= 3)
            Core.SendPackets("%xt%zm%aggroMon%1%1%2%3%4%5%6%7%8%9%");
        else
            Core.SendPackets("%xt%zm%aggroMon%1%1%2%3%4%5%6%7%8%9%10%11%12%13%14%15%");
    }
}