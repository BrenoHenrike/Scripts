/*
name: Complete Memets Realm Story
description: This will finish the Memets Realm story.
tags: story, quest, memets-realm, complete, all
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/MemetsRealm/CoreMemet.cs
using Skua.Core.Interfaces;

public class DoAllMemet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public MemetsRealm Memet = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Memet.DoAll();

        Core.SetOptions(false);
    }
}
