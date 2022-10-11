//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CandiedCostumesMerge
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
        Core.BankingBlackList.AddRange(
            new[]
            {
                "MarshMeowllows",
                "Horehound Bits",
                "Kitty Cordials",
                "Candied Jalapeno",
                "Spicy Sample "
            }
        );
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        if (!Core.isSeasonalMapActive("mogloween"))
            return;
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("candyshop", 1761, findIngredients);

        #region Dont edit this part
        void findIngredients()
        {
            ItemBase req = Adv.externalItem;
            int quant = Adv.externalQuant;
            int currentQuant = req.Temp
                ? Bot.TempInv.GetQuantity(req.Name)
                : Bot.Inventory.GetQuantity(req.Name);
            if (req == null)
            {
                Core.Logger("req is NULL");
                return;
            }

            switch (req.Name)
            {
                default:
                    bool shouldStop = Adv.matsOnly ? !dontStopMissingIng : true;
                    Core.Logger(
                        $"The bot hasn't been taught how to get {req.Name}."
                            + (shouldStop ? " Please report the issue." : " Skipping"),
                        messageBox: shouldStop,
                        stopBot: shouldStop
                    );
                    break;
                #endregion

                case "MarshMeowllows":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    //Chocolate and Caramel Cravings 7120
                    Core.RegisterQuests(7120);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("twigsarcade", "Clucky Moo", "Chocolate Candy", 10);
                        Core.KillMonster("pie", "r5", "left", "Gourdo", "Pumpkin Caramel", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Horehound Bits":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    //Loads of Gummies and Lollies 7121
                    Core.RegisterQuests(7121);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("twigsarcade", "Baby", "Gummy Babies", 10);
                        Core.HuntMonster("pie", "Myst Imp", "Licked Lollies", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Kitty Cordials":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    //Fizzies and Stickies and Gooies OH MY 7122
                    Core.RegisterQuests(7122);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("twigsarcade", "Ectoplasm", "GOO-dies", 6);
                        Core.HuntMonster("chromafection", "Free Samples", "Sour Stickies", 6);
                        Core.HuntMonster("candyshop", "Sugarrush Ghoul", "Fuzzy Fizzies", 6);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Candied Jalapeno":
                case "Spicy Sample":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("candyshop", "Sugarrush Ghoul", req.Name, isTemp: false);
                    break;

            }
        }
    }

    public List<IOption> Select =
        new()
        {
            new Option<bool>("42610","Fairy Tale Wanderer","Mode: [select] only\nShould the bot buy \"Fairy Tale Wanderer\" ?",false),
            new Option<bool>("47883","Flesh Ripper","Mode: [select] only\nShould the bot buy \"Flesh Ripper\" ?",false),
            new Option<bool>("47884","Horned Flesh Ripper Mask","Mode: [select] only\nShould the bot buy \"Horned Flesh Ripper Mask\" ?",false),
            new Option<bool>("47885","Flesh Ripper Mask","Mode: [select] only\nShould the bot buy \"Flesh Ripper Mask\" ?",false),
            new Option<bool>("47886","Flesh Ripper Cape","Mode: [select] only\nShould the bot buy \"Flesh Ripper Cape\" ?",false),
            new Option<bool>("47887","Flesh Ripper Backblades","Mode: [select] only\nShould the bot buy \"Flesh Ripper Backblades\" ?",false),
            new Option<bool>("47888","Flesh Ripper Blade","Mode: [select] only\nShould the bot buy \"Flesh Ripper Blade\" ?",false),
            new Option<bool>("50424","Dual Flesh Ripper Blades","Mode: [select] only\nShould the bot buy \"Dual Flesh Ripper Blades\" ?",false),
            new Option<bool>("48497","Force Guardian","Mode: [select] only\nShould the bot buy \"Force Guardian\" ?",false),
            new Option<bool>("48498","Force Guardian Helm","Mode: [select] only\nShould the bot buy \"Force Guardian Helm\" ?",false),
            new Option<bool>("48499","Force Guardian Hair + Beard","Mode: [select] only\nShould the bot buy \"Force Guardian Hair + Beard\" ?",false),
            new Option<bool>("48500","Force Guardian Hair","Mode: [select] only\nShould the bot buy \"Force Guardian Hair\" ?",false),
            new Option<bool>("48501","Force Guardian Locks","Mode: [select] only\nShould the bot buy \"Force Guardian Locks\" ?",false),
            new Option<bool>("48502","Force Guardian Back Staves","Mode: [select] only\nShould the bot buy \"Force Guardian Back Staves\" ?",false),
            new Option<bool>("48503","Force Guardian Staff + Cape","Mode: [select] only\nShould the bot buy \"Force Guardian Staff + Cape\" ?",false),
            new Option<bool>("48504","Force Guardian Daggers","Mode: [select] only\nShould the bot buy \"Force Guardian Daggers\" ?",false),
            new Option<bool>("48505","Force Guardian Light Staff","Mode: [select] only\nShould the bot buy \"Force Guardian Light Staff\" ?",false),
            new Option<bool>("48507","Force Guardian Light Blade","Mode: [select] only\nShould the bot buy \"Force Guardian Light Blade\" ?",false),
            new Option<bool>("48655","Super Scion Ninja","Mode: [select] only\nShould the bot buy \"Super Scion Ninja\" ?",false),
            new Option<bool>("48656","Super Scion Hair","Mode: [select] only\nShould the bot buy \"Super Scion Hair\" ?",false),
            new Option<bool>("48657","Super Scion SideMask","Mode: [select] only\nShould the bot buy \"Super Scion SideMask\" ?",false),
            new Option<bool>("48658","Super Scion SideMask + Scarf","Mode: [select] only\nShould the bot buy \"Super Scion SideMask + Scarf\" ?",false),
            new Option<bool>("48659","Super Scion Hair + Scarf","Mode: [select] only\nShould the bot buy \"Super Scion Hair + Scarf\" ?",false),
            new Option<bool>("48660","Super Scion Mask","Mode: [select] only\nShould the bot buy \"Super Scion Mask\" ?",false),
            new Option<bool>("48661","Super Scion Locks","Mode: [select] only\nShould the bot buy \"Super Scion Locks\" ?",false),
            new Option<bool>("48662","Super Scion Mask + Locks","Mode: [select] only\nShould the bot buy \"Super Scion Mask + Locks\" ?",false),
            new Option<bool>("48663","Super Scion Locks + Pigtails","Mode: [select] only\nShould the bot buy \"Super Scion Locks + Pigtails\" ?",false),
            new Option<bool>("48664","Super Scion Pigtails + SideMask","Mode: [select] only\nShould the bot buy \"Super Scion Pigtails + SideMask\" ?",false),
            new Option<bool>("48665","Super Scion Masked Pigtails + SideMask","Mode: [select] only\nShould the bot buy \"Super Scion Masked Pigtails + SideMask\" ?",false),
            new Option<bool>("48666","Super Scion Cape","Mode: [select] only\nShould the bot buy \"Super Scion Cape\" ?",false),
            new Option<bool>("48667","Super Scion BackFan","Mode: [select] only\nShould the bot buy \"Super Scion BackFan\" ?",false),
            new Option<bool>("48668","Super Scion Fan + Cape","Mode: [select] only\nShould the bot buy \"Super Scion Fan + Cape\" ?",false),
            new Option<bool>("48669","Super Scion Back Swords","Mode: [select] only\nShould the bot buy \"Super Scion Back Swords\" ?",false),
            new Option<bool>("48670","Super Scion Back Scroll","Mode: [select] only\nShould the bot buy \"Super Scion Back Scroll\" ?",false),
            new Option<bool>("48671","Super Scion BackBlades + Cape","Mode: [select] only\nShould the bot buy \"Super Scion BackBlades + Cape\" ?",false),
            new Option<bool>("48672","Super Scion Blade","Mode: [select] only\nShould the bot buy \"Super Scion Blade\" ?",false),
            new Option<bool>("48673","Dual Super Scion Blades","Mode: [select] only\nShould the bot buy \"Dual Super Scion Blades\" ?",false),
            new Option<bool>("48674","Super Scion Lightning Ball","Mode: [select] only\nShould the bot buy \"Super Scion Lightning Ball\" ?",false),
            new Option<bool>("48675","Dual Super Scion Balls","Mode: [select] only\nShould the bot buy \"Dual Super Scion Balls\" ?",false),
            new Option<bool>("36951","Soft Shelled Cape","Mode: [select] only\nShould the bot buy \"Soft Shelled Cape\" ?",false),
            new Option<bool>("36956","Tacobender","Mode: [select] only\nShould the bot buy \"Tacobender\" ?",false),
            new Option<bool>("36950","Tacomancer","Mode: [select] only\nShould the bot buy \"Tacomancer\" ?",false),
            new Option<bool>("36952","Taco on a Stick","Mode: [select] only\nShould the bot buy \"Taco on a Stick\" ?",false),
            new Option<bool>("36949","Destructo-Tacos","Mode: [select] only\nShould the bot buy \"Destructo-Tacos\" ?",false),
            new Option<bool>("36948","Taco Admiral Hat","Mode: [select] only\nShould the bot buy \"Taco Admiral Hat\" ?",false),
            new Option<bool>("36947","Taco Admiral Locks","Mode: [select] only\nShould the bot buy \"Taco Admiral Locks\" ?",false),
            new Option<bool>("64298","Lady Vayle Costume","Mode: [select] only\nShould the bot buy \"Lady Vayle Costume\" ?",false),
        };
}
