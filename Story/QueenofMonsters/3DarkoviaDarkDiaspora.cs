/*
name: Darkovia Dark Diaspora
description: This will finish the Darkovia Dark Diaspora quest.
tags: story, quest, queen-monsters, darkovia-dark-diaspora
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs

using Skua.Core.Interfaces;

public class CompleteDarkDiaspora
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    public CoreQOM QOM => new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        QOM.DarkoviaDarkDiaspora();

        Core.SetOptions(false);
    }
}
