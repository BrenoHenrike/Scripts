/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class SmartEnhance
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions(disableClassSwap: true);

        DoSmartEnhance();

        Core.SetOptions(false);
    }

    public void DoSmartEnhance()
    {
        if (Core.CBOBool("DisableAutoEnhance", out bool _disableAutoEnhance) && _disableAutoEnhance)
            return;

        Adv.SmartEnhance(Bot.Player.CurrentClass.Name);
    }
}
