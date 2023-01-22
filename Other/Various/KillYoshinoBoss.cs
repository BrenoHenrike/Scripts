/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class KillYoshinoBoss
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Yoshino();
        Core.SetOptions(false);
    }

    public void Yoshino()
    {
        if (!Bot.Quests.IsAvailable(5720))
            return;

        Core.EquipClass(ClassType.Solo);
        Adv.BestGear(GearBoost.dmgAll);

        Core.AddDrop("Limited Event Coin");

        Core.EnsureAccept(5720);
        Core.KillMonster("yoshino", "r1", "Right", "Xyfrag", "Limited Event Monster Proof");
        Core.JumpWait();
        Adv.BestGear(GearBoost.gold);
        Farm.ToggleBoost(BoostType.Gold);
        Bot.Sleep(Core.ActionDelay);
        Core.EnsureComplete(5720);
        Bot.Wait.ForPickup("Limited Event Coin");
        Farm.ToggleBoost(BoostType.Gold, false);

    }
}
