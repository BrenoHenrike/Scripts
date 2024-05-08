/*
name: Warfury Emblem
description: Farms "Warfury Emblem" from quest: "Warfury Training".
tags: war, fury, training, emblem
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;

public class WarfuryEmblem
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();
    private CoreSoW SoW = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        WarfuryEmblemFarm();

        Core.SetOptions(false);
    }

    public void WarfuryEmblemFarm(int quant = 60)
    {
        if (Core.CheckInventory("Warfury Emblem", quant))
            return;

        SoW.Tyndarius();

        Core.AddDrop("Warfury Emblem");
        Core.FarmingLogger("Warfury Emblems", quant);
        Core.EquipClass(ClassType.Farm);
        //Adv.BestGear(RacialGearBoost.Human);

        Core.RegisterQuests(8204);
        Core.HuntMonster("wartraining", "Warfury Soldier", "Warfury Emblem", quant, false);
        Core.CancelRegisteredQuests();
    }
}
