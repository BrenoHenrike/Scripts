//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaMythsong
{
	public CoreBots Core => CoreBots.Instance;

	public int questStart = 0;

	public string OptionsStorage = "SagaMythsong";
	public bool DontPreconfigure = true;

	public List<IOption> Options = new List<IOption>()
	{
		new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
	};

	public static readonly int[] qIDs = 
	{
		/* -- /Join Stairway --                              */
			648, /*  0 - Stairway to Heaven                  */
			649, /*  1 - Rolling Stones                      */
			650, /*  2 - Light My Fire                       */
			651, /*  3 - Knockin' on Haven's Door            */
		/* -- /Join Beehive --                               */
			658, /*  4 - Staying Alive                       */
			659, /*  5 - Killer Queen                        */
			660, /*  6 - Satisfaction                        */
			0  , /*  7 - Dance with Great Godfather of Souls */
		/* -- /Join Orchestra --                             */
			675, /*  8 - Bad Moon Rising                     */
			676, /*  9 - Burning Down The House              */
			677, /* 10 - Superstition                        */
			678, /* 11 - Soul Man                            */
		/* -- /Join MythsongWar & /Join Palooza --           */
			710  /* 12 - Kimberly 						     */
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
				case 0: //Stairway to Heaven
					Core.SmartKillMonster(qIDs[i], "stairway", new[] { "Rock Lobster", "Grateful Undead" });
					break;
				case 1: //Rolling Stones
					Core.SmartKillMonster(qIDs[i], "stairway", "Rock Lobster");
					break;
				case 2: //Light My Fire
					Core.SmartKillMonster(qIDs[i], "stairway", "Grateful Undead");
					break;
				case 3: //Knockin' on Haven's Door
					Core.SmartKillMonster(qIDs[i], "stairway", new[] { "Elwood Bruise", "Jake Bruise" });
					break;
				case 4: //Staying Alive
					Core.SmartKillMonster(qIDs[i], "beehive", "Stinger");
					break;
				case 5: //Killer Queen
					Core.SmartKillMonster(qIDs[i], "beehive", "Killer Queen Bee");
					break;
				case 6: //Satisfaction
					Core.SmartKillMonster(qIDs[i], "beehive", "Lord Ovthedance");
					break;
				case 7: //Dance with Great Godfather of Souls
					bot.Player.Join("beehive");
					bot.SendPacket("%xt%zm%tryQuestComplete%30004%661%-1%false%wvz%");
					break;
				case 8: //Bad Moon Rising
					Core.SmartKillMonster(qIDs[i], "orchestra", "Mozard|Pachelbel's Cannon");
					break;
				case 9: //Burning Down The House
					Core.SmartKillMonster(qIDs[i], "orchestra", "Pachelbel's Cannon");
					break;
				case 10: //Superstition
					Core.SmartKillMonster(qIDs[i], "orchestra", "Mozard");
					break;
				case 11: //Soul Man
					Core.SmartKillMonster(qIDs[i], "orchestra", "Faust");
					break;
				case 12: //Kimberly
					bot.Player.Join("mythsongwar");
					bot.Sleep(2500);
					Core.Jump("War4", "Right");
					bot.Player.Join("palooza");
					Core.Jump("Act5", "Left");
					bot.Player.Kill("Chaos Lord Discordia");
					Core.Jump("Cut", "Left");
					bot.Sleep(2000);
					Core.Jump("Act7", "Left");
					bot.Player.Kill("Pony Gary Yellow");
					bot.Sleep(2000);
					Core.Jump("Cut", "Left");
					bot.Sleep(4000);
					Core.Jump("Act9", "Left");
					bot.Player.Kill("Junior - Drummer");
					bot.Sleep(2000);
					Core.Jump("Act10", "Left");
					bot.Player.Kill("Mr. Socks - Bassist");
					bot.Sleep(2000);
					Core.SmartKillMonster(qIDs[i], "palooza", "Kimberly");
					bot.Sleep(2000);
					Core.Jump("Cut", "Left");
					bot.Sleep(4000);
					Core.Jump("End", "Left");
					break;
			}
			Core.EnsureComplete(qIDs[i]);
			Core.Logger($"Finished {i}");
			Core.Rest();
		}

		Core.SetOptions(false);
	}
}