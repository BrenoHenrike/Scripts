/*
name: Paladin
description: This bot farms the Paladin class for you.
tags: warrior, healer, paladin, class
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class Paladin
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetPaladin();

        Core.SetOptions(false);
    }

    public void GetPaladin(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Paladin"))
            return;

        Core.BuyItem("necropolis", 26, "Warrior");
        Core.BuyItem("necropolis", 26, "Healer");

        Adv.rankUpClass("Warrior");
        Adv.rankUpClass("Healer");

        Farm.GoodREP(5);

        Core.BuyItem("necropolis", 26, "Paladin");

        if (rankUpClass)
            Adv.rankUpClass("Paladin");
    }
}
