/*
name: Doom Pirate
description: This script will complete the storyline in /doompirate
tags: doom, pirate, story, dusk, doompirate, gallaeon
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
        if (!Story.QuestProgression(9353))
        {
            Core.EquipClass(ClassType.Farm);
            Core.EnsureAccept(9353);
            Core.HuntMonsterMapID("doompirate", 1, "Silver Doubloon", 6);
            Core.EnsureComplete(9353);
        }

        // Chasing Crimson October (9354)
        if (!Story.QuestProgression(9354))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(9354);
            Core.HuntMonsterMapID("doompirate", 3, "Dented Naval Medal");
            Core.EnsureComplete(9354);
        }
    }
}
