/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FireandIceMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public DragonFableOrigins DFO = new();

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

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        DFO.DragonFableOriginsAll();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("drakonnan", 1596, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Fire and Ice Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6326);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("drakonnan", "Fire Dragon", "Dragon Scale");
                        Core.HuntMonster("drakonnan", "Living Fire", "Ember of a Living Flame");
                        Core.HuntMonster("drakonnan", "Fire Elemental", "Fire Elemental's Gauntlet");
                        Core.HuntMonster("drakonnan", "Living Lava", "Lava Rock");
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Ice Katana":
                    Core.EquipClass(ClassType.Farm);
                    Core.EnsureAccept(6319);
                    Core.HuntMonster("drakonnan", "Living Fire", "Inferno Heart");
                    Core.EnsureComplete(6319);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("43757", "Floating Fire Orb Replica", "Mode: [select] only\nShould the bot buy \"Floating Fire Orb Replica\" ?", false),
        new Option<bool>("43760", "Dual Ice Katanas", "Mode: [select] only\nShould the bot buy \"Dual Ice Katanas\" ?", false),
        new Option<bool>("43777", "Cold as Ice Katana", "Mode: [select] only\nShould the bot buy \"Cold as Ice Katana\" ?", false),
        new Option<bool>("43685", "Enchanted Ice Katana", "Mode: [select] only\nShould the bot buy \"Enchanted Ice Katana\" ?", false),
        new Option<bool>("43761", "Dual Enchanted Ice Katanas", "Mode: [select] only\nShould the bot buy \"Dual Enchanted Ice Katanas\" ?", false),
        new Option<bool>("43778", "Ice Cold Katana", "Mode: [select] only\nShould the bot buy \"Ice Cold Katana\" ?", false),
        new Option<bool>("43768", "Lava Pillar I", "Mode: [select] only\nShould the bot buy \"Lava Pillar I\" ?", false),
        new Option<bool>("43769", "Lava Pillar II", "Mode: [select] only\nShould the bot buy \"Lava Pillar II\" ?", false),
        new Option<bool>("43770", "Lava Pillar III", "Mode: [select] only\nShould the bot buy \"Lava Pillar III\" ?", false),
        new Option<bool>("43771", "Lava Pillar IV", "Mode: [select] only\nShould the bot buy \"Lava Pillar IV\" ?", false),
        new Option<bool>("43772", "Lava Pool I", "Mode: [select] only\nShould the bot buy \"Lava Pool I\" ?", false),
        new Option<bool>("43773", "Lava Pool II", "Mode: [select] only\nShould the bot buy \"Lava Pool II\" ?", false),
        new Option<bool>("43774", "Large Lava Pool I", "Mode: [select] only\nShould the bot buy \"Large Lava Pool I\" ?", false),
        new Option<bool>("43775", "Large Lava Pool II", "Mode: [select] only\nShould the bot buy \"Large Lava Pool II\" ?", false),
        new Option<bool>("43776", "Vanilla Ice Katana", "Mode: [select] only\nShould the bot buy \"Vanilla Ice Katana\" ?", false),
        new Option<bool>("43579", "FlameDragon Knight", "Mode: [select] only\nShould the bot buy \"FlameDragon Knight\" ?", false),
        new Option<bool>("43580", "FlameDragon Knight's Helm", "Mode: [select] only\nShould the bot buy \"FlameDragon Knight's Helm\" ?", false),
        new Option<bool>("43581", "FlameDragon Knight's Blade", "Mode: [select] only\nShould the bot buy \"FlameDragon Knight's Blade\" ?", false),
        new Option<bool>("43582", "Wings of the Flame Dragon", "Mode: [select] only\nShould the bot buy \"Wings of the Flame Dragon\" ?", false),
    };
}
