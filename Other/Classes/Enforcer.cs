/*
name: Enforcer Class
description: This script farms the Enforcer class.
tags: dwakel,mithril man,crashsite,class
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class Enforcer
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetClass();
        Core.SetOptions(false);
    }

    public void GetClass(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Enforcer"))
        {
            Core.Logger("You already own Enforcer class.");
            return;
        }

        Core.HuntMonster("crashsite", "Mithril Man", "Enforcer", isTemp: false);

        if (rankUpClass)
            Adv.rankUpClass("Enforcer");
    }
}
