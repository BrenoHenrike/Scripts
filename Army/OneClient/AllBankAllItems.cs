/*
name: All accs bank all
description: banks all items on all accs in the "thefamily.txt" file.
tags: bank, army, all
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Tools/BankAllItems.cs
using Skua.Core.Interfaces;

public class ArmyBankAllItems
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private BankAllItems BAI = new();
    private CoreArmyLite Army = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        AllBankAll();

        Core.SetOptions(false);
    }

    public void AllBankAll()
    {
        while (!Bot.ShouldExit && Army.doForAll())
        {
            BAI.BankAll(true, true, false, string.Empty);
            Core.Logger("All \"Allowed\" items banked, onto the next Acc");

            // if either are enabled set them to empty, so the next acc doesnt try to 
            if (Core.FarmGearOn && Core.FarmGear.Length > 0)
                Core.FarmGear = Array.Empty<string>();
            if (Core.SoloGearOn && Core.SoloGear.Length > 0)
                Core.SoloGear = Array.Empty<string>();
        }

    }
}
