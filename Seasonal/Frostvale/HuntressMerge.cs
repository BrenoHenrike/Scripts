//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class HuntressMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
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
        Core.BankingBlackList.AddRange(new[] { "Sluagh Bell", "Punk Coal Elf Stabber", "Festive Punk Elf Stabber", "Wild Huntress' Sword " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("otziwar", 2088, findIngredients);

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

                case "Sluagh Bell":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8446, 8447, 8448);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("otziwar", "Sluagh Warrior", "Ancient Fragments", 3);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Punk Coal Elf Stabber":
                case "Festive Punk Elf Stabber":
                case "Wild Huntress' Sword":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("otziwar", "Huntress Valais", req.Name, isTemp: false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("65587", "Ice Wizard", "Mode: [select] only\nShould the bot buy \"Ice Wizard\" ?", false),
        new Option<bool>("65588", "Ice Warlock's Hood", "Mode: [select] only\nShould the bot buy \"Ice Warlock's Hood\" ?", false),
        new Option<bool>("65589", "Ice Witch's Hood", "Mode: [select] only\nShould the bot buy \"Ice Witch's Hood\" ?", false),
        new Option<bool>("65590", "Ice Wizard's Rune", "Mode: [select] only\nShould the bot buy \"Ice Wizard's Rune\" ?", false),
        new Option<bool>("65591", "Ice Wizard's Portal", "Mode: [select] only\nShould the bot buy \"Ice Wizard's Portal\" ?", false),
        new Option<bool>("65592", "Ice Wizard's Cane", "Mode: [select] only\nShould the bot buy \"Ice Wizard's Cane\" ?", false),
        new Option<bool>("66121", "Punk Coal Elf", "Mode: [select] only\nShould the bot buy \"Punk Coal Elf\" ?", false),
        new Option<bool>("66122", "Punk Coal Elf's Hair", "Mode: [select] only\nShould the bot buy \"Punk Coal Elf's Hair\" ?", false),
        new Option<bool>("66123", "Punk Coal Elf's Locks", "Mode: [select] only\nShould the bot buy \"Punk Coal Elf's Locks\" ?", false),
        new Option<bool>("66124", "Punk Coal Elf's Bag", "Mode: [select] only\nShould the bot buy \"Punk Coal Elf's Bag\" ?", false),
        new Option<bool>("66125", "Punk Coal Elf's Back Gun", "Mode: [select] only\nShould the bot buy \"Punk Coal Elf's Back Gun\" ?", false),
        new Option<bool>("66126", "Punk Coal Elf's Scythe", "Mode: [select] only\nShould the bot buy \"Punk Coal Elf's Scythe\" ?", false),
        new Option<bool>("66127", "Punk Coal Elf's Dynamite", "Mode: [select] only\nShould the bot buy \"Punk Coal Elf's Dynamite\" ?", false),
        new Option<bool>("66129", "Dual Punk Coal Elf's Stabbers", "Mode: [select] only\nShould the bot buy \"Dual Punk Coal Elf's Stabbers\" ?", false),
        new Option<bool>("66130", "Punk Coal Elf's Machine Gun", "Mode: [select] only\nShould the bot buy \"Punk Coal Elf's Machine Gun\" ?", false),
        new Option<bool>("66131", "Festive Punk Elf", "Mode: [select] only\nShould the bot buy \"Festive Punk Elf\" ?", false),
        new Option<bool>("66132", "Festive Punk Elf's Hair", "Mode: [select] only\nShould the bot buy \"Festive Punk Elf's Hair\" ?", false),
        new Option<bool>("66133", "Festive Punk Elf's Locks", "Mode: [select] only\nShould the bot buy \"Festive Punk Elf's Locks\" ?", false),
        new Option<bool>("66134", "Festive Punk Elf's Bag", "Mode: [select] only\nShould the bot buy \"Festive Punk Elf's Bag\" ?", false),
        new Option<bool>("66135", "Festive Punk Elf's Back Gun", "Mode: [select] only\nShould the bot buy \"Festive Punk Elf's Back Gun\" ?", false),
        new Option<bool>("66136", "Festive Punk Elf's Scythe", "Mode: [select] only\nShould the bot buy \"Festive Punk Elf's Scythe\" ?", false),
        new Option<bool>("66137", "Festive Punk Elf's Dynamite", "Mode: [select] only\nShould the bot buy \"Festive Punk Elf's Dynamite\" ?", false),
        new Option<bool>("66139", "Dual Festive Punk Elf's Stabbers", "Mode: [select] only\nShould the bot buy \"Dual Festive Punk Elf's Stabbers\" ?", false),
        new Option<bool>("66140", "Festive Punk Elf's Machine Gun", "Mode: [select] only\nShould the bot buy \"Festive Punk Elf's Machine Gun\" ?", false),
        new Option<bool>("66840", "Frostval Valencia Armor", "Mode: [select] only\nShould the bot buy \"Frostval Valencia Armor\" ?", false),
        new Option<bool>("66843", "Frostval Trissa Armor", "Mode: [select] only\nShould the bot buy \"Frostval Trissa Armor\" ?", false),
        new Option<bool>("66841", "Valencia's Hair + Hat", "Mode: [select] only\nShould the bot buy \"Valencia's Hair + Hat\" ?", false),
        new Option<bool>("66842", "Valencia's Locks + Hat", "Mode: [select] only\nShould the bot buy \"Valencia's Locks + Hat\" ?", false),
        new Option<bool>("66844", "Trissa's Hair + Hat", "Mode: [select] only\nShould the bot buy \"Trissa's Hair + Hat\" ?", false),
        new Option<bool>("66845", "Trissa's Locks + Hat", "Mode: [select] only\nShould the bot buy \"Trissa's Locks + Hat\" ?", false),
        new Option<bool>("66848", "Wild Huntress' Blade", "Mode: [select] only\nShould the bot buy \"Wild Huntress' Blade\" ?", false),
        new Option<bool>("66862", "Dual Huntress Blades", "Mode: [select] only\nShould the bot buy \"Dual Huntress Blades\" ?", false),
    };
}
