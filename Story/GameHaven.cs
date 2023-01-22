/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Gamehaven
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (!Core.IsMember || Core.isCompletedBefore(953))
            return;

        Story.PreLoad(this);

        // Agree to help Ethan
        Story.ChainQuest(940);

        // Clues for the Clueless
        Story.MapItemQuest(941, "gamehaven", 267, 10);

        // Laundry Day
        Story.KillQuest(942, "gamehaven", "Evil Arcade Machine");

        // Strategy Guides are Key
        Story.KillQuest(943, "gamehaven", "Evil Console Machine|Evil Arcade Machine");

        // Investigate the Storage Room
        Story.MapItemQuest(944, "gamehaven", 269);

        // Inventory In Your Inventory
        Story.MapItemQuest(945, "warehouse", 270, 10);

        // Sneevil Sabotage
        Story.KillQuest(946, "warehouse", "Delivery Sneevil");

        // A Bribe for the Bride
        Story.KillQuest(947, "warehouse", "Snapper Shrub");

        // Hey There Lie-lah
        Story.MapItemQuest(948, "warehouse", 272);

        // Fuel for Fought
        Story.MapItemQuest(950, "arcadion", 271, 8);

        // In Ctrl of Controllers
        Story.KillQuest(951, "arcadion", "Megadude");

        // To Form a Platform
        Story.KillQuest(952, "arcadion", "Blue Hedgehog");

        // The Infamous Plumber
        Story.KillQuest(953, "arcadion", "Orc Plumber");
    }
}
