/*
name: 1SanctifiedLightofDestiny
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
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class SanctifiedLightofDestiny
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreDailies Daily = new();
    private CoreBLOD BLOD = new();
    private CoreStory Story = new();
    private CoreAdvanced Adv = new();
    private Core13LoC LOC => new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetSanctifiedLightofDestiny();

        Core.SetOptions(false);
    }

    public void GetSanctifiedLightofDestiny()
    {
        if (Core.CheckInventory("Sanctified Light of Destiny"))
            return;

        BLOD.BlindingLightOfDestiny();

        Core.AddDrop("Sanctified Light of Destiny", "Pious Platinum");
        Core.EnsureAccept(8112);

        BLOD.UpgradeMetal(MineCraftingMetalsEnum.Gold);
        BLOD.UpgradeMetal(MineCraftingMetalsEnum.Platinum);
        Farm.BattleUnderB("Bone Dust", 300);
        BLOD.LoyalSpiritOrb(25);
        Core.HuntMonster("Extinction", "Cyworg", "Refined Metal", 5);

        Core.EnsureComplete(8112);
    }
}
