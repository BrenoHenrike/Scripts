/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/MemetsRealm/CoreMemet.cs
using Skua.Core.Interfaces;

public class DreamMaster
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public MemetsRealm Memet = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Memet.DreamMaster();

        Core.SetOptions(false);
    }
}
