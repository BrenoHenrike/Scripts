//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class ArmyLegionToken
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    public CoreLegion Legion = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyLegionToken";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        new Option<Method>("Method", "Which method to get LTs?", "Choose your method", Method.Dreadrock),
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Loot);

        Core.SetOptions(disableClassSwap: false);
        bot.Options.RestPackets = false;

        Setup(Bot.Config.Get<Method>("Method"), 25001);

        Core.SetOptions(false);
    }


    public void Setup(Method Method, int quant = 25000)
    {
        Legion.JoinLegion();

        Core.EquipClass(ClassType.Farm);

        if (Method.ToString() == "Dreadrock")
        {
            Core.BuyItem("underworld", 216, "Undead Champion");
            GetItem("dreadrock", new[] { "Fallen Hero", "Hollow Wraith", "Legion Sentinel", "Shadowknight", "Void Mercenary" }, 4849, "Legion Token", quant);
        }

        if (Method.ToString() == "Shogun_Paragon_Pet")
        {
            if (!Core.CheckInventory("Shogun Paragon Pet"))
                Core.Logger("Pet not owned, stopping", stopBot: true);
            GetItem("fotia", new[] { "Fotia Elemental", "Fotia Spirit" }, 5755, "Legion Token", quant);
        }

        if (Method.ToString() == "Thanatos_Paragon_Pet")
        {
            if (!Core.CheckInventory("Thanatos Paragon Pet"))
                Core.Logger("Pet not owned, stopping", stopBot: true);

            Adv.BestGear(GearBoost.Dragonkin);
            GetItem("dragonheart", new[] { "Zombie Dragon" }, 4100, "Legion Token", quant);
        }

        if (Method.ToString() == "Bright_Paragon_Pet")
        {
            if (!Core.CheckInventory("Bright Paragon Pet"))
                Core.Logger("Pet not owned, stopping", stopBot: true);
            Core.RegisterQuests(4703);
            Adv.BestGear(GearBoost.dmgAll);
            GetItem("brightfortress", "r3", "Right", new[] { "*" }, 4704, "Legion Token", quant);
            Core.CancelRegisteredQuests();

        }

        if (Method.ToString() == "Arcane_Paragon_Pet")
        {
            if (!Core.CheckInventory(" Arcane_Paragon_Pet"))
                Core.Logger("Pet not owned, stopping", stopBot: true);
            Adv.BestGear(GearBoost.dmgAll);
            while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
            {
                GetItem("dragonheart", new[] { "Granite Dracolich" }, 4896, "Granite Dracolich Soul", 4, isTemp: false);
                GetItem("dragonheart", new[] { "Tempest Dracolich" }, 4896, "Tempest Dracolich Soul", 4, isTemp: false);
                GetItem("dragonheart", new[] { "Inferno Dracolich" }, 4896, "Inferno Dracolich Soul", 4, isTemp: false);
                GetItem("dragonheart", new[] { "Deluge Dracolich" }, 4896, "Deluge Dracolich Soul", 4, isTemp: false);
            }
        }

        if (Method.ToString() == "Paragon_Dreadnaught_Pet")
        {
            if (!Core.CheckInventory("Paragon_Dreadnaught_Pet"))
                Core.Logger("Pet not owned, stopping", stopBot: true);
            Adv.BestGear(GearBoost.dmgAll);

            while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
            {
                GetItem("laken", new[] { "Augmented Guard" }, 5741, "Stolen Guard", 5);
                GetItem("laken", new[] { "Cyborg Dog" }, 5741, "Stolen Dog", 6);
                GetItem("laken", new[] { "Mad Scientist" }, 5741, "Taken Axe", 10);
            }

        }

        if (Method.ToString() == "Mounted_Paragon_Pet")
        {
            if (!Core.CheckInventory("Mounted_Paragon_Pet"))
                Core.Logger("Pet not owned, stopping", stopBot: true);
            Adv.BestGear(GearBoost.dmgAll);
            GetItem("frozentower", new[] { "Ice Wolf" }, 5604, "Legion Token", quant);

        }

        if (Method.ToString() == " Ascended_Paragon_Pet")
        {
            if (!Core.CheckInventory(" Ascended_Paragon_Pet"))
                Core.Logger("Pet not owned, stopping", stopBot: true);
            Adv.BestGear(GearBoost.dmgAll);
            while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
            {
                GetItem("tournament", new[] { "Lord Brentan" }, 2747, "Lord Brentan's Regal Blade");
                GetItem("tournament", new[] { "Roderick" }, 2747, "Roderick's Chaotic Bane");
                GetItem("tournament", new[] { "Knight of Thorns" }, 2747, "Knight of Thorns' Sword");
                GetItem("tournament", new[] { "Johann Wryce" }, 2747, "Platinum of Johann Wryce");
                GetItem("tournament", new[] { "Khai Kaldun" }, 2747, "Khai Kaldun's Scimitar");
            }
        }

        if (Method.ToString() == "Festive_Paragon_Dracolich_Rider")
        {
            if (!Core.isSeasonalMapActive("frozenruins"))
                return;

            if (!Core.CheckInventory("Festive_Paragon_Dracolich_Rider"))
                Core.Logger("Pet not owned, stopping", stopBot: true);
            Adv.BestGear(GearBoost.dmgAll);
            Core.RegisterQuests(3968);
            GetItem("frozenruins", new[] { "Frost Fangbeast" }, 3969, "Legion Token", quant);
            Core.CancelRegisteredQuests();

        }

        if (Method.ToString() == "Holiday_Paragon_Pet")
        {
            if (!Core.CheckInventory("Holiday_Paragon_Pet"))
                Core.Logger("Pet not owned, stopping", stopBot: true);
            Adv.BestGear(GearBoost.dmgAll);
            while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
            {
                GetItem("prison", new[] { "King Alteon's Knight" }, 3256, "Spirit of Loyalty", 6);
                GetItem("battlewedding", new[] { "Silver Knight" }, 3256, "Spirit of Love", 6);
                GetItem("lycan", new[] { "Lycan Knight" }, 3256, "Spirit of Love", 6);
            }

        }

        if (Method.ToString() == "UW3017_Pet")
        {
            if (!Core.CheckInventory("UW3017_Pet"))
                Core.Logger("Pet not owned, stopping", stopBot: true);
            Adv.BestGear(GearBoost.dmgAll);
            GetItem("underworld", new[] { "Bloodfiend" }, 5738, "Legion Token", quant);

        }

        if (Method.ToString() == "Infernal_Caladbolg")
        {
            if (!Core.CheckInventory("Infernal_Caladbolg"))
                Core.Logger("Pet not owned, stopping", stopBot: true);
            Adv.BestGear(GearBoost.dmgAll);
            while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
            {
                GetItem("fotia", new[] { "Fotia Elemental" }, 3722, "Betrayer Extinguished", 5);
                GetItem("evilwardage", new[] { "Dreadfiend of Nulgath" }, 3722, "Fiend Felled", 2);
            }

        }

        if (Method.ToString() == "Hardcore_Paragon_Pet")
        {
            if (!Core.CheckInventory("Hardcore_Paragon_Pet"))
                Core.Logger("Pet not owned, stopping", stopBot: true);
            Adv.BestGear(GearBoost.dmgAll);
            while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
            {
                if (!Bot.Quests.IsUnlocked(793))
                    GetItem("doomvault", new[] { "Binky" }, 3393, "Legion Token", quant);
                else
                    GetItem("chaosboss", new[] { "Ultra Chaos Warlord" }, 3394, "Legion Token", quant);
            }
        }

        else
        {
            Core.EquipClass(ClassType.Solo);
            GetItem("legionarena", new[] { "Legion Fiend Rider" }, 6743, "Legion Token", quant);
        }
    }

    public void GetItem(string map = null, string[] monsters = null, int questID = 0000, string item = null, int quant = 0, bool isTemp = true)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Quest QuestData = Core.EnsureLoad(questID);
        ItemBase[] RequiredItems = QuestData.Requirements.ToArray();
        ItemBase[] QuestReward = QuestData.Rewards.ToArray();

        Core.AddDrop(item);
        if (!Bot.Quests.Active.Contains(QuestData))
            Core.RegisterQuests(questID);

        foreach (string monster in monsters)
            Army.SmartAggroMonStart(map, monster);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            if (monsters == new[] { "Binky" })
                Core.HuntMonster("Doomvault", "Binky");
            else Bot.Combat.Attack("*");
        }

        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }

    public void GetItem(string map = null, string cell = null, string pad = null, string[] monsters = null, int questID = 0000, string item = null, int quant = 0, bool isTemp = true)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Quest QuestData = Core.EnsureLoad(questID);
        ItemBase[] RequiredItems = QuestData.Requirements.ToArray();
        ItemBase[] QuestReward = QuestData.Rewards.ToArray();

        Core.AddDrop(item);
        if (!Bot.Quests.Active.Contains(QuestData))
            Core.RegisterQuests(questID);

        foreach (string monster in monsters)
            Army.SmartAggroMonStart(map, monster);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            if (monsters == new[] { "Binky" })
                Core.HuntMonster("Doomvault", "Binky");
            else Bot.Combat.Attack("*");
        }

        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }


    private string[] Loot = { "Legion Token" };
    public enum Method
    {
        Dreadrock = 0,
        Bright_Paragon_Pet = 1,
        Shogun_Paragon_Pet = 2,
        Thanatos_Paragon_Pet = 3,
        Arcane_Paragon_Pet = 4,
        Paragon_Dreadnaught_Pet = 5,
        Mounted_Paragon_Pet = 6,
        Ascended_Paragon_Pet = 7,
        Festive_Paragon_Dracolich_Rider = 8,
        Holiday_Paragon_Pet = 9,
        UW3017_Pet = 10,
        Infernal_Caladbolg = 11,
        Hardcore_Paragon_Pet = 12,
        Legion_Arena = 13,
    }
}