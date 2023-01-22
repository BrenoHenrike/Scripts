/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class MasterRanger
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetMR();

        Core.SetOptions(false);
    }

    public void GetMR(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Master Ranger"))
            return;

        Farm.SandseaREP();

        Core.BuyItem("sandsea", 242, "Master Ranger");

        if (rankUpClass)
            Adv.rankUpClass("Master Ranger");
    }
}
