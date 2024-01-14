/*
name: Necromance
description: This will complete the Necromance story quest.
tags: necromance, seasonal, heros-heart-day, heart, hero, story
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Necromance
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        CompleteStory();
        Core.SetOptions(false);
    }

    public void CompleteStory()
    {
        if (Core.isCompletedBefore(4068))
            return;
            
        if (!Core.isSeasonalMapActive("necromance"))
            return;

        Story.PreLoad(this);

        // Cupid's Courier (4039)
        Story.MapItemQuest(4039, "necromance", 3151);

        // Last Minute Gift Ideas (4040)
        Story.KillQuest(4040, "love", new[] {"Love Shrub", "Huggy-Bear"});
        Story.MapItemQuest(4040, "love", 3152, 12);

        // Cravin' a Raven (4041)
        Story.KillQuest(4041, "judgement", "Raven");

        // Owl Always Love You (4042)
        Story.KillQuest(4042, "pines", "LeatherWing");

        // This is My Broom STICK! (4043)
        Story.KillQuest(4043, "farm", "Scarecrow");

        // Yes We Candle! (4044)
        Story.KillQuest(4044, "darkoviaforest", "Blood Maggot");

        // Itty Bitty Kitty (4045)
        Story.KillQuest(4045, "beleensdream", "Disgruntled DoomKitten");

        // Clock Blocker (4046)
        Story.KillQuest(4046, "timevoid", "Time-Travel Fairy");

        // Shoots! And Ladders (4047)
        Story.KillQuest(4047, "giant", "Red Ant");

        // Mirror Image (4048)
        Story.KillQuest(4048, "battleoff", "Evil Moglin");

        // Opal Omen (4049)
        Story.KillQuest(4049, "earthstorm", "Amethite");

        // A Salt with a Deadly Weapon (4050)
        if (!Story.QuestProgression(4050))
        {
            Core.EnsureAccept(4050);
            Core.HuntMonster("willowcreek", "Snail", "Iodized Salt", 3);
            Core.HuntMonster("waterstorm", "Deep Dweller", "Sea Salt");
            Core.EnsureComplete(4050);
        }

        // Mind your Manors, Magpie (4051)
        Story.KillQuest(4051, "manor", "Bird of Paradise");

        // Umbrella for my Bella (4052)
        Story.KillQuest(4052, "junkyard", "Tsukumo-Gami");

        // Bat Crazy! (4053)
        Story.KillQuest(4053, "darkoviagrave", "Albino Bat");

        // Present the Presents (4065)
        if (!Story.QuestProgression(4065))
        {
            Core.EnsureAccept(4065);
            while(!Core.CheckInventory("Presents for Nastasia", 13))
            {
                Core.EnsureAccept(4053);
                Core.HuntMonster("darkoviagrave", "Albino Bat", "Perfect Pet Bat");
                Core.EnsureComplete(4053);
                Bot.Wait.ForPickup("Presents for Nastasia");
            }
            Core.EnsureComplete(4065);
        }

        // I Want Tibia Your Valentine (4066)
        Story.KillQuest(4066, "love", "Forlorn Love");

        // Kill Nastasia (4067)
        Story.KillQuest(4067, "necromance", "Nastasia");

        // Gravelyn Stops Nastasia! (4068)
        Story.KillQuest(4068, "necromance", "Nastasia");
    }
}
