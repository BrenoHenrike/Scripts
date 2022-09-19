//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class AQWZombies
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(2128))
        {
            Core.Logger($"Story: AQW Zombies - Complete");
            return;
        }

        Story.PreLoad(this);

        // Undead Assault
        Story.KillQuest(2093, "battleundera", "Skeletal Soldier");

        // Skull Crusher Mountain
        Story.KillQuest(2094, "battleundera", "Skeletal Ice Mage");

        // The Undead Giant
        Story.KillQuest(2095, "battleundera", "Angry Undead Giant");

        // Talk to the Knights
        Story.MapItemQuest(2096, "doomhaven", 1174, 5);

        // Defend the Throne Room
        Story.KillQuest(2097, "Doomhaven", "Skeletal Viking");

        // Rolith Defeated
        Story.ChainQuest(2117);

        // Lair
        Story.ChainQuest(2120);

        // Mythsong
        Story.ChainQuest(2121);

        // Arcangrove
        Story.ChainQuest(2122);

        // Willowshire
        Story.ChainQuest(2123);

        // Battleon
        Story.ChainQuest(2119);

        // Keep the Area Clear
        Story.KillQuest(2124, "doomwar", "Angry Zombie");

        // Defeat Zombie Dragons
        Story.KillQuest(2125, "doomwar", "Zombie Dragon");

        // Defeat Your Fallen Friends
        if (!Story.QuestProgression(2126))
        {
            Core.EnsureAccept(2126);
            Core.KillMonster("doomwar", "r5", "left", "Cyzerombie");
            Core.KillMonster("doomwar", "r7", "left", "Zombie Warlic");
            Core.KillMonster("doomwar", "r9", "left", "Zombie Galanoth");
            Core.KillMonster("doomwar", "r3", "left", "Zhoombie");
            Core.EnsureComplete(2126);
        }

        // Long Unlive the King!
        Story.KillQuest(2127, "doomwar", "Zombie King Alteon");

        // Dark Sepulchure Must be Slain!
        Story.KillQuest(2128, "sepulchure", "Dark Sepulchure");
    }
}