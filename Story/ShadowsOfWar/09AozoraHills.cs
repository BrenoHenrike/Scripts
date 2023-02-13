/*
name: Aozora Hills
description: This will finish the Aozora Hills quest.
tags: story, quest, shadow-war, aozora-hills
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;

public class AozoraHills
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreSoW SoW = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SoW.AozoraHills();

        Core.SetOptions(false);
    }
}
