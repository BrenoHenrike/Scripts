/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class CoreHollowborn
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void HardcoreContract()
    {
        if (Core.CheckInventory(55157))
            return;

        Core.AddDrop("Human Soul", "Fallen Soul", "Lae\'s Hardcore Contract");
        Farm.Experience(65);

        Core.Logger("Getting Lae's Hardcore Contract");
        Core.EnsureAccept(7556);

        if (!Core.CheckInventory("Soul Potion"))
        {
            Farm.Gold(2500000);
            Core.BuyItem("alchemyacademy", 2036, "Gold Voucher 500k", 5);
            Core.BuyItem("alchemyacademy", 2036, "Soul Potion");
            Bot.Wait.ForItemBuy();
        }

        HumanSoul(50);

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("doomwood", "Undead Paladin", "Fallen Soul", 13, false);

        Core.EnsureComplete(7556);
    }

    public void HumanSoul(int quant)
    {
        if (Core.CheckInventory("Human Soul", quant))
            return;

        Core.AddDrop("Human Soul");

        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("noxustower", "r14", "Left", "*", "Human Soul", quant, false);
    }

    public void FreshSouls(int Uni36Quant = 3, int FSQuant = 1000)
    {
        if (Core.CheckInventory("Unidentified 36", Uni36Quant) && Core.CheckInventory("Fresh Soul", FSQuant))
            return;

        Farm.Experience(50);

        Core.AddDrop("Unidentified 36", "Fresh Soul");
        Core.RegisterQuests(7293);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Unidentified 36", Uni36Quant) || !Core.CheckInventory("Fresh Soul", FSQuant)))
        {
            Core.HuntMonster("citadel", "Inquisitor Guard", "Fresh Soul?", 10, log: false);
            Bot.Wait.ForPickup("Fresh Soul");
        }
        Core.CancelRegisteredQuests();
    }
}
