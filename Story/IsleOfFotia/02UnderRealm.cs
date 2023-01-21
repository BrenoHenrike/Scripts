/*
path: Story/IsleOfFotia/02UnderRealm.cs
fileName: 02UnderRealm.cs
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/IsleOfFotia/CoreIsleOfFotia.cs
using Skua.Core.Interfaces;

public class UnderRealm
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreIsleOfFotia CoreIsleOfFotia = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreIsleOfFotia.UnderRealm();

        Core.SetOptions(false);
    }
}
