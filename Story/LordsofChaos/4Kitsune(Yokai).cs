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
            Core.Logger("Chapter: \"Chaos Lord Kitsune\" already complete. Skipping");
            return;
        }

        //Turtle Power
        Core.KillQuest(380, "yokaiboat", "Kappa Ninja");
        Core.MapItemQuest(380, "yokaiboat", 64);

        //Setting Sail to Yokai
        if (!Core.QuestProgression(381))
        {
            Core.EnsureAccept(381);
            Core.KillMonster("yokaiboat", "r4", "Spawn", "3844", "Sail Permit");
            Core.EnsureComplete(381);
        }

        //Dragon Koi Tournament
        Core.KillQuest(382, "dragonkoi", "Ryoku", FollowupIDOverwrite: 402);

        //Dog Days
        // Core.KillQuest("hachiko", "Samurai Nopperabo");
        // Core.KillQuest("hachiko", "Ninja Nopperabo");
        if (!Core.QuestProgression(402))
        {
            Core.EnsureAccept(402);
            Core.HuntMonster("hachiko", "Samurai Nopperabo", "Samurai Questioned", 5);
            Core.HuntMonster("hachiko", "Ninja Nopperabo", "Ninja Questioned", 5);
            Core.EnsureComplete(402);
        }

        //Faceless Threat - i think it has to be this cell? it wasnt getting the item from the rest
        if (!Core.QuestProgression(403, FollowupIDOverwrite: 405))
        {
            Core.EnsureAccept(403);
            Core.KillMonster("hachiko", "Ox", "Center", "Samurai Nopperabo", "Note from DT");
            Core.EnsureComplete(403);
        }

        //Zodiac Puzzle Key
        if (!Core.QuestProgression(405))
        {
            Core.EnsureAccept(405);
            Core.KillMonster("hachiko", "Tiger", "Center", "Samurai Nopperabo", "Rat-Ox-Tiger Piece");
            Core.KillMonster("hachiko", "Snake", "Center", "Ninja Nopperabo", "Rabbit-Dragon-Snake piece");
            Core.KillMonster("hachiko", "Horse", "Center", "Samurai Nopperabo", "Horse-Sheep-Monkey piece");
            Core.KillMonster("hachiko", "Pig", "Center", "Ninja Nopperabo", "Rooster-Dog-Pig Piece");
            Core.EnsureComplete(405);
        }

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
        if (!Core.QuestProgression(470))
        {
            Core.EnsureAccept(470);
            Core.KillMonster("Junkyard", "Enter", "Spawn", 3847, "Wild Kara-Kasa", 5);
            Core.KillMonster("Junkyard", "Enter", "Spawn", 3848, "Wild Bakezouri", 1);
            Core.KillMonster("Junkyard", "r2", "Right", 3845, "Wild Bura-Bura", 4);
            Core.KillMonster("Junkyard", "r2", "Right", 3849, "Wild Biwa-Bokuboku", 3);
            Core.KillMonster("Junkyard", "r3", "Down", 3850, "Wild Koto-Furunushi", 2);
            Core.EnsureComplete(470);
        }
        // Core.KillQuest(470, "junkyard", new[] { "Tsukumo-Gami", "Tsukumo-Gami", "Tsukumo-Gami", "Tsukumo-Gami" });

        //The Hunt for the Hag
        Core.KillQuest(471, "junkyard", "Onibaba", FollowupIDOverwrite: 473);

        //Su-she
        Core.KillQuest(473, "yokairiver", new[] { "Funa-Yurei", "Funa-Yurei", "Funa-Yurei" });

        //Kappa Cuisine
        if (!Core.QuestProgression(474, FollowupIDOverwrite: 476))
        {
            Core.EnsureAccept(474);
            Core.KillMonster("yokairiver", "r4", "Left", "Kappa Ninja", "Dried Nori Leaf", 6);
            Core.KillMonster("yokairiver", "r4", "Left", "Kappa Ninja", "Sumeshi Bundle", 3);
            Core.KillMonster("yokairiver", "r4", "Left", "Kappa Ninja", "Fresh Cucumber", 1);
            Core.GetMapItem(92, 1, "yokairiver");
            Core.EnsureComplete(474);
        }

        //Hisssssy fit
        Core.KillQuest(476, "yokairiver", "Nure Onna");

        //The Purrrfect Crime
        Core.KillQuest(477, "yokaigrave", "Skello Kitty");

        //The Face Off
        Core.KillQuest(478, "yokaigrave", new[] { "Ninja Nopperabo", "Samurai Nopperabo" });

        //Confront Neko Mata
        Core.KillQuest(479, "yokaigrave", "Neko Mata");

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
