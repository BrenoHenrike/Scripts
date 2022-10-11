//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class MogGLOWweenMerge
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
        Core.BankingBlackList.AddRange(new[] { "Glowball " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        if (!Core.isSeasonalMapActive("mogloween"))
            return;
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("franken", 770, findIngredients);

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

                case "Glowball":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    //Survive Frankenwerepire! 3163
                    Core.RegisterQuests(3163);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("franken", "Frankenwerepire", "Defeat Frankenwerepire");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("20753", "Green Glowchucks", "Mode: [select] only\nShould the bot buy \"Green Glowchucks\" ?", false),
        new Option<bool>("20756", "Blue Glowchucks", "Mode: [select] only\nShould the bot buy \"Blue Glowchucks\" ?", false),
        new Option<bool>("20758", "Pink Glowchucks", "Mode: [select] only\nShould the bot buy \"Pink Glowchucks\" ?", false),
        new Option<bool>("20759", "Orange Glowchucks", "Mode: [select] only\nShould the bot buy \"Orange Glowchucks\" ?", false),
        new Option<bool>("36677", "Glow Dreads", "Mode: [select] only\nShould the bot buy \"Glow Dreads\" ?", false),
        new Option<bool>("36676", "Glow Falls", "Mode: [select] only\nShould the bot buy \"Glow Falls\" ?", false),
        new Option<bool>("36675", "Glow Smasher", "Mode: [select] only\nShould the bot buy \"Glow Smasher\" ?", false),
        new Option<bool>("36674", "Blade of Cursed Glow", "Mode: [select] only\nShould the bot buy \"Blade of Cursed Glow\" ?", false),
        new Option<bool>("36673", "Glow Flail", "Mode: [select] only\nShould the bot buy \"Glow Flail\" ?", false),
        new Option<bool>("36672", "Blacklight Bone Club", "Mode: [select] only\nShould the bot buy \"Blacklight Bone Club\" ?", false),
    };
}
