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
    public CoreStory Story = new CoreStory();
    public Originul_Story Originul = new Originul_Story();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Fiendshard_Questline();

        Core.SetOptions(false);
    }

    public void Fiendshard_Questline()
    {
        if (Core.isCompletedBefore(7898))
            return;

        Story.PreLoad(this);

        Originul.Originul_Questline();

        //used quest progs for mobs/room optimization

        // Sneak Attack
        if (!Core.isCompletedBefore(7892))
        {
            Core.EnsureAccept(7892);
            Core.KillMonster("Fiendshard", "r2", "Left", "Rogue Fiend");
            Story.MapItemQuest(7892, "Fiendshard", 7983);
        }

        // Fiend-terrogation
        if (!Core.isCompletedBefore(7893))
        {
            Core.EnsureAccept(7893);
            Core.KillMonster("Fiendshard", "r2", "Left", "Rogue Fiend");
            Core.EnsureComplete(7893);
        }

        // Key Difference Between Human and Fiend
        if (!Core.isCompletedBefore(7894))
        {
            Core.EnsureAccept(7894);
            Core.KillMonster("Fiendshard", "r2", "Left", "Rogue Fiend");
            Core.EnsureComplete(7894);
        }

        // Unlock the Door
        if (!Core.isCompletedBefore(7895))
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
        Bot.Events.CellChanged += CutSceneFixer;
        if (!Core.isCompletedBefore(7898))
        {
            Core.EnsureAccept(7898);
            Core.HuntMonsterMapID("fiendshard", 15, "Nulgath's Fiend Shard Destroyed");
            Core.HuntMonsterMapID("fiendshard", 14, "Fiends Fended Off", 15);
            Core.EnsureComplete(7898);
        }
    }


    void CutSceneFixer(string map, string cell, string pad)
    {
        if (map == "fiendshard" && cell != "r9")
        {
            Core.JumpWait();
            while (!Bot.ShouldExit && Bot.Player.Cell != "r9")
            {
                Bot.Sleep(2500);
                Core.Jump("r9", "Left");
                Bot.Sleep(2500);
            }
        }
        Bot.Events.CellChanged -= CutSceneFixer;
    }
}
