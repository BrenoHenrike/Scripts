//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;
public class DeathPitArena
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DeathPitArenaSaga();

        Core.SetOptions(false);
    }

    public void DeathPitArenaSaga()
    {
        if (Core.isCompletedBefore(5154))
            return;

        Story.PreLoad();

        // Mingle
        Story.MapItemQuest(5133, "deathpit", new[] { 4484, 4485, 4486, 4487, 4488, 4489, 4490, 4491 });

        // Those Dummies
        Story.KillQuest(5134, "deathpit", "Training Dummy");

        // Round 1: You vs Omar the Meek!
        Story.KillQuest(5135, "deathpit", "Omar the Meek");

        // Round 2: You vs a Bunch of Sneevils!
        Story.KillQuest(5136, "deathpit", "Sneevil");

        // Battle: You vs Hattori!
        Story.KillQuest(5137, "deathpit", "Hattori");

        // Battle: You vs the Slime Horde!
        Story.KillQuest(5138, "deathpit", new[] { "Slime", "Giant Green Slime" });

        // Battle: You vs the Sludge Lord!
        Story.KillQuest(5139, "deathpit", "Sludgelord");

        // Battle: You vs Some Salamanders!
        Story.KillQuest(5140, "deathpit", "Salamander");

        // Battle: You vs a Trobble!
        Story.KillQuest(5141, "deathpit", "Trobble");

        // Trouble Battle
        Story.KillQuest(5142, "deathpit", "Trobble");

        // Horc Battle
        Story.KillQuest(5143, "deathpit", "Horc Gladiator");

        // Battle: You vs the Brawlers!
        Story.KillQuest(5144, "deathpit", "Drakel Brawler");

        // Battle: You vs the Gladiators!
        Story.KillQuest(5145, "deathpit", "Drakel Gladiator");

        // Battle: You vs the Battlemasters!
        Story.KillQuest(5146, "deathpit", "Drakel Battlemaster");

        // Battle: You vs General Gall!
        Story.KillQuest(5147, "deathpit", "General Gall");

        // Velm's Minions
        Story.KillQuest(5148, "deathpit", "Drakel Brawler|Drakel Gladiator");

        // eneral Velm
        Story.KillQuest(5149, "deathpit", "General Velm");

        // Chud's Minions
        Story.KillQuest(5150, "deathpit", "Drakel Battlemaster");

        // General Chud
        Story.KillQuest(5151, "deathpit", "General Chud");

        // Hun'Gar's Minions
        Story.KillQuest(5152, "deathpit", "Drakel Battlemaster");

        // Hun'Gar Defeated
        Story.KillQuest(5153, "deathpit", "General Hun'Gar");

        // Pax Defeated
        Story.KillQuest(5154, "deathpit", "Warlord Pax");
    }
}
