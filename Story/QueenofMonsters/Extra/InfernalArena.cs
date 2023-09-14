/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/CelestialArena.cs
using Skua.Core.Interfaces;

public class InfernalArena
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();
    private CelestialArenaQuests CAQuests = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoStory();

        Core.SetOptions(false);
    }

    public void DoStory()
    {
        Core.OneTimeMessage("SOLO-ONLY", "this map is solo only\n" +
        "and anything past the duo's will require potions\n" +
        "so the script wont go past that mob.\n" +
        "[DO IT YOURSELF]", messageBox: false);

        if (Core.isCompletedBefore(9373))
            return;

        CAQuests.DoAll();

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Solo);

        // Maligned Magus 9356
        Story.KillQuest(9356, "infernalarena", "Infernal Mage");

        // Reverent Revenger 9357
        Story.KillQuest(9357, "infernalarena", "Infernal Revenger");

        // Infernal Harbinger 9358
        Story.KillQuest(9358, "infernalarena", "Infernal Harbinger");

        // First of the Fallen 9359
        Story.KillQuest(9359, "infernalarena", "First Fallen");

        // Infernal Warlord 9360
        Story.KillQuest(9360, "infernalarena", "Fallen Warlord");

        // Abhorrent Aperitif 9361
        Story.KillQuest(9361, "infernalarena", "Malicious Maw");

        // Touch of Death 9362
        Story.KillQuest(9362, "infernalarena", "Wicked Rotfinger");

        // Dire Dreams 9363
        Story.KillQuest(9363, "infernalarena", "Dark Devourax");

        // Festering Forest 9364
        Story.KillQuest(9364, "infernalarena", "Corrupt Terror");

        // Abominable Butchery 9365
        Story.KillQuest(9365, "infernalarena", "Infernal Abominator");

        // Infernal Screech 9366
        Story.KillQuest(9366, "infernalarena", "Twisted Harpy");

        // Frosty Burns 9367
        Story.KillQuest(9367, "infernalarena", "Infernal Izotz");

        // Kramping Your Style 9368
        Story.KillQuest(9368, "infernalarena", "Infernal Krampus");

        // Defiled Destiny 9369
        Story.KillQuest(9369, "infernalarena", "Destructive Defiler");

        // Searing Snake Oil 9370
        Story.KillQuest(9370, "infernalarena", "Infernal Naga");

        // Ambivalent Affection 9371
        Story.KillQuest(9371, "infernalarena", "Accursed Agape");

        // From the Crux of Shadows 9372
        Story.KillQuest(9372, "infernalarena", "Accursed Apephyrx");

        // Reviled Returner 9373
        //this ones barely soloable so gl
        Story.KillQuest(9373, "infernalarena", "Deadly Duo");

        //Rest below the line require potions and fuck that 
        /*---------------------------------------------------
        // Reign of the Deer 9374
        Story.KillQuest(9374, "infernalarena", "Cervus Malus");

        // Ars Infernum 9375
        Story.KillQuest(9375, "infernalarena", "Key of Sholemoh");

        // Unrepentant Culler 9376
        Story.KillQuest(9376, "infernalarena", "Azalith's Scythe");

        // Lord of the Scarred Barrens  9377
        Story.KillQuest(9377, "infernalarena", "Na'al");
        */

    }
}
