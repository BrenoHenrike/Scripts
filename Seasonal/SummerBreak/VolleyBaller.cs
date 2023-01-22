/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class VolleyBaller
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Rewards);
        Core.SetOptions();

        VolleyBallerQuest();

        Core.SetOptions(false);
    }

    public readonly string[] Rewards =
 {
        "Volleyball Captain",
        "Volleyball Hero",
        "Volleyball Hero's Hat",
        "Volleyball Heroine's Hat",
        "Volleyball Hero's Hat + Glasses",
        "Volleyball Heroine's Hat + Glasses",
        "Volleyball Hero's Glasses",
        "Volleyball Team A Mascot",
        "Volleyball Hero's Board Cape",
        "Volleyball Team A Mascot Pet",
        "Volleyball Hero's Rod",
        "Volleyball Hero's Surfboard",
        "Volleyball Hero's Foam Spear",
        "Volleyball Hero's Foam Gauntlets",
        "Volleyball Hero's WaterGun",
        "Volleyball Hero's WaterGuns",
    };
    public void VolleyBallerQuest()
    {
        if (!Core.isSeasonalMapActive("summerbreak"))
            return;

        Core.AddDrop(Rewards);

        int i = 1;
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards, toInv: false))
        {
            Core.EnsureAccept(8794);
            Core.HuntMonster("summerbreak", "MMMirage", "Gum Ball", 6);
            Core.EnsureCompleteChoose(8794);
            Core.ToBank(Rewards);
            Core.Logger($"Completed x{i++}");
        }
        Core.Logger("All drops Already Acquired");
    }

}
