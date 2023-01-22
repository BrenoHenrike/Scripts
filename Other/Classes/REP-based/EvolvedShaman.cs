/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class EvolvedShaman
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetES();

        Core.SetOptions(false);
    }

    public void GetES(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Evolved Shaman"))
            return;

        Farm.ArcangroveREP();

        Core.BuyItem("arcangrove", 214, "Evolved Shaman", shopItemID: 6396);

        if (rankUpClass)
            Adv.rankUpClass("Evolved Shaman");
    }
}
