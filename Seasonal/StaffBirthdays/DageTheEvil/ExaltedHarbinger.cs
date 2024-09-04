/*
name: Exalted Harbinger (Class)
description: This script will get Exalted Harbinger class.
tags: exalted, harbinger, exaltedharbinger, seasonal, dage, class, dark birthday token, darkbirthday
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/CoreDageBirthday.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/DarkBirthdayTokenMerge.cs

using Skua.Core.Interfaces;

public class ExaltedHarbinger
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    private DarkBirthdayTokenMerge DBTM = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetEH();

        Core.SetOptions(false);
    }

    public void GetEH(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Exalted Harbinger") || !Core.isSeasonalMapActive("darkbirthday"))
        {
            Core.Logger("You already own Exalted Harbinger or the map is unavailable.");

            if (rankUpClass)
                Adv.RankUpClass("Exalted Harbinger");
            return;
        }

        DBTM.BuyAllMerge("Exalted Harbinger");

        if (rankUpClass)
            Adv.RankUpClass("Exalted Harbinger");
    }
}
