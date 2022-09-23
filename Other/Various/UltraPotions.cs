//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs

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
        Core.BankingBlackList.Add("Dragon Runestone");
        INeedYourStrongestPotions();

        Core.SetOptions(false);
    }

    //potionQuant is set to 30, due to reagents max quant being 30
    public void INeedYourStrongestPotions(int potionQuant = 30, List<string> potions = null, string reagent1 = null, string reagent2 = null)
    {
        #region Potion Seller
        Core.Logger($"{Bot.Player.Username}: Hello Potion Seller, I'm going into battle and I want your strongest potions.");
        Core.Logger($"Potion Seller: You can't handle my potions, they are too strong for you!");
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
        #endregion

        #region Starting Requirements
        Farm.AlchemyREP();
        Farm.GoodREP();
        #endregion

        #region Potion Gathering
        if (potions is null)
            potions = new() { "Potent Malevolence Elixir", "Potent Battle Elixir", "Potent Honor Potion", "Fate Tonic", "Sage Tonic" };
        foreach (string potion in potions)
        {
            InventoryItem reagentstuffs1 = Bot.Inventory.GetItem(reagent1);
            InventoryItem reagentstuffs2 = Bot.Inventory.GetItem(reagent2);
            
            int PurchaseQuant = potionQuant - Bot.Inventory.GetQuantity(potion);
            if (PurchaseQuant < 0)
                PurchaseQuant = 0;
            else Core.FarmingLogger(potion, potionQuant);

            Core.AddDrop(reagent1, reagent2, "Potent Malevolence Elixir", "Potent Battle Elixir", "Potent Honor Potion", "Fate Tonic", "Sage Tonic");

            switch (potion)
            {
                case "Potent Malevolence Elixir":
                case "Potent Battle Elixir":

                    if (Core.CheckInventory(potion, potionQuant))
                    {
                        Core.Logger($"{potion}: {Bot.Inventory.GetQuantity(potion)} / {potionQuant}");
                        break;
                    }

                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        reagent1 = "Doomatter";
                        reagent2 = "Nimblestem";
                        Core.HuntMonster("VordredBoss", "Vordred", reagent1, potionQuant, isTemp: false);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("mudluk", "Swamp Frogdrake", reagent2, potionQuant, isTemp: false);

                        Farm.DragonRunestone(PurchaseQuant);
                        
                        if (potion == "Potent Malevolence Elixir")
                            Farm.AlchemyPacket(reagent1, reagent2, trait: CoreFarms.AlchemyTraits.SPw, P2w: true);
                        else
                            Farm.AlchemyPacket(reagent1, reagent2, trait: CoreFarms.AlchemyTraits.APw, P2w: true);
                    }
                    Core.TrashCan(reagent1, "Dragon Runestone");
                    break;


                case "Potent Honor Potion": //200k/pot vs 500k non-alchemy

                    if (Core.CheckInventory(potion, potionQuant))
                    {
                        Core.Logger($"{potion}: {Bot.Inventory.GetQuantity(potion)} / {potionQuant}");
                        break;
                    }

                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        reagent1 = "Chaos Entity";
                        reagent2 = "Nimblestem";

                        Farm.Gold(PurchaseQuant * 100000);
                        Core.BuyItem("alchemyacademy", 2114, "Gold Voucher 100k", PurchaseQuant);
                        Core.BuyItem("alchemyacademy", 2114, reagent1, PurchaseQuant);
                        Core.HuntMonster("mudluk", "Swamp Frogdrake", reagent2, potionQuant, isTemp: false);
                        Farm.DragonRunestone(PurchaseQuant);
                        Farm.AlchemyPacket(reagent1, reagent2, trait: CoreFarms.AlchemyTraits.Dam, P2w: true);
                    }

                    Core.TrashCan(reagent2, "Dragon Runestone");
                    break;

                case "Fate Tonic":

                    if (Core.CheckInventory(potion, potionQuant))
                    {
                        Core.Logger($"{potion}: {Bot.Inventory.GetQuantity(potion)} / {potionQuant}");
                        break;
                    }

                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        reagent1 = "Trollola Nectar";
                        reagent2 = "Arashtite Ore";


                        Core.HuntMonster("Bloodtusk", "Trollola Plant", reagent1, potionQuant, isTemp: false);
                        Core.HuntMonster("orecavern", "Deathmole", reagent2, potionQuant, isTemp: false);

                        Farm.DragonRunestone(PurchaseQuant);
                        Farm.AlchemyPacket(reagent1, reagent2, trait: CoreFarms.AlchemyTraits.Luc, P2w: true);
                    }
                    Core.TrashCan("Dragon Runestone");
                    break;


                case "Sage Tonic":

                    if (Core.CheckInventory(potion, potionQuant))
                    {
                        Core.Logger($"{potion}: {Bot.Inventory.GetQuantity(potion)} / {potionQuant}");
                        break;
                    }

                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        reagent1 = "Arashtite Ore";
                        reagent2 = "Trollola Nectar";

                        Core.AddDrop(reagent1, reagent2);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("orecavern", "Deathmole", reagent1, potionQuant, isTemp: false);
                        Core.HuntMonster("Bloodtusk", "Trollola Plant", reagent2, potionQuant, isTemp: false);

                        Farm.DragonRunestone(PurchaseQuant);
                        Farm.AlchemyPacket(reagent1, reagent2, trait: CoreFarms.AlchemyTraits.Int, P2w: true);
                    }
                    break;
            }
            Core.TrashCan("Arashtite Ore", "Trollola Nectar", "Chaos Entity", "Nimblestem", "Doomatter", "Dragon Runestone");
            #endregion
        }
    }
}