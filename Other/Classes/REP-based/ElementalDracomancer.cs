/*
name: Elemental Dracomancer Class
description: This script farms the Elemental Dracomancer class.
tags: dracomancer, class, etherstorm
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class ElementalDracomancer
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetED();
        Core.SetOptions(false);
    }

    public void GetED(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Elemental Dracomancer"))
        {
            Core.Logger("You already own Elemental Dracomancer class.");
            return;
        }

        Adv.BuyItem("dragontown", 1285, 11272);

        if (rankUpClass)
            Adv.RankUpClass("Elemental Dracomancer");
    }
}
