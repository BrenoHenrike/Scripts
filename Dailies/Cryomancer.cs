/*
name:  Cryomancer Daily
description:  Cryomancer
tags: daily, cryomancer, class, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Glacera.cs
using Skua.Core.Interfaces;

public class Cryomancer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();
    public GlaceraStory Glac = new();
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoCryomancer();

        Core.SetOptions(false);
    }

    public void DoCryomancer()
    {
        Glac.DoAll();
        Daily.Cryomancer();
    }

}
