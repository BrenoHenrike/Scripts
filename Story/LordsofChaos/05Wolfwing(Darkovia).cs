/*
name: (Darkovia) Wolfwing
description: This will finish the Wolfwing quest.
tags: story, quest, chaos-saga, 13-lords-of-chaos, darkovia, wolfwing
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class SagaDarkovia
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public Core13LoC LOC => new();
    public CoreAdvanced Adv = new();
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        LOC.Wolfwing();

        Core.SetOptions(false);
    }
}
