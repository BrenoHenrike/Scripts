/*
name: Midnight War
description: This script will complete the storyline in /midnightcrown and /midnightwar
tags: midnight-war, midnight-crown, flintfang, bonnie blood, rustclaw, tlapd, talk-like-a-pirate-day, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class MidnightWar
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Storyline();
        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(5417) || !Core.isSeasonalMapActive("midnightwar"))
            return;

        Story.PreLoad(this);

        // The New Swabbie (5388)
        Story.KillQuest(5388, "midnightcrown", "Spike the Swabbie");

        // It's A Thankless Job (5389)
        Story.MapItemQuest(5389, "midnightcrown", new[] { 4746, 4747 }, 4);

        // Gunpowder, Lots! (5390)
        Story.KillQuest(5390, "midnightcrown", "Scoundrel");
        Story.MapItemQuest(5390, "midnightcrown", 4748, 5);

        // Boar-ed of Seafood (5391)
        Story.KillQuest(5391, "midnightcrown", "Island Boar");

        // This Boar Is On Fire (5392)
        Story.MapItemQuest(5392, "midnightcrown", 4749, 4);
        Story.MapItemQuest(5392, "midnightcrown", new[] { 4750, 4751 });

        // Pirates Be Stealthy! (5393)
        Story.MapItemQuest(5393, "midnightcrown", 4752);

        // It Pays To Be Prepared! (5394)
        Story.KillQuest(5394, "midnightcrown", "Scoundrel");
        Story.MapItemQuest(5394, "midnightcrown", 4753);

        // A Bit O' Protein (5395)
        Story.KillQuest(5395, "midnightcrown", "Island Boar");

        // Meals on Wheels (5396)
        Story.MapItemQuest(5396, "midnightcrown", 4754, 5);

        // Decoratin' Time! (5397)
        Story.KillQuest(5397, "midnightcrown", "Rapscallion");

        // Catch Some Fireflies (5398)
        Story.MapItemQuest(5398, "midnightcrown", new[] { 4755, 4756 }, 4);

        // Black Veil Courier Service (5399)
        Story.MapItemQuest(5399, "midnightcrown", 4757, 6);

        // Bring Me the Bounty of the Black Veil! (5402)
        Story.KillQuest(5402, "midnightwar", "Black Veil Pirate");

        // Defeat Flintfang (5417)
        Story.KillQuest(5417, "midnightwar", "Flintfang");
    }
}
