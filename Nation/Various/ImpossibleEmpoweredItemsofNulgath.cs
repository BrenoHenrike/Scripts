/*
name: Impossible Empowered Items of Nulgath
description: does the 'Impossible, Empowered Items of Nulgath' quest for "soulreaper of nulgath"
tags: soulreaper of nulgath, member
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/TheLeeryContract[Member].cs
using Skua.Core.Interfaces;

public class ImpossibleEmpoweredItemsofNulgath
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private TheLeeryContract TLC = new();
    private CoreNation Nation = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetWep();

        Core.SetOptions(false);
    }

    public void GetWep()
    {
        if (Core.CheckInventory("SoulReaper of Nulgath") || !Core.IsMember)
            return;

        //Quest Requirments:
        Nation.DragonSlayerReward();

        Core.EnsureAccept(571);

        //Material Requirements:
        Nation.FarmDiamondofNulgath(10);
        Nation.FarmDarkCrystalShard(5);
        if (Core.CheckInventory("Gemstone of Nulgath") && Core.IsMember)
            Nation.ForgeTaintedGems(5);
        else
            Nation.SwindleBulk(5);
        Nation.FarmUni13(1);
        Core.HuntMonster("twilight", "Abaddon", "Abaddon's Terror", isTemp: false);
        TLC.QuestItems(TheLeeryContract.RewardsSelection.Godly_Golden_Dragon_Axe);

        Core.EnsureComplete(571);
        Bot.Wait.ForDrop("SoulReaper of Nulgath", 20);
        Bot.Wait.ForPickup("SoulReaper of Nulgath", 20);
    }
}
