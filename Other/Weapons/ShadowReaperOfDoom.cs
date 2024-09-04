/*
name: ShadowReaper of Doom
description: This bot farms the requiered materials to farm the ShadowReaper of Doom
tags: blod, mirror, realm, token, undead, paladin
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class SRoD
{
    private static IScriptInterface Bot => IScriptInterface.Instance;
    private static CoreBots Core => CoreBots.Instance;
    private readonly Core13LoC LoC = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        ShadowReaperOfDoom();

        Core.SetOptions(false);
    }

    public void ShadowReaperOfDoom()
    {
        if (Core.CheckInventory("ShadowReaper Of Doom"))
            return;

        Core.FarmingLogger("ShadowReaper Of Doom", 1);
        LoC.Xiang();

        Core.EquipClass(ClassType.Solo);
        Core.RegisterQuests(3188);
        Core.HuntMonsterMapID("mirrorportal", 1, "Mirror Realm Token", 300, false);
        Core.CancelRegisteredQuests();
        Core.KillMonster("overworld", "boss1", "Left", "Undead Artix", "Undead Paladin Token", isTemp: false);

        Core.BuyItem("overworld", 618, "ShadowReaper Of Doom", shopItemID: 1806);
    }
}
