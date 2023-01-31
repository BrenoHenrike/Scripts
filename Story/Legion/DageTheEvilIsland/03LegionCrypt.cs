/*
name: Legion Crypt
description: This will finish the Legion Crypt quest.
tags: story, quest, legion, dage-the-evil-island, legion-crypt
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Legion/DageTheEvilIsland/CoreDageTheEvilIsland.cs

using Skua.Core.Interfaces;

public class LegionCrypt
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDageTheEvilIsland CoreDageTheEvilIsland = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreDageTheEvilIsland.LegionCrypt();

        Core.SetOptions(false);
    }

}
