/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Yokai.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DragonSoulShinobiMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public YokaiQuests Yokai = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("shadowfortress", 1968, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Dragon Shinobi Token":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Yokai.Quests();
                    Core.RegisterQuests(7924);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("shadowfortress", "1st Head Of Orochi", "Perfect Orochi Scales", 10, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("59476", "DragonSoul Shinobi", "Mode: [select] only\nShould the bot buy \"DragonSoul Shinobi\" ?", false),
        new Option<bool>("59465", "DragonSoul Shinobi", "Mode: [select] only\nShould the bot buy \"DragonSoul Shinobi\" ?", false),
        new Option<bool>("59466", "DragonSoul Kabuto + DragonTail", "Mode: [select] only\nShould the bot buy \"DragonSoul Kabuto + DragonTail\" ?", false),
        new Option<bool>("59467", "DragonSoul Kabuto", "Mode: [select] only\nShould the bot buy \"DragonSoul Kabuto\" ?", false),
        new Option<bool>("59468", "DragonSoul Kunoichi Kabuto + DragonTail", "Mode: [select] only\nShould the bot buy \"DragonSoul Kunoichi Kabuto + DragonTail\" ?", false),
        new Option<bool>("59469", "DragonSoul Kunoichi Kabuto", "Mode: [select] only\nShould the bot buy \"DragonSoul Kunoichi Kabuto\" ?", false),
        new Option<bool>("59470", "Shinobi's Dragon Soul", "Mode: [select] only\nShould the bot buy \"Shinobi's Dragon Soul\" ?", false),
        new Option<bool>("59475", "DragonSoul Shinobi Back Katana", "Mode: [select] only\nShould the bot buy \"DragonSoul Shinobi Back Katana\" ?", false),
        new Option<bool>("59471", "DragonSealed Katana", "Mode: [select] only\nShould the bot buy \"DragonSealed Katana\" ?", false),
        new Option<bool>("59472", "DragonSoul Shinobi Katana", "Mode: [select] only\nShould the bot buy \"DragonSoul Shinobi Katana\" ?", false),
        new Option<bool>("59473", "DragonSoul Shinobi Katana + Sheath", "Mode: [select] only\nShould the bot buy \"DragonSoul Shinobi Katana + Sheath\" ?", false),
        new Option<bool>("59474", "DragonSoul Shinobi Sheathed Katana", "Mode: [select] only\nShould the bot buy \"DragonSoul Shinobi Sheathed Katana\" ?", false),
        new Option<bool>("59477", "Dual DragonSoul Shinobi Kunai", "Mode: [select] only\nShould the bot buy \"Dual DragonSoul Shinobi Kunai\" ?", false),
        new Option<bool>("59478", "DragonSoul Shinobi Kunai", "Mode: [select] only\nShould the bot buy \"DragonSoul Shinobi Kunai\" ?", false),
        new Option<bool>("59479", "Dual DragonSoul Reversed Kunai", "Mode: [select] only\nShould the bot buy \"Dual DragonSoul Reversed Kunai\" ?", false),
        new Option<bool>("59480", "DragonSoul Reversed Kunai", "Mode: [select] only\nShould the bot buy \"DragonSoul Reversed Kunai\" ?", false),
        new Option<bool>("59481", "DragonSoul Kama", "Mode: [select] only\nShould the bot buy \"DragonSoul Kama\" ?", false),
        new Option<bool>("59482", "DragonSoul Grand Kama", "Mode: [select] only\nShould the bot buy \"DragonSoul Grand Kama\" ?", false),
    };
}
