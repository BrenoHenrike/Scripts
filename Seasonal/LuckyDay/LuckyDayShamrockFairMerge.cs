/*
name: Lucky Day Shamrock Fair Merge
description: This will merge Rainbow Shard, Golden Ticket and Lucky Clovers.
tags: farm, merge, shop, seasonal, lucky, evolved, leprechaun, rainbow, shard, golden, ticket, clover
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class LuckyDayShamrockFairMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Golden Ticket", "Lucky Clover", "Rainbow Shard" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("rainbow"))
            return;

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("rainbow", 256, findIngredients, buyOnlyThis, buyMode: buyMode);

        #region Dont edit this part
        void findIngredients()
        {
            ItemBase req = Adv.externalItem;
            int quant = Adv.externalQuant;
            int currentQuant = req.Temp ? Bot.TempInv.GetQuantity(req.Name) : Bot.Inventory.GetQuantity(req.Name);
            if (req == null)
            {
                Core.Logger("req is NULL");
                return;
            }

            switch (req.Name)
            {
                default:
                    bool shouldStop = Adv.matsOnly ? !dontStopMissingIng : true;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion


                case "Golden Ticket":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.Join("luck");
                        Core.SendPackets("%xt%zm%getMapItem%10173%101%");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Lucky Clover":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(1759);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("rainbow", "Lucky Harms", "Clover Leaves", 1);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Rainbow Shard":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(1758);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("rainbow", "Rainbow Rat", "Prismatic Rainbow Fur", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("34273", "Simple Celtic Dagger", "Mode: [select] only\nShould the bot buy \"Simple Celtic Dagger\" ?", false),
        new Option<bool>("34278", "Three Leaf Clover", "Mode: [select] only\nShould the bot buy \"Three Leaf Clover\" ?", false),
        new Option<bool>("34277", "Snee-Vo's Mallet", "Mode: [select] only\nShould the bot buy \"Snee-Vo's Mallet\" ?", false),
        new Option<bool>("34292", "Bucket O' Gold", "Mode: [select] only\nShould the bot buy \"Bucket O' Gold\" ?", false),
        new Option<bool>("34302", "Pot of Gold Pet", "Mode: [select] only\nShould the bot buy \"Pot of Gold Pet\" ?", false),
        new Option<bool>("34305", "Prismatic Cat's Tail", "Mode: [select] only\nShould the bot buy \"Prismatic Cat's Tail\" ?", false),
        new Option<bool>("34254", "Mog-O'Rahilly", "Mode: [select] only\nShould the bot buy \"Mog-O'Rahilly\" ?", false),
        new Option<bool>("34279", "Whack O' Mallet", "Mode: [select] only\nShould the bot buy \"Whack O' Mallet\" ?", false),
        new Option<bool>("34276", "Lucky Hammer", "Mode: [select] only\nShould the bot buy \"Lucky Hammer\" ?", false),
        new Option<bool>("34258", "Snee-Vo the Clown", "Mode: [select] only\nShould the bot buy \"Snee-Vo the Clown\" ?", false),
        new Option<bool>("34259", "Box Head", "Mode: [select] only\nShould the bot buy \"Box Head\" ?", false),
        new Option<bool>("34256", "Shy Mog", "Mode: [select] only\nShould the bot buy \"Shy Mog\" ?", false),
        new Option<bool>("34270", "Heavy Celtic Axe", "Mode: [select] only\nShould the bot buy \"Heavy Celtic Axe\" ?", false),
        new Option<bool>("34257", "William Sneevil", "Mode: [select] only\nShould the bot buy \"William Sneevil\" ?", false),
        new Option<bool>("34255", "Moglin Wallace", "Mode: [select] only\nShould the bot buy \"Moglin Wallace\" ?", false),
        new Option<bool>("34268", "Sword of Caledonia", "Mode: [select] only\nShould the bot buy \"Sword of Caledonia\" ?", false),
        new Option<bool>("34261", "Clover Chopper", "Mode: [select] only\nShould the bot buy \"Clover Chopper\" ?", false),
        new Option<bool>("34281", "Shamrockin' Scythe", "Mode: [select] only\nShould the bot buy \"Shamrockin' Scythe\" ?", false),
        new Option<bool>("34287", "Celtic Caster", "Mode: [select] only\nShould the bot buy \"Celtic Caster\" ?", false),
        new Option<bool>("34298", "Celtic Caster Wrap", "Mode: [select] only\nShould the bot buy \"Celtic Caster Wrap\" ?", false),
        new Option<bool>("34294", "Celtic Caster Hood", "Mode: [select] only\nShould the bot buy \"Celtic Caster Hood\" ?", false),
        new Option<bool>("34300", "Lucky Broadsword on your Back", "Mode: [select] only\nShould the bot buy \"Lucky Broadsword on your Back\" ?", false),
        new Option<bool>("34289", "Celtic Destroyer", "Mode: [select] only\nShould the bot buy \"Celtic Destroyer\" ?", false),
        new Option<bool>("34295", "Celtic Destroyer Helm", "Mode: [select] only\nShould the bot buy \"Celtic Destroyer Helm\" ?", false),
        new Option<bool>("34282", "Emerald Intricacy Staff", "Mode: [select] only\nShould the bot buy \"Emerald Intricacy Staff\" ?", false),
        new Option<bool>("34288", "Celtic Cutthroat", "Mode: [select] only\nShould the bot buy \"Celtic Cutthroat\" ?", false),
        new Option<bool>("34293", "Celtic Cutthroat Hood", "Mode: [select] only\nShould the bot buy \"Celtic Cutthroat Hood\" ?", false),
        new Option<bool>("34274", "Double RainBow", "Mode: [select] only\nShould the bot buy \"Double RainBow\" ?", false),
        new Option<bool>("34260", "Claymore of the Celts", "Mode: [select] only\nShould the bot buy \"Claymore of the Celts\" ?", false),
        new Option<bool>("34304", "Lucky Lycaena Wings", "Mode: [select] only\nShould the bot buy \"Lucky Lycaena Wings\" ?", false),
        new Option<bool>("34284", "Kelly's Charm Staff", "Mode: [select] only\nShould the bot buy \"Kelly's Charm Staff\" ?", false),
        new Option<bool>("34269", "Viridian Twist Sword", "Mode: [select] only\nShould the bot buy \"Viridian Twist Sword\" ?", false),
        new Option<bool>("34262", "Fortune Bringer", "Mode: [select] only\nShould the bot buy \"Fortune Bringer\" ?", false),
        new Option<bool>("34265", "Kismet's Edge", "Mode: [select] only\nShould the bot buy \"Kismet's Edge\" ?", false),
        new Option<bool>("34266", "Malachite Cutter", "Mode: [select] only\nShould the bot buy \"Malachite Cutter\" ?", false),
        new Option<bool>("34264", "Fortune's Protector", "Mode: [select] only\nShould the bot buy \"Fortune's Protector\" ?", false),
        new Option<bool>("34301", "Luckee", "Mode: [select] only\nShould the bot buy \"Luckee\" ?", false),
        new Option<bool>("34280", "Golden Clover Cleaver", "Mode: [select] only\nShould the bot buy \"Golden Clover Cleaver\" ?", false),
        new Option<bool>("34283", "Golden Shamrock Staff", "Mode: [select] only\nShould the bot buy \"Golden Shamrock Staff\" ?", false),
        new Option<bool>("34286", "Evolved Leprechaun Armor", "Mode: [select] only\nShould the bot buy \"Evolved Leprechaun Armor\" ?", false),
        new Option<bool>("34306", "Shamrock Cape", "Mode: [select] only\nShould the bot buy \"Shamrock Cape\" ?", false),
        new Option<bool>("34303", "Lucky Gold Cape", "Mode: [select] only\nShould the bot buy \"Lucky Gold Cape\" ?", false),
        new Option<bool>("34299", "Luckiest Lorikeet Feathers", "Mode: [select] only\nShould the bot buy \"Luckiest Lorikeet Feathers\" ?", false),
        new Option<bool>("34272", "Leprechaun's Curse", "Mode: [select] only\nShould the bot buy \"Leprechaun's Curse\" ?", false),
        new Option<bool>("34271", "Lucky Emerald Cutter", "Mode: [select] only\nShould the bot buy \"Lucky Emerald Cutter\" ?", false),
        new Option<bool>("34291", "Scrooge McLuck", "Mode: [select] only\nShould the bot buy \"Scrooge McLuck\" ?", false),
        new Option<bool>("34267", "Sword McLuck", "Mode: [select] only\nShould the bot buy \"Sword McLuck\" ?", false),
        new Option<bool>("34275", "Celtic Paddle of Pain", "Mode: [select] only\nShould the bot buy \"Celtic Paddle of Pain\" ?", false),
        new Option<bool>("34285", "Lucky Charm", "Mode: [select] only\nShould the bot buy \"Lucky Charm\" ?", false),
        new Option<bool>("34307", "Sword McLuck Cape", "Mode: [select] only\nShould the bot buy \"Sword McLuck Cape\" ?", false),
        new Option<bool>("34358", "Scrooge McLuck Helm", "Mode: [select] only\nShould the bot buy \"Scrooge McLuck Helm\" ?", false),
        new Option<bool>("11460", "Evolved Leprechaun", "Mode: [select] only\nShould the bot buy \"Evolved Leprechaun\" ?", false),
        new Option<bool>("11373", "Evolved Leprechaun", "Mode: [select] only\nShould the bot buy \"Evolved Leprechaun\" ?", false),
        new Option<bool>("34290", "Lucky Knight", "Mode: [select] only\nShould the bot buy \"Lucky Knight\" ?", false),
        new Option<bool>("34296", "Lucky Knight's Helm", "Mode: [select] only\nShould the bot buy \"Lucky Knight's Helm\" ?", false),
        new Option<bool>("34297", "Lucky Rainbow Unicorn Hat", "Mode: [select] only\nShould the bot buy \"Lucky Rainbow Unicorn Hat\" ?", false),
        new Option<bool>("34263", "Fortune's Blade", "Mode: [select] only\nShould the bot buy \"Fortune's Blade\" ?", false),
        new Option<bool>("43129", "Noble Leprechaun", "Mode: [select] only\nShould the bot buy \"Noble Leprechaun\" ?", false),
        new Option<bool>("43131", "Leprechaun Lady Helm", "Mode: [select] only\nShould the bot buy \"Leprechaun Lady Helm\" ?", false),
        new Option<bool>("43130", "Leprechaun Lord Helm", "Mode: [select] only\nShould the bot buy \"Leprechaun Lord Helm\" ?", false),
        new Option<bool>("43132", "Noble Leprechaun Wrap", "Mode: [select] only\nShould the bot buy \"Noble Leprechaun Wrap\" ?", false),
        new Option<bool>("43133", "Noble Leprechaun's Cane", "Mode: [select] only\nShould the bot buy \"Noble Leprechaun's Cane\" ?", false),
        new Option<bool>("43134", "Cape of Rainbows", "Mode: [select] only\nShould the bot buy \"Cape of Rainbows\" ?", false),
        new Option<bool>("53763", "Celtic Hunter Staff", "Mode: [select] only\nShould the bot buy \"Celtic Hunter Staff\" ?", false),
        new Option<bool>("53761", "Celtic Hunter Mace", "Mode: [select] only\nShould the bot buy \"Celtic Hunter Mace\" ?", false),
        new Option<bool>("53762", "Celtic Hunter Reavers", "Mode: [select] only\nShould the bot buy \"Celtic Hunter Reavers\" ?", false),
        new Option<bool>("53760", "Celtic Hunter Bow", "Mode: [select] only\nShould the bot buy \"Celtic Hunter Bow\" ?", false),
    };
}
