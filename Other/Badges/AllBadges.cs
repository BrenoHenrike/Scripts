//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/SkyPirate.cs
//cs_include Scripts/Other/Badges/CtrlAltDelMemberBadge.cs
//cs_include Scripts/Other/Badges/SkyPirateSlayerBadge.cs
//cs_include Scripts/Other/Badges/YouMadBroBadge.cs
//cs_include Scripts/Other\Badges\DerpMoosefishBadge.cs
using RBot;

public class AllBadges
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public CtrlAltDelMemberBadge CADB = new();
    public SkyPirateBadge SPB = new();
    public YouMadBroBadge YMBB = new();
    public DerpMoosefishBadge DMF = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CADB.Badge();
        SPB.Badge();
        YMBB.Badge();
        DMF.Badge();
        //add more as they are made.

        Core.SetOptions(false);
    }
}
