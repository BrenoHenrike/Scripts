/*
name: Twilight Zone Merge
description: This bot will farm the items belonging to the selected mode for the Twilight Zone Merge [2293] in /twilightzone
tags: twilight, zone, merge, twilightzone, evacuation, protocol, suit, emergency, scuba, drill, , beard, sea, base, flotation, device, hose, ring, rings, mindflayers, infested, bow, arrow, devouring, seas, eldritch, dirk
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/AgeOfRuin/CoreAOR.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;
public class TwilightZoneMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreAOR AoR = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Undine Visitor Badge", "Sundered Tentacle", "Leviathan Scale"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        AoR.TwilightZone();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("twilightzone", 2293, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Undine Visitor Badge":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("sunlightzone", "Astravian Illusion", req.Name, quant, false, false);
                        Bot.Wait.ForPickup(req.Name);
                    break;

                case "Sundered Tentacle":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9269);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("twilightzone", "Leviathan", "Leviathan Tentacle", 1, true, false);
                        Core.HuntMonster("twilightzone", "Decay Spirit", "Decay Essence", 8, true, false);
                        Core.HuntMonster("twilightzone", "Ice Guardian", "Tarnished Icicle", 8, true, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Leviathan Scale":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.Logger("Better to use alts to farm it faster.");
                        Core.HuntMonster("twilightzone", "Leviathan", "Leviathan Scale", quant, false, false);
                        Bot.Wait.ForPickup(req.Name);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("77092", "Evacuation Protocol Suit", "Mode: [select] only\nShould the bot buy \"Evacuation Protocol Suit\" ?", false),
        new Option<bool>("77093", "Emergency Scuba Hood", "Mode: [select] only\nShould the bot buy \"Emergency Scuba Hood\" ?", false),
        new Option<bool>("77094", "Evacuation Drill Hair + Beard", "Mode: [select] only\nShould the bot buy \"Evacuation Drill Hair + Beard\" ?", false),
        new Option<bool>("77095", "Evacuation Drill Locks", "Mode: [select] only\nShould the bot buy \"Evacuation Drill Locks\" ?", false),
        new Option<bool>("77097", "Sea Base Flotation Device", "Mode: [select] only\nShould the bot buy \"Sea Base Flotation Device\" ?", false),
        new Option<bool>("77101", "Sea Base Emergency Hose", "Mode: [select] only\nShould the bot buy \"Sea Base Emergency Hose\" ?", false),
        new Option<bool>("77102", "Sea Base Flotation Ring", "Mode: [select] only\nShould the bot buy \"Sea Base Flotation Ring\" ?", false),
        new Option<bool>("77103", "Sea Base Flotation Rings", "Mode: [select] only\nShould the bot buy \"Sea Base Flotation Rings\" ?", false),
        new Option<bool>("78056", "Mindflayer's Infested Bow and Arrow", "Mode: [select] only\nShould the bot buy \"Mindflayer's Infested Bow and Arrow\" ?", false),
        new Option<bool>("78051", "Devouring Sea's Blade", "Mode: [select] only\nShould the bot buy \"Devouring Sea's Blade\" ?", false),
        new Option<bool>("78052", "Devouring Sea's Blades", "Mode: [select] only\nShould the bot buy \"Devouring Sea's Blades\" ?", false),
        new Option<bool>("78053", "Eldritch Twilight Dirk", "Mode: [select] only\nShould the bot buy \"Eldritch Twilight Dirk\" ?", false),
        new Option<bool>("78054", "Eldritch Twilight Daggers", "Mode: [select] only\nShould the bot buy \"Eldritch Twilight Daggers\" ?", false),
    };
}
