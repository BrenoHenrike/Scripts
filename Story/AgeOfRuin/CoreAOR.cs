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
        SunlightZone();
        TwilightZone();
        YulgarAria();
        MidnightZone();
        AbyssalZone();
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

    public void SunlightZone()
    {
        if (Core.isCompletedBefore(9251))
            return;

        AshrayVillage();

        Story.PreLoad(this);

        // Detergent Shortage (9242)
        Story.KillQuest(9242, "sunlightzone", "Blighted Water");

        // Ghost in the Machine (9243)
        Story.KillQuest(9243, "sunlightzone", "Spectral Jellyfish");

        // Efficient Division (9244)
        Story.KillQuest(9244, "sunlightzone", "Blighted Water");
        Story.MapItemQuest(9244, "sunlightzone", new[] { 11705, 11706 });

        // Tech Illiterate (9245)
        Story.KillQuest(9245, "sunlightzone", "Spectral Jellyfish");
        Story.MapItemQuest(9245, "sunlightzone", 11707, 3);

        // Plugging Leaks (9246)
        Story.KillQuest(9246, "sunlightzone", new[] { "Spectral Jellyfish", "Blighted Water" });

        // Shared History (9247)
        Story.MapItemQuest(9247, "sunlightzone", new[] { 11708, 11709, 11710 });

        // Flat Scares (9248)
        Story.KillQuest(9248, "sunlightzone", "Astravian Illusion");
        Story.MapItemQuest(9248, "sunlightzone", 11711);

        // Fishy Bully (9249)
        Story.KillQuest(9249, "sunlightzone", "Infernal Illusion");
        Story.MapItemQuest(9249, "sunlightzone", 11712, 5);

        // Faint Howls (9250)
        Story.MapItemQuest(9250, "sunlightzone", 11713);
        Story.KillQuest(9250, "sunlightzone", "Seraphic Illusion");

        // Down the Digestive Tract (9251)
        Story.KillQuest(9251, "sunlightzone", "Marine Snow");
    }

    public void TwilightZone()
    {
        if (Core.isCompletedBefore(9268))
            return;

        SunlightZone();

        Story.PreLoad(this);

        // Marshmallows With Bite (9258)
        Story.KillQuest(9258, "twilightzone", "Whale Louse");

        // Meaty Cold Spaghetti (9259)
        Story.KillQuest(9259, "twilightzone", "Polymelia Lamprey");

        // Songs in the Seams (9260)
        Story.MapItemQuest(9260, "twilightzone", 11749);
        Story.MapItemQuest(9260, "twilightzone", 11750, 4);

        // Parched Throats (9261)
        Story.KillQuest(9261, "twilightzone", new[] { "Whale Louse", "Polymelia Lamprey" });

        // Morning Stretches (9262)
        Story.MapItemQuest(9262, "twilightzone", new[] { 11751, 11752 });

        // Natural Empathy (9263)
        Story.KillQuest(9263, "twilightzone", "Decay Spirit");

        // Comfort Blanket of Snow (9264)
        Story.KillQuest(9264, "twilightzone", "Ice Guardian");

        // Whale Watching (9265)
        Story.MapItemQuest(9265, "twilightzone", new[] { 11753, 11754, 11755 });

        // Exhausted Spirits (9266)
        Story.KillQuest(9266, "twilightzone", new[] { "Decay Spirit", "Ice Guardian" });

        // Singing to Whales (9267)
        Story.KillQuest(9267, "twilightzone", "Leviathan");

        // The Sea's Commitment (9268)
        Story.MapItemQuest(9268, "twilightzone", 11756);
    }

    public void YulgarAria()
    {
        if (Core.isCompletedBefore(9274))
            return;

        TwilightZone();

        Story.PreLoad(this);

        // Octotree (9270)
        Story.KillQuest(9270, "twilightzone", "Polymelia Lamprey");

        // Thirsty Roots (9271)
        Story.KillQuest(9271, "sunlightzone", "Blighted Water");

        // Dollar Store Mogloween Costume (9272)
        Story.KillQuest(9272, "sunlightzone", new[] { "Astravian Illusion", "Infernal Illusion" });

        // Sea Snow Angels (9273)
        Story.KillQuest(9273, "sunlightzone", "Marine Snow");

        // Ten Klicks (9274)
        Story.KillQuest(9274, "twilightzone", "Leviathan");
    }

    public void MidnightZone()
    {
        if (Core.isCompletedBefore(9301))
            return;

        TwilightZone();

        Story.PreLoad(this);

        // Motivation Malady (9292)
        Story.MapItemQuest(9292, "midnightzone", new[] { 11842, 11843, 11844 });

        // Radical Renovation (9293)
        Story.KillQuest(9293, "midnightzone", "Polymelia Lamprey");
        Story.MapItemQuest(9293, "midnightzone", 11845);

        // Graveyard Shift (9294)
        Story.KillQuest(9294, "midnightzone", new[] { "Vowed ShadowSlayer", "Vowed ShadowSlayer" });

        // Educational Execution (9295)
        Story.MapItemQuest(9295, "midnightzone", 11846);
        Story.KillQuest(9295, "midnightzone", "Undead Prisoner");

        // Vows For Ignorance (9296)
        Story.KillQuest(9296, "midnightzone", new[] { "Undead Prisoner", "Vowed ShadowSlayer" });
        Story.MapItemQuest(9296, "midnightzone", 11847);

        // Protein Shake (9297)
        Story.MapItemQuest(9297, "midnightzone", 11848, 3);
        Story.KillQuest(9297, "midnightzone", "Shadow Viscera");

        // Duty Beyond Death (9298)
        Story.KillQuest(9298, "midnightzone", "Venerated Wraith");

        // Designated Taunters (9299)
        Story.KillQuest(9299, "midnightzone", new[] { "Venerated Wraith", "Shadow Viscera" });

        // Beloved Simulacrum (9230)
        Story.MapItemQuest(9300, "midnightzone", 11849, 4);
        Story.MapItemQuest(9300, "midnightzone", 11850);

        // Roko's Royal Basilisk (9301)
        Story.KillQuest(9301, "midnightzone", "Sparagmos");
    }

    public void AbyssalZone()
    {
        if (Core.isCompletedBefore(9315))
            return;

        MidnightZone();

        Story.PreLoad(this);

        // Shark Kiting (9306)
        Story.KillQuest(9306, "abyssalzone", "Kitefin Shark Bait");
        Story.MapItemQuest(9306, "abyssalzone", 11914);

        // Suckered Blockade (9307)
        Story.MapItemQuest(9307, "abyssalzone", 11893, 6);
        Story.MapItemQuest(9307, "abyssalzone", 11894);

        // Digestive Fluids (9308)
        Story.KillQuest(9308, "abyssalzone", "Blighted Water");

        // Sodden Secrets (9309)
        Story.KillQuest(9309, "abyssalzone", "Shadow Viscera");
        Story.MapItemQuest(9309, "abyssalzone", 11895, 3);

        // The Hidden Corpse (9310)
        Story.KillQuest(9310, "abyssalzone", new[] { "Shadow Viscera", "Blighted Water" });

        // Octo-Flake Fish Feed (9311)
        Story.KillQuest(9311, "abyssalzone", "Foam Scavenger");
        Story.MapItemQuest(9311, "abyssalzone", 11896);

        // Completely Surrounded (9312)
        Story.MapItemQuest(9312, "abyssalzone", 11897, 6);
        Story.MapItemQuest(9312, "abyssalzone", 11898);

        // Sea Salt Soap (9313)
        Story.KillQuest(9313, "abyssalzone", "Necro Adipocere");

        // In the Grip of Justice (9314)
        Story.KillQuest(9314, "abyssalzone", new[] { "Necro Adipocere", "Foam Scavenger" });
        Story.MapItemQuest(9314, "abyssalzone", 11899);

        // Together as One (9315)
        Story.KillQuest(9315, "abyssalzone", "The Ashray");
    }
}
