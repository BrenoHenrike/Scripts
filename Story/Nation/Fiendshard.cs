/*
name: Fiendshard
description: This will finish the Fiendshard quest.
tags: story, quest, nation, fiendshard
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Nation/Originul.cs
using Skua.Core.Interfaces;

public class Fiendshard_Story
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public Originul_Story Originul = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Fiendshard_QuestlineP1();

        Core.SetOptions(false);
    }

    public void Fiendshard_QuestlineP1()
    {
        if (Core.isCompletedBefore(7900))
            return;

        Story.PreLoad(this);

        Originul.Originul_Questline();

        Core.Logger("Making sure portal quest is done.. appaerntly its required...? (though not stated?)");
        Core.Join("tercessuinotlim");

        // Sneak Attack
        if (!Story.QuestProgression(7892))
        {
            Core.EnsureAccept(7892);
            Core.KillMonster("Fiendshard", "r2", "Left", "Rogue Fiend", "Rogue Fiends Defeated", 4);
            Story.MapItemQuest(7892, "Fiendshard", 7983);
        }

        // Fiend-terrogation
        if (!Story.QuestProgression(7893))
        {
            Core.EnsureAccept(7893);
            Core.KillMonster("Fiendshard", "r2", "Left", "Rogue Fiend", "Fiends Interrogated", 3);
            Core.EnsureComplete(7893);
        }

        // Key Difference Between Human and Fiend
        if (!Story.QuestProgression(7894))
        {
            Core.EnsureAccept(7894);
            Core.KillMonster("Fiendshard", "r2", "Left", "Rogue Fiend", "Key Fragments Located", 4);
            Core.EnsureComplete(7894);
        }

        // Unlock the Door
        if (!Story.QuestProgression(7895))
        {
            Core.EnsureAccept(7895);
            Core.KillMonster("fiendshard", "r2", "Left", "Rogue Fiend", "Rogue Fiend Defeated", 5);
            Core.KillMonster("fiendshard", "r5", "Left", "Paladin Fiend", "Paladin Fiend Defeated", 5);
            Core.HuntMonster("fiendshard", "Void Knight", "Void Knight Defeated", 3);
            Story.MapItemQuest(7895, "Fiendshard", 7984);
        }

        // Dirtlicking Guards
        Story.KillQuest(7896, "Fiendshard", "Paladin Fiend");

        // Defeat Dirtlicker
        Story.KillQuest(7897, "Fiendshard", new[] { "Fiend Shard", "Dirtlicker" });

        // Destroy the Fiend Shard
        // Archfiend DeathLord quests can be done without finishing this quest.
        if (!Story.QuestProgression(7898))
        {
            // Bot.Events.CellChanged += CutSceneFixer;
            Core.Join("fiendshard", "r9");
            while (!Bot.ShouldExit && Bot.Player.Cell != "r9")
            {
                Core.Sleep(2500);
                Core.Jump("r9");
            }
            Core.EnsureAccept(7898);
            Core.KillNulgathFiendShard("Nulgath's Fiend Shard Destroyed", isTemp: true);
            Core.KillMonster("fiendshard", "r9", "Left", "Paladin Fiend", "Fiends Fended Off", 15);
            Core.Jump("Enter", "Spawn");
            Bot.Wait.ForCombatExit();
            Core.EnsureComplete(7898);
        }
    }
}
