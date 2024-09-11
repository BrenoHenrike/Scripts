/*
name: Super-Fan Swag Token A (Army)
description: Farms Super-Fan Wwag Token A with f2p method
tags: super, fan, swag, toke, a, army, f2p, rep
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models;
using Skua.Core.Options;

public class ArmySwagTokens
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyTokens";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option <bool> ("MemOrNonMem", "use Member Method?", "True if member, false if not", false),
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.player7,
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();
        Core.BankingBlackList.AddRange(new[] { "Super-Fan Swag Token D", "Super-Fan Swag Token C", "Super-Fan Swag Token B", "Super-Fan Swag Token A" });
        Setup();

        Core.SetOptions(false);
    }

    public void Setup(int quant = 100)
    {
        if (Core.CheckInventory("Super-Fan Swag Token A", quant))
            return;

        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Core.AddDrop("Super-Fan Swag Token A", "Super-Fan Swag Token B", "Super-Fan Swag Token C", "Super-Fan Swag Token D");
        Core.FarmingLogger($"Super-Fan Swag Token A", quant);
        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(Bot.Config!.Get<bool>("MemOrNonMem") ? new[] { 1310, 1312, 1313, 1314 } : new[] { 1304, 1307 });
        while (!Bot.ShouldExit && !Core.CheckInventory("Super-Fan Swag Token A", quant))
        {
            int dQuantity = Bot.Inventory.GetQuantity("Super-Fan Swag Token D");
            int cQuantity = Bot.Inventory.GetQuantity("Super-Fan Swag Token C");
            int bQuantity = Bot.Inventory.GetQuantity("Super-Fan Swag Token B");
            int aQuantity = Bot.Inventory.GetQuantity("Super-Fan Swag Token A");

            while (!Bot.ShouldExit && !Core.CheckInventory("Super-Fan Swag Token C", 500))
            {
                // Army.RunGeneratedAggroMon(map, monNames, questIDs, classtype, drops);
                Army.AggroMonStart(Bot.Config.Get<bool>("MemOrNonMem") ? "collectorlab" : "terrarium");
                Army.DivideOnCells(Bot.Config.Get<bool>("MemOrNonMem") ? new[] { "r3", "Enter", "r2" } : new[] { "r3", "Enter" });
                Army.AggroMonIDs(Bot.Config.Get<bool>("MemOrNonMem") ? new[] { 710, 711, 712, 713 } : new[] { 701, 703 });

                while (!Bot.ShouldExit && !Core.CheckInventory("Super-Fan Swag Token C", 500))
                    Bot.Combat.Attack("*");
                Army.AggroMonStop();
                Core.JumpWait();
            }

            bool ShopCheck = ShopCheck = Bot.Map.Name == "collection" && Bot.Shops.IsLoaded && Bot.Shops.Name == "Super Fan Token Shop";

            while (!Bot.ShouldExit && !ShopCheck)
            {
                if (Bot.Map.Name != "Collection")
                    Core.Join("Collection");

                if (Bot.Player.Cell != "Begin")
                    Core.Jump("Begin");

                Bot.Shops.Load(325);
                Bot.Wait.ForActionCooldown(GameActions.LoadShop);
                Bot.Wait.ForTrue(() => Bot.Shops.IsLoaded, 20);
                ShopCheck = ShopCheck = Bot.Map.Name == "collection" && Bot.Shops.IsLoaded && Bot.Shops.Name == "Super Fan Token Shop";
                if (ShopCheck)
                    break;
            }
            Bot.Wait.ForActionCooldown(GameActions.LoadShop);

            Core.Sleep();
            ShopCheck = ShopCheck = Bot.Map.Name == "collection" && Bot.Shops.IsLoaded && Bot.Shops.Name == "Super Fan Token Shop";
            if (ShopCheck && dQuantity / 10 > 1 && cQuantity < 500 && dQuantity / 10 + cQuantity < 500)
            {
                int buyC = dQuantity / 10;
                Core.Logger($"Buying {buyC} Super-Fan Swag Token C.");
                Bot.Shops.BuyItem("Super-Fan Swag Token C", buyC);
                Bot.Wait.ForActionCooldown(GameActions.BuyItem);
                Bot.Wait.ForItemBuy();
            }
            Core.Sleep();
            ShopCheck = ShopCheck = Bot.Map.Name == "collection" && Bot.Shops.IsLoaded && Bot.Shops.Name == "Super Fan Token Shop";
            if (ShopCheck && cQuantity / 10 > 1 && bQuantity < 200 && cQuantity / 10 + bQuantity < 200)
            {
                int buyB = cQuantity / 10;
                Core.Logger($"Buying {buyB} Super-Fan Swag Token B.");
                Bot.Shops.BuyItem("Super-Fan Swag Token B", buyB);
                Bot.Wait.ForActionCooldown(GameActions.BuyItem);
                Bot.Wait.ForItemBuy();
            }
            Core.Sleep();
            ShopCheck = ShopCheck = Bot.Map.Name == "collection" && Bot.Shops.IsLoaded && Bot.Shops.Name == "Super Fan Token Shop";
            if (ShopCheck && bQuantity / 20 > 1 && aQuantity < 100 && bQuantity / 20 + aQuantity < 100)
            {
                int buyA = bQuantity / 20;
                Core.Logger($"Buying {buyA} Super-Fan Swag Token A.");
                Bot.Shops.BuyItem("Super-Fan Swag Token A", buyA);
                Bot.Wait.ForActionCooldown(GameActions.BuyItem);
                Bot.Wait.ForItemBuy();
            }
            Core.Sleep();
            ShopCheck = ShopCheck = Bot.Map.Name == "collection" && Bot.Shops.IsLoaded && Bot.Shops.Name == "Super Fan Token Shop";

        }
        Core.CancelRegisteredQuests();
    }
}
