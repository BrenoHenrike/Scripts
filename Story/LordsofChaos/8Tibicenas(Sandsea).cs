//cs_include Scripts/CoreBots.cs
using RBot;

public class SagaSandsea
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

    //Core.MapItemQuest(questid, "Mapname", mapitemid, amount);
    //Core.KillQuest(questid, "Mapname", "mobname");

    public void StoryLine()
    {
        Core.BuyItem("battleon", 952, "Angelic Lightning");
        if (Core.CheckInventory("Angelic Lightning", toInv: false))
        {
            Bot.Sleep(700);
            Core.ToBank("Angelic Lightning");
            Core.Logger("Chapter: \"Chaos Lord Tibicenas\" already complete. Skipping");
            return;
        }

        //Sandport and Starboard
        Core.MapItemQuest(930, "sandport", 251);

        //Shark Diving
        Core.KillQuest(931, "sandport", "Sandshark");

        //Thieving Cut Throats
        Core.KillQuest(932, "sandport", "Tomb Robber");

        //Lost and Found
        Core.KillQuest(933, "sandport", "Tomb Robber");

        //Sell-Sword Sell-Outs
        Core.KillQuest(934, "sandport", new[] { "Horc Sell-Sword", "Horc Sell-Sword" }, FollowupIDOverwrite: 967);

        //Sacred Scarabs
        Core.KillQuest(967, "pyramid", "Golden Scarab");

        //A Noob is Guard
        Core.KillQuest(968, "pyramid", "Anubis Deathguard");

        //Bandaged Aids
        Core.KillQuest(969, "pyramid", "Mummy");

        //Keys to the Royal Chamber
        Core.KillQuest(970, "pyramid", "Golden Scarab");

        //Confront Duat
        Core.MapItemQuest(971, "pyramid", 304);

        //They've Gone Dark
        Core.KillQuest(972, "wanders", "Kalestri Worshiper");

        //Bad Doggies
        Core.KillQuest(973, "wanders", "Kalestri Hound");

        //Essentially Evil
        Core.KillQuest(974, "wanders", "Kalestri Hound");

        //Loose Threads
        Core.KillQuest(975, "wanders", "Lotus Spider");

        //Seek The Treasure
        Core.MapItemQuest(976, "wanders", 306);

        //Dreamsand
        Core.KillQuest(977, "wanders", "Lotus Spider");

        //I Dream Of...
        Core.KillQuest(978, "wanders", "Sek-Duat", FollowupIDOverwrite: 995);

        //Sandsational Castle
        Core.MapItemQuest(995, "sandcastle", 361);

        //Furry Fury
        Core.KillQuest(996, "sandcastle", "War Hyena");

        //Keeping Secrets Under Wraps
        Core.KillQuest(997, "sandcastle", "War Mummy");

        //Gem Jam
        Core.KillQuest(998, "sandcastle", "War Hyena");

        //Enter the Sphinx
        Core.KillQuest(999, "sandcastle", "Chaos Sphinx");

        //Unlamented Lamia
        Core.KillQuest(1000, "djinn", "Lamia");

        //E-vase-ive Measures
        Core.KillQuest(1001, "sandsea", "Desert Vase");

        //Tri-hump-hant Camels
        Core.KillQuest(1002, "sandsea", "Bupers Camel");

        //I Don't Mean to Harp On It...
        Core.KillQuest(1003, "djinn", "Harpy");

        //In-djinn-ious Solution
        Core.MapItemQuest(1004, "djinn", 370, 5);

        //Chaos Lord Tibicenas
        Core.KillQuest(1005, "djinn", "Tibicenas", hasFollowup: false);

        Core.Relogin();
        Core.BuyItem("battleon", 952, "Angelic Lightning");
        Bot.Sleep(700);
        Core.ToBank("Angelic Lightning");

    }
}

