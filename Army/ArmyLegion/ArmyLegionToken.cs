/*
name: Army Legion Token
description: Uses an army, and method select to farm Legion tokens
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
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Legion.JoinLegion();

        switch (Method.ToString())
        {
            case "Dreadrock":
                Adv.BuyItem("underworld", 216, "Undead Champion");

                Core.EquipClass(ClassType.Farm);
                Core.RegisterQuests(4849);
                GetItem("dreadrock", "Fallen Hero", "Legion Token", quant);
                break;

            case "Shogun_Paragon_Pet":
                if (!Core.CheckInventory("Shogun Paragon Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);


                Core.EquipClass(ClassType.Farm);
                Adv.BestGear(GearBoost.dmgAll);

                if (Core.CheckInventory("Infernal Caladbolg"))
                    Core.RegisterQuests(3722, 5755);
                else Core.RegisterQuests(3722);
                while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
                {
                    GetItem("fotia", "Fotia Elemental", "Nothing Heard", 10);
                    GetItem("fotia", "Fotia Elemental", "Nothing to See", 10);
                    GetItem("fotia", "Fotia Elemental", "Area Secured and Quiet", 10);

                    if (Core.CheckInventory("Infernal Caladbolg"))
                    {
                        GetItem("fotia", "Fotia Elemental", "Betrayer Extinguished", 5);
                        GetItem("evilwardage", "Dreadfiend of Nulgath", "Fiend Felled", 2);
                    }
                }
                break;

            case "Thanatos_Paragon_Pet":
                if (!Core.CheckInventory("Thanatos Paragon Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Solo);
                Adv.BestGear(GearBoost.Dragonkin);

                Core.RegisterQuests(4100);
                GetItem("dragonheart", "Zombie Dragon", "Legion Token", quant);
                break;

            case "Bright_Paragon_Pet":
                if (!Core.CheckInventory("Bright Paragon Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Farm);
                Adv.BestGear(GearBoost.dmgAll);

                Core.RegisterQuests(4704);
                GetItem("brightfortress", "Brightscythe Reaver", "Legion Token", quant);
                break;

            case "Arcane_Paragon_Pet":
                if (!Core.CheckInventory("Arcane Paragon Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Farm);
                Adv.BestGear(GearBoost.dmgAll);

                Core.RegisterQuests(4896);
                while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
                {
                    GetItem("dragonheart", "Granite Dracolich", "Granite Dracolich Soul", 4, isTemp: false);
                    GetItem("dragonheart", "Tempest Dracolich", "Tempest Dracolich Soul", 4, isTemp: false);
                    GetItem("dragonheart", "Inferno Dracolich", "Inferno Dracolich Soul", 4, isTemp: false);
                    GetItem("dragonheart", "Deluge Dracolich", "Deluge Dracolich Soul", 4, isTemp: false);
                }
                break;

            case "Paragon_Dreadnaught_Pet":
                if (!Core.CheckInventory("Paragon Dreadnaught Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Farm);
                Adv.BestGear(GearBoost.dmgAll);

                Core.RegisterQuests(5741);
                while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
                {
                    GetItem("laken", "Augmented Guard", "Stolen Guard", 5);
                    GetItem("laken", "Cyborg Dog", "Stolen Dog", 6);
                    GetItem("laken", "Mad Scientist", "Taken Axe", 10);
                }
                break;

            case "Mounted_Paragon_Pet":
                if (!Core.CheckInventory("Mounted Paragon Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Farm);
                Adv.BestGear(GearBoost.dmgAll);

                Core.RegisterQuests(5604);
                GetItem("frozentower", "Ice Wolf", "Legion Token", quant);
                break;

            case "Ascended_Paragon_Pet":
                if (!Core.CheckInventory("Ascended Paragon Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Solo);
                Adv.BestGear(GearBoost.dmgAll);

                Core.RegisterQuests(2747);
                while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
                {
                    GetItem("tournament", "Lord Brentan", "Lord Brentan's Regal Blade", 1);
                    GetItem("tournament", "Roderick", "Roderick's Chaotic Bane", 1);
                    GetItem("tournament", "Knight of Thorns", "Knight of Thorns' Sword", 1);
                    GetItem("tournament", "Johann Wryce", "Platinum of Johann Wryce", 1);
                    GetItem("tournament", "Khai Kaldun", "Khai Kaldun's Scimitar", 1);
                }
                break;

            case "Festive_Paragon_Dracolich_Rider":
                if (!Core.CheckInventory("Festive Paragon Dracolich Rider"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);
                if (!Core.isSeasonalMapActive("frozenruins"))
                    return;

                Core.EquipClass(ClassType.Solo);
                Adv.BestGear(GearBoost.dmgAll);

                Core.RegisterQuests(3969);
                GetItem("frozenruins", "Frost Fangbeast", "Legion Token", quant);
                break;

            case "Holiday_Paragon_Pet":
                if (!Core.CheckInventory("Holiday Paragon Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Farm);
                Adv.BestGear(GearBoost.dmgAll);

                Core.RegisterQuests(3256);
                while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
                {
                    GetItem("prison", "King Alteon's Knight", "Spirit of Loyalty", 6);
                    GetItem("battlewedding", "Silver Knight", "Spirit of Love", 6);
                    GetItem("lycan", "Lycan Knight", "Spirit of Love", 6);
                }
                break;

            case "UW3017_Pet":
                if (!Core.CheckInventory("UW3017 Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Farm);
                Adv.BestGear(GearBoost.dmgAll);
                Core.RegisterQuests(5738);
                GetItem("underworld", "Bloodfiend", "Legion Token", quant);
                break;

            case "Infernal_Caladbolg":
                if (!Core.CheckInventory("Infernal Caladbolg"))
                    Core.Logger("Sword not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Farm);
                Adv.BestGear(GearBoost.dmgAll);

                if (Core.CheckInventory("Shogun Paragon Pet"))
                    Core.RegisterQuests(3722, 5755);
                else Core.RegisterQuests(3722);

                while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
                {
                    if (Core.CheckInventory("Shogun Paragon Pet"))
                    {
                        GetItem("fotia", "Fotia Elemental", "Nothing Heard", 10);
                        GetItem("fotia", "Fotia Elemental", "Nothing to See", 10);
                        GetItem("fotia", "Fotia Elemental", "Area Secured and Quiet", 10);
                    }

                    GetItem("fotia", "Fotia Elemental", "Betrayer Extinguished", 5);
                    GetItem("evilwardage", "Dreadfiend of Nulgath", "Fiend Felled", 2);
                }
                break;

            case "Hardcore_Paragon_Pet":
                if (!Core.CheckInventory("Hardcore Paragon Pet"))
                    Core.Logger("Pet not owned, stopping", stopBot: true);

                Core.EquipClass(ClassType.Solo);
                Adv.BestGear(GearBoost.dmgAll);

                Core.RegisterQuests(!Bot.Quests.IsUnlocked(793) ? 3393 : 3394);
                GetItem(!Bot.Quests.IsUnlocked(793) ? "doomvault" : "chaosboss", !Bot.Quests.IsUnlocked(793) ? "Binky" : "Ultra Chaos Warlord", "Legion Token", quant);
                break;

            default:
                Core.EquipClass(ClassType.Solo);
                Core.RegisterQuests(0000);
                GetItem("legionarena", "Legion Fiend Rider", "Legion Token", quant);
                break;
        }
    }

    public void GetItem(string map = null, string monster = null, string item = null, int quant = 1, bool isTemp = true)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        // Quest QuestData = Core.EnsureLoad(questID);
        // ItemBase[] RequiredItems = QuestData.Requirements.ToArray();
        // ItemBase[] QuestReward = QuestData.Rewards.ToArray();

        Core.AddDrop(item);
        // if (!Bot.Quests.Active.Contains(QuestData))
        //     Core.RegisterQuests(questID);

        Army.SmartAggroMonStart(map, monster);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            if (monster == "Binky")
                Core.HuntMonster("Doomvault", "Binky");
            else Bot.Combat.Attack("*");
        }

        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }

    public void GetItem(string map = null, string cell = null, string pad = null, string[] monsters = null, string item = null, int quant = 0, bool isTemp = true)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        // Quest QuestData = Core.EnsureLoad(questID);
        // ItemBase[] RequiredItems = QuestData.Requirements.ToArray();
        // ItemBase[] QuestReward = QuestData.Rewards.ToArray();

        Core.AddDrop(item);
        // if (!Bot.Quests.Active.Contains(QuestData))
        //     Core.RegisterQuests(questID);

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
