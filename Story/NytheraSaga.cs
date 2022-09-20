//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class NytheraSaga
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        if (Core.isCompletedBefore(904))
            return;

        Northlands();
        KingCoal();
        Swallowed();
        BlindingSnow();
        Void();
    }

    public void Northlands()
    {
        if (Core.isCompletedBefore(444))
            return;

        Story.PreLoad(this);

        //Element of Surprise 437
        Story.MapItemQuest(437, "NorthLands", 77);
        Story.KillQuest(437, "NorthLands", "Snow Golem");

        //Interrogation 438
        Story.KillQuest(438, "NorthLands", "Snow Golem");

        //Locked on Combinations 439
        Story.KillQuest(439, "NorthLands", new[] { "Snow Golem", "Snow Golem", "Snow Golem" });

        //Combo Breaker 440
        Story.KillQuest(440, "NorthLands", "Snow Golem");

        //Open the Door 8690
        Story.MapItemQuest(8690, "NorthLands", 80);

        //The Cave 8689
        Story.MapItemQuest(8689, "NorthLands", 10205);

        //Aisha's Minion 443
        Story.KillQuest(443, "NorthLands", "Water Draconian");

        //Aisha 444
        Story.KillQuest(444, "NorthLands", "Aisha's Drake");
    }

    public void KingCoal()
    {
        if (Core.isCompletedBefore(449))
            return;

        //Yellow Snow Cone 447
        Story.KillQuest(447, "KingCoal", new[] { "Snow Golem", "Ice Elemental" });

        //Cool Down Items 448
        Story.KillQuest(448, "KingCoal", "Snow Golem");

        //Take a Chill Pill 449
        Story.KillQuest(449, "KingCoal", "Snow Golem");
    }

    public void Swallowed()
    {

        if (Core.isCompletedBefore(455))
            return;

        Story.PreLoad(this);

        //Defeat 10 Viruses 450
        Story.KillQuest(450, "Swallowed", "Germs");

        //Defeat 10 More 451
        Story.KillQuest(451, "Swallowed", "Germs");

        //30? No way! 452
        Story.KillQuest(452, "Swallowed", "Germs");

        //Add 10 More 453
        Story.KillQuest(453, "Swallowed", "Germs");

        //50 is RIGHT OUT! 454
        Story.KillQuest(454, "Swallowed", "Germs");

        //Anything but Common 455
        Story.KillQuest(455, "Swallowed", "Rhinovirus");
    }

    public void BlindingSnow()
    {

        if (Core.isCompletedBefore(900))
            return;

        Story.PreLoad(this);

        //Chasing After a Girl 898
        if (!Story.QuestProgression(898))
        {
            Core.EnsureAccept(898);
            if (!Core.CheckInventory("C.P.S."))
            {
                Core.AddDrop("C.P.S.");
                Core.GetMapItem(233, 10, "BlindingSnow");
                Core.GetMapItem(235, 1, "Northlands");
            }
            Core.EnsureComplete(898);
        }

        //Tracking Down Nythera 899
        Story.KillQuest(899, "BlindingSnow", "Nythera");

        //Searching for Splinters 900
        Story.KillQuest(900, "BlindingSnow", "Chaorrupted Wolf");
    }

    public void Void()
    {

        if (Core.isCompletedBefore(904))
            return;

        Story.PreLoad(this);

        //A-Void-ing The Larva 901
        Story.KillQuest(901, "Void", "Void Larva");

        //Spawn Point 902
        Story.KillQuest(902, "Void", "Void Elemental");

        //Null and Void Spheres 903
        Story.KillQuest(903, "palooza ", new[] { "Chaos Lord Discordia", "Chaos Lord Discordia", "Chaos Lord Discordia", "Chaos Lord Discordia" });

        //Enter the Great Void Dragon's Lair 904
        Story.KillQuest(904, "Void", "Void Dragon");

    }
}
