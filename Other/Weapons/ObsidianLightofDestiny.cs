/*
name: ObsidianLightofDestiny
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
using Skua.Core.Interfaces;

public class ObsidianLightofDestiny
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreBLOD BLOD = new();
    public CoreDoomwood DW = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Axe();

        Core.SetOptions(false);
    }

    public void Axe()
    {
        if (Core.CheckInventory("Obsidian Light of Destiny"))
            return;

        DW.DoomwoodPart3();
        Core.AddDrop("Obsidian Light of Destiny");

        //The Edge of Destiny
        if (!Core.CheckInventory("Obsidian Light of Destiny"))
        {
            Core.EnsureAccept(7648);

            if (!Core.CheckInventory("Blinding Edge of Obsidian"))
            {
                Farm.MysteriousDungeonREP();
                Core.BuyItem("darkthronehub", 1308, "Blinding Edge of Obsidian");
                Bot.Wait.ForPickup("Blinding Edge of Obsidian");
            }

            BLOD.GetBlindingWeapon(WeaponOfDestiny.Mace);
            BLOD.GetBlindingWeapon(WeaponOfDestiny.Bow);
            BLOD.GetBlindingWeapon(WeaponOfDestiny.Blade);

            BLOD.BrilliantAura(40); //Brilliant Aura x40
            BLOD.BrightAura(120); //Bright Aura x120
            BLOD.BlindingAura(1); //Blinding Aura 
            BLOD.LoyalSpiritOrb(750); //Spirit Orb (Misc) x5,000 
            BLOD.SpiritOrb(5000); //Loyal Spirit Orb x750 

            Core.EnsureComplete(7648);
            Bot.Wait.ForPickup("Obsidian Light of Destiny");
        }
    }
}
