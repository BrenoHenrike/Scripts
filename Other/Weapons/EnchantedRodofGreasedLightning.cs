/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class EnchantedRodofGreasedLightning
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Core.HuntMonster("crashruins", "CluckMoo Idol", "Enchanted Rod of Greased Lightning", isTemp: false);

        Core.SetOptions(false);
    }
}
