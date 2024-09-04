/*
name: Shadowrealm
description: This script will complete the Whispering Helmet's storyline in /shadowrealm.
tags: hollowborn, saga, lae, whispering, helmet, whispering helmet, treasure, hunt, treasure hunt
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Hollowborn/CoreHollowbornStory.cs
using Skua.Core.Interfaces;

public class Shadowrealm
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreHollowbornStory HB = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        HB.Shadowrealm();

        Core.SetOptions(false);
    }
}
