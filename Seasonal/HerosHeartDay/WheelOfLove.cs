/*
name: Wheel of Love Story
description: This will complete the Wheel of Love story quest.
tags: story, quest, wheel, love, seasonal, hero, heart
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class WheeleOfLove
{
    public CoreBots Core => CoreBots.Instance;
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoWheeleOfLove();

        Core.SetOptions(false);
    }

    public void DoWheeleOfLove()
    {
        WheelOfLoveDungeon();
        WheelOfLoveQuest();
    }

    public void WheelOfLoveDungeon()
    {
        if (!Core.isSeasonalMapActive("wheeloflove"))
            return;
        if (Core.isCompletedBefore(5693))
            return;

        Story.PreLoad(this);

        // The Golden Heart
        Story.KillQuest(5684, "wheeloflove", "Love Shrub");

        // The Red Room
        Story.MapItemQuest(5685, "wheeloflove", 5146, 6);

        // Get out of here
        Story.KillQuest(5686, "wheeloflove", "Lava Slime");
        Story.MapItemQuest(5686, "wheeloflove", 5147, 4);

        // Spooky Cupid
        Story.KillQuest(5687, "wheeloflove", "Undead Cherub");

        // Get a Bullseye
        Story.MapItemQuest(5688, "wheeloflove", 5148);

        // And They Called it Puppy Love
        Story.KillQuest(5689, "wheeloflove", "Puppy Love");

        // Gotta Get the Keys
        Story.KillQuest(5690, "wheeloflove", "Lil' Poot");

        // They Already Lost
        Story.KillQuest(5691, "wheeloflove", "Last Year's Loser");

        // A Long Time Ago
        Story.KillQuest(5692, "wheeloflove", "Lost Love");

        // Is His Name Herbie?
        Story.KillQuest(5693, "wheeloflove", "Love Bug");
    }

    public void WheelOfLoveQuest()
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(5694).Rewards;
        List<string> RewardsList = new();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        if (Core.CheckInventory(Rewards))
            return;
        Core.AddDrop(Rewards);
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards))
        {
            Core.EnsureAccept(5694);
            Core.HuntMonster("wheeloflove", "Undead Cherub", "Unlove Dart", 13);
            Core.EnsureCompleteChoose(5694, Rewards);
            Core.Sleep();
        }
    }
}
