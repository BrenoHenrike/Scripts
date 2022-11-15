//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Shattersword
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(2690))
            return;
        
        if (!Core.IsMember)
        {
            Core.Logger("Shattersword is a member-only storyline.");
            return;
        }

        Story.PreLoad(this);

        // Secure Shattersword Cavern 2681
        if (!Story.QuestProgression(2681))
        {
            Core.EnsureAccept(2681);
            Core.GetMapItem(1642, 4, "shattersword");
            Core.KillMonster("shattersword", "Enter", "Spawn", "Forest Imp", "Imp Removed", 5, log: false);
            Core.EnsureComplete(2681);
        }

        // Shatter Fallen Warriors 2682
        if (!Story.QuestProgression(2682))
        {
            Core.EnsureAccept(2682);
            Core.KillMonster("shattersword", "r2", "Left", "Fallen Warrior", "Guards Slain", 4, log: false);
            Core.EnsureComplete(2682);
        }

        // Corruption Cleansed 2683
        if (!Story.QuestProgression(2683))
        {
            Core.EnsureAccept(2683);
            Core.KillMonster("shattersword", "r3", "Left", "Dark Fairy", "Faerie Defeated", 6, log: false);
            Core.KillMonster("shattersword", "r3", "Left", "Dark Fairy", "Taint Reduced", 5, log: false);
            Core.EnsureComplete(2683);
        }

        // Decimate the Defiler's Army 2684
        if (!Story.QuestProgression(2684))
        {
            Core.EnsureAccept(2684);
            Core.KillMonster("shattersword", "r6", "Right", "Shattersword Prisoner", "Minions Slain", 6, log: false);
            Core.EnsureComplete(2684);
        }

        // Explosive Protection Poison 2685
        if (!Story.QuestProgression(2685))
        {
            Core.EnsureAccept(2685);
            Core.GetMapItem(1643, 6, "shattersword");
            Core.GetMapItem(1644, 3, "shattersword");
            Core.GetMapItem(1645, 4, "shattersword");
            Core.KillMonster("shattersword", "r2", "Left", "Fallen Warrior", "Flint and Striker", log: false);
            Core.EnsureComplete(2685);
        }

        // Gravelyn's Stolen Soldiers 2686
        if (!Story.QuestProgression(2686))
        {
            Core.EnsureAccept(2686);
            Core.KillMonster("shattersword", "r6", "Right", "Shattersword Prisoner", "Attackers Slain", 7, log: false);
            Core.EnsureComplete(2686);
        }

        // Dark Sparks 2691
        if (!Story.QuestProgression(2691))
        {
            Core.EnsureAccept(2691);
            Core.KillMonster("shattersword", "r3", "Left", "Dark Fairy", "Dark Spark", 25, log: false);
            Core.EnsureComplete(2691);
        }

        // When Wild Elves Attack! 2687
        if (!Story.QuestProgression(2687))
        {
            Core.EnsureAccept(2687);
            Core.KillMonster("shattersword", "r7", "Right", "Forest Elf", "Treewalker Sandals", log: false);
            Core.KillMonster("shattersword", "r7", "Right", "Forest Elf", "Rappelling Gear", log: false);
            Core.KillMonster("shattersword", "r7", "Right", "Forest Elf", "Sap-B-Gone", 3, log: false);
            Core.EnsureComplete(2687);
        }

        // Tree of Light 2688
        if (!Story.QuestProgression(2688))
        {
            Core.EnsureAccept(2688);
            Core.KillMonster("shattersword", "Enter", "Spawn", "Forest Imp", "Imp Slain", 6, log: false);
            Core.EnsureComplete(2688);
        }

        // Purity Beats Corruption 2689
        if (!Story.QuestProgression(2689))
        {
            Core.EnsureAccept(2689);
            Core.GetMapItem(1646, 7, "shattersword");
            Core.GetMapItem(1647, 7, "shattersword");
            Core.KillMonster("shattersword", "r6", "Right", "Shattersword Prisoner", "Guards Slain", 5, log: false);
            Core.EnsureComplete(2689);
        }

        // Hero vs Beast 2690
        if (!Story.QuestProgression(2690))
        {
            Core.EnsureAccept(2690);
            Core.KillMonster("shattersword", "r11", "Left", "Graveclaw the Defiler", "Graveclaw Slain", log: false);
            Core.EnsureComplete(2690);
        }
    }
}