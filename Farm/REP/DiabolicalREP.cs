/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
public class DiabolicalREP
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoRep();

        Core.SetOptions(false);
    }

    private void UnlockDiabolical()
    {
        if (!Bot.Quests.IsUnlocked(7877))
        {
            Story.KillQuest(7875, "timevoid", "Unending Avatar");
            Story.KillQuest(7876, "twilightedge", "ChaosWeaver Warrior");
        }
    }

    public void DoRep()
    {
        UnlockDiabolical();
        Farm.DiabolicalREP();

    }
}
