/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class YearBall
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetDrops();

        Core.SetOptions(false);
    }

    public void GetDrops()
    {
        if (!Core.isSeasonalMapActive("newyear"))
            return;

        string[] ACNonmem = {
            "AntiRetrograde Clock",
            "Aurora Caller",
            "Aurora Caller's Bow",
            "Aurora Caller's Cape",
            "Aurora Caller's Hat",
            "Aurora Caller's Helm",
            "Aurora Caller's Rune",
            "Aurora Caller's Staff",
            "Aurora Caller's Wings",
            "Axeros' Snow Globe Wand",
            "Chaotic Chrono Eye",
            "Elegant Bejeweled Cane",
            "Frigid Wolf Spear",
            "Glacial Knight",
            "Glacial Knight Cape",
            "Glacial Knight Crown",
            "Glacial Knight Crown Locks",
            "Glacial Knight Hair",
            "Glacial Knight Locks",
            "Glacial Knight Rune",
            "Glacial Knight Rune Sword Cape",
            "Glacial Knight Shag",
            "Glacial Knight Sword",
            "Glacial Knight Swords",
            "Nascent ChronoWeaver's Runes",
            "New Year Dawning",
            "Party Time Twig Pet",
            "Sheathed Glacial Knight Cape",
            "Timeseeker's Blades",
            "Timeseeker's Staff"
        };

        if (Core.CheckInventory(ACNonmem, toInv: false))
        {
            Core.Logger("You already have all of the items.");
            return;
        }

        foreach (string Reward in ACNonmem)
        {
            Core.HuntMonster("newyear", "2023 Ball", Reward, isTemp: false);
            Core.ToBank(Reward);
        }
    }

}
