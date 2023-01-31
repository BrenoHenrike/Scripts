/*
name: Nightmare Mem
description: This will finish the Nightmare Mem quest.
tags: story, quest, memets-realm, nightmare-mem
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/MemetsRealm/CoreMemet.cs
using Skua.Core.Interfaces;

public class NightmareMem
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public MemetsRealm Memet = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Memet.NightmareMem();

        Core.SetOptions(false);
    }
}
