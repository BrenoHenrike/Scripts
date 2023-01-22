/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Legion/DageTheEvilIsland/CoreDageTheEvilIsland.cs

using Skua.Core.Interfaces;

public class Seraph
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDageTheEvilIsland CoreDageTheEvilIsland = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreDageTheEvilIsland.Seraph();

        Core.SetOptions(false);
    }

}
