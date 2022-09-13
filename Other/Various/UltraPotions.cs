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
    public void INeedYourStrongestPotions(int potionQuant = 30, List<string> potions = null, bool Alchemy = true, string reagent1 = null, string reagent2 = null)
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
        Core.Logger($"Potion Seller: My potions are too strong for you, traveller.");
        Bot.Sleep(2500);
        Core.Logger($"{Bot.Player.Username}: Potion Seller! I tell you, I'm going into battle and I want only your strongest potions.");
        Bot.Sleep(2500);
        Core.Logger($"Potion Seller: You can't handle my potions, they are too strong for you!");
        Bot.Sleep(2500);
        Core.Logger($"{Bot.Player.Username}: Potion Seller, listen to me, I want only your strongest potions.");
        Bot.Sleep(2500);
        Core.Logger($"Potion Seller: My potions would kill you traveller, you cannot handle my potions.");
        Bot.Sleep(2500);
        Core.Logger($"{Bot.Player.Username}: POTION SELLER! I require your strongest potions!");
        Bot.Sleep(2500);
        Core.Logger($"Potion Seller: My strongest potions would kill you traveller, you can't handle my strongest potions. You better go to a seller that sells weaker potions.");
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
            Core.AddDrop(potion, reagent1, reagent2);

            switch (potion)
            {
                case "Potent Battle Elixir":
                    Core.FarmingLogger("Potent Battle Elixir", potionQuant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        reagent1 = "Rhison Blood";
                        reagent2 = "Nimblestem";
                        
                        if (Alchemy)
                        {
                            Core.AddDrop(reagent1, reagent2);
                            if (!Core.CheckInventory(reagent1, potionQuant) && !Core.CheckInventory(reagent2, potionQuant))
                            {
                                Core.HuntMonster("bloodtusk", "Rhison", reagent1, potionQuant, isTemp: false);
                                Core.HuntMonster("mudluk", "Swamp Frogdrake", reagent2, potionQuant, isTemp: false);
                                Core.EquipClass(ClassType.Farm);
                            }
                            Farm.AlchemyPacket(reagent1, reagent2);
                        }
                        else Adv.BuyItem("alchemyacademy", 2036, potion, potionQuant, shopQuant);
                        Core.TrashCan(reagent1);
                        Core.TrashCan(reagent2);
                    }
                    break;


                case "Potent Honor Potion": //200k/pot vs 500k non-alchemy
                    Core.FarmingLogger("Potent Honor Potion", potionQuant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        reagent1 = "Fish Oil";
                        reagent2 = "Chaoroot";

                        if (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                        {
                            if (!Core.CheckInventory(11467, potionQuant))
                                Adv.BuyItem("alchemyacademy", 397, reagent1, potionQuant, shopQuant);
                            Core.HuntMonster("orecavern", "Naga Baas", reagent2, potionQuant, isTemp: false);

                            Farm.AlchemyPacket(reagent1, reagent2);
                        }
                        Core.TrashCan(reagent1);
                        Core.TrashCan(reagent2);
                    }
                    break;

                case "Fate Tonic":
                    Core.FarmingLogger("Fate Tonic", potionQuant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        reagent1 = "Dried Slime";
                        reagent2 = "Trollola Nectar";
                        
                        Core.EquipClass(ClassType.Farm);
                        Bot.Quests.UpdateQuest(2060); // puts you back to start otherwise.
                        Core.HuntMonster("necrodungeon", "SlimeSkull", reagent1, potionQuant, isTemp: false);
                        Core.HuntMonster("Bloodtusk", "Trollola Plant", reagent2, potionQuant, isTemp: false);

                        Farm.AlchemyPacket(reagent1, reagent2, rank: 8);
                        Core.TrashCan(reagent1);
                        Core.TrashCan(reagent2);
                    }
                    break;

                case "Potent Malevolence Elixir":
                    Core.FarmingLogger("Potent Malevolence Elixir", potionQuant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        reagent1 = "Nimblestem";
                        reagent2 = "Doommatter";
                        
                        if (Alchemy)
                        {
                            Core.AddDrop(reagent1, reagent2);
                            Core.EquipClass(ClassType.Solo);
                            Core.HuntMonster("mudluk", "Swamp Frogdrake", reagent1, potionQuant, isTemp: false);
                            if (Core.IsMember)
                                Core.HuntMonster("Creepy", "Fear Feeder", reagent2, potionQuant, isTemp: false);
                            else Core.HuntMonster("VordredBoss", "Vordred|Enraged Vordred", reagent2, potionQuant, isTemp: false);
                            Core.EquipClass(ClassType.Farm);

                            Farm.AlchemyPacket(reagent1, reagent2);
                        }
                        else Adv.BuyItem("alchemyacademy", 2036, potion, potionQuant, shopQuant);
                        Core.TrashCan(reagent1);
                        Core.TrashCan(reagent2);
                    }
                    break;

                case "Sage Tonic":
                    Core.FarmingLogger("Sage Tonic", potionQuant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        reagent1 = "Arashtite Ore";
                        reagent2 = "Doommatter";
                        
                        if (Alchemy)
                        {
                            Core.AddDrop(reagent1, reagent2);
                            Core.EquipClass(ClassType.Farm);
                            Core.HuntMonster("orecavern", "Deathmole", reagent1, potionQuant, isTemp: false);
                            if (Core.IsMember)
                                Core.HuntMonster("Creepy", "Fear Feeder", reagent2, potionQuant, isTemp: false);
                            else Core.HuntMonster("VordredBoss", "Vordred|Enraged Vordred", reagent2, potionQuant, isTemp: false);

                            Farm.AlchemyPacket(reagent1, reagent2);
                        }
                        else Adv.BuyItem("alchemyacademy", 2036, potion, potionQuant, shopQuant);
                        Core.TrashCan(reagent1);
                        Core.TrashCan(reagent2);
                    }
                    break;
            }
        }
    }
}