/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CelestialPast
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CompleteCeletialPast();

        Core.SetOptions(false);
    }

    public void CompleteCeletialPast()
    {
        if (Core.isCompletedBefore(7681))
            return;

        Story.PreLoad(this);

        //Wayfarer Potion 7674
        Story.KillQuest(7674, "CelestialPast", new[] { "Blessed Deer", "Blessed Bear", "Blessed Centaur", "Blessed Hydra" });

        //The Path Revealed 7675
        Story.MapItemQuest(7675, "CelestialPast", 7592);
        Story.KillQuest(7675, "CelestialPast", "Blessed Deer");

        //Well of Knowledge 7676
        Story.MapItemQuest(7676, "CelestialPast", 7593);
        Story.KillQuest(7676, "CelestialPast", "Blessed Deer");

        //Gather the Artifacts 7677
        Story.MapItemQuest(7677, "CelestialPast", 7594);
        Story.KillQuest(7677, "CelestialPast", new[] { "Blessed Deer", "Blessed Bear", "Blessed Centaur", "Blessed Hydra" });

        //Oh Well, Oh Well Guardian 7678
        Story.MapItemQuest(7678, "CelestialPast", 7595);
        Story.KillQuest(7678, "CelestialPast", "Well Guardian");

        //Infernal Commands 7679
        Story.KillQuest(7679, "CelestialPast", "Infernal Soldier");

        //Forward Unto Azalith 7680
        Story.KillQuest(7680, "CelestialPast", "Infernal Soldier");

        //Azalith Faced 7681    [Confront Quest]
        Story.KillQuest(7681, "CelestialPast", "Azalith");

    }
}
