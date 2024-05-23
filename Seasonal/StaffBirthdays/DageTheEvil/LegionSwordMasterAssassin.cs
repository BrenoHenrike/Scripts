/*
name: Legion Sword Master Assassin
description: farms the required materials for the class: "legion swordmaster assassin"
tags: legion, class, darkbirthday, legion swordmaster assassin
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/Various/SoulSand.cs
//cs_include Scripts/Legion/Various/LegionBonfire.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class LegionSwordMasterAssassin
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreLegion Legion = new();
    public CoreAdvanced Adv = new();
    public AnotherOneBitesTheDust SSand = new();
    public LegionBonfire Bon = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetClass();

        Core.SetOptions(false);
    }


    public void GetClass(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Legion SwordMaster Assassin") || !Core.isSeasonalMapActive("darkbirthday"))
            return;

        Core.AddDrop("Soul Essence");

        Legion.ObsidianRock(300);
        Legion.FarmLegionToken(5000);

        Adv.BuyItem("darkbirthday", 1697, "Legion SwordMaster Assassin");

        if (rankUpClass)
            Adv.RankUpClass("Legion SwordMaster Assassin");
    }
}
