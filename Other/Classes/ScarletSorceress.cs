/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Other/Classes/BloodSorceress.cs
using Skua.Core.Interfaces;

public class ScarletSorceress
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm => new();
    public CoreAdvanced Adv = new();
    public CoreToD TOD = new();
    public BloodSorceress BS = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetSSorc();

        Core.SetOptions(false);
    }

    public void GetSSorc(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Scarlet Sorceress"))
            return;

        Core.AddDrop("Scarlet Sorceress");

        TOD.TowerofMirrors();
        BS.GetBSorc(false);
        Farm.Experience(50);

        Core.ChainComplete(6236);
        Bot.Wait.ForPickup("Scarlet Sorceress");

        if (rankUpClass)
            Adv.rankUpClass("Scarlet Sorceress");
    }
}
