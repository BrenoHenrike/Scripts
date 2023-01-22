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
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;
public class GetAllRanks
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreToD TOD = new();
    public Core13LoC LOC => new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoGetAllRanks();

        Core.SetOptions(false);
    }


    public void DoGetAllRanks()
    {
        TOD.FourthDimensionalPyramid();
        TOD.BaconCatFortress();
        TOD.LaserSharkInvasion();
        LOC.Wolfwing();
        LOC.Kimberly();
        LOC.Lionfang();

        Farm.GetAllRanks();

    }
}
