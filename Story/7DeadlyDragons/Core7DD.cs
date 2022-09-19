//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Core7DD
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    bool doAll = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void Complete7DD()
    {
        int[] questIDs = { 5915, 5926, 5943, 5960, 5976, 6000, 6120 };
        Core.EnsureLoad(questIDs);
        if (questIDs.All(qID => Core.isCompletedBefore(qID)))
            return;

        Story.PreLoad(this);
        doAll = true;

        Gluttony();
        Pride();
        Greed();
        Sloth();
        Lust();
        Envy();
        Wrath();
    }

    public void Gluttony()
    {
        if (Core.isCompletedBefore(5915))
            return;

        if (doAll)
            Core.Logger("7 Deadly Dragons - Gluttony");
        Story.PreLoad(this);

        // Guts for Glutes
        Story.KillQuest(5903, "gluttony", "Glutus");

        // Mucus be Joking
        Story.KillQuest(5904, "gluttony", "Mucus");

        // Build a Bone-a-fied Ladder
        Story.MapItemQuest(5905, "gluttony", 5346, 2);
        Story.KillQuest(5905, "gluttony", "Skeletal Slayer");

        // Talk About Reflux
        Story.MapItemQuest(5906, "gluttony", 5344, 1);

        // How'd HE get here?
        Story.MapItemQuest(5907, "gluttony", 5345, 1);

        // Glowworms not Glowsticks
        Story.KillQuest(5908, "gluttony", "Bowel Worm");
        Story.MapItemQuest(5908, "gluttony", 5347, 10);

        // Ossification Needed
        if (!Story.QuestProgression(5909))
        {
            Core.EnsureAccept(5909);
            Core.KillMonster("gluttony", "r6", "Left", "Bile", "Falgar's Bones", 9);
            Core.EnsureComplete(5909);
        }

        // Eye Need Bones
        Story.MapItemQuest(5910, "gluttony", 5348, 5);
        Story.KillQuest(5910, "gluttony", "Skeletal Slayer");

        // Bile Burns
        Story.KillQuest(5911, "gluttony", new[] { "Bile", "Bowel Worm" });

        // Find the Chest
        Story.MapItemQuest(5912, "gluttony", 5349, 1);

        // We Need the Key
        Story.KillQuest(5913, "gluttony", "Giant Tapeworm");

        // Cha Cha Cha
        Story.MapItemQuest(5914, "gluttony", 5350, 1);

        // Glutus, Take 2
        Story.KillQuest(5915, "gluttony", "Deflated Glutus");

    }

    public void Pride()
    {
        if (Core.isCompletedBefore(5926))
            return;

        if (doAll)
            Core.Logger("7 Deadly Dragons - Pride");
        Story.PreLoad(this);

        // Defeat the Drakel
        Story.KillQuest(5917, "pride", "Storm Drakel");

        // Don’t Get Zapped!
        Story.KillQuest(5918, "pride", "Ball Lightning");

        // Get Grounded
        Story.KillQuest(5919, "pride", "Rubber Treeant ");

        // Get the Key
        Story.KillQuest(5920, "pride", "Cellar Guard");

        // Get the Boots
        Story.MapItemQuest(5921, "pride", 5351, 1);
        Story.MapItemQuest(5921, "pride", 5352, 6);

        // Make Lightning Rods
        Story.KillQuest(5922, "pride", "Storm Drakel");
        Story.MapItemQuest(5922, "pride", 5353, 8);

        // Free the Villagers
        Story.KillQuest(5923, "pride", "Drakel Guard");
        Story.MapItemQuest(5923, "pride", 5354, 4);

        // Open the Gate
        Story.KillQuest(5924, "pride", "Elite Guard");
        Story.MapItemQuest(5924, "pride", 5355, 1);

        // Fight Your Way Throug
        Story.KillQuest(5925, "pride", "Storm Drakel");
        Story.MapItemQuest(5925, "pride", 5356, 1);

        // Defeat Valsarian
        Story.KillQuest(5926, "pride", "Valsarian");

    }

    public void Greed()
    {
        if (Core.isCompletedBefore(5943))
            return;

        if (doAll)
            Core.Logger("7 Deadly Dragons - Greed");
        Story.PreLoad(this);

        // Looting is for Sneevils
        Story.KillQuest(5934, "greed", "Sneevil Looter");

        // Jumping in Puddles
        Story.MapItemQuest(5935, "greed", 5372, 1);

        // Pick the Right Chest
        Story.MapItemQuest(5936, "greed", 5373, 1);

        // Gelatin is Nasty
        Story.KillQuest(5937, "greed", "Jelly-Like Cube");

        // Explore the Cave
        Story.MapItemQuest(5938, "greed", 5374, 5);

        // Disarm the Trap
        Story.MapItemQuest(5939, "greed", 5377, 1);
        Story.MapItemQuest(5939, "greed", 5378, 1);

        // Crystal-eyes
        Story.KillQuest(5940, "greed", new[] { "Ice Crystal", "Glacial Horror" });

        // Go-bolds?
        Story.KillQuest(5941, "greed", "Kobold");
        Story.MapItemQuest(5941, "greed", 5375, 5);

        // Follow the Trail
        Story.MapItemQuest(5942, "greed", 5376, 3);

        // Get Rid of Greed
        Story.KillQuest(5943, "greed", "Goregold");

    }

    public void Sloth()
    {
        if (Core.isCompletedBefore(5960))
            return;

        if (doAll)
            Core.Logger("7 Deadly Dragons - Sloth");
        Story.PreLoad(this);

        HazMatSuit();

        // Are There Any Survivors?
        Story.KillQuest(5945, "sloth", "Plague Zombie");
        Story.MapItemQuest(5945, "sloth", 5382, 1);

        // Gathering Samples
        Story.KillQuest(5946, "sloth", new[] { "Snotgoblin", "Wandering Plague" });

        // Find a ‘Volunteer’
        Story.KillQuest(5947, "sloth", "Plague Zombie");

        // Cure the Volunteer
        Story.MapItemQuest(5948, "sloth", 5387, 1);

        // Herbal Help
        Story.KillQuest(5949, "sloth", "Marsh Thing");

        // Re-heal
        Story.MapItemQuest(5950, "sloth", 5383, 8);

        // Let's Try That Again
        Story.MapItemQuest(5951, "sloth", 5389, 1);

        // One More Time
        if (!Story.QuestProgression(5952))
        {
            Core.EnsureAccept(5952);
            Core.BuyItem("dragonhame", 865, "Airther Vitae");
            Core.Join("arcangrove", "Potion", "Right");
            Bot.Shops.Load(211);
            Bot.Shops.BuyItem("Health Potion");
            Core.EnsureComplete(5952);
        }

        // Who’s Up for Round 3
        Story.MapItemQuest(5953, "sloth", 5391, 1);

        // Cure the Villagers
        Story.KillQuest(5954, "sloth", "Plague Zombie");

        // Gotta Clean Up
        Story.MapItemQuest(5955, "sloth", 5384, 10);

        // Clear the Castellum
        Story.MapItemQuest(5956, "sloth", 5385, 1);
        Story.KillQuest(5956, "sloth", "SnotGoblin Prime");

        // Get rid of Phlegnn
        Story.KillQuest(5957, "sloth", "Phlegnn");

        // Cured is NOT GOOD
        Story.KillQuest(5958, "sloth", "Cured Phlegnn");

        // Actual Sloth Dragon
        Story.KillQuest(5960, "sloth", "Actual Sloth Dragon");

        // Mutated Plague
        Story.KillQuest(5959, "sloth", "Mutated Plague");
    }
    public void HazMatSuit()
    {
        if (!Core.CheckInventory(40710))
        {
            Core.Logger("Suit not found");
            Core.Join("Sloth");
            Core.EnsureAccept(5944);
            Core.GetMapItem(5380, 1, "sloth");
            Core.GetMapItem(5381, 1, "sloth");
            Core.EnsureComplete(5944);
            Bot.Wait.ForDrop("Hazmat Suit (Temp)");
            Bot.Send.Packet($"%xt%zm%equipItem%{Bot.Map.RoomID}%40710%");
        }
        else
        {
            Core.JumpWait();
            Bot.Send.Packet($"%xt%zm%equipItem%{Bot.Map.RoomID}%40710%");
        }
    }

    public void Lust()
    {
        if (Core.isCompletedBefore(5976))
            return;

        if (doAll)
            Core.Logger("7 Deadly Dragons - Lust");
        Story.PreLoad(this);

        // Hopelessly Devoted
        Story.KillQuest(5961, "lust", "Devoted Admirer");

        // Fighting the Feeling
        Story.MapItemQuest(5962, "lust", 5405, 1);
        Story.MapItemQuest(5962, "lust", 5406, 1);
        Story.MapItemQuest(5962, "lust", 5407, 1);
        Story.MapItemQuest(5962, "lust", 5408, 1);
        Story.MapItemQuest(5962, "lust", 5409, 1);

        // Love Potion #9.1
        Story.KillQuest(5963, "lust", "Golden Vase");

        // Leather is Better
        Story.KillQuest(5964, "lust", "Enamored Guard");

        // Gird your Loins
        Story.MapItemQuest(5965, "lust", 5410, 1);
        Story.MapItemQuest(5965, "lust", 5411, 1);
        Story.KillQuest(5965, "lust", "Devoted Admirer");

        // Get the Keys
        Story.KillQuest(5966, "lust", "Enamored Guard");

        // Open the Cages
        Story.MapItemQuest(5967, "lust", 5412, 5);

        // Get Past the Guards
        Story.KillQuest(5968, "lust", "Elite Guard");
        Story.MapItemQuest(5968, "lust", 5413, 1);

        // Viscyra’lly Yours
        Story.KillQuest(5969, "lust", "Viscyra");
        Story.MapItemQuest(5969, "lust", 5414, 1);

        // Lascivia’sness
        Story.KillQuest(5970, "lust", "Lascivia");

        // Talk to the Guards
        Story.KillQuest(5971, "lust", "Elite Guard");

        // Lascivia Stunk Pretty
        Story.KillQuest(5972, "lust", new[] { "Golden Vase", "Golden Vase" });

        // You Broke It, You Fix It
        Story.KillQuest(5973, "lust", "Devoted Admirer");

        // Elite Energy
        Story.MapItemQuest(5974, "lust", 5415, 8);
        Story.KillQuest(5974, "lust", "Elite Guard");

        // No Pillow Unturned
        Story.MapItemQuest(5975, "lust", 5416, 1);
        Story.MapItemQuest(5975, "lust", 5417, 1);

        // Take Down Killek
        Story.KillQuest(5976, "lust", "Killek Deadchewer");

    }

    public void Envy()
    {
        if (Core.isCompletedBefore(6005))
            return;

        if (doAll)
            Core.Logger("7 Deadly Dragons - Envy");
        Story.PreLoad(this);

        // Talk to the Guard
        Story.MapItemQuest(5983, "dragoncrown", 5420, 1);

        // Talk to the Villager
        Story.MapItemQuest(5984, "dragoncrown", 5421, 1);

        // Fire and Ice
        Story.MapItemQuest(5985, "dragoncrown", 5422, 6);
        Story.KillQuest(5985, "dragoncrown", "Fire Sprite");

        // Round ‘em Up
        Story.KillQuest(5986, "dragoncrown", new[] { "Llama", "Rampaging Boar" });

        // Get Rockin’
        Story.KillQuest(5987, "dragoncrown", new[] { "Rock Elemental", "Earth Elemental" });

        // Stop Fightin’
        Story.MapItemQuest(5988, "dragoncrown", 5423, 1);

        // Find Riddrug
        Story.MapItemQuest(5989, "dragoncrown", 5424, 1);

        // Defeat The Red Champion
        Story.KillQuest(5990, "dragoncrown", "Torgat");

        // Defeat The Ice Champion
        Story.KillQuest(5991, "dragoncrown", "Fressa");

        // Defeat The Undead Champion
        Story.KillQuest(5992, "dragoncrown", "Radroth");

        // Defeat The Water Champion
        Story.KillQuest(5993, "dragoncrown", "Nizex");

        // Defeat The Wind Champion
        Story.KillQuest(5994, "dragoncrown", "Tathu");

        // Defeat The Yokai Champion
        Story.KillQuest(5995, "dragoncrown", "Lanshen");

        // Defeat The Green Champion
        Story.KillQuest(5996, "dragoncrown", "Ashax");

        // Defeat The Faerie Champion
        Story.KillQuest(5997, "dragoncrown", "Letori");

        // Defeat The Chaos Champion
        Story.KillQuest(5998, "dragoncrown", "Nayzol");

        // Defeat the Void Champion
        Story.KillQuest(5999, "dragoncrown", "Zathas");

        // Argo’s Not Stopping Us!
        Story.KillQuest(6000, "dragoncrown", "Argo");

        //Defeat Ukki, The 1st Sentinel (6001)
        Story.KillQuest(6001, "maloth", "Ukki");

        //Defeat Kagan, The 2nd Guardian (6002)
        Story.KillQuest(6002, "maloth", "Kagan");

        //Defeat Golgar, The 3rd Guardian (6003)
        Story.KillQuest(6003, "maloth", "Golgar");

        //Hand over the Entry Code (6004)
        Story.KillQuest(6004, "maloth", new[] { "Golgar", "Castle Guard", "Scroll Keeper", "Nervous Serf", "Locked Chest" });

        //Confront the Dragon King (6005)
        Story.KillQuest(6005, "maloth", "Maloth");
    }

    public void Wrath()
    {
        if (Core.isCompletedBefore(6170))
            return;

        if (doAll)
            Core.Logger("7 Deadly Dragons - Wrath");
        Story.PreLoad(this);

        // Decimate the Horde
        Story.KillQuest(6110, "wrath", new[] { "Bone Terror", "Fishbones" });

        // Douse with Flames
        Story.KillQuest(6111, "wrath", "Dark Fire");

        // Grenades of AWE
        Story.MapItemQuest(6112, "wrath", 5541, 6);
        Story.KillQuest(6112, "wrath", "Bone Terror");

        // The Final Ingredient
        if (!Story.QuestProgression(6113))
        {
            Core.AddDrop("Holy Wasabi");
            if (!Core.CheckInventory("Holy Wasabi", 1))
            {
                Core.EnsureAccept(1075);
                Core.HuntMonster("doomwood", "Doomwood Ectomancer", "Dried Wasabi Powder", 4, true);
                Core.GetMapItem(428, 1, "lightguard");
                Core.EnsureComplete(1075);
                Bot.Sleep(5000);
            }

            Core.EnsureComplete(6113);
        }

        // Count Not to Four
        Story.MapItemQuest(6114, "wrath", 5542, 6);

        // Talk to Captain Rhubarb
        Story.MapItemQuest(6115, "wrath", 5540, 1);

        // Find the Manifest
        Story.MapItemQuest(6116, "wrath", 5544, 1);
        Story.KillQuest(6116, "wrath", "Undead Pirate");

        // Get the Cargo Hold Key
        Story.KillQuest(6117, "wrath", new[] { "Mutineer", "Mutineer" });

        // Find the Jewel
        Story.MapItemQuest(6118, "wrath", 5545, 1);

        // Defeat Droghor the Screecher!
        Story.KillQuest(6119, "wrath", "Droghor");

        // Defeat Gorhorath!
        Story.KillQuest(6120, "wrath", "Gorgorath");

        //Navigate the Maze (6163)
        Story.MapItemQuest(6163, "dragonbone", 5587);
        Story.KillQuest(6163, "dragonbone", new[] { "Bone Dragonling", "Dark Fire" });

        //Get a Clue (6164)
        Story.KillQuest(6164, "dragonbone", "Bone Terror");

        //Heart of a Dracolich (6165)
        Story.KillQuest(6165, "dragonbone", "Bone Wyvern");

        //Open the Door (6166)
        Story.MapItemQuest(6166, "dragonbone", 5588);

        //Find the Jewel (6167)
        Story.MapItemQuest(6167, "dragonbone", 5589);

        //Get to the Lair (6168)
        Story.MapItemQuest(6168, "dragonbone", 5590);

        //Unlock the Door (6169)
        Story.KillQuest(6169, "dragonbone", "Dragonshade");
        Story.MapItemQuest(6169, "dragonbone", 5591);

        //Slay Gorgorath (6170)
        Story.KillQuest(6170, "dragonbone", "Gorgorath");
    }
}