//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BeleensPartyPresentsMerge
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
        Core.BankingBlackList.AddRange(new[] { "Starry Bow", "DOOM Gift", "Scarbucks Gift Card " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        if (!Core.isSeasonalMapActive("yulgar20"))
            return;
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("yulgar20", 2177, findIngredients);

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

                case "Starry Bow":
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("spacepwny", "r3", "Right", "*", req.Name, quant, false);
                    break;

                case "DOOM Gift":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("spacepwny", "Mr DED", req.Name, quant, false);
                    break;

                case "Scarbucks Gift Card":
                    Core.EquipClass(ClassType.Solo);
                    Bot.Quests.UpdateQuest(8892);
                    Core.KillMonster("mermaidsushi", "r7a", "Left", "*", req.Name, quant, false);
                    break;

                case "Golden Anniversary Gift":
                case "Platinum Leaf":
                case "Ultimate Dragonlord Cape":
                case "Ultimate Dragonlord Wings":
                    Core.EquipClass(ClassType.Solo);
                    if (req.Name == "Platinum Leaf")
                        Core.RegisterQuests(8925);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("yulgarparty", "Treasure Pile", "Twilly's Treasure Defeated");
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("73072", "Gold Pony Star Warrior", "Mode: [select] only\nShould the bot buy \"Gold Pony Star Warrior\" ?", false),
        new Option<bool>("73073", "Gold Pony's Candied Helm", "Mode: [select] only\nShould the bot buy \"Gold Pony's Candied Helm\" ?", false),
        new Option<bool>("73074", "Gold Pony's Cloudy Helm", "Mode: [select] only\nShould the bot buy \"Gold Pony's Cloudy Helm\" ?", false),
        new Option<bool>("73075", "Rainbow Pony Morph", "Mode: [select] only\nShould the bot buy \"Rainbow Pony Morph\" ?", false),
        new Option<bool>("73076", "Gold Pony Cloak", "Mode: [select] only\nShould the bot buy \"Gold Pony Cloak\" ?", false),
        new Option<bool>("73077", "Rainbow Pony Wings", "Mode: [select] only\nShould the bot buy \"Rainbow Pony Wings\" ?", false),
        new Option<bool>("73078", "Raibow Pony Tail", "Mode: [select] only\nShould the bot buy \"Raibow Pony Tail\" ?", false),
        new Option<bool>("73079", "Gold Pony Star Lance", "Mode: [select] only\nShould the bot buy \"Gold Pony Star Lance\" ?", false),
        new Option<bool>("73080", "Rainbow Pony Plushie", "Mode: [select] only\nShould the bot buy \"Rainbow Pony Plushie\" ?", false),
        new Option<bool>("73081", "Pink Pony Star Warrior", "Mode: [select] only\nShould the bot buy \"Pink Pony Star Warrior\" ?", false),
        new Option<bool>("73082", "Pink Pony's Maned Helm", "Mode: [select] only\nShould the bot buy \"Pink Pony's Maned Helm\" ?", false),
        new Option<bool>("73083", "Pink Pony's Cotton Helm", "Mode: [select] only\nShould the bot buy \"Pink Pony's Cotton Helm\" ?", false),
        new Option<bool>("73084", "Pink Pony Morph", "Mode: [select] only\nShould the bot buy \"Pink Pony Morph\" ?", false),
        new Option<bool>("73085", "Pink Pony Cloak", "Mode: [select] only\nShould the bot buy \"Pink Pony Cloak\" ?", false),
        new Option<bool>("73086", "Pink Pony Wings", "Mode: [select] only\nShould the bot buy \"Pink Pony Wings\" ?", false),
        new Option<bool>("73087", "Pink Pony Tail", "Mode: [select] only\nShould the bot buy \"Pink Pony Tail\" ?", false),
        new Option<bool>("73088", "Pink Pony Star Lance", "Mode: [select] only\nShould the bot buy \"Pink Pony Star Lance\" ?", false),
        new Option<bool>("73089", "Pink Pony Plushie", "Mode: [select] only\nShould the bot buy \"Pink Pony Plushie\" ?", false),
        new Option<bool>("72279", "Bean Twilly", "Mode: [select] only\nShould the bot buy \"Bean Twilly\" ?", false),
    };
}
