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

public class TrickstersGiftsMerge
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
        Core.BankingBlackList.AddRange(new[] { "Poeira do Saci", "Ossos do Corpo-Seco", "Escamas da Cuca", "Pink Gem of the Sea", "Cuca's Dye " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("mythperception", 1910, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Poeira do Saci":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(7682);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("mythperception", "Saci", "Trapped Saci");
                    Core.CancelRegisteredQuests();
                    break;

                case "Ossos do Corpo-Seco":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7683);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("mythperception", "Corpo-Seco", "Corpo-Seco's Nails", 5);
                    Bot.Wait.ForPickup(req.Name);
                    Core.CancelRegisteredQuests();
                    break;

                case "Escamas da Cuca":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7684);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("mythperception", "Cuca", "Cuca's Hat", quant);
                    Bot.Wait.ForPickup(req.Name);
                    Core.CancelRegisteredQuests();
                    break;

                case "Pink Gem of the Sea":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("mythperception", "Boto", req.Name, quant);
                    break;

                case "Cuca's Dye":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("mythperception", "Cuca", req.Name, quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("56095", "Arara Azul on your Shoulder", "Mode: [select] only\nShould the bot buy \"Arara Azul on your Shoulder\" ?", false),
        new Option<bool>("56096", "Picapau Amarelo", "Mode: [select] only\nShould the bot buy \"Picapau Amarelo\" ?", false),
        new Option<bool>("56100", "Emiglin", "Mode: [select] only\nShould the bot buy \"Emiglin\" ?", false),
        new Option<bool>("56204", "Cuca's Apprentice", "Mode: [select] only\nShould the bot buy \"Cuca's Apprentice\" ?", false),
        new Option<bool>("56211", "Shadow Cuca", "Mode: [select] only\nShould the bot buy \"Shadow Cuca\" ?", false),
        new Option<bool>("56212", "Prismatic Shadow Cuca", "Mode: [select] only\nShould the bot buy \"Prismatic Shadow Cuca\" ?", false),
        new Option<bool>("56213", "Cuca's Apprentice Staff", "Mode: [select] only\nShould the bot buy \"Cuca's Apprentice Staff\" ?", false),
        new Option<bool>("56227", "Prismatic Cuca's Apprentice Staff", "Mode: [select] only\nShould the bot buy \"Prismatic Cuca's Apprentice Staff\" ?", false),
        new Option<bool>("56205", "Cuca's Apprentice Hair", "Mode: [select] only\nShould the bot buy \"Cuca's Apprentice Hair\" ?", false),
        new Option<bool>("56208", "Cuca's Apprentice Locks", "Mode: [select] only\nShould the bot buy \"Cuca's Apprentice Locks\" ?", false),
        new Option<bool>("56206", "Cuca's Apprentice Hat", "Mode: [select] only\nShould the bot buy \"Cuca's Apprentice Hat\" ?", false),
        new Option<bool>("56209", "Cuca's Apprentice Hat + Locks", "Mode: [select] only\nShould the bot buy \"Cuca's Apprentice Hat + Locks\" ?", false),
        new Option<bool>("56207", "Cuca's Apprentice Morph", "Mode: [select] only\nShould the bot buy \"Cuca's Apprentice Morph\" ?", false),
        new Option<bool>("56210", "Cuca's Apprentice Morph Hat + Locks", "Mode: [select] only\nShould the bot buy \"Cuca's Apprentice Morph Hat + Locks\" ?", false),
        new Option<bool>("63753", "Pastel Privateer", "Mode: [select] only\nShould the bot buy \"Pastel Privateer\" ?", false),
        new Option<bool>("63756", "Privateer's Scarf", "Mode: [select] only\nShould the bot buy \"Privateer's Scarf\" ?", false),
        new Option<bool>("63757", "Privateer's Scarf + Locks", "Mode: [select] only\nShould the bot buy \"Privateer's Scarf + Locks\" ?", false),
        new Option<bool>("63758", "Privateer's Eyepatch", "Mode: [select] only\nShould the bot buy \"Privateer's Eyepatch\" ?", false),
        new Option<bool>("63759", "Privateer's Eyepatch + Locks", "Mode: [select] only\nShould the bot buy \"Privateer's Eyepatch + Locks\" ?", false),
        new Option<bool>("63760", "Privateer's Bicorne", "Mode: [select] only\nShould the bot buy \"Privateer's Bicorne\" ?", false),
        new Option<bool>("63761", "Privateer's Bicorne + Locks", "Mode: [select] only\nShould the bot buy \"Privateer's Bicorne + Locks\" ?", false),
        new Option<bool>("63762", "Privateer's Eyepatched Bicorne", "Mode: [select] only\nShould the bot buy \"Privateer's Eyepatched Bicorne\" ?", false),
        new Option<bool>("63763", "Privateer's Eyepatched Bicorne + Locks", "Mode: [select] only\nShould the bot buy \"Privateer's Eyepatched Bicorne + Locks\" ?", false),
        new Option<bool>("63764", "Privateer's Scarf + Bicorne", "Mode: [select] only\nShould the bot buy \"Privateer's Scarf + Bicorne\" ?", false),
        new Option<bool>("63765", "Privateer's Scarfed Bicorne + Locks", "Mode: [select] only\nShould the bot buy \"Privateer's Scarfed Bicorne + Locks\" ?", false),
        new Option<bool>("63766", "Privateer's Cloak", "Mode: [select] only\nShould the bot buy \"Privateer's Cloak\" ?", false),
        new Option<bool>("63767", "Privateer's Cloaked Sheath Holster", "Mode: [select] only\nShould the bot buy \"Privateer's Cloaked Sheath Holster\" ?", false),
        new Option<bool>("63768", "Privateer's Holster", "Mode: [select] only\nShould the bot buy \"Privateer's Holster\" ?", false),
        new Option<bool>("63769", "Privateer's Sheath", "Mode: [select] only\nShould the bot buy \"Privateer's Sheath\" ?", false),
        new Option<bool>("63770", "Privateer's Sheath Holster", "Mode: [select] only\nShould the bot buy \"Privateer's Sheath Holster\" ?", false),
        new Option<bool>("63774", "Privateer's Pistol", "Mode: [select] only\nShould the bot buy \"Privateer's Pistol\" ?", false),
        new Option<bool>("63775", "Privateer's Dual Pistols", "Mode: [select] only\nShould the bot buy \"Privateer's Dual Pistols\" ?", false),
        new Option<bool>("63776", "Privateer's Hand Cannon", "Mode: [select] only\nShould the bot buy \"Privateer's Hand Cannon\" ?", false),
        new Option<bool>("63777", "Privateer's Cutlass + Pistol", "Mode: [select] only\nShould the bot buy \"Privateer's Cutlass + Pistol\" ?", false),
        new Option<bool>("72092", "Pitiless Privateer", "Mode: [select] only\nShould the bot buy \"Pitiless Privateer\" ?", false),
        new Option<bool>("72093", "Pitiless Privateer's Eyepatch", "Mode: [select] only\nShould the bot buy \"Pitiless Privateer's Eyepatch\" ?", false),
        new Option<bool>("72094", "Pitiless Privateer's Eyepatch + Locks", "Mode: [select] only\nShould the bot buy \"Pitiless Privateer's Eyepatch + Locks\" ?", false),
        new Option<bool>("72095", "Pitiless Privateer's Bicorne", "Mode: [select] only\nShould the bot buy \"Pitiless Privateer's Bicorne\" ?", false),
        new Option<bool>("72096", "Pitiless Privateer's Bicorne + Locks", "Mode: [select] only\nShould the bot buy \"Pitiless Privateer's Bicorne + Locks\" ?", false),
        new Option<bool>("72097", "Pitiless Privateer's Cloak", "Mode: [select] only\nShould the bot buy \"Pitiless Privateer's Cloak\" ?", false),
        new Option<bool>("72098", "Pitiless Privateer's Sheath", "Mode: [select] only\nShould the bot buy \"Pitiless Privateer's Sheath\" ?", false),
        new Option<bool>("72099", "Pitiless Privateer's Pistol", "Mode: [select] only\nShould the bot buy \"Pitiless Privateer's Pistol\" ?", false),
        new Option<bool>("72100", "Pitiless Privateer's Dual Pistols", "Mode: [select] only\nShould the bot buy \"Pitiless Privateer's Dual Pistols\" ?", false),
    };
}
