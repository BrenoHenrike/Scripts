//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaHorc
{
    public CoreBots Core => CoreBots.Instance;
    public ScriptInterface Bot => ScriptInterface.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CompleteSaga();

        Core.SetOptions(false);
    }

	public void CompleteSaga()
	{
		Core.Logger("Part 1");
		Part1();
		Core.Logger("Part 2");
		Part2();
		Core.Logger("Part 3");
		Part3();
		Core.Logger("Part 4");
		Part4();
		Core.Logger("Part 5");
		Part5();

        Core.Relogin();
        Core.BuyItem("bloodtusk", 308, "Elite Phoenix Bow");
        Bot.Sleep(2500);
        Core.ToBank("Elite Phoenix Bow");
        Core.Logger("Chapter: \"Chaos Lord Khasaanda\" complete");
	}

    public void Part1()
    {
        if (Bot.Quests.IsUnlocked(1280))
            return;

        //Troll Stink!
        Core.KillQuest(1232, "crossroads", "Chinchilizard");
        Core.KillQuest(1232, "bloodtusk", "Trollola Plant");
        Core.MapItemQuest(1232, "bloodtusk", 523);

        //It Not Time Yet
        Core.KillQuest(1233, "crossroads", new[] { "Lemurphant", "Koalion" });

        //Mountain Protection
        Core.KillQuest(1234, "bloodtusk", "Rock");
        Core.KillQuest(1234, "crossroads", new[] { "Chinchilizard", "Lemurphant" });
        Core.MapItemQuest(1234, "crossroads", 525);

        //Clear Mind, Cleanse Spirit
        Core.KillQuest(1235, "bloodtusk", "Trollola Plant"); 
        Core.KillQuest(1235, "crossroads", new[] { "Lemurphant", "Koalion" });
        Core.MapItemQuest(1235, "crossroads", 521, 10);

        //She Who Answers 1
        Core.EnsureAccept(1236);
        Core.Join("crossroads");
        Core.Jump("r11", "Down");
        Bot.SendPacket("%xt%zm%tryQuestComplete%21863%1236%-1%false%wvz%");
        Bot.Sleep(2000);

        //Be Horc Inside
        Core.KillQuest(1237, "crossroads", new[] { "Lemurphant", "Koalion" });
        Core.KillQuest(1237, "bloodtusk", "Rock");
        Core.MapItemQuest(1237, "crossroads", 524, 10);
        Core.MapItemQuest(1237, "mapname", 522, 5);

        //She Who Answers 2 - cutscene
        Core.EnsureAccept(1241);
        Core.Join("crossroads");
        Core.Jump("CutE", "Left");
        Bot.Sleep(2500);
        Core.JumpWait();
        Bot.SendPacket("%xt%zm%tryQuestComplete%22189%1241%-1%false%wvz%");
        Bot.Sleep(2000);

        //Chaos Enrages the Horcs
		Core.ChainComplete(1273);
	}
	
    public void Part2()
    {
        if (Bot.Quests.IsUnlocked(1424))
            return;


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
        Core.KillQuest(1284, "ravinetemple", "*");

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
        Core.KillQuest(1380, "alliance", new[] { "General Cynari", "General Tibias" }, hasFollowup: false);
	}
	
    public void Part3()
    {
        if (Bot.Quests.IsUnlocked(1456))
            return;


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
        Core.MapItemQuest(1428, "ancienttemple", 707, hasFollowup: false);
	}
	
    public void Part4()
    {
        if (Bot.Quests.IsUnlocked(1469))
            return;



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
        Core.KillQuest(1460, "orecavern", "Naga Baas", hasFollowup: false);
	}
	
    public void Part5()
    {
        if (Bot.Quests.IsUnlocked(1473))
            return;


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

    }
}
