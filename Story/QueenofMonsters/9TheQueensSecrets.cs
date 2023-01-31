/*
name: The Queen's Secrets
description: This will finish the The Queen's Secrets quest.
tags: story, quest, queen-of-monsters, the-queens-secrets
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
using Skua.Core.Interfaces;

public class TheQueensSecrets
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreQOM QOM => new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        QOM.TheQueensSecrets();

        Core.SetOptions(false);
    }
}
