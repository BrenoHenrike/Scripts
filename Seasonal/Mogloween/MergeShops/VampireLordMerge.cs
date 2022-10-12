//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class VampireLordMerge
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
        Core.BankingBlackList.AddRange(new[] { "Blood Moon Token "});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        if (!Core.isSeasonalMapActive("mogloween"))
            return;
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("mogloween", 1477, findIngredients);

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

                case "Blood Moon Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    // Core.RegisterQuests(Core.IsMember ? 6060 : 6059); // uncomment when registerquest is fixed. if more then 1 item is found in inv it only complets once then afks/
                    while (!Bot.ShouldExit && !Core.CheckInventory("Blood Moon Token", quant))
                    {
                        Core.EnsureAccept(Core.IsMember ? 6060 : 6059);
                        Core.KillMonster("bloodmoon", "r12a", "Left", "Black Unicorn", "Black Blood Vial", isTemp: false);
                        Core.KillMonster("bloodmoon", "r4a", "Left", "Lycan Guard", "Moon Stone", isTemp: false);
                        Core.EnsureComplete(Core.IsMember ? 6060 : 6059);
                        Bot.Wait.ForPickup("Blood Moon Token");
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("41575", "Vampire Lord (Class)", "Mode: [select] only\nShould the bot buy \"Vampire Lord\" ?", false),
        new Option<bool>("41619", "Vampire Lord", "Mode: [select] only\nShould the bot buy \"Vampire Lord\" ?", false),
        new Option<bool>("41623", "Vampire Lord Morph", "Mode: [select] only\nShould the bot buy \"Vampire Lord Morph\" ?", false),
        new Option<bool>("41621", "Vampire Lord Locks Morph", "Mode: [select] only\nShould the bot buy \"Vampire Lord Locks Morph\" ?", false),
        new Option<bool>("41624", "Vampire Lord Cape", "Mode: [select] only\nShould the bot buy \"Vampire Lord Cape\" ?", false),
        new Option<bool>("41666", "Enraged Vampire Morph", "Mode: [select] only\nShould the bot buy \"Enraged Vampire Morph\" ?", false),
    };
}
