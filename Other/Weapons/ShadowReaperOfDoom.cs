//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class SRoD
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public Core13LoC LoC = new();

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

        Core.Logger("Farming for ShadowReaper Of Doom");

        LoC.Xiang();
        Core.EquipClass(ClassType.Solo);
        Core.RegisterQuests(3188);
        while (!Bot.ShouldExit && !Core.CheckInventory("Mirror Realm Token", 300))
            Core.HuntMonsterMapID("mirrorportal", 1);
        Core.CancelRegisteredQuests();
        Core.EquipClass(ClassType.Solo);
        Core.KillMonster("overworld", "boss1", "Left", "Undead Artix", "Undead Paladin Token", isTemp: false);

        Core.BuyItem("overworld", 618, "ShadowReaper Of Doom", shopItemID: 1806);
    }
}
