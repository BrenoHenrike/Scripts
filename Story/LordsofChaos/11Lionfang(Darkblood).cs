//cs_include Scripts/CoreBots.cs

using System;
using RBot;
using System.Collections.Generic;

public class SagaDarkblood
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;



    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Core.AcceptandCompleteTries = 5;

        CompleteSaga();

        Core.SetOptions(false);
    }

    public void CompleteSaga()
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
        Core.KillQuest(2612, "blackhorn", "Restless Undead");

        //Disturbing The Peace
        Core.MapItemQuest(2613, "blackhorn", 1615, 10);

        //Sampling Silk
        Core.KillQuest(2614, "blackhorn", "Tomb Spider");

        //Fire Is The Thing
        Core.KillQuest(2615, "blackhorn", new[] { "Tomb Spider", "Restless Undead" });

        //The Wall Comes Down
        Core.MapItemQuest(2616, "blackhorn", 1617);

        //The Bonefeeder
        Core.KillQuest(2617, "blackhorn", "Bonefeeder Spider");

        //What Lies Beyond?
        Core.MapItemQuest(2618, "blackhorn", 1618);

        //Toxic
        Core.KillQuest(2619, "blackhorn", "Tomb Spider");

        //Very Toxic
        Core.KillQuest(2620, "blackhorn", "estless Undead");

        //Really, VERY VERY TOXIC!
        Core.MapItemQuest(2621, "blackhorn", 1619);

        //Lion Hunting
        Core.MapItemQuest(2622, "onslaughttower", 1620);
        Core.MapItemQuest(2622, "onslaughttower", 1621);
        Core.MapItemQuest(2622, "onslaughttower", 1622);
        Core.MapItemQuest(2622, "onslaughttower", 1623);

        //Secret Of The Death Fog
        Core.KillQuest(2623, "onslaughttower", "Golden Caster|Golden Warrior|Golden Cavalry");

        //The Key To Survival
        Core.KillQuest(2624, "onslaughttower", "Golden Caster|Golden Warrior|Golden Cavalry");

        //The Tools
        Core.MapItemQuest(2625, "onslaughttower", 1624, 8);

        //The Talent
        Core.MapItemQuest(2626, "onslaughttower", 1625);

        //The Local Locale
        Core.MapItemQuest(2627, "onslaughttower", 1626, 4);

        //Who Holds The Key?
        Core.KillQuest(2628, "onslaughttower", "Golden Caster|Golden Warrior|Golden Cavalry");

        //Leave No Rug Unturned
        Core.MapItemQuest(2629, "onslaughttower", 1627);

        //Tame The Lion
        Core.KillQuest(2630, "onslaughttower", "Maximillian Lionfang", FollowupIDOverwrite: 2666);

        //Take Up The Cause
        Core.KillQuest(2666, "falguard", "Chaonslaught Caster|Chaonslaught Warrior|Chaonslaught Cavalry");

        //Well Kept Secrets
        Core.MapItemQuest(2667, "falguard", 1628, 6);

        //Feeding On The Fallen
        Core.KillQuest(2668, "falguard", new[] { "Chaonslaught Warrior", "Chaonslaught Cavalry" });

        //Special Delivery
        Core.MapItemQuest(2669, "falguard", 1629);

        //Precious Scraps
        Core.KillQuest(2670, "falguard", "Chaonslaught Warrior|Chaonslaught Cavalry");

        //Restocking
        Core.MapItemQuest(2671, "falguard", 1630);

        //An Innside Job
        Core.MapItemQuest(2672, "falguard", 1631);

        //Streets Run Red
        Core.KillQuest(2673, "falguard", "Chaonslaught Caster");

        //Open the Temple
        Core.MapItemQuest(2674, "falguard", 1632);

        //The Open Temple
        Core.KillQuest(2675, "falguard", "Primarch", FollowupIDOverwrite: 2720);

        //Remains
        Core.KillQuest(2720, "deathpits", "Rotting Darkblood");

        //Thriving In Rot
        Core.MapItemQuest(2721, "deathpits", 1691, 5);

        //Rotting Ribs
        Core.KillQuest(2722, "deathpits", "Rotting Darkblood");

        //A Perfect Skull
        Core.KillQuest(2723, "deathpits", "Rotting Darkblood");

        //Deeper Into Death
        Core.KillQuest(2724, "deathpits", "Ghastly Darkblood");

        //Precise Placement
        Core.KillQuest(2725, "deathpits", "Ghastly Darkblood");

        //Painted Protection
        Core.MapItemQuest(2726, "deathpits", 1692, 6);

        //They Sense You
        Core.KillQuest(2727, "deathpits", "Rotting Darkblood");

        //They Hate You
        Core.KillQuest(2728, "deathpits", "Ghastly Darkblood");

        //The Sundered
        Core.KillQuest(2729, "deathpits", "Sundered Darkblood");

        //Rotstone
        Core.MapItemQuest(2730, "deathpits", 1693, 9);

        //Honor The Dead
        Core.KillQuest(2731, "deathpits", new[] { "Sundered Darkblood", "Ghastly Darkblood", "Rotting Darkblood" });

        //Ties to Life
        Core.MapItemQuest(2732, "deathpits", 1694, 12, FollowupIDOverwrite: 2740);

        //Destroy Wrathful Vestis and Secure The Tears
        Core.KillQuest(2740, "deathpits", "Wrathful Vestis", FollowupIDOverwrite: 2792);
        Core.MapItemQuest(2740, "deathpits", 1695, 1, FollowupIDOverwrite: 2792);

        //Surveillance for Sir Valence
        Core.MapItemQuest(2792, "venomvaults", 1724);

        //Well Planned Getaway
        Core.KillQuest(2793, "venomvaults", "Chaonslaught Warrior");

        //Secrets Of The Mad Prince
        Core.MapItemQuest(2794, "venomvaults", 2794, FollowupIDOverwrite: 2796);

        //Potion of Cleansing
        Core.MapItemQuest(2796, "venomvaults", 1726);

        //You've Been Noticed
        Core.KillQuest(2797, "venomvaults", "Chaonslaught Caster");

        //Thorny Situations
        Core.MapItemQuest(2798, "venomvaults", 1727, 5);

        //Other Ingredients
        Core.KillQuest(2799, "venomvaults", "Chaonslaught Caster", FollowupIDOverwrite: 2792);

        //Time For Supplies
        Core.KillQuest(2800, "venomvaults", "Chaonslaught Warrior");

        //Cooking Without Fire
        Core.KillQuest(2801, "venomvaults", "Chaonslaught Caster|Chaonslaught Warrior");

        //Introduction
        Core.MapItemQuest(2802, "venomvaults", 1728, 3);

        //Courtyard Key
        Core.KillQuest(2803, "venomvaults", "Chaonslaught Caster|Chaonslaught Warrior");

        //Take Out The Chaos Manticore!
        Core.KillQuest(2804, "venomvaults", "Manticore");

        //Shocking Footwear
        Core.MapItemQuest(2805, "stormtemple", 1729, 4);

        //New Shoes
        Core.KillQuest(2806, "stormtemple", "Chaonslaught Warrior");

        //Mouth Of The Lion
        Core.KillQuest(2807, "stormtemple", "Chaonslaught Caster|Chaonslaught Warrior|Chaonslaught Cavalry");

        //Storm the Storm Temple
        Core.KillQuest(2808, "stormtemple", "Chaonslaught Caster|Chaonslaught Warrior|Chaonslaught Cavalry");

        //A High Minded Matter
        Core.MapItemQuest(2809, "stormtemple", 1730, 3);

        //Storm Bottles
        Core.KillQuest(2810, "stormtemple", "Chaonslaught Caster");

        //Breaching Defenses
        Core.MapItemQuest(2811, "stormtemple", 1731);

        //Chaos Lightning Rods
        Core.KillQuest(2812, "MastormtemplepName", "Chaonslaught Cavalry");

        //Barrier Buster
        Core.MapItemQuest(2813, "stormtemple", 1732);

        //Face Chaos Lord Lionfang!
        Core.KillQuest(2814, "stormtemple", "Chaos Lord Lionfang", hasFollowup: false);

        Core.Relogin();
        Core.BuyItem("battleon", 990, "Blood Summoner");
        Bot.Sleep(700);
        Core.ToBank("Blood Summoner");
    }
}
