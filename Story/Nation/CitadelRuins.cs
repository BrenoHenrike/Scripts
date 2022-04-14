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

        Story.PreLoad();

        Core.AddDrop("Unidentified 9", "Unidentified 28", "Dark Crystal Shard", "Claw of Nulgath", "Relic of Chaos");

        // "Tercessuinotlim"

        //iron wing helm enchant=
        Nulgath.TheAssistant("Unidentified 9");
        Story.KillQuest(560, "underworld", "Undead Bruiser", GetReward: false);

        //cleansing of spinal tap=
        Nulgath.Supplies("Dark Crystal Shard", 5);
        Nulgath.TheAssistant("Unidentified 28");
        Story.KillQuest(585, "Tercessuinotlim", "Legion Fenrir");

        //purified claw
        Nulgath.Supplies("Tainted Gem", 7);
        Nulgath.Supplies("Claw of Nulgath");
        Story.KillQuest(668, "Tercessuinotlim", "Dark Makai");
    }

    public void PolishsQuestsCitadelRuins()
    {
        if (Core.isCompletedBefore(6182))
            return;

        Story.PreLoad();

        Core.AddDrop("Mage's Gratitude");
        // "citadelruins";

        //get ready to amplify
        Story.KillQuest(6172, "citadelruins", "Mana Sprites");
        Story.MapItemQuest(6172, "citadelruins", 5592);

        //break the seal
        Story.MapItemQuest(6173, "citadelruins", 5602);

        //clear out the squatters
        if(!Story.QuestProgression(6174))
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
        if(!Story.QuestProgression(6176))
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