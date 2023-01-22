/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class UltraCarnaxBadge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (Core.HasWebBadge(badge))
        {
            Core.Logger($"Already have the {badge} badge");
            return;
        }

        if (!Core.IsMember)
        {
            Core.Logger("You are not a member!");
            return;
        }

        Core.Logger($"Doing UltraCarnax story for {badge} badge");
        // Ultra Carnax Challenge Fight! 2388 
        Story.KillQuest(2388, "ultracarnax", "Ultra-Carnax");
    }

    private string badge = "Ultra-Carnax";
}
