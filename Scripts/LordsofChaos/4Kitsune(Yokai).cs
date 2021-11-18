//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaYokai
{
	public CoreBots Core => CoreBots.Instance;

	public int questStart = 0;

	public string OptionsStorage = "SagaYokai";
	public bool DontPreconfigure = true;

	public List<IOption> Options = new List<IOption>()
	{
		new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
	};

	public static readonly int[] qIDs =
	{
		/* -- /Join YokaiBoat --	              */
			380, /*  0 - Turtle Power             */
			381, /*  1 - Setting Sail to Yokai    */
		/* -- /Join DragonKoi --                  */
			0  , /*  2 - Dragon Koi Tournament    */
		/* -- /Join Hachiko --                    */
			402, /*  3 - Dog Days                 */
			403, /*  4 - Faceless Threat          */
			405, /*  5 - Zodiac Puzzle Key        */
			406, /*  6 - Rescue!                  */
		/* -- /Join Bamboo --                     */
			466, /*  7 - Jinmenju Tree            */
			467, /*  8 - Yokai Bandits            */
			468, /*  9 - The Fiery Fiend          */
		/* -- /Join Junkyard --                   */
			469, /* 10 - Dumpster Diving          */
			470, /* 11 - Reduce, Respawn, Recycle */
			471, /* 12 - The Hunt for the Hag     */
		/* -- /Join YokaiRiver --                 */
			473, /* 13 - Su-she                   */
			474, /* 14 - Kappa Cuisine            */
			476, /* 15 - Hisssssy fit             */
		/* -- /Join YokaiGrave --                 */
			477, /* 16 - The Purrrfect Crime      */
			478, /* 17 - The Face Off             */
			0  , /* 18 - Confront Neko Mata       */
		/* -- /Join Odokuro --                    */
			481, /* 19 - Defeat O-dokuro          */
		/* -- /Join YokaiWar --                   */
			484, /* 20 - Defeat O-Dokuro's Head   */
		/* -- /Join Kitsune --                    */
			488  /* 21 - Defeat Kitsune           */
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
				case 0: //Turtle Power
					Core.SmartKillMonster(qIDs[i], "yokaiboat", "Kappa Ninja");
					Core.GetMapItem(64, map: "yokaiboat");
					break;
				case 1: //Setting Sail to Yokai
					Core.SmartKillMonster(qIDs[i], "yokaiboat", "Kappa Ninja");
					break;
				case 2: //Dragon Koi Tournament
					Core.KillMonster("dragonkoi", "t1", "Left", "Pockey Chew");
					Core.KillMonster("dragonkoi", "t2", "Left", "Notruto");
					Core.KillMonster("dragonkoi", "t3", "Left", "Nekoyasha");
					Core.KillMonster("dragonkoi", "t4", "Left", "Absolute Zero");
					Core.KillMonster("dragonkoi", "t5", "Left", "Sporkion");
					Core.KillMonster("dragonkoi", "t6", "Left", "Ryoku");
					bot.Sleep(2000);
					bot.SendPacket("%xt%zm%tryQuestComplete%31161%382%-1%false%wvz%");
					break;
				case 3: //Dog Days
					Core.KillMonster("hachiko", "Enter", "Spawn", "Samurai Nopperabo", "Samurai Questioned", 5);
					Core.KillMonster("hachiko", "r2", "Left", "Ninja Nopperabo", "Ninja Questioned", 5);
					break;
				case 4: //Faceless Threat
					Core.KillMonster("hachiko", "Rat", "Left", "Samurai Nopperabo", "Note from DT");
					break;
				case 5: //Zodiac Puzzle Key
					Core.KillMonster("hachiko", "Ox", "Left", "Samurai Nopperabo", "Rat-Ox-Tiger Piece");
					Core.KillMonster("hachiko", "Rabbit", "Left", "Ninja Nopperabo", "Rabbit-Dragon-Snake Piece");
					Core.KillMonster("hachiko", "Horse", "Left", "Samurai Nopperabo", "Horse-Sheep-Monkey Piece");
					Core.KillMonster("hachiko", "Rooster", "Left", "Ninja Nopperabo", "Rooster-Dog-Pig Piece");
					break;
				case 6: //Rescue!
					Core.SmartKillMonster(qIDs[i], "hachiko", "Dai Tengu");
					break;
				case 7: //Jinmenju Tree
					Core.GetMapItem(90, map: "bamboo");
					break;
				case 8: //Yokai Bandits
					Core.SmartKillMonster(qIDs[i], "bamboo", new[] { "Tanuki", "Tanuki" });
					break;
				case 9: //The Fiery Fiend
					Core.SmartKillMonster(qIDs[i], "bamboo", "SoulTaker");
					break;
				case 10: //Dumpster Diving
					Core.GetMapItem(91, map: "junkyard");
					break;
				case 11: //Reduce, Respawn, Recycle
					Core.KillMonster("junkyard", "Enter", "Right", 3847, "Wild Kara-Kasa", 5);
					Core.KillMonster("junkyard", "Enter", "Right", 3848, "Wild Bakezouri", 1);
					Core.KillMonster("junkyard", "r2", "Left", 3849, "Wild Biwa-Bokuboku", 3);
					Core.KillMonster("junkyard", "r2", "Left", 3845, "Wild Bura-Bura", 4);
					Core.KillMonster("junkyard", "r3", "Left", 3850, "Wild Koto-Furunushi", 2);
					break;
				case 12: //The Hunt for the Hag
					Core.SmartKillMonster(qIDs[i], "junkyard", "Onibaba");
					break;
				case 13: //Su-she
					Core.SmartKillMonster(qIDs[i], "yokairiver", new[] { "Funa-Yurei", "Funa-Yurei", "Funa-Yurei" });
					break;
				case 14: //Kappa Cuisine
					Core.SmartKillMonster(qIDs[i], "yokairiver", new[] { "Kappa Ninja", "Kappa Ninja", "Kappa Ninja" });
					Core.GetMapItem(92, map: "yokairiver");
					break;
				case 15: //Hisssssy fit
					Core.SmartKillMonster(qIDs[i], "yokairiver", "Nure Onna");
					break;
				case 16: //The Purrrfect Crime
					Core.SmartKillMonster(qIDs[i], "yokaigrave", "Skello Kitty");
					break;
				case 17: //The Face Off
					Core.SmartKillMonster(qIDs[i], "yokaigrave", new[] { "Samurai Nopperabo", "Ninja Nopperabo" });
					break;
				case 18: //Confront Neko Mata
					Core.KillMonster("yokaigrave", "Enter2", "Center", "Neko Mata");
					break;
				case 19: //Defeat O-dokuro
					Core.KillMonster("odokuro", "Boss", "Right", "O-dokuro");
					break;
				case 20: //Defeat O-Dokuro's Head
					Core.SmartKillMonster(qIDs[i], "yokaiwar", "O-Dokuro's Head");
					break;
				case 21: //Defeat Kitsune
					Core.SmartKillMonster(qIDs[i], "kitsune", "Kitsune");
					break;
			}
			Core.EnsureComplete(qIDs[i]);
			Core.Logger($"Finished {i}");
			Core.Rest();
		}

		Core.SetOptions(false);
	}
}