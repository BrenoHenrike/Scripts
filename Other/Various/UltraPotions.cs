//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class PotionBuyer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public string OptionsStorage = "Potions";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<int>("potionQuant", "Potion Quantity", "Desired stack amount [max - 300]", 1),
        new Option<bool>("farmFate", "Fate", "Should the bot farm Fate Tonics?", false),
        new Option<bool>("farmSage", "Sage", "Should the bot farm Sage Tonics?", false),
        new Option<bool>("farmBattle", "Battle", "Should the bot farm Battle Elixirs?", false),
        new Option<bool>("farmMalevolence", "Malevolence", "Should the bot farm Malevolence Elixirs?", false),
        new Option<bool>("farmHonor", "Honor", "Should the bot farm Honor Potions?", false),
        new Option<bool>("farmDivine", "Divine", "Should the bot farm Unstable Divine Elixers?", false),
        new Option<bool>("farmRevitalize", "Revitalize", "Should the bot farm Potent Revitalize Elixirs", false),
        new Option<bool>("buyFeli", "Felicitous Philtre", "Should the bot buy Felicitous Philtre?", false),
        new Option<bool>("buyEndu", "Endurance Draught", "Should the bot buy Endurance Draught?", false),
        new Option<bool>("farmDestruction", "Destruction", "Should the bot farm Potent Destruction Elixir?", false),
        new Option<bool>("farmBody", "Body", "Should the bot farm Body Tonics?", false)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.Add("Dragon Runestone");
        Core.SetOptions();
        INeedYourStrongestPotions();

        Core.SetOptions(false);
    }

    public void INeedYourStrongestPotions(string[] potions = null, bool[] potionsFarm = null, int potionQuant = 300)
    {
        Farm.AlchemyREP();
        Farm.GoodREP();
        potionQuant = Bot.Config.Get<int>("potionQuant");
        if (potions is null)
        {
            potions = new[] { "Fate Tonic", "Sage Tonic", "Potent Battle Elixir",
            "Potent Malevolence Elixir","Potent Honor Potion", "Unstable Divine Elixir",
            "Potent Revitalize Elixir", "Felicitous Philtre", "Endurance Draught",
            "Potent Destruction Elixir", "Body Tonic" };

            potionsFarm = new[] { Bot.Config.Get<bool>("farmFate"), Bot.Config.Get<bool>("farmSage"),
            Bot.Config.Get<bool>("farmBattle"), Bot.Config.Get<bool>("farmMalevolence"), Bot.Config.Get<bool>("farmHonor"),
            Bot.Config.Get<bool>("farmDivine"), Bot.Config.Get<bool>("farmRevitalize"),
            Bot.Config.Get<bool>("buyFeli"), Bot.Config.Get<bool>("buyEndu"), Bot.Config.Get<bool>("farmDestruction"),
            Bot.Config.Get<bool>("farmBody") };
        }

        if (Array.IndexOf(potionsFarm, true) == -1 || potionQuant < 1 || potionQuant > 300)
        {
            Core.Logger("No potions were selected to farm or you entered an invalid number of potions to farm[<100], the bot will now stop", messageBox: true, stopBot: true);
            return;
        }

        Core.AddDrop(potions);
        Core.AddDrop("Potent Malice Potion");

        foreach (string potion in potions)
        {
            var t = Array.IndexOf(potions, potion);
            if (t < 0)
                continue;
            Core.Logger($"{potion} : {potionsFarm[t]}");
            t = Array.IndexOf(potions, potion);
            if (t < 0)
                continue;
            if (!potionsFarm[t])
                continue;
            Core.Logger($"{Core.CheckInventory(potion, potionQuant)}");
            if (Core.CheckInventory(potion, potionQuant))
                continue;

            Core.FarmingLogger(potion, potionQuant);
            CoreFarms.AlchemyTraits currTrait = CoreFarms.AlchemyTraits.Int;


            switch (potion)
            {

                case "Potent Malevolence Elixir":
                case "Potent Battle Elixir":
                    currTrait = potion == "Potent Malevolence Elixir" ? CoreFarms.AlchemyTraits.SPw : CoreFarms.AlchemyTraits.APw;
                    BulkGrind("Doomatter", "Chaoroot");
                    break;

                case "Potent Honor Potion":
                    currTrait = CoreFarms.AlchemyTraits.Dam;
                    BulkGrind("Chaoroot", "Chaos Entity");
                    break;

                case "Sage Tonic":
                case "Fate Tonic":
                    currTrait = potion == "Sage Tonic" ? CoreFarms.AlchemyTraits.Int : CoreFarms.AlchemyTraits.Luc;
                    BulkGrind("Arashtite Ore", "Dried Slime");
                    break;

                case "Unstable Divine Elixir":
                    potionQuant = 99;
                    Core.Logger($"{potionQuant} : {potionQuant} is the max for this pot. isntead of 300.");
                    currTrait = CoreFarms.AlchemyTraits.hOu;
                    BulkGrind("Dragon Scale", "Lemurphant Tears");
                    break;

                case "Potent Revitalize Elixir":
                    currTrait = CoreFarms.AlchemyTraits.hRe;
                    BulkGrind("Chaoroot", "Lemurphant Tears");
                    break;

                case "Felicitous Philtre": // No Farm method
                    if (potionQuant > 150)
                    {
                        Core.Logger($"{potion}, {potionQuant} is annoying to make so doing 150 as its the max in once shot. buy more yourself.");
                        potionQuant = 150;
                    }
                    if (Bot.Inventory.FreeSlots == 0)
                        Core.Logger("Your inventory is full, please clean it and restart the bot", messageBox: true, stopBot: true);
                    Adv.BuyItem("alchemyacademy", 2036, "Felicitous Philtre", potionQuant > 150 ? 150 : potionQuant);
                    break;

                case "Endurance Draught": // No Farm method
                    if (potionQuant > 150)
                    {
                        Core.Logger($"{potion}, {potionQuant} is annoying to make so doing 150 as its the max in once shot. buy more yourself.");
                        potionQuant = 150;
                    }
                    if (Bot.Inventory.FreeSlots == 0)
                        Core.Logger("Your inventory is full, please clean it and restart the bot", messageBox: true, stopBot: true);
                    Adv.BuyItem("alchemyacademy", 2036, "Endurance Draught", potionQuant > 150 ? 150 : potionQuant);
                    break;

                case "Potent Destruction Elixir":
                    currTrait = CoreFarms.AlchemyTraits.mRe;
                    BulkGrind("Dried Slime", "Arashtite Ore");
                    break;

                case "Body Tonic":
                    currTrait = CoreFarms.AlchemyTraits.End;
                    BulkGrind("Roc Tongue", "Chaoroot");
                    break;

                default:
                    Core.Logger("The bot was not taught how to make " + potion);
                    break;
            }

            void BulkGrind(string reagent1, string reagent2)
            {
                while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
                {
                    if (!Core.CheckInventory(reagent1, 1) || !Core.CheckInventory(reagent2, 1) || !Core.CheckInventory("Dragon Runestone", 1))
                    {
                        GetIngredient(reagent1);
                        GetIngredient(reagent2);
                        // Adv.BuyItem("Alchemy", 395, "Dragon Runestone", 30, 8844); //leave here incase
                        Farm.DragonRunestone(30);
                    }
                    Core.ToggleAggro(enable: false);
                    Farm.AlchemyPacket(reagent1, reagent2, trait: currTrait, P2w: true);
                }
            }

            void GetIngredient(string ingredient, int ingreQuant = 30)
            {
                // todo: add option to just use gold or prepruchased vouchers

                if (Core.CheckInventory(ingredient, ingreQuant))
                    return;

                Core.EquipClass(ClassType.Farm);
                Core.ToggleAggro(false);

                switch (ingredient)
                {
                    case "Lemurphant Tears":
                        Core.HuntMonster("ravinetemple", "Lemurphant", ingredient, ingreQuant, isTemp: false);
                        break;

                    case "Dried Slime":
                        Core.HuntMonster("orecavern", "Crashroom", ingredient, ingreQuant, isTemp: false);
                        break;

                    case "Arashtite Ore":
                        Core.HuntMonster("orecavern", "Deathmole", ingredient, ingreQuant, isTemp: false);
                        break;

                    case "Chaos Entity":
                        // Farm.Gold(100000 * (ingreQuant - Bot.Inventory.GetQuantity("Gold Voucher 100k")));
                        // Adv.BuyItem("alchemyacademy", 2114, "Gold Voucher 100k", ingreQuant);
                        Adv.BuyItem("alchemyacademy", 2114, ingredient, ingreQuant);
                        break;

                    case "Chaoroot":
                    case "Doomatter":
                        Adv.BuyItem("tercessuinotlim", 1951, ingredient, ingreQuant);
                        break;

                    case "Nimblestem":
                        Core.HuntMonster("mudluk", "Swamp Frogdrake", "Nimblestem", ingreQuant, isTemp: false);
                        break;

                    case "Trollola Nectar":
                        Core.HuntMonster("bloodtusk", "Trollola Plant", ingredient, ingreQuant, isTemp: false);
                        break;

                    case "Searbush":
                        Core.HuntMonster("mafic", "Living Fire", ingredient, ingreQuant, isTemp: false);
                        break;

                    case "Dragon Scale":
                        Bot.Drops.Add(11475);
                        while (!Bot.ShouldExit && !Core.CheckInventory(11475, ingreQuant))
                            Core.KillMonster("lair", "Hole", "Center", "*", isTemp: false, log: false);
                        break;

                    case "Roc Tongue":
                        Core.HuntMonster("roc", "Rock Roc", ingredient, ingreQuant, isTemp: false, log: false);
                        break;

                    default:
                        Core.Logger("The bot was not taught where to get " + ingredient);
                        break;
                }
            }
        }
    }
}