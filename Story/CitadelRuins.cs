//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class CitadelRuins
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNulgath Nulgath = new CoreNulgath();
    public CoreStory Story = new CoreStory();

    public string[] rewards = { "Purified Claymore of Destiny", "Good Iron Wing 1", "Spinal Tap of Retribution", "Purified Claw of Nulgath", "Mage's Gratitude" };

    public void ScriptMain(ScriptInterface bot)
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
    }

    public void MurrysQuests()
    {
        if (Core.isCompletedBefore(182))
            return;

        Story.PreLoad();

        // Map: "Citadel";
        Story.KillQuest(QuestID: 144, MapName: "Citadel", MonsterName: "Inquisitor Guard");
        Story.KillQuest(QuestID: 145, MapName: "Citadel", MonsterName: "Crusader");
        Story.KillQuest(QuestID: 146, MapName: "Citadel", MonsterName: "Inquisitor Captain");
        Story.KillQuest(QuestID: 147, MapName: "Citadel", MonsterName: "Burning Witch");
        Story.KillQuest(QuestID: 148, MapName: "Citadel", MonsterName: "Inquisitor Guard");
        Story.KillQuest(QuestID: 149, MapName: "Citadel", MonsterName: "Inquisitor Guard");
        Story.KillQuest(QuestID: 181, MapName: "Citadel", MonsterName: "Belrot The Fiend");
        if (!Story.QuestProgression(182))
        {
            Core.EnsureAccept(151, 182);
            Core.HuntMonster("Citadel", "Grand Inquisitor", "Grand Inquisitor's Gloves");
            Core.EnsureAccept(151);
            Core.HuntMonster(map: "Citadel", monster: "Grand Inquisitor", item: "Ring of Evil Intent");
            Core.EnsureComplete(182);
        }

    }

    public void PolishsQuestsTercessuinotlim()
    {
        if (Core.isCompletedBefore(668))
            return;

        Story.PreLoad();

        Core.AddDrop("Unidentified 9", "Unidentified 28", "Dark Crystal Shard", "Claw of Nulgath", "Relic of Chaos");

        // Map: "Tercessuinotlim"

        //iron wing helm enchant=
        Nulgath.TheAssistant(item: "Unidentified 9");
        Story.KillQuest(QuestID: 560, MapName: "underworld", MonsterName: "Undead Bruiser", GetReward: false);

        //cleansing of spinal tap=
        Nulgath.Supplies("Dark Crystal Shard", 5);
        Nulgath.TheAssistant(item: "Unidentified 28");
        Story.KillQuest(QuestID: 585, MapName: "Tercessuinotlim", MonsterName: "Legion Fenrir");

        //purified claw
        Nulgath.Supplies("Tainted Gem", 7);
        Nulgath.Supplies("Claw of Nulgath");
        Story.KillQuest(QuestID: 668, MapName: "Tercessuinotlim", MonsterName: "Dark Makai");
    }

    public void PolishsQuestsCitadelRuins()
    {
        if (Core.isCompletedBefore(6182))
            return;

        Story.PreLoad();

        Core.AddDrop("Mage's Gratitude");
        // Map: "citadelruins";
        //get ready to amplify
        Story.KillQuest(6172, MapName: "citadelruins", MonsterName: "Mana Sprites");
        Story.MapItemQuest(6172, MapName: "citadelruins", MapItemID: 5592);
        //break the seal
        Story.MapItemQuest(6173, MapName: "citadelruins", MapItemID: 5602);
        //clear out the squatters
        Story.KillQuest(6174, MapName: "citadelruins", MonsterName: "Inquisitor Hobo");
        //grab a clue
        Story.KillQuest(6175, MapName: "citadelruins", MonsterName: "Inquisitor Hobo");
        Story.MapItemQuest(6175, MapName: "citadelruins", MapItemID: 5593, Amount: 5);
        Bot.Sleep(2500);
        //unlock the door
        Story.KillQuest(6176, MapName: "citadelruins", MonsterName: "Inquisitor Heavy");
        Story.MapItemQuest(6176, MapName: "citadelruins", MapItemID: 5603);
        //search the labs
        Story.KillQuest(6177, MapName: "citadelruins", MonsterName: "Inquisitor Hobo");
        Story.MapItemQuest(6177, MapName: "citadelruins", MapItemID: 5594);
        Story.MapItemQuest(6177, MapName: "citadelruins", MapItemID: 5595);
        Story.MapItemQuest(6177, MapName: "citadelruins", MapItemID: 5596);
        //break another seal
        Story.KillQuest(6178, MapName: "citadelruins", MonsterName: "Mana Sprites");
        Story.MapItemQuest(6178, MapName: "citadelruins", MapItemID: 5597);
        //inspect time
        Story.MapItemQuest(6179, MapName: "citadelruins", MapItemID: 5598);
        //find the parts
        Story.KillQuest(6180, MapName: "citadelruins", MonsterName: "Inquisitor Hobo");
        Story.MapItemQuest(6178, MapName: "citadelruins", MapItemID: 5599);
        Story.MapItemQuest(6178, MapName: "citadelruins", MapItemID: 5600);
        Story.MapItemQuest(6178, MapName: "citadelruins", MapItemID: 5601);
        //defeat the grand inquisitor
        Story.KillQuest(6181, MapName: "citadelruins", MonsterName: "Grand Inquisitor Murry");
        //defeat enn'tropy
        Story.KillQuest(6182, MapName: "citadelruins", MonsterName: "Enn'tröpy");
        Core.ToBank(rewards);
    }
}