//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaHorc
{
	public CoreBots Core => CoreBots.Instance;

	public int questStart = 0;

	public string OptionsStorage = "SagaHorc";
	public bool DontPreconfigure = true;

	public List<IOption> Options = new List<IOption>()
	{
		new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
	};

	public static readonly int[] qIDs = 
	{
		/* -- /Join CrossRoads                             */
			1232, /*  0 - Troll Stink!                     */
		/* -- /Join BloodTusk --                           */
			1233, /*  1 - It Not Time Yet                  */
		/* -- /Join CrossRoads --                          */
			1234, /*  2 - Mountain Protection              */
			1235, /*  3 - Clear Mind, Cleanse Spirit       */
			0   , /*  4 - She Who Answers 1                */
			1237, /*  5 - Be Horc Inside                   */
			0   , /*  6 - She Who Answers 2 - cutscene     */
		/* -- /Join BloodTuskWar --                        */
			0   , /*  7 - Bloodtusk War                    */
		/* -- /Join RavineTemple --                        */
			1280, /*  8 - Into, Under the Mountain         */
			1281, /*  9 - Has the Land Been Tainted?       */
			1282, /* 10 - Tears of the Mountain            */
			1283, /* 11 - Defend the UnderMountain         */
			1284, /* 12 - Alliance Defiance                */
		/* -- /Join Alliance --                            */
			1375, /* 13 - Scout and Return                 */
			1376, /* 14 - Good and Evil Not Always Right   */
			1377, /* 15 - Trapping Savage Soldiers         */
			1378, /* 16 - Find What is Hidden Inside       */
			1379, /* 17 - Chaorruption Rejection           */
			1380, /* 18 - Alliance Subdued                 */
		/* -- /Join AncientTemple --                       */
			1424, /* 19 - Cleanse the Chaorruption         */
			1425, /* 20 - Chaorruption Cure?               */
			1426, /* 21 - Guardian Salvation               */
			1427, /* 22 - Poison for a Purpose             */
			1428, /* 23 - The Heart of the Temple Awaits   */
		/* -- /Join OreCavern --                           */
			1456, /* 24 - Wounds in Stones and Beasts      */
			1457, /* 25 - Light in Underhome               */
			1458, /* 26 - Truth is its Own Light           */
			1459, /* 27 - Horcs Know Mercy                 */
			1460, /* 28 - Battle the Baas!                 */
		/* -- /Join DreamNexus --                          */
			1469, /* 29 - Know the Nexus                   */
			1470, /* 30 - Secure a Route Home              */
			1471, /* 31 - DreamDancers' Orbs               */
			1472, /* 32 - Master the Flames                */
			1473  /* 33 - Choose: Khasaanda Confrontation? */
	};

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		questStart = bot.Config.Get<int>("startQuest");

		for (int i = questStart; i < qIDs.Length; i++)
		{
			bot.Config.Set("startQuest", i);
			Core.Logger($"Starting {i}");
			Core.EnsureAccept(qIDs[i]);
			switch(i)
			{
				case 0: //Troll Stink!
					Core.GetMapItem(523, map: "bloodtusk");
					Core.SmartKillMonster(qIDs[i], "bloodtusk", "Trollola Plant");
					Core.SmartKillMonster(qIDs[i], "crossroads", "Chinchilizard");
					break;
				case 1: //It Not Time Yet
					Core.SmartKillMonster(qIDs[i], "crossroads", new[] { "Lemurphant", "Koalion" });
					break;
				case 2: //Mountain Protection
					Core.GetMapItem(525, map: "crossroads");
					Core.SmartKillMonster(qIDs[i], "crossroads", new[] { "Chinchilizard", "Lemurphant" });
					Core.SmartKillMonster(qIDs[i], "bloodtusk", "Crystal-Rock");
					break;
				case 3: //Clear Mind, Cleanse Spirit
					Core.GetMapItem(521, 10, "crossroads");
					Core.SmartKillMonster(qIDs[i], "crossroads", new[] { "Lemurphant", "Koalion" });
					Core.SmartKillMonster(qIDs[i], "bloodtusk", "Trollola Plant");
					break;
				case 4: //She Who Answers 1
					bot.Player.Join("crossroads");
					Core.Jump("r11", "Down");
					bot.SendPacket("%xt%zm%tryQuestComplete%21863%1236%-1%false%wvz%");
					bot.Sleep(2000);
					break;
				case 5: //Be Horc Inside
					Core.GetMapItem(524, 10, "crossroads");
					Core.GetMapItem(522, 5);
					Core.SmartKillMonster(qIDs[i], "crossroads", new[] { "Lemurphant", "Koalion" });
					Core.SmartKillMonster(qIDs[i], "bloodtusk", "Rock");
					break;
				case 6: //She Who Answers 2 - cutscene
					bot.Player.Join("crossroads");
					Core.Jump("CutE", "Left");
					bot.SendPacket("%xt%zm%tryQuestComplete%22189%1241%-1%false%wvz%");
					bot.Sleep(2000);
					break;
				case 7: //Bloodtusk War
					bot.Player.Join("bloodtuskwar");
					Core.Jump("r7", "Left");
					bot.Player.Kill("Chaotic Troll");
					Core.Jump("Cut1", "Left");
					bot.Sleep(2000);
					bot.SendPacket("%xt%zm%tryQuestComplete%22224%1273%-1%false%wvz%");
					break;
				case 8: //Into, Under the Mountain
					Core.GetMapItem(553, map: "ravinetemple");
					break;
				case 9: //Has the Land Been Tainted?
					Core.GetMapItem(554, 5, "ravinetemple");
					Core.GetMapItem(555, 10);
					Core.GetMapItem(556, 10);
					break;
				case 10: //Tears of the Mountain
					Core.SmartKillMonster(qIDs[i], "ravinetemple", "*");
					break;
				case 11: //Defend the UnderMountain
					Core.GetMapItem(557, 10, "ravinetemple");
					Core.SmartKillMonster(qIDs[i], "ravinetemple", "*");
					break;
				case 12: //Alliance Defiance
					Core.SmartKillMonster(qIDs[i], "ravinetemple", "*");
					break;
				case 13: //Scout and Return
					Core.GetMapItem(679, map: "alliance");
					Core.GetMapItem(680);
					break;
				case 14: //Good and Evil Not Always Right
					Core.SmartKillMonster(qIDs[i], "alliance", new[] { "Good Soldier", "Evil Soldier" });
					break;
				case 15: //Trapping Savage Soldiers
					Core.GetMapItem(675, 10, "alliance");
					break;
				case 16: //Find What is Hidden Inside
					Core.GetMapItem(676, map: "alliance");
					break;
				case 17: //Chaorruption Rejection
					Core.SmartKillMonster(qIDs[i], "alliance", "Chaorrupted Evil Soldier|Chaorrupted Good Soldier");
					break;
				case 18: //Alliance Subdued
					Core.SmartKillMonster(qIDs[i], "alliance", new[] { "General Cynari", "General Tibias" });
					break;
				case 19: //Cleanse the Chaorruption
					Core.SmartKillMonster(qIDs[i], "ancienttemple", "Chaotic Vulture|Chaotic Horcboar");
					break;
				case 20: //Chaorruption Cure?
					Core.GetMapItem(706, 7, "ancienttemple");
					Core.SmartKillMonster(qIDs[i], "ancienttemple", "Chaotic Vulture|Chaotic Horcboar");
					break;
				case 21: //Guardian Salvation
					Core.SmartKillMonster(qIDs[i], "ancienttemple", "Chaos Troll Spirit|Chaos Horc Spirit");
					break;
				case 22: //Poison for a Purpose
					Core.SmartKillMonster(qIDs[i], "ancienttemple", "Serpentress");
					break;
				case 23: //The Heart of the Temple Awaits
					Core.GetMapItem(707, map: "ancienttemple");
					break;
				case 24: //Wounds in Stones and Beasts
					Core.GetMapItem(717, map: "orecavern");
					break;
				case 25: //Light in Underhome
					Core.GetMapItem(719, 5, "orecavern");
					Core.SmartKillMonster(qIDs[i], "orecavern", "Crashroom");
					break;
				case 26: //Truth is its Own Light
					Core.GetMapItem(718, 5, "orecavern");
					break;
				case 27: //Horcs Know Mercy
					Core.SmartKillMonster(qIDs[i], "orecavern", "Chaorrupted Evil Soldier|Chaorrupted Good Soldier");
					break;
				case 28: //Battle the Baas!
					Core.SmartKillMonster(qIDs[i], "orecavern", "Naga Baas");
					break;
				case 29: //Know the Nexus
					Core.GetMapItem(734, map: "dreamnexus");
					Core.GetMapItem(735);
					Core.GetMapItem(736);
					Core.GetMapItem(737);
					break;
				case 30: //Secure a Route Home
					Core.SmartKillMonster(qIDs[i], "dreamnexus", new[] { "Dark Wyvern", "Dark Wyvern", "Aether Serpent", "Aether Serpent" });
					break;
				case 31: //DreamDancers' Orbs
					Core.GetMapItem(738, 10, "dreamnexus");
					Core.GetMapItem(739, 11);
					break;
				case 32: //Master the Flames
					Core.SmartKillMonster(qIDs[i], "dreamnexus", new[] { "Solar Phoenix", "Solar Phoenix"});
					break;
				case 33: //Choose: Khasaanda Confrontation?
					Core.SmartKillMonster(qIDs[i], "dreamnexus", "Khasaanda");
					break;
			}
			Core.EnsureComplete(qIDs[i]);
			Core.Logger($"Finished {i}");
			Core.Rest();
		}

		Core.SetOptions(false);
	}
}