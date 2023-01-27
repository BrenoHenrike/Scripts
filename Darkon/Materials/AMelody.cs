/*
name:  Farms the A Melody for the Darkon Debris 2 [reconstructed]
description:  Farms the A Melody for the Darkon Debris 2 [reconstructed]
tags: a melody, darkon, debris
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using Skua.Core.Interfaces;

public class AMelody
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDarkon Darkon = new CoreDarkon();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.Add("A Melody");

        Core.SetOptions();

        Darkon.AMelody();

        Core.SetOptions(false);
    }
}
