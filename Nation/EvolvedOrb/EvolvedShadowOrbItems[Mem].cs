/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Other/Classes/REP-based/Bard.cs
//cs_include Scripts/Nation/EvolvedOrb/EvolvedShadowOrb[Mem].cs
//cs_include Scripts/Other/MergeShops/BattleConGearMerge.cs
//cs_include Scripts/Other/Various/Potions.cs
using Skua.Core.Interfaces;

public class EvolvedShadowOrbItems
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new CoreAdvanced();
    private CoreFarms Farm = new CoreFarms();
    private CoreNation Nation = new();
    private EvolvedShadowOrb ESO = new();
    private Bard Bard = new();
    private BattleConGearMerge BCon = new();
    private PotionBuyer Potion = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetItems();

        Core.SetOptions(false);
    }

    public void GetItems()
    {
        if (!Core.IsMember || Core.CheckInventory(Rewards, toInv: false))
            return;

        ESO.GetEvolvedShadowOrb();
        if (Core.CheckInventory("Evolved Shadow Orb")) //recheck
            return;

        RebornDarkSide();
        VoidEmotions();
        ShapeNothingness();
        //Not yet added
        // Reborn in the Dark Side (Rare) 4772
        // Reborn in the Dark Side (Shadow) 4773
    }

    public void RebornDarkSide()
    {
        // Reborn in the Dark Side 4771
        Core.AddDrop("Evolved Shadow of Nulgath");
        if (!Core.CheckInventory("Platinum Coin of Nulgath: 2500") || !Core.CheckInventory(33360))
            return;
        Bard.GetBard(false);
        Nation.FarmUni13(3);
        Adv.BuyItem("tercessuinotlim", 1951, "Unidentified 25");
        Nation.FarmVoucher(true);
        if (!Core.CheckInventory("Bahemoth Blade of shadow"))
        {
            Core.EquipClass(ClassType.Solo);
            if (!Core.CheckInventory("Basic War Sword"))
            {
                Farm.BludrutBrawlBoss(quant: 100);
                Core.BuyItem("battleon", 222, "Basic War Sword");
            }
            if (!Core.CheckInventory("Steel Afterlife"))
            {
                Farm.BludrutBrawlBoss(quant: 100);
                Core.BuyItem("battleon", 222, "Steel Afterlife");
            }
            Farm.BludrutBrawlBoss(quant: 500);
            Core.BuyItem("battleon", 222, "Bahemoth Blade of shadow");
        }
        Core.EquipClass(ClassType.Farm);
        Nation.ApprovalAndFavor(1, 0);
        Nation.FarmFiendToken(30);
        BCon.BuyAllMerge("Azure Starblade");
        Bot.Wait.ForPickup("Evolved Shadow of Nulgath");
    }

    public void VoidEmotions()
    {
        //Void Emotion 4774
        if (!Core.CheckInventory("Platinum Coin of Nulgath: 300") || !Core.CheckInventory(33361))
            return;
        Core.EnsureAccept(4774);
        Core.EquipClass(ClassType.Farm);
        Nation.FarmDarkCrystalShard(10);
        Nation.FarmDiamondofNulgath(50);
        Nation.FarmVoucher(false);
        Nation.FarmBloodGem(5);
        Core.EquipClass(ClassType.Solo);
        Core.KillMonster("chaoslord", "r2", "Left", "*", "There is no Myself", isTemp: false);
        Core.EnsureComplete(4774);
        Bot.Wait.ForPickup("Evolved Shadow Helm");
    }

    public void ShapeNothingness()
    {
        // Shape your Nothingness 4775
        Core.AddDrop("Unidentified 29", "Random Weapon of Nulgath", "Evolved Shadow Spear of Nulgath");
        Core.EnsureAccept(4775);
        Core.EquipClass(ClassType.Farm);
        Nation.FarmUni10(30);
        Nation.SwindleBulk(30);
        Nation.FarmDarkCrystalShard(30);
        Nation.Supplies("Unidentified 29");
        Nation.Supplies("Random Weapon of Nulgath");
        Nation.FarmVoucher(false);
        Nation.FarmTotemofNulgath(10);
        Potion.INeedYourStrongestPotions(new[] { "Potent Destruction Elixir" }, potionQuant: 15);
        Core.EnsureComplete(4775);
        Bot.Wait.ForPickup("Evolved Shadow Spear of Nulgath");
    }

    private string[] Rewards =
    {
        "Evolved Shadow of Nulgath",
        "Evolved Shadow Helm",
        "Evolved Shadow Spear of Nulgath",
    };
}
