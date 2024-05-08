/*
name: Yokai Portal Merge
description: This bot will farm the items belonging to the selected mode for the Yokai Portal Merge [2429] in /yokaiportal
tags: yokai, portal, merge, yokaiportal, amethyst, noble, plate, earring, war, march, shikigami, katanas, katana, sheath, jade, dragonguard, ruby, kitsune, duelist, banner
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/DragonsOfYokai/CoreDOY.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class YokaiPortalMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreDOY DOY = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Shikigami String", "Zakhvatchik's Sapphire", "Noble Amethyst Katana", "Admiral Zheng's Jade", "Kitsune's Ruby" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        DOY.YokaiPortal();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("yokaiportal", 2429, findIngredients, buyOnlyThis, buyMode: buyMode);

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
                    bool shouldStop = !Adv.matsOnly || !dontStopMissingIng;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                case "Shikigami String":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9677);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("yokaiportal", "Kitsune Spirits", "Kitsune Spirit Incense", 15, log: false);
                        Core.HuntMonster("yokaiportal", "Puppeted Dragonling", "Draconic Red String", 15, log: false);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("yokaiportal", "Kitsune Kukol'nyy", "Lord Kitsune's Red String", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Zakhvatchik's Sapphire":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("hakuwar", "Zakhvatchik", req.Name, quant, false, false);
                    break;

                case "Noble Amethyst Katana":
                case "Kitsune's Ruby":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("yokaiportal", "Kitsune Kukol'nyy", req.Name, quant, false, false);
                    break;

                case "Admiral Zheng's Jade":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("yokaitreasure", "Admiral Zheng", req.Name, quant, false, false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("84198", "Yokai Amethyst Noble", "Mode: [select] only\nShould the bot buy \"Yokai Amethyst Noble\" ?", false),
        new Option<bool>("84199", "Yokai Amethyst Noble Plate", "Mode: [select] only\nShould the bot buy \"Yokai Amethyst Noble Plate\" ?", false),
        new Option<bool>("84200", "Yokai Amethyst Noble Hair", "Mode: [select] only\nShould the bot buy \"Yokai Amethyst Noble Hair\" ?", false),
        new Option<bool>("84201", "Yokai Amethyst Noble Earring", "Mode: [select] only\nShould the bot buy \"Yokai Amethyst Noble Earring\" ?", false),
        new Option<bool>("84202", "Yokai Amethyst Noble Helm", "Mode: [select] only\nShould the bot buy \"Yokai Amethyst Noble Helm\" ?", false),
        new Option<bool>("84203", "Yokai Amethyst Noble Locks", "Mode: [select] only\nShould the bot buy \"Yokai Amethyst Noble Locks\" ?", false),
        new Option<bool>("84204", "Yokai Amethyst Noble Earring and Locks", "Mode: [select] only\nShould the bot buy \"Yokai Amethyst Noble Earring and Locks\" ?", false),
        new Option<bool>("84206", "Yokai Amethyst Noble War March", "Mode: [select] only\nShould the bot buy \"Yokai Amethyst Noble War March\" ?", false),
        new Option<bool>("84207", "Noble Amethyst Shikigami", "Mode: [select] only\nShould the bot buy \"Noble Amethyst Shikigami\" ?", false),
        new Option<bool>("84210", "Noble Amethyst Katanas", "Mode: [select] only\nShould the bot buy \"Noble Amethyst Katanas\" ?", false),
        new Option<bool>("84213", "Noble Amethyst Katana and Sheath", "Mode: [select] only\nShould the bot buy \"Noble Amethyst Katana and Sheath\" ?", false),
        new Option<bool>("85325", "Jade Yokai Dragonguard", "Mode: [select] only\nShould the bot buy \"Jade Yokai Dragonguard\" ?", false),
        new Option<bool>("85326", "Jade Yokai Dragonguard Plate", "Mode: [select] only\nShould the bot buy \"Jade Yokai Dragonguard Plate\" ?", false),
        new Option<bool>("85327", "Jade Yokai Dragonguard Hair", "Mode: [select] only\nShould the bot buy \"Jade Yokai Dragonguard Hair\" ?", false),
        new Option<bool>("85328", "Jade Yokai Dragonguard Earring", "Mode: [select] only\nShould the bot buy \"Jade Yokai Dragonguard Earring\" ?", false),
        new Option<bool>("85329", "Jade Yokai Dragonguard Helm", "Mode: [select] only\nShould the bot buy \"Jade Yokai Dragonguard Helm\" ?", false),
        new Option<bool>("85330", "Jade Yokai Dragonguard Locks", "Mode: [select] only\nShould the bot buy \"Jade Yokai Dragonguard Locks\" ?", false),
        new Option<bool>("85331", "Jade Yokai Dragonguard Earring and Locks", "Mode: [select] only\nShould the bot buy \"Jade Yokai Dragonguard Earring and Locks\" ?", false),
        new Option<bool>("85333", "Jade Yokai Dragonguard War March", "Mode: [select] only\nShould the bot buy \"Jade Yokai Dragonguard War March\" ?", false),
        new Option<bool>("85334", "Jade Yokai Shikigami", "Mode: [select] only\nShould the bot buy \"Jade Yokai Shikigami\" ?", false),
        new Option<bool>("85336", "Jade Dragonguard Katana", "Mode: [select] only\nShould the bot buy \"Jade Dragonguard Katana\" ?", false),
        new Option<bool>("85337", "Jade Dragonguard Katanas", "Mode: [select] only\nShould the bot buy \"Jade Dragonguard Katanas\" ?", false),
        new Option<bool>("85340", "Jade Dragonguard Katana and Sheath", "Mode: [select] only\nShould the bot buy \"Jade Dragonguard Katana and Sheath\" ?", false),
        new Option<bool>("85419", "Ruby Kitsune Duelist", "Mode: [select] only\nShould the bot buy \"Ruby Kitsune Duelist\" ?", false),
        new Option<bool>("85420", "Ruby Kitsune Duelist Plate", "Mode: [select] only\nShould the bot buy \"Ruby Kitsune Duelist Plate\" ?", false),
        new Option<bool>("85421", "Ruby Kitsune Duelist Hair", "Mode: [select] only\nShould the bot buy \"Ruby Kitsune Duelist Hair\" ?", false),
        new Option<bool>("85422", "Ruby Kitsune Duelist Earring", "Mode: [select] only\nShould the bot buy \"Ruby Kitsune Duelist Earring\" ?", false),
        new Option<bool>("85423", "Ruby Kitsune Duelist Helm", "Mode: [select] only\nShould the bot buy \"Ruby Kitsune Duelist Helm\" ?", false),
        new Option<bool>("85424", "Ruby Kitsune Duelist Locks", "Mode: [select] only\nShould the bot buy \"Ruby Kitsune Duelist Locks\" ?", false),
        new Option<bool>("85425", "Ruby Kitsune Duelist Earring and Locks", "Mode: [select] only\nShould the bot buy \"Ruby Kitsune Duelist Earring and Locks\" ?", false),
        new Option<bool>("85426", "Ruby Kitsune Duelist Banner", "Mode: [select] only\nShould the bot buy \"Ruby Kitsune Duelist Banner\" ?", false),
        new Option<bool>("85427", "Ruby Kitsune Duelist War March", "Mode: [select] only\nShould the bot buy \"Ruby Kitsune Duelist War March\" ?", false),
        new Option<bool>("85428", "Ruby Yokai Shikigami", "Mode: [select] only\nShould the bot buy \"Ruby Yokai Shikigami\" ?", false),
        new Option<bool>("85430", "Ruby Kitsune Katana", "Mode: [select] only\nShould the bot buy \"Ruby Kitsune Katana\" ?", false),
        new Option<bool>("85431", "Ruby Kitsune Katanas", "Mode: [select] only\nShould the bot buy \"Ruby Kitsune Katanas\" ?", false),
        new Option<bool>("85434", "Ruby Kitsune Katana and Sheath", "Mode: [select] only\nShould the bot buy \"Ruby Kitsune Katana and Sheath\" ?", false),
    };
}
