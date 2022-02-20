//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class AQWZombies
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();
        Core.Logger("AQWZombies Finished");

        Core.SetOptions(false);
    }


    public void Storyline()
    {
        if (Story.isCompletedBefore(2128))
            return;

        Story.KillQuest(2093, "battleundera", "Skeletal Soldier");
        Story.KillQuest(2094, "battleundera", "Skeletal Ice Mage");
        Story.KillQuest(2095, "battleundera", "Angry Undead Giant");
        Story.MapItemQuest(2096, "doomhaven", 1174, 5);
        Story.KillQuest(2097, "Doomhaven", "Skeletal Viking");
        Story.KillQuest(2117, "doomhaven", "Zombie Rolith");
        Story.KillQuest(2119, "doomwar", "Zombiue Galanoth");
        Story.KillQuest(2120, "doomwar", "Zombiue Warlic");
        Story.KillQuest(2121, "doomwar", "Cyzerombie");
        Story.KillQuest(2122, "doomwar", "Zhoombie");
        Story.KillQuest(2123, "doomwar", "Zhoombie");
        Story.KillQuest(2124, "doomwar", "Angry Zombie");
        Story.KillQuest(2125, "doomwar", "Zombie Dragon");
        if (!Story.QuestProgression(2126))
        {
            Core.EnsureAccept(2126);
            Core.KillMonster("doomwar", "r5", "left", "Cyzerombie");
            Core.KillMonster("doomwar", "r7", "left", "Zombie Warlic");
            Core.KillMonster("doomwar", "r9", "left", "Zombie Galanoth");
            Core.KillMonster("doomwar", "r3", "left", "Zhoombie");
            Core.EnsureComplete(2126);
        }
        Story.KillQuest(2127, "doomwar", "Zombie King Alteon");
        Story.KillQuest(2128, "sepulchure", "Dark Sepulchure");
        //----------------------------------------
    }
}