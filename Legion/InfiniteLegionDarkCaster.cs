/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
using Skua.Core.Interfaces;
public class InfiniteLegionDC
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreLegion Legion = new CoreLegion();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetILDC();

        Core.SetOptions(false);
    }

    public void GetILDC(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Infinite Legion Dark Caster"))
            return;

        Legion.FarmLegionToken(2000);
        Core.BuyItem("underworld", 238, "Infinite Legion Dark Caster");

        if (rankUpClass)
            Adv.rankUpClass("Infinite Legion Dark Caster");
    }
}
