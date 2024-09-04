/*
name: Lost Villa
description: This script will complete the questline in /lostvilla.
tags: lostvilla, diabolical, story, hikari, ira
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Banished.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs
using Skua.Core.Interfaces;

public class LostVilla
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();
    private Banished Ban = new();
    private CoreQOM CoreQOM = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(9570))
            return;
            
        CoreQOM.TheBook();
        Ban.HimisQuests();

        Story.PreLoad(this);

        // Scout Ahead (9561)
        Story.MapItemQuest(9561, "lostvilla", new[] { 12649, 12650 });
        Story.KillQuest(9561, "lostvilla", "Eldritch Parasite");

        // Gluttonous Slithering (9562)
        Story.KillQuest(9562, "lostvilla", "Gorewyrm");

        // Relics of Bygone Days (9563)
        Story.KillQuest(9563, "lostvilla", "Eldritch Parasite");
        Story.MapItemQuest(9563, "lostvilla", 12651);

        // Rapacious Desires (9564)
        Story.MapItemQuest(9564, "lostvilla", 12652, 3);
        Story.KillQuest(9564, "lostvilla", "Diabolical Hoard");

        // Uninvited Guests (9565)
        Story.KillQuest(9565, "lostvilla", new[] { "Eldritch Parasite", "Gorewyrm" });

        // Key of Life (9566)
        Story.MapItemQuest(9566, "lostvilla", 12653);
        Story.KillQuest(9566, "lostvilla", "Diabolical Hoard");

        // Proper Procedures (9567)
        Story.KillQuest(9567, "noxustower", "Lightguard Caster");
        Story.MapItemQuest(9567, "lostvilla", 12654);

        // A Peculiar Feast (9568)
        Story.KillQuest(9568, "lostvilla", "Mutilated Atrocity");

        // Clingy Creatures (9569)
        Story.KillQuest(9569, "lostvilla", "Eldritch Parasite");

        // Disfigured Aristrocrat (9570)
        Story.KillQuest(9570, "lostvilla", "Covetous Disgrace");
    }
}
