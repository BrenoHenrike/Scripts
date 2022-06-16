//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class Akriloth
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(6310))
            return;

        Story.PreLoad();

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
        Core.EquipClass(ClassType.Farm);

        // Getting a Feel for the Area
        Story.MapItemQuest(6301, "northmountain", 5812);
        Story.KillQuest(6301, "northmountain", "Ice Elemental");

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

        // The Fires of War
        Story.KillQuest(6308, "charredplains", "Fire Drakel");

        // Meet Up
        Story.MapItemQuest(6309, "charredplains", 5815);

        Core.EquipClass(ClassType.Solo);
        // Akriloth    
        Story.KillQuest(6310, "charredplains", "Akriloth");
    }
}
