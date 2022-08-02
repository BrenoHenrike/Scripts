//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
using RBot;

public class PolishedBLOD
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreBLOD BLOD = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetPBLOD();

        Core.SetOptions(false);
    }

    public void GetPBLOD()
    {
        if (Core.CheckInventory("Polished Blinding Light of Destiny"))
            return;

        BLOD.DoAll();

        Core.AddDrop("Polished Blinding Light of Destiny");
        Core.EnsureAccept(7063);

        Farm.BattleUnderB("Undead Energy", 3000);
        Adv.BuyItem("alchemyacademy", 2114, "Bright Tonic", 10, 10);
        Core.HuntMonster("doomforest", "Undead Paladin", "Purification Orb", 10, false);
        Core.KillMonster("doomforest", "r7", "Up", "*", "Shoelace of a Fallen Paladin", 3, false);
        Core.HuntMonster("therift", "Plague Spreader", "Slimed Sigil", 75, false);
        Core.HuntMonster("lightguardwar", "Sigrid Sunshield", "Medal of Justice", 150, false);

        Core.EnsureComplete(7063);
        Bot.Wait.ForPickup("Polished Blinding Light of Destiny");
    }
}
