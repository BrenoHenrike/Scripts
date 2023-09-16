/*
name: Doom Pirate
description: This script will complete the storyline in /doompirate
tags: doom,pirate,story,dusk,doompirate,gallaeon
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DoomPirate
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
        if (!Core.isSeasonalMapActive("doompirate") || Core.isCompletedBefore(9354))
            return;

        Story.PreLoad(this);

        // Spare Hydra Heads (9353)
        Story.KillQuest(9353, "doompirate", "Hydra Swabbie");

        // Chasing Crimson October (9354)
        Story.KillQuest(9354, "doompirate", "Gallaeon");
    }
}
