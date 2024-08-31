/*
name: Potions
description: null
tags: null
*/
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
    public List<IOption> Options = new()
    {
        new Option<bool>("BuyReagents", "Buy Reagents?", "Use gold to buy the reagents for the Potions [ this takes **ALOT** of gold.].", false),
        new Option<int>("PotionQuant", "Potion Quantity", "Desired stack amount [max - 300]", 0),
        new Option<bool>("MaxAll", "Max all Potions iwthin the script.", "as the name says", false),
        CoreBots.Instance.SkipOptions,

        //Tonic
        new Option<bool>("FarmFate", "Fate", "Should the bot Farm Fate Tonics?", false),
        new Option<bool>("FarmSage", "Sage", "Should the bot Farm Sage Tonics?", false),
        new Option<bool>("FarmMight", "Might", "Should the bot Farm Might Tonics?", false),
        new Option<bool>("FarmFortitude", "Fortitude", "Should the bot Farm Fortitude Tonics?", false),
        new Option<bool>("FarmJudgment", "Judgment", "Should the bot Farm Judgment Tonics?", false),

        //Elixir
        new Option<bool>("FarmBattle", "Battle", "Should the bot Farm Battle Elixirs?", false),
        new Option<bool>("FarmMalevolence", "Malevolence", "Should the bot Farm Malevolence Elixirs?", false),
        new Option<bool>("FarmDivine", "Divine", "Should the bot Farm Unstable Divine Elixers?", false),
        new Option<bool>("FarmRevitalize", "Revitalize", "Should the bot Farm Potent Revitalize Elixirs", false),
        new Option<bool>("FarmDestruction", "Destruction", "Should the bot Farm Potent Destruction Elixir?", false),
        new Option<bool>("FarmFelicitousPhiltre", "Felicitous Philtre", "Should the bot Farm Felicitous Philtre?", false),
        new Option<bool>("FarmEnduranceDraught", "Endurance Draught", "Should the bot Farm Endurance Draught?", false),

        //Potion
        new Option<bool>("FarmMalic", "Malic", "Should the bot Farm Malic Potions?", false),
        new Option<bool>("FarmHonor", "Honor", "Should the bot Farm Honor Potions?", false),
        new Option<bool>("FarmLife", "Life", "Should the bot Potent Life Potion?", false),
        new Option<bool>("FarmBody", "Body", "Should the bot Farm Body Tonics?", false),
        new Option<bool>("FarmSoul", "Soul", "Should the bot Farm Soul Potions?", false),

        //Other          Potent Life Potion
        new Option<bool>("buyFeli", "Felicitous Philtre", "Should the bot buy Felicitous Philtre?", false),
        new Option<bool>("buyEndu", "Endurance Draught", "Should the bot buy Endurance Draught?", false)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.Add("Dragon Runestone");
        Core.SetOptions();
        // INeedYourStrongestPotions(null, null, PotionQuant: Bot.Config!.Get<int>("PotionQuant"), BuyReagents: Bot.Config!.Get<bool>("BuyReagents"));

        INeedYourStrongestPotions(
            Potions: Bot.Config!.Get<bool>("MaxAll") ?
                        new[]
                        {
                            "Judgment Tonic", "Fortitude Tonic", "Fate Tonic", "Sage Tonic", "Potent Battle Elixir",
                            "Potent Malevolence Elixir", "Potent Honor Potion", "Unstable Divine Elixir",
                            "Potent Revitalize Elixir", "Endurance Draught", "Felicitous Philtre", "Potent Destruction Elixir",
                            "Body Tonic", "Soul Potion", "Unstable Battle Elixir", "Unstable Body Tonic", "Unstable Fate Tonic",
                            "Unstable Keen Elixir", "Unstable Mastery Tonic", "Unstable Might Tonic", "Unstable Wise Tonic",
                            "Might Tonic", "Malic Potion", "Potent Life Potion"
                        } : null,
                        PotionsFarm: Bot.Config!.Get<bool>("MaxAll") ?
                        new[]
                        {
                            true, true, true, true, true, true, true, true, true, true, true, true,
                            true, true, true, true, true, true, true, true, true, true, true, true, true
                        } : null,
                        PotionQuant: Bot.Config!.Get<bool>("MaxAll") ? 300 : Bot.Config!.Get<int>("PotionQuant"),
                        BuyReagents: Bot.Config!.Get<bool>("BuyReagents")
                        );

        Core.SetOptions(false);
    }

    public void INeedYourStrongestPotions(string[]? Potions = null, bool[]? PotionsFarm = null, int PotionQuant = 300, bool BuyReagents = false, bool Seperate = false)
    {
        BuyReagents = Bot.Config!.Get<bool>("BuyReagents") || BuyReagents != false;

        Farm.AlchemyREP();
        Farm.GoodREP();

        Core.Logger(BuyReagents ? "Method Choose: Buy Reagents" : "Farm Reagents");

        bool maxAll = Bot.Config!.Get<bool>("MaxAll");

        // Set default potion names if Potions array is not provided
        if (!Seperate || Potions == null && PotionsFarm == null)
        {
            Potions = new[]
            {
            "Judgment Tonic", "Fortitude Tonic", "Fate Tonic", "Sage Tonic", "Potent Battle Elixir",
            "Potent Malevolence Elixir", "Potent Honor Potion", "Unstable Divine Elixir",
            "Potent Revitalize Elixir", "Endurance Draught", "Felicitous Philtre", "Potent Destruction Elixir",
            "Body Tonic", "Soul Potion", "Unstable Battle Elixir", "Unstable Body Tonic",
            "Unstable Fate Tonic", "Unstable Keen Elixir", "Unstable Mastery Tonic",
            "Unstable Might Tonic", "Unstable Wise Tonic", "Might Tonic", "Malic Potion","Potent Life Potion"
        };
        }

        if (maxAll)
        {
            // If MaxAll is enabled, log a message indicating that other options are being overridden
            Core.Logger("MaxAll option is enabled. Other potion farming options will be overridden.");

            // Set all individual potion farming options to true and PotionQuant to 300
            PotionsFarm = new[]
            {
            true, true, true, true, true, true, true, true, true, true, true, true,
            true, true, true, true, true, true, true, true, true, true, true, true, true
        };

            PotionQuant = 300;
        }
        else
        {
            // Allow individual potion farming options to be set based on configuration
            if (!Seperate || Potions == null && PotionsFarm == null)
            {
                PotionsFarm = new[]
                {
                Bot.Config!.Get<bool>("FarmJudgment"), Bot.Config!.Get<bool>("FarmFortitude"),
                Bot.Config!.Get<bool>("FarmFate"), Bot.Config!.Get<bool>("FarmSage"),
                Bot.Config!.Get<bool>("FarmBattle"), Bot.Config!.Get<bool>("FarmMalevolence"),
                Bot.Config!.Get<bool>("FarmHonor"), Bot.Config!.Get<bool>("FarmDivine"),
                Bot.Config!.Get<bool>("FarmRevitalize"), Bot.Config!.Get<bool>("FarmEnduranceDraught"),
                Bot.Config!.Get<bool>("buyFeli"), Bot.Config!.Get<bool>("FarmDestruction"),
                Bot.Config!.Get<bool>("FarmBody"), Bot.Config!.Get<bool>("FarmSoul"),
                Bot.Config!.Get<bool>("UnstableBattle"), Bot.Config!.Get<bool>("UnstableBody"),
                Bot.Config!.Get<bool>("UnstableFate"), Bot.Config!.Get<bool>("UnstableKeen"),
                Bot.Config!.Get<bool>("UnstableMastery"), Bot.Config!.Get<bool>("UnstableMight"),
                Bot.Config!.Get<bool>("UnstableWise"), Bot.Config!.Get<bool>("FarmMight"),
                Bot.Config!.Get<bool>("FarmLife"), Bot.Config!.Get<bool>("FarmLife")
            };
            }
        }


        if (!Seperate && !PotionsFarm!.Any(x => x) || PotionQuant < 1 || PotionQuant > 300)
        {
            Core.Logger($"No Potions were selected to Farm or you entered an invalid number of Potions to Farm[{Bot.Config!.Get<int>("PotionQuant")} / 300], the bot will now stop", messageBox: true, stopBot: true);
            return;
        }

        for (int i = 0; i < Potions!.Length; i++)
            Core.Logger($"- {Potions[i]} x {Core.dynamicQuant(Potions[i], false)}/ {PotionQuant}");

        Core.AddDrop(Potions);

        //2ndary potions that are obtained alongside the normal versions, to be banked and added as a drop.
        string[] SecondaryPotions = new[] {"Potent Malice Potion", "Potent Soul Potion"};
        Core.ToBank(SecondaryPotions);
        Bot.Drops.Add(SecondaryPotions);

        for (int t = 0; t < Potions!.Length; t++)
        {
            if (PotionsFarm != null && !PotionsFarm[t] && !Seperate)
            {
                Core.Logger($"{t}: {PotionsFarm[t]}");
                Core.Sleep();
                continue;
            }

            string Potion = Potions[t];
            Core.Logger($"{t}: {Potion}");

            if (Core.CheckInventory(Potion, PotionQuant))
            {
                Core.FarmingLogger(Potion, PotionQuant);
                continue;
            }

            Core.FarmingLogger(Potion, PotionQuant);

            var currTrait = CoreFarms.AlchemyTraits.Int;

            switch (Potion)
            {
                case "Fate Tonic":
                case "Sage Tonic":
                    currTrait = Potion == "Sage Tonic" ? CoreFarms.AlchemyTraits.Int : CoreFarms.AlchemyTraits.Luc;
                    BulkGrind("Arashtite Ore", "Dried Slime");
                    break;

                case "Potent Battle Elixir":
                case "Potent Malevolence Elixir":
                    currTrait = Potion == "Potent Malevolence Elixir" ? CoreFarms.AlchemyTraits.SPw : CoreFarms.AlchemyTraits.APw;
                    BulkGrind("Doomatter", "Chaoroot");
                    break;

                case "Potent Honor Potion":
                case "Potent Malice Potion":
                    currTrait = CoreFarms.AlchemyTraits.Dam;
                    BulkGrind("Chaoroot", "Chaos Entity");
                    break;

                case "Potent Life Potion":
                    currTrait = CoreFarms.AlchemyTraits.Hea;
                    BulkGrind("Dragon Scale", "Searbush");
                    break;

                case "Might Tonic":
                    currTrait = CoreFarms.AlchemyTraits.Dam;
                    BulkGrind("Chaos Entity", "Rhison Blood");
                    break;

                case "Unstable Divine Elixir":
                    if (PotionQuant > 99)
                    {
                        Core.Logger($"Max quant for [{Potion}] is [{PotionQuant}] -Adjusting");
                        PotionQuant = 99;
                    }
                    currTrait = CoreFarms.AlchemyTraits.hOu;
                    BulkGrind("Dragon Scale", "Lemurphant Tears");
                    break;

                case "Potent Revitalize Elixir":
                    currTrait = CoreFarms.AlchemyTraits.hRe;
                    BulkGrind("Chaoroot", "Lemurphant Tears");
                    break;

                case "Felicitous Philtre":
                    // No Farm method
                    Core.Logger($"item: [{Potion}] doesn't currently have a Farm method, forced to buy.");
                    Adv.BuyItem("alchemyacademy", 2036, "Felicitous Philtre", PotionQuant);
                    break;

                case "Endurance Draught":
                    // No Farm method
                    Core.Logger($"item: [{Potion}] doesn't currently have a Farm method, forced to buy.");
                    Adv.BuyItem("alchemyacademy", 2036, "Endurance Draught", PotionQuant);
                    break;

                case "Potent Destruction Elixir":
                    currTrait = CoreFarms.AlchemyTraits.mRe;
                    BulkGrind("Dried Slime", "Arashtite Ore");
                    break;

                case "Body Tonic":
                    currTrait = CoreFarms.AlchemyTraits.End;
                    BulkGrind("Roc Tongue", "Chaoroot");
                    break;

                case "Soul Potion":
                    currTrait = CoreFarms.AlchemyTraits.Dam;
                    BulkGrind("Necrot", "Nimblestem");
                    break;

                case "Unstable Battle Elixir":
                    currTrait = CoreFarms.AlchemyTraits.APw;
                    BulkGrind("Doomatter", "Nimblestem");
                    break;

                case "Unstable Body Tonic":
                    currTrait = CoreFarms.AlchemyTraits.End;
                    BulkGrind("Nimblestem", "Roc Tongue");
                    break;

                case "Unstable Fate Tonic":
                    currTrait = CoreFarms.AlchemyTraits.Luc;
                    BulkGrind("Dried Slime", "Trollola Nectar");
                    break;

                case "Unstable Keen Elixir":
                    currTrait = CoreFarms.AlchemyTraits.Cri;
                    // BulkGrind("Chaos Entity", "Fish Oil");
                    BulkGrind("Trollola Nectar", "Doomatter");
                    break;

                case "Unstable Mastery Tonic":
                    currTrait = CoreFarms.AlchemyTraits.Dex;
                    BulkGrind("Chaos Entity", "Dried Slime");
                    break;

                case "Unstable Might Tonic":
                    currTrait = CoreFarms.AlchemyTraits.Str;
                    BulkGrind("Chaos Entity", "Fish Oil");
                    break;

                case "Unstable Velocity Elixir":
                    currTrait = CoreFarms.AlchemyTraits.Eva;
                    BulkGrind("Doomatter", "Nimblestem");
                    break;

                case "Unstable Wise Tonic":
                    currTrait = CoreFarms.AlchemyTraits.Wis;
                    BulkGrind("Moglin Tears", "Rhison Blood");
                    break;

                case "Judgment Tonic":
                    currTrait = CoreFarms.AlchemyTraits.Wis;
                    BulkGrind("Dragon Scale", "Moglin Tears", AlchemyRunes.Jera);
                    break;

                case "Fortitude Tonic":
                    currTrait = CoreFarms.AlchemyTraits.End;
                    BulkGrind("Necrot", "Roc Tongue", AlchemyRunes.Fehu);
                    break;

                default:
                    Core.Logger("The bot was not taught how to make " + Potion);
                    break;
            }

            void BulkGrind(string reagent1, string reagent2, AlchemyRunes AlchemyRune = AlchemyRunes.Gebo)
            {
                while (!Bot.ShouldExit && !Core.CheckInventory(Potion, PotionQuant))
                {
                    // if (!Core.CheckInventory(reagent1, 1) || !Core.CheckInventory(reagent2, 1) || !Core.CheckInventory("Dragon Runestone", 30))
                    // {
                    GetIngredient(reagent1);
                    GetIngredient(reagent2);

                    // if(Bot.Shops.LoadedCache.Find(X=> X.ID == 395))
                    Adv.BuyItem("alchemyacademy", 395, 62749, 30, 1, 8777);
                    Core.BuyItem("alchemyacademy", 395, "Dragon Runestone", 30, 8844);
                    // Adv.BuyItem("alchemyacademy", 395, "Dragon Runestone", 30, 8844);
                    // }
                    Core.Join("alchemy");
                    Farm.AlchemyPacket(reagent1, reagent2, AlchemyRune, trait: currTrait, item: Potion, quant: PotionQuant);
                }
            }

            void GetIngredient(string ingredient, int ingreQuant = 30)
            {
                if (Core.CheckInventory(ingredient, ingreQuant))
                    return;
                Core.EquipClass(ClassType.Farm);
                Core.ToggleAggro(false);

                switch (ingredient)
                {
                    case "Ice Vapor":
                        if (!BuyReagents)
                            Core.KillMonster("lair", "Enter", "Spawn", "*", "Ice Vapor", 2, isTemp: false, log: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11478, ingreQuant, 2, 1235);
                        break;

                    case "Moglin Tears":
                        if (!Core.IsMember && !BuyReagents)
                            Core.Logger("Farming map is members only, buying the materials");

                        if (!BuyReagents && Core.IsMember)
                            Core.HuntMonster("twig", "Sweetish Fish", ingredient, ingreQuant, isTemp: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11472, ingreQuant, 2, 1229);
                        break;
                    case "Lemurphant Tears":
                        if (!BuyReagents)
                            Core.HuntMonster("ravinetemple", "Lemurphant", ingredient, ingreQuant, isTemp: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11479, ingreQuant, 2, shopItemID: 1236);
                        break;

                    case "Dried Slime":
                        if (!BuyReagents)
                            Core.HuntMonster("orecavern", "Crashroom", ingredient, ingreQuant, isTemp: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11474, ingreQuant, 2, 1231);
                        break;

                    case "Arashtite Ore":
                        if (!BuyReagents)
                            Core.HuntMonster("orecavern", "Deathmole", ingredient, ingreQuant, isTemp: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11473, ingreQuant, 2, 1230);
                        break;

                    case "Chaos Entity":
                        Adv.BuyItem("alchemyacademy", 2114, 11482, ingreQuant, 1, 9740);
                        break;

                    case "Fish Oil":
                        Adv.BuyItem("alchemyacademy", 397, 11467, ingreQuant, 3, 1224);
                        break;

                    case "Doomatter":
                        if (!BuyReagents)
                        {
                            if (Bot.Player.IsMember)
                                Core.HuntMonster("Creepy", "Fear Feeder", ingredient, ingreQuant, isTemp: false);
                            else Core.HuntMonster("maul", "Creature Creation", ingredient, ingreQuant, isTemp: false);
                        }
                        else Adv.BuyItem("alchemyacademy", 397, 11477, ingreQuant, 2, 1234);
                        break;

                    case "Chaoroot":
                        if (!BuyReagents)
                            Core.HuntMonster("orecavern", "Naga Baas", ingredient, ingreQuant, isTemp: false);
                        else Adv.BuyItem("tercessuinotlim", 1951, 11481, ingreQuant, 10, 7911);
                        break;

                    case "Nimblestem":
                        if (!BuyReagents)
                            Core.HuntMonster("mudluk", "Swamp Frogdrake", "Nimblestem", ingreQuant, isTemp: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11469, ingreQuant, 2, 1226);
                        break;

                    case "Trollola Nectar":
                        if (!BuyReagents)
                            Core.HuntMonster("bloodtusk", "Trollola Plant", ingredient, ingreQuant, isTemp: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11476, ingreQuant, 2, 1233);
                        break;

                    case "Searbush":
                        if (!BuyReagents)
                            Core.HuntMonster("mafic", "Living Fire", ingredient, ingreQuant, isTemp: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11468, ingreQuant, 2, 1225);
                        break;

                    case "Dragon Scale":
                        Core.AddDrop(11475);
                        if (!BuyReagents)
                        {
                            while (!Bot.ShouldExit && !Core.CheckInventory(11475, ingreQuant))
                                Core.KillMonster("lair", "Hole", "Center", "*", isTemp: false, log: false);
                        }
                        else if (!Core.CheckInventory(11475, ingreQuant))
                            Adv.BuyItem("alchemyacademy", 397, 11475, ingreQuant, 2, 1232);
                        break;

                    case "Roc Tongue":
                        if (!BuyReagents)
                            Core.HuntMonster("roc", "Rock Roc", ingredient, ingreQuant, isTemp: false, log: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11471, ingreQuant, 2, 1228);
                        break;

                    case "Necrot":
                        if (!BuyReagents)
                            Core.HuntMonster("deathsrealm", "Skeleton Fighter", ingredient, ingreQuant, isTemp: false, log: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11480, ingreQuant, 2, 1237);
                        break;

                    case "Rhison Blood":
                        if (!BuyReagents)
                            Core.HuntMonster("bloodtusk", "Rhison", ingredient, ingreQuant, isTemp: false, log: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11470, ingreQuant, 2, 1227);
                        break;

                    default:
                        Core.Logger("The bot was not taught where to get " + ingredient);
                        break;
                }
            }
        }
    }

}
