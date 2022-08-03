//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/SkyPirate.cs
//cs_include Scripts/Other/Badges/CornelisReboron.cs
//cs_include Scripts/Other/Badges/DerpMoosefishBadge.cs
//cs_include Scripts/Other/Badges/DesolothFreed.cs
//cs_include Scripts/Story/EtherstormWastes.cs
//cs_include Scripts/Other/Badges/SkyPirateSlayerBadge.cs
//cs_include Scripts/Other/Badges/YouMadBroBadge.cs
using RBot;

public class AllBadges
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public CornelisRebornbadge Cornelis = new();
    public DerpMoosefishBadge DMF = new();
    public DesolothFreedBadge Deso = new();
    public SkyPirateBadge SPB = new();
    public YouMadBroBadge YMBB = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Cornelis.Badge();
        SPB.Badge();
        Deso.Badge();
        DMF.Badge();
        YMBB.Badge();
        //add more as they are made.

        Core.SetOptions(false);
    }
}
