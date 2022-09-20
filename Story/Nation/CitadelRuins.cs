//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class CitadelRuins
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();
    public CoreStory Story = new CoreStory();

    public string[] rewards = { "Purified Claymore of Destiny", "Good Iron Wing 1", "Spinal Tap of Retribution", "Purified Claw of Nulgath", "Mage's Gratitude" };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        Farm.Experience(30);
        Farm.GoodREP();
        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        MurrysQuests();
        PolishsQuestsTercessuinotlim();
        PolishsQuestsCitadelRuins();
        CutieMakaisQuest();
        CasparillasQuests();
    }


    public void CasparillasQuests()
    {

        if (Core.isCompletedBefore(6682))
            return;

        Story.PreLoad(this);

        // Easy AND Breezy!
        Story.KillQuest(6669, "forest", "Boss Zardman");

        // Disguises and a Ruse
        Story.KillQuest(6671, "bludrut2", "Shadow Creeper");

        // Icky and Smelly
        Story.KillQuest(6672, "marsh2", "Thrax Ironhide");

        // Dress for the Occasion!
        Story.KillQuest(6673, "sleuthhound", "Harmoire");

        // Can't Play Games without A Lunch Break
        Story.KillQuest(6674, "noobshire", "Kittarian Mouse Eater");

        // She Needs a Hero
        if (!Story.QuestProgression(6675))
        {
            Bot.Quests.UpdateQuest(3008);
            Core.EnsureAccept(6675);
            Core.HuntMonsterMapID("doomvault", 21, "Your Princess is Not in This Castle");
            Core.EnsureComplete(6675);
        }

        // Sun and Fun
        Story.KillQuest(6676, "beachparty", "Sun Flare");

        // Heebie Jeebies
        Story.KillQuest(6677, "marsh", "Dreadspider");

        // A New Pet
        Story.KillQuest(6678, "iceplane", "Enfield");

        // Spoiled Dragon
        Story.KillQuest(6679, "lair", "Red Dragon");

        // The Unpredictable Element
        Story.KillQuest(6680, "ledgermayne", "Ledgermayne");

        // There, But Not There
        Story.KillQuest(6681, "palace", "Invisible");

        // Staying Humble
        Core.AddDrop("Elite Void Sword Pet");
        Story.KillQuest(6682, "underlair", "ArchFiend DragonLord");

    }

    public void CutieMakaisQuest()
    {
        if (Core.isCompletedBefore(4325))
            return;

        Story.PreLoad(this);

        // Drearia On Demand
        Story.MapItemQuest(4312, "drearia", 3485);
        Story.KillQuest(4312, "drearia", new[] { "Dark Makai", "Evil Elemental", "Green Rat" });

        // Plant a Little Seed and Nature Grows
        Story.KillQuest(4313, "drearia", "Dark Makai");

        // A Key Discovery
        Story.MapItemQuest(4314, "drearia", 3466);
        Story.KillQuest(4314, "drearia", "Green Rat");

        // Creepy House... Yay!
        Story.MapItemQuest(4315, "drearia", 3467);
        Story.KillQuest(4315, "drearia", "Green Rat");

        // Sparkling Books
        Story.MapItemQuest(4316, "drearia", 3468);

        // A Paladin in Peril
        Story.MapItemQuest(4317, "swordhavenpink", 3469);

        // Pink Stinks!
        Story.MapItemQuest(4318, "swordhavenpink", 3486, 5);
        Story.KillQuest(4318, "swordhavenpink", "Pink Slime");

        // Rats, RATS!
        Story.KillQuest(4319, "sewerpink", "Pink Rat");

        // AdventureQuest Worm
        Story.KillQuest(4320, "sewerpink", "Cutie Grumbley");

        // UnBEARable Sight
        Story.MapItemQuest(4321, "pinewoodpink", 3470);
        Story.KillQuest(4321, "pinewoodpink", "Pink Grizzly");

        // Too Much Pink in Pinewood!
        Story.MapItemQuest(4322, "pinewoodpink", 3471, 5);
        Story.KillQuest(4322, "pinewoodpink", "Pink Shell Turtle");

        // Kill Sparkletooth
        Story.KillQuest(4323, "pinewoodpink", "Sparkletooth");

        // The Citadorable Plot   
        Story.MapItemQuest(4324, "Citadel", 3472);

        // Fuzzy Run Minigame        
        Story.KillQuest(4325, "pinewoodpink", "Pink Grizzly");

    }

    public void MurrysQuests()
    {
        if (Core.isCompletedBefore(182))
            return;

        Story.PreLoad(this);

        // "Citadel"
        Story.KillQuest(144, "Citadel", "Inquisitor Guard");
        Story.KillQuest(145, "Citadel", "Crusader");
        Story.KillQuest(146, "Citadel", "Inquisitor Captain");
        Story.KillQuest(147, "Citadel", "Burning Witch");
        Story.KillQuest(148, "Citadel", "Inquisitor Guard");
        Story.KillQuest(149, "Citadel", "Inquisitor Guard");
        Story.KillQuest(181, "Citadel", "Belrot The Fiend");
        if (!Story.QuestProgression(182)) // 151 dcs you when u try to load it, and can be skipped.. somehow idk itd break otherwise.
        {
            Core.EnsureAccept(182);
            Core.HuntMonster("Citadel", "Grand Inquisitor", "Ring of Evil Intent");
            Core.EnsureComplete(182);
        }

    }

    public void PolishsQuestsTercessuinotlim()
    {
        if (Core.isCompletedBefore(668))
            return;

        Story.PreLoad(this);

        Core.AddDrop("Unidentified 9", "Unidentified 28", "Dark Crystal Shard", "Claw of Nulgath", "Relic of Chaos");

        // "Tercessuinotlim"

        //iron wing helm enchant=
        Nation.TheAssistant("Unidentified 9");
        Story.KillQuest(560, "underworld", "Undead Bruiser", GetReward: false);

        //cleansing of spinal tap=
        Nation.Supplies("Dark Crystal Shard", 5);
        Nation.TheAssistant("Unidentified 28");
        Story.KillQuest(585, "Tercessuinotlim", "Legion Fenrir");

        //purified claw
        Nation.Supplies("Tainted Gem", 7);
        Nation.Supplies("Claw of Nulgath");
        Story.KillQuest(668, "Tercessuinotlim", "Dark Makai");
    }

    public void PolishsQuestsCitadelRuins()
    {
        if (Core.isCompletedBefore(6182))
            return;

        Story.PreLoad(this);

        Core.AddDrop("Mage's Gratitude");
        // "citadelruins";

        //get ready to amplify
        Story.KillQuest(6172, "citadelruins", "Mana Sprites");
        Story.MapItemQuest(6172, "citadelruins", 5592);

        //break the seal
        Story.MapItemQuest(6173, "citadelruins", 5602);

        //clear out the squatters
        if (!Story.QuestProgression(6174))
        {
            Core.EnsureAccept(6174);
            Core.HuntMonster("citadelruins", "Inquisitor Hobo", "Inquisitor Hobos Defeated", 8);
            Core.HuntMonster("citadelruins", "Inquisitor Hobo", "Inquisitors' Manifesto", 5);
            Core.EnsureComplete(6174);
        }

        //grab a clue
        Story.KillQuest(6175, "citadelruins", "Inquisitor Hobo");
        Story.MapItemQuest(6175, "citadelruins", 5593, Amount: 5);
        Bot.Sleep(2500);

        //unlock the door
        if (!Story.QuestProgression(6176))
        {
            Core.EnsureAccept(6176);
            Core.HuntMonster("citadelruins", "Inquisitor Heavy", "Heavies Beat Down", 2);
            Core.HuntMonster("citadelruins", "Inquisitor Heavy", "Citadel Key");
            Story.MapItemQuest(6176, "citadelruins", 5603);
        }

        //search the labs
        Story.KillQuest(6177, "citadelruins", "Inquisitor Hobo");
        Story.MapItemQuest(6177, "citadelruins", new[] { 5594, 5595, 5596 });

        //break another seal
        Story.KillQuest(6178, "citadelruins", "Mana Sprites");
        Story.MapItemQuest(6178, "citadelruins", 5597);

        //inspect time
        Story.MapItemQuest(6179, "citadelruins", 5598);

        //find the parts
        Story.KillQuest(6180, "citadelruins", "Inquisitor Hobo");
        Story.MapItemQuest(6180, "citadelruins", new[] { 5599, 5600, 5601 });

        //defeat the grand inquisitor
        Story.KillQuest(6181, "citadelruins", "Grand Inquisitor Murry");

        //defeat enn'tropy
        Story.KillQuest(6182, "citadelruins", "Enn'tröpy");
        Core.ToBank(rewards);
    }
}