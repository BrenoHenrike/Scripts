/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class Adam1a1Merge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetItems();

        Core.SetOptions(false);
    }

    public void GetItems()
    {
        //Needed AddDrop
        Core.AddDrop("Fresh Ectoplasm", "IOU Slip");

        while (!Bot.ShouldExit && !Core.CheckInventory("IOU Slip", 100) | !Core.CheckInventory("Fresh Ectoplasm", 300))
        {
            //Fresh Ecotplasm & IOU Slip
            Core.EnsureAccept(8009);
            Core.HuntMonster("vendorbooths", "Caffeine Imp", "Coffee Beans", 10);
            Core.HuntMonster("djinn", "Lamia", "Tasty Poison", 10);
            Core.HuntMonster("charredpath", "Toxic Wisteria", "Necessary Antidote");
            Core.EnsureComplete(8009);
        }
    }
}
