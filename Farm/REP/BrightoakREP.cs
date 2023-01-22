/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/BrightOak.cs

using Skua.Core.Interfaces;
public class BrightoakREP
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public BrightOak BrightOak = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoRep();

        Core.SetOptions(false);
    }

    public void DoRep()
    {

        BrightOak.doall();
        Farm.BrightoakREP();

    }
}
