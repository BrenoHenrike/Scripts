//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Friday13th/CoreFriday13th.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DeadflyMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreFriday13th CoreFriday13th = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Ragged Cloth Scrap", "Deadfly's Armor", "Deadfly Morph", "Rotfinger's Bow", "Rotfinger's ArmBlades", "Rotfinger's Scythe", "Rotfinger's Staff " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("deadfly", 2037, findIngredients);

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

                case "Ragged Cloth Scrap":
                    CoreFriday13th.Deadfly();
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8212);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Underworld Home Renovations 8212
                        Core.HuntMonster("RotFinger", "Rotfinger", "Rotfinger Parts", 3);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Deadfly's Armor":
                case "Deadfly Morph":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("DeadFly", "Deadfly", req.Name, isTemp: false);
                    break;

                case "Rotfinger's Bow":
                case "Rotfinger's ArmBlades":
                case "Rotfinger's Scythe":
                case "Rotfinger's Staff":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("RotFinger", "Rotfinger", req.Name, isTemp: false);
                    break;
                
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("62803", "Necro Deadfly", "Mode: [select] only\nShould the bot buy \"Necro Deadfly\" ?", false),
        new Option<bool>("62804", "Necro Deadfly's Morph", "Mode: [select] only\nShould the bot buy \"Necro Deadfly's Morph\" ?", false),
        new Option<bool>("62650", "Necrotfinger's Bow", "Mode: [select] only\nShould the bot buy \"Necrotfinger's Bow\" ?", false),
        new Option<bool>("62652", "Necrotfinger's ArmBlades", "Mode: [select] only\nShould the bot buy \"Necrotfinger's ArmBlades\" ?", false),
        new Option<bool>("62654", "Necrotfinger's Scythe", "Mode: [select] only\nShould the bot buy \"Necrotfinger's Scythe\" ?", false),
        new Option<bool>("62656", "Necrotfinger's Staff", "Mode: [select] only\nShould the bot buy \"Necrotfinger's Staff\" ?", false),
        new Option<bool>("62802", "Doom Deadfly", "Mode: [select] only\nShould the bot buy \"Doom Deadfly\" ?", false),
        new Option<bool>("62875", "Doom Deadfly's Morph", "Mode: [select] only\nShould the bot buy \"Doom Deadfly's Morph\" ?", false),
        new Option<bool>("62751", "Thirteenth Goalie Mask", "Mode: [select] only\nShould the bot buy \"Thirteenth Goalie Mask\" ?", false),
        new Option<bool>("62847", "Thirteenth Zard Pet", "Mode: [select] only\nShould the bot buy \"Thirteenth Zard Pet\" ?", false),
        new Option<bool>("62715", "BlackSkulls Weapon Kit", "Mode: [select] only\nShould the bot buy \"BlackSkulls Weapon Kit\" ?", false),
    };
}
