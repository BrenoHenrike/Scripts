/*
name: DoomKnight Class[Mem]
description: This script will get DoomKnight Class [Member]
tags: doomknight, class, evil, doom knight, member
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class DoomKnight
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetDoomKnight();

        Core.SetOptions(false);
    }

    public void GetDoomKnight(bool rankUpClass = true)
    {
        if (Core.CheckInventory("DoomKnight"))
            return;

        Core.BuyItem("necropolis", 26, "Warrior");
        Core.BuyItem("necropolis", 26, "Healer");

        Adv.RankUpClass("Warrior");
        Adv.RankUpClass("Healer");

        Farm.EvilREP(5);

        Core.BuyItem("shadowfall", 100, "DoomKnight", shopItemID: 6309);

        if (rankUpClass)
            Adv.RankUpClass("DoomKnight");
    }
}
