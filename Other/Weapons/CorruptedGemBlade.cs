/*
name: Corrupted Gem Blade
description: does 'Drudgen the Salesman' for "Corrupted Gem Blade"
tags: corrupted gem blade, nation, drudgen the assistant, drudgen the salesman
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class CorruptedGemBlade
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DrudgentheSalesman();

        Core.SetOptions(false);
    }

    public void DrudgentheSalesman()
    {
        if (Core.CheckInventory("Corrupted Gem Blade") || !Core.CheckInventory("Drudgen the Assistant"))
            return;

        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop("Corrupted Gem Blade");

        Core.RegisterQuests(605, 2857);
        while (!Bot.ShouldExit && !Core.CheckInventory("Corrupted Gem Blade"))
        {
            Nation.Supplies("Voucher of Nulgath (non-mem)");
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("cloister", "Acornent", "Diamonds of Time", isTemp: false, log: false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("evilmarsh", "Tainted Elemental", "Tainted Rune of Evil", log: false);
        }
        Core.CancelRegisteredQuests();
    }
}
