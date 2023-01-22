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
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs
//cs_include Scripts/Story/StarSinc.cs
//cs_include Scripts/Story/TitanAttack.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Other/MergeShops/TitanGearIIMerge.cs
//cs_include Scripts/Other/Badges/ChaosPuppetMaster.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

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
    private TitanGearIIMerge TGM = new();
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
            false),
        CoreBots.Instance.SkipOptions,
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.RunCore();
    }

    public void GetAll(bool getAllDrops = true)
    {
        StirringDiscord(getAllDrops);
        Core.ToBank(Core.EnsureLoad(8998).Rewards.Select(x => x.Name).ToArray());
        InTheBeastsShadow(getAllDrops);
        Core.ToBank(Core.EnsureLoad(8999).Rewards.Select(x => x.Name).ToArray());
        UniqueQuarry(getAllDrops);
        Core.ToBank(Core.EnsureLoad(9000).Rewards.Select(x => x.Name).ToArray());
        WaveringIllusions(getAllDrops);
        Core.ToBank(Core.EnsureLoad(9001).Rewards.Select(x => x.Name).ToArray());
        ShadowsOfDisdain();
        Core.ToBank(Core.EnsureLoad(9002).Rewards.Select(x => x.Name).ToArray());
        PersistingMayhem(getAllDrops);
        Core.ToBank(Core.EnsureLoad(9003).Rewards.Select(x => x.Name).ToArray());
    }

    public void StirringDiscord(bool getAll = true)
    {
        string[] rewards = Core.EnsureLoad(8998).Rewards.Select(x => x.Name).ToArray();
        if (Core.CheckInventory(rewards, any: !getAll, toInv: false))
            return;

        Core.AddDrop(rewards);

        HB.HardcoreContract();
        Farm.Experience(75);

        Core.RegisterQuests(8998);
        Core.EnsureAccept(7158);
        while (!Bot.ShouldExit && !Core.CheckInventory(rewards, any: !getAll))
        {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("lagunabeach", "Heart of Chaos", "Chaos Pirate Crew", isTemp: false, publicRoom: true);
            Core.HuntMonster("backroom", "Book Wyrm", "Maledictus Magum", isTemp: false, publicRoom: true);
            Core.HuntMonster("chaosboss", "Ultra Chaos Warlord", "Chaotic War Essence", 15, false);

            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("wardwarf", "Chaotic Draconian", "Chaotic Draconian Wings", isTemp: false);
            Core.KillMonster("blindingsnow", "r5", "Spawn", "*", "Shard of Chaos", 100, isTemp: false);

            Adv.BuyItem("crownsreach", 1383, "Chaotic Knight Helm");

            foreach (string s in rewards)
                Bot.Wait.ForPickup(s);
        }
        Core.CancelRegisteredQuests();
    }

    public void InTheBeastsShadow(bool getAll = true)
    {
        string[] rewards = Core.EnsureLoad(8999).Rewards.Select(x => x.Name).ToArray();
        if (Core.CheckInventory(rewards, any: !getAll, toInv: false))
            return;

        Core.AddDrop(rewards);
        if (!Bot.Quests.IsUnlocked(8999))
            StirringDiscord(getAll);

        Farm.Experience(75);

        Core.RegisterQuests(8999);
        while (!Bot.ShouldExit && !Core.CheckInventory(rewards, any: !getAll))
        {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("hydra", "Hydra Head", "Hydra Armor", isTemp: false);
            Core.HuntMonster("roc", "Rock Roc", "Mini Rock Roc", isTemp: false);
            Core.KillMonster("odokuro", "Boss", "Right", "O-dokuro", "O-dokuro on Your Back", isTemp: false);
            Core.HuntMonster("chaoscave", "Dracowerepyre", "Burning Dragon Mace", isTemp: false);
            Core.HuntMonster("palooza", "Pony Gary Yellow", "Mini Pony Gary Yellow", isTemp: false);
            Core.HuntMonster("elemental", "Mana Golem", "Mana Golem", isTemp: false);
            Core.KillEscherion("Relic of Chaos", 13);

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("mountdoomskull", "b1", "Left", "*", "Fragment of Mount Doomskull", 1000, isTemp: false);

            foreach (string s in rewards)
                Bot.Wait.ForPickup(s);
        }
        Core.CancelRegisteredQuests();
    }

    public void UniqueQuarry(bool getAll = true)
    {
        string[] rewards = Core.EnsureLoad(9000).Rewards.Select(x => x.Name).ToArray();
        if (Core.CheckInventory(rewards, any: !getAll, toInv: false))
            return;

        Core.AddDrop(rewards);
        if (!Bot.Quests.IsUnlocked(9000))
            InTheBeastsShadow(getAll);

        Farm.Experience(75);
        ADG.AscendedGear("Ascended Face of Chaos");

        Core.RegisterQuests(9000);
        while (!Bot.ShouldExit && !Core.CheckInventory(rewards, any: !getAll))
        {
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

            foreach (string s in rewards)
                Bot.Wait.ForPickup(s);
        }
        Core.CancelRegisteredQuests();
    }

    public void WaveringIllusions(bool getAll = true)
    {
        string[] rewards = Core.EnsureLoad(9001).Rewards.Select(x => x.Name).ToArray();
        if (Core.CheckInventory(rewards, any: !getAll, toInv: false))
            return;

        Core.AddDrop(rewards);
        if (!Bot.Quests.IsUnlocked(9001))
            UniqueQuarry(getAll);

        Farm.Experience(80);
        QOM.TheQueensSecrets();
        Core.HuntMonster("finalbattle", "Drakath", "Drakath Wings", isTemp: false);
        if (!Core.CheckInventory("Supreme Arcane Staff of Chaos"))
        {
            Core.HuntMonster("ledgermayne", "Ledgermayne", "The Supreme Arcane Staff", isTemp: false); //Can buyback
            Adv.BuyItem("deepforest", 1999, "Supreme Arcane Staff of Chaos");
        }

        CPM.Badge();
        Core.RegisterQuests(9001);
        while (!Bot.ShouldExit && !Core.CheckInventory(rewards, any: !getAll))
        {
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("chaoscrypt", "Chaorrupted Knight", "Chaos Gem", 200, isTemp: false);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("chaoslab", "Chaos Artix", "Chaorrupted Light of Destiny", isTemp: false);
            Core.HuntMonster("mqlesson", "Dragonoid", "Dragonoid of Hours", isTemp: false);
            Core.HuntMonster("timespace", "Chaos Lord Iadoa", "Chaorrupted Hourglass", 30, isTemp: false, publicRoom: true);
            Core.HuntMonster("chaoskraken", "Chaos Kraken", "Chaotic Invertebrae", 20, isTemp: false, publicRoom: true);

            Core.BuyItem("downbelow", 2004, "Chaos PuppetMaster");

            foreach (string s in rewards)
                Bot.Wait.ForPickup(s);
        }
        Core.CancelRegisteredQuests();
    }

    public void ShadowsOfDisdain()
    {
        string[] rewards = Core.EnsureLoad(9002).Rewards.Select(x => x.Name).ToArray();
        if (Core.CheckInventory(rewards, toInv: false))
            return;

        Core.AddDrop(rewards);
        if (!Bot.Quests.IsUnlocked(9002))
            WaveringIllusions(Bot.Config == null || Bot.Config.Get<bool>("getAll"));

        Farm.Experience(95);
        ED.getSet();
        if (!Core.CheckInventory("Titan Drakath"))
            TGM.BuyAllMerge("Titan Drakath");

        Core.EnsureAccept(9002);

        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("mountdoomskull", "Chaos Spider", "Chaos War Medal", 1000, isTemp: false);

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("finalshowdown", "Prince Drakath", "Drakath Pet", isTemp: false);
        CAV.FragmentsoftheLordsA();
        CAV.FragmentsoftheLordsB();
        Core.HuntMonster("ultradrakath", "Champion of Chaos", "Trace of Chaos", 13, isTemp: false, publicRoom: true);

        Adv.BuyItem("transformation", 2002, "Chaorrupted Usurper");

        Core.EnsureComplete(9002);
    }

    public void PersistingMayhem(bool getAll = true)
    {
        string[] rewards = Core.EnsureLoad(9003).Rewards.Select(x => x.Name).ToArray();
        if (Core.CheckInventory(rewards, any: !getAll, toInv: false))
            return;

        Core.AddDrop(rewards);
        if (!Bot.Quests.IsUnlocked(9003))
            ShadowsOfDisdain();
        Farm.Experience(95);

        Core.EquipClass(ClassType.Solo);

        Core.RegisterQuests(9003);
        while (!Bot.ShouldExit && !Core.CheckInventory(rewards, any: !getAll))
        {
            Core.HuntMonster("ultradrakath", "Champion of Chaos", "Trace of Chaos", 13, isTemp: false, publicRoom: true);

            foreach (string s in rewards)
                Bot.Wait.ForPickup(s);
        }
        Core.CancelRegisteredQuests();
    }
}
