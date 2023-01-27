/*
name:  Bandits Correspondence
description:  Bandits Correspondence
tags: darkon, bandits correspondence
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using Skua.Core.Interfaces;

public class BanditsCorrespondence
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDarkon Darkon = new CoreDarkon();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.Add("Bandit's Correspondence");

        Core.SetOptions();

        Darkon.BanditsCorrespondence();

        Core.SetOptions(false);
    }
}
