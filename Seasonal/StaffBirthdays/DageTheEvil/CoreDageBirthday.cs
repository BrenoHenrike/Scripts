/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CoreDageBirthday
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
        if (!Core.isSeasonalMapActive("darkbirthday"))
            return;

        DarkPath();
        FutureLegion();
        Undervoid();
        LegionBarracks();
        CocytusBarracks();
    }

    public void DarkPath()
    {
        if (Core.isCompletedBefore(6234) || !Core.isSeasonalMapActive("darkpath"))
            return;

        Story.PreLoad(this);

        //Soul Energy (6220)
        Story.KillQuest(6220, "darkpath", "Dark Makai");

        //Open the Portal (6221)
        Story.KillQuest(6221, "darkpath", "Void Elemental");
        Story.MapItemQuest(6221, "darkpath", 5663, 5);

        //Go Through the Portal (6222)
        Story.MapItemQuest(6222, "darkpath", 5664);

        //We Need a Guide (6223)
        if (!Story.QuestProgression(6223))
        {
            Core.EnsureAccept(6223);
            // there are 2 "Void Energy" - `40070` [Wrong] & `43068` [Correct]
            while (!Bot.ShouldExit && !Core.CheckInventory(43068, 10))
                Core.KillMonster("darkpath", "r7", "Left", "Void Makai", log: false);
            Core.EnsureComplete(6223);
        }

        //Darkness is Energy (6224)
        Story.KillQuest(6224, "darkpath", "Void Elemental");

        //Reach the Vault (6225)
        Story.MapItemQuest(6225, "darkpath", 5665);

        //Open the Vault (6226)
        Story.KillQuest(6226, "darkpath", new[] { "Void Makai", "Void Wyrm" });
        Story.MapItemQuest(6226, "darkpath", 5666);

        //Examine the Souls (6227)
        Story.KillQuest(6227, "darkpath", "Wandering Soul");

        //Find Another Path (6228)
        Story.MapItemQuest(6228, "darkpath", 5667);

        //More Energy for Zep (6229)
        Story.KillQuest(6229, "darkpath", "Void Makai");

        //Pass Through the Shadows (6230)
        Story.KillQuest(6230, "darkpath", new[] { "Void Makai", "Void Elemental" });
        Story.MapItemQuest(6230, "darkpath", 5668, 5);

        //Find Contract (6231)
        Story.MapItemQuest(6231, "darkpath", 5669);

        //Battle the Void Army (6233)
        Story.KillQuest(6233, "voidvault", "Void Knight");
        Story.MapItemQuest(6233, "voidvault", 5673);

        //Defeat Zeph'gorog (6234)
        Story.KillQuest(6234, "voidvault", "Zeph'gorog");
    }
    public void FutureLegion()
    {
        if (Core.isCompletedBefore(5736))
            return;
        if (!Core.isSeasonalMapActive("futurelegion"))
            return;

        Story.PreLoad(this);

        //Examine the Area 5724
        Story.MapItemQuest(5724, "futurelegion", new[] { 5162, 5163, 5164 });
        Story.KillQuest(5724, "futurelegion", "UW3017 Gunner");

        //Get the Key 5725
        Story.KillQuest(5725, "futurelegion", "UW3017 Gunner");

        //Obtain Agravh's Soul 5726
        Story.MapItemQuest(5726, "futurelegion", 5165);
        Story.KillQuest(5726, "futurelegion", "Commander Agravh");

        //Obtain Uslaw's Soul 5727
        Story.MapItemQuest(5727, "futurelegion", 5166);
        Story.KillQuest(5727, "futurelegion", "Commander Uslaw");

        //Access the Control Room 5728
        Story.MapItemQuest(5728, "futurelegion", 5167);

        //Destory the Force Field 5729
        Story.KillQuest(5729, "futurelegion", "UW3017 Blaster");
        Story.MapItemQuest(5729, "futurelegion", 5168);

        //Obtain Ozar's Soul 5730
        Story.KillQuest(5730, "futurelegion", "Commander Ozar");

        //Obtain Pavon's Soul 5731
        Story.KillQuest(5731, "futurelegion", "Commander Pavon");

        //Activate the Teleporter 5732
        Story.MapItemQuest(5732, "futurelegion", 5169);

        //Keep It Grounded 5733
        Story.MapItemQuest(5733, "futurelegion", 5170, 7);
        Story.KillQuest(5733, "futurelegion", "SF3017 Gunner");

        //Get the Code 5734
        Story.KillQuest(5734, "futurelegion", new[] { "SF3017 Gunner", "SF3017 Blade" });

        //Open the Door 5735
        Story.MapItemQuest(5735, "futurelegion", 5171);

        //Take out the Legionator 5736
        Story.KillQuest(5736, "futurelegion", "Legionator");
    }

    public void Undervoid()
    {
        if (Core.isCompletedBefore(3406) || !Core.isSeasonalMapActive("undervoid"))
            return;

        Story.PreLoad(this);

        Core.AddDrop("Hollowborn Soul Stealer");

        //Dark, Deadly Warmup
        Story.KillQuest(3399, "underworld", "Dark Makai");

        //Destroy the Good
        Story.KillQuest(3400, "alliance", "Good Soldier");

        //Destroy Chaorrupted Evil
        Story.KillQuest(3401, "alliance", "Chaorrupted Evil Soldier");

        //Legion Fenrir Gauntlet
        Story.KillQuest(3402, "underworld", "Legion Fenrir");

        //Conquer Conquest
        Story.KillQuest(3403, "undervoid", "Conquest");

        //Conquer War
        Story.KillQuest(3404, "undervoid", "War");

        //Conquer Famine
        Story.KillQuest(3405, "undervoid", "Famine");

        //Conquer Death
        Story.KillQuest(3406, "undervoid", "Death");
    }

    public void LegionBarracks()
    {
        if (Core.isCompletedBefore(9619) || !Core.isSeasonalMapActive("legionbarracks"))
            return;

        Story.PreLoad(this);

        // Backroom Whispers (9611)
        Story.KillQuest(9611, "legionbarracks", "Legion Evocator");

        // Cur Dolorem Sentis (9612)
        Story.MapItemQuest(9612, "legionbarracks", 12774, 3);

        // Genuine Character (9608)
        Story.KillQuest(9608, "legionbarracks", "Legion Knight");

        // Seen the Light (9613)
        Story.KillQuest(9613, "legionbarracks", "Sullied Master");

        // Bad Beef (9614)
        Story.KillQuest(9614, "legionbarracks", "Off-Duty Minos");

        // Tomb of Memories (9615)
        Story.KillQuest(9615, "legionbarracks", "Enlightened Master");

        // Reap What's Sown (9616)
        Story.KillQuest(9616, "legionbarracks", "Overdriven paladin");
        Story.MapItemQuest(9616, "legionbarracks", 12775);


        // Wails of Unrest (9617)
        Story.KillQuest(9617, "legionbarracks", new[] { "Legion Evocator", "Legion Knight" });

        // Lamenting Aestiua (9618)
        Story.MapItemQuest(9618, "legionbarracks", new[] { 12776, 12777 });

        // Unblemished Snow (9619)
        if (!Core.isCompletedBefore(9619))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(9619, "legionbarracks", "Paladin Arondight");
        }

    }

    public void CocytusBarracks()
    {
        if (Core.isCompletedBefore(9632) || !Core.isSeasonalMapActive("cocytusbarracks"))
            return;

        LegionBarracks();

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // Cocytus Wails (9623)
        Story.KillQuest(9623, "cocytusbarracks", new[] { "Legion Evocator", "Legion Knight" });

        // Light Up Ice Cube (9624)
        Story.MapItemQuest(9624, "cocytusbarracks", 12798, 6);

        // Blessed Blind (9625)
        Story.KillQuest(9625, "cocytusbarracks", "Overdriven Evocator");

        // Falsified Hope (9626)
        Story.KillQuest(9626, "cocytusbarracks", "Overdriven Knight");

        // Saint of the Battlefield (9627)
        Story.MapItemQuest(9627, "cocytusbarracks", 12799);
        Story.KillQuest(9627, "cocytusbarracks", "Overdriven Paladin");

        // River Bends (9628)
        Story.MapItemQuest(9628, "cocytusbarracks", 12800);
        Story.KillQuest(9628, "cocytusbarracks", new[] { "Legion Evocator", "Legion Knight" });

        // Styx and Stones (9629)
        Story.KillQuest(9629, "cocytusbarracks", new[] { "Overdriven Knight", "Overdriven Evocator" });

        // Cry Me a River (9630)
        Story.KillQuest(9630, "cocytusbarracks", "Mourner");

        // Boiling Blood (9631)
        Story.KillQuest(9631, "cocytusbarracks", "Cerberus Pup");

        // The Knight of Summer Set (9632)
        if (!Core.isCompletedBefore(9632))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(9632, "cocytusbarracks", "Maleagant");
        }

    }

}

