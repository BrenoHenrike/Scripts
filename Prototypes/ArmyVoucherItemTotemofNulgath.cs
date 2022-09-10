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
        new Option<Rewards>("QuestReward", "Totems, Gems or Essences?", "Select the reward to farm first - if you pick Essences it will just Army while picking them up", Rewards.EssenceOfNulgath),
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
        if (reward.ToString() == "TotemOfNulgath")
        {
            Core.Logger("Totems Of Nulgath selected, farming max Totems first - then Gems");
            Totems();
        }
        else if (reward.ToString() == "GemOfNulgath")
        {
            Core.Logger("Gems Of Nulgath selected, farming max Gems first - then Totems");
            Gems();
        }
        else
            Core.Logger("Essences Of Nulgath selected - Armying");
        Army();

        void Totems()
        {
            if (Core.CheckInventory(5357, 100))
            {
                Core.Logger("You already own Max Totems - checking Gems, if maxed will just army for the others");
                if (Bot.Inventory.Contains(6136, 300))
                {
                    Core.Logger("You also have Max Gems - proceeding to army for the others.");
                    Army();
                }
                else
                    Gems();
            }
            Core.FarmingLogger("Totem of Nulgath", 100);
            var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%2%3%4%5%6%7%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token);
            while (!Bot.ShouldExit && !Core.CheckInventory(5357, 100))
            {
                Bot.Combat.Attack("*");
                if (Bot.Inventory.Contains("Essence of Nulgath", 65))
                {
                    Bot.Quests.EnsureComplete(4778, (int)Rewards.TotemOfNulgath);
                    Bot.Sleep(Core.ActionDelay);
                    Bot.Quests.EnsureAccept(4778);
                    t++;
                    Core.Logger($"Quest for Totems completed x{t} times");
                }
            }
            cts.Cancel();
            Core.Jump(Bot.Player.Cell);
            if (!Core.CheckInventory(6136, 300))
                Gems();
            else
                Army();
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
                else
                    Totems();
            }
            Core.FarmingLogger("Gem of Nulgath", 300);
            var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%2%3%4%5%6%7%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token);
            while (!Bot.ShouldExit && !Core.CheckInventory(6136, 300))
            {
                Bot.Combat.Attack("*");
                if (Bot.Inventory.Contains("Essence of Nulgath", 65))
                {
                    Bot.Quests.EnsureComplete(4778, (int)Rewards.GemOfNulgath);
                    Bot.Sleep(Core.ActionDelay);
                    Bot.Quests.EnsureAccept(4778);
                    g++;
                    Core.Logger($"Quest for Gems completed x{g} times");
                }
            }
            cts.Cancel();
            Core.Jump(Bot.Player.Cell);
            if (!Core.CheckInventory(5357, 100))
                Totems();
            else
                Army();
        }

        void Army()
        {
            Core.Logger("Armying for the squad");
            var task = Bot.Send.PacketSpam("%xt%zm%aggroMon%1%2%3%4%5%6%7%", "String", Bot.Config.Get<int>("PacketDelay"), cts.Token);
            while (!Bot.ShouldExit)
                Bot.Combat.Attack("*");
        }
    }
    public enum Rewards
    {
        TotemOfNulgath = 5357,
        GemOfNulgath = 6136,
        EssenceOfNulgath = 0
    }
}