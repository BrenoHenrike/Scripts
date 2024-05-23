/*
name: SwordMaster (Class)
description: This script will get SwordMaster class and rank it up to rank 10.
tags: sword master, swordmaster, legion, class, meditation, ynr
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
using Skua.Core.Interfaces;

public class SwordMaster
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreLegion Legion = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetSwordMaster();

        Core.SetOptions(false);
    }

    public void GetSwordMaster(bool rankUpClass = true)
    {
        if (Core.CheckInventory("SwordMaster"))
            return;

        Legion.FarmLegionToken(2000);
        Core.BuyItem("underworld", 238, "SwordMaster", 1);
        if (rankUpClass)
        {
            Adv.EnhanceItem("SwordMaster", EnhancementType.Lucky);
            Adv.RankUpClass("SwordMaster");
        }
    }
}
