/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class Arcangrove
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public Core13LoC LoC => new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GravelynandVictoria();

        Core.SetOptions(false);
    }

    public void GravelynandVictoria()
    {
        if (Core.isCompletedBefore(3375))
            return;

        LoC.Ledgermayne();
        LoC.Escherion();
        LoC.Vath();


        Story.PreLoad(this);

        // Gravelyn and Victoria Quests

        //Like Fireflies 3371
        Story.KillQuest(3371, "arcangrove", "Chaos Sprites");

        //Like Siege Engines 3372
        Story.KillQuest(3372, "arcangrove", "Gorillaphant");

        //Plants vs Chaos 3373
        Story.MapItemQuest(3373, "arcangrove", 2505, 6);
        Story.KillQuest(3373, "arcangrove", "Seed Spitter");

        //Like Aloe... With Teeth 3374
        Story.KillQuest(3374, "arcangrove", "Seed Spitter");

        //Kinda Like a Giant Bubble 3375
        Story.MapItemQuest(3375, "arcangrove", new[] { 2514, 2515, 2516, 2517 });

    }
}
