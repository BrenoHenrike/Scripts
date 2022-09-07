//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;

public class JuggernautItemsofNulgath
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();


    public string OptionsStorage = "Reward Select";
    public bool DontPreconfigure = false;
    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("SkipOption", "Skip this window next time", "You will be able to return to this screen via [Options] -> [Script Options] if you wish to change anything.", false),
        new Option<RewardsSelection>("RewardsSelection", "Select Your Quest Reward", "Select Your Quest Reward for The JuggerNaught items of Nulgath quest.", RewardsSelection.OblivionofNulgath),
    };

    public void ScriptMain(IScriptInterface bot)
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
        Quest jugg = /*Bot.Quests.EnsureLoad*/Core.EnsureLoad(837);
        var item = jugg.Rewards.Find(i => i.ID == (int)reward) ?? null;
        Count = Rewards.Count();



        while (!Bot.ShouldExit && !Core.CheckInventory((int)reward, toInv: false))
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
        OblivionofNulgath = 2232,
        UngodlyReaversofNulgath = 4939,
        WarlordofNulgath = 5527,
        ArcaneofNulgath = 5530,
        DimensionalChampionofNulgath = 5531,
        CrystalPhoenixBladeofNulgath = 6137,
        OverfiendBladeofNulgath = 6138,
        BattlefiendBladeofNulgath = 6141,
        DarkMakaiofNulgath = 6142,
        NulgathArmor = 6375,
        PolishHussar = 42596,
        PolishHussarHelm = 42597,
        PolishHussarSpear = 42598,
        PolishHussarWings = 42599,
        VoidCowboy = 52799,
        VoidCowboyHat = 52800,
        VoidCowboyMorph = 52801,
        VoidCowboysMaskLocks = 52802,
        VoidCowboysistol = 52803,
        DualVoidCowboyPistols = 52818,
        All
    };
}