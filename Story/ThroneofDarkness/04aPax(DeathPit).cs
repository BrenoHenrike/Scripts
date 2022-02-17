//cs_include Scripts/CoreBots.cs
using RBot;
public class DeathPitArena
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

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

        // Mingle
        Core.MapItemQuest(5133, "deathpit", 4484, 1);
        Core.MapItemQuest(5133, "deathpit", 4485, 1);
        Core.MapItemQuest(5133, "deathpit", 4486, 1);
        Core.MapItemQuest(5133, "deathpit", 4487, 1);
        Core.MapItemQuest(5133, "deathpit", 4488, 1);
        Core.MapItemQuest(5133, "deathpit", 4489, 1);
        Core.MapItemQuest(5133, "deathpit", 4490, 1);
        Core.MapItemQuest(5133, "deathpit", 4491, 1);
        // Those Dummies
        Core.KillQuest(5134, "deathpit", "Training Dummy");
        // Round 1: You vs Omar the Meek!
        Core.KillQuest(5135, "deathpit", "Omar the Meek");
        // Round 2: You vs a Bunch of Sneevils!
        Core.KillQuest(5136, "deathpit", "Sneevil");
        // Battle: You vs Hattori!
        Core.KillQuest(5137, "deathpit", "Hattori");
        // Battle: You vs the Slime Horde!
        Core.KillQuest(5138, "deathpit", new[] { "Slime", "Giant Green Slime" });
        // Battle: You vs the Sludge Lord!
        Core.KillQuest(5139, "deathpit", "Sludgelord");
        // Battle: You vs Some Salamanders!
        Core.KillQuest(5140, "deathpit", "Salamander");
        // Battle: You vs a Trobble!
        Core.KillQuest(5141, "deathpit", "Trobble");
        // Trouble Battle
        Core.KillQuest(5142, "deathpit", "Trobble");
        // Horc Battle
        Core.KillQuest(5143, "deathpit", "Horc Gladiator");
        // Battle: You vs the Brawlers!
        Core.KillQuest(5144, "deathpit", "Drakel Brawler");
        // Battle: You vs the Gladiators!
        Core.KillQuest(5145, "deathpit", "Drakel Gladiator");
        // Battle: You vs the Battlemasters!
        Core.KillQuest(5146, "deathpit", "Drakel Battlemaster");
        // Battle: You vs General Gall!
        Core.KillQuest(5147, "deathpit", "General Gall");
        // Velm's Minions
        Core.KillQuest(5148, "deathpit", "Drakel Brawler|Drakel Gladiator");
        // eneral Velm
        Core.KillQuest(5149, "deathpit", "General Velm");
        // Chud's Minions
        Core.KillQuest(5150, "deathpit", "Drakel Battlemaster");
        // General Chud
        Core.KillQuest(5151, "deathpit", "General Chud");
        // Hun'Gar's Minions
        Core.KillQuest(5152, "deathpit", "Drakel Battlemaster");
        // Hun'Gar Defeated
        Core.KillQuest(5153, "deathpit", "General Hun'Gar");
        // Pax Defeated
        Core.KillQuest(5154, "deathpit", "Warlord Pax");
    }
}
