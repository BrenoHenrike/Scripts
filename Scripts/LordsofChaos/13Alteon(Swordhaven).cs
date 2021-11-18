//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaSwordhaven
{
	public CoreBots Core => CoreBots.Instance;

	public int questStart = 0;

	public string OptionsStorage = "SagaSwordhaven";
	public bool DontPreconfigure = true;

	public List<IOption> Options = new List<IOption>()
	{
		new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
	};

	public static readonly int[] qIDs = 
	{
		/* -- /Join Archives --  */
			3077, /* 0  - Bandit Bounty */
			3078, /* 1  - Thwarting the Spies */
			3079, /* 2  - Fight Chaos With Clerics */
			3080, /* 3  - Locate the Source */
			3081, /* 4  - Plagued Rats */
			3082, /* 5  - Nope, Nope, Nope! */
			3083, /* 6  - Still More Research To Be Done! */
			3084, /* 7  - That's One Big Sludgebeast. */
		/* -- /Join Armory --  */
			3094, /* 8  - Back to Jail With You! */
			3095, /* 9  - We May Need A Militia */
			3096, /* 10 - An Ounce Of Prevention */
			0   , /* 11 - Axe Them To Leave! / Freeze 'Em Out! / Burn 'Em Up! */
			3092, /* 12 - Under Siege */
			3093, /* 13 - No, NOW We're Under Siege */
		/* -- /Join Ceremony --  */
			3120, /* 14 - Chaos Not Invited */
			3121, /* 15 - Better Letter Go! */
			3122, /* 16 - Decor Rater */
			3123, /* 17 - Cold Feet, Warm Heart */
			3124, /* 18 - Chaos STILL Not Invited */
			3125, /* 19 - Protect the Princesses */
			3126, /* 20 - Seal the Chapel */
			3127, /* 21 - Chaos Kills! */
		/* -- /Join ChaosAltar --  */
			3133, /* 22 - Endless Aisle of Chaos */
			3134, /* 23 - Save the Princess... Again! */
		/* -- /Join CastleRoof --  */
			3158, /* 24 - Chaos Dragon Confrontation */
			3159, /* 25 - To Catch a King */
			3160  /* 26 - Chaos Lord Alteon */
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
				case 0: //Bandit Bounty
					Core.SmartKillMonster(qIDs[i], "archives", "Chaos Bandit");
					break;
				case 1: //Thwarting the Spies
					Core.SmartKillMonster(qIDs[i], "archives", "Camouflaged Sp-Eye");
					break;
				case 2: //Fight Chaos With Clerics
					Core.SmartKillMonster(qIDs[i], "archives", new[] {"Chaos Bandit", "Camouflaged Sp-Eye" });
					break;
				case 3: //Locate the Source
					Core.SmartKillMonster(qIDs[i], "archives", "Chaos Bandit|Camouflaged Sp-Eye");
					Core.GetMapItem(1937, map: "archives");
					break;
				case 4: //Plagued Rats
					Core.SmartKillMonster(qIDs[i], "archives", "Chaos Rat");
					break;
				case 5: //Nope, Nope, Nope!
					Core.SmartKillMonster(qIDs[i], "archives", "Chaos Spider");
					break;
				case 6: //Still More Research To Be Done!
					Core.SmartKillMonster(qIDs[i], "archives", new[] { "Chaos Spider", "Chaos Rat" });
					break;
				case 7: //That's One Big Sludgebeast.
					Core.SmartKillMonster(qIDs[i], "archives", "Sludgelord");
					break;
				case 8: //Back to Jail With You!
					Core.SmartKillMonster(qIDs[i], "armory", "Chaorrupted Prisoner");
					break;
				case 9: //We May Need A Militia
					Core.SmartKillMonster(qIDs[i], "armory", "Chaorrupted Prisoner");
					Core.GetMapItem(1956, 4, "armory");
					break;
				case 10: //An Ounce Of Prevention
					Core.SmartKillMonster(qIDs[i], "armory", "Chaos Drifter");
					break;
				case 11: //Axe Them To Leave! / Freeze 'Em Out! / Burn 'Em Up!
					Core.SmartKillMonster(3089, "armory", "Chaorrupted Prisoner");
					Core.SmartKillMonster(3090, "armory", "Chaos Mage");
					Core.SmartKillMonster(3091, "armory", "Chaos Mage");
					break;
				case 12: //Under Siege
					Core.GetMapItem(1957, map: "armory");
					break;
				case 13: //No, NOW We're Under Siege
					Core.SmartKillMonster(qIDs[i], "armory", "Chaos General");
					break;
				case 14: //Chaos Not Invited
					Core.SmartKillMonster(qIDs[i], "ceremony", "Chaos Invader");
					break;
				case 15: //Better Letter Go!
					Core.GetMapItem(2108, map: "yulgar");
					Core.GetMapItem(2109);
					Core.GetMapItem(2110);
					Core.GetMapItem(2111, map: "archives");
					Core.GetMapItem(2112, map: "swordhaven");
					Core.GetMapItem(2113);
					Core.GetMapItem(2114);
					Core.GetMapItem(2115);
					break;
				case 16: //Decor Rater
					Core.GetMapItem(2116, 8, "swordhaven");
					break;
				case 17: //Cold Feet, Warm Heart
					Core.SmartKillMonster(qIDs[i], "mafic", "Living Fire");
					break;
				case 18: //Chaos STILL Not Invited
					Core.SmartKillMonster(qIDs[i], "ceremony", "Chaos Invader");
					break;
				case 19: //Protect the Princesses
					Core.GetMapItem(2118, 6, "ceremony");
					break;
				case 20: //Seal the Chapel
					Core.GetMapItem(2119, map: "ceremony");
					Core.SmartKillMonster(qIDs[i], "ceremony", "Chaos Invader");
					break;
				case 21: //Chaos Kills!
					Core.SmartKillMonster(qIDs[i], "ceremony", "Chaos Justicar");
					break;
				case 22: //Endless Aisle of Chaos
					Core.GetMapItem(2127, 12, "chaosaltar");
					break;
				case 23: //Save the Princess... Again!
					Core.SmartKillMonster(qIDs[i], "chaosaltar", "Princess Thrall");
					break;
				case 24: //Chaos Dragon Confrontation
					Core.SmartKillMonster(qIDs[i], "chaosroof", "Chaos Dragon");
					break;
				case 25: //To Catch a King
					Core.GetMapItem(2158, map: "swordhavenfalls");
					break;
				case 26: //Chaos Lord Alteon
					Core.SmartKillMonster(qIDs[i], "swordhavenfalls", "Chaos Lord Alteon");
					break;
			}
			Core.EnsureComplete(qIDs[i]);
			Core.Logger($"Finished {i}");
			Core.Rest();
		}

		Core.SetOptions(false);
	}
}