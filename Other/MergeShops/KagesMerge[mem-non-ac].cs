//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Nukemichi[Mem].cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class KagesMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public Nukemichi Nukemichi = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Jade Box Trinket", "Jade Box Jewel", "Jade Box Heirloom "});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        Nukemichi.NukemichiQuests();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("akiba", 355, findIngredients);

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

                case "Jade Box Trinket":
                case "Jade Box Jewel":
                case "Jade Box Heirloom":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(1593);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("akiba", "Shadow Nukemichi", "Jade Box");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("10762", "Shadow Dancer", "Mode: [select] only\nShould the bot buy \"Shadow Dancer\" ?", false),
        new Option<bool>("10763", "Shadow Dancer's Headband", "Mode: [select] only\nShould the bot buy \"Shadow Dancer's Headband\" ?", false),
        new Option<bool>("10764", "Shadow Dancer's Lethal Headband", "Mode: [select] only\nShould the bot buy \"Shadow Dancer's Lethal Headband\" ?", false),
        new Option<bool>("10765", "Shadow Dancer Mask", "Mode: [select] only\nShould the bot buy \"Shadow Dancer Mask\" ?", false),
        new Option<bool>("10766", "Shadow Dancer Lethal Mask", "Mode: [select] only\nShould the bot buy \"Shadow Dancer Lethal Mask\" ?", false),
        new Option<bool>("10768", "Shadowed Butterfly Blades", "Mode: [select] only\nShould the bot buy \"Shadowed Butterfly Blades\" ?", false),
        new Option<bool>("10767", "Sai of Shadows", "Mode: [select] only\nShould the bot buy \"Sai of Shadows\" ?", false),
        new Option<bool>("10770", "Shuriken", "Mode: [select] only\nShould the bot buy \"Shuriken\" ?", false),
        new Option<bool>("10769", "Dual Dragonblades", "Mode: [select] only\nShould the bot buy \"Dual Dragonblades\" ?", false),
    };
}
