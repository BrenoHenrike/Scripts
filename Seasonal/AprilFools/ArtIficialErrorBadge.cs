/*
name: Art-ificial Error Badge
description: This script will get the Art-ificial Error Badge.
tags: art, artificial, error, ai no miko, ai, ebilart, badge, seasonal, april-fools
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/AprilFools/EbilArt.cs
using Skua.Core.Interfaces;

public class ArtificialErrorBadge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private EbilArt EA = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetBadge();

        Core.SetOptions(false);
    }

    public void GetBadge()
    {
        if (Core.HasWebBadge(badge))
        {
            Core.Logger($"Already have the {badge} badge.");
            return;
        }

        EA.StoryLine();

        Core.EnsureAccept(9666);
        Core.GetMapItem(12883, 17, "ebilart");
        Core.EnsureComplete(9666);
    }
    private string badge = "Art-ificial Error";

}
