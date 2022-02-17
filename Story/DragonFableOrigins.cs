//cs_include Scripts/CoreBots.cs
using RBot;

public class DragonFableOrigins
{
    public CoreBots Core => CoreBots.Instance;
    public ScriptInterface Bot => ScriptInterface.Instance;

    public void ScriptMain(ScriptInterface Bot)
    {
        Core.AcceptandCompleteTries = 5;
        Core.SetOptions();

        DragonFableOriginsAll();

        Core.SetOptions(false);
    }

    public void DragonFableOriginsAll()
    {
        Core.EquipClass(ClassType.Solo);

        GreatFireWar();
        NorthMountain();
        CharredPlains();
        Drakonnan();
    }

    public void GreatFireWar()
    {
        if (Core.isCompletedBefore(6300))
            return;

        // Fire Dragon Scales
        if (!Core.QuestProgression(6294))
        {
            Core.EnsureAccept(6294);
            Core.KillMonster("firewar", "r7", "Left", "Fire Dragon", "Fire Dragon Scale", 5);
            Core.EnsureComplete(6294);
        }

        // Fire Dragon Hearts
        if (!Core.QuestProgression(6295))
        {
            Core.EnsureAccept(6295);
            Core.KillMonster("firewar", "r7", "Left", "Fire Dragon", "Fire Dragon Heart", 3);
            Core.EnsureComplete(6295);
        }

        // Pyritium Shards
        if (!Core.QuestProgression(6296))
        {
            Core.EnsureAccept(6296);
            Core.KillMonster("firewar", "r7", "Left", "Inferno Dragon", "Pyritium Shards", 5);
            Core.EnsureComplete(6296);
        }

        // Perfect Pyritium
        if (!Core.QuestProgression(6297))
        {
            Core.EnsureAccept(6297);
            Core.KillMonster("firewar", "r7", "Left", "Inferno Dragon", "Perfect Pyritium", 1);
            Core.EnsureComplete(6297);
        }

        // Uriax, Inferno of Akriloth
        Core.KillQuest(6298, "firewar", "Uriax");

        // Confront Akriloth
        Core.KillQuest(6299, "firewar", "Akriloth");

        //Clear out the Dragons
        if (!Core.QuestProgression(6300))
        {
            Core.EnsureAccept(6300);
            Core.KillMonster("firewar", "r7", "Left", "Fire Dragon", "Fire Dragon Slain", 3);
            Core.KillMonster("firewar", "r7", "Left", "Inferno Dragon", "Inferno Dragon Slain", 2);
            Core.EnsureComplete(6300);
        }
    }
    public void NorthMountain()
    {
        if (Core.isCompletedBefore(6307))
            return;

        // Getting a Feel for the Area
        Core.MapItemQuest(6301, "northmountain", 5812, 1);
        Core.KillQuest(6301, "northmountain", new[] { "Ice Elemental|Ice Spitter|Ursice Savage" });

        // Finding the First Rune
        Core.KillQuest(6302, "northmountain", "Ice Elemental");

        // A Slippery Slope
        Core.KillQuest(6303, "northmountain", "Ursice Savage");

        // A 100% Chance of Hail
        Core.KillQuest(6304, "northmountain", "Ice Spitter");

        // The Final Rune
        Core.KillQuest(6305, "northmountain", new[] { "Ice Elemental|Ice Spitter|Ursice Savage" });

        // Unlock the Cave
        Core.MapItemQuest(6306, "northmountain", 5813, 4);
        Core.MapItemQuest(6306, "northmountain", 5814, 1);

        // The Guardian
        Core.KillQuest(6307, "northmountain", "Izotz");
    }
    public void CharredPlains()
    {
        if (Core.isCompletedBefore(6311))
            return;

        // The Fires of War
        Core.KillQuest(6308, "charredplains", new[] { "Fire Dragon|Fire Drakel|Inferno Dragon" });

        // Meet Up
        Core.MapItemQuest(6309, "charredplains", 5815, 1);

        // Akriloth
        Core.KillQuest(6310, "charredplains", "Akriloth");

        // Enchanted Ice Shards
        Core.KillQuest(6311, "northmountain", "Izotz");
    }
    public void Drakonnan()
    {
        if (Core.isCompletedBefore(6325))
            return;

        // Gathering the Pieces
        Core.MapItemQuest(6312, "drakonnan", 5827, 5);

        // This Weapon Scales
        Core.KillQuest(6313, "drakonnan", "Fire Dragon");

        // Reinforced Lava Steel
        Core.KillQuest(6314, "drakonnan", "Living Lava");

        // Claymore Shards
        Core.KillQuest(6315, "drakonnan", new[] { "Fire Dragon|Fire Drakel|Inferno Dragon" });

        // Keeping Cool
        Core.KillQuest(6316, "northmountain", new[] { "Ice Elemental|Ice Spitter|Ursice Savage" });

        // Cryogenic Decacrystals
        Core.MapItemQuest(6317, "northmountain", 5826, 6);

        // Fiamme's Blessing
        Core.KillQuest(6318, "drakonnan", "Fire Elemental");

        // Inferno Heart
        Core.KillQuest(6319, "drakonnan", "Living Fire");

        // Dragons!
        Core.KillQuest(6320, "drakonnan", "Fire Dragon");

        // No Lava For My Enemies
        Core.KillQuest(6321, "drakonnan", "Living Lava");

        // Clearing Out the Fodder
        Core.KillQuest(6322, "drakonnan", new[] { "Fire Elemental|Living Fire|Living Lava" });

        // Unlocking the Door
        Core.KillQuest(6323, "drakonnan", "Fire Elemental");

        // Drakonnan
        Core.KillQuest(6324, "drakonnan", "Drakonnan");

        // Ultra Drakonnan
        Core.KillQuest(6325, "drakonnan", "Ultra Drakonnan");
    }
}
