//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaSandsea
{
	public CoreBots Core => CoreBots.Instance;

	public int questStart = 0;

	public string OptionsStorage = "SagaSandsea";
	public bool DontPreconfigure = true;

	public List<IOption> Options = new List<IOption>()
	{
		new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
	};

	public static readonly int[] qIDs = 
	{
		/* -- /Join SandPort -- 					   */
			930 , /* 0  - Sandport and Starboard 	   */
			931 , /* 1  - Shark Diving 				   */
			932 , /* 2  - Thieving Cut Throats 		   */
			933 , /* 3  - Lost and Found 			   */
			934 , /* 4  - Sell-Sword Sell-Outs 		   */
		/* -- /Join Pyramid -- 						   */
			967 , /* 5  - Sacred Scarabs 			   */
			968 , /* 6  - A Noob is Guard 			   */
			969 , /* 7  - Bandaged Aids 			   */
			970 , /* 8  - Keys to the Royal Chamber    */
			971 , /* 9  - Confront Duat 			   */
		/* -- /Join Wanders -- 						   */
			972 , /* 10 - They've Gone Dark 		   */
			973 , /* 11 - Bad Doggies 				   */
			974 , /* 12 - Essentially Evil 			   */
			975 , /* 13 - Loose Threads 			   */
			976 , /* 14 - Seek The Treasure 		   */
			977 , /* 15 - Dreamsand 				   */
			978 , /* 16 - I Dream Of... 			   */
		/* -- /Join SandCastle -- 					   */
			995 , /* 17 - Sandsational Castle 		   */
			996 , /* 18 - Furry Fury 				   */
			997 , /* 19 - Keeping Secrets Under Wraps  */
			998 , /* 20 - Gem Jam 			  		   */
			999 , /* 21 - Enter the Sphinx 			   */
		/* -- /Join Djinn -- 						   */
			1000, /* 22 - Unlamented Lamia 			   */
			1001, /* 23 - E-vase-ive Measures 		   */
			1002, /* 24 - Tri-hump-hant Camels 		   */
			1003, /* 25 - I Don't Mean to Harp On It...*/
			1004, /* 26 - In-djinn-ious Solution 	   */
			1005  /* 27 - Chaos Lord Tibicenas 		   */
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
				case 0: //Sandport and Starboard
					Core.GetMapItem(251, map: "sandport");
					break;
				case 1: //Shark Diving
					Core.SmartKillMonster(qIDs[i], "sandport", "Sandshark");
					break;
				case 2: //Thieving Cut Throats
					Core.SmartKillMonster(qIDs[i], "sandport", "Tomb Robber");
					break;
				case 3: //Lost and Found
					Core.SmartKillMonster(qIDs[i], "sandport", "Tomb Robber");
					break;
				case 4: //Sell-Sword Sell-Outs
					Core.KillMonster("sandport", "r5", "Right", "Horc Sell-Sword", "Horc Sell-Swords Defeated", 3);
					Core.KillMonster("sandport", "r6", "Right", "Horc Sell-Sword");
					break;
				case 5: //Sacred Scarabs
					Core.SmartKillMonster(qIDs[i], "pyramid", "Golden Scarab");
					break;
				case 6: //A Noob is Guard
					Core.SmartKillMonster(qIDs[i], "pyramid", "Anubis Deathguard");
					break;
				case 7: //Bandaged Aids
					Core.SmartKillMonster(qIDs[i], "pyramid", "Mummy");
					break;
				case 8: //Keys to the Royal Chamber
					Core.SmartKillMonster(qIDs[i], "pyramid", "Golden Scarab");
					break;
				case 9: //Confront Duat
					Core.GetMapItem(304, map: "pyramid");
					break;
				case 10: //They've Gone Dark
					Core.SmartKillMonster(qIDs[i], "wanders", "Kalestri Worshiper");
					break;
				case 11: //Bad Doggies
					Core.SmartKillMonster(qIDs[i], "wanders", "Kalestri Hound");
					break;
				case 12: //Essentially Evil
					Core.SmartKillMonster(qIDs[i], "wanders", "Kalestri Hound|Kalestri Worshiper");
					break;
				case 13: //Loose Threads
					Core.SmartKillMonster(qIDs[i], "wanders", "Lotus Spider");
					break;
				case 14: //Seek The Treasure
					Core.GetMapItem(306, map: "wanders");
					break;
				case 15: //Dreamsand
					Core.SmartKillMonster(qIDs[i], "wanders", "Lotus Spider");
					break;
				case 16: //I Dream Of...
					Core.SmartKillMonster(qIDs[i], "wanders", "Sek-Duat");
					break;
				case 17: //Sandsational Castle
					Core.GetMapItem(361, map: "sandcastle");
					break;
				case 18: //Furry Fury
					Core.SmartKillMonster(qIDs[i], "sandcastle", "War Hyena");
					break;
				case 19: //Keeping Secrets Under Wraps
					Core.SmartKillMonster(qIDs[i], "sandcastle", "War Mummy");
					break;
				case 20: //Gem Jam
					Core.SmartKillMonster(qIDs[i], "sandcastle", "War Hyena|War Mummy");
					break;
				case 21: //Enter the Sphinx
					Core.SmartKillMonster(qIDs[i], "sandcastle", "Chaos Sphinx");
					break;
				case 22: //Unlamented Lamia
					Core.SmartKillMonster(qIDs[i], "djinn", "Lamia");
					break;
				case 23: //E-vase-ive Measures
					Core.SmartKillMonster(qIDs[i], "sandsea", "Desert Vase");
					break;
				case 24: //Tri-hump-hant Camels
					Core.SmartKillMonster(qIDs[i], "sandsea", "Bupers Camel");
					break;
				case 25: //I Don't Mean to Harp On It...
					Core.SmartKillMonster(qIDs[i], "djinn", "Harpy");
					break;
				case 26: //In-djinn-ious Solution
					Core.GetMapItem(370, 5, "djinn");
					break;
				case 27: //Chaos Lord Tibicenas
					Core.SmartKillMonster(qIDs[i], "djinn", "Tibicenas");
					break;
			}
			Core.EnsureComplete(qIDs[i]);
			Core.Logger($"Finished {i}");
			Core.Rest();
		}

		Core.SetOptions(false);
	}
}
