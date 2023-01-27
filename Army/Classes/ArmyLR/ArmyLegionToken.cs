/*
name:  Army Legion Token
description:  Uses an army, and method select to farm Legion tokens
tags: army, legion, legion tokens, dreadrock, pet, bright paragon pet, shogun paragon pet, thanatos paragon pet, arcane paragon pet, paragon dreadnaught pet, munted paragon pet, ascended paragon pet, festive paragon dracolich rider, holiday paragon pet, uw3017 pet, infernal caladbolg, hardcore paragon pet, legion arena
*/
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
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    public CoreLegion Legion = new();

    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyLegionToken";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
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
        Core.BankingBlackList.Add("Legion Token");
        Core.SetOptions(disableClassSwap: false);

        Setup(Bot.Config.Get<Method>("Method"), 25001);
    }


    public void Setup(Method Method, int quant = 25000)
    {
        Legion.JoinLegion();

        switch (Method.ToString())
        {
            case "Dreadrock":
                Core.BuyItem("underworld", 216, "Undead Champion");
                Core.EquipClass(ClassType.Farm);

                GetItem("dreadrock", new[] { "Fallen Hero", "Hollow Wraith", "Legion Sentinel", "Shadowknight", "Void Mercenary" }, 4849, "Legion Token", quant);
                break;

            case "Shogun_Paragon_Pet":
                if (!Core.CheckInventory("Shogun Paragon Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                if (Core.CheckInventory("Infernal Caladbolg"))
                    Core.RegisterQuests(3722);

                Core.EquipClass(ClassType.Farm);
                Adv.BestGear(GearBoost.dmgAll);

                while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
                {
                    GetItem("fotia", new[] { "Fotia Elemental", "Fotia Spirit" }, 5755, "Nothing Heard", 10);
                    GetItem("fotia", new[] { "Fotia Elemental", "Fotia Spirit" }, 5755, "Nothing to See", 10);
                    GetItem("fotia", new[] { "Fotia Elemental", "Fotia Spirit" }, 5755, "Area Secured and Quiet", 10);

                    if (Core.CheckInventory("Infernal Caladbolg"))
                    {
                        GetItem("fotia", new[] { "Fotia Elemental" }, 3722, "Betrayer Extinguished", 5);
                        GetItem("evilwardage", new[] { "Dreadfiend of Nulgath" }, 3722, "Fiend Felled", 2);
                    }
                }
                break;

            case "Thanatos_Paragon_Pet":
                if (!Core.CheckInventory("Thanatos Paragon Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Solo);
                Adv.BestGear(GearBoost.Dragonkin);

                GetItem("dragonheart", new[] { "Zombie Dragon" }, 4100, "Legion Token", quant);
                break;

            case "Bright_Paragon_Pet":
                if (!Core.CheckInventory("Bright Paragon Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Farm);
                Adv.BestGear(GearBoost.dmgAll);

                GetItem("brightfortress", "r3", "Right", new[] { "*" }, 4704, "Legion Token", quant);
                break;

            case "Arcane_Paragon_Pet":
                if (!Core.CheckInventory("Arcane Paragon Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Farm);
                Adv.BestGear(GearBoost.dmgAll);

                while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
                {
                    GetItem("dragonheart", new[] { "Granite Dracolich" }, 4896, "Granite Dracolich Soul", 4, isTemp: false);
                    GetItem("dragonheart", new[] { "Tempest Dracolich" }, 4896, "Tempest Dracolich Soul", 4, isTemp: false);
                    GetItem("dragonheart", new[] { "Inferno Dracolich" }, 4896, "Inferno Dracolich Soul", 4, isTemp: false);
                    GetItem("dragonheart", new[] { "Deluge Dracolich" }, 4896, "Deluge Dracolich Soul", 4, isTemp: false);
                }
                break;

            case "Paragon_Dreadnaught_Pet":
                if (!Core.CheckInventory("Paragon Dreadnaught Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Farm);
                Adv.BestGear(GearBoost.dmgAll);

                while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
                {
                    GetItem("laken", new[] { "Augmented Guard" }, 5741, "Stolen Guard", 5);
                    GetItem("laken", new[] { "Cyborg Dog" }, 5741, "Stolen Dog", 6);
                    GetItem("laken", new[] { "Mad Scientist" }, 5741, "Taken Axe", 10);
                }
                break;

            case "Mounted_Paragon_Pet":
                if (!Core.CheckInventory("Mounted Paragon Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Farm);
                Adv.BestGear(GearBoost.dmgAll);

                GetItem("frozentower", new[] { "Ice Wolf" }, 5604, "Legion Token", quant);
                break;

            case "Ascended_Paragon_Pet":
                if (!Core.CheckInventory("Ascended Paragon Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Solo);
                Adv.BestGear(GearBoost.dmgAll);

                while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
                {
                    GetItem("tournament", new[] { "Lord Brentan" }, 2747, "Lord Brentan's Regal Blade");
                    GetItem("tournament", new[] { "Roderick" }, 2747, "Roderick's Chaotic Bane");
                    GetItem("tournament", new[] { "Knight of Thorns" }, 2747, "Knight of Thorns' Sword");
                    GetItem("tournament", new[] { "Johann Wryce" }, 2747, "Platinum of Johann Wryce");
                    GetItem("tournament", new[] { "Khai Kaldun" }, 2747, "Khai Kaldun's Scimitar");
                }
                break;

            case "Festive_Paragon_Dracolich_Rider":
                if (!Core.CheckInventory("Festive Paragon Dracolich Rider"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);
                if (!Core.isSeasonalMapActive("frozenruins"))
                    return;

                Core.EquipClass(ClassType.Solo);
                Adv.BestGear(GearBoost.dmgAll);

                GetItem("frozenruins", new[] { "Frost Fangbeast" }, 3969, "Legion Token", quant);
                break;

            case "Holiday_Paragon_Pet":
                if (!Core.CheckInventory("Holiday Paragon Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Farm);
                Adv.BestGear(GearBoost.dmgAll);

                while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
                {
                    GetItem("prison", new[] { "King Alteon's Knight" }, 3256, "Spirit of Loyalty", 6);
                    GetItem("battlewedding", new[] { "Silver Knight" }, 3256, "Spirit of Love", 6);
                    GetItem("lycan", new[] { "Lycan Knight" }, 3256, "Spirit of Love", 6);
                }
                break;

            case "UW3017_Pet":
                if (!Core.CheckInventory("UW3017 Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Farm);
                Adv.BestGear(GearBoost.dmgAll);

                GetItem("underworld", new[] { "Bloodfiend" }, 5738, "Legion Token", quant);
                break;

            case "Infernal_Caladbolg":
                if (!Core.CheckInventory("Infernal Caladbolg"))
                    Core.Logger("Sword not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Farm);
                Adv.BestGear(GearBoost.dmgAll);

                if (Core.CheckInventory("Shogun Paragon Pet"))
                    Core.RegisterQuests(3722);
                while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
                {
                    if (Core.CheckInventory("Shogun Paragon Pet"))
                    {
                        GetItem("fotia", new[] { "Fotia Elemental", "Fotia Spirit" }, 5755, "Nothing Heard", 10);
                        GetItem("fotia", new[] { "Fotia Elemental", "Fotia Spirit" }, 5755, "Nothing to See", 10);
                        GetItem("fotia", new[] { "Fotia Elemental", "Fotia Spirit" }, 5755, "Area Secured and Quiet", 10);
                    }

                    GetItem("fotia", new[] { "Fotia Elemental" }, 3722, "Betrayer Extinguished", 5);
                    GetItem("evilwardage", new[] { "Dreadfiend of Nulgath" }, 3722, "Fiend Felled", 2);
                }
                break;

            case "Hardcore_Paragon_Pet":

                if (!Core.CheckInventory("Hardcore Paragon Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Solo);
                Adv.BestGear(GearBoost.dmgAll);

                if (!Bot.Quests.IsUnlocked(793))
                    GetItem("doomvault", new[] { "Binky" }, 3393, "Legion Token", quant);
                else
                    GetItem("chaosboss", new[] { "Ultra Chaos Warlord" }, 3394, "Legion Token", quant);
                break;

            default:
                Core.EquipClass(ClassType.Solo);
                GetItem("legionarena", new[] { "Legion Fiend Rider" }, 6743, "Legion Token", quant);
                break;
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
