//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using RBot;

public class BanditsCorrespondence
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDarkon Darkon = new CoreDarkon();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.Add("Bandit's Correspondence");

        Core.SetOptions();

        Darkon.BanditsCorrespondence();

        Core.SetOptions(false);
    }
}