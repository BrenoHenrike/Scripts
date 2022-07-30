//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Evil/SepulchuresOriginalHelm.cs
//cs_include Scripts/Other/Materials/DarknessShard.cs
//cs_include Scripts/Story/Doomwood/AQWZombies.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using Skua.Core.Interfaces;

public class UpgradeSepulchuresOriginalHelm
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public DarknessShard DS = new();
    public SepulchuresOriginalHelm Seppy = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Upgrade();

        Core.SetOptions(false);
    }

    public void Upgrade()
    {
        if (Core.CheckInventory("Reborn Sepulchure's Helm"))
            return;

        Seppy.DoAll();

        Core.AddDrop("Reborn Sepulchure's Helm", "Ultimate Darkness Gem");

        Core.EnsureAccept(7069);
        DS.GetShard(1);
        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("shadowfallwar", "Garden1", "Bottom", "Bonemuncher", "Ultimate Darkness Gem", 75, isTemp: false);
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("frozenlair", "Lich Lord", "Necrotic Orb", 150, isTemp: false);
        Core.HuntMonster("underworld", "Frozen Pyromancer", "Flaming Skull", 100, isTemp: false);
        Core.EnsureComplete(7069);
        Bot.Wait.ForPickup("Reborn Sepulchure's Helm");
    }
}
