/*
name: Cryomancer Daily
description: Cryomancer
tags: daily, cryomancer, class, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class Cryomancer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();
    public GlaceraStory Glac = new();
    public CoreStory Story = new();
    private CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoCryomancer();

        Core.SetOptions(false);
    }

    public void DoCryomancer(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Cryomancer"))
        {
            if (rankUpClass)
                Adv.RankUpClass("Cryomancer");
            return;
        }

        Glac.FrozenTower();
        Daily.Cryomancer();

        if (rankUpClass)
            Adv.RankUpClass("Cryomancer");
    }

}
