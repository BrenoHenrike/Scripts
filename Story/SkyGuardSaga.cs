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
    }

    public void DreamMaze()
    {
        if (Core.isCompletedBefore(1222))
            return;

        // Sweet Dreamlands are Made Like This 1215
        Story.MapItemQuest(1215, "Dreammaze", 519);

        // Through the Gates of the Silver Portal
        Story.KillQuest(1222, "Dreammaze", "Nightmare Lieutenant");
    }

    public void Strategy()
    {
        if (Core.isCompletedBefore(1290))
            return;

        Story.PreLoad(this);

        // SkyPirates Slaying Strategies
        Story.KillQuest(1286, "strategy", "Dravir Pirate");

        // Strategic Alarm Sequence
        Story.MapItemQuest(1287, "strategy", 558);

        // SkyShip Chase Scene
        Story.MapItemQuest(1288, "strategy", 559);

        // SkyPirate Shot-caller Neutralized
        Story.KillQuest(1289, "strategy", "Dravir Pirate Captain");

        // SkyPirate Map Hunt    
        Story.KillQuest(1290, "strategy", new[] { "Dravir Pirate", "Dravir Pirate", "Dravir Pirate", "Dravir Pirate" });


    }

    public void PirateBase()
    {
        if (Core.isCompletedBefore(1700))
            return;

        Story.PreLoad(this);

        //Deserter in the Ranks 1696
        Story.KillQuest(1696, "Lair", "Golden Draconian");

        //Secure the Ship 1697
        Story.MapItemQuest(1697, "PirateBase", 896, 14);
        Story.MapItemQuest(1697, "PirateBase", 895);
        Story.KillQuest(1697, "PirateBase", "Security Sky Pirate");

        //Infiltration is a Go 1698
        Story.MapItemQuest(1698, "PirateBase", 897, 10);
        Story.KillQuest(1698, "PirateBase", "Chaorrupted Sky Pirate");

        //Eluding Capture is not an Option 1699
        Story.KillQuest(1699, "PirateBase", "Chaorrupted Sky Pirate");

        //Don't Get Caught Weak in the Knees 1700
        Story.KillQuest(1700, "PirateBase", "Chaorrupted SkyGeneral");
    }

    public void HighCommand()
    {
        if (Core.isCompletedBefore(2042))
            return;

        Story.PreLoad(this);

        //Bridge the Gap 2038
        Story.MapItemQuest(2038, "HighCommand", 997, 4);
        Story.KillQuest(2038, "HighCommand", new[] { "Bronze Sky Pirate", "Storagebox" });

        //Crack the Code 2039
        Story.MapItemQuest(2039, "HighCommand", 1000);

        //Comm Module 4dX 2040
        Story.KillQuest(2040, "HighCommand", new[] { "Bronze Sky Pirate", "Chaorrupted Invader" });

        //Can You Hear Me Now? 2043
        Story.MapItemQuest(2043, "HighCommand", 996, 10);
        Story.MapItemQuest(2043, "HighCommand", new[] { 998, 999 });

        //Back to Base-ics 2041
        Story.KillQuest(2041, "Piratebase", new[] { "Chaorrupted Sky Pirate", "Chaorrupted Sky Pirate" });

        //Secret Weapon Mecha-Death! 2042
        Story.KillQuest(2042, "HighCommand", "M3CH4-D34TH");

    }

    public void Bunker()
    {
        if (Core.isCompletedBefore(2252))
            return;

        Story.PreLoad(this);

        if (!Core.CheckInventory("Bag of Gross Supplies"))
        {

            //Hoof it til it Beaks 2248
            if (!Core.CheckInventory(new[] { "Tanned Skin", "Hootbeak Piercer" }))
            {
                Core.AddDrop("Tanned Skin", "Hootbeak Piercer");
                Core.EnsureAccept(2248);
                Core.GetMapItem(1410, 5, "Bunker");
                Core.HuntMonster("Bunker", "Subrysa", "Subrysa Skin", 5);
                Core.HuntMonster("Bunker", "Hootbear", "Hootbeak", 5);
                Core.EnsureComplete(2248);
            }

            //Bind it with Twine 2249
            if (!Core.CheckInventory(new[] { "Emu Egg Binder", "Medical Twine" }))
            {
                Core.AddDrop("Emu Egg Binder", "Medical Twine");
                Core.EnsureAccept(2249);
                Core.GetMapItem(1411, 7, "Bunker");
                Core.GetMapItem(1412, 3, "Bunker");
                Core.HuntMonster("Bunker", "Tainted Emu", "Emu Spine", 3);
                Core.HuntMonster("Bunker", "Koalion", "Koalion Claw", 6);
                Core.EnsureComplete(2249);
            }

            //Hold it Together 2250
            if (!Core.CheckInventory("Rhison Glue"))
            {
                Core.AddDrop("Rhison Glue");
                Core.EnsureAccept(2250);
                Core.HuntMonster("bloodtusk", "Rhison", "Rhison Hooves", 10);
                Core.EnsureComplete(2250);
            }

            Core.BuyItem("Bunker", 467, "Bag of Gross Supplies");
        }

        //Build a Beast! 2251
        if (!Story.QuestProgression(2251))
        {
            Core.EnsureAccept(2251);
            Core.HuntMonster("Palace", "Subrysa", "Subrysa Corpse", 3);
            Core.HuntMonster("Bunker", "Koalion", "Koalion Corpse", 3);
            Core.HuntMonster("Bunker", "Tainted Emu", "Tainted Emu Corpse", 3);
            Core.HuntMonster("Bunker", "Hootbear", "Hootbear Corpse", 3);
            Core.EnsureComplete(2251);
        }

        //Break a Beast! 2252
        Story.KillQuest(2252, "Bunker", "Chaos Beast Attempt");

    }

    public void ChaosGuard()
    {
        if (Core.isCompletedBefore(2488))
            return;

        Story.PreLoad(this);

        //Break the Chains 2484
        if (!Core.CheckInventory("All the Guards Defeated"))
        {
            Core.EnsureAccept(2484);
            Core.HuntMonster("ChaosGuard", "Chaos Skylord", "Defeated SkyLord", 2);
            Core.HuntMonster("ChaosGuard", "Chaos Commander", "Defeated Commander", 5);
            Core.EnsureComplete(2484);
        }

        //Eye Spy Victory! 2486
        if (!Core.CheckInventory("No Spy-eyes on me"))
        {
            Core.EnsureAccept(2486);
            Core.HuntMonster("ChaosGuard", "Sky Spy-Eye", "Defeated Sky Spy-Eye", 8);
            Core.EnsureComplete(2486);
        }

        //Discover the Passage 2489
        if (!Core.CheckInventory("Explore the passage"))
        {
            Core.EnsureAccept(2489);
            Core.GetMapItem(1553, 1, "ChaosGuard");
            Core.HuntMonster("ChaosGuard", "Chaos Skywatcher", "SkyWatcher Defeated");
            Core.EnsureComplete(2489);
        }

        //Master Exos' Keep 2482
        Story.ChainQuest(2482);

        //Defeat Exos 2487
        Story.KillQuest(2487, "ChaosGuard", "Exos");

    }
}
