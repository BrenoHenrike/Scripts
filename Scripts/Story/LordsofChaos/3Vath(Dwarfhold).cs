//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaDwarfhold
{
	public CoreBots Core => CoreBots.Instance;

	public int questStart = 0;

	public string OptionsStorage = "SagaDwarfhold";
	public bool DontPreconfigure = true;

	public List<IOption> Options = new List<IOption>()
	{
		new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
	};

	public static readonly int[] qIDs =
	{
		/* -- /Join Tavern --	                                                        */
			319, /*  0 - Adorable Sisters                                               */
			320, /*  1 - Warm and Furry                                                 */
			321, /*  2 - Shell Rock                                                     */
			322, /*  3 - Bear Facts                                                     */
		/* -- /Join Llama --                                                            */
			324, /*  4 - The Spittoon Saloon                                            */
			325, /*  5 - Bear it all!                                                   */
			326, /*  6 - Leather Feathers                                               */
			327, /*  7 - Follow your Nose!                                              */
		/* -- /Join Tavern --                                                           */
			323, /*  8 - Give Snowbeard his Gold ****Needs item from previous quest**** */
		/* -- /Join Dwarfhold --                                                        */
			344, /*  9 - Bad Memory                                                     */
			331, /* 10 - Squeeze Water from Stone                                       */
			332, /* 11 - Carrion Carrying On                                            */
			333, /* 12 - Bagged Lunch                                                   */
			334, /* 13 - Radiant Lamps                                                  */
			335, /* 14 - Having a Blast                                                 */
			336, /* 15 - Secret Weapons                                                 */
			337, /* 16 - Rock Star                                                      */
			338, /* 17 - All that Glitters                                              */
			339, /* 18 - Gemeralds                                                      */
			340, /* 19 - Talc to Me                                                     */
			0  , /* 20 - Uppercity Door                                                 */
			341, /* 21 - Rock me Amadeus                                                */
		/* -- /Join UpperCity --                                                        */
			346, /* 22 - Disapoofed                                                     */
			347, /* 23 - Hoodwinked                                                     */
			348, /* 24 - Claws for the Cause                                            */
			349, /* 25 - Scrambled Eggs                                                 */
			350, /* 26 - The King's Wings                                               */
			351, /* 27 - Bugging Out                                                    */
			352, /* 28 - Lizard Gizzard                                                 */
			0  , /* 29 - Confront Vath                                                  */
			353, /* 30 - Mock the Lock                                                  */
			0  , /* 31 - Mocking the lock                                               */
			355, /* 32 - Jailhouse Rock                                                 */
			356, /* 33 - Explosives 101                                                 */
			0  , /* 34 - Exploding                                                      */
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
			switch (i)
			{
				case 0: //Adorable Sisters
					Core.GetMapItem(56, 7, "tavern");
					break;
				case 1: //Warm and Furry
					Core.SmartKillMonster(qIDs[i], "pines", "Pine Grizzly");
					break;
				case 2: //Shell Rock
					Core.SmartKillMonster(qIDs[i], "pines", "Red Shell Turtle");
					break;
				case 3: //Bear Facts
					Core.SmartKillMonster(qIDs[i], "pines", "Twistedtooth");
					break;
				case 4: //The Spittoon Saloon
					Core.SmartKillMonster(qIDs[i], "pines", "Red Shell Turtle");
					break;
				case 5: //Bear it all!
					Core.SmartKillMonster(qIDs[i], "pines", "Pine Grizzly");
					break;
				case 6: //Leather Feathers
					Core.SmartKillMonster(qIDs[i], "pines", "Leatherwing");
					break;
				case 7: //Follow your Nose!
					Core.Logger("#Start 7 if stuck in 8#");
					Core.SmartKillMonster(qIDs[i], "pines", "Pine Troll");
					break;
				case 8: //Give Snowbeard his Gold ****Needs item from previous quest****
					Core.Logger("#Start 7 if stuck in 8#");
					bot.Player.Join("tavern");
					bot.Sleep(2000);
					break;
				case 9: //Bad Memory
					Core.GetMapItem(60, map: "dwarfhold");
					break;
				case 10: //Squeeze Water from Stone
					Core.SmartKillMonster(qIDs[i], "mountainpath", "Ore Balboa");
					break;
				case 11: //Carrion Carrying On
					Core.SmartKillMonster(qIDs[i], "mountainpath", "Vultragon");
					break;
				case 12: //Bagged Lunch
					Core.SmartKillMonster(qIDs[i], "dwarfhold", "Chaos Drow");
					break;
				case 13: //Radiant Lamps
					Core.SmartKillMonster(qIDs[i], "dwarfhold", "Glow Worm");
					break;
				case 14: //Having a Blast
					Core.SmartKillMonster(qIDs[i], "dwarfhold", "Albino Bat");
					break;
				case 15: //Secret Weapons
					Core.SmartKillMonster(qIDs[i], "dwarfhold", "Chaotic Draconian");
					break;
				case 16: //Rock Star
					Core.GetMapItem(59, 7, "dwarfhold");
					break;
				case 17: //All that Glitters
					Core.SmartKillMonster(qIDs[i], "dwarfhold", "Chaos Drow");
					break;
				case 18: //Gemeralds
					Core.SmartKillMonster(qIDs[i], "dwarfhold", "Chaotic Draconian");
					break;
				case 19: //Talc to Me
					Core.SmartKillMonster(qIDs[i], "dwarfhold", "Albino Bat");
					break;
				case 20: //Uppercity Door
					bot.Player.Join("dwarfhold");
					Core.Jump("rdoor", "Right");
					bot.SendPacket("%xt%zm%tryQuestComplete%8273%343%-1%false%wvz%");
					bot.Sleep(2500);
					break;
				case 21: //Rock me Amadeus
					Core.SmartKillMonster(qIDs[i], "dwarfhold", "Amadeus");
					break;
				case 22: //Disapoofed
					Core.GetMapItem(61, map: "uppercity");
					break;
				case 23: //Hoodwinked
					Core.SmartKillMonster(qIDs[i], "uppercity", "Drow Assassin");
					break;
				case 24: //Claws for the Cause
					Core.SmartKillMonster(qIDs[i], "uppercity", "Chaotic Draconian");
					break;
				case 25: //Scrambled Eggs
					Core.SmartKillMonster(qIDs[i], "uppercity", "Chaos Egg");
					break;
				case 26: //The King's Wings
					Core.SmartKillMonster(qIDs[i], "uppercity", "Terradactyl");
					break;
				case 27: //Bugging Out
					Core.SmartKillMonster(qIDs[i], "uppercity", "Rhino Beetle");
					break;
				case 28: //Lizard Gizzard
					Core.SmartKillMonster(qIDs[i], "uppercity", "Cave Lizard");
					break;
				case 29: //Confront Vath
					bot.Player.Join("vath");
					bot.Player.Jump("CutCap", "Left");
					bot.Sleep(2500);
					break;
				case 30: //Mock the Lock
					Core.SmartKillMonster(qIDs[i], "dwarfprison", new[] { "Balboa", "Albino Bat", "Chaos Drow" });
					break;
				case 31: //Mocking the lock
					bot.Player.Join("dwarfprison");
					Core.Jump("Enter", "Right");
					bot.SendPacket("%xt%zm%tryQuestComplete%2434%354%-1%false%wvz%");
					break;
				case 32: //Jailhouse Rock
					Core.SmartKillMonster(qIDs[i], "dwarfprison", "Warden Elfis");
					break;
				case 33: //Explosives 101
					Core.SmartKillMonster(qIDs[i], "dwarfprison", new[] { "Balboa", "Albino Bat", "Chaos Drow" });
					break;
				case 34: //Exploding
					bot.Player.Join("dwarfprison");
					bot.SendPacket("%xt%zm%tryQuestComplete%2434%357%-1%false%wvz%");
					break;
			}
			Core.EnsureComplete(qIDs[i]);
			Core.Logger($"Finished {i}");
			Core.Rest();
		}

		Core.SetOptions(false);
	}
}
