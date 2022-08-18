//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using RBot;
using RBot.Options;
using RBot.Items;
using RBot.Quests;

public class JuggernautItemsofNulgath
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();


    public string OptionsStorage = "Reward Select";
    public bool DontPreconfigure = false;
    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("SkipOption", "Skip this window next time", "You will be able to return to this screen via [Options] -> [Script Options] if you wish to change anything.", false),
        new Option<RewardsSelection>("RewardsSelection", "Select Your Quest Reward", "Select Your Quest Reward for The JuggerNaught items of Nulgath quest.", RewardsSelection.Oblivion_of_Nulgath),
    };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.AddRange(Rewards);
        Core.SetOptions();

        JuggItems(Bot.Config.Get<RewardsSelection>("RewardsSelection"));

        Core.SetOptions(false);
    }

    public void JuggItems(RewardsSelection reward = RewardsSelection.All)
    {
        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop(Rewards);

        Nation.FarmUni13();


        var Count = 0;
        int x = 1;
        Quest jugg = Bot.Quests.EnsureLoad(837);
        var item = jugg.Rewards.Find(i => i.ID == (int)reward) ?? null;
        Count = Rewards.Count();



        while (!Bot.ShouldExit() && !Core.CheckInventory((int)reward, toInv: false))
        {
            if (Bot.Config.Get<RewardsSelection>("RewardsSelection") == RewardsSelection.All)
                Core.Logger($"Farming All {x++}/{Count}");
            Core.Logger($"... {Bot.Config.Get<RewardsSelection>("RewardsSelection")} ...");

            Core.EnsureAccept(837);
            Nation.FarmDiamondofNulgath(13);
            Nation.FarmDarkCrystalShard(50);
            Nation.FarmTotemofNulgath(3);
            Nation.FarmGemofNulgath(20);
            Nation.FarmVoucher(false);
            Nation.SwindleBulk(50);
            Core.HuntMonster("underworld", "Undead Bruiser", "Undead Bruiser Rune");

            if (Bot.Config.Get<RewardsSelection>("RewardsSelection") != RewardsSelection.All)
                Core.EnsureComplete(837, (int)reward);
            else Core.EnsureCompleteChoose(837);
        }
    }

    public readonly string[] Rewards =
    {
        "Oblivion of Nulgath",
        "Ungodly Reavers of Nulgath",
        "Warlord of Nulgath",
        "Arcane of Nulgath",
        "Dimensional Champion of Nulgath",
        "Crystal Phoenix Blade of Nulgath",
        "Overfiend Blade of Nulgath",
        "Battlefiend Blade of Nulgath",
        "Dark Makai of Nulgath",
        "Nulgath Armor",
        "Polish Hussar",
        "Polish Hussar Helm",
        "Polish Hussar Spear",
        "Polish Hussar Wings",
        "Void Cowboy",
        "Void Cowboy Hat",
        "Void Cowboy Morph",
        "Void Cowboy's Mask + Locks",
        "Void Cowboy's Pistol",
        "Dual Void Cowboy Pistols"
    };

    public enum RewardsSelection
    {
        Oblivion_of_Nulgath = 2232,
        UngodlyReavers_of_Nulgath = 4939,
        Warlord_of_Nulgath = 5527,
        Arcane_of_Nulgath = 5530,
        Dimensional_Champion_of_Nulgath = 5531,
        Crystal_Phoenix_Blade_of_Nulgath = 6137,
        Overfiend_Blade_of_Nulgath = 6138,
        Battle_fiend_Blade_of_Nulgath = 6141,
        Dark_Makai_of_Nulgath = 6142,
        Nulgath_Armor = 6375,
        Polish_Hussar = 42596,
        Polish_Hussar_Helm = 42597,
        Polish_Hussar_Spear = 42598,
        Polish_Hussar_Wings = 42599,
        VoidCowboy_ = 52799,
        VoidCowboy_Hat = 52800,
        VoidCowboy_Morph = 52801,
        VoidCowboys_MaskLocks = 52802,
        VoidCowboys_Pistol = 52803,
        Dual_VoidCowboy_Pistols = 52818,
        All
    };
}