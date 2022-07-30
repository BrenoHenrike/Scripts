//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class SkyPirateQuests
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (!Core.IsMember)
            return;

        if (Core.isCompletedBefore(1290))
            return;

        // Fiends in High Places
        Story.KillQuest(883, "airship", "Sky Pirate Draconian");

        // A Whirled Wide Traveler
        if (!Story.QuestProgression(884))
        {
            Story.MapItemQuest(884, "airship", 217, 10, AutoCompleteQuest: false);
            Core.ChainComplete(889);
            Story.ChainQuest(884);
        }

        // Boiler Spoiler
        Story.KillQuest(885, "airship", "Rehydrated Gell Oh No");

        // This Fight Will Dragon
        Story.KillQuest(886, "airship", "Sky Pirate Dragon");

        // Things Are Looking Up
        Story.KillQuest(887, "airship", new[] { "Sky Pirate Draconian", "Rehydrated Gell Oh No", "Sky Pirate Dragon" });

        // Don't Get Mad, Get Gladius            
        Story.KillQuest(888, "airship", "Gladius");

        // School's Out for the Invasion 1038
        Story.MapItemQuest(1038, "academy", 399);

        // Chaobold Bullies 1039
        Story.MapItemQuest(1039, "academy", 400, 5);
        Story.KillQuest(1039, "academy", new[] { "Chaobold", "Bronze Sky Pirate" });

        // Trip the Traps 1040
        Story.MapItemQuest(1040, "academy", 401, 15);

        // Wreck the Warder 1041       
        Story.KillQuest(1041, "academy", "Inbunche");

        // Banditing Together  
        Story.KillQuest(1104, "anders", new[] { "Dravir", "Copper Sky Pirate" });

        // Booty Becomes Barrier
        Story.MapItemQuest(1105, "anders", 439, 10);
        Story.MapItemQuest(1105, "anders", 440, 2);
        Story.KillQuest(1105, "anders", new[] { "Dravir", "Copper Sky Pirate" });

        // We Didn't Start The Fire (Oh, Wait...)
        Story.MapItemQuest(1106, "anders", 441);
        Story.KillQuest(1106, "anders", new[] { "Copper Sky Pirate", "Copper Sky Pirate", "Dravir" });

        // Granny's Final Request   
        Story.KillQuest(1107, "anders", "Iron Hoof");

        // Sweet Dreamlands are Made Like This 1215
        Story.MapItemQuest(1215, "Dreammaze", 519);

        // Through the Gates of the Silver Portal
        Story.KillQuest(1222, "Dreammaze", "Nightmare Lieutenant");

        // SkyPirates Slaying Strategies
        Story.KillQuest(1286, "strategy", "Dravir Pirate");

        // Strategic Alarm Sequence
        Story.MapItemQuest(1287, "strategy", 558);

        // SkyPirate Shot-caller Neutralized
        Story.KillQuest(1289, "strategy", "Dravir Pirate Captain");

        // SkyPirate Map Hunt    
        Story.KillQuest(1290, "strategy", new[] { "Dravir Pirate", "Dravir Pirate", "Dravir Pirate", "Dravir Pirate" });

        // SkyShip Chase Scene
        Story.MapItemQuest(1288, "strategy", 559);

    }
}
