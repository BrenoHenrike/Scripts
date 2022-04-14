//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class CoreHollowborn
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void HardcoreContract()
    {
        if (Core.CheckInventory("Lae\'s Hardcore Contract"))
            return;

        Core.AddDrop("Human Soul", "Fallen Soul", "Lae\'s Hardcore Contract");
        Farm.IcestormArena(65);

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

    public void FreshSouls(int quant = 350)
    {
        if (Core.CheckInventory("Fresh Soul", quant))
            return;

        Core.AddDrop("Fresh Soul", "Unidentified 36");
        Farm.Experience(50);

        Core.Logger($"Farming x{quant} Fresh Soul");

        while (!Core.CheckInventory("Fresh Soul", quant))
        {
            Core.EnsureAccept(7293);
            Core.HuntMonster("citadel", "Inquisitor Guard", "Fresh Soul?", 10, log: false);
            Core.EnsureComplete(7293);
            Bot.Wait.ForPickup("Fresh Soul");
        }
    }
}