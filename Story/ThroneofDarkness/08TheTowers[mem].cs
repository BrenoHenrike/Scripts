//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class TheTowersQuests
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }
    public void DoAll()
    {
        StoryLineSilver();
        StoryLineGold();
    }

    public void StoryLineSilver()
    {
        if (!Core.IsMember)
            return;

        if (Core.isCompletedBefore(5008))
            return;

        // Eye Sp-eye
        Story.KillQuest(4996, "towersilver", "Flying Spyball");

        // Stone Cold
        Story.KillQuest(4997, "towersilver", "Fallen Emperor Statue");

        // Slay 'Em All
        Story.KillQuest(4999, "towersilver", new[] { "Undead Knight", "Undead Guard" });

        // Slay 'Em All Again
        Story.KillQuest(4998, "towersilver", new[] { "Fallen DeathKnight", "Undead Warrior" });

        // Farming For Loot
        Story.KillQuest(5000, "towersilver", new[] { "Flying Spyball", "Fallen DeathKnight", "Undead Warrior", "Undead Knight", "Undead Guard" });

        // Or... Not.
        Story.MapItemQuest(5001, "towersilver", new[] { 4368, 4369, 4370, 4371, 4372, });

        // Mirror, Mirror
        Story.MapItemQuest(5002, "towersilver", 437, 3);

        // Bloody Scary
        Story.KillQuest(5003, "towersilver", "Bloody Scary");

        // Just Nasty
        Story.KillQuest(5004, "towersilver", "Bone Creeper");

        // Scavenger Hunt
        Story.KillQuest(5005, "towersilver", "Ghoul");

        // Ghoul Booty
        Story.MapItemQuest(5006, "towersilver", 4374, 5);

        // Flester's Guards
        Story.KillQuest(5007, "towersilver", "Undead Golden Knight");

        // Flester the Silver
        Story.KillQuest(5008, "towersilver", "Flester The Silver");

        // Get my Stuff
        Story.KillQuest(5008, "towersilver", new[] { "Fallen DeathKnight", "Undead Knight", "Undead Warrior", "Ghoul", "Undead Guard" });

        // In the Mix        
        Story.KillQuest(5008, "towersilver", "Bloody Scary");

    }

    public void StoryLineGold()
    {
        if (Core.isCompletedBefore(5022))
            return;

        // They Know We're Coming
        Story.KillQuest(5011, "towergold", "Grim Souldier");

        // Elitist Jerks
        Story.KillQuest(5012, "towergold", "Undead Golden Knight");

        // Loot, Hero!
        Story.MapItemQuest(5013, "towergold", 4375, 5);
        Story.KillQuest(5013, "towergold", "Skullspider");

        // Those Aren't Big Birds, Sweetheart
        Story.KillQuest(5014, "towergold", "Vampire Bat");

        // The Nope Room
        Story.KillQuest(5015, "towergold", "Webbed Ghoul");

        // Arach-NO-phobia
        Story.KillQuest(5016, "towergold", "Bone Widow");

        // Library Infestation
        Story.KillQuest(5017, "towergold", "Book Maggot");

        // Creepy!
        Story.MapItemQuest(5018, "towergold", 4376, 6);
        Story.KillQuest(5018, "towergold", "Bone Creeper");

        // It's Not Like They Need Them Anymore
        Story.MapItemQuest(5019, "towergold", 4377, 3);

        // Advanced Self Defense
        Story.KillQuest(5020, "towergold", new[] { "Undead Knight", "Undead Guard" });

        // Take A Mallet To 'Em
        Story.KillQuest(5021, "towergold", "Fallen Emperor Statue");

        // Yurrod the Gold       
        Story.KillQuest(5022, "towergold", "Yurrod the Gold");
    }
}
