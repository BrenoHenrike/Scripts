/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/ShadowsOfChaos/CoreSoC.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class ShadowflameWarMedal
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreSoW SoW = new();
    public CoreSoC SoC = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Medals();

        Core.SetOptions(false);
    }

    public void Medals(int quant = 300)
    {
        if (Core.CheckInventory("ShadowFlame War Medal", quant))
            return;

        SoW.ShadowWar();
        SoC.DualPlane();

        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(7685, 7686);
        while (!Bot.ShouldExit && !Core.CheckInventory("ShadowFlame War Medal", quant))
        {
            Core.HuntMonster("chaosamulet", "Shadowflame Warrior|Shadowflame Scout", "Shadow Medal", 5);
            Core.HuntMonster("chaosamulet", "Shadowflame Warrior|Shadowflame Scout", "Mega Shadow Medal", 3);
            Bot.Wait.ForPickup("ShadowFlame War Medal");
        }
        Core.CancelRegisteredQuests();
    }
}
