/*
name: FreakiTiki
description: This will finish the FreakiTiki quest.
tags: story, quest, memets-realm, freakitiki
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/MemetsRealm/CoreMemet.cs
using Skua.Core.Interfaces;

public class FreakiTiki
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public MemetsRealm Memet = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Memet.FreakiTiki();

        Core.SetOptions(false);
    }
}
