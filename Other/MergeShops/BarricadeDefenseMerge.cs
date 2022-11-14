//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BarricadeDefenseMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreQOM QOM => new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Rift Defense Medal "});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        QOM.TheDestroyer();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("greenguardeast", 1401, findIngredients);

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

                case "Rift Defense Medal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(5825);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("charredpath", "Infected Hare", "Invader Slain", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("39202", "RiftBreaker", "Mode: [select] only\nShould the bot buy \"RiftBreaker\" ?", false),
        new Option<bool>("39203", "Riftbreaker Helm", "Mode: [select] only\nShould the bot buy \"Riftbreaker Helm\" ?", false),
        new Option<bool>("39204", "Riftbreaker Horns", "Mode: [select] only\nShould the bot buy \"Riftbreaker Horns\" ?", false),
        new Option<bool>("39209", "Riftbreaker Sword", "Mode: [select] only\nShould the bot buy \"Riftbreaker Sword\" ?", false),
        new Option<bool>("39210", "Riftbreaker Cutlass", "Mode: [select] only\nShould the bot buy \"Riftbreaker Cutlass\" ?", false),
        new Option<bool>("39205", "Riftbreaker Wings", "Mode: [select] only\nShould the bot buy \"Riftbreaker Wings\" ?", false),
        new Option<bool>("39206", "Riftbreaker Cape", "Mode: [select] only\nShould the bot buy \"Riftbreaker Cape\" ?", false),
    };
}
