/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CoreNewYear
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Core.RunCore();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        if (!Core.isSeasonalMapActive("newyear"))
            return;

        NewYear();
        DarkSun();
        Frostmane();
        Everfrost();
        ChronoPhoenix();
        TimeRitual();
    }

    public void NewYear()
    {
        if (Core.isCompletedBefore(1531) || !Core.isSeasonalMapActive("newyear"))
            return;

        Story.PreLoad(this);

        //newyear

        //Circuit Breakers (463)
        Story.KillQuest(463, "newyear", "Sneevil");

        //Current Affairs (464)
        Story.KillQuest(464, "newyear", new[] { "Sneevil", "Sneevil" });

        //Not Finished Yet-i (465)
        Story.KillQuest(465, "newyear", "Ice Master Yeti");

        //newyearlab

        //Is the Area as Big Inside as Outside? (1524)
        Story.MapItemQuest(1524, "newyearlab", 762);

        //The Final Hour? (1525)
        Story.KillQuest(1525, "newyearlab", "Chaos-Saw Sneevil");

        //See-Sawing Through Time (1526)
        Story.MapItemQuest(1526, "newyearlab", 763, 6);

        //A Crack in Time Saves More Than Nine (1527)
        Story.KillQuest(1527, "newyearlab", new[] { "Chaos Rhino Beetle", "Chaos Rhino Beetle" });

        //Time Bashes On (1528)
        Story.KillQuest(1528, "newyearlab", "Chaorrupted Polar Bear");

        //SHUTDOWN Sequence (1530)
        Story.MapItemQuest(1530, "newyearlab", new[] { 764, 765, 766, 767 });

        //Chronomancy and Chaos (1531)
        Story.KillQuest(1531, "newyearlab", "Iadoa");
    }

    public void DarkSun()
    {
        if (Core.isCompletedBefore(2597) || !Core.isSeasonalMapActive("darksun"))
            return;

        Story.PreLoad(this);

        //Power the Sun 2589
        Story.MapItemQuest(2589, "darkoviaforest", 1595, 3);
        Story.MapItemQuest(2589, "darksun", 1596, 4);

        //Interrogate Night Creatures 2590
        if (!Story.QuestProgression(2590))
        {
            Core.EnsureAccept(2590);
            Core.KillMonster("lycanwar", "War", "Left", "*", "Raskar's Location");
            Core.KillMonster("lycanwar", "War", "Left", "*", "Jealous Whispers");
            Core.KillMonster("vampirewar", "War", "Left", "*", "Knowledge about Jinella");
            Core.KillMonster("vampirewar", "War", "Left", "*", "Unfounded Rumors");
            Core.EnsureComplete(2590);
        }

        //Nocturan Adept Battle 2591
        Story.KillQuest(2591, "darksun", "Aku");

        //Fight the Darkness 2592
        Story.KillQuest(2592, "darkoviagrave", "Albino Bat");
        Story.MapItemQuest(2592, "darkoviaforest", 1604);

        //Cloaked in Darkness 2593
        Story.MapItemQuest(2593, "darksun", 1597);

        //Escape to Freedom 2594
        Story.KillQuest(2594, "darkoviaforest", new[] { "Dire Wolf", "Blood Maggot" });

        //Shine a Light on Truth 2595
        Story.MapItemQuest(2595, "darksun", 1598);

        //Weave Light into Dark 2596
        Story.MapItemQuest(2596, "darksun", 1599, 6);
        Story.KillQuest(2596, "darkoviagrave", "Skeletal Fire Mage");

        //Battle for a New Start 2597
        Story.KillQuest(2597, "darksun", "Raskar");
    }
    
    public void Frostmane()
    {
        if (Core.isCompletedBefore(3281) || !Core.isSeasonalMapActive("frostmane"))
            return;

        Story.PreLoad(this);

        //Wood & Herbs 3271
        if (!Story.QuestProgression(3271))
        {
            Core.EnsureAccept(3271);
            Core.HuntMonster("frostmane", "Pine Troll", "Lunar Herbs", 5);
            Core.HuntMonster("frostmane", "Pine Troll", "Moon Wood", 5);
            Core.EnsureComplete(3271);
        }

        //That Should Get His Attention 3272
        Story.KillQuest(3272, "frostmane", "Arctic Wolf");

        //Calling Frostmane 3273
        Story.MapItemQuest(3273, "frostmane", 2326);

        //Nightmare Juice, Om Nom Nom 3274
        Story.KillQuest(3274, "frostmane", "Abandoned Dolly");

        //Gentle Persuasion 3275
        Story.KillQuest(3275, "frostmane", "Abandoned Dolly");

        //Breaking & Entering 3276
        Story.KillQuest(3276, "frostmane", "Shadow Person");

        //The Hills Are Alive 3277
        Story.KillQuest(3277, "frostmane", "Rock");

        //A Geode for Geo 3278
        Story.KillQuest(3278, "frostmane", "Rock");

        //Bribery 3279
        Story.MapItemQuest(3279, "frostmane", 2325);

        //Defeat Frostmane's Nightmare 3280
        Story.KillQuest(3280, "frostmane", "Nightmare Frostmane");

        //Defeat Frostmane's Ultra Nightmare 3281
        Story.KillQuest(3281, "frostmane", "Ultra Nightmare Frostmane");
    }

    public void Everfrost()
    {
        if (Core.isCompletedBefore(6152) || !Core.isSeasonalMapActive("everfrost"))
            return;

        Story.PreLoad(this);

        //Get some Pelts 6144
        Story.KillQuest(6144, "everfrost", "Arctic Wolf");

        //Find the Sled 6145
        Story.MapItemQuest(6145, "everfrost", new[] { 5565, 5566 });
        Story.MapItemQuest(6145, "everfrost", 5567, 2);

        //It's Sledding Time 6146
        Story.MapItemQuest(6146, "everfrost", 5568);

        //Gather Peppers 6147
        Story.KillQuest(6147, "everfrost", "Ice Spitter");

        //Wood for Moknak 6148
        Story.KillQuest(6148, "everfrost", "Frozen Treeant");

        //Cross the Ice 6149
        Story.MapItemQuest(6149, "everfrost", 5569);

        //Smash the Shards 6150
        Story.KillQuest(6150, "everfrost", "Chill Shard");

        //Make it to the Top 6151
        Story.MapItemQuest(6151, "everfrost", 5570, 4);
        Story.KillQuest(6151, "everfrost", "Ice Golem");

        //Defeat Chillbite! 6152
        Story.KillQuest(6152, "everfrost", "Chillbite");
    }

    public void ChronoPhoenix()
    {
        if (Core.isCompletedBefore(7275) || !Core.isSeasonalMapActive("chronophoenix"))
            return;

        Story.PreLoad(this);

        //Put out the Fire 7265
        Story.KillQuest(7265, "chronophoenix", "Phoenix Fire");

        //Assist the Assistants 7266
        Story.KillQuest(7266, "chronophoenix", "Chrono-Assistant");

        //Energy for Repairs 7267
        Story.KillQuest(7267, "chronophoenix", "Errant Spacetime Energy");

        //Stabilize the Rift 7268
        Story.MapItemQuest(7268, "chronophoenix", 6905, 5);

        //Freezies 7269
        Story.KillQuest(7269, "chronophoenix", "Frost Elemental");

        //Freeze the Energy 7270
        Story.KillQuest(7270, "chronophoenix", "Errant Spacetime Energy");

        //More Flames! 7271
        if (!Story.QuestProgression(7271))
        {
            Core.EnsureAccept(7271);
            Core.KillMonster("chronophoenix", "r5", "Left", "Phoenix Fire", "Fire Extinguished", 10);
            Core.EnsureComplete(7271);
        }

        //Find the Phoenix 7272
        Story.MapItemQuest(7272, "chronophoenix", 6906);

        //Close out the Year 7273
        Story.KillQuest(7273, "chronophoenix", "2019");

        //Freeze the Phoenix 7274
        Story.KillQuest(7274, "chronophoenix", "Chrono-Phoenix");

        //Head Outside 7275
        Story.MapItemQuest(7275, "chronophoenix", 6907);
    }

    public void TimeRitual()
    {
        if (Core.isCompletedBefore(7867) || !Core.isSeasonalMapActive("timeritual"))
            return;

        Story.PreLoad(this);

        //Chronomancing the Stone 7860
        Story.KillQuest(7860, "bludrut", "Rock Elemental");

        //Hourglass Half Full 7861
        Story.KillQuest(7861, "sandsea", "Bupers Camel");

        //Quartz Past Twelve 7862
        Story.KillQuest(7862, "undergroundlabb", "Window");

        //Spring Forward a Few Minutes 7863
        Story.KillQuest(7863, "thespan", "Shadowscythe");

        //A Nickel for Everytime That Happened 7864
        Story.KillQuest(7864, "mountainpath", "Ore Balboa");

        //Back to the Present 7865
        Story.KillQuest(7865, "worldsoul", "Radioactive Hydra");

        //Stop Au'eir 7866
        Story.KillQuest(7866, "timeritual", "Au'eir");

        //Defeat Chronocide 7867
        Story.KillQuest(7867, "timeritual", "Chronocide");
    }

}
