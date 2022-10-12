//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ChromaCandyMerge
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
        Core.BankingBlackList.AddRange(new[] { "Orange Dye", "Green Dye", "Yellow Dye", "Black Dye", "Purple Dye", "Pink Dye "});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        if (!Core.isSeasonalMapActive("mogloween"))
            return;
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("chromafection", 1622, findIngredients);

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

                case "Orange Dye":
                case "Green Dye":
                case "Yellow Dye":
                case "Black Dye":
                case "Purple Dye":
                case "Pink Dye":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(6538);
                        Core.HuntMonster("chromafection", "Chromafection", "Candy Dye", 3);
                        Core.EnsureComplete(6538, req.ID);
                    }
                    break;  

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("45197", "Pumpking Overlord", "Mode: [select] only\nShould the bot buy \"Pumpking Overlord\" ?", false),
        new Option<bool>("45198", "Possessed Pumpkin Helm", "Mode: [select] only\nShould the bot buy \"Possessed Pumpkin Helm\" ?", false),
        new Option<bool>("45199", "Hungry Pumpking Helm", "Mode: [select] only\nShould the bot buy \"Hungry Pumpking Helm\" ?", false),
        new Option<bool>("45200", "Blade of the Pumpking", "Mode: [select] only\nShould the bot buy \"Blade of the Pumpking\" ?", false),
        new Option<bool>("45201", "Daggers of the Pumpking", "Mode: [select] only\nShould the bot buy \"Daggers of the Pumpking\" ?", false),
        new Option<bool>("45202", "A Gourdly Spirit", "Mode: [select] only\nShould the bot buy \"A Gourdly Spirit\" ?", false),
        new Option<bool>("45203", "Pumpkin Moglin", "Mode: [select] only\nShould the bot buy \"Pumpkin Moglin\" ?", false),
        new Option<bool>("45204", "Ghostly Pumpkin Spirit", "Mode: [select] only\nShould the bot buy \"Ghostly Pumpkin Spirit\" ?", false),
        new Option<bool>("45246", "Wacky Top Hat", "Mode: [select] only\nShould the bot buy \"Wacky Top Hat\" ?", false),
        new Option<bool>("45247", "Wacky Top Hat + Locks", "Mode: [select] only\nShould the bot buy \"Wacky Top Hat + Locks\" ?", false),
    };
}
