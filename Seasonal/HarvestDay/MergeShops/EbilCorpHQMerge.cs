//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class EbilHQMerge
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
        Core.BankingBlackList.AddRange(NeededItems);
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        Core.AddDrop(NeededItems);
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("ebilcorphq", 2067, findIngredients);

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

                case "EbilCoin":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    //Core.RegisterQuests(8408); // Problem Exists Between Chairman and Keyboard
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(8408);
                        Core.HuntMonster("ebilcorphq", "Master Chairman", "Master Chairman Destroyed (again)", 10, log:false);
                        Core.EnsureCompleteMulti(8408);
                    }
                    //Core.CancelRegisteredQuests();
                    break;

                case "Ebil Operative":
                case "Ebil Operative Hair + Mask":
                case "Ebil Operative Protective Gear":
                case "Ebil Operative Hair + Muffler":
                case "Ebil Operative Hair + Scarf":
                case "Ebil Operative Edgy Hair + Mask":
                case "Ebil Operative Edgy Protective Gear":
                case "Ebil Operative Edgy Hair + Scarf":
                case "Ebil Operative Edgy Hair + Muffler":
                case "Ebil Operative Mask Helm":
                case "Ebil Operative Helm + Scarf":
                case "Ebil Operative Hood + Mask":
                case "Ebil Operative Crossed Baton Blades":
                case "Ebil Operative Cape":
                case "Ebil Operative Baton Blade":
                case "Ebil Operative Death Blade":
                case "Ebil Operative Long Baton":
                case "Ebil Operative Tactical Rifle":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ebilcorphq", "Master Chairman", req.Name, isTemp: false, log:false);
                        Core.Logger("This item is not setup yet");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("65106", "Prismatic Ebil Operative", "Mode: [select] only\nShould the bot buy \"Prismatic Ebil Operative\" ?", false),
        new Option<bool>("65107", "Prismatic Operative Hair + Mask", "Mode: [select] only\nShould the bot buy \"Prismatic Operative Hair + Mask\" ?", false),
        new Option<bool>("65108", "Prismatic Operative Protective Gear", "Mode: [select] only\nShould the bot buy \"Prismatic Operative Protective Gear\" ?", false),
        new Option<bool>("65109", "Prismatic Operative Hair + Muffler", "Mode: [select] only\nShould the bot buy \"Prismatic Operative Hair + Muffler\" ?", false),
        new Option<bool>("65110", "Prismatic Operative Hair + Scarf", "Mode: [select] only\nShould the bot buy \"Prismatic Operative Hair + Scarf\" ?", false),
        new Option<bool>("65111", "Prismatic Operative Edgy Hair + Mask", "Mode: [select] only\nShould the bot buy \"Prismatic Operative Edgy Hair + Mask\" ?", false),
        new Option<bool>("65112", "Prismatic Operative Edgy Protective Gear", "Mode: [select] only\nShould the bot buy \"Prismatic Operative Edgy Protective Gear\" ?", false),
        new Option<bool>("65113", "Prismatic Operative Edgy Hair + Scarf", "Mode: [select] only\nShould the bot buy \"Prismatic Operative Edgy Hair + Scarf\" ?", false),
        new Option<bool>("65114", "Prismatic Operative Edgy Hair + Muffler", "Mode: [select] only\nShould the bot buy \"Prismatic Operative Edgy Hair + Muffler\" ?", false),
        new Option<bool>("65115", "Prismatic Operative's Masked Helm", "Mode: [select] only\nShould the bot buy \"Prismatic Operative's Masked Helm\" ?", false),
        new Option<bool>("65116", "Prismatic Operative Helm + Scarf", "Mode: [select] only\nShould the bot buy \"Prismatic Operative Helm + Scarf\" ?", false),
        new Option<bool>("65117", "Prismatic Operative Hooded Mask", "Mode: [select] only\nShould the bot buy \"Prismatic Operative Hooded Mask\" ?", false),
        new Option<bool>("65118", "Prismatic Operative Crossed Baton Blades", "Mode: [select] only\nShould the bot buy \"Prismatic Operative Crossed Baton Blades\" ?", false),
        new Option<bool>("65119", "Prismatic Operative Cape", "Mode: [select] only\nShould the bot buy \"Prismatic Operative Cape\" ?", false),
        new Option<bool>("65120", "Prismatic Operative Baton Blade", "Mode: [select] only\nShould the bot buy \"Prismatic Operative Baton Blade\" ?", false),
        new Option<bool>("65121", "Prismatic Operative Death Blade", "Mode: [select] only\nShould the bot buy \"Prismatic Operative Death Blade\" ?", false),
        new Option<bool>("65122", "Prismatic Operative Long Baton", "Mode: [select] only\nShould the bot buy \"Prismatic Operative Long Baton\" ?", false),
        new Option<bool>("65123", "Prismatic Operative Tactical Rifle", "Mode: [select] only\nShould the bot buy \"Prismatic Operative Tactical Rifle\" ?", false),
    };

    private string[] NeededItems =
    {
        "EbilCoin",
        "Ebil Operative",
        "Ebil Operative Hair + Mask",
        "Ebil Operative Protective Gear",
        "Ebil Operative Hair + Muffler",
        "Ebil Operative Hair + Scarf",
        "Ebil Operative Edgy Hair + Mask",
        "Ebil Operative Edgy Protective Gear",
        "Ebil Operative Edgy Hair + Scarf",
        "Ebil Operative Edgy Hair + Muffler",
        "Ebil Operative Mask Helm",
        "Ebil Operative Helm + Scarf",
        "Ebil Operative Hood + Mask",
        "Ebil Operative Crossed Baton Blades",
        "Ebil Operative Cape",
        "Ebil Operative Baton Blade",
        "Ebil Operative Death Blade",
        "Ebil Operative Long Baton",
        "Ebil Operative Tactical Rifle",
    };
}
