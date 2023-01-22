/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Cleric
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        StoryLine();
        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(2293))
            return;

        Story.PreLoad(this);

        //Purely Elemental Knowledge 2288
        if (!Story.QuestProgression(2288))
        {
            Core.EnsureAccept(2288);
            Core.HuntMonster("lair", "Dark Draconian", "Pure Darkness");
            Core.HuntMonster("lair", "Golden Draconian", "Pure Light");
            Core.HuntMonster("gilead", "Fire Elemental", "Pure Fire");
            Core.HuntMonster("gilead", "Wind Elemental", "Pure Air");
            Core.HuntMonster("gilead", "Earth Elemental", "Pure Earth");
            Core.HuntMonster("gilead", "Water Elemental", "Pure Water");
            Core.HuntMonster("kingcoal", "Ice Elemental", "Pure Ice");
            Core.HuntMonster("airstorm", "Lightning Ball", "Pure Energy");
            Core.EnsureComplete(2288);
        }

        //Animal vs Human? 2289
        if (!Story.QuestProgression(2289))
        {
            Core.EnsureAccept(2289);
            Core.HuntMonster("bloodtuskwar", "Chaotic Vulture", "Avian Report");
            Core.HuntMonster("bloodtuskwar", "Chaotic Chinchilizard", "Reptile Report");
            Core.HuntMonster("bloodtuskwar", "Chaotic Rhison", "Mammal Report");
            Core.HuntMonster("alliance", "Chaorrupted Evil Soldier", "Evil Human Report");
            Core.HuntMonster("alliance", "Chaorrupted Good Soldier", "Good Human Report");
            Core.EnsureComplete(2289);
        }

        //Gateways to Chaos? 2290
        if (!Story.QuestProgression(2290))
        {
            Core.EnsureAccept(2290);
            Core.HuntMonster("sewer", "Grumble", "Grumble Portal Not Found");
            Core.HuntMonster("boxes", "Grizzlespit", "Grizzlespit Portal Not Found");
            Core.HuntMonster("j6", "Sketchy Dragon", "Sketchy Portal Not Found");
            Core.HuntMonster("giant", "Giant Cat", "Giant Portal Not Found");
            Core.HuntMonster("shadowfall", "Shadow Muncher", "ShadowMuncher Portal Not Found");
            Core.HuntMonster("orctown", "General Porkon", "Porkon Portal Not Found");
            Core.EnsureComplete(2290);
        }

        //The Nature of Chaos 2291
        Story.MapItemQuest(2291, "cleric", new[] { 1453, 1454, 1455, 1456, 1457 });

        //Chaos Theory 2292
        if (!Story.QuestProgression(2292))
        {
            Core.EnsureAccept(2292);
            Core.HuntMonster("thespan", "Shadowscythe", "Shadowscythe Effluvium", 8);
            Core.HuntMonster("thespan", "Shadowscythe", "Shadowscythe Plasma", 5);
            Core.EnsureComplete(2292);
        }

        //A Little Chaos Goes a Long Way 2293
        if (!Story.QuestProgression(2293))
        {
            Core.EnsureAccept(2293);
            Core.HuntMonster("cleric", "Chaos Turtle", "Full Chaorruption Sample");
            Core.HuntMonster("cleric", "Chaos Dragon", "New Chaorruption Sample");
            Core.EnsureComplete(2293);
        }
    }
}
