/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class Bard
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public Core13LoC LOC => new Core13LoC();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetBard();

        Core.SetOptions(false);
    }

    public void GetBard(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Bard"))
            return;

        LOC.Kimberly();
        Farm.MythsongREP(4);

        Core.BuyItem("mythsong", 186, "Bard");

        if (rankUpClass)
            Adv.rankUpClass("Bard");
    }
}
