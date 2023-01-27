/*
name: Harvest Merge
description: This will get all or selected items on this merge shop.
tags: harvest-merge, seasonal, harvest-day
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/HarvestDay/CoreHarvestDay.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class HarvestMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    public CoreHarvestDay HarvestDay = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Goredon's Zard Sauce", "Harvest Golem Parfait", "Ultra Turdrakogiblet", "Wretched Rider Meat", "Overgourd Seed", "Autumnal Civilian", "Maple Leaf", "Burnt Feather", "Autumnal Civilian Hair", "Autumnal Locks", "Scarbucks Pumpkin Spice Latte", "Scarbucks Pumpkin Pie", "Iron Ore", "TurKing Claw", "Guncraft Shadowslayer Big Iron", "Guncraft Shadowslayer Big Irons " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("feast", 2181, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Autumnal Civilian Hair":
                case "Autumnal Locks":
                case "Scarbucks Pumpkin Spice Latte":
                case "Maple Leaf":
                case "Burnt Feather":
                case "Scarbucks Pumpkin Pie":
                case "Autumnal Civilian":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("birdswithharms", "Rawrgobble", req.Name, quant, isTemp: false, log: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;


                case "Iron Ore":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.AddDrop(req.ID);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.ID, quant))
                        Core.HuntMonster("birdswithharms", "TurKing", log: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "TurKing Claw":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.AddDrop(req.ID);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.ID, quant))
                        Core.KillMonster("birdswithharms", "r10", "Left", "TurKing", log: false);
                    Bot.Wait.ForPickup(req.ID);
                    break;

                case "Guncraft Shadowslayer Big Irons":
                case "Guncraft Shadowslayer Big Iron":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("ebilmech", "Ebil Mech Dragon", req.Name, isTemp: false, log: false);
                    Bot.Wait.ForPickup(req.Name);
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
        new Option<bool>("73827", "Autumniphile", "Mode: [select] only\nShould the bot buy \"Autumniphile\" ?", false),
        new Option<bool>("73830", "Autumnal Civilian Beret", "Mode: [select] only\nShould the bot buy \"Autumnal Civilian Beret\" ?", false),
        new Option<bool>("73831", "Autumnal Civilian Beret + Locks", "Mode: [select] only\nShould the bot buy \"Autumnal Civilian Beret + Locks\" ?", false),
        new Option<bool>("73832", "Autumniphile Beret", "Mode: [select] only\nShould the bot buy \"Autumniphile Beret\" ?", false),
        new Option<bool>("73833", "Autumniphile Beret + Locks", "Mode: [select] only\nShould the bot buy \"Autumniphile Beret + Locks\" ?", false),
        new Option<bool>("73834", "Full Autumniphile Morph", "Mode: [select] only\nShould the bot buy \"Full Autumniphile Morph\" ?", false),
        new Option<bool>("73835", "Full Autumniphile Morph + Locks", "Mode: [select] only\nShould the bot buy \"Full Autumniphile Morph + Locks\" ?", false),
        new Option<bool>("73836", "Autumnal Maple Leaves", "Mode: [select] only\nShould the bot buy \"Autumnal Maple Leaves\" ?", false),
        new Option<bool>("73844", "Scarbucks Double Dessert", "Mode: [select] only\nShould the bot buy \"Scarbucks Double Dessert\" ?", false),
        new Option<bool>("74252", "Elite Guncraft Shadowslayer", "Mode: [select] only\nShould the bot buy \"Elite Guncraft Shadowslayer\" ?", false),
        new Option<bool>("74253", "Elite Guncraft Stetson", "Mode: [select] only\nShould the bot buy \"Elite Guncraft Stetson\" ?", false),
        new Option<bool>("74254", "Elite Guncraft Stetson Locks", "Mode: [select] only\nShould the bot buy \"Elite Guncraft Stetson Locks\" ?", false),
        new Option<bool>("74255", "Elite Guncraft Masked Stetson", "Mode: [select] only\nShould the bot buy \"Elite Guncraft Masked Stetson\" ?", false),
        new Option<bool>("74256", "Elite Guncraft Masked Stetson Locks", "Mode: [select] only\nShould the bot buy \"Elite Guncraft Masked Stetson Locks\" ?", false),
        new Option<bool>("74257", "Elite Guncraft Vigilante Mask", "Mode: [select] only\nShould the bot buy \"Elite Guncraft Vigilante Mask\" ?", false),
        new Option<bool>("74258", "Elite Guncraft Vigilante Masked Locks", "Mode: [select] only\nShould the bot buy \"Elite Guncraft Vigilante Masked Locks\" ?", false),
        new Option<bool>("74238", "Guncraft Glory", "Mode: [select] only\nShould the bot buy \"Guncraft Glory\" ?", false),
        new Option<bool>("74246", "Guncraft Shadowslayer Bigger Iron", "Mode: [select] only\nShould the bot buy \"Guncraft Shadowslayer Bigger Iron\" ?", false),
        new Option<bool>("74247", "Guncraft Shadowslayer Bigger Irons", "Mode: [select] only\nShould the bot buy \"Guncraft Shadowslayer Bigger Irons\" ?", false),
        new Option<bool>("74248", "Guncraft Shadowslayer Biggest Iron", "Mode: [select] only\nShould the bot buy \"Guncraft Shadowslayer Biggest Iron\" ?", false),
        new Option<bool>("74249", "Guncraft Shadowslayer Biggest Irons", "Mode: [select] only\nShould the bot buy \"Guncraft Shadowslayer Biggest Irons\" ?", false),
        new Option<bool>("74251", "Guncraft Shadowslayer Vulcan Cannon", "Mode: [select] only\nShould the bot buy \"Guncraft Shadowslayer Vulcan Cannon\" ?", false),
        new Option<bool>("74239", "Guncraft Artillery Beast", "Mode: [select] only\nShould the bot buy \"Guncraft Artillery Beast\" ?", false),
    };
}
