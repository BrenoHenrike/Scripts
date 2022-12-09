//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DarkWarNationMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    public CoreNation Nation = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Nation Defender Medal", "Nation Trophy", "Nation War Banner " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        if (!Core.isSeasonalMapActive("darkwarnation"))
            return;

        Adv.StartBuyAllMerge("darkwarnation", 2121, findIngredients);

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

                case "Nation Defender Medal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8578, 8579);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("darkwarlegion", "*", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Nation Trophy":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8580);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("darkwarnation", "Legion Doomknight", "Legion Doomed", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Nation War Banner":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8581);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("darkwarnation", "Legion Dread Knight", "Legion's Dread", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Spoils of War":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8582);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("darkwarnation", "War", "War Defeated");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("67464", "Fiendfang Scythe", "Mode: [select] only\nShould the bot buy \"Fiendfang Scythe\" ?", false),
        new Option<bool>("67466", "Narakan Fiend", "Mode: [select] only\nShould the bot buy \"Narakan Fiend\" ?", false),
        new Option<bool>("67467", "Narakan Fiend's Helm", "Mode: [select] only\nShould the bot buy \"Narakan Fiend's Helm\" ?", false),
        new Option<bool>("67468", "Narakan Fiend's Shadow", "Mode: [select] only\nShould the bot buy \"Narakan Fiend's Shadow\" ?", false),
        new Option<bool>("67469", "Narakan Fiend's Spear", "Mode: [select] only\nShould the bot buy \"Narakan Fiend's Spear\" ?", false),
        new Option<bool>("67470", "Narakan Fiend's ArmBlade", "Mode: [select] only\nShould the bot buy \"Narakan Fiend's ArmBlade\" ?", false),
        new Option<bool>("67471", "Narakan Fiend's ArmBlades", "Mode: [select] only\nShould the bot buy \"Narakan Fiend's ArmBlades\" ?", false),
        new Option<bool>("67488", "Tempest Void", "Mode: [select] only\nShould the bot buy \"Tempest Void\" ?", false),
        new Option<bool>("67489", "Tempest Void's Shroud", "Mode: [select] only\nShould the bot buy \"Tempest Void's Shroud\" ?", false),
        new Option<bool>("67490", "Tempest Void's Orb", "Mode: [select] only\nShould the bot buy \"Tempest Void's Orb\" ?", false),
        new Option<bool>("67491", "Tempest Void's Piercers", "Mode: [select] only\nShould the bot buy \"Tempest Void's Piercers\" ?", false),
        new Option<bool>("68337", "Void Recruit's BackSword + Shield", "Mode: [select] only\nShould the bot buy \"Void Recruit's BackSword + Shield\" ?", false),
        new Option<bool>("68339", "Void Recruit's Sword", "Mode: [select] only\nShould the bot buy \"Void Recruit's Sword\" ?", false),
        new Option<bool>("68340", "Void Recruit's Swords", "Mode: [select] only\nShould the bot buy \"Void Recruit's Swords\" ?", false),
        new Option<bool>("68338", "Void Recruit's Back Shield", "Mode: [select] only\nShould the bot buy \"Void Recruit's Back Shield\" ?", false),
        new Option<bool>("67492", "Midnight Storm Void", "Mode: [select] only\nShould the bot buy \"Midnight Storm Void\" ?", false),
        new Option<bool>("67493", "Midnight Storm Void's Shroud", "Mode: [select] only\nShould the bot buy \"Midnight Storm Void's Shroud\" ?", false),
        new Option<bool>("67494", "Midnight Storm Void's Piercers", "Mode: [select] only\nShould the bot buy \"Midnight Storm Void's Piercers\" ?", false),
        new Option<bool>("68774", "Staff of the Archfiend", "Mode: [select] only\nShould the bot buy \"Staff of the Archfiend\" ?", false),
        new Option<bool>("68775", "Brutal Axe of the Archfiend", "Mode: [select] only\nShould the bot buy \"Brutal Axe of the Archfiend\" ?", false),
        new Option<bool>("68776", "Brutal Axes of the Archfiend", "Mode: [select] only\nShould the bot buy \"Brutal Axes of the Archfiend\" ?", false),
        new Option<bool>("68779", "Reaver of the Archfiend", "Mode: [select] only\nShould the bot buy \"Reaver of the Archfiend\" ?", false),
        new Option<bool>("68780", "Reavers of the Archfiend", "Mode: [select] only\nShould the bot buy \"Reavers of the Archfiend\" ?", false),
        new Option<bool>("68781", "Abysal BloodSpear", "Mode: [select] only\nShould the bot buy \"Abysal BloodSpear\" ?", false),
        new Option<bool>("68782", "Abysal BloodSpears", "Mode: [select] only\nShould the bot buy \"Abysal BloodSpears\" ?", false),
    };
}
