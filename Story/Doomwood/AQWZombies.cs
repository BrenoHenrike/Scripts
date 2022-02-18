//cs_include Scripts/CoreBots.cs
using RBot;

public class AQWZombies
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();
        Core.Logger("AQWZombies Finished");

        Core.SetOptions(false);
    }


    public void Storyline()
    {
        if (Core.isCompletedBefore(2128))
            return;

        Core.KillQuest(2093, "battleundera", "Skeletal Soldier");
        Core.KillQuest(2094, "battleundera", "Skeletal Ice Mage");
        Core.KillQuest(2095, "battleundera", "Angry Undead Giant");
        Core.MapItemQuest(2096, "doomhaven", 1174, 5);
        Core.KillQuest(2097, "Doomhaven", "Skeletal Viking");
        Core.KillQuest(2117, "doomhaven", "Zombie Rolith");
        Core.KillQuest(2119, "doomwar", "Zombiue Galanoth");
        Core.KillQuest(2120, "doomwar", "Zombiue Warlic");
        Core.KillQuest(2121, "doomwar", "Cyzerombie");
        Core.KillQuest(2122, "doomwar", "Zhoombie");
        Core.KillQuest(2123, "doomwar", "Zhoombie");
        Core.KillQuest(2124, "doomwar", "Angry Zombie");
        Core.KillQuest(2125, "doomwar", "Zombie Dragon");
        if (!Core.QuestProgression(2126))
        {
            Core.EnsureAccept(2126);
            Core.KillMonster("doomwar", "r5", "left", "Cyzerombie");
            Core.KillMonster("doomwar", "r7", "left", "Zombie Warlic");
            Core.KillMonster("doomwar", "r9", "left", "Zombie Galanoth");
            Core.KillMonster("doomwar", "r3", "left", "Zhoombie");
            Core.EnsureComplete(2126);
        }
        Core.KillQuest(2127, "doomwar", "Zombie King Alteon");
        Core.KillQuest(2128, "sepulchure", "Dark Sepulchure");
        //----------------------------------------
    }
}