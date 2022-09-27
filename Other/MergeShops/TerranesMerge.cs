//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class TerranesMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreQOM QOM = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Feather of Purity", "Blade of Semiramis", "Bow of Semiramis", "Daggers of Semiramis", "Staff of Semiramis " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        QOM.TheReshaper(true);
        Adv.StartBuyAllMerge("guardiantree", 1584, findIngredients);

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

                case "Feather of Purity":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6287);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("guardiantree", "Blossoming Treeant", "Treeant Blossom Nectar", 3);
                        Core.HuntMonster("guardiantree", "Myconid", "Myconid Spore", 3);
                        Core.HuntMonster("guardiantree", "Corrupted Zard", "Corrupted Zard", 3);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Bow of Semiramis":
                case "Blade of Semiramis":
                case "Daggers of Semiramis":
                case "Staff of Semiramis":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("guardiantree", "Terrane", req.Name, quant, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("43470", "Feral Hedgehog", "Mode: [select] only\nShould the bot buy \"Feral Hedgehog\" ?", false),
        new Option<bool>("43498", "Dark Druid", "Mode: [select] only\nShould the bot buy \"Dark Druid\" ?", false),
        new Option<bool>("43499", "Dark Fur Wrap", "Mode: [select] only\nShould the bot buy \"Dark Fur Wrap\" ?", false),
        new Option<bool>("43500", "Dark Druid's Staff", "Mode: [select] only\nShould the bot buy \"Dark Druid's Staff\" ?", false),
        new Option<bool>("43501", "Dark Druid's Blade", "Mode: [select] only\nShould the bot buy \"Dark Druid's Blade\" ?", false),
        new Option<bool>("43502", "Dark Druid's Locks", "Mode: [select] only\nShould the bot buy \"Dark Druid's Locks\" ?", false),
        new Option<bool>("43503", "Dark Druid's Horned Locks", "Mode: [select] only\nShould the bot buy \"Dark Druid's Horned Locks\" ?", false),
        new Option<bool>("43504", "Dark Druid's Hood", "Mode: [select] only\nShould the bot buy \"Dark Druid's Hood\" ?", false),
        new Option<bool>("43505", "Dark Hood + Horns", "Mode: [select] only\nShould the bot buy \"Dark Hood + Horns\" ?", false),
        new Option<bool>("43472", "Purified BeastMaster", "Mode: [select] only\nShould the bot buy \"Purified BeastMaster\" ?", false),
        new Option<bool>("43473", "Purified BeastMaster Horns", "Mode: [select] only\nShould the bot buy \"Purified BeastMaster Horns\" ?", false),
        new Option<bool>("43474", "Purified BeastMaster Locks", "Mode: [select] only\nShould the bot buy \"Purified BeastMaster Locks\" ?", false),
        new Option<bool>("43490", "Wings of Semiramis", "Mode: [select] only\nShould the bot buy \"Wings of Semiramis\" ?", false),
        new Option<bool>("43489", "Semiramis Cape", "Mode: [select] only\nShould the bot buy \"Semiramis Cape\" ?", false),
        new Option<bool>("43484", "Enchanted BeastMaster Blade", "Mode: [select] only\nShould the bot buy \"Enchanted BeastMaster Blade\" ?", false),
        new Option<bool>("43476", "Enchanted BeastMaster Bow", "Mode: [select] only\nShould the bot buy \"Enchanted BeastMaster Bow\" ?", false),
        new Option<bool>("43479", "Enchanted BeastMaster Daggers", "Mode: [select] only\nShould the bot buy \"Enchanted BeastMaster Daggers\" ?", false),
        new Option<bool>("43481", "Enchanted BeastMaster Staff", "Mode: [select] only\nShould the bot buy \"Enchanted BeastMaster Staff\" ?", false),
    };
}
