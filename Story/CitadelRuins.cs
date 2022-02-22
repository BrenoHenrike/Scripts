//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.
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

        // Map: "Citadel";
        Story.KillQuest(QuestID: 144, MapName: "Citadel", MonsterName: "Inquisitor Guard");
        Story.KillQuest(QuestID: 145, MapName: "Citadel", MonsterName: "Crusader");
        Story.KillQuest(QuestID: 146, MapName: "Citadel", MonsterName: "Inquisitor Captain");
        Story.KillQuest(QuestID: 147, MapName: "Citadel", MonsterName: "Burning Witch");
        Story.KillQuest(QuestID: 148, MapName: "Citadel", MonsterName: "Inquisitor Guard");
        Story.KillQuest(QuestID: 149, MapName: "Citadel", MonsterName: "Inquisitor Guard");
        Story.KillQuest(QuestID: 181, MapName: "Citadel", MonsterName: "Belrot The Fiend");
        Story.KillQuest(QuestID: 151, MapName: "Citadel", MonsterName: "Grand Inquisitor");
        Story.KillQuest(QuestID: 182, MapName: "Citadel", MonsterName: "Grand Inquisitor");
    }

    public void PolishsQuestsTercessuinotlim()
    {
        if (Core.isCompletedBefore(668))
            return;

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

        Core.AddDrop("Mage's Gratitude");
        // Map: "citadelruins";
        //get ready to amplify
        Story.KillQuest(QuestID: 6172, MapName: "citadelruins", MonsterName: "Mana Sprites");
        Story.MapItemQuest(QuestID: 6172, MapName: "citadelruins", MapItemID: 5592);
        //break the seal
        Story.MapItemQuest(QuestID: 6173, MapName: "citadelruins", MapItemID: 5602);
        //clear out the squatters
        Story.KillQuest(QuestID: 6174, MapName: "citadelruins", MonsterName: "Inquisitor Hobo");
        //grab a clue
        Story.KillQuest(QuestID: 6175, MapName: "citadelruins", MonsterName: "Inquisitor Hobo");
        Story.MapItemQuest(QuestID: 6175, MapName: "citadelruins", MapItemID: 5593, Amount: 5);
        Bot.Sleep(2500);
        //unlock the door
        Story.KillQuest(QuestID: 6176, MapName: "citadelruins", MonsterName: "Inquisitor Heavy");
        Story.MapItemQuest(QuestID: 6176, MapName: "citadelruins", MapItemID: 5603);
        //search the labs
        Story.KillQuest(QuestID: 6177, MapName: "citadelruins", MonsterName: "Inquisitor Hobo");
        Story.MapItemQuest(QuestID: 6177, MapName: "citadelruins", MapItemID: 5594);
        Story.MapItemQuest(QuestID: 6177, MapName: "citadelruins", MapItemID: 5595);
        Story.MapItemQuest(QuestID: 6177, MapName: "citadelruins", MapItemID: 5596);
        //break another seal
        Story.KillQuest(QuestID: 6178, MapName: "citadelruins", MonsterName: "Mana Sprites");
        Story.MapItemQuest(QuestID: 6178, MapName: "citadelruins", MapItemID: 5597);
        //inspect time
        Story.MapItemQuest(QuestID: 6179, MapName: "citadelruins", MapItemID: 5598);
        //find the parts
        Story.KillQuest(QuestID: 6180, MapName: "citadelruins", MonsterName: "Inquisitor Hobo");
        Story.MapItemQuest(QuestID: 6178, MapName: "citadelruins", MapItemID: 5599);
        Story.MapItemQuest(QuestID: 6178, MapName: "citadelruins", MapItemID: 5600);
        Story.MapItemQuest(QuestID: 6178, MapName: "citadelruins", MapItemID: 5601);
        //defeat the grand inquisitor
        Story.KillQuest(QuestID: 6181, MapName: "citadelruins", MonsterName: "Grand Inquisitor Murry");
        //defeat enn'tropy
        Story.KillQuest(QuestID: 6182, MapName: "citadelruins", MonsterName: "Enn'tröpy");
        Core.ToBank(rewards);
    }
}