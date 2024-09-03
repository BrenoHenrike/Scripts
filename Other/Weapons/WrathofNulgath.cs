/*
name: Wrath of Nulgath
description: This script will get Wrath of Nulgath.
tags: wrathofnulgath, wrath, ravenous, weapon
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/JuggernautItems.cs
//cs_include Scripts/Story/Legion/DarkWarLegionandNation.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class WrathofNulgath
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreStory Story = new();
    public CoreNation Nation = new();
    public JuggernautItemsofNulgath juggernaut = new();
    private DarkWarLegionandNation DWLN = new();

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

        DWLN.DoBoth();

        Core.Logger("Farming Wrath of Nulgath.");

        juggernaut.JuggItems(reward: JuggernautItemsofNulgath.RewardsSelection.Overfiend_Blade_of_Nulgath);
        Nation.FarmVoucher(false, true);
        Nation.FarmVoucher(true, true);
        Nation.FarmUni13(1);
        Nation.FarmTaintedGem(80);
        Nation.FarmDarkCrystalShard(60);
        Nation.FarmDiamondofNulgath(100);
        Adv.BuyItem("darkwarnation", 2123, "Wrath of Nulgath");
        Bot.Wait.ForPickup("Wrath of Nulgath");
    }
}

