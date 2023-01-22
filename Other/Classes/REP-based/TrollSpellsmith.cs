/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class TrollSpellsmith
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetTS();

        Core.SetOptions(false);
    }

    public void GetTS(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Troll Spellsmith"))
            return;

        Adv.BuyItem("bloodtusk", 306, "Troll Spellsmith");

        if (rankUpClass)
            Adv.rankUpClass("Troll Spellsmith");
    }
}
