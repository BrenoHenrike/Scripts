/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/JuggernautItems.cs
using Skua.Core.Interfaces;

public class ScriptTemplate
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new CoreDailies();
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetSword();

        Core.SetOptions(false);
    }

    public void GetSword()
    {
        if (Core.CheckInventory("Wrath of Nulgath"))
            return;

        Nation.FarmUni13();
        Nation.TheAssistant("Tainted Gem", 80);
        Nation.FarmDarkCrystalShard(60);
        Nation.FarmDiamondofNulgath(100);
        Nation.FarmVoucher(true);
        Nation.FarmVoucher(false);
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
        Adv.EnhanceItem("Wrath of Nulgath", EnhancementType.Lucky, CapeSpecial.None, HelmSpecial.None, WeaponSpecial.Spiral_Carve);
    }

    public void OverfiendBlade()
    {
        if (Core.CheckInventory("Overfiend Blade of Nulgath"))
            return;

        Core.AddDrop("Overfiend Blade of Nulgath");

        Farm.Experience(30);
        Nation.FarmUni13();
        Nation.FarmDiamondofNulgath(13);
        Nation.FarmDarkCrystalShard(50);
        Nation.FarmTotemofNulgath(3);
        Nation.FarmGemofNulgath(20);
        Nation.FarmVoucher(false);
        Nation.SwindleBulk(50);

        Core.EnsureAccept(837);
        Core.HuntMonster("underworld", "Undead Bruiser", "Undead Bruiser Rune");
        Core.EnsureComplete(837, 6138);
        Bot.Wait.ForPickup("Overfiend Blade of Nulgath");
    }
}
