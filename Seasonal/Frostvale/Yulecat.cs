/*
name: Yulecat Drops
description: This will repeatedly kill Yulecat until you got all of the drops.
tags: yulecat-drops, seasonal, frostvale
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class Yulecat
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
        if (!Core.isSeasonalMapActive("yulecat"))
        {
            Core.Logger("This seasonal map is not available right now.");
            return;
        }

        string[] DropsM = {
            "Festive Flying Piggy Guard",
            "Northlands Hunter Blades",
            "Northlands Hunter Winged Helm",
            "Ziri's Holiday Horror Kitty"
        };

        string[] Drops = {
            "Festive Wand",
            "Furred Hunter's Cape",
            "Holiday Party Piggy Guard",
            "Kitty Yu-Yule Guard",
            "Kitty's Last Lunch",
            "Northlands BeastHunter",
            "Northlands Hunter Blade",
            "Northlands Hunter Helmet"
        };

        string[] MemDrops = DropsM.Concat(Drops).ToArray();

        if ((Core.CheckInventory(MemDrops) && Core.IsMember) || (Core.CheckInventory(Drops) && !Core.IsMember))
        {
            Core.Logger("You already have all of the items.");
            return;
        }
        Core.EquipClass(ClassType.Solo);

        if (Core.IsMember)
            Core.AddDrop(MemDrops);
        else
            Core.AddDrop(Drops);

        if (Core.IsMember)
            while (!Bot.ShouldExit && !Core.CheckInventory(MemDrops))
                Core.HuntMonster("yulecat", "Kitty Yu Yule", "*", isTemp: false);
        else
            while (!Bot.ShouldExit && !Core.CheckInventory(Drops))
                Core.HuntMonster("yulecat", "Kitty Yu Yule", "*", isTemp: false);
    }
}
