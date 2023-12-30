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
        new Option<bool>("BuyReagents", "Buy Reagents?", "Use gold to buy the reagents for the potions [ this takes **ALOT** of gold.].", false),
        new Option<int>("potionQuant", "Potion Quantity", "Desired stack amount [max - 300]", 300),
        CoreBots.Instance.SkipOptions,

        //Tonic
        new Option<bool>("farmFate", "Fate", "Should the bot farm Fate Tonics?", false),
        new Option<bool>("farmSage", "Sage", "Should the bot farm Sage Tonics?", false),
        new Option<bool>("farmMight", "Might", "Should the bot farm Might Tonics?", false),

        //Elixer
        new Option<bool>("farmBattle", "Battle", "Should the bot farm Battle Elixirs?", false),
        new Option<bool>("farmMalevolence", "Malevolence", "Should the bot farm Malevolence Elixirs?", false),
        new Option<bool>("farmDivine", "Divine", "Should the bot farm Unstable Divine Elixers?", false),
        new Option<bool>("farmRevitalize", "Revitalize", "Should the bot farm Potent Revitalize Elixirs", false),
        new Option<bool>("farmDestruction", "Destruction", "Should the bot farm Potent Destruction Elixir?", false),

        //Potion
        new Option<bool>("farmHonor", "Honor", "Should the bot farm Honor Potions?", false),
        new Option<bool>("farmBody", "Body", "Should the bot farm Body Tonics?", false),
        new Option<bool>("farmSoul", "Soul", "Should the bot farm Soul Potions?", false),
        new Option<bool>("UnstableBattle", "Unstable Battle Elixir", "Should the bot farm Soul Potions?", false),
        new Option<bool>("UnstableBody", "Unstable Body Tonic", "Should the bot farm Soul Potions?", false),
        new Option<bool>("UnstableFate", "Unstable Fate Tonic ", "Should the bot farm Soul Potions?", false),
        new Option<bool>("UnstableKeen", "Unstable Keen Elixir", "Should the bot farm Soul Potions?", false),
        new Option<bool>("UnstableMastery", "Unstable Mastery Tonic", "Should the bot farm Soul Potions?", false),
        new Option<bool>("UnstableMight", "Unstable Might Tonic ", "Should the bot farm Soul Potions?", false),
        new Option<bool>("UnstableVelocity", "Unstable Velocity Elixir", "Should the bot farm Soul Potions?", false),
        new Option<bool>("UnstableWise", "Unstable Wise Tonic", "Should the bot farm Soul Potions?", false),

        //Other        
        new Option<bool>("buyFeli", "Felicitous Philtre", "Should the bot buy Felicitous Philtre?", false),
        new Option<bool>("buyEndu", "Endurance Draught", "Should the bot buy Endurance Draught?", false)

    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.Add("Dragon Runestone");
        Core.SetOptions();
        INeedYourStrongestPotions(null, null, Bot.Config!.Get<int>("potionQuant"));

        Core.SetOptions(false);
    }

    public void INeedYourStrongestPotions(string[]? potions = null, bool[]? potionsFarm = null, int potionQuant = 300)
    {
        Farm.AlchemyREP();
        Farm.GoodREP();

        Core.Logger(Bot.Config!.Get<bool>("BuyReagents") ? "Method Choose: Buy Reagents" : "Farm Reagents");

        potions ??= new[] { "Fate Tonic", "Sage Tonic", "Potent Battle Elixir",
            "Potent Malevolence Elixir","Potent Honor Potion", "Unstable Divine Elixir",
            "Potent Revitalize Elixir", "Felicitous Philtre", "Endurance Draught",
            "Potent Destruction Elixir", "Body Tonic", "Soul Potion", "Unstable Battle Elixir",
            "Unstable Body Tonic", "Unstable Fate Tonic", "Unstable Keen Elixir",
            "Unstable Mastery Tonic", "Unstable Might Tonic", "Unstable Velocity Elixir",
            "Unstable Wise Tonic", "Might Tonic"};

        potionsFarm ??= new[] {                         Bot.Config!.Get<bool>("farmFate"),           Bot.Config!.Get<bool>("farmSage"),
            Bot.Config!.Get<bool>("farmBattle"),         Bot.Config!.Get<bool>("farmMalevolence"),    Bot.Config!.Get<bool>("farmHonor"),
            Bot.Config!.Get<bool>("farmDivine"),         Bot.Config!.Get<bool>("farmRevitalize"),
            Bot.Config!.Get<bool>("buyFeli"),            Bot.Config!.Get<bool>("buyEndu"),            Bot.Config!.Get<bool>("farmDestruction"),
            Bot.Config!.Get<bool>("farmBody"),           Bot.Config!.Get<bool>("FarmSoul"),           Bot.Config!.Get<bool>("UnstableBattle"),
            Bot.Config!.Get<bool>("UnstableBody"),       Bot.Config!.Get<bool>("UnstableFate"),       Bot.Config!.Get<bool>("UnstableKeen"),
            Bot.Config!.Get<bool>("UnstableMastery"),    Bot.Config!.Get<bool>("UnstableMight"),      Bot.Config!.Get<bool>("UnstableVelocity"),
            Bot.Config!.Get<bool>("UnstableWise"),       Bot.Config!.Get<bool>("MightTonic") };

        if (!potionsFarm.Any(x => x) || potionQuant < 1 || potionQuant > 300)
        {
            Core.Logger("No potions were selected to farm or you entered an invalid number of potions to farm[<100], the bot will now stop", messageBox: true, stopBot: true);
            return;
        }

        Core.AddDrop(potions);
        Core.AddDrop("Potent Malice Potion");

        for (int t = 0; t < potions.Length; t++)
        {
            if (potionsFarm != null && !potionsFarm[t])
                continue;

            string potion = potions[t];
            if (Core.CheckInventory(potion, potionQuant))
                continue;
            Core.FarmingLogger(potion, potionQuant);

            var currTrait = CoreFarms.AlchemyTraits.Int;

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

                case "Might Tonic":
                    currTrait = CoreFarms.AlchemyTraits.Dam;
                    BulkGrind("Chaos Entity", "Rhison Blood");
                    break;

                case "Sage Tonic":
                case "Fate Tonic":
                    currTrait = potion == "Sage Tonic" ? CoreFarms.AlchemyTraits.Int : CoreFarms.AlchemyTraits.Luc;
                    BulkGrind("Arashtite Ore", "Dried Slime");
                    break;

                case "Potent Revitalize Elixir":
                    currTrait = CoreFarms.AlchemyTraits.hRe;
                    BulkGrind("Chaoroot", "Lemurphant Tears");
                    break;

                case "Felicitous Philtre": // No Farm method
                    Core.Logger($"item: [{potion}] doesn't currently have a farm method, forced to buy.");
                    Adv.BuyItem("alchemyacademy", 2036, "Felicitous Philtre", potionQuant);
                    break;

                case "Endurance Draught": // No Farm method
                    Core.Logger($"item: [{potion}] doesn't currently have a farm method, forced to buy.");
                    Adv.BuyItem("alchemyacademy", 2036, "Endurance Draught", potionQuant);
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

                case "Unstable Divine Elixir":
                    if (potionQuant > 99)
                    {
                        Core.Logger($"Max quant for [{potion}] is [{potionQuant}] -Adjusting");
                        potionQuant = 99;
                    }
                    currTrait = CoreFarms.AlchemyTraits.hOu;
                    BulkGrind("Dragon Scale", "Lemurphant Tears");
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

                //For other scripts:

                case "Bright Tonic":
                    currTrait = CoreFarms.AlchemyTraits.Int;
                    BulkGrind("Arashtite Ore", "Dried Slime");
                    break;

                default:
                    Core.Logger("The bot was not taught how to make " + potion);
                    break;
            }

            void BulkGrind(string reagent1, string reagent2)
            {
                while (!Bot.ShouldExit && !Core.CheckInventory(potion, potionQuant))
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
                    Core.ToggleAggro(false);
                    Farm.AlchemyPacket(reagent1, reagent2, trait: currTrait, P2w: true);
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
                        if (!Bot.Config!.Get<bool>("BuyReagents"))
                            Core.KillMonster("lair", "Enter", "Spawn", "*", "Ice Vapor", 2, isTemp: false, log: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11478, ingreQuant, shopItemID: 1235);
                        break;

                    case "Moglin Tears":
                        if (Bot.Player.IsMember)
                            Core.HuntMonster("twig", "Sweetish Fish", ingredient, ingreQuant, isTemp: false);
                        else
                        {
                            // Adv.BuyItem("alchemyacademy", 395, 7132, ingreQuant, 1, 8844);
                            Adv.BuyItem("alchemyacademy", 395, 62749, 1, 1, 8777);
                            Core.BuyItem("alchemyacademy", 395, "Dragon Runestone", 1, 8844);
                            Adv.BuyItem("alchemyacademy", 397, 11472, ingreQuant, 2, 1229);
                        }
                        break;
                    case "Lemurphant Tears":
                        if (!Bot.Config!.Get<bool>("BuyReagents"))
                            Core.HuntMonster("ravinetemple", "Lemurphant", ingredient, ingreQuant, isTemp: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11479, ingreQuant, shopItemID: 1236);
                        break;

                    case "Dried Slime":
                        if (!Bot.Config!.Get<bool>("BuyReagents"))
                            Core.HuntMonster("orecavern", "Crashroom", ingredient, ingreQuant, isTemp: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11474, ingreQuant, shopItemID: 1231);
                        break;

                    case "Arashtite Ore":
                        if (!Bot.Config!.Get<bool>("BuyReagents"))
                            Core.HuntMonster("orecavern", "Deathmole", ingredient, ingreQuant, isTemp: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11473, ingreQuant, shopItemID: 1230);
                        break;

                    case "Chaos Entity":
                        Adv.BuyItem("alchemyacademy", 2114, 11482, ingreQuant, shopItemID: 9740);
                        break;

                    case "Fish Oil":
                        Adv.BuyItem("alchemyacademy", 397, 11467, ingreQuant, shopItemID: 1224);
                        break;

                    case "Doomatter":
                        if (!Bot.Config!.Get<bool>("BuyReagents"))
                        {
                            if (Bot.Player.IsMember)
                                Core.HuntMonster("Creepy", "Fear Feeder", ingredient, ingreQuant, isTemp: false);
                            else Core.HuntMonster("maul", "Creature Creation", ingredient, ingreQuant, isTemp: false);
                        }
                        else Adv.BuyItem("tercessuinotlim", 1951, 11477, ingreQuant, shopItemID: 7910);
                        break;

                    case "Chaoroot":
                        if (!Bot.Config!.Get<bool>("BuyReagents"))
                            Core.HuntMonster("orecavern", "Naga Baas", ingredient, ingreQuant, isTemp: false);
                        else Adv.BuyItem("tercessuinotlim", 1951, 11481, ingreQuant, shopItemID: 7911);
                        break;

                    case "Nimblestem":
                        if (!Bot.Config!.Get<bool>("BuyReagents"))
                            Core.HuntMonster("mudluk", "Swamp Frogdrake", "Nimblestem", ingreQuant, isTemp: false);
                        else Adv.BuyItem("alchemyacademy", 397, ingredient, ingreQuant, shopItemID: 1226);
                        break;

                    case "Trollola Nectar":
                        if (!Bot.Config!.Get<bool>("BuyReagents"))
                            Core.HuntMonster("bloodtusk", "Trollola Plant", ingredient, ingreQuant, isTemp: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11476, ingreQuant, shopItemID: 1233);
                        break;

                    case "Searbush":
                        if (!Bot.Config!.Get<bool>("BuyReagents"))
                            Core.HuntMonster("mafic", "Living Fire", ingredient, ingreQuant, isTemp: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11468, ingreQuant, shopItemID: 1225);
                        break;

                    case "Dragon Scale":
                        Bot.Drops.Add(11475);
                        if (!Bot.Config!.Get<bool>("BuyReagents"))
                        {
                            while (!Bot.ShouldExit && !Core.CheckInventory(11475, ingreQuant))
                                Core.KillMonster("lair", "Hole", "Center", "*", isTemp: false, log: false);
                        }
                        else if (!Core.CheckInventory(11475, ingreQuant))
                            Adv.BuyItem("alchemyacademy", 397, 11475, ingreQuant, shopItemID: 1232);
                        break;

                    case "Roc Tongue":
                        if (!Bot.Config!.Get<bool>("BuyReagents"))
                            Core.HuntMonster("roc", "Rock Roc", ingredient, ingreQuant, isTemp: false, log: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11471, ingreQuant, shopItemID: 1228);
                        break;

                    case "Necrot":
                        if (!Bot.Config!.Get<bool>("BuyReagents"))
                            Core.HuntMonster("deathsrealm", "Skeleton Fighter", ingredient, ingreQuant, isTemp: false, log: false);
                        else Adv.BuyItem("tercessuinotlim", 1951, 11480, ingreQuant, shopItemID: 7909);
                        break;

                    case "Rhison Blood":
                        if (!Bot.Config!.Get<bool>("BuyReagents"))
                            Core.HuntMonster("bloodtusk", "Rhison", ingredient, ingreQuant, isTemp: false, log: false);
                        else Adv.BuyItem("alchemyacademy", 397, 11470, ingreQuant, shopItemID: 1227);
                        break;

                    default:
                        Core.Logger("The bot was not taught where to get " + ingredient);
                        break;
                }
            }
        }
    }
}
