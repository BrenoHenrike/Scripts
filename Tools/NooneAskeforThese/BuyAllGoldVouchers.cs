/*
name: Buy All Gold Vouchers
description: as the name says
tags: gold voucher, gold, voucher
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Quests;
using Skua.Core.Models.Shops;

public class BuyAllGoldVouchers
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    private CoreAdvanced Adv = new();
    private CoreFarms Farm = new();
    private CoreStory Story = new();
    private CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Getemall();

        Core.SetOptions(false);
    }

    public void Getemall(bool TestMode = false)
    {
        Core.Logger("This script is pointless... but was requested to waste gold.");
        foreach (string voucher in new[] { "500", "200", "100", "25", "7.5" })
        {
            string formattedVoucher = $"Gold Voucher {voucher}k"; // Create the formatted string
            switch (voucher)
            {
                case "500":
                case "100":
                case "200":
                    while (!Bot.ShouldExit && Bot.Shops.Name != "Gebo Shop")
                    {
                        Core.Join("alchemyacademy");
                        Bot.Wait.ForMapLoad("alchemyacademy");
                        Bot.Shops.Load(2036);
                        Bot.Wait.ForActionCooldown(Skua.Core.Models.GameActions.LoadShop);
                        Core.Sleep();
                    }
                    ShopItem? Item = Bot.Shops.Items.FirstOrDefault(s => s != null && s.Name == formattedVoucher);
                    if (Item != null)
                    {
                        Core.FarmingLogger(Item.Name, Item.MaxStack);
                        int currentQuantity = Bot.Inventory.GetQuantity(formattedVoucher);
                        Farm.Gold(Math.Max(0, Math.Min(Item.MaxStack, 300 - currentQuantity) * 500000));
                        Core.BuyItem("alchemyacademy", 2036, Item.Name, Math.Min(200, Math.Max(0, Math.Min(Item.MaxStack, 300 - currentQuantity))));
                    }
                    else
                    {
                        Core.Logger($"Item '{formattedVoucher}' not found in the shop.");
                    }
                    break;

                case "25":
                    while (!Bot.ShouldExit && Bot.Shops.Name != "Hydra Merge")
                    {
                        Core.Join("titanattack");
                        Bot.Wait.ForMapLoad("titanattack");
                        Bot.Shops.Load(1597);
                        Bot.Wait.ForActionCooldown(Skua.Core.Models.GameActions.LoadShop);
                        Core.Sleep();
                    }
                    ShopItem? Item2 = Bot.Shops.Items.FirstOrDefault(s => s != null && s.Name == formattedVoucher);
                    if (Item2 != null)
                    {
                        int currentQuantity2 = Bot.Inventory.GetQuantity(formattedVoucher);
                        Farm.Gold(Math.Max(0, Math.Min(Item2.MaxStack, 300 - currentQuantity2) * 500000));
                        Core.BuyItem("mobius", 1597, Item2.Name, Math.Min(200, Math.Max(0, Math.Min(Item2.MaxStack, 300 - currentQuantity2))));
                    }
                    else
                    {
                        Core.Logger($"Item '{formattedVoucher}' not found in the shop.");
                    }
                    break;

                case "7.5":
                    while (!Bot.ShouldExit && Bot.Shops.Name != "Fehu Shop")
                    {
                        Core.Join("alchemyacademy");
                        Bot.Wait.ForMapLoad("alchemyacademy");
                        Bot.Shops.Load(2116);
                        Bot.Wait.ForActionCooldown(Skua.Core.Models.GameActions.LoadShop);
                        Core.Sleep();
                    }
                    ShopItem? Item3 = Bot.Shops.Items.FirstOrDefault(s => s != null && s.Name == formattedVoucher);
                    if (Item3 != null)
                    {
                        int currentQuantity3 = Bot.Inventory.GetQuantity(formattedVoucher);
                        Farm.Gold(Math.Max(0, Math.Min(Item3.MaxStack, 300 - currentQuantity3) * 500000));
                        Core.BuyItem("alchemyacademy", 2116, Item3.Name, Math.Min(200, Math.Max(0, Math.Min(Item3.MaxStack, 300 - currentQuantity3))));
                    }
                    else
                    {
                        Core.Logger($"Item '{formattedVoucher}' not found in the shop.");
                    }
                    break;
            }
        }
    }
}
