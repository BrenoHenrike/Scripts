//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmyEmblemOfNulgath
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreArmyLite Army = new();
    public CoreNation Nation = new();
    public string OptionsStorage = "ArmyEmblemOfNulgath";
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

        Core.SetOptions(disableClassSwap: true);
        bot.Options.RestPackets = false;

        FarmingTime();

        Core.SetOptions(false);
    }

    public string[] Loot = { "Fiend Seal", "Gem of Domination", "Emblem of Nulgath" };

    public void FarmingTime()
    {
        if (!Core.CheckInventory("Nation Round 4 Medal"))
        {
            Core.Logger("Nation Round 4 Medal not found, getting it for you");
            Nation.NationRound4Medal();
        }

        Core.EquipClass(ClassType.Farm);
        Core.AddDrop(Loot);
        Core.RegisterQuests(4748);
        if (string.IsNullOrEmpty(Bot.Config.Get<string>("player5").Trim()) && string.IsNullOrEmpty(Bot.Config.Get<string>("player6").Trim()))
        {
            Army.AggroMonCells("r13", "r14", "r15", "r16");
            Army.AggroMonStart("shadowblast");
            Army.DivideOnCells("r13", "r14", "r15", "r16");
        }
        else
        {
            Army.AggroMonCells("r13", "r14", "r15", "r16", "r17", "r4");
            Army.AggroMonStart("shadowblast");
            Army.DivideOnCells("r13", "r14", "r15", "r16", "r17", "r4");
        }
        while (!Bot.ShouldExit)
            Bot.Combat.Attack("*");
        Army.AggroMonStop();
        Army.AggroMonClear();
        Core.CancelRegisteredQuests();
    }
}

// public enum Rewards
// {
//     TotemofNulgath = 5357,
//     GemofNulgath = 6136,
//     EssenceofNulgath = 0
// }