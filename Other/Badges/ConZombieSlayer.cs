/*
name: ConZombieSlayer
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ConZombieSlayer

{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (Core.HasAchievement(74))
        {
            Core.Logger($"Already have the {badge} badge");
            return;
        }       

        Core.Logger($"Doing Artix quest for {badge} badge");

        Core.EquipClass(ClassType.Farm);

        //I Guess We DO Need Steenkin' Badges 3135
        Story.KillQuest(3135, "vendorbooths", "Ravin' Skelly");

        //Con Kit 3136
        if (!Story.QuestProgression(3136))
        {
            Core.EnsureAccept(3136);
            Core.HuntMonster("battlecon", "Cosplay Zombie", "Con Survival Guide", log: false);
            Core.HuntMonster("battlecon", "Cosplay Zombie", "Water Bottle", log: false);
            Core.HuntMonster("battlecon", "Cosplay Zombie", "Hoopy Frood brand Towel", log: false);
            Core.HuntMonster("battlecon", "Cosplay Zombie", "Event Schedule", log: false);
            Core.HuntMonster("battlecon", "Cosplay Zombie", "Xtra-Strength Energy Potion", log: false);
            Core.HuntMonster("battlecon", "Cosplay Zombie", "Anti-Con Rot Sanitation Device", log: false);
            Core.EnsureComplete(3136);
        }

        Core.EquipClass(ClassType.Solo);
        //BrutalCorn Barrier 3137
        Story.KillQuest(3137, "battlecon", "BrutalCorn");

        // Badge Signing 3138
        Story.MapItemQuest(3138, "battlecon", Core.FromTo(2129, 2138));

        // 3139 does *not* exist

        Core.EquipClass(ClassType.Farm);
        // Con Rot 3140 - not required?
        // Story.KillQuest(3140, "vendorbooths", "Con Rot");

        // // Buzz-Killer: Caffeine Overload
        // Story.KillQuest(3141, "vendorbooths", "Caffeine Imp");

        // Core.Logger("3140 - 3141 appaerntly.. dont need done?");

        // Lights Out for Ravers 3142
        Story.KillQuest(3141, "artistalley", "Ravin' Skelly");

        if (Core.IsMember)
            // Silent but Undeadly 3143
            Story.KillQuest(3143, "artistalley", "Battle Odor");

        //Badge Quest - Cosplay Zombies On Parade 3144
        Core.EnsureAccept(3144);
        while (!Bot.ShouldExit && !Core.HasAchievement(74))
        {
            if (Bot.Map.Name != "battlecon")
                Core.Join("battlecon");
            if (Bot.Player.Cell != "a2")
                Core.Jump("a2", "left");

            Bot.Combat.Attack("*");
            Core.Sleep();
        }
        Core.AbandonQuest(3144);

    }

    private string badge = "ConZombie Slayer";
}
