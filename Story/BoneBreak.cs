/*
name: Bone Break Story
description: This will complete the Bone Break Story.
tags: story, quest, bone-break
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class BoneBreak
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
        if (Core.isCompletedBefore(5981))
            return;

        if (!Core.HasAchievement(30, "ip6") && !Core.IsMember)
        {
            Core.Logger("\"BoneBreak\" map requires you to have membership or purchased BoneBreaker Adventure Pack to be able to access it.");
            return;
        }
        if (Core.HasAchievement(30, "ip6"))
            Core.BuyItem("battleon", 1046, 27222);

        Story.PreLoad(this);

        //Bracken, Bone, and Branch 3892
        if (!Story.QuestProgression(3892))
        {
            Core.EnsureAccept(3892);
            Core.HuntMonster("bonebreak", "Bone Leech", "Venomous Leech Fang", 3);
            Core.HuntMonster("bonebreak", "Marsh Treeant", "Dread Tree Branch", 6);
            Core.HuntMonster("bonebreak", "Marsh Thing", "Marsh Weed", 12);
            Core.EnsureComplete(3892);
        }

        //Break Braddok’s Backbone 3893
        Story.KillQuest(3893, "bonebreak", "Undead Berserker");

        //Stone of Binding 3894
        Story.MapItemQuest(3894, "bonebreak", 2990, 13);
        Story.KillQuest(3894, "bonebreak", "Kidnapped Prisoner");

        //Break the Bone Fortress 3895
        if (!Story.QuestProgression(3895))
        {
            Core.EnsureAccept(3895);
            Core.HuntMonster("bonebreak", "Unbroken Minion", "Unbroken Minion Defeated", 10);
            Core.HuntMonster("bonebreak", "Undead Berserker", "Berserker Warrior Defeated", 10);
            Core.HuntMonster("bonebreak", "Bone Leech", "Bone Leech Defeated", 5);
            Core.EnsureComplete(3895);
        }

        //It’s a TRAP! (No, really.) 3896
        Story.KillQuest(3896, "bonebreak", "Undead Berserker");

        //Defeat Braddock Bonebreaker  3897
        Story.KillQuest(3897, "bonebreak", "Bonebreaker");

        //Hunt for Killek 5977
        Story.MapItemQuest(5977, "bonebreak", 5418);

        //Chew the Dead 5978
        Story.KillQuest(5978, "bonebreak", new[] { "Undead Berserker", "Bone Leech" });

        //Marsh Mash Pit 5979
        Story.KillQuest(5979, "bonebreak", "Marsh Thing");

        //Ash Me No Questions 5980
        Story.KillQuest(5980, "bonebreak", "Kidnapped Prisoner");

        //Defeat Killek Bonebreaker 5981
        Story.KillQuest(5981, "bonebreak", "Killek BoneBreaker");

    }
}
