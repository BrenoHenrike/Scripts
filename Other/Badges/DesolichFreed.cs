/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/EtherstormWastes.cs
using Skua.Core.Interfaces;

public class DesolichFreed
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public EtherStormWastes ESW = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (!Core.IsMember)
            return;

        if (Core.HasWebBadge("Desoloth Freed"))
        {
            Core.Logger($"Already have the Desoloth Freed badge");
            return;
        }

        Core.Logger("Gotta do the EtherStorm story first, will get the badge during the story don't worry :D");
        ESW.DoAll();
    }
}
