//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
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
        // new Option<bool>("GoldMethod", "Use Gold", "Buy Potions with Gold(Expensive)", false),
        new Option<bool>("farmFate", "Fate", "Should the bot farm Fate Tonics?", false),
        new Option<bool>("farmBattle", "Battle/Malevolence", "Should the bot farm Battle and Malevolence Tonics?", false),
        new Option<bool>("farmHonor", "Honor", "Should the bot farm Honor Potions?", false),
        new Option<bool>("farmSage", "Sage", "Should the bot farm Sage Tonics?", false),
        new Option<bool>("farmDivine", "Divine", "Should the bot farm Unstabe Divine Elixers?", false),
        new Option<int>("potionQuant", "Potion Quantity", "Desired stack amount:")
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
            potions = new[] { "Potent Malevolence Elixir", "Potent Battle Elixir", "Potent Honor Potion", "Fate Tonic", "Sage Tonic", "Unstable Divine Elixir" };
            potionsFarm = new[] { Bot.Config.Get<bool>("farmBattle"), Bot.Config.Get<bool>("farmHonor"), Bot.Config.Get<bool>("farmFate"), Bot.Config.Get<bool>("farmSage"), Bot.Config.Get<bool>("farmFate"), Bot.Config.Get<bool>("farmDivine") };
        }

        if (Array.IndexOf(potionsFarm, true) == -1 || potionQuant < 1 || potionQuant > 300)
        {
            Core.Logger("No potions were selected to farm or you entered an invalid number of potions to farm, the bot will now stop", messageBox: true, stopBot: true);
            return;
        }

        Core.AddDrop(potions);
        Core.AddDrop("Potent Malice Potion");

        foreach (string potion in potions)
        {
            Core.Logger($"{potionsFarm[Array.IndexOf(potions, potion)]}");
            if (!potionsFarm[Array.IndexOf(potions, potion)])
                continue;
            Core.Logger($"{Core.CheckInventory(potion, potionQuant)}");
            if (Core.CheckInventory(potion, potionQuant))
                continue;
                

            // if (Bot.Config.Get<bool>("GoldMethod"))
            // {
            //     Core.Logger($"Using Gold to Purchase {potion} Exceptions: \"Unstable Divine Elixir\"");
            // }   
            
            Core.FarmingLogger(potion, potionQuant);
            CoreFarms.AlchemyTraits currTrait = CoreFarms.AlchemyTraits.Int;

            switch (potion)
            {
                case "Potent Malevolence Elixir":
                case "Potent Battle Elixir":
                    // if (Bot.Config.Get<bool>("GoldMethod"))
                    // {
                        Adv.BuyItem("alchemyacademy", 2036, potion, potionQuant);
                    //     break;
                    // }
                    currTrait = potion == "Potent Malevolence Elixir" ? CoreFarms.AlchemyTraits.SPw : CoreFarms.AlchemyTraits.APw;
                    BulkGrind("Doomatter", "Nimblestem");
                    break;

                case "Potent Honor Potion": //200k/pot vs 500k non-alchemy
                    // if (Bot.Config.Get<bool>("GoldMethod"))
                    // {
                    //     Adv.BuyItem("alchemyacademy", 2036, potion, potionQuant);
                    //     break;
                    // }
                    currTrait = CoreFarms.AlchemyTraits.Dam;
                    BulkGrind("Chaos Entity", "Nimblestem");
                    break;

                case "Fate Tonic":
                    // if (Bot.Config.Get<bool>("GoldMethod"))
                    // {
                    //     Adv.BuyItem("alchemyacademy", 2036, potion, potionQuant);
                    //     break;
                    // }
                    currTrait = CoreFarms.AlchemyTraits.Luc;
                    BulkGrind("Trollola Nectar", "Arashtite Ore");
                    break;

                case "Sage Tonic":
                    // if (Bot.Config.Get<bool>("GoldMethod"))
                    // {
                    //     Adv.BuyItem("alchemyacademy", 2036, potion, potionQuant);
                    //     break;
                    // }
                    currTrait = CoreFarms.AlchemyTraits.Int;
                    BulkGrind("Arashtite Ore", "Doomatter");
                    break;

                case "Unstable Divine Elixir":
                    currTrait = CoreFarms.AlchemyTraits.hOu;
                    BulkGrind("Dragon Scale", "Searbush");
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
                        Farm.DragonRunestone(30);
                    }
                    Farm.AlchemyPacket(reagent1, reagent2, trait: currTrait, P2w: true);
                }
            }

            void GetIngredient(string ingredient, int ingreQuant = 30)
            {
                // todo: add option to just use gold or prepruchased vouchers

                if (!Bot.Config.Get<bool>("GoldMethod") && ingredient == "Dragon Scale" && Core.CheckInventory(11475, ingreQuant))
                    return; //there are 2 dragon scales and it has to be checked by itemID

                if (Core.CheckInventory(ingredient, ingreQuant))
                    return;

                Core.EquipClass(ClassType.Farm);

                switch (ingredient)
                {
                    case "Arashtite Ore":
                        Core.HuntMonster("orecavern", "Deathmole", ingredient, ingreQuant, isTemp: false);
                        break;
                    case "Chaos Entity":
                        //     Farm.Gold(ingreQuant * 100000);
                        //     Core.BuyItem("alchemyacademy", 2114, "Gold Voucher 100k", ingreQuant);
                        Adv.BuyItem("alchemyacademy", 2114, ingredient, ingreQuant);
                        break;
                    case "Doomatter":
                        // Farm.Gold(ingreQuant * 30000);
                        Adv.BuyItem("tercessuinotlim", 1951, "Doomatter", ingreQuant);
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
                        while (!Bot.ShouldExit && !Core.CheckInventory(11475, ingreQuant))
                            Core.KillMonster("lair", "Hole", "Center", "*", log: false);
                        break;
                    default:
                        Core.Logger("The bot was not taught where to get " + ingredient);
                        break;
                }
            }
        }
    }
}