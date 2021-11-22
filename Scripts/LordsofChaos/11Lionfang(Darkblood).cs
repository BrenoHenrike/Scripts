//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaDarkblood
{
	public CoreBots Core => CoreBots.Instance;

	public int questStart = 0;

	public string OptionsStorage = "SagaDarkblood";
	public bool DontPreconfigure = true;

	public List<IOption> Options = new List<IOption>()
	{
		new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
	};

	public static readonly int[] qIDs =
	{
		/* -- /Join Blackhorn --                                       */
			2612, /*  0 - Final Rest                                   */
			2613, /*  1 - Disturbing The Peace                         */
			2614, /*  2 - Sampling Silk                                */
			2615, /*  3 - Fire Is The Thing                            */
			2616, /*  4 - The Wall Comes Down                          */
			2617, /*  5 - The Bonefeeder                               */
			2618, /*  6 - What Lies Beyond?                            */
			2619, /*  7 - Toxic                                        */
			2620, /*  8 - Very Toxic                                   */
			2621, /*  9 - Really, VERY, VERY TOXIC!                    */
		/* -- /Join OnslaughtTower --                                  */
			2622, /* 10 - /Lion Hunting                                */
			2623, /* 11 - Secret Of The Death Fog                      */
			2624, /* 12 - The Key To Survival                          */
			2625, /* 13 - The Tools                                    */
			2626, /* 14 - The Talent                                   */
			2627, /* 15 - The Local Locale                             */
			2628, /* 16 - Who Holds The Key?                           */
			2629, /* 17 - Leave No Rug Unturned                        */
			2630, /* 18 - Tame The Lion                                */
		/* -- /Join Falguard --                                        */
			2666, /* 19 - Take Up The Cause                            */
			2667, /* 20 - Well Kept Secrets                            */
			2668, /* 21 - Feeding On The Fallen                        */
			2669, /* 22 - Special Delivery                             */
			2670, /* 23 - Precious Scraps                              */
			2671, /* 24 - Restocking                                   */
			2672, /* 25 - An Innside Job                               */
			2673, /* 26 - Streets Run Red                              */
			2674, /* 27 - Open the Temple                              */
			2675, /* 28 - The Open Temple                              */
		/* -- /Join DeathPits --                                       */
			2720, /* 29 - Remains                                      */
			2721, /* 30 - Thriving In Rot                              */
			2722, /* 31 - Rotting Ribs                                 */
			2723, /* 32 - A Perfect Skull                              */
			2724, /* 33 - Deeper Into Death                            */
			2725, /* 34 - Precise Placement                            */
			2726, /* 35 - Painted Protection                           */
			2727, /* 36 - They Sense You                               */
			2728, /* 37 - They Hate You                                */
			2729, /* 38 - The Sundered                                 */
			2730, /* 39 - Rotstone                                     */
			2731, /* 40 - Honor The Dead                               */
			2732, /* 41 - Ties to Life                                 */
			2740, /* 42 - Destroy Wrathful Vestis and Secure The Tears */
		/* -- /Join VenomVaults --                                     */
			2792, /* 43 - Surveillance for Sir Valence                 */
			2793, /* 44 - Well Planned Getaway                         */
			2794, /* 45 - Secrets Of The Mad Prince                    */
			2796, /* 46 - Potion of Cleansing                          */
			2797, /* 47 - You've Been Noticed                          */
			2798, /* 48 - Thorny Situations                            */
			2799, /* 49 - Other Ingredients                            */
			2800, /* 50 - Time For Supplies                            */
			2801, /* 51 - Cooking Without Fire                         */
			2802, /* 52 - Introduction                                 */
			2803, /* 53 - Courtyard Key                                */
			2804, /* 54 - Take Out The Chaos Manticore!                */
		/* -- /Join StormTemple --                                     */
			2805, /* 55 - Shocking Footwear                            */
			2806, /* 56 - New Shoes                                    */
			2807, /* 57 - Mouth Of The Lion                            */
			2808, /* 58 - Storm the Storm Temple                       */
			2809, /* 59 - A High Minded Matter                         */
			2810, /* 60 - Storm Bottles                                */
			2811, /* 61 - Breaching Defenses                           */
			2812, /* 62 - Chaos Lightning Rods                         */
			2813, /* 63 - Barrier Buster                               */
			2814  /* 64 - Face Chaos Lord Lionfang!                    */
	};
	
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();
        Core.AddDrop("Mage Glove (Required)");

        questStart = bot.Config.Get<int>("startQuest");

		for (int i = questStart; i < qIDs.Length; i++)
		{
			bot.Config.Set("startQuest", i);
			Core.Logger($"Starting {i}");
			Core.EnsureAccept(qIDs[i]);
			switch(i)
			{
				case 0: //Final Rest
					Core.SmartKillMonster(qIDs[i], "blackhorn", "Restless Undead");
					break;
				case 1: //Disturbing The Peace
					Core.GetMapItem(1615, 10, "blackhorn");
					break;
				case 2: //Sampling Silk
					Core.SmartKillMonster(qIDs[i], "blackhorn", "Tomb Spider");
					break;
				case 3: //Fire Is The Thing
					Core.GetMapItem(1616, map: "blackhorn");
					Core.SmartKillMonster(qIDs[i], "blackhorn", new[] { "Tomb Spider", "Restless Undead" });
					break;
				case 4: //The Wall Comes Down
					Core.GetMapItem(1617, map: "blackhorn");
					break;
				case 5: //The Bonefeeder
					Core.SmartKillMonster(qIDs[i], "blackhorn", "Bonefeeder Spider");
					break;
				case 6: //What Lies Beyond?
					Core.GetMapItem(1618, map: "blackhorn");
					break;
				case 7: //Toxic
					Core.SmartKillMonster(qIDs[i], "blackhorn", "Tomb Spider");
					break;
				case 8: //Very Toxic
					Core.SmartKillMonster(qIDs[i], "blackhorn", "Restless Undead");
					break;
				case 9: //Really, VERY VERY TOXIC!
					Core.GetMapItem(1619, map: "blackhorn");
					break;
				case 10: //Lion Hunting
					Core.GetMapItem(1620, map: "onslaughttower");
					Core.GetMapItem(1621);
					Core.GetMapItem(1622);
					Core.GetMapItem(1623);
					break;
				case 11: //Secret Of The Death Fog
					Core.SmartKillMonster(qIDs[i], "onslaughttower", "Golden Caster|Golden Warrior|Golden Cavalry");
					break;
				case 12: //The Key To Survival
					Core.SmartKillMonster(qIDs[i], "onslaughttower", "Golden Caster|Golden Warrior|Golden Cavalry");
					break;
				case 13: //The Tools
					Core.GetMapItem(1624, 8, "onslaughttower");
					break;
				case 14: //The Talent
					Core.GetMapItem(1625, map: "onslaughttower");
					break;
				case 15: //The Local Locale
					Core.GetMapItem(1626, 4, "onslaughttower");
					break;
				case 16: //Who Holds The Key?
					Core.SmartKillMonster(qIDs[i], "onslaughttower", "Golden Caster|Golden Warrior|Golden Cavalry");
					break;
				case 17: //Leave No Rug Unturned
					Core.GetMapItem(1627, map: "onslaughttower");
					break;
				case 18: //Tame The Lion
					Core.SmartKillMonster(qIDs[i], "onslaughttower", "Maximillian Lionfang");
					break;
				case 19: //Take Up The Cause
					Core.SmartKillMonster(qIDs[i], "falguard", "Chaonslaught Caster|Chaonslaught Warrior|Chaonslaught Cavalry");
					break;
				case 20: //Well Kept Secrets
					Core.GetMapItem(1628, 6, "falguard");
					break;
				case 21: //Feeding On The Fallen
					Core.SmartKillMonster(qIDs[i], "falguard", new[] { "Chaonslaught Warrior", "Chaonslaught Cavalry" });
					break;
				case 22: //Special Delivery
					Core.GetMapItem(1629, map: "falguard");
					break;
				case 23: //Precious Scraps
					Core.SmartKillMonster(qIDs[i], "falguard", "Chaonslaught Warrior|Chaonslaught Cavalry");
					break;
				case 24: //Restocking
					Core.GetMapItem(1630, map: "falguard");
					break;
				case 25: //An Innside Job
					Core.GetMapItem(1631, map: "falguard");
					break;
				case 26: //Streets Run Red
					Core.SmartKillMonster(qIDs[i], "falguard", "Chaonslaught Caster");
					break;
				case 27: //Open the Temple
					Core.GetMapItem(1632, map: "falguard");
					break;
				case 28: //The Open Temple
					Core.SmartKillMonster(qIDs[i], "falguard", "Primarch");
					break;
				case 29: //Remains
					Core.SmartKillMonster(qIDs[i], "deathpits", "Rotting Darkblood");
					break;
				case 30: //Thriving In Rot
					Core.GetMapItem(1691, 5, "deathpits");
					break;
				case 31: //Rotting Ribs
					Core.SmartKillMonster(qIDs[i], "deathpits", "Rotting Darkblood");
					break;
				case 32: //A Perfect Skull
					Core.SmartKillMonster(qIDs[i], "deathpits", "Rotting Darkblood");
					break;
				case 33: //Deeper Into Death
					Core.SmartKillMonster(qIDs[i], "deathpits", "Ghastly Darkblood");
					break;
				case 34: //Precise Placement
					Core.SmartKillMonster(qIDs[i], "deathpits", "Ghastly Darkblood");
					break;
				case 35: //Painted Protection
					Core.GetMapItem(1692, 6, "deathpits");
					break;
				case 36: //They Sense You
					Core.SmartKillMonster(qIDs[i], "deathpits", "Rotting Darkblood");
					break;
				case 37: //They Hate You
					Core.SmartKillMonster(qIDs[i], "deathpits", "Ghastly Darkblood");
					break;
				case 38: //The Sundered
					Core.SmartKillMonster(qIDs[i], "deathpits", "Sundered Darkblood");
					break;
				case 39: //Rotstone
					Core.GetMapItem(1693, 9, "deathpits");
					break;
				case 40: //Honor The Dead
					Core.SmartKillMonster(qIDs[i], "deathpits", new[] {"Sundered Darkblood", "Ghastly Darkblood", "Rotting Darkblood" });
					break;
				case 41: //Ties to Life
					Core.GetMapItem(1694, 12, "deathpits");
					break;
				case 42: //Destroy Wrathful Vestis and Secure The Tears
					Core.SmartKillMonster(qIDs[i], "deathpits", "Wrathful Vestis");
					Core.GetMapItem(1695, map: "deathpits");
					break;
				case 43: //Surveillance for Sir Valence
					Core.GetMapItem(1724, map: "venomvaults");
					break;
				case 44: //Well Planned Getaway
					Core.SmartKillMonster(qIDs[i], "venomvaults", "Chaonslaught Warrior");
					break;
				case 45: //Secrets Of The Mad Prince
					Core.GetMapItem(1725, map: "venomvaults");
					break;
				case 46: //Potion of Cleansing
					Core.GetMapItem(1726, map: "venomvaults");
					break;
				case 47: //You've Been Noticed
					Core.SmartKillMonster(qIDs[i], "venomvaults", "Chaonslaught Caster");
					break;
				case 48: //Thorny Situations
					Core.GetMapItem(1727, 5, "venomvaults");
					break;
				case 49: //Other Ingredients
					Core.SmartKillMonster(qIDs[i], "venomvaults", "Chaonslaught Caster");
					break;
				case 50: //Time For Supplies
					Core.SmartKillMonster(qIDs[i], "venomvaults", "Chaonslaught Warrior");
					break;
				case 51: //Cooking Without Fire
					Core.SmartKillMonster(qIDs[i], "venomvaults", "Chaonslaught Caster|Chaonslaught Warrior");
					break;
				case 52: //Introduction
					Core.GetMapItem(1728, 3, "venomvaults");
					break;
				case 53: //Courtyard Key
					Core.SmartKillMonster(qIDs[i], "venomvaults", "Chaonslaught Caster|Chaonslaught Warrior");
					break;
				case 54: //Take Out The Chaos Manticore!
					Core.SmartKillMonster(qIDs[i], "venomvaults", "Manticore");
					break;
				case 55: //Shocking Footwear
					Core.GetMapItem(1729, 4, "stormtemple");
					break;
				case 56: //New Shoes
					Core.SmartKillMonster(qIDs[i], "stormtemple", "Chaonslaught Warrior");
					break;
				case 57: //Mouth Of The Lion
					Core.SmartKillMonster(qIDs[i], "stormtemple", "Chaonslaught Caster|Chaonslaught Warrior|Chaonslaught Cavalry");
					break;
				case 58: //Storm the Storm Temple
					Core.SmartKillMonster(qIDs[i], "stormtemple", "Chaonslaught Caster|Chaonslaught Warrior|Chaonslaught Cavalry");
					break;
				case 59: //A High Minded Matter
					Core.GetMapItem(1730, 3, "stormtemple");
					break;
				case 60: //Storm Bottles
					Core.SmartKillMonster(qIDs[i], "stormtemple", "Chaonslaught Caster");
					break;
				case 61: //Breaching Defenses
					Core.GetMapItem(1731, map: "stormtemple");
					break;
				case 62: //Chaos Lightning Rods
					Core.SmartKillMonster(qIDs[i], "stormtemple", "Chaonslaught Cavalry");
					break;
				case 63: //Barrier Buster
					Core.GetMapItem(1732, map: "stormtemple");
					break;
				case 64: //Face Chaos Lord Lionfang!
					Core.SmartKillMonster(qIDs[i], "stormtemple", "Chaos Lord Lionfang");
					break;
			}
			Core.EnsureComplete(qIDs[i]);
            bot.Player.Pickup("Mage Glove (Required)");
            Core.Logger($"Finished {i}");
			Core.Rest();
		}

		Core.SetOptions(false);
	}
}