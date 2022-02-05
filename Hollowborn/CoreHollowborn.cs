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
        Farm.IcestormArena(65);

        Core.AddDrop("Soul Potion", "Dried Slime", "Arashtite Ore", "Human Soul", "Fallen Soul", "Lae\'s Hardcore Contract");
        Core.Logger("Getting Lae's Hardcore Contract");
        if (!Core.CheckInventory("Soul Potion"))
        {
            Core.Logger("Getting Soul Potion");
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("orecavern", "Crashroom", "Dried Slime", 1, false);
            Core.HuntMonster("orecavern", "Deathmole", "Arashtite Ore", 1, false);
            Farm.AlchemyPacket("Dried Slime", "Arashtite Ore", AlchemyRunes.Gebo, rank: 8, loop: false, modifier: "Man");
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

}