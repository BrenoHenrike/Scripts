/*
name:  Army Free 500 accs
description:  the 500 free acs quest 
tags: acs, free, thefamily, army.
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;

public class ArmyFreeAcs
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        FreeAcs();

        Core.SetOptions(false);
    }

    public void FreeAcs()
    {
        while (Army.doForAll())
        {
            Farm.Experience(20);
            if (!Core.isCompletedBefore(9057))
            {
                Core.EnsureAccept(9057);
                Core.KillMonster("battleontown", "Enter", "Spawn", "Frogzard", "Free AC Giftbox");
                Core.EnsureComplete(9057);
            }            
        }
    }
}
