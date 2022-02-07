//cs_include Scripts/CoreBots.cs

using System;
using RBot;

public class SagaTheSpan
{

    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;



    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Core.AcceptandCompleteTries = 5;
        StoryLine();

        Core.SetOptions(false);
    }

    // Core.MapItemQuest(QuestID: QuestID, MapName: "MapName", MapItemID: MapItemID, Amount: Amount, MapItemID: MapItemID);
    // CoreBots.KillQuest(QuestID: QuestID, MapName: "MapName", MonsterNames: new[] { "Mobmname" });
    // CoreBots.KillQuest(QuestID: QuestID, MapName: "MapName", MonsterName: "Mobmname");

    public void StoryLine()
    {
        Core.BuyItem("battleon", 989, "Ruler Of The Deep");
        if (Core.CheckInventory("Ruler Of The Deep", toInv: false))
        {
            Bot.Sleep(700);
            Core.ToBank("Ruler Of The Deep");
            Core.Logger("Chapter: \"Chaos Lord Iadoa\" already complete. Skipping");
            return;
        }

        //Time to Learn the Truth
        Core.MapItemQuest(QuestID: 2239, MapName: "thespan", MapItemID: 1358);
        Core.MapItemQuest(QuestID: 2239, MapName: "thespan", MapItemID: 1359);
        Core.MapItemQuest(QuestID: 2239, MapName: "thespan", MapItemID: 1360);
        Core.MapItemQuest(QuestID: 2239, MapName: "thespan", MapItemID: 1361);
        Core.MapItemQuest(QuestID: 2239, MapName: "thespan", MapItemID: 1362);
        Core.MapItemQuest(QuestID: 2239, MapName: "thespan", MapItemID: 1363);

        //Gain Access to Doors
        Core.KillQuest(QuestID: 2240, "timelibrary", new[] { "Sneak", "Tog", "Shadowscythe" });

        //Adventures and Quests
        Core.MapItemQuest(QuestID: 2241, "timelibrary", MapItemID: 1365, 3);
        Core.KillQuest(QuestID: 2241, "timelibrary", new[] { "Moglin Ghost|Undead Knight", "Moglin Ghost|Undead Knight" });

        //A Fable of Dragons
        Core.MapItemQuest(QuestID: 2242, "timelibrary", MapItemID: 1366, 2);
        Core.KillQuest(QuestID: 2242, "timelibrary", new[] { "Ninja|Tog", "Ninja|Tog" });

        //Mechas and Quests
        Core.MapItemQuest(QuestID: 2243, MapName: "timelibrary", MapItemID: 1367);
        Core.KillQuest(QuestID: 2243, "timelibrary", new[] { "Shadowscythe|Training Globe", "Shadowscythe|Training Globe" });

        //After the Chaos
        Core.MapItemQuest(QuestID: 2244, MapName: "timelibrary", MapItemID: 1368);
        Core.KillQuest(QuestID: 2244, "timelibrary", new[] { "Queen's Knight", "Queen's Knight", "Queen's Knight" }, FollowupIDOverwrite: 2253);

        //Trust is Not Ephemeral
        Core.KillQuest(QuestID: 2253, "timevoid", "Ephemerite");

        //In a Split Exasecond
        Core.MapItemQuest(QuestID: 2254, "timevoid", MapItemID: 1438, 8);
        Core.KillQuest(QuestID: 2254, "timevoid", new[] { "Time-Travel Fairy", "Time-Travel Fairy" });

        //Time to Prove Yourself
        Core.MapItemQuest(QuestID: 2255, "timevoid", MapItemID: 1439, 15);
        Core.KillQuest(QuestID: 2255, "timevoid", new[] { "Time-Travel Fairy", "Ephemerite" });

        //Fill the Empty Hours
        Core.KillQuest(QuestID: 2256, "timevoid", new[] { "Void Phoenix", "Time-Travel Fairy" });

        //Clock of the Long Now
        Core.MapItemQuest(QuestID: 2257, MapName: "timevoid", MapItemID: 1440);
        Core.MapItemQuest(QuestID: 2257, MapName: "timevoid", MapItemID: 1441);
        Core.MapItemQuest(QuestID: 2257, MapName: "timevoid", MapItemID: 1442);
        Core.MapItemQuest(QuestID: 2257, MapName: "timevoid", MapItemID: 1443);

        //Unending Avatar
        Core.KillQuest(QuestID: 2258, "timevoid", "Unending Avatar", FollowupIDOverwrite: 2376);

        //Construct Your Reality
        Core.MapItemQuest(QuestID: 2376, MapName: "aqlesson", MapItemID: 1467);

        //Reach the Temple
        Core.KillQuest(QuestID: 2377, "aqlesson", "Ninja ");

        //Not All Hope is Lost
        Core.MapItemQuest(QuestID: 2378, "aqlesson", MapItemID: 1468, 8);
        Core.MapItemQuest(QuestID: 2378, "aqlesson", MapItemID: 1469);

        //Bolster the Elements
        Core.MapItemQuest(QuestID: 2379, "aqlesson", MapItemID: 1470, 3);
        Core.MapItemQuest(QuestID: 2379, "aqlesson", MapItemID: 1471, 3);
        Core.KillQuest(QuestID: 2379, "aqlesson", new[] { "Water Elemental", "Eternite Ore" });

        //Maintain Elemental Strength
        Core.MapItemQuest(QuestID: 2380, "aqlesson", MapItemID: 1473, 3);
        Core.MapItemQuest(QuestID: 2380, "aqlesson", MapItemID: 1473, 3);
        Core.KillQuest(QuestID: 2380, "aqlesson", new[] { "Ice Elemental", "Fire Elemental" });

        //Rescue the Innocent
        Core.KillQuest(QuestID: 2381, "aqlesson", "Void Dragon");

        //Get Fired Up... or Shatter!
        Core.KillQuest(QuestID: 2382, "aqlesson", "Firezard");

        //Enemies on Ice
        Core.KillQuest(QuestID: 2383, "aqlesson", "Ice Elemental");

        //Tek-nical Forging Skill
        Core.MapItemQuest(QuestID: 2384, MapName: "thespan", MapItemID: 1474);

        //Akriloth Assault
        Core.KillQuest(QuestID: 2385, "aqlesson", "Akriloth");

        //Proto-Chaos Beast Battle!
        Core.KillQuest(QuestID: 2386, "aqlesson", "Carnax", FollowupIDOverwrite: 2470);

        //Elemental Orb Awareness
        Core.MapItemQuest(QuestID: 2470, "dflesson", MapItemID: 1549, 8);

        //Fight Chaos with Fire!
        Core.KillQuest(QuestID: 2471, "dflesson", new[] { "Fire Elemental", "Fire Elemental" });

        //Save Aria
        Core.KillQuest(QuestID: 2472, "dflesson", new[] { "Lava Slime", "Fire Elemental", "Fire Elemental" });

        //Find the Time to Travel
        Core.KillQuest(QuestID: 2473, "dflesson", new[] { "Tog", "Agitated Orb", "Tog|Agitated Orb" });

        //Dragon Egg... or Junk?
        Core.KillQuest(QuestID: 2474, "dflesson", "Vultragon");

        //Dracolich Fortress Detected
        Core.KillQuest(QuestID: 2475, "dflesson", "Chaos Sp-Eye");

        //Bone up on the Boss
        Core.KillQuest(QuestID: 2476, "dflesson", "Chaorrupted Evil Soldier");

        //Defend the Town!
        Core.KillQuest(QuestID: 2477, "dflesson", new[] { "Lava Golem", "Fire Elemental" });

        //ChickenCows, Bacon, and Battle!
        Core.KillQuest(QuestID: 2478, "dflesson", new[] { "Chaotic Chicken", "Chaotic Horcboar" });

        //The 2nd Proto-Chaos Beast
        Core.KillQuest(QuestID: 2479, "dflesson", "Fluffy the Dracolich", FollowupIDOverwrite: 2504);

        //Board the Ship to Your Future
        Core.MapItemQuest(QuestID: 2504, MapName: "mqlesson", MapItemID: 1580);

        //Heal the Chaos Lord
        Core.KillQuest(QuestID: 2505, "mqlesson", "Asteroid");

        //Shadowscythe Detection Beacons
        Core.MapItemQuest(QuestID: 2506, "mqlesson", MapItemID: 1581, 5);

        //Test Potential Traitors
        Core.KillQuest(QuestID: 2507, "mqlesson", "MystRaven Student");

        //Defeat Training Globes
        Core.KillQuest(QuestID: 2508, "mqlesson", "Training Globe");

        //Take Flight into the Future
        Core.KillQuest(QuestID: 2509, "mqlesson", "MystRaven Student");

        //Secrets of the Universe
        Core.KillQuest(QuestID: 2510, "mqlesson", "Chaos Shadowscythe");

        //Mysterious!
        Core.KillQuest(QuestID: 2511, "mqlesson", new[] { "Chaos Shadowscythe", "Chaos Shadowscythe" });

        //The 3rd Proto-Chaos Beast
        Core.KillQuest(QuestID: 2512, "mqlesson", "Dragonoid");

        //Chaos Waits and Watches
        Core.KillQuest(QuestID: 2513, "deepchaos", "Chaotic Merdrac", FollowupIDOverwrite: 2515);

        //The Lure of Chaosanity
        Core.KillQuest(QuestID: 2515, "deepchaos", "Chaos Angler");

        //Music of Nightmares
        Core.MapItemQuest(QuestID: 2516, "deepchaos", MapItemID: 1582, 3);

        //Chaos Beast Kathool
        Core.KillQuest(QuestID: 2517, "deepchaos", "Kathool");

        //Starry, Starry Night
        Core.KillQuest(QuestID: 2518, "timespace", "Astral Ephemerite");

        //Chaos Lord Iadoa
        Core.KillQuest(QuestID: 2519, "timespace", "Chaos Lord Iadoa", hasFollowup: false);
        
        Core.Relogin();
        Core.BuyItem("battleon", 989, "Ruler Of The Deep");
        Bot.Sleep(700);
        Core.ToBank("Ruler Of The Deep");
    }
}