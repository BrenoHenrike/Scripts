//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaChiralValley
{
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core => CoreBots.Instance;

	public int questStart = 0;

	public string OptionsStorage = "SagaChiralValley";
	public bool DontPreconfigure = true;

	public List<IOption> Options = new List<IOption>()
	{
		new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
	};

	public static readonly int[] qIDs = 
	{
		/* -- /Join Mobius --                      */
			245, /*  0 - Winged Spies              */
			246, /*  1 - Chaos Prisoners           */
			247, /*  2 - IMP-possible Task         */
			260, /*  3 - You Can't Miss It         */
			248, /*  4 - Far Sighted               */
			249, /*  5 - Slugfest                  */
		/* -- /Join Faerie --                      */
			250, /*  6 - Chain Reaction            */
			251, /*  7 - Epic Drops                */
			252, /*  8 - Jarring Theft             */
			255, /*  9 - Tree Hugger               */
			256, /* 10 - The Second Piece          */
		/* -- /Join Cornelis --                    */
			257, /* 11 - Ruined Ruins              */
			261, /* 12 - Energize!                 */
			258, /* 13 - Blueish Glow              */
			262, /* 14 - Quickdraw                 */
			259, /* 15 - Arm Yourself              */
			263, /* 16 - You've Been Framed        */
		/* -- /Join Mobius --                      */
			266, /* 17 - Some Assembly Required    */
		/* -- /Join Cornelis --                    */
			267, /* 18 - Teleporter Report         */
			264, /* 19 - Disguise!                 */
			265, /* 20 - To-go box                 */
		/* -- /Join Relativity -- 				   */
			268, /* 21 - Find the Key! (Part One)  */
			269, /* 22 - Find the Key! (Part Two)  */
			270, /* 23 - Find the Key! (Part Three)*/
		/* -- /Join Hydra --					   */
			271, /* 24 - The Lake Hydra            */
		/* -- /Join Escherion -- 				   */
			272  /* 25 - Escherion                 */
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
				case 0: //Winged Spies
					Core.SmartKillMonster(qIDs[i], "mobius", "Chaos Sp-Eye");
					break;
				case 1: //Chaos Prisoners
					Core.GetMapItem(42, 5, "mobius");
					break;
				case 2: //IMP-possible Task
					Core.SmartKillMonster(qIDs[i], "mobius", "Fire Imp");
					break;
				case 3: //You Can't Miss It
					Core.GetMapItem(44, map: "mobius");
					break;
				case 4: //Far Sighted
					Core.SmartKillMonster(qIDs[i], "mobius", "Cyclops Raider");
					break;
				case 5: //Slugfest
					Core.SmartKillMonster(qIDs[i], "mobius", "Slugfit");
					break;
				case 6: //Chain Reaction
					Core.SmartKillMonster(qIDs[i], "faerie", "Chainsaw Sneevil");
					break;
				case 7: //Epic Drops
					Core.GetMapItem(43, 7, "faerie");
					break;
				case 8: //Jarring Theft
					Core.SmartKillMonster(qIDs[i], "faerie", "Chainsaw Sneevil");
					break;
				case 9: //Tree Hugger
					Core.SmartKillMonster(qIDs[i], "faerie", "Cyclops Warlord");
					break;
				case 10: //The Second Piece
					Core.SmartKillMonster(qIDs[i], "faerie", "Aracara");
					break;
				case 11: //Ruined Ruins
					Core.SmartKillMonster(qIDs[i], "cornelis", "Gargoyle");
					break;
				case 12: //Energize!
					Core.GetMapItem(45, map: "cornelis");
					break;
				case 13: //Blueish Glow
					Core.SmartKillMonster(qIDs[i], "cornelis", "Gargoyle");
					break;
				case 14: //Quickdraw
					Core.GetMapItem(46, map: "cornelis");
					break;
				case 15: //Arm Yourself
					Core.SmartKillMonster(qIDs[i], "cornelis", "Stone Golem");
					break;
				case 16: //You've Been Framed
					Core.GetMapItem(47, map: "cornelis");
					break;
				case 17: //Some Assembly Required
					Core.GetMapItem(48, map: "mobius");
					break;
				case 18: //Teleporter Report
					Core.GetMapItem(49, map: "mobius");
					break;
				case 19: //Disguise!
					Core.SmartKillMonster(qIDs[i], "mobius", "Cyclops Raider");
					break;
				case 20: //To-go box
					Core.SmartKillMonster(qIDs[i], "faerie", "Chainsaw Sneevil");
					break;
				case 21: //Find the Key! (Part One)
					Core.SmartKillMonster(qIDs[i], "relativity", "Ciclops Raider");
					break;
				case 22: //Find the Key! (Part Two)
					Core.SmartKillMonster(qIDs[i], "relativity", "Fire Imp");
					break;
				case 23: //Find the Key! (Part Three)
					Core.SmartKillMonster(qIDs[i], "relativity", "Head Gargoyle");
					break;
				case 24: //The Lake Hydra
					bot.Player.Join("hydra");
					Core.Jump("Boss", "Left");
					for (int d = 0; d < 9; d++)
						bot.Player.Kill("Hydra Head");
					Core.JumpWait();
					Core.GetMapItem(50);
					Core.GetMapItem(51);
					Core.GetMapItem(52);
					break;
				case 25: //Escherion
					Core.KillEscherion();
					break;
			}
			Core.EnsureComplete(qIDs[i]);
			Core.Logger($"Finished {i}");
			Core.Rest();
		}
		Core.SetOptions(false);
	}
}
