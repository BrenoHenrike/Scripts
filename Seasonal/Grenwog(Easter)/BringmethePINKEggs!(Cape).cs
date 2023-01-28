/*
name: The Eggcubator
description: This will finish the Bring me the PINK Eggs! quest to obtain The Eggcubator Cape.
tags: the-eggcubator, bring-me-the-pink-eggs, seasonal, easter
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class TheEggcubator
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetPINKEggs();

        Core.SetOptions(false);
    }

    public void GetPINKEggs()
    {
        if (Core.CheckInventory("The Eggcubator"))
        {
            Core.Logger("You already own this item, Stopping Bot.");
            return;
        }

        Core.EnsureAccept(5784);
        Core.HuntMonster("Mythsong", "Pink Chick", "Pink Chick");
        Core.GetMapItem(5222, 2, "Mythsong");
        Core.EnsureCompleteChoose(5784);
    }
}



