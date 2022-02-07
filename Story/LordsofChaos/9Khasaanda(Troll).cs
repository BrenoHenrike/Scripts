//cs_include Scripts/CoreBots.cs
using RBot;

public class SagaTroll
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;


    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Core.AcceptandCompleteTries = 5;
        CompleteSaga();

        Core.SetOptions(false);
    }

    public void CompleteSaga()
    {
        StoryLine();

        Core.Relogin();
        Core.BuyItem("bloodtusk", 308, "Elite Phoenix Bow");
        Bot.Sleep(2500);
        Core.ToBank("Elite Phoenix Bow");
        Core.Logger("Chapter: \"Chaos Lord Khasaanda\" complete");
    }

    public void StoryLine()
    {
        if (Bot.Quests.IsUnlocked(1468))
            return;

        //Horc Stink! 
        if (!Core.QuestProgression(1226))
        {
            Core.EnsureAccept(1226);
            Core.HuntMonster("crossroads", "Chinchilizard", "Scaly Skin Scrub", 7);
            Core.HuntMonster("bloodtusk", "Trollola Plant", "Perfumed Trollola Flower", 10);
            Core.MapItemQuest(1226, "bloodtusk", 523);
        }

        //The Time Grows Closer
        Core.KillQuest(1227, "crossroads", new[] { "Koalion", "Lemurphant" });

        //Like Calls to Like
        if (!Core.QuestProgression(1228))
        {
            Core.EnsureAccept(1228);
            Core.HuntMonster("bloodtusk", "Crystal-Rock", "Mountain Crystal", 3);
            Core.HuntMonster("crossroads", "Chinchilizard", "Liz-Leather Thongs", 5);
            Core.HuntMonster("crossroads", "Lemurphant", "Lemurphant Ivory", 5);
            Core.MapItemQuest(1228, "crossroads", 525);
        }

        //Incense Makes Sense
        if (!Core.QuestProgression(1229))
        {
            Core.EnsureAccept(1229);
            Core.HuntMonster("crossroads", "Lemurphant", "Lemurphant Musk", 5);
            Core.HuntMonster("bloodtusk", "Trollola Plant", "Trollola Plant Resin", 4);
            Core.HuntMonster("crossroads", "Koalion", "Fur for Firestarting", 5);
            Core.MapItemQuest(1229, "crossroads", 521, 10);
        }

        //She Who asks 1
        if (!Core.QuestProgression(1230))
        {
            Core.ChainQuest(1230);
        }

        //The Troll Inside
        if (!Core.QuestProgression(1231))
        {
            Core.EnsureAccept(1231);
            Core.HuntMonster("crossroads", "Lemurphant", "Lemurphant Tusks", 5);
            Core.HuntMonster("crossroads", "Koalion", "Koalion Claw", 5);
            Core.HuntMonster("bloodtusk", "Crystal-Rock", "Singing Crystals", 10);
            Core.MapItemQuest(1231, "crossroads", 522, 10);
            Core.MapItemQuest(1231, "crossroads", 524, 5);
        }

        //She Who asks 2 - cutscene
            // Core.Join("crossroads");
            // Core.ChainQuest(1240, FollowupIDOverwrite: 1272);
            Core.Join("crossroads");
            Core.Jump("CutE", "Left");
            Bot.Sleep(2000);
            Core.SendPackets("%xt%zm%tryQuestComplete%76051%1240%-1%false%wvz%");

        //Bloodtusk War
        Core.KillQuest(1272, "bloodtuskwar", "Chaotic Troll", FollowupIDOverwrite: 1274, AutoCompleteQuest: false);

        //Guarded Secrets, Hidden Treasures
        Core.MapItemQuest(1274, "ravinetemple", 553);

        //Evidence of Chaos
        Core.MapItemQuest(1275, "ravinetemple", 554, 5);
        Core.MapItemQuest(1275, "ravinetemple", 555, 10);
        Core.MapItemQuest(1275, "ravinetemple", 556, 10);

        //Learn More of the Ore
        Core.KillQuest(1276, "ravinetemple", "*");

        //Too Little, Too Late. Still Needed
        Core.MapItemQuest(1277, "ravinetemple", 557, 10);
        Core.KillQuest(1277, "ravinetemple", "*");
        Core.MapItemQuest(1277, "ravinetemple", 557, 10);

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
        Core.KillQuest(1419, "ancienttemple", "Chaotic Vulture|Chaotic Horcboar");

        //Ancient Ointment
        Core.KillQuest(1420, "ancienttemple", "Chaotic Vulture|Chaotic Horcboar");
        Core.MapItemQuest(1420, "ancienttemple", 706, 7);

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
        Core.MapItemQuest(1456, "dreamnexus", 734, FollowupIDOverwrite: 1465);
        Core.MapItemQuest(1456, "dreamnexus", 735, FollowupIDOverwrite: 1465);
        Core.MapItemQuest(1456, "dreamnexus", 736, FollowupIDOverwrite: 1465);
        Core.MapItemQuest(1456, "dreamnexus", 737, FollowupIDOverwrite: 1465);

        //Secure a Route Home
        Core.KillQuest(1465, "dreamnexus", new[] { "Dark Wyvern", "Dark Wyvern", "Aether Serpent", "Aether Serpent" });

        //DreamDancers' Orbs
        Core.MapItemQuest(1466, "dreamnexus", 738, 10);
        Core.MapItemQuest(1466, "dreamnexus", 739, 11);

        //Master the Flames
        Core.KillQuest(1467, "dreamnexus", new[] { "Solar Phoenix", "Solar Phoenix" });

        //Choose: Khasaanda Confrontation?
        Core.KillQuest(1468, "dreamnexus", "Khasaanda", hasFollowup: false);
        
        Core.Relogin();
        Core.BuyItem("battleon", 308, "Elite Phoenix Bow");
        Bot.Sleep(700);
        Core.ToBank("Elite Phoenix Bow");
    }
}
