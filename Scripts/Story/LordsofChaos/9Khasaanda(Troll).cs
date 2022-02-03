//cs_include Scripts/CoreBots.cs
using RBot;

public class SagaTroll
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;


    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    //Core.MapItemQuest(questid, "Mapname", mapitemid, amount);
    //Core.KillQuest(questid, "Mapname", "mobname");

    public void StoryLine()
    {
        if (Bot.Quests.IsUnlocked(1468))
            return;

        //Horc Stink! 
        Core.MapItemQuest(1226, "bloodtusk", 523);
        Core.KillQuest(1226, "bloodtusk", "Trollola Plant");
        Core.KillQuest(1226, "crossroads", "Chinchilizard");

        //The Time Grows Closer
        Core.KillQuest(1227, "crossroads", new[] { "Lemurphant", "Koalion" });

        //Like Calls to Like
        Core.KillQuest(1228, "crossroads", new[] { "Chinchilizard", "Lemurphant" });
        Core.KillQuest(1228, "bloodtusk", "Crystal-Rock");

        //Incense Makes Sense
        Core.MapItemQuest(1229, "crossroads", 521, 10, hasFollowup: false);
        Core.KillQuest(1229, "crossroads", new[] { "Lemurphant", "Koalion" }, hasFollowup: false);
        Core.KillQuest(1229, "bloodtusk", "Trollola Plant", hasFollowup: false);

        //She Who Answers 1
        if (!Bot.Quests.IsUnlocked(1231))
        {
            Core.EnsureAccept(1230);
            Core.Join("crossroads");
            Core.Jump("r11", "Down");
            Core.EnsureComplete(1230);
        }

        //The Troll Inside
        Core.MapItemQuest(1231, "crossroads", 524, 10, hasFollowup: false);
        Core.MapItemQuest(1231, "crossroads", 522, 5, hasFollowup: false);
        Core.KillQuest(1231, "crossroads", new[] { "Lemurphant", "Koalion" }, hasFollowup: false);
        Core.KillQuest(1231, "bloodtusk", "Crystal-Rock", hasFollowup: false);

        //She Who Answers 2 - cutscene
        if (!Bot.Quests.IsUnlocked(1272))
        {
            Core.Join("crossroads");
            Core.Jump("CutE", "Left");
            Core.SendPackets("%xt%zm%tryQuestComplete%76051%1240%-1%false%wvz%");
        }

        //Bloodtusk War
        Core.KillQuest(1272, "bloodtuskwar", "Chaotic Troll", FollowupIDOverwrite: 1274, AutoCompleteQuest = false);

        //Guarded Secrets, Hidden Treasures
        Core.MapItemQuest(1274, "ravinetemple", 553);

        //Evidence of Chaos
        Core.MapItemQuest(1275, "ravinetemple", 554, 5);
        Core.MapItemQuest(1275, "ravinetemple", 555, 10);
        Core.MapItemQuest(1275, "ravinetemple", 556, 10);

        //Learn More of the Ore
        Core.KillQuest(1276, "ravinetemple", "*");

        //Too Little, Too Late. Still Needed
        Core.MapItemQuest(1277, "ravinetemple", 557, 10,);
        Core.KillQuest(1277, "ravinetemple", "*");

        //Alliance Defiance
        Core.KillQuest(1278, "ravinetemple", "*", FollowupIDOverwrite: 1369);

        //The Headquartes of Good and Evil
        Core.MapItemQuest(1369, "alliance", 679);
        Core.MapItemQuest(1369, "alliance", 680);

        //Treat Nullification, Good and Bad
        Core.KillQuest(1370, "alliance", new[] { "Good Soldier", "Evil Soldier" });

        //Trap the Keepers
        Core.MapItemQuest(1371, "alliance", 675, 10);

        //Find What is Hidden Inside
        Core.MapItemQuest(1372, "alliance", 676);

        //Chaorruption Annihilation
        Core.KillQuest(1373, "alliance", "Chaorrupted Evil Soldier|Chaorrupted Good Soldier");

        //Alliance Demotion
        Core.KillQuest(1374, "alliance", new[] { "General Cynari", "General Tibias" }, FollowupIDOverwrite: 1419);

        //Contain the Chaorruption
        Core.KillQuest(1419, "ancienttemple", "Chaotic Vulture|Chaotic Horcboar"

        //Ancient Ointment
        Core.MapItemQuest(1420, "ancienttemple", 706, 7);
        Core.KillQuest(1420, "ancienttemple", "Chaotic Vulture|Chaotic Horcboar");

        //Anoint the Ancients
        Core.KillQuest(1421, "ancienttemple", "Chaos Troll Spirit|Chaos Horc Spirit");

        //Serpents Do No Harm
        Core.KillQuest(1421, "ancienttemple", "Serpentress");

        //Though Nature Bars the Way
        //Core.MapItemQuest(questid, "Mapname", mapitemid, amount);
        Core.MapItemQuest(1423, "ancienttemple", 707, FollowupIDOverwrite: 1451);

        //Descent Into Darkness
        Core.MapItemQuest(1451, "orecavern", 717);

        //Out of the Darkness
        Core.KillQuest(1452, "orecavern", "Crashroom");
        Core.MapItemQuest(1452, "orecavern", 719, 5);

        //Shine a Light on Deception
        Core.MapItemQuest(1453, "orecaveern", 718, 5);

        //Save Yourself, Save the Soldiers
        Core.KillQuest(1454, "orecavern", "Chaorrupted Evil Soldier|Chaorrupted Good Soldier");

        //Battle the Baas!
        Core.KillQuest(1455, "orecavern", "Naga Baas");

        //Know the Nexus
        Core.MapItemQuest(1456, "dreamnexus", 734, FollowupIDOverwrite: 1464);
        Core.MapItemQuest(1456, "dreamnexus", 735, FollowupIDOverwrite: 1464);
        Core.MapItemQuest(1456, "dreamnexus", 736, FollowupIDOverwrite: 1464);
        Core.MapItemQuest(1456, "dreamnexus", 737, FollowupIDOverwrite: 1464);

        //Secure a Route Home
        Core.KillQuest(1465, "dreamnexus", new[] { "Dark Wyvern", "Dark Wyvern", "Aether Serpent", "Aether Serpent" });

        //DreamDancers' Orbs
        Core.MapItemQuest(1466, "dreamnexus", 738, 10);
        Core.MapItemQuest(1466, "dreamnexus", 739, 11);

        //Master the Flames
        Core.KillQuest(1467, "dreamnexus", new[] { "Solar Phoenix", "Solar Phoenix" });

        //Choose: Khasaanda Confrontation?
        Core.KillQuest(1468, "dreamnexus", "Khasaanda", hasFollowup: false);
    }
}
