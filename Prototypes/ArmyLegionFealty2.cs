//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmyLegionFealty2
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    CancellationTokenSource cts = new();
    public bool DontPreconfigure = true;
    public string OptionsStorage = "ArmyLegionFealty2";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<string>("player1", "Account #1", "Name of one of your accounts.", ""),
        new Option<string>("player2", "Account #2", "Name of one of your accounts.", ""),
        new Option<string>("player3", "Account #3", "Name of one of your accounts.", ""),
        new Option<string>("player4", "Account #4", "Name of one of your accounts.", ""),
        new Option<int>("armysize","Number of Accounts", "Input the number of players that it will be waiting for", 1),
        new Option<int>("PacketDelay", "Delay for Packet Spam", "Sets the delay for the Packet Spam \n" +
        "Increase if spamming too much - Decrease if missing kills\n" +
        "Recommended setting: 500 or 1000)", 500),
        new Option<bool>("skipSetup", "Skip this window next time?", "You will be able to return to this screen via [Scripts] -> [Edit Script Options] if you wish to change anything.", false)
    };

    public string[] LR2 =
    {
        "Conquest Wreath",
        "Grim Cohort Conquered",
        "Ancient Cohort Conquered",
        "Pirate Cohort Conquered",
        "Battleon Cohort Conquered",
        "Mirror Cohort Conquered",
        "Darkblood Cohort Conquered",
        "Vampire Cohort Conquered",
        "Spirit Cohort Conquered",
        "Dragon Cohort Conquered",
        "Doomwood Cohort Conquered"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        if (!Bot.Config.Get<bool>("skipSetup"))
            Bot.Config.Configure();

        Bot.Events.PlayerAFK += PlayerAFK;
        Core.BankingBlackList.AddRange(LR2);

        Core.SetOptions();

        LegionFealty2();

        Core.SetOptions(false);
    }

    public void LegionFealty2(int quant = 50)
    {
        if (Core.CheckInventory("Conquest Wreath", quant))
            return;

        Core.AddDrop(LR2);
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger($"Conquest Wreath", quant);
        while (!Bot.ShouldExit && !Core.CheckInventory("Conquest Wreath", quant))
        {
            Core.EnsureAccept(6898);
            doomvault();
            mummies();
            wrath();
            doomwar();
            overworld();
            deathpits();
            maxius();
            curseshore();
            dragonbone();
            doomwood();
            Bot.Sleep(1500);
            Core.EnsureComplete(6898);
        }
    }
    public void Setup()
    {
        if (Bot.Map.Name == "doomvault")
        {
            if (Bot.Player.Username == Bot.Config.Get<string>("player1").ToLower())
                Core.Jump("r1", "Right");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player2").ToLower())
                Core.Jump("r3", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player3").ToLower())
                Core.Jump("r24", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player4").ToLower())
                Core.Jump("r18", "Left");
            else
                Core.Jump("r1", "Right");
        }
        else if (Bot.Map.Name == "mummies")
        {
            if (Bot.Player.Username == Bot.Config.Get<string>("player1").ToLower())
                Core.Jump("Enter", "Spawn");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player2").ToLower())
                Core.Jump("r2", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player3").ToLower())
                Core.Jump("r3", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player4").ToLower())
                Core.Jump("r4", "bottom");
            else
                Core.Jump("Enter", "Spawn");
        }
        else if (Bot.Map.Name == "wrath")
        {
            if (Bot.Player.Username == Bot.Config.Get<string>("player1").ToLower())
                Core.Jump("r2", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player2").ToLower())
                Core.Jump("r3", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player3").ToLower())
                Core.Jump("r4", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player4").ToLower())
                Core.Jump("r5", "Left");
            else
                Core.Jump("r2", "Left");
        }
        else if (Bot.Map.Name == "doomwar")
        {
            if (Bot.Player.Username == Bot.Config.Get<string>("player1").ToLower())
                Core.Jump("r4", "Right");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player2").ToLower())
                Core.Jump("r10", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player3").ToLower())
                Core.Jump("r6", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player4").ToLower())
                Core.Jump("r8", "Left");
            else
                Core.Jump("r4", "Right");
        }
        else if (Bot.Map.Name == "doomwar")
        {
            if (Bot.Player.Username == Bot.Config.Get<string>("player1").ToLower())
                Core.Jump("r4", "Right");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player2").ToLower())
                Core.Jump("r10", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player3").ToLower())
                Core.Jump("r6", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player4").ToLower())
                Core.Jump("r8", "Left");
            else
                Core.Jump("r4", "Right");
        }
        else if (Bot.Map.Name == "overworld")
        {
            if (Bot.Player.Username == Bot.Config.Get<string>("player1").ToLower())
                Core.Jump("Enter", "Spawn");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player2").ToLower())
                Core.Jump("r2", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player3").ToLower())
                Core.Jump("r4", "Right");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player4").ToLower())
                Core.Jump("r5", "Up");
            else
                Core.Jump("Enter", "Spawn");
        }
        else if (Bot.Map.Name == "deathpits")
        {
            if (Bot.Player.Username == Bot.Config.Get<string>("player1").ToLower())
                Core.Jump("r1", "Right");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player2").ToLower())
                Core.Jump("r3", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player3").ToLower())
                Core.Jump("r4", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player4").ToLower())
                Core.Jump("r10", "Left");
            else
                Core.Jump("r1", "Right");
        }
        else if (Bot.Map.Name == "maxius")
        {
            if (Bot.Player.Username == Bot.Config.Get<string>("player1").ToLower() || (Bot.Player.Username == Bot.Config.Get<string>("player2").ToLower()))
                Core.Jump("r2", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player3").ToLower() || (Bot.Player.Username == Bot.Config.Get<string>("player4").ToLower()))
                Core.Jump("r4", "Left");
            else
                Core.Jump("r2", "Left");
        }
        else if (Bot.Map.Name == "curseshore")
        {
            if (Bot.Player.Username == Bot.Config.Get<string>("player1").ToLower() || (Bot.Player.Username == Bot.Config.Get<string>("player2").ToLower()))
                Core.Jump("Enter", "Spawn");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player3").ToLower() || (Bot.Player.Username == Bot.Config.Get<string>("player4").ToLower()))
                Core.Jump("r2", "Left");
            else
                Core.Jump("Enter", "Spawn");
        }
        else if (Bot.Map.Name == "dragonbone")
        {
            if (Bot.Player.Username == Bot.Config.Get<string>("player1").ToLower())
                Core.Jump("Enter", "Spawn");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player2").ToLower())
                Core.Jump("r2", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player3").ToLower() || (Bot.Player.Username == Bot.Config.Get<string>("player4").ToLower()))
                Core.Jump("r3", "Left");
            else
                Core.Jump("Enter", "Spawn");
        }
        else if (Bot.Map.Name == "doomwood")
        {
            if (Bot.Player.Username == Bot.Config.Get<string>("player1").ToLower())
                Core.Jump("r6", "Right");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player2").ToLower())
                Core.Jump("r3", "Left");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player3").ToLower())
                Core.Jump("r7", "Up");
            else if (Bot.Player.Username == Bot.Config.Get<string>("player4").ToLower())
                Core.Jump("r8", "Left");
            else
                Core.Jump("r6", "Right");
        }
        else
            Core.Logger("Current map not found", messageBox: true, stopBot: true);
        while (Bot.Map.PlayerCount < Bot.Config.Get<int>("armysize"))
        {
            Core.Logger($"Waiting for the squad. [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
            Bot.Sleep(1500);
        }
    }

    public void doomvault()
    {
        Core.Join("doomvault");
        Bot.Quests.UpdateQuest(3008);
        Setup();
        var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%1%2%3%6%7%8%30%31%32%33%38%39%40%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Grim Cohort Conquered", 500)))
            Bot.Combat.Attack("*");
        cts.Cancel();
        Core.JumpWait();
        Bot.Sleep(2000);
    }

    public void mummies()
    {
        Bot.Quests.UpdateQuest(4614);
        Core.Join("mummies");
        Setup();
        var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%1%2%3%4%5%6%7%8%9%10%11%12%13%14%15%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Ancient Cohort Conquered", 500)))
            Bot.Combat.Attack("*");
        cts.Cancel();
        Core.JumpWait();
        Bot.Sleep(2000);
    }

    public void wrath()
    {
        Core.Join("wrath");
        Setup();
        var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%1%2%3%4%5%6%7%8%9%20%10%11%12%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Pirate Cohort Conquered", 500)))
            Bot.Combat.Attack("*");
        cts.Cancel();
        Core.JumpWait();
        Bot.Sleep(2000);
    }

    public void doomwar()
    {
        Core.Join("doomwar");
        Setup();
        var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%7%8%9%25%26%27%13%14%15%19%20%21%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Battleon Cohort Conquered", 500)))
            Bot.Combat.Attack("*");
        cts.Cancel();
        Core.JumpWait();
        Bot.Sleep(2000);
    }

    public void overworld()
    {
        Core.Join("overworld");
        Setup();
        var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%1%2%3%4%5%6%10%11%12%13%14%15%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Mirror Cohort Conquered", 500)))
            Bot.Combat.Attack("*");
        cts.Cancel();
        Core.JumpWait();
        Bot.Sleep(2000);
    }

    public void deathpits()
    {
        Core.Join("deathpits");
        Setup();
        var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%1%2%3%6%7%8%9%10%11%23%24%25%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Darkblood Cohort Conquered", 500)))
            Bot.Combat.Attack("*");
        cts.Cancel();
        Core.JumpWait();
        Bot.Sleep(2000);
    }

    public void maxius()
    {
        Core.Join("maxius");
        Setup();
        var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%1%2%3%4%5%7%8%9%10%11%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Vampire Cohort Conquered", 500)))
            Bot.Combat.Attack("*");
        cts.Cancel();
        Core.JumpWait();
        Bot.Sleep(2000);
    }

    public void curseshore()
    {
        Core.Join("curseshore");
        Setup();
        var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%1%2%3%4%5%6%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Spirit Cohort Conquered", 500)))
            Bot.Combat.Attack("*");
        cts.Cancel();
        Core.JumpWait();
        Bot.Sleep(2000);
    }

    public void dragonbone()
    {
        Core.Join("dragonbone");
        Setup();
        var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%1%2%3%4%5%6%7%8%9%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Dragon Cohort Conquered", 500)))
            Bot.Combat.Attack("*");
        cts.Cancel();
        Core.JumpWait();
        Bot.Sleep(2000);
    }

    public void doomwood()
    {
        Core.Join("doomwood");
        Setup();
        var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%1%2%3%4%5%6%7%8%9%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Doomwood Cohort Conquered", 500)))
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