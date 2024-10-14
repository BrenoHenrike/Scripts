/*
name: Shadowbattleon
description: Does the Shadow of Doom Saga
tags: shadow of doom, shadowbattleon, story, saga, doall, camlan
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/BlackFriday/ShadowofDoom/CoreShadowofDoom.cs
using Skua.Core.Interfaces;

public class Shadowbattleon
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();
    private CoreShadowofDoom CoreSoD = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        CoreSoD.ShadowBattleon();

        Core.SetOptions(false);
    }

    
}
