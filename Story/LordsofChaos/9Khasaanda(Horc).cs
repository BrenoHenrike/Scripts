//cs_include Scripts/CoreBots.cs

using System;
using RBot;
using System.Collections.Generic;

public class SagaHorc
{
    public CoreBots Core => CoreBots.Instance;
    public ScriptInterface Bot => ScriptInterface.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Core.AcceptandCompleteTries = 5;

        CompleteSaga();

        Core.SetOptions(false);
    }

    public void CompleteSaga()
    {
        Core.Relogin();
        Core.BuyItem("bloodtusk", 308, "Elite Phoenix Bow");
        Bot.Sleep(2500);
        Core.ToBank("Elite Phoenix Bow");
        Core.Logger("Chapter: \"Chaos Lord Khasaanda\" complete");


        //Troll Stink!
        if (!Core.QuestProgression(1232))
        {
            Core.EnsureAccept(1232);
            Core.HuntMonster("crossroads", "Chinchilizard", "Scaly Skin Scrub", 7);
            Core.HuntMonster("bloodtusk", "Trollola Plant", "Perfumed Trollola Flower", 10);
            Core.MapItemQuest(1232, "bloodtusk", 523);
        }

        //It Not Time Yet
        Core.KillQuest(1233, "crossroads", "Lemurphant");
        Core.KillQuest(1233, "crossroads", "Koalion");

        //Mountain Protection
        if (!Core.QuestProgression(1234))
        {
            Core.EnsureAccept(1234);
            Core.HuntMonster("bloodtusk", "Crystal-Rock", "Polished Rocks", 3);
            Core.HuntMonster("crossroads", "Lemurphant", "Lemurphant Ivory", 5);
            Core.HuntMonster("crossroads", "Chinchilizard", "Liz-Leather Thongs", 5);
            Core.MapItemQuest(1234, "crossroads", 525);
        }

        //Clear Mind, Cleanse Spirit
        if (!Core.QuestProgression(1235))
        {
            Core.EnsureAccept(1235);
            Core.HuntMonster("bloodtusk", "Trollola Plant", "Trollola Plant Resin", 4);
            Core.HuntMonster("crossroads", "Lemurphant", "Lemurphant Musk", 5);
            Core.HuntMonster("crossroads", "Koalion", "Fur for Firestarting", 5);
            Core.MapItemQuest(1235, "crossroads", 521, 10);
        }

        //She Who Asks
        Core.ChainQuest(1236);

        //Be Horc Inside
        Core.KillQuest(1237, "crossroads", new[] { "Lemurphant", "Koalion", "Koalion" }, FollowupIDOverwrite: 1241);
        Core.MapItemQuest(1237, "crossroads", 524, 5, FollowupIDOverwrite: 1241);
        Core.MapItemQuest(1237, "crossroads", 522, 10, FollowupIDOverwrite: 1241);

        //She Who Answers 2 - cutscene
        Core.ChainQuest(1241, FollowupIDOverwrite: 1273);

        //Chaos Enrages the Horcs
        Core.ChainQuest(1273, FollowupIDOverwrite: 1280);

        //Into, Under the Mountain
        Core.MapItemQuest(1280, "ravinetemple", 553);

        //Has the Land Been Tainted?
        Core.MapItemQuest(1281, "ravinetemple", 554, 5);
        Core.MapItemQuest(1281, "ravinetemple", 555, 10);
        Core.MapItemQuest(1281, "ravinetemple", 556, 10);

        //Tears of the Mountain
        Core.KillQuest(1282, "ravinetemple", "*");

        //Defend the UnderMountain
        Core.KillQuest(1283, "ravinetemple", "*");
        Core.MapItemQuest(1283, "ravinetemple", 557, 10);

        //Alliance Defiance
        Core.KillQuest(1284, "ravinetemple", "*", FollowupIDOverwrite: 1375);

        //Scout and Return
        Core.MapItemQuest(1375, "alliance", 679);
        Core.MapItemQuest(1375, "alliance", 680);

        //Good and Evil Not Always Right
        Core.KillQuest(1376, "alliance", new[] { "Good Soldier", "Evil Soldier" });

        //Trapping Savage Soldiers
        Core.MapItemQuest(1377, "alliance", 675, 10);

        //Find What is Hidden Inside
        Core.MapItemQuest(1378, "alliance", 676);

        //Chaorruption Rejection
        Core.KillQuest(1379, "alliance", "Chaorrupted Evil Soldier|Chaorrupted Good Soldier");

        //Alliance Subdued
        Core.KillQuest(1380, "alliance", new[] { "General Cynari", "General Tibias" }, FollowupIDOverwrite: 1424);

        //Cleanse the Chaorruption
        Core.KillQuest(1424, "ancienttemple", "Chaotic Vulture");

        //Chaorruption Cure?
        Core.KillQuest(1425, "ancienttemple", "Chaotic Vulture");
        Core.MapItemQuest(1425, "ancienttemple", 706, 7);

        //Guardian Salvation
        Core.KillQuest(1426, "ancienttemple", "Chaos Troll Spirit");

        //Poison for a Purpose
        Core.KillQuest(1427, "ancienttemple", "Serpentress");

        //The Heart of the Temple Awaits
        Core.MapItemQuest(1428, "ancienttemple", 707, FollowupIDOverwrite: 1456);

        //Wounds in Stones and Beasts
        Core.MapItemQuest(1456, "orecavern", 717);

        //Light in Underhome
        Core.KillQuest(1457, "orecavern", "Crashroom");
        Core.MapItemQuest(1457, "orecavern", 719, 5);

        //Truth is its Own Light
        Core.MapItemQuest(1458, "orecavern", 718, 5);

        //Horcs Know Mercy
        Core.KillQuest(1459, "orecavern", "Chaorrupted Evil Soldier");

        //Battle the Baas!
        Core.KillQuest(1460, "orecavern", "Naga Baas", FollowupIDOverwrite: 1469);

        //Know the Nexus
        Core.MapItemQuest(1469, "dreamnexus", 734);
        Core.MapItemQuest(1469, "dreamnexus", 735);
        Core.MapItemQuest(1469, "dreamnexus", 736);
        Core.MapItemQuest(1469, "dreamnexus", 737);

        //Secure a Route Home
        Core.KillQuest(1470, "dreamnexus", new[] { "Dark Wyvern", "Dark Wyvern", "Aether Serpent", "Aether Serpent" });

        //DreamDancers' Orbs
        Core.MapItemQuest(1471, "dreamnexus", 738, 10);
        Core.MapItemQuest(1471, "dreamnexus", 739, 11);

        //Master the Flames
        Core.KillQuest(1472, "dreamnexus", new[] { "Solar Phoenix", "Solar Phoenix" });

        //Choose: Khasaanda Confrontation?
        Core.KillQuest(1473, "dreamnexus", "Khasaanda", hasFollowup: false);
        
        Core.Relogin();
        Core.BuyItem("battleon", 308, "Elite Phoenix Bow");
        Bot.Sleep(700);
        Core.ToBank("Elite Phoenix Bow");

    }
}
