/*
name: Dragonslayer Class
description: This script farms the Dragonslayer Class
tags: galanoth, class, lair, dsg, general, slayer
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Lair.cs
using Skua.Core.Interfaces;

public class Dragonslayer
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();
    private Lair Lair = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetDragonslayer();
        Core.SetOptions(false);
    }

    public void GetDragonslayer(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Dragonslayer"))
        {
            if (rankUpClass)
                Adv.RankUpClass("Dragonslayer");
            return;
        }

        Lair.Galanoth();
        Core.BuyItem("lair", 38, "Dragonslayer");
        if (rankUpClass)
            Adv.RankUpClass("Dragonslayer");
    }
}
