/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class TempleSiege
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        CompleteTempleSiege();
        Core.SetOptions(false);
    }

    public void CompleteTempleSiege()
    {
        if (Core.isCompletedBefore(9067))
            return;

        if (!Core.isSeasonalMapActive("templesiege"))
            return;

        Story.PreLoad(this);

        //Fairweather Elementals || 9058
        Story.KillQuest(9058, "templesiege", "Light Elemental");
        Story.MapItemQuest(9058, "templesiege", 11129);

        //In Case of Emergency || 9059
        Story.KillQuest(9059, "templesiege", "Overdriven Paladin");

        //Blind Devotion || 9060
        Story.KillQuest(9060, "templesiege", new[] { "Overdriven Paladin", "Light Elemental" });

        //Blessed Visage || 9061
        Story.MapItemQuest(9061, "templesiege", new[] { 11130, 11131, 11132 });

        //Banging on Light's Door || 9062
        Story.KillQuest(9062, "templesiege", new[] { "Overdriven Paladin", "Light Elemental" });

        //Words Bind || 9063
        Story.MapItemQuest(9063, "templesiege", 11133, 6);
        Story.MapItemQuest(9063, "templesiege", 11134);

        //Doomed Darkness || 9064
        Story.KillQuest(9064, "templesiege", "Doomed Beast");

        //Shady Costuming || 9065
        Story.KillQuest(9065, "templesiege", "Doomed Troll");

        //The Emperor's Shadow || 9066
        Story.KillQuest(9066, "templesiege", new[] { "Doomed Beast", "Doomed Troll" });

        //Throat Sores || 9067
        Story.KillQuest(9067, "templesiege", "Doomed Oblivion");
    }
}
