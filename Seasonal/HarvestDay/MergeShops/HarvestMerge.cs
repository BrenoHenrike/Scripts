//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/HarvestDay/CoreHarvestDay.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class HarvestMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreHarvestDay HarvestDay = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Goredon's Zard Sauce", "Harvest Golem Parfait", "Ultra Turdrakogiblet", "Wretched Rider Meat", "Overgourd Seed " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        if (!Core.isSeasonalMapActive("feastboss"))
            return;
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("feast", 2181, findIngredients);

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

                case "Goredon's Zard Sauce":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("feastboss", "Goredon Rampage", req.Name, quant, isTemp: false);
                    break;

                case "Harvest Golem Parfait":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("feastwarevil", "Harvest Golem", req.Name, quant, isTemp: false);
                    break;

                case "Ultra Turdrakogiblet":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("killerkitchen", "Ultra Turdrakolich", req.Name, quant, isTemp: false);
                    break;

                case "Wretched Rider Meat":
                    HarvestDay.FoulFarm();
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("dullahan", "Wretched Rider", req.Name, quant, isTemp: false);
                    break;

                case "Overgourd Seed":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("fearfeast", "OverGourd", req.Name, quant, isTemp: false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("70928", "Candy Corn Crusher", "Mode: [select] only\nShould the bot buy \"Candy Corn Crusher\" ?", false),
        new Option<bool>("70929", "Candy Corn Crusher Helm", "Mode: [select] only\nShould the bot buy \"Candy Corn Crusher Helm\" ?", false),
        new Option<bool>("70930", "Candy Corn Crusher Hood", "Mode: [select] only\nShould the bot buy \"Candy Corn Crusher Hood\" ?", false),
        new Option<bool>("70931", "Candy Corn Crusher Hat", "Mode: [select] only\nShould the bot buy \"Candy Corn Crusher Hat\" ?", false),
        new Option<bool>("70932", "Candy Corn Crusher Mask", "Mode: [select] only\nShould the bot buy \"Candy Corn Crusher Mask\" ?", false),
        new Option<bool>("70933", "Candy Corn Crusher Cape", "Mode: [select] only\nShould the bot buy \"Candy Corn Crusher Cape\" ?", false),
        new Option<bool>("70934", "Candy Corn Crusher Rune", "Mode: [select] only\nShould the bot buy \"Candy Corn Crusher Rune\" ?", false),
        new Option<bool>("70935", "Candy Corn Crusher Pet", "Mode: [select] only\nShould the bot buy \"Candy Corn Crusher Pet\" ?", false),
        new Option<bool>("70936", "Candy Corn Crusher Blade", "Mode: [select] only\nShould the bot buy \"Candy Corn Crusher Blade\" ?", false),
        new Option<bool>("70937", "Candy Corn Crusher Axe", "Mode: [select] only\nShould the bot buy \"Candy Corn Crusher Axe\" ?", false),
        new Option<bool>("70938", "Candy Corn Crusher Drills", "Mode: [select] only\nShould the bot buy \"Candy Corn Crusher Drills\" ?", false),
        new Option<bool>("73488", "Headless Pumpkin", "Mode: [select] only\nShould the bot buy \"Headless Pumpkin\" ?", false),
        new Option<bool>("73491", "Headless Pumpkin Blade", "Mode: [select] only\nShould the bot buy \"Headless Pumpkin Blade\" ?", false),
        new Option<bool>("73489", "Headless Pumpkin's... Head?", "Mode: [select] only\nShould the bot buy \"Headless Pumpkin's... Head?\" ?", false),
        new Option<bool>("73490", "Headless Pumpkin Cape", "Mode: [select] only\nShould the bot buy \"Headless Pumpkin Cape\" ?", false),
        new Option<bool>("73492", "Headless Pumpkin Blades", "Mode: [select] only\nShould the bot buy \"Headless Pumpkin Blades\" ?", false),
    };
}
