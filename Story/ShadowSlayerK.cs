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
    public BuyScrolls Scroll = new();

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

        Story.PreLoad(this);
        Core.AddDrop("Shadowslayer Apprentice Badge", "Dairy Ration", "Grain Ration", "Meat Ration", "Holy Wasabi", "Racing Trophy");

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
            Bot.Quests.UpdateQuest(8060);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("backroom", "Book Wyrm", "Book of Monsters Mace", isTemp: false);
            Core.BuyItem("chronohub", 2024, "Chronomancer's Opus");
            Core.EnsureComplete(8830);
        }

        // 8831 | Shadow Slayer Slayer
        Story.KillQuest(8831, "newfinale", "Shadow Slayer");

        // 8832 | Dinner for Two
        if (!Story.QuestProgression(8832))
        {
            Core.EnsureAccept(8832);
            Core.HuntMonster("dragonchallenge", "Greenguard Dragon", "Greenguard Dragon Ribs", log: false);
            Core.HuntMonster("battlefowl", "ChickenCow", "Chickencow Wings", log: false);
            Core.HuntMonster("pirates", "Shark Bait", "Shark Bait Fillet", log: false);
            Core.HuntMonster("greenguardwest", "Big Bad Boar", "Big Bad Boar Sausage", log: false);
            Core.HuntMonster("trunk", "Greenguard Basilisk", "Greenguard Basilisk Tail", log: false);
            Core.HuntMonster("Well", "Gell Oh No", "Gell Oh No Jello", log: false);
            Core.HuntMonster("deathgazer", "Deathgazer", "Deathgazer Takoyaki", log: false);
            Core.HuntMonster("river", "Kuro", "Kuro Geso Karaage", log: false);
            Core.EnsureComplete(8832);
        }

        // 8833 | Preparedness Awareness
        if (!Story.QuestProgression(8833))
        {
            Core.EnsureAccept(8833);
            Core.BuyItem("arcangrove", 211, "Health Potion", 25);
            Core.BuyItem("arcangrove", 211, "Mana Potion", 25);
            Core.HuntMonster("cleric", "Chaos Dragon", "Medicinal Unguent");
            Core.EnsureComplete(8833);
        }

        // 8834 | Quality Tea Time
        if (!Story.QuestProgression(8834))
        {
            Core.EquipClass(ClassType.Farm);
            Core.EnsureAccept(8834);
            if (!Core.CheckInventory("Tea Cup (Mem)"))
            {
                Table.DoAll();
                while (!Bot.ShouldExit && !Core.CheckInventory("Racing Trophy", 100))
                    Core.ChainComplete(746);
                Core.EnsureAccept(741);
                Core.HuntMonster("table", "Roach", "Gold Roach Leg", 10);
                Core.EnsureComplete(741, 5401);
            }
            Core.HuntMonster("sleuthhound", "Chair", "Rich Tea Leaves");
            Core.HuntMonster("guru", "Wisteria", "Fragrant Wisteria Bloom");
            Core.HuntMonster("hachiko", "Samurai Nopperabo", "Bitter Matcha");
            Story.KillQuest(8834, "elemental", "Tree of Destiny", false);
        }


        // 8835 | Shadowslayer Summoning Ritual
        if (!Story.QuestProgression(8835))
        {
            if (!Core.CheckInventory("ShadowSlayer's Apprentice"))
            {
                Core.HuntMonster("chaosbeast", "Kathool", "Chibi Eldritch Yume", isTemp: false);
                Core.EnsureAccept(8266);
                Daily.EldersBlood();
                if (!Core.CheckInventory("Holy Wasabi"))
                {
                    Core.EnsureAccept(1075);

                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("doomwood", "Doomwood Ectomancer", "Dried Wasabi Powder", 4);
                    Core.GetMapItem(428, 1, "lightguard");

                    Core.EnsureComplete(1075);
                    Bot.Wait.ForPickup("Holy Wasabi");
                }
                Adv.BuyItem("alchemyacademy", 2036, "Sage Tonic", 3, 10);
                DD.HazMatSuit();
                Core.HuntMonster("sloth", "Phlegnn", "Unnatural Ooze", 8);
                Core.HuntMonster("beehive", "Killer Queen Bee", "Sleepy Honey");
                Core.EnsureComplete(8266);
                Core.BuyItem("safiria", 2044, "ShadowSlayer's Apprentice");
            }

            Core.EnsureAccept(8835);
            Scroll.BuyScroll(BuyScrolls.Scrolls.SpiritRend, 30);
            Scroll.BuyScroll(BuyScrolls.Scrolls.Eclipse, 15);
            Scroll.BuyScroll(BuyScrolls.Scrolls.BlessedShard, 30);
            if (!Core.CheckInventory("Meat Ration"))
            {
                Core.EnsureAccept(8263);
                Core.HuntMonster("cellar", "GreenRat", "Green Mystery Meat", 10, log: false);
                Core.EnsureComplete(8263);
                Bot.Wait.ForPickup("Meat Ration");
            }
            Core.RegisterQuests(8264);
            while (!Bot.ShouldExit && !Core.CheckInventory("Grain Ration", 2))
            {
                Core.HuntMonster("castletunnels", "Blood Maggot", "Bundle of Rice", 3, log: false);
                Bot.Wait.ForPickup("Grain Ration");
            }
            Core.CancelRegisteredQuests();
            if (!Core.CheckInventory("Dairy Ration"))
            {
                Core.EnsureAccept(8265);
                Core.KillMonster("odokuro", "Boss", "Right", "O-dokuro", "Bone Hurt Juice", 5);
                Core.EnsureComplete(8265);
                Bot.Wait.ForPickup("Dairy Ration");
            }
            Core.EnsureComplete(8835);
        }
    }
}
