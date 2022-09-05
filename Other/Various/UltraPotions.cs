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

    //potionQuant is set to 30, due to reagents max quant being 30
    public void INeedYourStrongestPotions(int potionQuant = 30, List<string> potions = null, bool Alchemy = true)
    {
        Farm.AlchemyREP();
        Core.Logger($"{Bot.Player.Username}: Hello Potion Seller, I'm going into battle and I want your strongest potions.");
        if (potions is null)
            potions = new() { "Potent Battle Elixir", "Potent Honor Potion", "Fate Tonic", "Potent Malevolence Elixir", "Sage Tonic" };
        List<ShopItem> shopItems;
        Core.Logger($"Potion Seller: You can't handle my potions, they are too strong for you!");
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
        // Core.Logger($"Potion Seller: My potions are too strong for you, traveller.");
        // Bot.Sleep(2500);
        // Core.Logger($"{Bot.Player.Username}: Potion Seller! I tell you, I'm going into battle and I want only your strongest potions.");
        // Bot.Sleep(2500);
        // Core.Logger($"Potion Seller: You can't handle my potions, they are too strong for you!");
        // Bot.Sleep(2500);
        // Core.Logger($"{Bot.Player.Username}: Potion Seller, listen to me, I want only your strongest potions.");
        // Bot.Sleep(2500);
        // Core.Logger($"Potion Seller: My potions would kill you traveller, you cannot handle my potions.");
        // Bot.Sleep(2500);
        // Core.Logger($"{Bot.Player.Username}: POTION SELLER! I require your strongest potions!");
        // Bot.Sleep(2500);
        // Core.Logger($"Potion Seller: My strongest potions would kill you traveller, you can't handle my strongest potions. You better go to a seller that sells weaker potions.");
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

            if (!Alchemy)
                potionQuant = 50;

            switch (potion)
            {
                case "Potent Battle Elixir":
                    Core.FarmingLogger("Potent Battle Elixir", potionQuant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        string reagent1 = "Doomatter";
                        string reagent2 = "Nimblestem";
                        if (Alchemy)
                        {
                            Core.AddDrop(reagent1, reagent2);
                            Core.AddDrop(reagent1, reagent2);
                            if (!Core.CheckInventory(reagent1, potionQuant) && !Core.CheckInventory(reagent2, potionQuant))
                            {
                                Core.EquipClass(ClassType.Solo);
                                Core.HuntMonster("maul", "Creature Creation", reagent1, potionQuant);
                                Core.EquipClass(ClassType.Farm);
                                Core.HuntMonster("mudluk", "Swamp Frogdrake", reagent2, potionQuant);
                            }
                            Farm.AlchemyPacket(reagent1, reagent2);
                        }
                        else Adv.BuyItem("alchemyacademy", 2036, potion, potionQuant, shopQuant);
                        Core.SellItem(reagent1, all: true);
                        Core.SellItem(reagent2, all: true);
                    }
                    break;


                case "Potent Honor Potion": //200k/pot vs 500k non-alchemy
                    Core.FarmingLogger("Potent Honor Potion", potionQuant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        string reagent1 = "Chaos Entity";
                        string reagent2 = "Fish Oil";

                        Core.AddDrop(reagent1, reagent2);

                        if (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                        {
                            Adv.BuyItem("alchemyacademy", 2114, reagent1, potionQuant, shopQuant);
                            if (!Core.CheckInventory(11467, potionQuant))
                                Adv.BuyItem("alchemyacademy", 397, reagent2, potionQuant, shopQuant);

                            Farm.AlchemyPacket(reagent1, reagent2);
                        }
                        Core.SellItem(reagent1, all: true);
                        Core.SellItem(reagent2, all: true);
                    }
                    break;

                case "Fate Tonic":
                    Core.FarmingLogger("Fate Tonic", potionQuant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        string reagent1 = "Trollola Nectar";
                        string reagent2 = "Swamp Frogdrake";
                        if (Alchemy)
                        {
                            Core.AddDrop(reagent1, reagent2);
                            Core.EquipClass(ClassType.Farm);
                            Core.HuntMonster("Bloodtusk", "Trollola Plant", reagent1, potionQuant, isTemp: false);
                            Core.HuntMonster("mudluk", "Swamp Frogdrake", reagent2, potionQuant);

                            Farm.AlchemyPacket(reagent1, reagent2, rank: 8);
                        }
                        else Adv.BuyItem("alchemyacademy", 2036, potion, potionQuant, shopQuant);
                        Core.SellItem(reagent1, all: true);
                        Core.SellItem(reagent2, all: true);
                    }
                    break;

                case "Potent Malevolence Elixir":
                    Core.FarmingLogger("Potent Malevolence Elixir", potionQuant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        string reagent1 = "Doomatter";
                        string reagent2 = "Nimblestem";
                        if (Alchemy)
                        {
                            Core.AddDrop(reagent1, reagent2);
                            Core.EquipClass(ClassType.Solo);
                            Core.HuntMonster("maul", "Creature Creation", reagent1, potionQuant);
                            Core.EquipClass(ClassType.Farm);
                            Core.HuntMonster("mudluk", "Swamp Frogdrake", reagent2, potionQuant);

                            Farm.AlchemyPacket(reagent1, reagent2);
                        }
                        else Adv.BuyItem("alchemyacademy", 2036, potion, potionQuant, shopQuant);
                        Core.SellItem(reagent1, all: true);
                        Core.SellItem(reagent2, all: true);
                    }
                    break;

                case "Sage Tonic":
                    Core.FarmingLogger("Sage Tonic", potionQuant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        string reagent1 = "Arashtite Ore";
                        string reagent2 = "Dragon Scale";
                        if (Alchemy)
                        {
                            Core.AddDrop(reagent1, reagent2);
                            Core.EquipClass(ClassType.Farm);
                            Core.HuntMonster("orecavern", "Deathmole", reagent1, potionQuant);
                            if (!Core.CheckInventory(11475))
                                Core.KillMonster("lair", "Enter", "Spawn", "*", reagent2, potionQuant, false);

                            Farm.AlchemyPacket(reagent1, reagent2);
                        }
                        else Adv.BuyItem("alchemyacademy", 2036, potion, potionQuant, shopQuant);
                        Core.SellItem(reagent1, all: true);
                        Core.SellItem(reagent2, all: true);
                    }
                    break;
            }
        }
    }
}