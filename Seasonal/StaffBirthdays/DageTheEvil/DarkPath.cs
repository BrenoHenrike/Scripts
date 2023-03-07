/*
name: Dark Path Story
description: This script completes the storyline in darkpath and voidvault.
tags: darkpath, voidvault, seasonal, dage, story
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DarkPath
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
        Story.KillQuest(6223, "darkpath", "Void Makai");

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
}
