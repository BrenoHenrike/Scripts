//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs

using RBot;

public class CitadelRuins
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();
    public CoreNulgath Nulgath = new CoreNulgath();
    public string[] rewards = { "Purified Claymore of Destiny", "Good Iron Wing 1", "Spinal Tap of Retribution", "Purified Claw of Nulgath", "Mage's Gratitude"};

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
        if (Bot.Quests.IsUnlocked(560))
        return;

        // Map: "Citadel";
        Core.KillQuest(QuestID: 144, MapName: "Citadel", MonsterName: "Inquisitor Guard");
        Core.KillQuest(QuestID: 145, MapName: "Citadel", MonsterName: "Inquisitor Captain");
        Core.KillQuest(QuestID: 146, MapName: "Citadel", MonsterName: "Burning Witch");
        Core.KillQuest(QuestID: 147, MapName: "Citadel", MonsterName: "Crusader");
        Core.KillQuest(QuestID: 148, MapName: "Citadel", MonsterName: "Crusader");
        Core.KillQuest(QuestID: 149, MapName: "Citadel", MonsterName: "Grand Inquisitor");
        Core.KillQuest(QuestID: 181, MapName: "Citadel", MonsterName: "Belrot The Fiend");
        Core.KillQuest(QuestID: 151, MapName: "Citadel", MonsterName: "Grand Inquisitor");
        Core.KillQuest(QuestID: 182, MapName: "Citadel", MonsterName: "Grand Inquisitor", hasFollowup: false);
    }

    public void PolishsQuestsTercessuinotlim()
    {
        if (Bot.Quests.IsUnlocked(6172))
        return;

        Core.AddDrop("Unidentified 9", "Unidentified 28", "Dark Crystal Shard", "Claw of Nulgath", "Relic of Chaos" );

        // Map: "Tercessuinotlim"

        //iron wing helm enchant=
        Nulgath.TheAssistant(item: "Unidentified 9");
        Core.KillQuest(QuestID: 560, MapName: "underworld", MonsterName: "Undead Bruiser", GetReward: false, FollowupIDOverwrite: 585);

        //cleansing of spinal tap=
        Nulgath.Supplies("Dark Crystal Shard", 5);
        Nulgath.TheAssistant(item: "Unidentified 28");      
        Core.KillQuest(QuestID: 585, MapName: "Tercessuinotlim", MonsterName: "Legion Fenrir", FollowupIDOverwrite: 668);

        //purified claw
        Nulgath.Supplies(item: "Tainted Gem", 7); 
        Nulgath.Supplies(item: "Claw of Nulgath");
        Core.KillQuest(QuestID: 668, MapName: "Tercessuinotlim", MonsterName: "Dark Makai", hasFollowup: false);
    }

    public void PolishsQuestsCitadelRuins()
    {
        Core.AddDrop("Mage's Gratitude");
        // Map: "citadelruins";
        //get ready to amplify
        Core.KillQuest(QuestID: 6172, MapName: "citadelruins", MonsterName: "Mana Sprites");
        //break the seal
        Core.MapItemQuest(QuestID: 6172, MapName: "citadelruins", MapItemID: 5591);
        //clear out the squatters
        Core.MapItemQuest(QuestID: 6173, MapName: "citadelruins", MapItemID: 5592);
        Core.KillQuest(QuestID: 6174, MapName: "citadelruins", MonsterName: "Inquisitor Hobo");
        //grab a clue
        Core.KillQuest(QuestID: 6175, MapName: "citadelruins", MonsterName: "Inquisitor Hobo");
        Core.MapItemQuest(QuestID: 6175, MapName: "citadelruins", MapItemID: 5593, Amount: 5);
        Bot.Sleep(2500);
        //unlock the door
        Core.KillQuest(QuestID: 6176, MapName: "citadelruins", MonsterName: "Inquisitor Heavy");
        Core.MapItemQuest(QuestID: 6176, MapName: "citadelruins", MapItemID: 5603);
        //search the labs
        Core.KillQuest(QuestID: 6177, MapName: "citadelruins", MonsterName: "Inquisitor Hobo");
        Core.MapItemQuest(QuestID: 6177, MapName: "citadelruins", MapItemID: 5594);
        Core.MapItemQuest(QuestID: 6177, MapName: "citadelruins", MapItemID: 5595);
        Core.MapItemQuest(QuestID: 6177, MapName: "citadelruins", MapItemID: 5596);
        //break another seal
        Core.KillQuest(QuestID: 6178, MapName: "citadelruins", MonsterName: "Mana Sprites");
        Core.MapItemQuest(QuestID: 6178, MapName: "citadelruins", MapItemID: 5597);
        //inspect time
        Core.MapItemQuest(QuestID: 6179, MapName: "citadelruins", MapItemID: 5598);
        //find the parts
        Core.KillQuest(QuestID: 6180, MapName: "citadelruins", MonsterName: "Inquisitor Hobo");
        Core.MapItemQuest(QuestID: 6178, MapName: "citadelruins", MapItemID: 5599);
        Core.MapItemQuest(QuestID: 6178, MapName: "citadelruins", MapItemID: 5600);
        Core.MapItemQuest(QuestID: 6178, MapName: "citadelruins", MapItemID: 5601);
        //defeat the grand inquisitor
        Core.KillQuest(QuestID: 6181, MapName: "citadelruins", MonsterName: "Grand Inquisitor Murry");
        //defeat enn'tropy
        Core.KillQuest(QuestID: 6182, MapName: "citadelruins", MonsterName: "Enn'tröpy", hasFollowup: false);
        Core.ToBank(rewards);
    }
}