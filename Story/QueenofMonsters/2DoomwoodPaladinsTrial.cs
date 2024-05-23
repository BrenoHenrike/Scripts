/*
name: Doomwood Paladins Trial
description: This will finish the Doomwood Paladins Trial quest.
tags: story, quest, queen-of-monsters, doomwood-paladins-trial
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs

using Skua.Core.Interfaces;

public class CompletePaladinsTrial
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    public CoreQOM QOM => new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        QOM.DoomwoodPaladinsTrial();

        Core.SetOptions(false);
    }
}
