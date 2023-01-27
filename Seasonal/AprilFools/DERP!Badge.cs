/*
name: DERP! Badge
description: This will get the DERP! Badge.
tags: derp-badge, seasonal, april-fools
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DERPBadge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetBadge();

        Core.SetOptions(false);
    }

    public void GetBadge()
    {
        //Progress Check
        if (Core.isCompletedBefore(8007))
            return;
        if (!Core.isSeasonalMapActive("gardenquest"))
            return;

        Core.AddDrop(new[] { "Rainbow Derpicorn Guard (L)", "Rainbow Derpicorn Guard (R)" });
        Core.Logger("Hunting For Item: \"Rainbow Derpicorn Guard(R)\"");
        while (!Bot.ShouldExit && !Bot.House.Contains("Rainbow Derpicorn Guard (R)"))
        {
            Core.HuntMonster("battlefools", "Rainbow Derpicorn", log: false);
        }

        Core.EnsureAccept(8007);
        Core.HuntMonster("gardenquest", "Sketchy Chickencow", "Sketchy Chickencow Slain");
        Core.EnsureComplete(8007);

    }
}
