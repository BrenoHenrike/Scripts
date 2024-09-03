/*
name: PockeyMogs Story
description: Compeletes the PockeyMogs Story
tags: lim, pockeymogs, 
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class PockeyMogsStory
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        PockeyMogs();
        Core.SetOptions(false);
    }

    public void PockeyMogs()
    {
        if (Core.isCompletedBefore(5272))
            return;

        Story.PreLoad(this);

        //Mogzard 5261
        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(5261, "greenguardwest", "Mogzard");

        //Fishlin 5262
        Story.KillQuest(5262, "river", "Fishlin");

        //Groglin 5263
        Story.KillQuest(5263, "sewer", "Groglin");

        //Mogdrake 5264
        Story.KillQuest(5264, "willowcreek", "Mogdrake");

        //Mucklin 5265
        Story.KillQuest(5265, "swordhaven", "Mucklin");

        //Linix 5266
        Story.KillQuest(5266, "arcangrove", "Linix");

        //Pockey-Chew 5267
        Story.MapItemQuest(5267, "crossroads", 4613);
        
        //Pukasnooze vs the Moglurker 5268
        Story.KillQuest(5268, "pockeymogs", "Moglurker");

        //Pukasnooze vs Zaplin 5269
        Story.KillQuest(5269, "pockeymogs", "Zaplin");

        //Pukasnooze vs Blood Moggot 5270
        Story.KillQuest(5270, "pockeymogs", "Blood Moggot");

        //Pukasnooze vs Flamog 5271
        Story.KillQuest(5271, "pockeymogs", "Flamog");

        //Pukasnooze vs Vizally 5272
        Story.KillQuest(5272, "pockeymogs", "Vizally");
    }
}
