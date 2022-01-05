//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaArcangrove
{
	public CoreBots Core => CoreBots.Instance;

	public int questStart = 0;

	public string OptionsStorage = "SagaArcangrove";
	public bool DontPreconfigure = true;

	public List<IOption> Options = new List<IOption>()
	{
		new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
	};

	public static readonly int[] qIDs = 
	{
		/* -- /Join Arcangrove --						   */
			805, /*  0 - Observing the Observatory         */
			806, /*  1 - Ewa the Treekeeper                */
		/* -- /Join Cloister --                            */
			807, /*  2 - Bear Necessities of LifeRoot      */
			808, /*  3 - Acorny Quest                      */
			809, /*  4 - Ravenloss                         */
			810, /*  5 - It's A Bough-t Time               */
			811, /*  6 - Wendigo Whereabouts               */
		/* -- /Join Arcangrove --                          */
			813, /*  7 - Find Paddy Lump                   */
		/* -- /Join Mudluk --                              */
			814, /*  8 - Toothy Smiles                     */
			815, /*  9 - Slimy Cyrus                       */
			816, /* 10 - Lord Of The Fleas                 */
			817, /* 11 - Not The Best Idea                 */
			818, /* 12 - Gates and Guardians               */
		/* -- /Join Arcangrove --                          */
			825, /* 13 - Water You Waiting For--Find Nisse */
		/* -- /Join Natatorium --                          */
			826, /* 14 - Dive Right In                     */
			827, /* 15 - Seafood Diet                      */
			828, /* 16 - Mercenaries                       */
			829, /* 17 - Synchronized Slaying              */
			830, /* 18 - The Deep End                      */
		/* -- /Join Arcangrove --                          */
			831, /* 19 - Find Umbra, the Master Shaman     */
		/* -- /Join Gilead --                              */
			832, /* 20 - The Root of Elementals            */
			833, /* 21 - Eupotamic Elementals              */
			834, /* 22 - Breaking Wind Elementals          */
			835, /* 23 - Fight Fire With Fire Salamanders  */
			836, /* 24 - Guardian of the Gilead Wrap       */
		/* -- /Join Arcangrove --                          */
			838, /* 25 - Find Felsic the Magma Golem       */
		/* -- /Join Mafic --                               */
			839, /* 26 - Liquid Hot Magma Maggots          */
			840, /* 27 - Scorched Serpents                 */
			841, /* 28 - Playing With Living Fire          */
			842, /* 29 - Kindling Relationship             */
		/* -- /Join Elemental --                           */
			843, /* 30 - Obey Your Thirst for Adventure    */
			844, /* 31 - Captain Falcons                   */
			845, /* 32 - Big, bad, and Baddest Bosses      */
			846, /* 33 - The Great Mana Golem]             */
		/* -- /Join Ledgermayne --                         */
			847  /* 34 - Chaos Lord Ledgermayne            */
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
				case 0: //Observing the Observatory
					Core.GetMapItem(139, map: "arcangrove");
					break;
				case 1: //Ewa the Treekeeper
					Core.GetMapItem(142, map: "cloister");
					break;
				case 2: //Bear Necessities of LifeRoot
					Core.GetMapItem(140, map: "cloister");
					break;
				case 3: //Acorny Quest
					Core.SmartKillMonster(qIDs[i], "cloister", "Acornent");
					break;
				case 4: //Ravenloss
					Core.SmartKillMonster(qIDs[i], "cloister", "Karasu");
					break;
				case 5: //It's A Bough-t Time
					bot.Player.Join("arcangrove");
					Core.Jump("Potion", "Right");
					bot.Shops.Load(211);
					bot.Shops.BuyItem(211, "Mana Potion");
					Core.GetMapItem(141, 3, "cloister");
					break;
				case 6: //Wendigo Whereabouts
					Core.SmartKillMonster(qIDs[i], "cloister", "Wendigo");
					break;
				case 7: //Find Paddy Lump
					Core.GetMapItem(143, map: "mudluk");
					break;
				case 8: //Toothy Smiles
					Core.SmartKillMonster(qIDs[i], "mudluk", "Swamp Lurker");
					break;
				case 9: //Slimy Cyrus
					Core.SmartKillMonster(qIDs[i], "mudluk", "Swamp Lurker");
					break;
				case 10: //Lord Of The Fleas
					Core.SmartKillMonster(qIDs[i], "arcangrove", "Gorillaphant");
					break;
				case 11: //Not The Best Idea
					Core.SmartKillMonster(qIDs[i], "mudluk", "Swamp Frogdrake");
					break;
				case 12: //Gates and Guardians
					Core.SmartKillMonster(qIDs[i], "mudluk", "Tiger Leech");
					break;
				case 13: //Water You Waiting For--Find Nisse
					Core.GetMapItem(144, map: "natatorium");
					break;
				case 14: //Dive Right In
					Core.GetMapItem(145, 12, "natatorium");
					break;
				case 15: //Seafood Diet
					Core.SmartKillMonster(qIDs[i], "natatorium", "Anglerfish");
					break;
				case 16: //Mercenaries
					Core.SmartKillMonster(qIDs[i], "natatorium", "Merdraconian");
					break;
				case 17: //Synchronized Slaying
					Core.SmartKillMonster(qIDs[i], "arcangrove", "Seed Spitter|Gorillaphant");
                    Core.SmartKillMonster(qIDs[i], "cloister", new[] { "Acornent", "Karasu", "Wendigo" });
					Core.SmartKillMonster(qIDs[i], "mudluk", "Swamp Frogdrake|Swamp Lurker");
					break;
				case 18: //The Deep End
					Core.SmartKillMonster(qIDs[i], "natatorium", "Nessie");
					break;
				case 19: //Find Umbra, the Master Shaman
					Core.GetMapItem(146, map: "gilead");
					break;
				case 20: //The Root of Elementals
					Core.SmartKillMonster(qIDs[i], "gilead", "Earth Elemental");
					Core.SmartKillMonster(qIDs[i], "arcangrove", "Seed Spitter");
					break;
				case 21: //Eupotamic Elementals
					Core.SmartKillMonster(qIDs[i], "gilead", "Water Elemental");
					Core.SmartKillMonster(qIDs[i], "natatorium", "Merdraconian");
					break;
				case 22: //Breaking Wind Elementals
					Core.SmartKillMonster(qIDs[i], "gilead", "Wind Elemental");
					Core.SmartKillMonster(qIDs[i], "cloister", "Karasu");
					break;
				case 23: //Fight Fire With Fire Salamanders
					Core.SmartKillMonster(qIDs[i], "gilead", "Fire Elemental");
					Core.SmartKillMonster(qIDs[i], "mudluk", "Swamp Frogdrake");
					break;
				case 24: //Guardian of the Gilead Wrap
					Core.SmartKillMonster(qIDs[i], "gilead", "Mana Elemental");
					break;
				case 25: //Find Felsic the Magma Golem
					Core.GetMapItem(147, map: "mafic");
					break;
				case 26: //Liquid Hot Magma Maggots
					Core.SmartKillMonster(qIDs[i], "mafic", "Volcanic Maggot");
					break;
				case 27: //Scorched Serpents
					Core.SmartKillMonster(qIDs[i], "mafic", "Scoria Serpent");
					break;
				case 28: //Playing With Living Fire
					Core.SmartKillMonster(qIDs[i], "mafic", "Living Fire");
					break;
				case 29: //Kindling Relationship
					Core.SmartKillMonster(qIDs[i], "mafic", "Mafic Dragon");
					break;
				case 30: //Obey Your Thirst for Adventure
					Core.SmartKillMonster(qIDs[i], "elemental", "Mana Imp");
					break;
				case 31: //Captain Falcons
					Core.SmartKillMonster(qIDs[i], "elemental", "Mana Falcon");
					break;
				case 32: //Big, bad, and Baddest Bosses
                    Core.SmartKillMonster(qIDs[i], "cloister", "Wendigo");
					Core.SmartKillMonster(qIDs[i], "mudluk", "Tiger Leech");
					Core.SmartKillMonster(qIDs[i], "natatorium", "Nessie");
					Core.SmartKillMonster(qIDs[i], "gilead", "Mana Elemental");
					Core.SmartKillMonster(qIDs[i], "mafic", "Mafic Dragon");
					break;
				case 33: //The Great Mana Golem
					bot.Player.Join("elemental");
					Core.Jump("r5", "Down");
					bot.Sleep(2000);
					Core.Jump("Enter", "Spawn");
					Core.SmartKillMonster(qIDs[i], "elemental", "Mana Golem");
					break;
				case 34: //Chaos Lord Ledgermayne
					Core.SmartKillMonster(qIDs[i], "ledgermayne", "Ledgermayne");
					break;
			}
			Core.EnsureComplete(qIDs[i]);
			Core.Logger($"Finished {i}");
			Core.Rest();
		}

		Core.SetOptions(false);
	}
}
