/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ChaosLabStory
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        ChaosLabQuests();

        Core.SetOptions(false);
    }

    public void ChaosLabQuests()
    {
        if (Core.isCompletedBefore(3567))
            return;

        Story.PreLoad(this);

        //Survey the Damage 3556
        Story.MapItemQuest(3556, "chaoslab", new[] { 2704, 2706, 2707, 2708, });

        //Defeat Chaorrupted Alina 3557
        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(3557, "chaoslab", "Chaos Alina");

        //Gather the Crystals 3558
        Story.KillQuest(3558,"chaoslab", "Chaorrupted Moglin");

        //Cleanse Alina 3559
        Story.KillQuest(3559, "chaoslab", "Chaorrupted Alina");

        //Gather More Cystals 3560
        Story.KillQuest(3560, "chaoslab", "Chaorrupted Moglin");

        //Cleanse Beleen 3561
        Story.KillQuest(3561, "chaoslab", "Chaorrupted Beleen");

        //Cleanse Cysero 3562
        Story.KillQuest(3562, "chaoslab", "Chaorrupted Cysero");

        //Cleanse Artix 3563
        Story.KillQuest(3563, "chaoslab", "Chaorrupted Artix");

        //Ficus Your Powers 3564
        Story.KillQuest(3564, "chaoslab", "Chaorrupted Ficus");
        
        //Face the Hamster 3565
        Story.KillQuest(3565, "chaoslab", "Chaorrupted Server Hamster");

        //Cleaning Up 3566
        Story.KillQuest(3566, "chaoslab", new[] { "Chaorrupted Alina", "Chaoruppted Beleen", "Chaorrupted Cysero", "Chaorrupter Artix"});
        
        //MORE HAMSTER? 3567
        Story.KillQuest(3560, "chaoslab", "Ultra Chaotic Server Hamster");
    }
}
