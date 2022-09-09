//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmyTotemAndGem
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public string OptionsStorage = "ArmyTotemAndGem";
    public bool DontPreconfigure = true;
    CancellationTokenSource cts = new();
    public int t = 0;
    public int g = 0;
    public List<IOption> Options = new List<IOption>()
    {
        new Option<string>("", "Don't use capitals", "Don't capitalize any names - won't work if you do."),
        new Option<string>("player1", "Account #1", "Name of one of your accounts.", ""),
        new Option<string>("player2", "Account #2", "Name of one of your accounts.", ""),
        new Option<string>("player3", "Account #3", "Name of one of your accounts.", ""),
        new Option<string>("player4", "Account #4", "Name of one of your accounts.", ""),
        new Option<string>("player5", "Account #5", "Name of one of your accounts.", ""),
        new Option<string>("player6", "Account #6", "Name of one of your accounts.", ""),
        new Option<int>("PacketDelay", "Delay for Packet Spam", "Sets the delay for the Packet Spam \n" +
        "Increase if spamming too much - Decrease if missing kills\n" +
        "Recommended setting: 500 or 1000)", 500),
        new Option<Rewards>("QuestReward", "Totems or Gems?", "Select the reward to farm first"),
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

    public string[] Loot = { "Totem of Nulgath", "Gem of Nulgath", "Essence of Nulgath" };

    public void Setup()
    {
        if (!Core.CheckInventory("Voucher of Nulgath (non-mem)"))
            Core.Logger("Voucher of Nulgath (non-mem) not found - go get one before running this", messageBox: true, stopBot: true);

        Core.AddDrop(Loot);
        Core.EquipClass(ClassType.Farm);
        Core.Join("tercessuinotlim");
        if ((Bot.Player.Username == Bot.Config.Get<string>("player1")) || (Bot.Player.Username == Bot.Config.Get<string>("player4")))
            Core.Jump("m1", "Left");
        else if ((Bot.Player.Username == Bot.Config.Get<string>("player2")) || (Bot.Player.Username == Bot.Config.Get<string>("player5")))
            Core.Jump("m2", "Left");
        else if ((Bot.Player.Username == Bot.Config.Get<string>("player3")) || (Bot.Player.Username == Bot.Config.Get<string>("player6")))
            Core.Jump("m3", "Left");
        else
            Core.Jump("m2", "Left");
        TotemAndGem(Bot.Config.Get<Rewards>("QuestReward"));
    }

    public void TotemAndGem(Rewards reward)
    {
        Core.EnsureAccept(4778);
        if (reward.ToString() == "0" || reward.ToString() == "TotemofNulgath")
        {
            if (reward.ToString() == "TotemofNulgath")
                Core.Logger("Totem of Nulgath selected - farming max Totems first.");
            else
                Core.Logger("No reward selected, farming Totems - then Gems");
            Totems();
        }
        else
            Core.Logger("Gem of Nulgath selected - farming max Gems first.");
        Gems();

        void Totems()
        {
            if (Core.CheckInventory(5357, 100))
            {
                Core.Logger("You already own Max Totems - farming Gems instead");
                Gems();
            }
            var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%2%3%4%5%6%7%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token).ContinueWith(_ => cts.Dispose());
            while (!Bot.ShouldExit && !Core.CheckInventory(5357, 100))
            {
                Core.SendPackets("%xt%zm%aggroMon%1%2%3%4%5%6%7%");
                Bot.Combat.Attack("*");
                if (Bot.Inventory.Contains("Essence of Nulgath", 65))
                {
                    Bot.Quests.EnsureComplete(4778, (int)Rewards.TotemofNulgath);
                    Bot.Sleep(Core.ActionDelay);
                    Bot.Quests.EnsureAccept(4778);
                    t++;
                    Core.Logger($"Quest for Totems completed x{t} times");
                }
            }
            cts.Cancel();
            Gems();
        }

        void Gems()
        {
            if (Core.CheckInventory(6136, 300))
            {
                Core.Logger("You already own Max Gems - checking Totems, if maxed will just army for the others.");
                if (Bot.Inventory.Contains(5357, 100))
                {
                    Core.Logger("You also have Max Totems - proceeding to army for the others.");
                    Army();
                }
                Totems();
            }
            var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%2%3%4%5%6%7%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token).ContinueWith(_ => cts.Dispose());
            while (!Bot.ShouldExit && !Core.CheckInventory(6136, 300))
            {
                Bot.Combat.Attack("*");
                if (Bot.Inventory.Contains("Essence of Nulgath", 65))
                {
                    Bot.Quests.EnsureComplete(4778, (int)Rewards.GemofNulgath);
                    Bot.Sleep(Core.ActionDelay);
                    Bot.Quests.EnsureAccept(4778);
                    g++;
                    Core.Logger($"Quest for Gems completed x{g} times");
                }
            }
            cts.Cancel();
        }

        void Army()
        {
            var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%2%3%4%5%6%7%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token).ContinueWith(_ => cts.Dispose());
            while (!Bot.ShouldExit)
                Bot.Combat.Attack("*");
            cts.Cancel();
        }
    }

    public enum Rewards
    {
        TotemofNulgath = 5357,
        GemofNulgath = 6136,
    }
}

// public void ArmyTotemGem(string monster)
// {
//     while (Bot.Inventory.Contains("Essence of Nulgath", 65))
//     {
//         Core.SendPackets("%xt%zm%aggroMon%1%2%3%4%5%6%7%");
//         Bot.Combat.Attack(monster);
//         Core.EnsureComplete(4778, (!Bot.Inventory.Contains("Totem of Nulgath", 100)) && (Bot.Config.Get<Rewards>("QuestReward") == Rewards.TotemofNulgath) ? (int)Rewards.TotemofNulgath : (int)Rewards.GemofNulgath);
//         i++;
//         Core.Logger($"Quest completed x{i} times");
//     }
//     Bot.Quests.EnsureAccept(4778);
//     Core.SendPackets("%xt%zm%aggroMon%1%2%3%4%5%6%7%");
//     Bot.Combat.Attack(monster);
// }

// public string AvailableCell(params string[] cells)
// {
//     return (cells.ToList() ?? Bot.Map.Cells).First(c => !Bot.Map.Players.Any(p => p.Cell == c)) ?? "m1";
// }