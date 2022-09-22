//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FirePlaneWarMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreSoW SoW = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("fireplanewar", 2006, findIngredients);

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

                case "Elemental Embers":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8125, 8126);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("fireplanewar", "r5", "Right", "*", "War Medal", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Burnt Cinders":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    SoW.Tyndarius();
                    Core.RegisterQuests(8131);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("fireplanewar", "ShadowClaw", "ShadowClaw Defeated", quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Seared Ashes":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("fireplanewar", "ShadowFlame Phedra", req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "ShadowFlame Flamberge":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("fireplanewar", 2007, req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Refulgent Flamberge":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("fireplanewar", "Shadowflame Soldier", req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "ShadowFlame Great Harp":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("fireplanewar", 2007, req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Vulcan Great Harp":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("fireplanewar", "Shadefire Onslaught", req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("60979", "Infernus Guardian", "Mode: [select] only\nShould the bot buy \"Infernus Guardian\" ?", false),
        new Option<bool>("60980", "Infernus Guardian's Helm", "Mode: [select] only\nShould the bot buy \"Infernus Guardian's Helm\" ?", false),
        new Option<bool>("60981", "Infernus Guardian's Wings", "Mode: [select] only\nShould the bot buy \"Infernus Guardian's Wings\" ?", false),
        new Option<bool>("60982", "Infernus Guardian's Mace", "Mode: [select] only\nShould the bot buy \"Infernus Guardian's Mace\" ?", false),
        new Option<bool>("61398", "Shadowflame Firebender", "Mode: [select] only\nShould the bot buy \"Shadowflame Firebender\" ?", false),
        new Option<bool>("61399", "ShadowFlame Firebender Morph", "Mode: [select] only\nShould the bot buy \"ShadowFlame Firebender Morph\" ?", false),
        new Option<bool>("61400", "ShadowFlame Firebender Guard", "Mode: [select] only\nShould the bot buy \"ShadowFlame Firebender Guard\" ?", false),
        new Option<bool>("61401", "Shadow of Malgor", "Mode: [select] only\nShould the bot buy \"Shadow of Malgor\" ?", false),
        new Option<bool>("61402", "ShadowFlame Firebender Tail", "Mode: [select] only\nShould the bot buy \"ShadowFlame Firebender Tail\" ?", false),
        new Option<bool>("61403", "ShadowFlame Firebender Daggers", "Mode: [select] only\nShould the bot buy \"ShadowFlame Firebender Daggers\" ?", false),
        new Option<bool>("61404", "ShadowFlame Firebender Claws", "Mode: [select] only\nShould the bot buy \"ShadowFlame Firebender Claws\" ?", false),
        new Option<bool>("61405", "ShadowFlame Firebender Blade", "Mode: [select] only\nShould the bot buy \"ShadowFlame Firebender Blade\" ?", false),
        new Option<bool>("61334", "Enchanted ShadowDrake's Lance", "Mode: [select] only\nShould the bot buy \"Enchanted ShadowDrake's Lance\" ?", false),
        new Option<bool>("61335", "Enchanted ShadowFlame Flamberge", "Mode: [select] only\nShould the bot buy \"Enchanted ShadowFlame Flamberge\" ?", false),
        new Option<bool>("61336", "Enchanted ShadowFlame Great Harp", "Mode: [select] only\nShould the bot buy \"Enchanted ShadowFlame Great Harp\" ?", false),
        new Option<bool>("61337", "Enchanted FireDrake's Lance", "Mode: [select] only\nShould the bot buy \"Enchanted FireDrake's Lance\" ?", false),
        new Option<bool>("61595", "Dual ShadowFlame Firebender Blades", "Mode: [select] only\nShould the bot buy \"Dual ShadowFlame Firebender Blades\" ?", false),
    };
}
