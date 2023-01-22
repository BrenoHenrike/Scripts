/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/DeerHunt.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DeerHuntMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public DeerHunt DeerHunt = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Icy Pelt", "WinterWild Axe", "Old Moglin Teddy Mace " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        DeerHunt.StoryLine();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("deerhunt", 2077, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Icy Pelt":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8433);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("deerhunt", "Scared Wolf", "Wolf Warded", 9);
                        Core.HuntMonster("deerhunt", "Deer?", "Deer Deterred", 3);
                        Core.HuntMonster("deerhunt", "Frightened Owl", "Owl Ousted", 6);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "WinterWild Axe":
                case "Old Moglin Teddy Mace":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("deerhunt", "r8", "left", "Zweinichthirsch");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("49776", "WinterWild Warrior", "Mode: [select] only\nShould the bot buy \"WinterWild Warrior\" ?", false),
        new Option<bool>("49777", "WinterWild Locks", "Mode: [select] only\nShould the bot buy \"WinterWild Locks\" ?", false),
        new Option<bool>("49778", "WinterWild Beard", "Mode: [select] only\nShould the bot buy \"WinterWild Beard\" ?", false),
        new Option<bool>("49779", "WinterWild Hood", "Mode: [select] only\nShould the bot buy \"WinterWild Hood\" ?", false),
        new Option<bool>("49780", "WinterWild Hood + Beard", "Mode: [select] only\nShould the bot buy \"WinterWild Hood + Beard\" ?", false),
        new Option<bool>("49781", "WinterWild Warrior Back Axes", "Mode: [select] only\nShould the bot buy \"WinterWild Warrior Back Axes\" ?", false),
        new Option<bool>("49784", "Dual WinterWild Axes", "Mode: [select] only\nShould the bot buy \"Dual WinterWild Axes\" ?", false),
        new Option<bool>("49785", "WinterWild Dagger", "Mode: [select] only\nShould the bot buy \"WinterWild Dagger\" ?", false),
        new Option<bool>("66271", "Moglin Teddy Mace", "Mode: [select] only\nShould the bot buy \"Moglin Teddy Mace\" ?", false),
    };
}
