/*
name: Hollow Soul
description: Farms "Hollow Soul"
tags: hollow soul, shadowrealm, hollowborn
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nation/CoreNation.cs

using Skua.Core.Interfaces;

public class HollowSoul
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreHollowborn HB = new CoreHollowborn();
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetYaSoulsHeeeere();

        Core.SetOptions(false);
    }

    public void GetYaSoulsHeeeere(int HSQuant = 2500)
    {
        if (Core.CheckInventory("Hollow Soul", HSQuant))
            return;

        Core.FarmingLogger("Hollow Soul", HSQuant);
        Core.EquipClass(ClassType.Farm);
        Core.AddDrop("Hollow Soul");
        Core.RegisterQuests(7553, 7555);
        Core.KillMonster("shadowrealm", "r2", "Left", "*", "Hollow Soul", HSQuant, isTemp: false, log: false);

        // while (!Bot.ShouldExit && !Core.CheckInventory("Hollow Soul", HSQuant))
        // {

        // Core.KillMonster("shadowrealm", "r2", "Left", "*", "Hollow Soul", HSQuant, isTemp: false, log: false);
        //     if (Core.CheckInventory("Hollow Soul", HSQuant))
        //     {
        //         Core.JumpWait();
        //         break;
        //     }
        // }
    }
}
