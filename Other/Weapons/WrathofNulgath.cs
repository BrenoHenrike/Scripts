//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Nulgath/Various/JuggernautItems.cs
using RBot;

public class ScriptTemplate
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new CoreDailies();
    public CoreNulgath Nulgath = new CoreNulgath();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetSword();

        Core.SetOptions(false);
    }

    public void GetSword()
    {
        if (Core.CheckInventory("Wrath of Nulgath"))
            return;

        Nulgath.FarmUni13();
        Nulgath.TheAssistant("Tainted Gem", 80);
        Nulgath.FarmDarkCrystalShard(60);
        Nulgath.FarmDiamondofNulgath(100);
        Nulgath.FarmVoucher(true);
        Nulgath.FarmVoucher(false);
        OverfiendBlade();
        if (!Core.isCompletedBefore(8580))
        {
            Core.EnsureAccept(new[] { 8578, 8579 });
            Core.KillMonster("darkwarnation", "Enter", "Spawn", "*", "Legion Badge", 5);
            Core.KillMonster("darkwarnation", "Enter", "Spawn", "*", "Mega Legion Badge", 3);
            Core.EnsureComplete(new[] { 8578, 8579 });
            Story.KillQuest(8580, "darkwarnation", "Legion Doomknight");
        }
        Core.BuyItem("darkwarnation", 2123, "Wrath of Nulgath");
        Bot.Wait.ForPickup("Wrath of Nulgath");
        Adv.EnhanceItem("Wrath of Nulgath", EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
    }

    public void OverfiendBlade()
    {
        if (Core.CheckInventory("Overfiend Blade of Nulgath"))
            return;

        Core.AddDrop("Overfiend Blade of Nulgath");

        Farm.Experience(30);
        Nulgath.FarmUni13();
        Nulgath.FarmDiamondofNulgath(13);
        Nulgath.FarmDarkCrystalShard(50);
        Nulgath.FarmTotemofNulgath(3);
        Nulgath.FarmGemofNulgath(20);
        Nulgath.FarmVoucher(false);
        Nulgath.SwindleBulk(50);

        Core.EnsureAccept(837);
        Core.HuntMonster("underworld", "Undead Bruiser", "Undead Bruiser Rune");
        Core.EnsureComplete(837, 6138);
        Bot.Wait.ForPickup("Overfiend Blade of Nulgath");
    }
}
