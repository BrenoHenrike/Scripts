//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs

//cs_include Scripts/CoreFile(Or folder)/Filename.cs

using RBot;

public class AQWZombies
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        AdventureQuestWorldsZombiesQuestline();
    }


    public void AdventureQuestWorldsZombiesQuestline()
    {
        Core.Logger("AQW Zombies Questline;")
        Core.Logger("------------------------------");
        Core.Logger(" Doom Haven Questline");
        Core.Logger("------------------------------");
        //Doom Haven - Atrix
        // [2093]    Undead Assault
        // [2094]    Skull Crusher Mountain
        // [2095]    The Undead Giant
        //----------------------------------------
        Core.Logger("Undead Assult;");
        //Undead Assult-
        //-Slain Skeletal Soldier x10
        //-Dropped by Skeletal Soldier (1)
        //-battleundera
        Core.KillQuest(2093, "battleundera", "keletal Soldier");

        Core.Logger("Skull Crusher Moutnain;");
        //Skull Crusher Moutnain
        //-Frozen Bonehead x8
        //-Dropped by Skeletal Ice Mage (Level 1) (Version 1)
        //-battleundera
        Core.KillQuest(2094, "battleundera", "Skeletal Ice Mage");

        Core.Logger("The Undead Giant;");
        //The Undead Giant-
        //-Undead Giant's Sword x1
        //-Dropped by Angry Undead Giant
        //-battleundera
        Core.KillQuest(2095, "battleundera", "Angry Undead Giant");
        //----------------------------------------



        //Doom Haven - Robina
        // [2096]    Talk to the Knights
        // [2097]    Defend the Throne Room
        //----------------------------------------
        Core.Logger("Talk to The Knights;");
        //Talk to The Knights-
        //-Convince the Knights x5
        //-Talk to the knights in the castle
        bot.SendPacket($"InsertPacket");
        EnsureComplete(2096);

        Core.Logger("Defend the Throne Room;");
        //Defend the Throne Room-
        //Skeleton Slain x12
        //-Dropped by Skeletal Viking (Level 3 (Version 1))
        //-battleundera
        Core.KillQuest(2097, "battleundera", "Skeletal Viking", FollowupIDOverwrite: 2124);
        //----------------------------------------



        //Doom War - Atrix
        Core.Logger(" Doom War Questline");
        Core.Logger("------------------------------");
        // [2124]    Keep the Area Clear
        // [2125]    Defeat Zombie Dragons
        // [2126]    Defeat Your Fallen Friends
        // [2127]    Long Unlive the King!
        // [2128]    Dark Sepulchure Must be Slain!
        //----------------------------------------
        Core.Logger("Keep the Alter Clear;");
        //Keep the Alter Clear-
        //-Zombie Slain x5
        //-Dropped by Angry Zombie
        //-doomwar
        Core.KillQuest(2124, "doomwar", "Angry Zombie");

        Core.Logger("Defeat Zombie Dragons;");
        //Defeat Zombie Dragons
        //-Zombie Dragon Defeated x3
        //-Dropped by Zombie Dragon (Level 20)
        //-doomwar
        Core.KillQuest(2125, "doomwar", "Zombie Dragon");

        Core.Logger("Defated Your Falled Friends;");
        //Defated Your Falled Friends-
        //-Fallen Friend Defeated x4
        //-Dropped by Cyzerombie - doomwar
        //-Dropped by Zombie Alina - doomwar
        //-Dropped by Zombie Rolith - doomhaven
        //-Dropped by Zhoombie - doomwar
        Core.KillQuest(2126, "doomwar", new[] { "Cyzerombie", "Zombie Alina", "Zombie Rolith", "Zhoombie" });

        Core.Logger("Long Unlive the King;");
        //Long Unlive the King-
        //-Fallen King Defeated x1
        //-Dropped by Zombie King Alteon
        //-doomwar
        Core.KillQuest(2127, "doomwar", "Zombie King Alteon");

        Core.Logger("Dark Sepulchure Must be Slain!;");
        //Dark Sepulchure Must be Slain!-
        //-Defeat Dark Sepulchure x1
        //-Dropped by Dark Sepulchure
        //-sepulchure
        Core.KillQuest(2128, "sepulchure", "Dark Sepulchure");
        //----------------------------------------
    }
}