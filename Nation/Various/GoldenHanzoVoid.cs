/*
name: GoldenHanzoVoid
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Shops;

public class GoldenHanzoVoid
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetGHV();

        Core.SetOptions(false);
    }

    public void GetGHV()
    {
        if (Core.CheckInventory("Golden Hanzo Void"))
            return;


        Nation.ApprovalAndFavor(50, 200);
        Farm.BattleGroundE(100000);
        Nation.SwindleBulk(30);
        Nation.TheAssistant("Dark Crystal Shard", 15);
        Nation.FarmDiamondofNulgath(50);
        Nation.FarmVoucher(false);

        Adv.BuyItem("evilwarnul", 456, 27843, shopItemID: 3142);
        Bot.Wait.ForItemBuy();
    }
}

