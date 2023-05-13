/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story\ShadowsOfWar\CoreSoW.cs
using Skua.Core.Interfaces;

public class CoreAOR
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    private CoreSoW SoW = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void DoAll()
    {
        TerminaTemple();
        AshrayVillage();
    }

    public void TerminaTemple()
    {
        if (Core.isCompletedBefore(9214))
            return;

        SoW.ManaCradle();

        Story.PreLoad(this);

        // Familiar Faces (9213)
        Story.KillQuest(9213, "terminatemple", "Termina Defender");
        Story.MapItemQuest(9213, "terminatemple", new[] { 11625, 11626, 11627 });

        // Loaded Resume (9214)
        Story.KillQuest(9214, "terminatemple", "Clandestine Guard");
        Story.MapItemQuest(9214, "terminatemple", new[] { 11628, 11629, 11630 });
    }

    public void AshrayVillage()
    {
        if (Core.isCompletedBefore(9234))
            return;

        TerminaTemple();

        Story.PreLoad(this);

        // Big ol' Eyes (9225)
        Story.KillQuest(9225, "ashray", "Kitefin Shark Bait");

        // Angry Angler (9226)
        Story.KillQuest(9226, "ashray", "Ashray Fisherman");
        Story.MapItemQuest(9226, "ashray", new[] { 11663, 11664 });

        // Slimy Scavenger (9227)
        Story.KillQuest(9227, "ashray", "Ghostly Eel");

        // Troubled Waters (9228)
        Story.KillQuest(9228, "ashray", "Stagnant Water");
        Story.MapItemQuest(9228, "ashray", 11665);

        // Washed Ink (9229)
        Story.KillQuest(9229, "ashray", "Ashray Fisherman");
        Story.MapItemQuest(9229, "ashray", 11666);

        // Fishy Hospitality (9230)
        Story.KillQuest(9230, "ashray", "Kitefin Shark Bait");

        // Doctoring Papers (9231)
        Story.KillQuest(9231, "ashray", "Ghostly Eel");
        Story.MapItemQuest(9231, "ashray", 11667);

        // Psychic Pollution (9232)
        Story.KillQuest(9232, "ashray", "Stagnant Water");
        Story.MapItemQuest(9232, "ashray", 11668);

        // Duck Dive (9233)
        Story.MapItemQuest(9233, "ashray", new[] { 11669, 11670 });

        // Faces in the Foam (9234)
        Story.KillQuest(9234, "ashray", "Seafoam Elemental");
    }
}
