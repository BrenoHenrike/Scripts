//cs_include Scripts/CoreBots.cs

using System;
using RBot;
using System.Collections.Generic;

public class SagaYokai
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
        Core.BuyItem("battleon", 948, "Amethyst Mace");
        if (Core.CheckInventory("Amethyst Mace", toInv: false))
        {
            Bot.Sleep(700);
            Core.ToBank("Amethyst Mace");
            Core.Logger("Chapter: \"Chaos Lord Wolfwing\" already complete. Skipping");
            return;
        }

        //Turtle Power
        Core.KillQuest(380, "yokaiboat", "Kappa Ninja");
        Core.MapItemQuest(380, "yokaiboat", 64);

        //Setting Sail to Yokai
        Core.KillQuest(381, "yokaiboat", "Kappa Ninja", hasFollowup: false);

        //Dragon Koi Tournament
        if(!Bot.Quests.IsUnlocked(402))
        {
        Core.EnsureAccept(382);
        Core.KillMonster("dragonkoi", "t1", "Left", "Pockey Chew");
        Core.KillMonster("dragonkoi", "t2", "Left", "Notruto");
        Core.KillMonster("dragonkoi", "t3", "Left", "Nekoyasha");
        Core.KillMonster("dragonkoi", "t4", "Left", "Absolute Zero");
        Core.KillMonster("dragonkoi", "t5", "Left", "Sporkion");
        Core.KillQuest(382, "dragonkoi", "Ryoku", FollowupIDOverwrite: 402);
        }

        //Dog Days
        Core.KillQuest(402, "hachiko", "Samurai Nopperabo");
        Core.KillQuest(402, "hachiko", "Ninja Nopperabo");

        //Faceless Threat
        Core.KillQuest(380, "yokaiboat", "Samurai Nopperabo", FollowupIDOverwrite: 405);

        //Zodiac Puzzle Key
        Core.KillQuest(403, "hachiko", "Samurai Nopperabo");
        Core.KillQuest(403, "hachiko", "Ninja Nopperabo");
        Core.KillQuest(403, "hachiko", "Samurai Nopperabo");
        Core.KillQuest(403, "hachiko", "Ninja Nopperabo");
 
        //Rescue!
        Core.KillQuest(406, "hachiko", "Dai Tengu", FollowupIDOverwrite: 466);

        //Jinmenju Tree
        Core.MapItemQuest(466, "bamboo", 90);

        //Yokai Bandits
        Core.KillQuest(467, "bamboo", new[] { "Tanuki", "Tanuki" });

        //The Fiery Fiend
        Core.KillQuest(468, "bamboo", "SoulTaker");

        //Dumpster Diving
        Core.MapItemQuest(469, "junkyard", 91);

        //Reduce, Respawn, Recycle
        Core.KillQuest(470, "junkyard", new[] { "Wild Kara-Kasa", "Wild Kara-Kasa", "Wild Biwa-Bokuboku", "Wild Bura-Bura", "Wild Koto-Furunushi" });

        //The Hunt for the Hag
        Core.KillQuest(471, "junkyard", "Onibaba", FollowupIDOverwrite: 473);

        //Su-she
        Core.KillQuest(473, "yokairiver", new[] { "Funa-Yurei", "Funa-Yurei", "Funa-Yurei" });

        //Kappa Cuisine
        Core.KillQuest(474, "yokairiver", new[] { "Kappa Ninja", "Kappa Ninja", "Kappa Ninja" }, FollowupIDOverwrite: 476);
        Core.MapItemQuest(474, "yokairiver", 92, FollowupIDOverwrite: 476);

        //Hisssssy fit
        Core.KillQuest(476, "yokairiver", "Nure Onna");

        //The Purrrfect Crime
        Core.KillQuest(477, "yokaigrave", "Skello Kitty");

        //The Face Off
        Core.KillQuest(478, "yokaigrave", new[] { "Samurai Nopperabo", "Ninja Nopperabo" });

        //Confront Neko Mata
        Core.KillQuest(479, "yokaigrave", "Neko Mata", AutoCompleteQuest: true);

        //Defeat O-dokuro
        Core.KillQuest(481, "odokuro", "O-dokuro", FollowupIDOverwrite: 484);

        //Defeat O-Dokuro's Head
        Core.KillQuest(484, "yokaiwar", "O-Dokuro's Head", FollowupIDOverwrite: 488);

        //Defeat Kitsune
        Core.KillQuest(488, "kitsune", "kitsune", hasFollowup: false);

        Core.Relogin();
        Core.BuyItem("battleon", 948, "Amethyst Mace");
        Bot.Sleep(700);
        Core.ToBank("Amethyst Mace");
    }
}
