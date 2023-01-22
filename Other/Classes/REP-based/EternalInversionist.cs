/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class EternalInversionist
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreToD TOD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetEI();

        Core.SetOptions(false);
    }

    public void GetEI(bool rankUpClass = true)
    {
        if (Core.CheckInventory(35602))
            return;

        TOD.FourthDimensionalPyramid();
        Adv.BuyItem("fourdpyramid", 1275, "Eternal Inversionist", shopItemID: 21138);
        if (rankUpClass)
        {
            Adv.GearStore();
            Core.Equip("Eternal Inversionist");
            Adv.rankUpClass("Eternal Inversionist");
            Adv.GearStore(true);
        }

    }
}
