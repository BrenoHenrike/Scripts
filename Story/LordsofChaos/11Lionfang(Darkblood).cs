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

    // Core.MapItemQuest(QuestID: QuestID, MapName: "MapName", MapItemID: MapItemID, Amount);
    // CoreBots.KillQuest(QuestID: QuestID, MapName: "MapName", MonsterNames: new[] { "Mobmname" });
    // CoreBots.KillQuest(QuestID: QuestID, MapName: "MapName", MonsterName: "Mobmname");

    public void StoryLine()
    {
        Core.BuyItem("battleon", 990, "Blood Summoner");
        if (Core.CheckInventory("Blood Summoner", toInv: false))
        {
            Bot.Sleep(700);
            Core.ToBank("Blood Summoner");
            Core.Logger("Chapter: \"Chaos Lord LionFang\" already complete. Skipping");
            return;
        }


        //Final Rest
        CoreBots.KillQuest(QuestID: 2612, MapName: "blackhorn", MonsterName: "Restless Undead");

        //Disturbing The Peace
        Core.MapItemQuest(QuestID: 2613, MapName: "blackhorn", MapItemID: 1615, Amount: 10);

        //Sampling Silk
        CoreBots.KillQuest(QuestID: 2614, MapName: "blackhorn", MonsterName: "Tomb Spider");

        //Fire Is The Thing
        CoreBots.KillQuest(QuestID: 2615, MapName: "blackhorn", MonsterNames: new[] { "Tomb Spider", "Restless Undead" });

        //The Wall Comes Down
        Core.MapItemQuest(QuestID: 2616, QuestID, MapName: "blackhorn", MapItemID: 1617, Amount: 1);

        //The Bonefeeder
        CoreBots.KillQuest(QuestID: 2617, MapName: "blackhorn", MonsterName: "Bonefeeder Spider");

        //What Lies Beyond?
        Core.MapItemQuest(QuestID: 2618, MapName: "blackhorn", MapItemID: 1618);

        //Toxic
        CoreBots.KillQuest(QuestID: 2619, MapName: "blackhorn", MonsterName: "Tomb Spider");

        //Very Toxic
        CoreBots.KillQuest(QuestID: 2620, MapName: "blackhorn", MonsterName: "estless Undead");

        //Really, VERY VERY TOXIC!
        Core.MapItemQuest(QuestID: 2621, MapName: "blackhorn", MapItemID: 1619);

        //Lion Hunting
        Core.MapItemQuest(QuestID: 2622, MapName: "onslaughttower", MapItemID: 1620);
        Core.MapItemQuest(QuestID: 2622, MapName: "onslaughttower", MapItemID: 1621);
        Core.MapItemQuest(QuestID: 2622, MapName: "onslaughttower", MapItemID: 1622);
        Core.MapItemQuest(QuestID: 2622, MapName: "onslaughttower", MapItemID: 1623);

        //Secret Of The Death Fog
        CoreBots.KillQuest(QuestID: 2623, MapName: "onslaughttower", MonsterName: "Golden Caster|Golden Warrior|Golden Cavalry");

        //The Key To Survival
        CoreBots.KillQuest(QuestID: 2624, MapName: "onslaughttower", MonsterName: "Golden Caster|Golden Warrior|Golden Cavalry");

        //The Tools
        Core.MapItemQuest(QuestID: 2625, MapName: "onslaughttower", MapItemID: 1624, 8);

        //The Talent
        Core.MapItemQuest(QuestID: 2626, MapName: "onslaughttower", MapItemID: 1625);

        //The Local Locale
        Core.MapItemQuest(QuestID: 2627, MapName: "onslaughttower", MapItemID: 1626, 4);

        //Who Holds The Key?
        CoreBots.KillQuest(QuestID: 2628, MapName: "onslaughttower", MonsterName: "Golden Caster|Golden Warrior|Golden Cavalry");

        //Leave No Rug Unturned
        Core.MapItemQuest(QuestID: 2629, MapName: "onslaughttower", MapItemID: 1627);

        //Tame The Lion
        CoreBots.KillQuest(QuestID: 2630, MapName: "onslaughttower", MonsterName: "Maximillian Lionfang", FollowupIDOverwrite: 2666);

        //Take Up The Cause
        CoreBots.KillQuest(QuestID: 2666, MapName: "falguard", MonsterName: "Chaonslaught Caster|Chaonslaught Warrior|Chaonslaught Cavalry");

        //Well Kept Secrets
        Core.MapItemQuest(QuestID: 2667, MapName: "falguard", MapItemID: 1628, 6);

        //Feeding On The Fallen
        CoreBots.KillQuest(QuestID: 2668, MapName: "falguard", MonsterNames: new[] { "Chaonslaught Warrior", "Chaonslaught Cavalry" });

        //Special Delivery
        Core.MapItemQuest(QuestID: 2669, MapName: "falguard", MapItemID: 1629);

        //Precious Scraps
        CoreBots.KillQuest(QuestID: 2670, MapName: "falguard", MonsterName: "Chaonslaught Warrior|Chaonslaught Cavalry");

        //Restocking
        Core.MapItemQuest(QuestID: 2671, MapName: "falguard", MapItemID: 1630);

        //An Innside Job
        Core.MapItemQuest(QuestID: 2672, MapName: "falguard", MapItemID: 1631);

        //Streets Run Red
        CoreBots.KillQuest(QuestID: 2673, MapName: "falguard", MonsterName: "Chaonslaught Caster");

        //Open the Temple
        Core.MapItemQuest(QuestID: 2674, MapName: "falguard", MapItemID: 1632);

        //The Open Temple
        CoreBots.KillQuest(QuestID: 2675, MapName: "falguard", MonsterName: "Primarch", FollowupIDOverwrite: 2720);

        //Remains
        CoreBots.KillQuest(QuestID: 2720, MapName: "deathpits", MonsterName: "Rotting Darkblood");

        //Thriving In Rot
        Core.MapItemQuest(QuestID: 2721, MapName: "deathpits", MapItemID: 1691, 5);

        //Rotting Ribs
        CoreBots.KillQuest(QuestID: 2722, MapName: "deathpits", MonsterName: "Rotting Darkblood");

        //A Perfect Skull
        CoreBots.KillQuest(QuestID: 2723, MapName: "deathpits", MonsterName: "Rotting Darkblood");

        //Deeper Into Death
        CoreBots.KillQuest(QuestID: 2724, MapName: "deathpits", MonsterName: "Ghastly Darkblood");

        //Precise Placement
        CoreBots.KillQuest(QuestID: 2725, MapName: "deathpits", MonsterName: "Ghastly Darkblood");

        //Painted Protection
        Core.MapItemQuest(QuestID: 2726, MapName: "MapName", MapItemID: 1692, 6);

        //They Sense You
        CoreBots.KillQuest(QuestID: 2727, MapName: "deathpits", MonsterName: "Rotting Darkblood");

        //They Hate You
        CoreBots.KillQuest(QuestID: 2728, MapName: "deathpits", MonsterName: "Ghastly Darkblood");

        //The Sundered
        CoreBots.KillQuest(QuestID: 2729, MapName: "deathpits", MonsterName: "Sundered Darkblood");

        //Rotstone
        Core.MapItemQuest(QuestID: 2730, MapName: "deathpits", MapItemID: 1693, 9);

        //Honor The Dead
        CoreBots.KillQuest(QuestID: 2731, MapName: "deathpits", MonsterNames: new[] { "Sundered Darkblood", "Ghastly Darkblood", "Rotting Darkblood" });

        //Ties to Life
        Core.MapItemQuest(QuestID: 2732, MapName: "deathpits", MapItemID: 1694, 12, FollowupIDOverwrite: 2740);

        //Destroy Wrathful Vestis and Secure The Tears
        CoreBots.KillQuest(QuestID: 2740, MapName: "deathpits", MonsterName: "Wrathful Vestis", FollowupIDOverwrite: 2792);
        Core.MapItemQuest(QuestID: 2740, MapName: "deathpits", MapItemID: 1695, 1, FollowupIDOverwrite: 2792);

        //Surveillance for Sir Valence
        Core.MapItemQuest(QuestID: 2792, MapName: "venomvaults", MapItemID: 1724);

        //Well Planned Getaway
        CoreBots.KillQuest(QuestID: 2793, MapName: "venomvaults", MonsterName: "Chaonslaught Warrior");

        //Secrets Of The Mad Prince
        Core.MapItemQuest(QuestID: 2794, MapName: "venomvaults", MapItemID: 2794, Amount, FollowupIDOverwrite: 2796);

        //Potion of Cleansing
        Core.MapItemQuest(QuestID: 2796, MapName: "venomvaults", MapItemID: 1726);

        //You've Been Noticed
        CoreBots.KillQuest(QuestID: 2797, MapName: "venomvaults", MonsterName: "Chaonslaught Caster");

        //Thorny Situations
        Core.MapItemQuest(QuestID: 2798, MapName: "venomvaults", MapItemID: 1727, 5);

        //Other Ingredients
        CoreBots.KillQuest(QuestID: 2799, MapName: "venomvaults", MonsterName: "Chaonslaught Caster", FollowupIDOverwrite: 2792);

        //Time For Supplies
        CoreBots.KillQuest(QuestID: 2800, MapName: "venomvaults", MonsterName: "Chaonslaught Warrior");

        //Cooking Without Fire
        CoreBots.KillQuest(QuestID: 2801, MapName: "venomvaults", MonsterName: "Chaonslaught Caster|Chaonslaught Warrior");

        //Introduction
        Core.MapItemQuest(QuestID: 2802, MapName: "venomvaults", MapItemID: 1728, 3);

        //Courtyard Key
        CoreBots.KillQuest(QuestID: 2803, MapName: "venomvaults", MonsterName: "Chaonslaught Caster|Chaonslaught Warrior");

        //Take Out The Chaos Manticore!
        CoreBots.KillQuest(QuestID: 2804, MapName: "venomvaults", MonsterName: "Manticore");

        //Shocking Footwear
        Core.MapItemQuest(QuestID: 2805, MapName: "stormtemple", MapItemID: 1729, Amount: 4);

        //New Shoes
        CoreBots.KillQuest(QuestID: 2806, MapName: "stormtemple", MonsterName: "Chaonslaught Warrior");

        //Mouth Of The Lion
        CoreBots.KillQuest(QuestID: 2807, MapName: "stormtemple", MonsterName: "Chaonslaught Caster|Chaonslaught Warrior|Chaonslaught Cavalry");

        //Storm the Storm Temple
        CoreBots.KillQuest(QuestID: 2808, MapName: "stormtemple", MonsterName: "Chaonslaught Caster|Chaonslaught Warrior|Chaonslaught Cavalry");

        //A High Minded Matter
        Core.MapItemQuest(QuestID: 2809, MapName: "MapName", MapItemID: 1730, Amount: 3);

        //Storm Bottles
        CoreBots.KillQuest(QuestID: 2810, MapName: "stormtemple", MonsterName: "Chaonslaught Caster");

        //Breaching Defenses
        Core.MapItemQuest(QuestID: 2811, MapName: "stormtemple", MapItemID: 1731, Amount: 1);

        //Chaos Lightning Rods
        CoreBots.KillQuest(QuestID: 2812, MapName: "MastormtemplepName", MonsterName: "Chaonslaught Cavalry");

        //Barrier Buster
        Core.MapItemQuest(QuestID: 2813, MapName: "stormtemple", MapItemID: 1732, Amount: 1);

        //Face Chaos Lord Lionfang!
        CoreBots.KillQuest(QuestID: 2814, MapName: "stormtemple", MonsterName: "Chaos Lord Lionfang");
    }
}
