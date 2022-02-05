//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Chaos/DrakathArmorBot.cs
//cs_include Scripts/Story/TowerOfDoom.cs
using RBot;

public class AscendedDrakathGear
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public DrakathArmorBot DA = new DrakathArmorBot();
    public TowerOfDoom TOD = new TowerOfDoom();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        if (Core.CheckInventory(new[] {"Ascended Blade of Awe", "Ascended Light of Destiny", "Ascended Face of Chaos"}))
            return;

        Core.AddDrop("Ascended Blade of Awe", "Ascended Light of Destiny", "Ascended Face of Chaos");

        AscendedGear("Ascended Blade of Awe");
        AscendedGear("Ascended Light of Destiny");
        AscendedGear("Ascended Face of Chaos");
    }

    public void AscendedGear(string Target)
    {
        if (Core.CheckInventory(Target))
            return;

        DA.DrakathOriginalArmor();
        Core.AddDrop(Target);
        TOD.TowerProgress(4);
        while (Core.CheckInventory(Target))
        {
            Core.EnsureAccept(3767);
            Core.HuntMonster("towerofdoom4", "Dread Stranglerfish", "Holy Wasabi");
            Core.EnsureComplete(3767);
        }
    }
}
