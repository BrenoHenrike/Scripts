/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Core7DD
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void Complete7DD()
    {
        Gluttony();
        Pride();
        Greed();
        Sloth();
        Lust();
        Envy();
        Wrath();
    }

    #region Gluttony
    public void Gluttony()
    {
        if (Core.isCompletedBefore(5915))
            return;

        Core.Logger("Hunting the Fire Dragon of Gluttony!");

        Story.PreLoad(this);

        // Guts for Glutes 5903
        Story.KillQuest(5903, "Gluttony", "Glutus");

        // Mucus be Joking 5904
        Story.KillQuest(5904, "Gluttony", "Mucus");

        // Build a Bone-a-fied Ladder 5905]
        Story.MapItemQuest(5905, "Gluttony", 5346, 2);
        Story.KillQuest(5905, "Gluttony", "Skeletal Slayer");

        // Talk About Reflux 5906
        Story.MapItemQuest(5906, "Gluttony", 5344);

        // How'd HE get here? 5907
        Story.MapItemQuest(5907, "Gluttony", 5345);

        // Glowworms not Glowsticks 5908
        Story.MapItemQuest(5908, "Gluttony", 5347, 10);
        Story.KillQuest(5908, "Gluttony", "Bowel Worm");

        // Ossification Needed 5909
        if (!Story.QuestProgression(5909))
        {
            Core.EnsureAccept(5909);
            Core.KillMonster("Gluttony", "r6", "Left", "Bile", "Falgar's Bones", 9);
            Core.EnsureComplete(5909);
        }

        // Eye Need Bones 5910
        Story.MapItemQuest(5910, "Gluttony", 5348, 5);
        Story.KillQuest(5910, "Gluttony", "Skeletal Slayer");

        // Bile Burns 5911
        Story.KillQuest(5911, "Gluttony", new[] { "Bile", "Bowel Worm" });

        // Find the Chest 5912
        Story.MapItemQuest(5912, "Gluttony", 5349);

        // We Need the Key 5913
        Story.KillQuest(5913, "Gluttony", "Giant Tapeworm");

        // Cha Cha Cha 5914
        Story.MapItemQuest(5914, "Gluttony", 5350);

        // Glutus, Take 2 5915
        Story.KillQuest(5915, "Gluttony", "Deflated Glutus");
    }
    #endregion Gluttony

    #region Pride
    public void Pride()
    {
        if (Core.isCompletedBefore(5926))
            return;

        Core.Logger("Hunting the Energy Dragon of Pride!");

        Story.PreLoad(this);

        // Defeat the Drakel 5917
        Story.KillQuest(5917, "Pride", "Storm Drakel");

        // Don’t Get Zapped! 5918
        Story.KillQuest(5918, "Pride", "Ball Lightning");

        // Get Grounded 5919
        Story.KillQuest(5919, "Pride", "Rubber Treeant");

        // Get the Key 5920
        Story.KillQuest(5920, "Pride", "Cellar Guard");

        // Get the Boots 5921
        Story.MapItemQuest(5921, "Pride", 5351);
        Story.MapItemQuest(5921, "Pride", 5352, 6);

        // Make Lightning Rods 5922
        Story.MapItemQuest(5922, "Pride", 5353, 8);
        Story.KillQuest(5922, "Pride", "Storm Drakel");

        // Free the Villagers 5923
        Story.MapItemQuest(5923, "Pride", 5354, 4);
        Story.KillQuest(5923, "Pride", "Drakel Guard");

        // Open the Gate 5924
        Story.MapItemQuest(5924, "Pride", 5355);
        Story.KillQuest(5924, "Pride", "Elite Guard");

        // Fight Your Way Throug 5925
        Story.MapItemQuest(5925, "Pride", 5356);
        Story.KillQuest(5925, "Pride", "Storm Drakel");

        // Defeat Valsarian 5926
        Story.KillQuest(5926, "Pride", "Valsarian");
    }
    #endregion Pride

    #region Greed
    public void Greed()
    {
        if (Core.isCompletedBefore(5943))
            return;

        Core.Logger("Hunting the Ice Dragon of Greed!");

        Story.PreLoad(this);

        // Looting is for Sneevils 5934
        Story.KillQuest(5934, "Greed", "Sneevil Looter");

        // Jumping in Puddles 5935
        Story.MapItemQuest(5935, "Greed", 5372);

        // Pick the Right Chest 5936
        Story.MapItemQuest(5936, "Greed", 5373);

        // Gelatin is Nasty 5937
        Story.KillQuest(5937, "Greed", "Jelly-Like Cube");

        // Explore the Cave 5938
        Story.MapItemQuest(5938, "Greed", 5374, 5);

        // Disarm the Trap 5939
        Story.MapItemQuest(5939, "Greed", new[] { 5377, 5378 });

        // Crystal-eyes 5940
        Story.KillQuest(5940, "Greed", new[] { "Ice Crystal", "Glacial Horror" });

        // Go-bolds? 5941
        Story.MapItemQuest(5941, "Greed", 5375, 5);
        Story.KillQuest(5941, "Greed", "Kobold");

        // Follow the Trail 5942
        Story.MapItemQuest(5942, "Greed", 5376, 3);

        // Get Rid of Greed 5943
        Story.KillQuest(5943, "Greed", "Goregold");
    }
    #endregion Greed

    #region Sloth
    public void Sloth()
    {
        if (Core.isCompletedBefore(5959))
            return;

        Core.Logger("Hunting the Slime Dragon of Sloth!");

        Story.PreLoad(this);

        HazMatSuit();

        // Are There Any Survivors? 5945
        Story.MapItemQuest(5945, "Sloth", 5382);
        Story.KillQuest(5945, "Sloth", "Plague Zombie");

        // Gathering Samples 5946
        Story.KillQuest(5946, "Sloth", new[] { "Snotgoblin", "Wandering Plague" });

        // Find a ‘Volunteer’ 5947
        Story.KillQuest(5947, "Sloth", "Plague Zombie");

        // Cure the Volunteer 5948
        Story.MapItemQuest(5948, "Sloth", 5387);

        // Herbal Help 5949
        Story.KillQuest(5949, "Sloth", "Marsh Thing");

        // Re-heal 5950
        Story.MapItemQuest(5950, "Sloth", 5383, 8);

        // Let's Try That Again 5951
        Story.MapItemQuest(5951, "Sloth", 5389);

        // One More Time 5952
        if (!Story.QuestProgression(5952))
        {
            Core.EnsureAccept(5952);
            Core.BuyItem("Dragonhame", 865, "Airther Vitae");
            Core.BuyItem("Arcangrove", 211, "Health Potion");
            Core.EnsureComplete(5952);
        }

        // Who’s Up for Round 3 5953
        Story.MapItemQuest(5953, "Sloth", 5391);

        // Cure the Villagers 5954
        Story.KillQuest(5954, "Sloth", "Plague Zombie");

        // Gotta Clean Up 5955
        Story.MapItemQuest(5955, "Sloth", 5384, 10);

        // Clear the Castellum 5956
        Story.MapItemQuest(5956, "Sloth", 5385);
        Story.KillQuest(5956, "Sloth", "SnotGoblin Prime");

        // Get rid of Phlegnn 5957
        Story.KillQuest(5957, "Sloth", "Phlegnn");

        // Cured is NOT GOOD 5958
        Story.KillQuest(5958, "Sloth", "Cured Phlegnn");

        // Actual Sloth Dragon 5960
        Story.KillQuest(5960, "Sloth", "Actual Sloth Dragon");

        // Mutated Plague 5959
        Story.KillQuest(5959, "Sloth", "Mutated Plague");
    }
    #endregion Sloth

    #region Lust
    public void Lust()
    {
        if (Core.isCompletedBefore(5976))
            return;

        Core.Logger("Hunting the Dragon of Lust!");

        Story.PreLoad(this);

        // Hopelessly Devoted 5961
        Story.KillQuest(5961, "Lust", "Devoted Admirer");

        // Fighting the Feeling 5962
        Story.MapItemQuest(5962, "Lust", new[] { 5405, 5406, 5407, 5408, 5409 });

        // Love Potion #9.1 5963
        Story.KillQuest(5963, "Lust", "Golden Vase");

        // Leather is Better 5964
        Story.KillQuest(5964, "Lust", "Enamored Guard");

        // Gird your Loins 5965
        Story.MapItemQuest(5965, "Lust", new[] { 5410, 5411 });
        Story.KillQuest(5965, "Lust", "Devoted Admirer");

        // Get the Keys 5966
        Story.KillQuest(5966, "Lust", "Enamored Guard");

        // Open the Cages 5967
        Story.MapItemQuest(5967, "Lust", 5412, 5);

        // Get Past the Guards 5968
        Story.MapItemQuest(5968, "Lust", 5413);
        Story.KillQuest(5968, "Lust", "Elite Guard");

        // Viscyra’lly Yours 5969
        Story.MapItemQuest(5969, "Lust", 5414);
        Story.KillQuest(5969, "Lust", "Viscyra");

        // Lascivia’sness 5970
        Story.KillQuest(5970, "Lust", "Lascivia");

        // Talk to the Guards 5971
        Story.KillQuest(5971, "Lust", "Elite Guard");

        // Lascivia Stunk Pretty 5972
        Story.KillQuest(5972, "Lust", new[] { "Golden Vase", "Golden Vase" });

        // You Broke It, You Fix It 5973
        Story.KillQuest(5973, "Lust", "Devoted Admirer");

        // Elite Energy 5974
        Story.MapItemQuest(5974, "Lust", 5415, 8);
        Story.KillQuest(5974, "Lust", "Elite Guard");

        // No Pillow Unturned 5975
        Story.MapItemQuest(5975, "Lust", new[] { 5416, 5417 });

        // Take Down Killek 5976
        Story.KillQuest(5976, "Lust", "Killek Deadchewer");
    }
    #endregion Lust

    #region Envy
    public void Envy()
    {
        if (Core.isCompletedBefore(6005))
            return;

        Core.Logger("Hunting the Darkness Dragon of Envy!");

        Story.PreLoad(this);

        // Talk to the Guard 5983
        Story.MapItemQuest(5983, "DragonCrown", 5420);

        // Talk to the Villager 5984
        Story.MapItemQuest(5984, "DragonCrown", 5421);

        // Fire and Ice 5985
        Story.MapItemQuest(5985, "DragonCrown", 5422, 6);
        Story.KillQuest(5985, "DragonCrown", "Fire Sprite");

        // Round ‘em Up 5986
        Story.KillQuest(5986, "DragonCrown", new[] { "Llama", "Rampaging Boar" });

        // Get Rockin’ 5987
        Story.KillQuest(5987, "DragonCrown", new[] { "Rock Elemental", "Earth Elemental" });

        // Stop Fightin’ 5988
        Story.MapItemQuest(5988, "DragonCrown", 5423);

        // Find Riddrug 5989
        Story.MapItemQuest(5989, "DragonCrown", 5424);

        // Defeat The Red Champion 5990
        Story.KillQuest(5990, "DragonCrown", "Torgat");

        // Defeat The Ice Champion 5991
        Story.KillQuest(5991, "DragonCrown", "Fressa");

        // Defeat The Undead Champion 5992
        Story.KillQuest(5992, "DragonCrown", "Radroth");

        // Defeat The Water Champion 5993
        Story.KillQuest(5993, "DragonCrown", "Nizex");

        // Defeat The Wind Champion 5994
        Story.KillQuest(5994, "DragonCrown", "Tathu");

        // Defeat The Yokai Champion 5995
        Story.KillQuest(5995, "DragonCrown", "Lanshen");

        // Defeat The Green Champion 5996
        Story.KillQuest(5996, "DragonCrown", "Ashax");

        // Defeat The Faerie Champion 5997
        Story.KillQuest(5997, "DragonCrown", "Letori");

        // Defeat The Chaos Champion 5998
        Story.KillQuest(5998, "DragonCrown", "Nayzol");

        // Defeat the Void Champion 5999
        Story.KillQuest(5999, "DragonCrown", "Zathas");

        // Argo’s Not Stopping Us! 6000
        Story.KillQuest(6000, "DragonCrown", "Argo");

        // Defeat Ukki, The 1st Sentinel 6001
        Story.KillQuest(6001, "Maloth", "Ukki");

        // Defeat Kagan, The 2nd Guardian 6002
        Story.KillQuest(6002, "Maloth", "Kagan");

        // Defeat Golgar, The 3rd Guardian 6003
        Story.KillQuest(6003, "Maloth", "Golgar");

        // Hand over the Entry Code 6004
        Story.KillQuest(6004, "Maloth", new[] { "Golgar", "Castle Guard", "Scroll Keeper", "Nervous Serf", "Locked Chest" });

        // Confront the Dragon King 6005
        Story.KillQuest(6005, "Maloth", "Maloth");
    }
    #endregion Envy

    #region Wrath
    public void Wrath()
    {
        if (Core.isCompletedBefore(6170))
            return;

        Core.Logger("Hunting the Dracolich of Wrath!");

        Story.PreLoad(this);

        // Decimate the Horde 6110
        Story.KillQuest(6110, "Wrath", new[] { "Bone Terror", "Fishbones" });

        // Douse with Flames 6111
        Story.KillQuest(6111, "Wrath", "Dark Fire");

        // Grenades of AWE 6112
        Story.MapItemQuest(6112, "Wrath", 5541, 6);
        Story.KillQuest(6112, "Wrath", "Bone Terror");

        // The Final Ingredient 6113
        if (!Story.QuestProgression(6113))
        {
            Core.AddDrop("Holy Wasabi");
            if (!Core.CheckInventory("Holy Wasabi", 1))
            {
                Core.EnsureAccept(1075);
                Core.HuntMonster("Doomwood", "Doomwood Ectomancer", "Dried Wasabi Powder", 4, true);
                Core.GetMapItem(428, 1, "lightguard");
                Core.EnsureComplete(1075);
                Bot.Sleep(5000);
            }
            Core.EnsureComplete(6113);
        }

        // Count Not to Four 6114
        Story.MapItemQuest(6114, "Wrath", 5542, 6);

        // Talk to Captain Rhubarb 6115
        Story.MapItemQuest(6115, "Wrath", 5540);

        // Find the Manifest 6116
        Story.MapItemQuest(6116, "Wrath", 5544);
        Story.KillQuest(6116, "Wrath", "Undead Pirate");

        // Get the Cargo Hold Key 6117
        Story.KillQuest(6117, "Wrath", new[] { "Mutineer", "Mutineer" });

        // Find the Jewel 6118
        Story.MapItemQuest(6118, "Wrath", 5545);

        // Defeat Droghor the Screecher! 6119
        Story.KillQuest(6119, "Wrath", "Droghor");

        // Defeat Gorhorath! 6120
        Story.KillQuest(6120, "Wrath", "Gorgorath");

        // Navigate the Maze 6163
        Story.MapItemQuest(6163, "Dragonbone", 5587);
        Story.KillQuest(6163, "Dragonbone", new[] { "Bone Dragonling", "Dark Fire" });

        // Get a Clue 6164
        Story.KillQuest(6164, "Dragonbone", "Bone Terror");

        // Heart of a Dracolich 6165
        Story.KillQuest(6165, "Dragonbone", "Bone Wyvern");

        // Open the Door 6166
        Story.MapItemQuest(6166, "Dragonbone", 5588);

        // Find the Jewel 6167
        Story.MapItemQuest(6167, "Dragonbone", 5589);

        // Get to the Lair 6168
        Story.MapItemQuest(6168, "Dragonbone", 5590);

        // Unlock the Door 6169
        Story.MapItemQuest(6169, "Dragonbone", 5591);
        Story.KillQuest(6169, "Dragonbone", "Dragonshade");

        // Slay Gorgorath 6170
        Story.KillQuest(6170, "Dragonbone", "Gorgorath");
    }
    #endregion Wrath

    #region Hazmat Suit for Sloth
    public void HazMatSuit()
    {
        if (!Core.CheckInventory(40710))
        {
            Core.Logger("Suit not found");
            Core.Join("Sloth");
            Core.EnsureAccept(5944);
            Core.GetMapItem(5380, 1, "Sloth");
            Core.GetMapItem(5381, 1, "Sloth");
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
    #endregion Hazmat Suit for Sloth
}
