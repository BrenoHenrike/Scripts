/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class SpirePast
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(6834))
            return;

        Story.PreLoad(this);

        //Battle Azal (6824)
        Story.KillQuest(6824, "dreadrock", "Azal the Infernal");

        //Journal Entry 1 (6825)
        Story.KillQuest(6825, "spirepast", "Infernal Imp");

        //Journal Entry 15 (6826)
        Story.KillQuest(6826, "spirepast", "Infernal Imp");

        //Journal Entry 56 (6827)
        Story.KillQuest(6827, "spirepast", "Rookie Guard");

        //Journal Entry 77 (6828)
        Story.KillQuest(6828, "spirepast", "Infernal Imp");

        //Journal Entry 151 (6829)
        Story.KillQuest(6829, "spirepast", "Celestial Knight");

        //Journal Entry 324 (6830)
        Story.KillQuest(6830, "spirepast", "Celestial Knight");

        //Journal Entry 583 (6831)
        Story.KillQuest(6831, "spirepast", new[] { "Infernal Guard", "Infernal Hound" });

        //Journal Entry 600 (6832)
        Story.KillQuest(6832, "spirepast", "Infernal Hound");

        //Journal Entry 826 (6833)
        Story.KillQuest(6833, "spirepast", "Celestial Knight");

        //Journal Entry 1000 (6834)
        Story.KillQuest(6834, "spirepast", "Infernal Guard Captain");
    }
}
