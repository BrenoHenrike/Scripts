//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
using RBot;

public class LetsGetYouASuit
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreHollowborn HB = new CoreHollowborn();
    public CoreBLOD BLOD = new CoreBLOD();
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Bot.Player.LoadBank();
        Core.SetOptions();

        HBPaladin();

        Core.SetOptions(false);
    }

    public void HBPaladin()
    {
        if (Core.CheckInventory("Hollowborn Paladin"))
            return;
        HB.HardcoreContract();
        Farm.IcestormArena(75);

        Core.AddDrop("Sparrow's Blood", "Brilliant Aura", "Gem of Superiority", "Condensed Mana", "Hollowborn Paladin");
        Core.EnsureAccept(7557);
        if (!Core.CheckInventory("Sparrow's Blood")) 
        {
            Core.EquipClass(ClassType.Farm);
            Core.EnsureAccept(803);
            Core.HuntMonster("Arcangrove", "Seed Spitter", "Snapdrake", 17);
            Core.HuntMonster("Arcangrove", "Gorillaphant", "Blood Lily", 30);
            Core.HuntMonster("Arcangrove", "Seed Spitter", "Doom Dirt", 12);
            Core.EnsureComplete(803);
        }
        BLOD.FindingFragmentsMace();
        Farm.DoomwoodREP(3);
        Core.BuyItem("lightguard", 277, "Dark Arts Scholar");
        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("shadowblast", "Legion Fenrir", "Gem of Superiority", 1, false);
        if (!Core.CheckInventory("Exalted Paladin Seal"))
        {
            Farm.GoodREP();
            Farm.Gold(500000);
            Core.BuyItem("darkthronehub", 1308, "Exalted Paladin Seal");
        }
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("timevoid", "Unending Avatar", "Condensed Mana", 1, false);
        HB.HumanSoul(200);
        Core.EnsureComplete(7557);
        return;
    }
}
