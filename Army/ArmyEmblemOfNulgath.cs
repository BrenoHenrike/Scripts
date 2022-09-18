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

    public static CoreBots sCore = new();
    public static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyEmblemOfNulgathV2";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sCore.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        if (!Bot.Config.Get<bool>("SkipOption"))
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
            Army.AggroMonCells("r13", "r14", "r15", "r16");
        else Army.AggroMonCells("r13", "r14", "r15", "r16", "r17", "r4");
        Army.AggroMonStart("shadowblast");
        Army.DivideOnCells("r13", "r14", "r15", "r16", "r17", "r4");

        while (!Bot.ShouldExit)
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }
}

// public enum Rewards
// {
//     TotemofNulgath = 5357,
//     GemofNulgath = 6136,
//     EssenceofNulgath = 0
// }