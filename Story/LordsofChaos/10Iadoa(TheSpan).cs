//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaTheSpan
{
    public CoreBots Core => CoreBots.Instance;

    public int questStart = 0;

    public string OptionsStorage = "SagaTheSpan";
    public bool DontPreconfigure = true;

    public List<IOption> Options = new List<IOption>()
    {
        new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
    };

    public static readonly int[] qIDs = 
    {
        /* -- /Join TimeLibrary --                         */
            2239, /*  0 - Time to Learn the Truth          */
            2240, /*  1 - Gain Access to Doors             */
            2241, /*  2 - Adventures and Quests            */
            2242, /*  3 - A Fable of Dragons               */
            2243, /*  4 - Mechas and Quests                */
            2244, /*  5 - After the Chaos                  */
        /* -- /Join TimeVoid --                            */
            2253, /*  6 - Trust is Not Ephemeral           */
            2254, /*  7 - In a Split Exasecond             */
            2255, /*  8 - Time to Prove Yourself           */
            2256, /*  9 - Fill the Empty Hours             */
            2257, /* 10 - Clock of the Long Now            */
            2258, /* 11 - Unending Avatar                  */
        /* -- /Join AQLesson --	                           */
            2376, /* 12 - Construct Your Reality           */
            2377, /* 13 - Reach the Temple                 */
            2378, /* 14 - Not All Hope is Lost             */
            2379, /* 15 - Bolster the Elements             */
            2380, /* 16 - Maintain Elemental Strength      */
            2381, /* 17 - Rescue the Innocent              */
            2382, /* 18 - Get Fired Up... or Shatter!      */
            2383, /* 19 - Enemies on Ice                   */
            2384, /* 20 - Tek-nical Forging Skill          */
            2385, /* 21 - Akriloth Assault                 */
            2386, /* 22 - Proto-Chaos Beast Battle!        */
        /* -- /Join DFLesson --	                           */
            2470, /* 23 - Elemental Orb Awareness          */
            2471, /* 24 - Fight Chaos with Fire!           */
            2472, /* 25 - Save Aria                        */
            2473, /* 26 - Find the Time to Travel          */
            2474, /* 27 - Dragon Egg... or Junk?           */
            2475, /* 28 - Dracolich Fortress Detected      */
            2476, /* 29 - Bone up on the Boss              */
            2477, /* 30 - Defend the Town!                 */
            2478, /* 31 - ChickenCows, Bacon, and Battle!  */
            2479, /* 32 - The 2nd Proto-Chaos Beast        */
            2504, /* 33 - Board the Ship to Your Future    */
        /* -- /Join MQLesson --                            */
            2505, /* 34 - Heal the Chaos Lord              */
            2506, /* 35 - Shadowscythe Detection Beacons   */
            2507, /* 36 - Test Potential Traitors          */
            2508, /* 37 - Defeat Training Globes           */
            2509, /* 38 - Take Flight into the Future      */
            2510, /* 39 - Secrets of the Universe          */
            2511, /* 40 - Mysterious!                      */
            2512, /* 41 - The 3rd Proto-Chaos Beast        */
            2513, /* 42 - Chaos Waits and Watches          */
        /* -- /Join DeepChaos --                           */
            2515, /* 43 - The Lure of Chaosanity           */
            2516, /* 44 - Music of Nightmares              */
            2517, /* 45 - Chaos Beast Kathool              */
            2518, /* 46 - Starry, Starry Night             */
            2519  /* 47 - Chaos Lord Iadoa                 */
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
                case 0: //Time to Learn the Truth
                    Core.GetMapItem(1358, map: "thespan");
                    Core.GetMapItem(1359);
                    Core.GetMapItem(1360);
                    Core.GetMapItem(1361);
                    Core.GetMapItem(1362);
                    Core.GetMapItem(1363);
                    break;
                case 1: //Gain Access to Doors
                    Core.SmartKillMonster(qIDs[i], "timelibrary", new[] { "Sneak", "Tog", "Shadowscythe" });
                    break;
                case 2: //Adventures and Quests
                    Core.GetMapItem(1365, 3, "timelibrary");
                    Core.SmartKillMonster(qIDs[i], "timelibrary", new[] { "Moglin Ghost|Undead Knight", "Moglin Ghost|Undead Knight" });
                    break;
                case 3: //A Fable of Dragons
                    Core.GetMapItem(1366, 2, "timelibrary");
                    Core.SmartKillMonster(qIDs[i], "timelibrary", new[] { "Ninja|Tog", "Ninja|Tog" });
                    break;
                case 4: //Mechas and Quests
                    Core.GetMapItem(1367, map: "timelibrary");
                    Core.SmartKillMonster(qIDs[i], "timelibrary", new[] { "Shadowscythe|Training Globe", "Shadowscythe|Training Globe" });
                    break;
                case 5: //After the Chaos
                    Core.GetMapItem(1368, map: "timelibrary");
                    Core.SmartKillMonster(qIDs[i], "timelibrary", new[] { "Queen's Knight", "Queen's Knight", "Queen's Knight" });
                    break;
                case 6: //Trust is Not Ephemeral
                    Core.SmartKillMonster(qIDs[i], "timevoid", "Ephemerite");
                    break;
                case 7: //In a Split Exasecond
                    Core.GetMapItem(1438, 8, "timevoid");
                    Core.SmartKillMonster(qIDs[i], "timevoid", new[] { "Time-Travel Fairy", "Time-Travel Fairy" });
                    break;
                case 8: //Time to Prove Yourself
                    Core.GetMapItem(1439, 15, "timevoid");
                    Core.SmartKillMonster(qIDs[i], "timevoid", new[] { "Time-Travel Fairy", "Ephemerite" });
                    break;
                case 9: //Fill the Empty Hours
                    Core.SmartKillMonster(qIDs[i], "timevoid", new[] { "Void Phoenix", "Time-Travel Fairy" });
                    break;
                case 10: //Clock of the Long Now
                    Core.GetMapItem(1440, map: "timevoid");
                    Core.GetMapItem(1441);
                    Core.GetMapItem(1442);
                    Core.GetMapItem(1443);
                    break;
                case 11: //Unending Avatar
                    Core.SmartKillMonster(qIDs[i], "timevoid", "Unending Avatar");
                    break;
                case 12: //Construct Your Reality
                    Core.GetMapItem(1467, map: "aqlesson");
                    break;
                case 13: //Reach the Temple
                    Core.SmartKillMonster(qIDs[i], "aqlesson", "Ninja ");
                    break;
                case 14: //Not All Hope is Lost
                    Core.GetMapItem(1468, 8, "aqlesson");
                    Core.GetMapItem(1469);
                    break;
                case 15: //Bolster the Elements
                    Core.GetMapItem(1470, 3, "aqlesson");
                    Core.GetMapItem(1471, 3);
                    Core.SmartKillMonster(qIDs[i], "aqlesson", new[] {"Water Elemental", "Eternite Ore" });
                    break;
                case 16: //Maintain Elemental Strength
                    Core.GetMapItem(1472, 3, "aqlesson");
                    Core.GetMapItem(1473, 3);
                    Core.SmartKillMonster(qIDs[i], "aqlesson", new[] {"Ice Elemental", "Fire Elemental" });
                    break;
                case 17: //Rescue the Innocent
                    Core.SmartKillMonster(qIDs[i], "aqlesson", "Void Dragon");
                    break;
                case 18: //Get Fired Up... or Shatter!
                    Core.SmartKillMonster(qIDs[i], "aqlesson", "Firezard");
                    break;
                case 19: //Enemies on Ice
                    Core.SmartKillMonster(qIDs[i], "aqlesson", "Ice Elemental");
                    break;
                case 20: //Tek-nical Forging Skill
                    Core.GetMapItem(1474, map: "thespan");
                    break;
                case 21: //Akriloth Assault
                    Core.SmartKillMonster(qIDs[i], "aqlesson", "Akriloth");
                    break;
                case 22: //Proto-Chaos Beast Battle!
                    Core.SmartKillMonster(qIDs[i], "aqlesson", "Carnax");
                    break;
                case 23: //Elemental Orb Awareness
                    Core.GetMapItem(1549, 8, "dflesson");
                    break;
                case 24: //Fight Chaos with Fire!
                    Core.SmartKillMonster(qIDs[i], "dflesson", new[] { "Fire Elemental", "Fire Elemental" });
                    break;
                case 25: //Save Aria
                    Core.SmartKillMonster(qIDs[i], "dflesson", new[] { "Lava Slime", "Fire Elemental", "Fire Elemental" });
                    break;
                case 26: //Find the Time to Travel
                    Core.SmartKillMonster(qIDs[i], "dflesson", new[] { "Tog", "Agitated Orb", "Tog|Agitated Orb" });
                    break;
                case 27: //Dragon Egg... or Junk?
                    Core.SmartKillMonster(qIDs[i], "dflesson", "Vultragon");
                    break;
                case 28: //Dracolich Fortress Detected
                    Core.SmartKillMonster(qIDs[i], "dflesson", "Chaos Sp-Eye");
                    break;
                case 29: //Bone up on the Boss
                    Core.SmartKillMonster(qIDs[i], "dflesson", "Chaorrupted Evil Soldier");
                    break;
                case 30: //Defend the Town!
                    Core.SmartKillMonster(qIDs[i], "dflesson", new[] {"Lava Golem", "Fire Elemental" });
                    break;
                case 31: //ChickenCows, Bacon, and Battle!
                    Core.SmartKillMonster(qIDs[i], "dflesson", new[] {"Chaotic Chicken", "Chaotic Horcboar"});
                    break;
                case 32: //The 2nd Proto-Chaos Beast
                    Core.SmartKillMonster(qIDs[i], "dflesson", "Fluffy the Dracolich");
                    break;
                case 33: //Board the Ship to Your Future
                    Core.GetMapItem(1580, map: "mqlesson");
                    break;
                case 34: //Heal the Chaos Lord
                    Core.SmartKillMonster(qIDs[i], "mqlesson", "Asteroid");
                    break;
                case 35: //Shadowscythe Detection Beacons
                    Core.GetMapItem(1581, 5, "mqlesson");
                    break;
                case 36: //Test Potential Traitors
                    Core.SmartKillMonster(qIDs[i], "mqlesson", "MystRaven Student");
                    break;
                case 37: //Defeat Training Globes
                    Core.SmartKillMonster(qIDs[i], "mqlesson", "Training Globe");
                    break;
                case 38: //Take Flight into the Future
                    Core.SmartKillMonster(qIDs[i], "mqlesson", "MystRaven Student");
                    break;
                case 39: //Secrets of the Universe
                    Core.SmartKillMonster(qIDs[i], "mqlesson", "Chaos Shadowscythe");
                    break;
                case 40: //Mysterious!
                    Core.SmartKillMonster(qIDs[i], "mqlesson", new[] { "Chaos Shadowscythe", "Chaos Shadowscythe" });
                    break;
                case 41: //The 3rd Proto-Chaos Beast
                    Core.SmartKillMonster(qIDs[i], "mqlesson", "Dragonoid");
                    break;
                case 42: //Chaos Waits and Watches
                    Core.SmartKillMonster(qIDs[i], "deepchaos", "Chaotic Merdrac");
                    break;
                case 43: //The Lure of Chaosanity
                    Core.SmartKillMonster(qIDs[i], "deepchaos", "Chaos Angler");
                    break;
                case 44: //Music of Nightmares
                    Core.GetMapItem(1582, 3, "deepchaos");
                    break;
                case 45: //Chaos Beast Kathool
                    Core.SmartKillMonster(qIDs[i], "deepchaos", "Kathool");
                    break;
                case 46: //Starry, Starry Night
                    Core.SmartKillMonster(qIDs[i], "timespace", "Astral Ephemerite");
                    break;
                case 47: //Chaos Lord Iadoa
                    Core.SmartKillMonster(qIDs[i], "timespace", "Chaos Lord Iadoa");
                    break;
            }
            Core.EnsureComplete(qIDs[i]);
            Core.Logger($"Finished {i}");
            Core.Rest();
        }

        Core.SetOptions(false);
    }
}
