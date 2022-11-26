//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class SkyGuardSaga
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        if (!Core.IsMember)
            return;

        if (Core.isCompletedBefore(2488))
            return;

        AirShip();
        Academy();
        Anders();
        DreamMaze();
        Strategy();
        PirateBase();
        HighCommand();
        Bunker();
        ChaosGuard();
    }

    public void AirShip()
    {
        if (Core.isCompletedBefore(888))
            return;

        Story.PreLoad(this);

        // Fiends in High Places 883
        Story.KillQuest(883, "airship", "Sky Pirate Draconian");

        // A Whirled Wide Traveler 884
        if (!Story.QuestProgression(884))
        {
            Story.MapItemQuest(884, "airship", 217, 10, AutoCompleteQuest: false);
            Core.ChainComplete(889);
            Story.ChainQuest(884);
        }

        // Boiler Spoiler 885
        Story.KillQuest(885, "airship", "Rehydrated Gell Oh No");

        // This Fight Will Dragon 886
        Story.KillQuest(886, "airship", "Sky Pirate Dragon");

        // Things Are Looking Up 887
        Story.KillQuest(887, "airship", new[] { "Sky Pirate Draconian", "Rehydrated Gell Oh No", "Sky Pirate Dragon" });

        // Don't Get Mad, Get Gladius 888
        Story.KillQuest(888, "airship", "Gladius");
    }

    public void Academy()
    {
        if (Core.isCompletedBefore(1041))
            return;

        Story.PreLoad(this);

        // School's Out for the Invasion 1038
        Story.MapItemQuest(1038, "academy", 399);

        // Chaobold Bullies 1039
        Story.MapItemQuest(1039, "academy", 400, 5);
        Story.KillQuest(1039, "academy", new[] { "Chaobold", "Bronze Sky Pirate" });

        // Trip the Traps 1040
        Story.MapItemQuest(1040, "academy", 401, 15);

        // Wreck the Warder 1041
        Story.KillQuest(1041, "academy", "Inbunche");
    }

    public void Anders()
    {
        if (Core.isCompletedBefore(1107))
            return;

        Story.PreLoad(this);

        // Banditing Together 1104
        Story.KillQuest(1104, "anders", new[] { "Dravir", "Copper Sky Pirate" });

        // Booty Becomes Barrier 1105
        Story.MapItemQuest(1105, "anders", 439, 10);
        Story.MapItemQuest(1105, "anders", 440, 2);
        Story.KillQuest(1105, "anders", new[] { "Dravir", "Copper Sky Pirate" });

        // We Didn't Start The Fire (Oh, Wait...) 1106
        Story.MapItemQuest(1106, "anders", 441);
        Story.KillQuest(1106, "anders", new[] { "Copper Sky Pirate", "Copper Sky Pirate", "Dravir" });

        // Granny's Final Request 1107
        Story.KillQuest(1107, "anders", "Iron Hoof");
    }

    public void DreamMaze()
    {
        if (Core.isCompletedBefore(1222))
            return;

        // Sweet Dreamlands are Made Like This 1215
        Story.MapItemQuest(1215, "dreammaze", 519);

        // Through the Gates of the Silver Portal 1222
        Story.KillQuest(1222, "dreammaze", "Nightmare Lieutenant");
    }

    public void Strategy()
    {
        if (Core.isCompletedBefore(1290))
            return;

        Story.PreLoad(this);

        // SkyPirates Slaying Strategies 1286
        Story.KillQuest(1286, "strategy", "Dravir Pirate");

        // Strategic Alarm Sequence 1287
        Story.MapItemQuest(1287, "strategy", 558);

        // SkyShip Chase Scene 1288
        Story.MapItemQuest(1288, "strategy", 559);

        // SkyPirate Shot-caller Neutralized 1289
        Story.KillQuest(1289, "strategy", "Dravir Pirate Captain");

        // SkyPirate Map Hunt 1290
        Story.KillQuest(1290, "strategy", new[] { "Dravir Pirate", "Dravir Pirate", "Dravir Pirate", "Dravir Pirate" });
    }

    public void PirateBase()
    {
        if (Core.isCompletedBefore(1700))
            return;

        Story.PreLoad(this);

        // Deserter in the Ranks 1696
        Story.KillQuest(1696, "lair", "Golden Draconian");

        // Secure the Ship 1697
        Story.MapItemQuest(1697, "piratebase", 896, 14);
        Story.MapItemQuest(1697, "piratebase", 895);
        Story.KillQuest(1697, "piratebase", "Security Sky Pirate");

        // Infiltration is a Go 1698
        Story.MapItemQuest(1698, "piratebase", 897, 10);
        Story.KillQuest(1698, "piratebase", "Chaorrupted Sky Pirate");

        // Eluding Capture is not an Option 1699
        Story.KillQuest(1699, "piratebase", "Chaorrupted Sky Pirate");

        // Don't Get Caught Weak in the Knees 1700
        Story.KillQuest(1700, "piratebase", "Chaorrupted SkyGeneral");
    }

    public void HighCommand()
    {
        if (Core.isCompletedBefore(2042))
            return;

        Story.PreLoad(this);

        // Bridge the Gap 2038
        Story.MapItemQuest(2038, "highcommand", 997, 4);
        Story.KillQuest(2038, "highcommand", new[] { "Bronze Sky Pirate", "Storagebox" });

        // Crack the Code 2039
        Story.MapItemQuest(2039, "highcommand", 1000);

        // Comm Module 4dX 2040
        Story.KillQuest(2040, "highcommand", new[] { "Bronze Sky Pirate", "Chaorrupted Invader" });

        // Can You Hear Me Now? 2043
        Story.MapItemQuest(2043, "highcommand", 996, 10);
        Story.MapItemQuest(2043, "highcommand", new[] { 998, 999 });

        // Back to Base-ics 2041
        Story.KillQuest(2041, "piratebase", new[] { "Chaorrupted Sky Pirate", "Chaorrupted Sky Pirate" });

        // Secret Weapon Mecha-Death! 2042
        Story.KillQuest(2042, "highcommand", "M3CH4-D34TH");

    }

    public void Bunker()
    {
        if (Core.isCompletedBefore(2252))
            return;

        Story.PreLoad(this);

        if (!Core.CheckInventory("Bag of Gross Supplies"))
        {
            // Hoof it til it Beaks 2248
            if (!Core.CheckInventory(new[] { "Tanned Skin", "Hootbeak Piercer" }))
            {
                Core.AddDrop("Tanned Skin", "Hootbeak Piercer");
                Core.EnsureAccept(2248);
                Core.GetMapItem(1410, 5, "bunker");
                Core.HuntMonster("bunker", "Subrysa", "Subrysa Skin", 5);
                Core.HuntMonster("bunker", "Hootbear", "Hootbeak", 5);
                Core.EnsureComplete(2248);
            }

            // Bind it with Twine 2249
            if (!Core.CheckInventory(new[] { "Emu Egg Binder", "Medical Twine" }))
            {
                Core.AddDrop("Emu Egg Binder", "Medical Twine");
                Core.EnsureAccept(2249);
                Core.GetMapItem(1411, 7, "bunker");
                Core.GetMapItem(1412, 3, "bunker");
                Core.HuntMonster("bunker", "Tainted Emu", "Emu Spine", 3);
                Core.HuntMonster("bunker", "Koalion", "Koalion Claw", 6);
                Core.EnsureComplete(2249);
            }

            // Hold it Together 2250
            if (!Core.CheckInventory("Rhison Glue"))
            {
                Core.AddDrop("Rhison Glue");
                Core.EnsureAccept(2250);
                Core.HuntMonster("bloodtusk", "Rhison", "Rhison Hoof", 10);
                Core.EnsureComplete(2250);
            }

            Core.BuyItem("bunker", 467, "Bag of Gross Supplies");
        }

        // Build a Beast! 2251
        if (!Story.QuestProgression(2251))
        {
            Core.EnsureAccept(2251);
            Core.HuntMonster("palace", "Subrysa", "Subrysa Corpse", 3);
            Core.HuntMonster("bunker", "Koalion", "Koalion Corpse", 3);
            Core.HuntMonster("bunker", "Tainted Emu", "Tainted Emu Corpse", 3);
            Core.HuntMonster("bunker", "Hootbear", "Hootbear Corpse", 3);
            Core.EnsureComplete(2251);
        }

        // Break a Beast! 2252
        Story.KillQuest(2252, "bunker", "Chaos Beast Attempt");
    }

    public void ChaosGuard()
    {
        if (Core.isCompletedBefore(2488))
            return;

        Story.PreLoad(this);

        // Break the Chains 2484
        if (!Core.CheckInventory("All the Guards Defeated"))
        {
            Core.EnsureAccept(2484);
            Core.HuntMonster("chaosguard", "Chaos Skylord", "Defeated SkyLord", 2);
            Core.HuntMonster("chaosguard", "Chaos Commander", "Defeated Commander", 5);
            Core.EnsureComplete(2484);
        }

        // Eye Spy Victory! 2486
        if (!Core.CheckInventory("No Spy-eyes on me"))
        {
            Core.EnsureAccept(2486);
            Core.HuntMonster("chaosguard", "Sky Spy-Eye", "Defeated Sky Spy-Eye", 8);
            Core.EnsureComplete(2486);
        }

        // Discover the Passage 2489
        if (!Core.CheckInventory("Explore the passage"))
        {
            Core.EnsureAccept(2489);
            Core.GetMapItem(1553, 1, "chaosguard");
            Core.HuntMonster("chaosguard", "Chaos Skywatcher", "SkyWatcher Defeated");
            Core.EnsureComplete(2489);
        }

        // Master Exos' Keep 2482
        Story.ChainQuest(2482);

        // Defeat Exos 2487
        Story.KillQuest(2487, "chaosguard", "Exos");
    }
}
