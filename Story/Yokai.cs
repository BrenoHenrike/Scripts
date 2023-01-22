/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class YokaiQuests
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new CoreStory();
    public CoreAdvanced Adv = new CoreAdvanced();
    public Core13LoC LOC => new Core13LoC();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Quests();

        Core.SetOptions(false);
    }

    public void Quests()
    {
        ShogunWar();
        ShinrinGrove();
        Shadowfortress();
    }


    public void ShogunWar()
    {
        if (Core.isCompletedBefore(6459))
            return;

        Story.PreLoad(this);

        Core.Logger("ShogunWar Quest line");

        Core.EquipClass(ClassType.Farm);

        // Shadow Medals 6450
        Story.KillQuest(6450, "ShogunWar", "Shadow Samurai");

        // We Need Supplies 6451
        Story.KillQuest(6451, "ShogunWar", new[] { "Reishi", "Kijimuna" });

        // Help the Samurai 6452
        Story.MapItemQuest(6452, "ShogunWar", 5956, 5);

        // Arrows for Archers 6453
        Story.KillQuest(6453, "ShogunWar", "Shadow Samurai");

        // Fix the Arrows 6454
        Story.KillQuest(6454, "ShogunWar", "Bamboo Treeant");

        // Empowered Shadow Medallions 6455
        Story.KillQuest(6455, "ShogunWar", "Shadow Samurai");

        // Advice from the Spirits 6456
        Story.MapItemQuest(6456, "ShogunWar", new[] { 5957, 5958, 5959, 5960 });

        // Fight the Shadows 6457
        Story.KillQuest(6457, "ShogunWar", "Reishi");

        // Bamboo for Tea 6458
        Story.KillQuest(6458, "ShogunWar", "Bamboo Treeant");

        // Defeat the Beast 6459
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6459, "ShogunWar", "Orochi");

        // Get the Medallions 6460
        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(6460, "ShogunWar", "Shadow Samurai");

        // Tea for Me 6461
        Story.KillQuest(6461, "ShogunWar", new[] { "Reishi", "ijimuna", "Bamboo Treeant" });
    }

    public void ShinrinGrove()
    {
        if (Core.isCompletedBefore(6472))
            return;

        Story.PreLoad(this);


        Core.Logger("ShinrinGrove Quest line");

        Core.EquipClass(ClassType.Farm);

        // Get Rid of Kame 6462
        Story.KillQuest(6462, "shinringrove", "Kame");

        // Food for the Utoroshi 6463
        Story.KillQuest(6463, "shinringrove", new[] { "Reishi", "Tsurubebi" });

        // Summon Otoroshi 6464
        Story.MapItemQuest(6464, "shinringrove", 5962);
        Story.KillQuest(6464, "shinringrove", "Otoroshi");

        // Put out the Fire 6465
        Story.MapItemQuest(6465, "greenshell", 5964, 5);
        Story.KillQuest(6465, "greenshell", "Tsurubebi");

        // Expell the Tsukumogami 6466
        Story.KillQuest(6466, "greenshell", "Tsukumogami");

        // Convince the Kodama 6467
        Story.KillQuest(6467, "shinringrove", "Kodama");

        // Refill the Barrels 6468
        Story.KillQuest(6468, "shinringrove", "Moglinberry Bush");

        // How to Breathe Underwater 6469
        Story.MapItemQuest(6469, "greenshell", 5965, 8);
        Story.KillQuest(6469, "shinringrove", "Reishi");

        // Search for the Shinrin Do 6470
        Story.MapItemQuest(6470, "greenshell", 5966);
        Story.KillQuest(6470, "greenshell", "Merdraconian");

        // Take the Shinrin Do 6471
        Story.MapItemQuest(6471, "greenshell", 5967);

        // Battle for the Shinrin Do 6472  
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6472, "greenshell", "Nagami");

        // For the Road 6473
        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(6473, "shinringrove", new[] { "Moglinberry Bush", "Reishi" });
    }




    public void Shadowfortress()
    {
        if (Core.isCompletedBefore(6494))
            return;

        Story.PreLoad(this);


        Core.Logger("Shadowfortress Quest line");

        Core.EquipClass(ClassType.Farm);

        // Strange Creatures 6474
        Story.KillQuest(6474, "heiwavalley", "Abumi Guchi");

        // Arm the Militia 6475
        Story.KillQuest(6475, "heiwavalley", new[] { "Shadow Samurai", "Shadow Samurai" });

        // Burn the Bones 6476
        Story.MapItemQuest(6476, "heiwavalley", 5968, 6);
        Story.KillQuest(6476, "heiwavalley", "Kosenjobi");

        // Goryuken 6477
        Story.KillQuest(6477, "heiwavalley", "Goryo");

        // Feel the Heat 6478
        Story.BuyQuest(6478, "heiwavalley", 1608, "Wasabi");
        Story.KillQuest(6478, "heiwavalley", "Kosenjobi");

        // Get Some Fur 6479
        Story.KillQuest(6479, "heiwavalley", "Abumi Guchi");


        // Howl-ing For You! 6480
        Story.KillQuest(6480, "heiwavalley", "Inugami");


        // Eyes on the Prize 6481
        Story.MapItemQuest(6481, "heiwavalley", 5970, 8);
        Story.KillQuest(6481, "heiwavalley", "Inugami");


        // Reveal the Trail 6482
        Story.MapItemQuest(6482, "heiwavalley", 5971);
        Story.MapItemQuest(6482, "heiwavalley", 5972, 8);


        // Defeat the Onryo 6483
        Story.KillQuest(6483, "heiwavalley", "Onryo");

        // Calm the Spirits 6485
        Story.KillQuest(6485, "shadowfortress", "Restless Spirit");

        // Disarm the Samurai 6486
        Story.KillQuest(6486, "shadowfortress", "Shadow Samurai");

        // Find the Throne Room 6487
        Story.MapItemQuest(6487, "shadowfortress", 5973);

        // Shadow Essences 6488
        Story.MapItemQuest(6488, "shadowfortress", 5974);
        Story.KillQuest(6488, "shadowfortress", "Living Shadow");

        // New Recruits 6489
        Story.KillQuest(6489, "shadowfortress", "Shadow Samurai");

        // Shadow Heads 6490
        Story.MapItemQuest(6490, "shadowfortress", 5975, 7);
        Story.KillQuest(6490, "shadowfortress", "Creeping Shadow");

        // A Head has Woken
        Story.KillQuest(6491, "shadowfortress", "7th Head of Orochi");

        // Get to the Roof 6492
        Story.MapItemQuest(6492, "shadowfortress", new[] { 5976, 5979 });

        // Find Jaaku! 6493
        Core.EquipClass(ClassType.Solo);
        Story.MapItemQuest(6493, "shadowfortress", 5977);
        Story.KillQuest(6493, "shadowfortress", new[] { "6th Head of Orochi", "5th Head of Orochi", "4th Head of Orochi", "3rd Head of Orochi", "2nd Head of Orochi", "1st Head of Orochi" });

        // Defeat Jaaku! 6494        
        Story.KillQuest(6494, "shadowfortress", "Jaaku");
    }
}
