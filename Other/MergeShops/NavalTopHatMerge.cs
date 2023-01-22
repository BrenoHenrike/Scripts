/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class NavalTopHatMerge
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
        Core.BankingBlackList.AddRange(new[] { "Facial Hair", "Blue Skull", "Top Hat", "Stardust", "Gears", "Pink Cloth", "Zombie Flesh", "Red Cloth", "Nugget of Platinum", "Shard of Ice", "Chaos Eye", "Breath of Flame", "Scrap of Cloth", "Toxic Gas Mask " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("pirates", 723, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Facial Hair":
                case "Blue Skull":
                case "Stardust":
                case "Gears":
                case "Pink Cloth":
                case "Zombie Flesh":
                case "Red Cloth":
                case "Nugget of Platinum":
                case "Shard of Ice":
                case "Chaos Eye":
                case "Breath of Flame":
                case "Toxic Gas Mask":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.BuyItem("Pirates", 724, req.Name, quant);
                    break;

                case "Top Hat":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("Pirates", "Fishman Soldier", req.Name, quant, isTemp: false);
                    break;

                case "Scrap of Cloth":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("tlapd", "Skellkie|Bedrock Lobster|Blind Eel|Giant Crabble", req.Name, quant, isTemp: false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("25673", "Bearded Icy Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Bearded Icy Naval Top Hat\" ?", false),
        new Option<bool>("25674", "Bearded Galactic Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Bearded Galactic Naval Top Hat\" ?", false),
        new Option<bool>("25675", "Bearded Platinum Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Bearded Platinum Naval Top Hat\" ?", false),
        new Option<bool>("25677", "Bearded Red Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Bearded Red Naval Top Hat\" ?", false),
        new Option<bool>("25678", "Bearded Rotting Top Naval Hat", "Mode: [select] only\nShould the bot buy \"Bearded Rotting Top Naval Hat\" ?", false),
        new Option<bool>("25682", "Sir Legion Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Sir Legion Naval Top Hat\" ?", false),
        new Option<bool>("25683", "Miss Legion Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Miss Legion Naval Top Hat\" ?", false),
        new Option<bool>("25684", "Miss Galactic Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Miss Galactic Naval Top Hat\" ?", false),
        new Option<bool>("25686", "ChronoLady Naval Top Hat", "Mode: [select] only\nShould the bot buy \"ChronoLady Naval Top Hat\" ?", false),
        new Option<bool>("25688", "Missy Scallywag Top Hat", "Mode: [select] only\nShould the bot buy \"Missy Scallywag Top Hat\" ?", false),
        new Option<bool>("25689", "Lady Rotting Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Lady Rotting Naval Top Hat\" ?", false),
        new Option<bool>("25690", "Miss Red Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Miss Red Naval Top Hat\" ?", false),
        new Option<bool>("25691", "Mrs. Platinum Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Mrs. Platinum Naval Top Hat\" ?", false),
        new Option<bool>("25692", "Lassy Icy Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Lassy Icy Naval Top Hat\" ?", false),
        new Option<bool>("25693", "Cutie Chaos Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Cutie Chaos Naval Top Hat\" ?", false),
        new Option<bool>("25694", "Femme Blazing Naval Top Hat Locks", "Mode: [select] only\nShould the bot buy \"Femme Blazing Naval Top Hat Locks\" ?", false),
        new Option<bool>("25695", "Male Rotting Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Male Rotting Naval Top Hat\" ?", false),
        new Option<bool>("25696", "Mr. Red Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Mr. Red Naval Top Hat\" ?", false),
        new Option<bool>("25697", "Sir Platinum Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Sir Platinum Naval Top Hat\" ?", false),
        new Option<bool>("25669", "Male Galactic Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Male Galactic Naval Top Hat\" ?", false),
        new Option<bool>("26083", "Sir Icy Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Sir Icy Naval Top Hat\" ?", false),
        new Option<bool>("25625", "Brilliant Naval Tophat Locks", "Mode: [select] only\nShould the bot buy \"Brilliant Naval Tophat Locks\" ?", false),
        new Option<bool>("25624", "Brilliant Naval Tophat", "Mode: [select] only\nShould the bot buy \"Brilliant Naval Tophat\" ?", false),
        new Option<bool>("25627", "Doom Lass Tophat", "Mode: [select] only\nShould the bot buy \"Doom Lass Tophat\" ?", false),
        new Option<bool>("25628", "Explorer Naval Tophat", "Mode: [select] only\nShould the bot buy \"Explorer Naval Tophat\" ?", false),
        new Option<bool>("25629", "Explorer Naval Tophat Locks", "Mode: [select] only\nShould the bot buy \"Explorer Naval Tophat Locks\" ?", false),
        new Option<bool>("25630", "Void Naval Tophat", "Mode: [select] only\nShould the bot buy \"Void Naval Tophat\" ?", false),
        new Option<bool>("25631", "Void Lass Tophat", "Mode: [select] only\nShould the bot buy \"Void Lass Tophat\" ?", false),
        new Option<bool>("26084", "Icy Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Icy Naval Top Hat\" ?", false),
        new Option<bool>("25626", "Doom Top Hat", "Mode: [select] only\nShould the bot buy \"Doom Top Hat\" ?", false),
        new Option<bool>("56320", "Classy Naval Bearded Tophat", "Mode: [select] only\nShould the bot buy \"Classy Naval Bearded Tophat\" ?", false),
        new Option<bool>("56321", "Classy Naval Tophat + Locks", "Mode: [select] only\nShould the bot buy \"Classy Naval Tophat + Locks\" ?", false),
        new Option<bool>("56324", "Classy Naval Tophat", "Mode: [select] only\nShould the bot buy \"Classy Naval Tophat\" ?", false),
        new Option<bool>("56020", "Chaotic Naval Top Hat + Locks", "Mode: [select] only\nShould the bot buy \"Chaotic Naval Top Hat + Locks\" ?", false),
        new Option<bool>("56021", "Chaotic Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Chaotic Naval Top Hat\" ?", false),
        new Option<bool>("56536", "Toxic Naval Commander's Top Hat", "Mode: [select] only\nShould the bot buy \"Toxic Naval Commander's Top Hat\" ?", false),
        new Option<bool>("56391", "Bearded Toxic Top Hat", "Mode: [select] only\nShould the bot buy \"Bearded Toxic Top Hat\" ?", false),
        new Option<bool>("56394", "Toxic Top Hat + Mask", "Mode: [select] only\nShould the bot buy \"Toxic Top Hat + Mask\" ?", false),
        new Option<bool>("56393", "Toxic Naval Commander's Top Hat + Locks", "Mode: [select] only\nShould the bot buy \"Toxic Naval Commander's Top Hat + Locks\" ?", false),
        new Option<bool>("56395", "Masked Toxic Top Hat + Locks", "Mode: [select] only\nShould the bot buy \"Masked Toxic Top Hat + Locks\" ?", false),
        new Option<bool>("56643", "Grim Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Grim Naval Top Hat\" ?", false),
        new Option<bool>("56645", "Grim Naval Top Hat + Locks", "Mode: [select] only\nShould the bot buy \"Grim Naval Top Hat + Locks\" ?", false),
        new Option<bool>("56644", "Grim Naval Commander's Top Hat", "Mode: [select] only\nShould the bot buy \"Grim Naval Commander's Top Hat\" ?", false),
    };
}
