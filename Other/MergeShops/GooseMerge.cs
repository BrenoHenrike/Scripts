//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class GooseMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Cysero's Cookie "});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("goose", 58, findIngredients);

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

                case "Cysero's Cookie":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("goose", "Queen's Sage", "Cysero's Cookie");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("30448", "Queen's Sage", "Mode: [select] only\nShould the bot buy \"Queen's Sage\" ?", false),
        new Option<bool>("30449", "Queen's Sage Hood", "Mode: [select] only\nShould the bot buy \"Queen's Sage Hood\" ?", false),
        new Option<bool>("30450", "Queen's Sage Cape", "Mode: [select] only\nShould the bot buy \"Queen's Sage Cape\" ?", false),
        new Option<bool>("30451", "Queen's Sage Scythe", "Mode: [select] only\nShould the bot buy \"Queen's Sage Scythe\" ?", false),
        new Option<bool>("30422", "GIANT Mountain of Socks", "Mode: [select] only\nShould the bot buy \"GIANT Mountain of Socks\" ?", false),
        new Option<bool>("30421", "Cyser-Duck Painting", "Mode: [select] only\nShould the bot buy \"Cyser-Duck Painting\" ?", false),
        new Option<bool>("30404", "Bucket of Paint Helm", "Mode: [select] only\nShould the bot buy \"Bucket of Paint Helm\" ?", false),
        new Option<bool>("30418", "Sock Ape", "Mode: [select] only\nShould the bot buy \"Sock Ape\" ?", false),
        new Option<bool>("30419", "Chris P. Bacon", "Mode: [select] only\nShould the bot buy \"Chris P. Bacon\" ?", false),
        new Option<bool>("30420", "Grandhonk Goose the Gray", "Mode: [select] only\nShould the bot buy \"Grandhonk Goose the Gray\" ?", false),
    };
}
