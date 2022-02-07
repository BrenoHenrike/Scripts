//cs_include Scripts/CoreBots.cs

using System;
using RBot;
using System.Collections.Generic;

public class SagaSwordhaven
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
        Core.BuyItem("battleon", 991, "Cyber King");
        if (Core.CheckInventory("Cyber King", toInv: false))
        {
            Bot.Sleep(700);
            Core.ToBank("Cyber King");
            Core.Logger("Chapter: \"Chaos Lord Alteon\" already complete. Skipping");
            return;
        }

        //Bandit Bounty
        Core.KillQuest(3077, "archives", "Chaos Bandit");

        //Thwarting the Spies
        Core.KillQuest(3078, "archives", "Camouflaged Sp-Eye");

        //Fight Chaos With Clerics
        Core.KillQuest(3079, "archives", new[] { "Chaos Bandit", "Camouflaged Sp-Eye" });

        //Locate the Source
        Core.KillQuest(3080, "archives", "Chaos Bandit|Camouflaged Sp-Eye");
        Core.MapItemQuest(3080, MapName: "archives", 1937);

        //Plagued Rats
        Core.KillQuest(3081, "archives", "Chaos Rat");

        //Nope, Nope, Nope!
        Core.KillQuest(3082, "archives", "Chaos Spider");

        //Still More Research To Be Done!
        Core.KillQuest(3083, "archives", new[] { "Chaos Spider", "Chaos Rat" });

        //That's One Big Sludgebeast.
        Core.KillQuest(3084, "archives", "Sludgelord", FollowupIDOverwrite: 3094);

        //Back to Jail With You!
        Core.KillQuest(3094, "armory", "Chaorrupted Prisoner");

        //We May Need A Militia
        Core.KillQuest(3095, "armory", "Chaorrupted Prisoner");
        Core.MapItemQuest(3095, MapName: "armory", 1956, 4);

        //An Ounce Of Prevention
        Core.KillQuest(3096, "armory", "Chaos Drifter");

        //Axe Them To Leave! / Freeze 'Em Out! / Burn 'Em Up!
        Core.KillQuest(3096, "armory", "Chaorrupted Prisoner");

        //Freeze 'Em Out!
        Core.KillQuest(3090, "armory", "Chaos Mage");

        //Burn 'Em Up!
        Core.KillQuest(3091, "armory", "Chaos Mage");

        //Under Siege
        Core.MapItemQuest(3092, MapName: "armory", 1957);

        //No, NOW We're Under Siege
        Core.KillQuest(3093, "armory", "Chaos General");

        //Chaos Not Invited
        Core.KillQuest(3120, "ceremony", "Chaos Invader");

        //Better Letter Go!
        // if(Core.QuestProgression(3121))
        // {
        Core.MapItemQuest(3121, MapName: "yulgar", 2108);
        Core.MapItemQuest(3121, MapName: "yulgar", 2109);
        Core.MapItemQuest(3121, MapName: "yulgar", 2110);
        Core.MapItemQuest(3121, MapName: "archives", 2111);
        Core.MapItemQuest(3121, MapName: "swordhaven", 2112);
        Core.MapItemQuest(3121, MapName: "swordhaven", 2113);
        Core.MapItemQuest(3121, MapName: "swordhaven", 2114);
        Core.MapItemQuest(3121, MapName: "swordhaven", 2115);
        // }

        //Decor Rater
        Core.MapItemQuest(3122, MapName: "swordhaven", 2116, 8);

        //Cold Feet, Warm Heart
        Core.KillQuest(3123, "mafic", "Living Fire");

        //Chaos STILL Not Invited
        Core.KillQuest(3124, "ceremony", "Chaos Invader");

        //Protect the Princesses
        Core.MapItemQuest(3125, MapName: "ceremony", 2118, 6);

        //Seal the Chapel
        Core.MapItemQuest(3126, MapName: "ceremony", 2119);
        Core.KillQuest(3126, "ceremony", "Chaos Invader");

        //Chaos Kills!
        Core.KillQuest(3127, "ceremony", "Chaos Justicar", FollowupIDOverwrite: 3133);

        //Endless Aisle of Chaos
        Core.MapItemQuest(3133, MapName: "", 2127, 12, "chaosaltar");

        //Save the Princess... Again!
        Core.KillQuest(3134, "chaosaltar", "Princess Thrall", FollowupIDOverwrite: 3158);

        //Chaos Dragon Confrontation
        Core.KillQuest(3158, "castleroof", "Chaos Dragon");

        //To Catch a King
        Core.MapItemQuest(3159, MapName: "swordhavenfalls", 2158);

        //Chaos Lord Alteon
        Core.KillQuest(3160, "swordhavenfalls", "Chaos Lord Alteon", hasFollowup: false);

        Core.Relogin();
        Core.BuyItem("battleon", 991, "Cyber King");
        Bot.Sleep(700);
        Core.ToBank("Cyber King");
    }
}
