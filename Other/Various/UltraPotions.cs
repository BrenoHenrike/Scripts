//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs

using System.Collections.Generic;
using System.Linq;
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Shops;

public class PotionBuyer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        INeedYourStrongestPotions();

        Core.SetOptions(false);
    }


    public void INeedYourStrongestPotions(int potionQuant = 50, List<string> potions = null)
    {
        Farm.AlchemyREP();
        Core.Logger($"{Bot.Player.Username}: Hello Potion Seller, I’m going into battle and I want your strongest potions.");
        if (potions is null)
            potions = new() { "Potent Battle Elixir", "Potent Honor Potion", "Fate Tonic", "Potent Malevolence Elixir", "Sage Tonic" };
        List<ShopItem> shopItems;
        Core.Logger($"Potion Seller: You can’t handle my potions, they are too strong for you!");
        if (Bot.Shops.LoadedCache.Any(s => s.ID == 2036))
        {
            shopItems = Bot.Shops.LoadedCache.First(s => s.ID == 2036).Items;
        }
        else
        {
            Core.Join("alchemyacademy");
            Bot.Shops.Load(2036);
            shopItems = Bot.Shops.Items;
        }

        Core.Logger($"Potion Seller: My potions are too strong for you, traveller.");
        Bot.Sleep(2500);
        Core.Logger($"{Bot.Player.Username}: Potion Seller! I tell you, I’m going into battle and I want only your strongest potions.");
        Bot.Sleep(2500);
        Core.Logger($"Potion Seller: You can’t handle my potions, they are too strong for you!");
        Bot.Sleep(2500);
        Core.Logger($"{Bot.Player.Username}: Potion Seller, listen to me, I want only your strongest potions.");
        Bot.Sleep(2500);
        Core.Logger($"Potion Seller: My potions would kill you traveller, you cannot handle my potions.");
        Bot.Sleep(2500);
        Core.Logger($"{Bot.Player.Username}: POTION SELLER! I require your strongest potions!");
        Bot.Sleep(2500);
        Core.Logger($"Potion Seller: My strongest potions would kill you traveller, you can’t handle my strongest potions. You better go to a seller that sells weaker potions.");
        foreach (string potion in potions)
        {
            int currentQuant = Bot.Inventory.GetQuantity(potion);
            if (currentQuant >= potionQuant)
                continue;

            int shopQuant = shopItems.First(p => p.Name.ToLower() == potion.ToLower()).Quantity;
            int purchaseQuant = (potionQuant - currentQuant) / shopQuant;
            purchaseQuant = purchaseQuant == 0 ? 1 : purchaseQuant;
            int voucherQuant = shopItems.First(p => p.Name.ToLower() == potion.ToLower()).Requirements[0].Quantity * purchaseQuant;
            voucherQuant = voucherQuant == 0 ? 1 : voucherQuant;

            Adv.BuyItem("alchemyacademy", 2036, potion, potionQuant, shopQuant);
        }
    }
}