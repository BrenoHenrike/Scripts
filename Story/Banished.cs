//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class Banished
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();


    public string[] QuestDrops = { "Diabolical Tome Pet", "Diabolical Tome Cape" };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Story.PreLoad();
        HikarisQuests();
        Knave1sQuests();

        Core.SetOptions(false);
    }

    public void HikarisQuests()
    {
        if (Core.isCompletedBefore(8464))
            return;

        Core.EquipClass(ClassType.Solo);
        Adv.BestGear(GearBoost.dmgAll);

        // he First Task
        Story.KillQuest(7875, "timevoid", "Unending Avatar");

        // The Second Task
        Story.KillQuest(7876, "twilightedge", "ChaosWeaver Warrior");

        // The Third Task
        Story.KillQuest(7877, "mudluk", "Tiger Leech");

        // The Fourth Task
        Story.KillQuest(7878, "deathsrealm", "Death Alive");

        // The Fifth Task
        Story.KillQuest(7879, "thevoid", "Void Dragon");

        // The Sixth Task
        if (!Story.QuestProgression(7880))
        {
            Core.EnsureAccept(7880);
            Core.HuntMonster("banished", "Desterrat Moya", "Infected Tentacle");
            Core.EnsureComplete(7880);
        }

        // Short of Reach
        Story.KillQuest(8458, "transformation", "Eldritch Abomination");

        // Stringing Along
        Story.KillQuest(8459, "blackhorn", "Bonefeeder Spider");

        // Review the Reanimated
        Story.KillQuest(8460, "noxustower", "Lightguard Caster");

        // Resting Place
        Story.KillQuest(8461, "aozorahills", "Ghostly Hasu");

        // Health is Wealth
        // Vial of Antitoxins x1  
        if (!Story.QuestProgression(8462))
        {
            Core.BuyItem("alchemyacademy", 395, "Gold Voucher 100k", 1);
            Story.BuyQuest(8462, "alchemyacademy", 2114, "Vial of Antitoxins");
        }

        // A Guilty Conscience
        Story.KillQuest(8463, "ghostnexus", "Manifestation of Grief");

        // Insomniacs
        Story.KillQuest(8464, "somnia", "Dream Larva");
    }

    public void Knave1sQuests()
    {
        if (Core.isCompletedBefore(2027))
            return;

        Adv.BestGear(GearBoost.dmgAll);

        // Knave1's Route to the Void
        Story.MapItemQuest(2022, "northlands", 979);

        // Void Spell
        if (!Story.QuestProgression(2023))
        {
            Core.EnsureAccept(2023);
            Core.HuntMonster("northlands", "Chaos Gemrald", "Chaos Gemerald Cluster");
            Core.HuntMonster("northlands", "Chaos Gemrald", "Chaos Gemerald Shard", 5);
            Core.EnsureComplete(2023);
        }
        Core.SetOptions(false);

        // Know Thy Enemy
        Story.MapItemQuest(2024, "banished", 980);

        // An Enemy Unblooded
        Story.KillQuest(2025, "banished", new[] { "Desterrat Cruor", "Desterrat Crux" });

        // Re - Open the Seal
        Story.MapItemQuest(2026, "banished", 981, 7);

        // Weaken the Moya
        Story.KillQuest(2027, "doomwood", "Doomwood Ectomancer");

        // Banish the Banished One
        Story.KillQuest(2028, "banished", "Desterrat Moya");
    }
}