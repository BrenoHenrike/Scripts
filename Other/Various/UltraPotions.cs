//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class PotionBuyer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.Add("Dragon Runestone");
        Core.SetOptions();

        INeedYourStrongestPotions();

        Core.SetOptions(false);
    }

    //potionQuant is set to 30, due to reagents max quant being 30
    public void INeedYourStrongestPotions(string[] potions = null, int potionQuant = 30)
    {
        // #region Potion Seller
        // Core.Logger($"{Bot.Player.Username}: Hello Potion Seller, I'm going into battle and I want your strongest potions.");
        // Core.Logger($"Potion Seller: You can't handle my potions, they are too strong for you!");
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
        // #endregion

        Farm.AlchemyREP();
        Farm.GoodREP();

        #region Potion Gathering

        if (potions is null)
            potions = new[] { "Potent Malevolence Elixir", "Potent Battle Elixir", "Potent Honor Potion", "Fate Tonic", "Sage Tonic", "Potent Malice Potion" };

        Core.AddDrop(potions);

        foreach (string potion in potions)
        {
            if (Core.CheckInventory(potion, potionQuant))
                continue;

            int PurchaseQuant = potionQuant - Bot.Inventory.GetQuantity(potion);
            Core.FarmingLogger(potion, potionQuant);

            string reagent1 = null;
            string reagent2 = null;
            Farm.DragonRunestone(PurchaseQuant);

            switch (potion)
            {
                case "Potent Malevolence Elixir":
                case "Potent Battle Elixir":
                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        GetIngredient("Doomatter");
                        GetIngredient("Nimblestem");
                        Farm.AlchemyPacket(reagent1, reagent2, trait: potion == "Potent Malevolence Elixir" ? CoreFarms.AlchemyTraits.SPw : CoreFarms.AlchemyTraits.APw, P2w: true);
                    }
                    break;

                case "Potent Honor Potion": //200k/pot vs 500k non-alchemy
                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        GetIngredient("Chaos Entity");
                        GetIngredient("Nimblestem");
                        Farm.AlchemyPacket(reagent1, reagent2, trait: CoreFarms.AlchemyTraits.Dam, P2w: true);
                    }
                    break;

                case "Fate Tonic":

                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        GetIngredient("Trollola Nectar");
                        GetIngredient("Arashtite Ore");
                        Farm.AlchemyPacket(reagent1, reagent2, trait: CoreFarms.AlchemyTraits.Luc, P2w: true);
                    }
                    break;

                case "Sage Tonic":
                    while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                    {
                        GetIngredient("Arashtite Ore");
                        GetIngredient("Trollola Nectar");
                        Farm.AlchemyPacket(reagent1, reagent2, trait: CoreFarms.AlchemyTraits.Int, P2w: true);
                    }
                    break;

                default:
                    Core.Logger("The bot was not taught how to make " + potion);
                    break;
            }
            Core.TrashCan("Arashtite Ore", "Trollola Nectar", "Chaos Entity", "Nimblestem", "Doomatter");
            #endregion

            void GetIngredient(string ingredient)
            {
                if (reagent1 == null)
                    reagent1 = ingredient;
                else if (reagent2 == null)
                    reagent2 = ingredient;

                if (Core.CheckInventory(ingredient, potionQuant))
                    return;

                Core.EquipClass(ClassType.Farm);

                switch (ingredient)
                {
                    case "Arashtite Ore":
                        Core.HuntMonster("orecavern", "Deathmole", ingredient, potionQuant, isTemp: false);
                        break;
                    case "Chaos Entity":
                        Farm.Gold(PurchaseQuant * 100000);
                        Core.BuyItem("alchemyacademy", 2114, "Gold Voucher 100k", PurchaseQuant);
                        Core.BuyItem("alchemyacademy", 2114, ingredient, PurchaseQuant);
                        break;
                    case "Doomatter":
                        Core.HuntMonster("vordredboss", "Vordred", reagent1, potionQuant, isTemp: false);
                        break;
                    case "Nimblestem":
                        Core.HuntMonster("mudluk", "Swamp Frogdrake", reagent2, potionQuant, isTemp: false);
                        break;
                    case "Trollola Nectar":
                        Core.HuntMonster("bloodtusk", "Trollola Plant", ingredient, potionQuant, isTemp: false);
                        break;
                    default:
                        Core.Logger("The bot was not taught where to get " + ingredient);
                        break;
                }
            }
        }
    }
}