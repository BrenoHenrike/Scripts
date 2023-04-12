/*
name: CaptainLoresQuests
description: CaptainLores Questline
tags: storyline, captainlore, pollution, totengeldfactory
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CaptainLoresQuests
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Pollution();
        TotengeldFactory();

        Core.SetOptions(false);
    }

    public void Pollution()
    {
        if (Core.isCompletedBefore(6823) || !Core.isSeasonalMapActive("pollution"))
            return;

        Story.PreLoad(this);

        // All Wet 620
        Story.KillQuest(620, "pollution", new[] { "Water Elemental", "Kuro" });

        // Flame On 621
        Story.KillQuest(621, "pollution", new[] { "Fire Elemental", "Thermax" });

        // Plant Matters 619
        Story.KillQuest(619, "pollution", new[] { "Rock Elemental", "Stone Golem" });

        // Breaking Wind Turbines 618
        Story.KillQuest(618, "pollution", new[] { "Wind Elemental", "Ozone" });

        // Chaos Footprint 622
        Story.KillQuest(622, "pollution", "General Pollution");

        // Reduce, Reuse, Recycle 1036
        Story.KillQuest(1036, "pollution", new[] { "Thermax", "Kuro", "Stone Golem", "Ozone" });

        // Go Green like a Boss 1037
        Story.KillQuest(1037, "pollution", new[] { "Monstername", "Monstername" });

        // Clean Up the Core 6823
        Story.KillQuest(6823, "pollution", "Commodore Core");

    }
    public void TotengeldFactory()
    {
        if (Core.isCompletedBefore(3474) || !Core.isSeasonalMapActive("totengeld"))
            return;

        Story.PreLoad(this);

        // No More Chainsaws 3467
        Story.KillQuest(3467, "totengeld", "Chainsaw Sneevil");

        // Cage Rage 3468
        Story.MapItemQuest(3468, "totengeld", 2604, 6);

        // Factory Cleanup 3469
        Story.KillQuest(3469, "totengeld", "Toxic Waste");

        // Find A Way In 3470
        Story.KillQuest(3470, "totengeld", "Horc Minion");

        // More Like DourDrank 3471
        Story.KillQuest(3471, "totengeld", "Factory Sneevil");

        // Days Without Incident: 0 3472
        Story.MapItemQuest(3472, "totengeld", 2605, 4);
        Story.MapItemQuest(3472, "totengeld", 2606, 2);

        // Boot to the Head 3473
        Story.KillQuest(3473, "totengeld", "Horc Minion");

        // Defeat CEO Totengeld 3474
        if (!Core.isCompletedBefore(3474))
        {
            Core.HuntMonster("totengeld", "CEO Totengeld", log: false);
            Story.KillQuest(3474, "totengeld", "Mecha Totengeld");
        }
    }
}
