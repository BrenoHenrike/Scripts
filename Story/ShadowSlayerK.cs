//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Story/Table.cs
//cs_include Scripts/Farm/BuyScrolls.cs
using Skua.Core.Interfaces;

public class ShadowSlayerK
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    public CoreDailies Daily = new();
    public CoreAdvanced Adv = new();
    public Core7DD DD = new();
    public Table Table = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(8835))
            return;

        Story.PreLoad();

        // 8263 | Knee Jerk Reaction
        Story.KillQuest(8263, "cellar", "GreenRat");

        // 8264 | Nice, But Not Quite...
        Story.KillQuest(8264, "castletunnels", "Blood Maggot");

        // 8265 | Alternative Solution
        Story.KillQuest(8265, "Odokuro", "O-dokuro");

        // 8266 | Compassion For A Companion
        if (!Story.QuestProgression(8266))
        {
            Core.EnsureAccept(8266);
            Daily.EldersBlood();
            if (!Core.CheckInventory("Holy Wasabi"))
            {
                Core.AddDrop("Holy Wasabi");
                Core.EnsureAccept(1075);

                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("doomwood", "Doomwood Ectomancer", "Dried Wasabi Powder", 4);
                Core.GetMapItem(428, 1, "lightguard");

                Core.EnsureComplete(1075);
                Bot.Wait.ForPickup("Holy Wasabi");
            }
            Adv.BuyItem("alchemyacademy", 2036, "Sage Tonic", 3, 10);
            DD.HazMatSuit();
            Core.HuntMonster("sloth", "Cured Phlegnn", "Unnatural Ooze", 8);
            Core.HuntMonster("beehive", "Killer Queen Bee", "Sleepy Honey");
            Core.EnsureComplete(8266);
        }


        // 8829 | Lend an Ear
        if (!Story.QuestProgression(8829))
        {
            Core.EnsureAccept(8829);
            Core.HuntMonster("arcangrove", "Gorillaphant", "Gorillaphant Ear");
            Core.HuntMonster("boxes", "Sneevil", "Sneevil Ear");
            Core.HuntMonster("terrarium", "Dustbunny of Doom", "Dustbunny of Doom Ear");
            Core.HuntMonster("uppercity", "Drow Assassin", "Drow Ear");
            Core.EnsureComplete(8829);
        }

        // 8830 | The Voice from Yesterday
        if (!Story.QuestProgression(8830))
        {
            Core.EnsureAccept(8830);
            Adv.BuyItem("Northpointe", 1085, "Dark Book");
            Core.HuntMonster("Maxius", "Ghoul Minion", "Crimson BoneLord Tome", isTemp: false);
            Core.HuntMonster("backroom", "Book Wyrm", "Book of Monsters Mace", isTemp: false);
            Adv.BuyItem("chronohub", 2024, "Chronomancer's Opus");
            Core.EnsureComplete(8830);
        }

        // 8831 | Shadow Slayer Slayer
        Story.KillQuest(8831, "newfinale", "Shadow Slayer");

        // 8832 | Dinner for Two
        // Story.KillQuest(8832, "dragonchallenge", "Greenguard Dragon");
        // Story.KillQuest(8832, "battlefowl", "ChickenCow");
        // Story.KillQuest(8832, "pirates", "Shark Bait");
        // Story.KillQuest(8832, "greenguardwest", "Big Bad Boar");
        // Story.KillQuest(8832, "trunk", "Greenguard Basilisk");
        // Story.KillQuest(8832, "Well", "Gell Oh No");
        // Story.KillQuest(8832, "deathgazer", "Deathgazer");
        // Story.KillQuest(8832, "river", "Kuro");
        if (!Story.QuestProgression(8832))
        {
            Core.EnsureAccept(8832);
            Core.HuntMonster("dragonchallenge", "Greenguard Dragon", "Greenguard Dragon Ribs");
            Core.HuntMonster("battlefowl", "ChickenCow", "Chickencow Wings");
            Core.HuntMonster("pirates", "Shark Bait", "Shark Bait Fillet");
            Core.HuntMonster("greenguardwest", "Big Bad Boar", "Big Bad Boar Sausage");
            Core.HuntMonster("trunk", "Greenguard Basilisk", "Greenguard Basilisk Tail");
            Core.HuntMonster("Well", "Gell Oh No", "Gell Oh No Jello");
            Core.HuntMonster("deathgazer", "Deathgazer", "Deathgazer Takoyaki");
            Core.HuntMonster("river", "Kuro", "Kuro Geso Karaage");
            Core.EnsureComplete(8832);
        }

        // 8833 | Preparedness Awareness
        Story.BuyQuest(8833, "arcangrove", 211, "Health Potion");
        Story.BuyQuest(8833, "arcangrove", 211, "Mana Potion");
        Story.KillQuest(8833, "cleric", "Chaos Dragon");

        // 8834 | Quality Tea Time
        if (!Story.QuestProgression(8834))
        {
            if (!Core.CheckInventory("Tea Cup (Mem)"))
            {
                Core.EnsureAccept(8834);
                Table.DoAll();
                while (!Bot.ShouldExit && !Core.CheckInventory("Racing Trophy", 100))
                    Core.ChainComplete(746);
                Core.HuntMonster("table", "Roach", "Gold Roach Wing", 4);
                Core.EnsureComplete(741, 5401);
            }
            Story.KillQuest(8834, "sleuthhound", "Chair");
            Story.KillQuest(8834, "guru", "Wisteria");
            Story.KillQuest(8834, "hachiko", "Samurai Nopperabo");
            Story.KillQuest(8834, "elemental", "Tree of Destiny");
            Core.EnsureComplete(8834);
        }

        // 8835 | Shadowslayer Summoning Ritual
        if (!Story.QuestProgression(8835))
        {
            Core.EnsureAccept(8835);
            Core.HuntMonster("tercessuinotlim", "Dark makai", "Mystic Parchment", 4, isTemp: false);
            Core.BuyItem("spellcraft", 549, "fading Ink", 45, 5);
            Core.BuyItem("spellcraft", 549, "Elemenatal Ink", 30, 5);
            while (!Bot.ShouldExit && Core.CheckInventory("Scroll of Spirit Rend", 30) && Core.CheckInventory("Scroll of Eclipse", 15) && Core.CheckInventory("Scroll of Blessed Shard", 30))
                Core.ChainComplete(2309);
            while (!Bot.ShouldExit && Core.CheckInventory("Scroll of Eclipse", 15))
                Core.ChainComplete(2312);
            while (!Bot.ShouldExit && Core.CheckInventory("Scroll of Blessed Shard", 30))
                Core.ChainComplete(2317);
            Core.EnsureComplete(8835);
        }
    }
}
