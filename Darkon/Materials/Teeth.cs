/*
name: Teeth (Darkon)
description: This script will farm Teeth using "Sweet Dreams Are Made of Teeth" quest.
tags: darkon, teeth, sweet dreams are made of teeth, tooth, wisdom tooth,eridani,re
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using Skua.Core.Interfaces;

public class Teeth
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDarkon Darkon = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Darkon.Teeth();

        Core.SetOptions(false);
    }
}
