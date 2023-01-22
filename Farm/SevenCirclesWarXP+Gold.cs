/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
using Skua.Core.Interfaces;

public class SevenCirclesWarXP
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public SevenCircles SC = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        
        DoSevenCirclesWarXP();

        Core.SetOptions(false);
    }

    public void DoSevenCirclesWarXP()
    {
        SC.Circles();

        Adv.BestGear(GearBoost.exp);
        Adv.BestGear(GearBoost.gold);
        //Farm.UseBoost(ChangeToBoostID, Skua.Core.Models.Items.BoostType.Experience, true);

        Farm.SevenCirclesWar(Bot.Player.Level == 100 ? 101 : 100, 100000000);

    }
}
