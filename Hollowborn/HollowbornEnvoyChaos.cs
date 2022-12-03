//cs_include Scripts/Chaos/AscendedDrakathGear.cs
//cs_include Scripts/Chaos/ChaosAvengerPreReqs.cs
//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/Chaos/EternalDrakathSet.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs
//cs_include Scripts/Story/StarSinc.cs
//cs_include Scripts/Story/TitanAttack.cs
//cs_include Scripts/Story/TowerOfDoom.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class HollowbornEnvoyChaos
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreHollowborn HB = new CoreHollowborn();
    public Core13LoC LOC = new Core13LoC();
    public CoreQOM QOM = new();
    public ChaosAvengerClass CAV = new();
    public EternalDrakath ED = new EternalDrakath();
    public TitanAttackStory TAS = new();
    public AscendedDrakathGear ADG = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        if (Core.CheckInventory(QuestsRewards, toInv: false))
            return;

        Reqs();
        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        if (!Core.CheckInventory(Quest1, toInv: false))
            AutoReward(8998);
        if (!Core.CheckInventory(Quest2, toInv: false))
            AutoReward(8999);
        if (!Core.CheckInventory(Quest3, toInv: false))
            AutoReward(9000);
        if (!Core.CheckInventory(Quest4, toInv: false))
            AutoReward(9001);
        if (!Core.CheckInventory(Quest5, toInv: false))
            AutoReward(9002);
        if (!Core.CheckInventory(Quest6, toInv: false))
            AutoReward(9003);
    }

    public void Reqs()
    {
        if (!Core.CheckInventory("Lae\'s Hardcore Contract"))
            HB.HardcoreContract();
        LOC.Complete13LOC();
        QOM.CompleteEverything();
        TAS.DoAll();
        //Stiring Discord
        Farm.ChaosMilitiaREP(5);
        //Unique Quarry req
        ADG.AscendedGear("Ascended Face of Chaos");
        //Wavering Illusion req
        Core.HuntMonster("finalbattle-999999", "Drakath", "Drakath Wings", isTemp: false); //Public because of room limit 1
        if (!Core.CheckInventory("Supreme Arcane Staff of Chaos"))
        {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("ledgermayne", "Ledgermayne", "The Supreme Arcane Staff", isTemp: false); //Can buyback
            Adv.BuyItem("deepforest", 1999, "Gold Voucher 500k", 4);
            Core.BuyItem("deepforest", 1999, "Supreme Arcane Staff of Chaos");

        }
        //Shadows of Desdain
        if (!Core.CheckInventory("Drakath the Eternal"))
            ED.getSet();
        if (!Core.CheckInventory("Titan Drakath"))
        {
            //Titan Paladin
            if (!Core.CheckInventory("Titan Paladin"))
            {
                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("titanattack", "Chaorrupted Bandit", "AntiTitan Supplies", 100, false);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("titanattack", "Titanic Vindicator", "Titanic Fluid", 40, false);
                Adv.BuyItem("titanattack", 2149, "Titan Paladin");
            }

            //Vindicator Titan XL
            if (!Core.CheckInventory("Vindicator Titan XL"))
            {
                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("titanattack", "Chaorrupted Bandit", "AntiTitan Supplies", 100, false);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("titanattack", "Titanic Vindicator", "Titanic Fluid", 40, false);
                Core.HuntMonster("titanattack", "Titanic Vindicator", "Vindicator Titan", isTemp: false);
                Adv.BuyItem("titanattack", 2149, "Vindicator Titan XL");
            }

            //Titanic Destroyer
            if (!Core.CheckInventory("Titanic Destroyer"))
            {
                Adv.BuyItem("titanattack", 2154, "Gold Voucher 500k");
                Core.HuntMonster("titanstrike", "Titanic Destroyer", "Destroyer Essence", 60, isTemp: false);
                Core.BuyItem("titanattack", 2154, "Titanic Destroyer");
            }

            //Heroic Titan
            if (!Core.CheckInventory("Heroic Titan"))
            {
                Adv.BuyItem("titanattack", 2154, "Gold Voucher 500k");
                Core.HuntMonster("titanstrike", "Titanic Destroyer", "Destroyer Essence", 40, isTemp: false);
                Core.BuyItem("titanattack", 2154, "Heroic Titan");
            }

            //Titan Drakath
            Core.BuyItem("titanattack", 2154, "Titan Drakath");
            Core.ToBank(new string[] { "Titan Paladin", "Vindicator Titan XL", "Titanic Destroyer", "Heroic Titan" }); //just incase, don't think needed
        }
    }
    private void AutoReward(int questID)
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        // if(Core.CheckInventory(Rewards, toInv: false))
        //     return;

        Core.AddDrop(Rewards);

        if (questID == 8998) //
        {
            foreach (string item in Rewards)
            {
                Core.FarmingLogger(item, 1);
                while (!Bot.ShouldExit && !Core.CheckInventory(item))
                {
                    Core.EnsureAccept(new int[] {7158, questID}); //7158 Needed for the item to drop
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("lagunabeach", "Heart of Chaos", "Chaos Pirate Crew", isTemp: false);
                    Core.HuntMonster("backroom", "Book Wyrm", "Maledictus Magum", isTemp: false);
                    Core.HuntMonster("wardwarf", "Chaotic Draconian", "Chaotic Draconian Wings", isTemp: false);
                    Core.HuntMonster("chaosboss", "Ultra Chaos Warlord", "Chaotic War Essence", 15, false);
                    Core.BuyItem("crownsreach", 1383, "Chaotic Knight Helm");
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("blindingsnow", "r5", "Spawn", "*", "Shard of Chaos", 100, isTemp: false);
                    Core.EnsureCompleteChoose(questID, new string[] {item});
                }
                Core.ToBank(Rewards);
            }
        }

        if (questID == 8999) //In the Beasts' Shadow
        {
            foreach (string item in Rewards)
            {
                Core.FarmingLogger(item, 1);
                while (!Bot.ShouldExit && !Core.CheckInventory(item))
                {
                    Core.EnsureAccept(questID);
                    Core.EquipClass(ClassType.Solo);
                    Core.KillMonster("hydra", "Boss", "Left", "*", "Hydra Armor", isTemp: false);
                    Core.HuntMonster("roc", "Rock Roc", "Mini Rock Roc", isTemp: false);
                    Core.HuntMonster("palooza", "Pony Gary Yellow", "Mini Pony Gary Yellow", isTemp: false);
                    Core.HuntMonster("elemental", "Mana Golem", "Mana Golem", isTemp: false);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("mountdoomskull", "Chaorrupted Rogue", "Fragment of Mount Doomskull", 1300, isTemp: false);
                    Core.KillEscherion("Relic of Chaos", 13, log: false);
                    Core.EnsureCompleteChoose(questID, new string[] {item});
                }
                Core.ToBank(Rewards);
            }
        }

        if (questID == 9000) //Unique Quarry
        {
            foreach (string item in Rewards)
            {
                Core.FarmingLogger(item, 1);
                while (!Bot.ShouldExit && !Core.CheckInventory(item))
                {
                    Core.EnsureAccept(questID);
                    Core.EquipClass(ClassType.Solo);
                    Core.BuyItem("venomvaults", 585, "Chaotic Manticore Head");
                    Core.HuntMonster("sandcastle", "Chaos Sphinx", "Chaos Sphinx", isTemp: false);
                    Core.HuntMonster("deepchaos", "Kathool", "Kathool Annihilator", isTemp: false);
                    Core.HuntMonster("castleroof", "Chaos Dragon", "Chaos Dragon Slayer", isTemp: false);
                    Core.HuntMonster("mirrorportal", "Chaos Harpy", "HarpyHunter", isTemp: false);
                    Core.HuntMonster("orecavern", "Naga Baas", "Naga Baas Pet", isTemp: false);
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("chaoswar", "r2", "Spawn", "*", "Chaos Tentacle", 300, isTemp: false);
                    Adv.BuyItem("tercessuinotlim", 1951, "Chaoroot", 30);
                    Core.EnsureCompleteChoose(questID, new string[] {item});
                }
                Core.ToBank(Rewards);
            }
        }

        if (questID == 9001) //Wavering Illusions
        {
            foreach (string item in Rewards)
            {
                Core.FarmingLogger(item, 1);
                while (!Bot.ShouldExit && !Core.CheckInventory(item))
                {
                    Core.EnsureAccept(questID);
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("chaoscrypt", "Basement", "Left", "*", "Chaos Gem", 200, isTemp: false);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("chaoslab", "Chaos Artix", "Chaorrupted Light of Destiny", isTemp: false);
                    Core.HuntMonster("timespace", "Chaos Lord Iadoa", "Chaorrupted Hourglass", 20, isTemp: false);
                    Core.HuntMonster("chaoskraken", "Chaos Kraken", "Chaotic Invertebrae", 20, isTemp: false);
                    Core.BuyItem("downbelow", 2004, "Chaos PuppetMaster");
                    Core.EnsureCompleteChoose(questID, new string[] {item});
                }
                Core.ToBank(Rewards);
            }
        }

        if (questID == 9002) //Shadows of Disdain
        {
            foreach (string item in Rewards)
            {
                Core.FarmingLogger(item, 1);
                while (!Bot.ShouldExit && !Core.CheckInventory(item))
                {
                    Core.EnsureAccept(questID);
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("mountdoomskull", "b1", "Left", "*", "Chaos War Medal", 1000, isTemp: false);
                    Adv.BuyItem("transformation", 2002, "Chaorrupted Usurper");
                    CAV.FragmentsoftheLordsA();
                    CAV.FragmentsoftheLordsB();
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("finalshowdown", "Prince Drakath", "Drakath Pet", isTemp: false);
                    Core.HuntMonster("ultradrakath", "Champion of Chaos", "Trace of Chaos", 13, isTemp: false);
                    Core.EnsureCompleteChoose(questID, new string[] {item});
                }
                Core.ToBank(Rewards);
            }
        }

        if (questID == 9003) //Persisting Mayhem
        {
            Core.EquipClass(ClassType.Solo);
            foreach (string item in Rewards)
            {
                Core.EnsureAccept(questID);
                while (!Bot.ShouldExit && !Core.CheckInventory(item))
                    Core.HuntMonster("ultradrakath", "Champion of Chaos", "Trace of Chaos", 13, isTemp: false);
                Core.EnsureCompleteChoose(questID, new string[] {item});
            }
            Core.ToBank(Rewards);
        }
    }

    private string[] QuestsRewards =
    {
        "Hollowborn Chaos Warrior",
        "Hollowborn Chaos Morph",
        "Hollowborn Chaotic Wings",
        "Hollowborn Omega Blades",
        "Hollowborn Omega Sword",
        "Hollowborn Chaos Unlocker",
        "Hollowborn Benevolent Locks",
        "Hollowborn Malignant Locks",
        "Hollowborn Face of Chaos",
        "Hollowborn Gaze of Chaos",
        "Hollowborn Eye of Chaos",
        "Hollowborn Wings of Chaos",
        "Hollowborn Chaotic Portal",
        "Hollowborn Envoy of Chaos",
        "Hollowborn Alignment Aspects",
        "Hollowborn Blade of Chaos Cape",
        "Hollowborn Chaotic Portal",
        "Hollowborn Blade of Chaos",
        "Hollowborn Claw of Chaos",
        "Hollowborn Benevolent",
        "Hollowborn Benevolent Morph",
        "Hollowborn Malignant",
        "Hollowborn Malignant Morph",
        "Idle Hollowborn Envoy of Chaos",
        "Hollowborn Blade of Chaos",
        "Hollowborn Blades of Chaos",
        "Hollowborn Omega Swords",
        "Hollowborn Chaos Unlockers"
    };

    private string[] Quest1 =
    {
        "Hollowborn Chaos Warrior",
        "Hollowborn Chaos Morph",
        "Hollowborn Chaotic Wings"
    };

    private string[] Quest2 =
    {
        "Hollowborn Omega Blades",
        "Hollowborn Omega Sword",
        "Hollowborn Chaos Unlocker"
    };

    private string[] Quest3 =
    {
        "Hollowborn Benevolent Locks",
        "Hollowborn Malignant Locks",
        "Hollowborn Face of Chaos",
        "Hollowborn Gaze of Chaos"
    };

    private string[] Quest4 =
    {
        "Hollowborn Eye of Chaos",
        "Hollowborn Wings of Chaos",
        "Hollowborn Chaotic Portal"
    };

    private string[] Quest5 =
    {
        "Hollowborn Envoy of Chaos",
        "Hollowborn Alignment Aspects",
        "Hollowborn Blade of Chaos Cape",
        "Hollowborn Chaotic Portal",
        "Hollowborn Blade of Chaos",
        "Hollowborn Claw of Chaos"
    };

    private string[] Quest6 =
    {
        "Hollowborn Benevolent",
        "Hollowborn Benevolent Morph",
        "Hollowborn Malignant",
        "Hollowborn Malignant Morph",
        "Idle Hollowborn Envoy of Chaos",
        "Hollowborn Blade of Chaos",
        "Hollowborn Blades of Chaos",
        "Hollowborn Omega Swords",
        "Hollowborn Chaos Unlockers"
    };
}