/*
name: Swaggys Chateau
description: This will complete the Swaggys Chateau story quest.
tags: swaggys-chateau, seasonal, heros-heart-day, heart, hero, story
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class SwaggysChateau
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        CompleteStory();
        Core.SetOptions(false);
    }

    public void CompleteStory()
    {
        if (Core.isCompletedBefore(6198))
            return;
            
        if (!Core.isSeasonalMapActive("chateau"))
            return;

        Story.PreLoad(this);

        // LET'S PINKIFY (6188)
        Story.MapItemQuest(6188, "chateau", new[] {5623, 5624, 5625, 5626});

        // HEART-SHAPED FRUIT (6189)
        Story.KillQuest(6189, "chateau", "Love Shrub");

        // Bear Hugs (6190)
        Story.KillQuest(6190, "chateau", new[] {"Mood Slime", "Chinchilla"});

        // Pay the Cover Charge (6191)
        if (!Story.QuestProgression(6191))
        {
            Core.EnsureAccept(6191);
            Core.BuyItem("chateau", 1548, "Admit One", 3);
            Core.EnsureComplete(6191);
        }

        // Get Some Clues (6192)
        if (!Story.QuestProgression(6192))
        {
            Core.EnsureAccept(6192, 6193, 6194, 6195);
            Core.HuntMonster("chateau", "Hopeless Romantic", "Pretty Flowers", 6, log: false);
            Core.HuntMonster("chateau", "Barista", "\"Hot Drink\"", 5, log: false);
            Core.HuntMonster("chateau", "Stray Foam", "Floor Cleaned", 6, log: false);
            Core.GetMapItem(5627, 3, "chateau");
            Core.EnsureComplete(6193, 6194, 6195, 6192);
        }

        // The VIP Lounge (6196)
        Story.KillQuest(6196, "chateau", "Important Guest");

        // Get Past the Bouncer (6197)
        Story.KillQuest(6197, "chateau", "Bouncer");

        // Pinky the Unicorn??? (6198)
        Story.KillQuest(6198, "chateau", "Pinky");
    }
}
