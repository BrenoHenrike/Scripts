//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DeathPitArenaRepMerge
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
        Core.BankingBlackList.AddRange(new[] { "Death Pit Arena Medal", "General Gall Medal", "General Velm Medal", "General Hun'Gar Medal", "General Chud Medal "});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("deathpit", 1261, findIngredients);

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

                case "Death Pit Arena Medal":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("deathpit", "Training Dummy", req.Name, quant, false);
                    break;

                case "General Gall Medal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(5147);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Battle: You vs General Gall! 5147
                        Core.HuntMonster("deathpit", "General Gall", "General Gall Defeated");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "General Velm Medal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(5149);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //General Velm 5149
                        Core.HuntMonster("deathpit", "General Velm", "General Velm Defeated");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "General Hun'Gar Medal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(5155);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Do You Even Brawl 5155
                        Core.HuntMonster("deathpit", "Velm's Restorer|Velm's Brawler", "Death Pit Token");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "General Chud Medal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(5151);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //General Chud 5151
                        Core.HuntMonster("deathpit", "General Chud", "General Chud Defeated");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("35269", "General Gall's Armor", "Mode: [select] only\nShould the bot buy \"General Gall's Armor\" ?", false),
        new Option<bool>("35270", "General Velm's Armor", "Mode: [select] only\nShould the bot buy \"General Velm's Armor\" ?", false),
        new Option<bool>("35272", "General Hun'Gar's Armor", "Mode: [select] only\nShould the bot buy \"General Hun'Gar's Armor\" ?", false),
        new Option<bool>("35274", "General Chud's Armor", "Mode: [select] only\nShould the bot buy \"General Chud's Armor\" ?", false),
        new Option<bool>("35271", "General Velm's Helm", "Mode: [select] only\nShould the bot buy \"General Velm's Helm\" ?", false),
        new Option<bool>("35273", "General Hun'Gar's Helm", "Mode: [select] only\nShould the bot buy \"General Hun'Gar's Helm\" ?", false),
        new Option<bool>("35275", "General Chud's Helm", "Mode: [select] only\nShould the bot buy \"General Chud's Helm\" ?", false),
        new Option<bool>("35350", "Spinal Carnage Staff", "Mode: [select] only\nShould the bot buy \"Spinal Carnage Staff\" ?", false),
        new Option<bool>("35355", "Sword of Malice", "Mode: [select] only\nShould the bot buy \"Sword of Malice\" ?", false),
        new Option<bool>("35419", "Drakel Chud Armor", "Mode: [select] only\nShould the bot buy \"Drakel Chud Armor\" ?", false),
        new Option<bool>("35417", "Drakel Velm Armor", "Mode: [select] only\nShould the bot buy \"Drakel Velm Armor\" ?", false),
        new Option<bool>("35418", "Drakel Hun'Gar Armor", "Mode: [select] only\nShould the bot buy \"Drakel Hun'Gar Armor\" ?", false),
        new Option<bool>("35416", "Drakel Gall Armor (Green)", "Mode: [select] only\nShould the bot buy \"Drakel Gall Armor (Green)\" ?", false),
        new Option<bool>("35421", "Drakel Velm Helmet", "Mode: [select] only\nShould the bot buy \"Drakel Velm Helmet\" ?", false),
        new Option<bool>("35422", "Drakel Green Morph", "Mode: [select] only\nShould the bot buy \"Drakel Green Morph\" ?", false),
        new Option<bool>("35423", "Drakel Gall Armor", "Mode: [select] only\nShould the bot buy \"Drakel Gall Armor\" ?", false),
        new Option<bool>("35424", "Drakel Gall Morph", "Mode: [select] only\nShould the bot buy \"Drakel Gall Morph\" ?", false),
        new Option<bool>("34946", "Drakel Chud Helm", "Mode: [select] only\nShould the bot buy \"Drakel Chud Helm\" ?", false),
    };
}
