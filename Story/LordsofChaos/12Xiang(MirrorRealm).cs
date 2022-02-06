//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaMirrorRealm
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;




    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }


    public void StoryLine()
    {
        Core.BuyItem("battleon", 992, "PaladinSlayer Daimyo");
        if (Core.CheckInventory("PaladinSlayer Daimyo", toInv: false))
        {
            Bot.Sleep(700);
            Core.ToBank("PaladinSlayer Daimyo");
            Core.Logger("Chapter: \"Chaos Lord Xiang\" already complete. Skipping");
            return;
        }

        //Bright Idea
        Core.MapItemQuest(2909, MapItemID: 1779, MapName: "battleoff");

        //Spare Parts
        Core.MapItemQuest(2910, MapItemID: 1780, MapName: "battleoff" );

        //Power It Uo
        Core.KillQuest(2911, "battleoff", "Evil Moglin");

        //Filthy Creatures
        Core.KillQuest(2912, "battleoff", "Evil Moglin");

        //Wave After Wave
        Core.KillQuest(2913, "brightfall", "Undead Minion");

        //Take out Their Firepower
        Core.KillQuest(2914, "brightfall", "Undead Mage");

        //Help Where It is Needed
        Core.MapItemQuest(2915, MapItemID: 1781, MapName: "brightfall");

        //Bring A Ward To A Swordfight
        Core.MapItemQuest(2916, MapItemID: 1782, MapName: "brightfall");

        //Cut Off The Head
        Core.KillQuest(2917, "brightfall", "Painadin Overlord");

        //Rearm The Legion of Light
        Core.Join("overworld");
        Core.ChainComplete(2918);
        Core.KillQuest(2919, "overworld", "Undead Minion");

        //Free Their Souls
        Core.KillQuest(2920, "overworld", "Undead Minion");

        //One Ring
        Core.KillQuest(2921, "overworld", "Undead Minion");

        //Severing Ties
        Core.KillQuest(2922, "overworld", "Undead Mage");

        //Legion's Lifesblood
        Core.MapItemQuest(2923, MapItemID: 1800, MapName: "overworld");

        //Legion's Purpose
        Core.KillQuest(2924, "overworld", "Undead Bruiser");

        //What's His Endgame
        Core.KillQuest(2925, "overworld", "Undead Bruiser");

        //A Stopping Block
        Core.KillQuest(2926, "overworld", "Undead Minion|Undead Mage|Undead Bruiser");

        //Boost Morale
        Core.MapItemQuest(2927, MapItemID: 1801, MapName: "overworld");

        //Alteon's Folly
        Core.KillQuest(2928, "overworld", "Undead Minion|Undead Mage|Undead Bruiser");

        //DoomFire
        Core.KillQuest(2929, "overworld", "Undead Minion|Undead Mage|Undead Bruiser");

        //Spoiled Souls
        Core.KillQuest(2930, "overworld", "Undead Minion|Undead Mage|Undead Bruiser");

        //Purity of Bone
        Core.MapItemQuest(2931, MapItemID: 1802, MapName: "overworld");

        //Undead Artix Returns!
        Core.KillQuest(2932, "overworld", "Undead Artix", FollowupIDOverwrite: 3166);

        //I Can't Touch This
        Core.KillQuest(3166, "reddeath", "Fire Leech|Grim Widow|Reddeath Moglin|Swamp Wraith");

        //Nope, Still a Ghost
        Core.KillQuest(3167, "reddeath", "Reddeath Moglin");
        Core.MapItemQuest(3167, MapItemID: 2178, MapName: "reddeath");
        Core.MapItemQuest(3167, MapItemID: 2179, MapName: "reddeath");
        //First We Need a Beacon...
        Core.MapItemQuest(3168, MapItemID: 2180, MapName: "reddeath");

        //Light It Up
        Core.KillQuest(3169, "reddeath", "Fire Leech");

        //...Next We need a Trap
        Core.KillQuest(3170, "reddeath", "Grim Widow");

        //For Spirits, Not People
        Core.KillQuest(3171, "reddeath", "Swamp Wraith");

        //Still To Fragile
        Core.KillQuest(3172, "reddeath", "Swamp Wraith");

        //Craft a Better Defense
        Core.MapItemQuest(3183, MapItemID: 2203, MapName: "battleontown", FollowupIDOverwrite: 3183);

        //Reflect the Damage
        Core.KillQuest(3184, "earthstorm", "Shard Spinner");

        //Pure Chaos
        Core.KillQuest(3185, "bloodtuskwar", "Chaotic Horcboar");

        //Enemies of a Feather Flock Together
        Core.KillQuest(3186, "bloodtuskwar", "Chaos Tigriff");

        //Ward Off the Beast
        Core.Join("mirrorportal");
        Core.ChainComplete(3187);

        //Horror Takes Flight
        Core.KillQuest(3188, "mirrorportal", "Chaos Harpy");

        //Good, Evil and Chaos Battle!
        Core.KillQuest(3189, "mirrorportal", "Chaos Lord Xiang");
    }
}
