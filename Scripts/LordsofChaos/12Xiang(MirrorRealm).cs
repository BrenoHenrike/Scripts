//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaMirrorRealm
{
	public CoreBots Core => CoreBots.Instance;

	public int questStart = 0;

	public string OptionsStorage = "SagaMirrorRealm";
	public bool DontPreconfigure = true;

	public List<IOption> Options = new List<IOption>()
	{
		new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
	};

	public static readonly int[] qIDs = {
		/* -- /Join BattleOff --                                                                                   */
			2909, /*  0 - Bright Idea                                                                            */
			2910, /*  1 - Spare Parts                                                                            */
			2911, /*  2 - Power It Uo                                                                            */
			2912, /*  3 - Filthy Creatures                                                                       */
		/* -- /Join BrightFall --                                                                                  */
			2913, /*  4 - Wave After Wave                                                                        */
			2914, /*  5 - Take out Their Firepower                                                               */
			2915, /*  6 - Help Where It is Needed                                                                */
			2916, /*  7 - Bring A Ward To A Swordfight                                                           */
			2917, /*  8 - Cut Off The Head                                                                       */
		/* -- /Join OverWorld --                                                                                   */
			2919, /*  9 - Rearm The Legion of Light                                                              */
			2920, /* 10 - Free Their Souls                                                                       */
			2921, /* 11 - One Ring                                                                               */
			2922, /* 12 - Severing Ties                                                                          */
			2923, /* 13 - Legion's Lifesblood                                                                    */
			2924, /* 14 - Legion's Purpose                                                                       */
			2925, /* 15 - What's His Endgame                                                                     */
			2926, /* 16 - A Stopping Block                                                                       */
			2927, /* 17 - Boost Morale                                                                           */
			2928, /* 18 - Alteon's Folly                                                                         */
			2929, /* 19 - DoomFire                                                                               */
			2930, /* 20 - Spoiled Souls                                                                          */
			2931, /* 21 - Purity of Bone                                                                         */
			2932, /* 22 - Undead Artix Returns!                                                                  */
		/* -- /Join RedDeath --                                                                                    */
			3166, /* 23 - I Can't Touch This                                                                     */
			3167, /* 24 - Nope, Still a Ghost                                                                    */
			3168, /* 25 - First We Need a Beacon...                                                              */
			3169, /* 26 - Light It Up                                                                            */
			3170, /* 27 - ...Next We need a Trap                                                                 */
			3171, /* 28 - For Spirits, Not People                                                                */
			3172, /* 29 - Still To Fragile                                                                       */
		/* -- /Join MirrorPortal --                                                                                */
			3183, /* 30 - Craft a Better Defense                                                                 */
			0   , /* 31 - Reflect the Damage / Pure Chaos, Corrupted Blood / Enemies of a Feather Flock Together */
			3187, /* 32 - Ward Off the Beast                                                                     */
			3188, /* 33 - Horror Takes Flight                                                                    */
			3189 /* 34 - Good, Evil and Chaos Battle!														   */
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
				case 0: //Bright Idea
					Core.GetMapItem(1779, map: "battleoff");
					break;
				case 1: //Spare Parts
					Core.GetMapItem(1780, 8, "battleoff");
					break;
				case 2: //Power It Uo
					Core.SmartKillMonster(qIDs[i], "battleoff", "Evil Moglin");
					break;
				case 3: //Filthy Creatures
					Core.SmartKillMonster(qIDs[i], "battleoff", "Evil Moglin");
					break;
				case 4: //Wave After Wave
					Core.SmartKillMonster(qIDs[i], "brightfall", "Undead Minion");
					break;
				case 5: //Take out Their Firepower
					Core.SmartKillMonster(qIDs[i], "brightfall", "Undead Mage");
					break;
				case 6: //Help Where It is Needed
					Core.GetMapItem(1781, 6, "brightfall");
					break;
				case 7: //Bring A Ward To A Swordfight
					Core.GetMapItem(1782, 8, "brightfall");
					break;
				case 8: //Cut Off The Head
					Core.SmartKillMonster(qIDs[i], "brightfall", "Painadin Overlord");
					break;
				case 9: //Rearm The Legion of Light
                    bot.Player.Join("overworld");
                    bot.SendPacket("%xt%zm%tryQuestComplete%96506%2918%-1%false%wvz%");
                    Core.SmartKillMonster(qIDs[i], "overworld", "Undead Minion");
					break;
				case 10: //Free Their Souls
					Core.SmartKillMonster(qIDs[i], "overworld", "Undead Minion");
					break;
				case 11: //One Ring
					Core.SmartKillMonster(qIDs[i], "overworld", "Undead Minion");
					break;
				case 12: //Severing Ties
					Core.SmartKillMonster(qIDs[i], "overworld", "Undead Mage");
					break;
				case 13: //Legion's Lifesblood
					Core.GetMapItem(1800, 6, "overworld");
					break;
				case 14: //Legion's Purpose
					Core.SmartKillMonster(qIDs[i], "overworld", "Undead Bruiser");
					break;
				case 15: //What's His Endgame
					Core.SmartKillMonster(qIDs[i], "overworld", "Undead Bruiser");
					break;
				case 16: //A Stopping Block
					Core.SmartKillMonster(qIDs[i], "overworld", "Undead Minion|Undead Mage|Undead Bruiser");
					break;
				case 17: //Boost Morale
					Core.GetMapItem(1801, 8, "overworld");
					break;
				case 18: //Alteon's Folly
					Core.SmartKillMonster(qIDs[i], "overworld", "Undead Minion|Undead Mage|Undead Bruiser");
					break;
				case 19: //DoomFire
					Core.SmartKillMonster(qIDs[i], "overworld", "Undead Minion|Undead Mage|Undead Bruiser");
					break;
				case 20: //Spoiled Souls
					Core.SmartKillMonster(qIDs[i], "overworld", "Undead Minion|Undead Mage|Undead Bruiser");
					break;
				case 21: //Purity of Bone
					Core.GetMapItem(1802, 10, "overworld");
					break;
				case 22: //Undead Artix Returns!
					Core.SmartKillMonster(qIDs[i], "overworld", "Undead Artix");
					break;
				case 23: //I Can't Touch This
					Core.SmartKillMonster(qIDs[i], "reddeath", "Fire Leech|Grim Widow|Reddeath Moglin|Swamp Wraith");
					break;
				case 24: //Nope, Still a Ghost
					Core.SmartKillMonster(qIDs[i], "reddeath", "Reddeath Moglin");
					Core.GetMapItem(2178, map: "reddeath");
					Core.GetMapItem(2179);
					break;
				case 25: //First We Need a Beacon...
					Core.GetMapItem(2180, map: "reddeath");
					break;
				case 26: //Light It Up
					Core.SmartKillMonster(qIDs[i], "reddeath", "Fire Leech");
					break;
				case 27: //...Next We need a Trap
					Core.SmartKillMonster(qIDs[i], "reddeath", "Grim Widow");
					break;
				case 28: //For Spirits, Not People
					Core.SmartKillMonster(qIDs[i], "reddeath", "Swamp Wraith");
					break;
				case 29: //Still To Fragile
					Core.SmartKillMonster(qIDs[i], "reddeath", "Swamp Wraith");
					break;
				case 30: //Craft a Better Defense
					Core.GetMapItem(2203, map: "battleontown");
					break;
				case 31: //Reflect the Damage / Pure Chaos, Corrupted Blood / Enemies of a Feather Flock Together
					Core.SmartKillMonster(3184, "earthstorm", "Shard Spinner", completeQuest: true);
					Core.SmartKillMonster(3185, "bloodtuskwar", "Chaotic Vulture|Chaotic Horcboar", completeQuest: true);
					Core.SmartKillMonster(3186, "bloodtuskwar", "Chaos Tigriff|Chaotic Vulture", completeQuest: true);
					break;
				case 32: //Ward Off the Beast
					bot.Player.Join("mirrorportal");
					bot.Sleep(2500);
					break;
				case 33: //Horror Takes Flight
					Core.SmartKillMonster(qIDs[i], "mirrorportal", "Chaos Harpy");
					break;
				case 34: //Good, Evil and Chaos Battle!
					Core.SmartKillMonster(qIDs[i], "mirrorportal", "Chaos Lord Xiang");
					break;

			}
			Core.EnsureComplete(qIDs[i]);
			bot.Player.Pickup("Shriekward Potion");
			Core.Logger($"Finished {i}");
			Core.Rest();
		}

		Core.SetOptions(false);
	}
}