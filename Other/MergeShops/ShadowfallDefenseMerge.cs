//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/ShadowGates.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShadowfallDefenseMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public ShadowGates ShadowGates = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Dark Heart Medal " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("shadowgates", 803, findIngredients);

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

                case "Dark Heart Medal":
                    ShadowGates.StoryLine();
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(3294);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Gravelyn's Dark Rewards 3294
                        Core.HuntMonster("Shadowfall", "Skeletal Knight", "Infected Skull", 7);
                        Core.HuntMonster("ShadowGates", "Chaos Warrior", "Chaorrupted Bones", 6);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("22172", "Tentacular Locks", "Mode: [select] only\nShould the bot buy \"Tentacular Locks\" ?", false),
        new Option<bool>("22171", "Tentacular Buzzcut", "Mode: [select] only\nShould the bot buy \"Tentacular Buzzcut\" ?", false),
        new Option<bool>("22169", "Chaos Gem Staff", "Mode: [select] only\nShould the bot buy \"Chaos Gem Staff\" ?", false),
        new Option<bool>("22168", "Runes, Interrupted", "Mode: [select] only\nShould the bot buy \"Runes, Interrupted\" ?", false),
        new Option<bool>("22167", "Tentacular Crown", "Mode: [select] only\nShould the bot buy \"Tentacular Crown\" ?", false),
        new Option<bool>("22166", "Chaorrupted Arcana Mage", "Mode: [select] only\nShould the bot buy \"Chaorrupted Arcana Mage\" ?", false),
        new Option<bool>("22162", "Chaorrupted Basher", "Mode: [select] only\nShould the bot buy \"Chaorrupted Basher\" ?", false),
        new Option<bool>("22161", "Broken Horn Basher", "Mode: [select] only\nShould the bot buy \"Broken Horn Basher\" ?", false),
        new Option<bool>("22103", "Burning Shadow Horns", "Mode: [select] only\nShould the bot buy \"Burning Shadow Horns\" ?", false),
        new Option<bool>("22102", "Blade of Rage and Fury", "Mode: [select] only\nShould the bot buy \"Blade of Rage and Fury\" ?", false),
        new Option<bool>("22101", "Cloak of Shadows", "Mode: [select] only\nShould the bot buy \"Cloak of Shadows\" ?", false),
        new Option<bool>("22100", "Knight of Shadows", "Mode: [select] only\nShould the bot buy \"Knight of Shadows\" ?", false),
    };
}
