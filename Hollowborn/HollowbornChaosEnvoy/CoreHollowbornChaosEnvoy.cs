/*
name: null
description: null
tags: null
*/
//cs_include Scripts/Chaos/ChaosAvengerPreReqs.cs
//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Chaos/AscendedDrakathGear.cs
//cs_include Scripts/Chaos/EternalDrakathSet.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs
//cs_include Scripts/Story/StarSinc.cs
//cs_include Scripts/Story/TitanAttack.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Other/MergeShops/TitanStrikeGearMerge.cs
//cs_include Scripts/Other/Badges/ChaosPuppetMaster.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using System.Runtime.Serialization;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;

public class CoreHollowbornChaosEnvoy
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CoreHollowborn HB = new();
    private CoreQOM QOM = new();
    private ChaosAvengerClass CAV = new();
    private EternalDrakath ED = new();
    private AscendedDrakathGear ADG = new();
    private TitanStrikeGearMerge TGM = new();
    private ChaosPuppetMaster CPM = new();

    public string OptionsStorage = "HollowbornChaosEnvoy";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<bool>(
            "getAll", "Get all items",
            "Some quests need to be done multiple times in order to get everything, "+
            "if true the bot will continue untill it has everything from that quest before moving on" +
            "\nRecommended setting: True",
            true),
            new Option<bool>("BankAfter", "Bank Rewards", "bank Rewards after", true),
            CoreBots.Instance.SkipOptions,

            new Option<StirringDiscordRewards>("Stirring Discord", "Stirring Discord Reward", "Reward Selection for Stirring Discord", StirringDiscordRewards.None),
            new Option<InTheBeastsShadowRewards>("In TheBeasts Shadow", "In TheBeasts Shadow Reward", "Reward Selection for Stirring Discord", InTheBeastsShadowRewards.None),
            new Option<UniqueQuarryRewards>("Unique Quarry", "Unique Quarry Reward", "Reward Selection for Stirring Discord", UniqueQuarryRewards.None),
            new Option<WaveringIllusionsRewards>("Wavering Illusions", "Wavering Illusions Reward", "Reward Selection for Stirring Discord", WaveringIllusionsRewards.None),
            new Option<ShadowsOfDisdainRewards>("Shadows Of Disdain", "Shadows Of Disdain Reward", "Reward Selection for Stirring Discord", ShadowsOfDisdainRewards.None),
            new Option<PersistingMayhemRewards>("Persisting Mayhem", "Persisting Mayhem Reward", "Reward Selection for Stirring Discord", PersistingMayhemRewards.None),
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.RunCore();
    }

    public void GetAll()
    {
        bool getAllDrops = Bot.Config!.Get<bool>("getAll");
        bool BankAfter = Bot.Config!.Get<bool>("BankAfter");

        string? stirringDiscord = Bot.Config!.Get<string>("Stirring Discord");
        string? inTheBeastsShadow = Bot.Config!.Get<string>("In TheBeasts Shadow");
        string? uniqueQuarry = Bot.Config!.Get<string>("Unique Quarry");
        string? waveringIllusions = Bot.Config!.Get<string>("Wavering Illusions");
        string? shadowsOfDisdain = Bot.Config!.Get<string>("Shadows Of Disdain");
        string? persistingMayhem = Bot.Config!.Get<string>("Persisting Mayhem");

        if (stirringDiscord != null &&
            inTheBeastsShadow != null &&
            uniqueQuarry != null &&
            waveringIllusions != null &&
            shadowsOfDisdain != null &&
            persistingMayhem != null)
            Core.Logger($"Options Selected:\n" +
                $"\t\tStirring Discord:  [{stirringDiscord.Replace("_", " ")}]\n" +
                $"\t\tIn TheBeasts Shadow: [{inTheBeastsShadow.Replace("_", " ")}]\n" +
                $"\t\tUnique Quarry:  [{uniqueQuarry.Replace("_", " ")}\n" +
                $"\t\tWavering Illusions:  [{waveringIllusions.Replace("_", " ")}]\n" +
                $"\t\tShadows Of Disdain:  [{shadowsOfDisdain.Replace("_", " ")}]\n" +
                $"\t\tPersisting Mayhem: [{persistingMayhem.Replace("_", " ")}]\n");



        if (getAllDrops)
        {
            StirringDiscord(StirringDiscordRewards.All);
            if (BankAfter)
            {
                foreach (string item in Core.QuestRewards(8998))
                {
                    if (Core.CheckInventory(item, toInv: false))
                        Core.ToBank(item);
                }
            }

            InTheBeastsShadow(InTheBeastsShadowRewards.All);
            if (BankAfter)
            {
                foreach (string item in Core.QuestRewards(8999))
                {
                    if (Core.CheckInventory(item, toInv: false))
                        Core.ToBank(item);
                }
            }

            UniqueQuarry(UniqueQuarryRewards.All, false);
            if (BankAfter)
            {
                foreach (string item in Core.QuestRewards(9000))
                {
                    if (Core.CheckInventory(item, toInv: false))
                        Core.ToBank(item);
                }
            }

            WaveringIllusions(WaveringIllusionsRewards.All);
            if (BankAfter)
            {
                foreach (string item in Core.QuestRewards(9001))
                {
                    if (Core.CheckInventory(item, toInv: false))
                        Core.ToBank(item);
                }
            }

            ShadowsOfDisdain(ShadowsOfDisdainRewards.All);
            if (BankAfter)
            {
                foreach (string item in Core.QuestRewards(9002))
                {
                    if (Core.CheckInventory(item, toInv: false))
                        Core.ToBank(item);
                }
            }

            PersistingMayhem(PersistingMayhemRewards.All);
            if (BankAfter)
            {
                foreach (string item in Core.QuestRewards(9003))
                {
                    if (Core.CheckInventory(item, toInv: false))
                        Core.ToBank(item);
                }
            }
        }
        else
        {
            StirringDiscord(Bot.Config!.Get<StirringDiscordRewards>("Stirring Discord"), true);
            if (BankAfter)
                Core.ToBank((int)Bot.Config!.Get<StirringDiscordRewards>("Stirring Discord"));

            InTheBeastsShadow(Bot.Config!.Get<InTheBeastsShadowRewards>("In TheBeasts Shadow"), true);
            if (BankAfter)
                Core.ToBank((int)Bot.Config!.Get<InTheBeastsShadowRewards>("In TheBeasts Shadow"));

            UniqueQuarry(Bot.Config!.Get<UniqueQuarryRewards>("Unique Quarry"), true);
            if (BankAfter)
                Core.ToBank((int)Bot.Config!.Get<UniqueQuarryRewards>("Unique Quarry"));

            WaveringIllusions(Bot.Config!.Get<WaveringIllusionsRewards>("Wavering Illusions"), true);
            if (BankAfter)
                Core.ToBank((int)Bot.Config!.Get<WaveringIllusionsRewards>("Wavering Illusions"));

            ShadowsOfDisdain(Bot.Config!.Get<ShadowsOfDisdainRewards>("Shadows Of Disdain"), true);
            if (BankAfter)
                Core.ToBank((int)Bot.Config!.Get<ShadowsOfDisdainRewards>("Shadows Of Disdain"));

            PersistingMayhem(Bot.Config!.Get<PersistingMayhemRewards>("Persisting Mayhem"), true);
            if (BankAfter)
                Core.ToBank((int)Bot.Config!.Get<PersistingMayhemRewards>("Persisting Mayhem"));
        }
    }

    public void StirringDiscord(StirringDiscordRewards rewardSelection = StirringDiscordRewards.None, bool completeOnce = false)
    {
        string[] rewards = Core.QuestRewards(8998);

        if ((Bot.Config!.Get<StirringDiscordRewards>("Stirring Discord") == StirringDiscordRewards.All && Core.CheckInventory(rewards))
        || Bot.Config!.Get<StirringDiscordRewards>("Stirring Discord") == StirringDiscordRewards.None
        || Core.CheckInventory((int)Bot.Config!.Get<StirringDiscordRewards>("Stirring Discord")))
            return;

        Core.AddDrop(rewards);

        HB.HardcoreContract();
        Farm.Experience(75);

        Core.Logger($"Reward Choosen: {Bot.Config!.Get<StirringDiscordRewards>("Stirring Discord")}");
        while (!Bot.ShouldExit && !Core.CheckInventory(rewards))
        {
            Core.EnsureAccept(7158);
            Core.EnsureAccept(8998);
            Core.HuntMonster("lagunabeach", "Heart of Chaos", "Chaos Pirate Crew", isTemp: false);
            Core.HuntMonster("backroom", "Book Wyrm", "Maledictus Magum", isTemp: false);
            Core.HuntMonster("wardwarf", "Chaotic Draconian", "Chaotic Draconian Wings", isTemp: false);
            Core.KillMonster("blindingsnow", "r5", "Spawn", "*", "Shard of Chaos", 100, isTemp: false);
            Core.HuntMonster("chaosboss", "Ultra Chaos Warlord", "Chaotic War Essence", 15, false);
            Adv.BuyItem("crownsreach", 1383, "Chaotic Knight Helm");


            if (completeOnce)
            {
                Core.EnsureCompleteChoose(8998, Core.QuestRewards(8998));
                break;
            }
            else if (rewardSelection == StirringDiscordRewards.All)
                Core.EnsureCompleteChoose(8998, Core.QuestRewards(8998));

            else if (rewardSelection != StirringDiscordRewards.All && !completeOnce)
            {
                Core.EnsureComplete(8998, (int)Bot.Config!.Get<WaveringIllusionsRewards>("Unique Quarry"));
                Bot.Wait.ForPickup((int)Bot.Config!.Get<WaveringIllusionsRewards>("Unique Quarry"));
            }
        }
        Core.CancelRegisteredQuests();
    }

    public void InTheBeastsShadow(InTheBeastsShadowRewards rewardSelection = InTheBeastsShadowRewards.None, bool completeOnce = false)
    {
        string[] rewards = Core.QuestRewards(8999);

        if ((Bot.Config!.Get<InTheBeastsShadowRewards>("In TheBeasts Shadow") == InTheBeastsShadowRewards.All && Core.CheckInventory(rewards))
        || Bot.Config!.Get<InTheBeastsShadowRewards>("In TheBeasts Shadow") == InTheBeastsShadowRewards.None
        || Core.CheckInventory((int)Bot.Config!.Get<InTheBeastsShadowRewards>("In TheBeasts Shadow")))
            return;

        Core.AddDrop(rewards);

        if (!Bot.Quests.IsUnlocked(8999))
            StirringDiscord();

        Farm.Experience(75);

        Core.Logger($"Reward Choosen: {Bot.Config!.Get<InTheBeastsShadowRewards>("In TheBeasts Shadow")}");
        while (!Bot.ShouldExit)
        {
            Core.EnsureAccept(8999);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("hydra", "Hydra Head", "Hydra Armor", isTemp: false);
            Core.HuntMonster("roc", "Rock Roc", "Mini Rock Roc", isTemp: false);
            Core.KillMonster("odokuro", "Boss", "Right", "O-dokuro", "O-dokuro on Your Back", isTemp: false);
            Core.HuntMonster("chaoscave", "DracoWerePyre", "Burning Dragon Mace", isTemp: false);
            Core.HuntMonster("palooza", "Pony Gary Yellow", "Mini Pony Gary Yellow", isTemp: false);
            Core.HuntMonster("elemental", "Mana Golem", "Mana Golem", isTemp: false);
            Core.KillEscherion("Relic of Chaos", 13);

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("mountdoomskull", "b1", "Left", "*", "Fragment of Mount Doomskull", 1000, isTemp: false);

            // Check if all rewards are collected or the specific item is collected
            if (rewardSelection == InTheBeastsShadowRewards.All)
                Core.EnsureCompleteChoose(8999, Core.QuestRewards(8999));
            else if (rewardSelection != InTheBeastsShadowRewards.All && !completeOnce)
            {
                Core.EnsureComplete(8999, (int)Bot.Config!.Get<WaveringIllusionsRewards>("Unique Quarry"));
                Bot.Wait.ForPickup((int)Bot.Config!.Get<WaveringIllusionsRewards>("Unique Quarry"));
            }

            if (completeOnce || Core.CheckInventory(Bot.Config!.Get<InTheBeastsShadowRewards>("Unique Quarry").ToString()) || Core.CheckInventory(rewards))
                break; // Exit the loop if the condition is met
        }
        Core.CancelRegisteredQuests();
    }

    public void UniqueQuarry(UniqueQuarryRewards rewardSelection = UniqueQuarryRewards.None, bool completeOnce = false)
    {
        string[] rewards = Core.QuestRewards(9000);

        if ((Bot.Config!.Get<UniqueQuarryRewards>("Unique Quarry") == UniqueQuarryRewards.All && Core.CheckInventory(rewards))
        || Bot.Config!.Get<UniqueQuarryRewards>("Unique Quarry") == UniqueQuarryRewards.None
        || Core.CheckInventory((int)Bot.Config!.Get<UniqueQuarryRewards>("Unique Quarry")))
            return;

        Core.AddDrop(rewards);

        if (!Bot.Quests.IsUnlocked(9000))
            InTheBeastsShadow(completeOnce: true);

        Farm.Experience(75);
        ADG.AscendedGear("Ascended Face of Chaos");

        Core.Logger($"Reward Choosen: {Bot.Config!.Get<UniqueQuarryRewards>("Unique Quarry")}");
        while (!Bot.ShouldExit)
        {
            Core.EnsureAccept(9000);
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("chaoswar", "r2", "Spawn", "*", "Chaos Tentacle", 300, isTemp: false);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("sandcastle", "Chaos Sphinx", "Chaos Sphinx", isTemp: false);
            Core.HuntMonster("deepchaos", "Kathool", "Kathool Annihilator", isTemp: false);
            Core.HuntMonster("castleroof", "Chaos Dragon", "Chaos Dragon Slayer", isTemp: false);
            Core.HuntMonster("mirrorportal", "Chaos Harpy", "HarpyHunter", isTemp: false);
            Core.HuntMonster("orecavern", "Naga Baas", "Naga Baas Pet", isTemp: false);
            Core.HuntMonster("venomvaults", "Manticore", "Treasure Vault Key", isTemp: false);

            Adv.BuyItem("venomvaults", 585, "Chaotic Manticore Head");

            if (!Core.CheckInventory("Chaoroot", 30))
            {
                Farm.Gold(900000);
                Core.BuyItem("tercessuinotlim", 1951, "Receipt of Swindle", 3);
                Core.BuyItem("tercessuinotlim", 1951, "Chaoroot", 30);
            }

            // Check if all rewards are collected or the specific item is collected
            if (rewardSelection == UniqueQuarryRewards.All)
                Core.EnsureCompleteChoose(9000, Core.QuestRewards(9000));

            else if (rewardSelection != UniqueQuarryRewards.All && !completeOnce)
            {
                Core.EnsureComplete(9000, (int)Bot.Config!.Get<WaveringIllusionsRewards>("Unique Quarry"));
                Bot.Wait.ForPickup((int)Bot.Config!.Get<WaveringIllusionsRewards>("Unique Quarry"));
            }

            if (completeOnce || Core.CheckInventory((int)Bot.Config!.Get<UniqueQuarryRewards>("Unique Quarry")) || Core.CheckInventory(rewards))
                break; // Exit the loop if the condition is met
            Core.CancelRegisteredQuests();
        }
    }

    public void WaveringIllusions(WaveringIllusionsRewards rewardSelection = WaveringIllusionsRewards.None, bool completeOnce = false)
    {
        string[] rewards = Core.QuestRewards(9001);

        if ((Bot.Config!.Get<WaveringIllusionsRewards>("Wavering Illusions") == WaveringIllusionsRewards.All && Core.CheckInventory(rewards))
        || Bot.Config!.Get<WaveringIllusionsRewards>("Wavering Illusions") == WaveringIllusionsRewards.None
        || Core.CheckInventory((int)Bot.Config!.Get<WaveringIllusionsRewards>("Wavering Illusions"), toInv: false))
            return;

        Core.AddDrop(rewards);

        if (!Bot.Quests.IsUnlocked(9001))
            UniqueQuarry();

        Farm.Experience(80);
        QOM.TheQueensSecrets();
        Core.HuntMonster("finalbattle", "Drakath", "Drakath Wings", isTemp: false);

        Core.Logger($"Reward Choosen: {Bot.Config!.Get<WaveringIllusionsRewards>("Wavering Illusions")}");
        if (!Core.CheckInventory("Supreme Arcane Staff of Chaos"))
        {
            Core.HuntMonster("ledgermayne", "Ledgermayne", "The Supreme Arcane Staff", isTemp: false); // Can buyback
            Adv.BuyItem("deepforest", 1999, "Supreme Arcane Staff of Chaos");
        }

        CPM.Badge();

        while (!Bot.ShouldExit)
        {
            Core.EnsureAccept(9001);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("chaoscrypt", "Chaorrupted Knight", "Chaos Gem", 200, isTemp: false);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("chaoslab", "Chaos Artix", "Chaorrupted Light of Destiny", isTemp: false);
            Core.HuntMonster("mqlesson", "Dragonoid", "Dragonoid of Hours", isTemp: false);
            Core.HuntMonster("timespace", "Chaos Lord Iadoa", "Chaorrupted Hourglass", 30, isTemp: false, publicRoom: true);
            Core.HuntMonster("chaoskraken", "Chaos Kraken", "Chaotic Invertebrae", 20, isTemp: false, publicRoom: true);

            Core.BuyItem("downbelow", 2004, "Chaos PuppetMaster");

            if (rewardSelection == WaveringIllusionsRewards.All)
                Core.EnsureCompleteChoose(9001, Core.QuestRewards(9001));

            else if (rewardSelection != WaveringIllusionsRewards.All && !completeOnce)
            {
                Core.EnsureComplete(9001, (int)Bot.Config!.Get<WaveringIllusionsRewards>("Unique Quarry"));
                Bot.Wait.ForPickup((int)Bot.Config!.Get<WaveringIllusionsRewards>("Unique Quarry"));
            }

            else if (completeOnce || Core.CheckInventory((int)Bot.Config!.Get<WaveringIllusionsRewards>("Unique Quarry")) || Core.CheckInventory(rewards))
                break; // Exit the loop if the condition is met
        }
        Core.CancelRegisteredQuests();
    }

    public void ShadowsOfDisdain(ShadowsOfDisdainRewards rewardSelection = ShadowsOfDisdainRewards.None, bool completeOnce = false)
    {
        string[] rewards = Core.QuestRewards(9002);

        if ((Bot.Config!.Get<ShadowsOfDisdainRewards>("Shadows Of Disdain") == ShadowsOfDisdainRewards.All && Core.CheckInventory(rewards))
        || Bot.Config!.Get<ShadowsOfDisdainRewards>("Shadows Of Disdain") == ShadowsOfDisdainRewards.None
        || Core.CheckInventory((int)Bot.Config!.Get<ShadowsOfDisdainRewards>("Shadows Of Disdain"), toInv: false))
            return;

        Core.AddDrop(rewards);

        if (!Bot.Quests.IsUnlocked(9002))
            WaveringIllusions();

        Farm.Experience(95);
        ED.getSet();

        if (!Core.CheckInventory("Titan Drakath"))
            TGM.BuyAllMerge("Titan Drakath");


        Core.Logger($"Reward Choosen: {Bot.Config!.Get<ShadowsOfDisdainRewards>("Shadows Of Disdain")}");
        while (!Bot.ShouldExit)
        {
            Core.EnsureAccept(9002);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("mountdoomskull", "Chaos Spider", "Chaos War Medal", 1000, isTemp: false);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("finalshowdown", "Prince Drakath", "Drakath Pet", isTemp: false);
            CAV.FragmentsoftheLordsA();
            CAV.FragmentsoftheLordsB();
            Core.HuntMonster("ultradrakath", "Champion of Chaos", "Trace of Chaos", 13, isTemp: false, publicRoom: true);

            Adv.BuyItem("transformation", 2002, "Chaorrupted Usurper");

            if (rewardSelection == ShadowsOfDisdainRewards.All)
                Core.EnsureCompleteChoose(9002, Core.QuestRewards(9002));

            else if (rewardSelection != ShadowsOfDisdainRewards.All && !completeOnce)
            {
                Core.EnsureComplete(9002, (int)Bot.Config!.Get<WaveringIllusionsRewards>("Unique Quarry"));
                Bot.Wait.ForPickup((int)Bot.Config!.Get<WaveringIllusionsRewards>("Unique Quarry"));
            }

            else if (completeOnce || Core.CheckInventory(Bot.Config!.Get<ShadowsOfDisdainRewards>("Unique Quarry").ToString()) || Core.CheckInventory(rewards))
                break; // Exit the loop if the condition is met
        }

        Core.CancelRegisteredQuests();
    }

    public void PersistingMayhem(PersistingMayhemRewards rewardSelection = PersistingMayhemRewards.None, bool completeOnce = false)
    {
        string[] rewards = Core.QuestRewards(9003);

        if ((Bot.Config!.Get<PersistingMayhemRewards>("Persisting Mayhem") == PersistingMayhemRewards.All && Core.CheckInventory(rewards))
        || Bot.Config!.Get<PersistingMayhemRewards>("Persisting Mayhem") == PersistingMayhemRewards.None
        || Core.CheckInventory((int)Bot.Config!.Get<PersistingMayhemRewards>("Persisting Mayhem"), toInv: false))
            return;

        Core.AddDrop(rewards);

        if (!Bot.Quests.IsUnlocked(9003))
            ShadowsOfDisdain();

        Farm.Experience(95);

        Core.EquipClass(ClassType.Solo);

        Core.Logger($"Reward Choosen: {Bot.Config!.Get<PersistingMayhemRewards>("Persisting Mayhem")}");
        while (!Bot.ShouldExit)
        {
            Core.EnsureAccept(9003);
            Core.HuntMonster("ultradrakath", "Champion of Chaos", "Trace of Chaos", 13, isTemp: false, publicRoom: true);

            // Check if all rewards are collected or the specific item is collected
            if (completeOnce)
            {
                Core.EnsureCompleteChoose(9003, Core.QuestRewards(9003));
                break;
            }
            else if (rewardSelection == PersistingMayhemRewards.All)
                Core.EnsureCompleteChoose(9003, Core.QuestRewards(9003));

            else if (rewardSelection != PersistingMayhemRewards.All && !completeOnce)
            {
                Core.EnsureComplete(9003, (int)Bot.Config!.Get<WaveringIllusionsRewards>("Unique Quarry"));
                Bot.Wait.ForPickup((int)Bot.Config!.Get<WaveringIllusionsRewards>("Unique Quarry"));
            }

            if (completeOnce || Core.CheckInventory(Bot.Config!.Get<PersistingMayhemRewards>("Unique Quarry").ToString()) || Core.CheckInventory(rewards))
                break; // Exit the loop if the condition is met
        }

        Core.CancelRegisteredQuests();
    }

    public enum StirringDiscordRewards
    {
        Hollowborn_Chaos_Warrior = 74476,
        Hollowborn_Chaos_Morph = 74477,
        Hollowborn_Chaoti_Wings = 74478,
        All,
        None
    }
    public enum InTheBeastsShadowRewards
    {
        Hollowborn_Omega_Blades = 74480,
        Hollowborn_Omega_Sword = 74481,
        Hollowborn_Chaos_Unlocker = 74482,
        All,
        None
    }
    public enum UniqueQuarryRewards
    {
        Hollowborn_Benevolent_Locks = 74485,
        Hollowborn_Malignant_Locks = 74488,
        Hollowborn_Face_of_Chaos = 74491,
        Hollowborn_Gaze_of_Chaos = 74492,
        All,
        None
    }
    public enum WaveringIllusionsRewards
    {
        Hollowborn_Eye_of_Chaos = 74479,
        Hollowborn_Wings_of_Chaos = 74493,
        Hollowborn_Chaotic_Portal = 74499,
        All,
        None
    }
    public enum ShadowsOfDisdainRewards
    {
        Hollowborn_Envoy_of_Chaos = 74489,
        Hollowborn_Alignment_Aspects = 74494,
        Hollowborn_Blade_of_Chaos_Cape = 74495,
        Hollowborn_Chaotic_Portal = 74496,
        Hollowborn_Blade_of_Chaos = 74497,
        Hollowborn_Claw_of_Chaos = 74498,
        All,
        None
    }
    public enum PersistingMayhemRewards
    {
        Hollowborn_Benevolent = 74483,
        Hollowborn_Benevolent_Morph = 74484,
        Hollowborn_Malignant = 74486,
        Hollowborn_Malignant_Morph = 74487,
        Idle_Hollowborn_Envoy_of_Chaos = 74490,

        [EnumMember(Value = "Hollowborn_Blade_of_Chaos_(Floor)")]
        Hollowborn_Blade_of_Chaos_Floor = 74500,

        Hollowborn_Blades_of_Chaos = 74501,
        Hollowborn_Omega_Swords = 74502,
        Hollowborn_Chaos_Unlockers = 74503,
        All,
        None
    }

}
