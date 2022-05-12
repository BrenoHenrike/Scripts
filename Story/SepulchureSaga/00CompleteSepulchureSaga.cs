//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/SepulchureSaga/01SepulchurePrequelAlden.cs
//cs_include Scripts/Story/SepulchureSaga/02SepulchurePrequelLynaria.cs
//cs_include Scripts/Story/SepulchureSaga/03SepulchuresRise.cs
//cs_include Scripts/Story/SepulchureSaga/04ShadowfallRise.cs
using RBot;

public class CompleteSepulchureSaga
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public SepulchurePrequelAlden s01 = new SepulchurePrequelAlden();
    public SepulchurePrequelLynaria s02 = new SepulchurePrequelLynaria();
    public SepulchuresRise s03 = new SepulchuresRise();
    public ShadowfallRise s04 = new ShadowfallRise();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CompleteALL();

        Core.SetOptions(false);
    }

    public void CompleteALL()
    {
        Core.Logger("SepulchurePrequelAlden");
        s01.StoryLine();
        Core.Logger("SepulchurePrequelLynaria");
        s02.StoryLine();
        Core.Logger("SepulchuresRise");
        s03.StoryLine();
        Core.Logger("ShadowfallRise");
        s04.StoryLine();
    }
}
