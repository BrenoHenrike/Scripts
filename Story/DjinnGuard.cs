/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/DjinnGate.cs
using Skua.Core.Interfaces;

public class DjinnGuard
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public DjinnGateStory DjinnGateStory = new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CompleteDjinnGuard();

        Core.SetOptions(false);
    }

    public void CompleteDjinnGuard()
    {
        DjinnGateStory.DjinnGate();
        if (Core.isCompletedBefore(6274))
            return;

        Story.PreLoad(this);

        //Trial of Water 6270
        Story.KillQuest(6270, "DjinnGuard", "Jaan al Bahar");

        //Trial of Air 6271
        Story.KillQuest(6271, "DjinnGuard", "Jaan al Hawa");

        //Trial of Earth 6272
        Story.KillQuest(6272, "DjinnGuard", "Jaan al Ard");

        //Trial of Fire 6273
        Story.KillQuest(6273, "DjinnGuard", "Jaan al Nair");

        //Blessing of the Guardian 6274
        Story.KillQuest(6274, "DjinnGuard", "Image of Crulon");

    }
}
