/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CtrlAltDelMemberBadge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (!Core.IsMember || Core.isCompletedBefore(953))
            return;

        // Map - GameHavaen:
        Core.EquipClass(ClassType.Farm);

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

        // Map - WareHouse: 

        // Inventory In Your Inventory
        Story.MapItemQuest(945, "WareHouse", 270, 10);

        // Sneevil Sabotage
        Story.KillQuest(946, "WareHouse", "Delivery Sneevil");

        // A Bribe for the Bride
        Story.KillQuest(947, "WareHouse", "Snapper Shrub");

        // Hey There Lie-lah
        Story.MapItemQuest(948, "WareHouse", 272);


        // Map - Arcadion:
        Core.EquipClass(ClassType.Solo);

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
