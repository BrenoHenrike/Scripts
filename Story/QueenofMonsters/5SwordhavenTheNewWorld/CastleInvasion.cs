//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs

using RBot;

public class CompleteTheNewWorld
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreFarms Farm = new CoreFarms();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CastleInvasion();

        Core.SetOptions(false);
    }

    public void CastleInvasion()
    {
        //Progress Check
        if (Core.isCompletedBefore(5586))
            return;

        //Preload Quests
        Story.PreLoad();

        //Stand for Swordhaven
        Story.KillQuest(5575, "LycanInvasion", new[] { "Fallen Knight", "Infernal Knight" });

        //Use Their Energy Against Them
        Story.KillQuest(5576, "ShadowfallInvasion", "Nethermage");

        //Scry this!
        Story.KillQuest(5577, "ShadowfallInvasion", "Diabolical Scryer");

        //Recalibration
        if (!Story.QuestProgression(5578))
        {
            Core.EnsureAccept(5578);
            Core.KillMonster("DoomPally", "r3", "Right", "*", "Doomwood Invaders Fought", 4);
            Core.KillMonster("DarkoviaInvasion", "Enter", "Spawn", "*", "Darkovia Invaders Fought", 4);
            Core.KillMonster("ShadowfallInvasion", "r4", "Left", "*", "Shadowfall Invaders Fought", 4);
            Core.EnsureComplete(5578);
        }

        //Arm the Armored!
        Story.MapItemQuest(5579, "CastleInvasion", 5055, 5);
        Story.MapItemQuest(5579, "CastleInvasion", 5056);

        //Save the Citizens
        Story.MapItemQuest(5580, "CastleInvasion", 5058, 5);

        //Destroy the Infernals
        Story.KillQuest(5581, "CastleInvasion", new[] { "Infernal Knight", "Fallen Knight", "Nethermage" });

        //What is THAT??
        Story.KillQuest(5582, "CastleInvasion", "Giant Worm of Teeth");

        //Arm the Civilians
        Story.MapItemQuest(5583, "CastleInvasion", 5057, 4);

        //Get Those Beasts
        Story.KillQuest(5584, "CastleInvasion", new[] { "Infernal Imp", "Underworld Hound" });

        //It's Baaaaack!
        Story.KillQuest(5585, "CastleInvasion", "Giant Worm of Teeth");

        //Him Again???
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5586, "CastleInvasion", "Lord Balax'el");
    }
}