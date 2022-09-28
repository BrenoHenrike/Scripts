//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SynderesMerge
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
        Core.BankingBlackList.AddRange(new[] { "Synderes' Souvenir "});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("enemyforest", 332, findIngredients);

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

                case "Synderes' Souvenir":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(4247);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Synderes Souvenirs Shop 4247
                        Core.HuntMonster("enemyforest", "Evil Elemental", "Forest Denizen Slain", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("29574", "Synderes' Cape of Doom", "Mode: [select] only\nShould the bot buy \"Synderes' Cape of Doom\" ?", false),
        new Option<bool>("29576", "NO BOTS Armor", "Mode: [select] only\nShould the bot buy \"NO BOTS Armor\" ?", false),
        new Option<bool>("29711", "Synderes Groupie Armor", "Mode: [select] only\nShould the bot buy \"Synderes Groupie Armor\" ?", false),
        new Option<bool>("29753", "Sock Monkey Cape", "Mode: [select] only\nShould the bot buy \"Sock Monkey Cape\" ?", false),
        new Option<bool>("29754", "Synderes Acoustic Guitar", "Mode: [select] only\nShould the bot buy \"Synderes Acoustic Guitar\" ?", false),
        new Option<bool>("29755", "Synderes Electric", "Mode: [select] only\nShould the bot buy \"Synderes Electric\" ?", false),
        new Option<bool>("29756", "Expendable Rogue Mask", "Mode: [select] only\nShould the bot buy \"Expendable Rogue Mask\" ?", false),
        new Option<bool>("29757", "Expendable Rogue", "Mode: [select] only\nShould the bot buy \"Expendable Rogue\" ?", false),
        new Option<bool>("29758", "Synderes Minion Mask and Locks", "Mode: [select] only\nShould the bot buy \"Synderes Minion Mask and Locks\" ?", false),
        new Option<bool>("29759", "Synderes Minion Mask", "Mode: [select] only\nShould the bot buy \"Synderes Minion Mask\" ?", false),
    };
}
