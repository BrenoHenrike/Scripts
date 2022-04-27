//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class DragonFableOrigins
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface Bot)
    {
        Core.AcceptandCompleteTries = 5;
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

        Story.PreLoad();

        // Fire Dragon Scales
        if (!Story.QuestProgression(6294))
        {
            Core.EnsureAccept(6294);
            Core.KillMonster("firewar", "r7", "Left", "Fire Dragon", "Fire Dragon Scale", 5);
            Core.EnsureComplete(6294);
        }

        // Fire Dragon Hearts
        if (!Story.QuestProgression(6295))
        {
            Core.EnsureAccept(6295);
            Core.KillMonster("firewar", "r7", "Left", "Fire Dragon", "Fire Dragon Heart", 3);
            Core.EnsureComplete(6295);
        }

        // Pyritium Shards
        if (!Story.QuestProgression(6296))
        {
            Core.EnsureAccept(6296);
            Core.KillMonster("firewar", "r7", "Left", "Inferno Dragon", "Pyritium Shards", 5);
            Core.EnsureComplete(6296);
        }

        // Perfect Pyritium
        if (!Story.QuestProgression(6297))
        {
            Core.EnsureAccept(6297);
            Core.KillMonster("firewar", "r7", "Left", "Inferno Dragon", "Perfect Pyritium");
            Core.EnsureComplete(6297);
        }

        // Uriax, Inferno of Akriloth
        Story.KillQuest(6298, "firewar", "Uriax");

        // Confront Akriloth
        Story.KillQuest(6299, "firewar", "Akriloth");

        //Clear out the Dragons
        if (!Story.QuestProgression(6300))
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

        Story.PreLoad();

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

        Story.PreLoad();

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

        Story.PreLoad();

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
