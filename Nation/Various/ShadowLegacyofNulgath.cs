/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class ShadowLegacyofNulgath
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetSLoN();

        Core.SetOptions(false);
    }

    public void GetSLoN()
    {
        if (Core.CheckInventory("Shadow Legacy of Nulgath"))
            return;

        Core.AddDrop("Shadow Legacy of Nulgath", "Letter from Asuka and Tendou");

        Nation.ApprovalAndFavor(100, 0);
        Nation.Supplies("Voucher of Nulgath (non-mem)");
        Nation.EssenceofNulgath(100);
        Core.KillMonster("tercessuinotlim", "m4", "Right", "Shadow of Nulgath", "Hadean Onyx of Nulgath", 1, false);
        Core.HuntMonster("Citadel", "Burning Witch", "Letter from Asuka and Tendou", isTemp: false);
        Farm.Gold(3000000);
        Core.BuyItem("archportal", 1211, "Shadow Legacy of Nulgath");
        Bot.Wait.ForPickup("Shadow Legacy of Nulgath");
    }
}
