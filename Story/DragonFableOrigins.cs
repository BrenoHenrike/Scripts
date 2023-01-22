/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DragonFableOrigins
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DragonFableOriginsAll();

        Core.SetOptions(false);
    }

    public void DragonFableOriginsAll()
    {
        GreatFireWar();
        NorthMountain();
        CharredPlains();
        Drakonnan();
    }

    public void GreatFireWar()
    {
        if (Core.isCompletedBefore(6300))
            return;

        Story.PreLoad(this);

        // Fire Dragon Scales
        Story.KillQuest(6294, "firewar", "Fire Dragon");

        // Fire Dragon Hearts
        Story.KillQuest(6295, "firewar", "Fire Dragon");

        // Pyritium Shards
        if (!Core.isCompletedBefore(6296))
        {
            Core.EnsureAccept(6296);
            Core.KillMonster("firewar", "r8", "Right", "Inferno Dragon", "Pyritium Shards", 5);
            Core.EnsureComplete(6296);
        }

        // Perfect Pyritium
        if (!Core.isCompletedBefore(6297))
        {
            Core.EnsureAccept(6296);
            Core.KillMonster("firewar", "r8", "Right", "Inferno Dragon", "Perfect Pyritium");
            Core.EnsureComplete(6297);
        }
        Core.EquipClass(ClassType.Solo);

        // Uriax, Inferno of Akriloth
        Story.KillQuest(6298, "firewar", "Uriax");

        // Confront Akriloth
        Story.KillQuest(6299, "firewar", "Akriloth");

        // Clear out the Dragons
        if (!Core.isCompletedBefore(6300))
        {
            Core.EnsureAccept(6300);
            Core.HuntMonster("firewar", "Fire Dragon", "Fire Dragon Slain", 3);
            Core.KillMonster("firewar", "r8", "Left", "Inferno Dragon", "Inferno Dragon Slain", 2);
            Core.EnsureComplete(6300);
        }
    }

    public void NorthMountain()
    {
        if (Core.isCompletedBefore(6307))
            return;

        Story.PreLoad(this);

        // Getting a Feel for the Area
        if (!Story.QuestProgression(6301))
        {
            Core.EnsureAccept(6301);
            Core.HuntMonster("northmountain", "Ice Elemental", "Monsters Defeated", 8);
            Bot.Map.GetMapItem(5812);
            Core.EnsureComplete(6301);
        }

        // Finding the First Rune
        Story.KillQuest(6302, "northmountain", "Ice Elemental");

        // A Slippery Slope
        Story.KillQuest(6303, "northmountain", "Ursice Savage");

        // A 100% Chance of Hail
        Story.KillQuest(6304, "northmountain", "Ice Spitter");

        // The Final Rune
        Story.KillQuest(6305, "northmountain", "Ice Elemental");

        // Unlock the Cave
        Story.MapItemQuest(6306, "northmountain", 5813, 4);
        Story.MapItemQuest(6306, "northmountain", 5814);

        // The Guardian
        Story.KillQuest(6307, "northmountain", "Izotz");
    }

    public void CharredPlains()
    {
        if (Core.isCompletedBefore(6311))
            return;

        Story.PreLoad(this);

        // The Fires of War
        Story.KillQuest(6308, "charredplains", "Fire Dragon");

        // Meet Up
        Story.MapItemQuest(6309, "charredplains", 5815);

        // Akriloth
        Story.KillQuest(6310, "charredplains", "Akriloth");

        // Enchanted Ice Shards
        Story.KillQuest(6311, "northmountain", "Izotz");
    }

    public void Drakonnan()
    {
        if (Core.isCompletedBefore(6325))
            return;

        Story.PreLoad(this);

        // Gathering the Pieces
        Story.MapItemQuest(6312, "drakonnan", 5827, 5);

        // This Weapon Scales
        Story.KillQuest(6313, "drakonnan", "Fire Dragon");

        // Reinforced Lava Steel
        Story.KillQuest(6314, "drakonnan", "Living Lava");

        // Claymore Shards
        Story.KillQuest(6315, "drakonnan", "Fire Dragon");

        // Keeping Cool
        Story.KillQuest(6316, "northmountain", "Ice Elemental");

        // Cryogenic Decacrystals
        Story.MapItemQuest(6317, "northmountain", 5826, 6);

        // Fiamme's Blessing
        Story.KillQuest(6318, "drakonnan", "Fire Elemental");

        // Inferno Heart
        Story.KillQuest(6319, "drakonnan", "Living Fire");

        // Dragons!
        Story.KillQuest(6320, "drakonnan", "Fire Dragon");

        // No Lava For My Enemies
        Story.KillQuest(6321, "drakonnan", "Living Lava");

        // Clearing Out the Fodder
        Story.KillQuest(6322, "drakonnan", "Fire Elemental");

        // Unlocking the Door
        Story.KillQuest(6323, "drakonnan", "Fire Elemental");

        // Drakonnan
        Story.KillQuest(6324, "drakonnan", "Drakonnan");

        // Ultra Drakonnan
        Story.KillQuest(6325, "drakonnan", "Ultra Drakonnan");
    }
}
