//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class FezziniStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        FezziniScript();

        Core.SetOptions(false);
    }

    public void FezziniScript()
    {
        if (!Core.isSeasonalMapActive("fezzini"))
            return;
        if (Core.isCompletedBefore(7389))
            return;

        Story.PreLoad(this);

        Bot.Drops.Start();

        //The Dancing Dead
        Story.KillQuest(7377, "fezzini", "Zombie Dancer");

        //Rats n' Goo
        Story.KillQuest(7378, "fezzini", new[] { "Street Rat", "Zombie Goo" });

        //Get a Clue
        Story.KillQuest(7379, "fezzini", "Zombie Dancer");

        //Find Lim
        Story.MapItemQuest(7380, "fezzini", 7100);

        //Bottle Time
        Story.KillQuest(7381, "fezzini", "Zombie Dancer");

        //Get Some Fur
        Story.KillQuest(7382, "fezzini", new[] { "Zombie Goo", "Street Rat" });

        //Go Tell Beleen
        Story.MapItemQuest(7383, "fezzini", 7101);

        //Ask Around
        Story.MapItemQuest(7384, "fezzini", new[] { 7102, 7103, 7104, 7105, 7106 });

        //Zombie Invasion
        Story.KillQuest(7385, "fezzini", "Zombie Dancer");

        //Castle Zombies
        Story.KillQuest(7386, "fezzini", "Hostile Minion");

        //Warn the King and Queen
        Story.MapItemQuest(7387, "fezzini", 7107);

        //Monstrous Guards!
        Story.KillQuest(7388, "fezzini", "Monstrous Guard");

        //It's Salvaza!
        Story.KillQuest(7389, "fezzini", "Salvaza");
    }
}