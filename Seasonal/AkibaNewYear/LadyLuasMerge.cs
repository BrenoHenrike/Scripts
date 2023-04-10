/*
name: Lady Lua's Merge
description: Farms the materials needed for Lady Lua's Merge.
tags: yokai, lady lua, seasonal, akibacny, akiba new year, merge shop
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/AkibaNewYear/LadyLua.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class LadyLuasMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private LadyLua LL = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Tidal Byakko Warrior", "Lua's Lucky Envelope", "Tidal Byakko Warrior Hair", "Tidal Byakko Warrior Locks", "Tidal Tiger Blissus' Fighting Stance", "Tidal Byakko Wakizashi", "Tidal Byakko Wakizashis", "Tidal Byakko Fan", "Tidal Byakko Fans", "Tidal Byakko's Grasps", "Tidal Byakko's Claws", "Lady Lua's Fan" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        LL.Storyline();
        Adv.StartBuyAllMerge("akibacny", 2108, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Tidal Byakko Warrior":
                case "Tidal Byakko Warrior Hair":
                case "Tidal Byakko Warrior Locks":
                case "Tidal Tiger Blissus' Fighting Stance":
                case "Tidal Byakko Wakizashi":
                case "Tidal Byakko Wakizashis":
                case "Tidal Byakko Fan":
                case "Tidal Byakko Fans":
                case "Tidal Byakko's Grasps":
                case "Tidal Byakko's Claws":
                case "Lady Lua's Fan":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("akibacny", "Umitora", req.Name, isTemp: false, log: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;


                case "Lua's Lucky Envelope":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8506);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("akibacny", "Umitora", req.Name, quant, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;




            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("67856", "Tidal Tiger Warrior", "Mode: [select] only\nShould the bot buy \"Tidal Tiger Warrior\" ?", false),
        new Option<bool>("67857", "Tidal Tiger Warrior's Hair", "Mode: [select] only\nShould the bot buy \"Tidal Tiger Warrior's Hair\" ?", false),
        new Option<bool>("67858", "Tidal Tiger Warrior's Locks", "Mode: [select] only\nShould the bot buy \"Tidal Tiger Warrior's Locks\" ?", false),
        new Option<bool>("67860", "Crouching Blissus, Hidden far0", "Mode: [select] only\nShould the bot buy \"Crouching Blissus, Hidden far0\" ?", false),
        new Option<bool>("67861", "Tidal Tiger far0", "Mode: [select] only\nShould the bot buy \"Tidal Tiger far0\" ?", false),
        new Option<bool>("67862", "Tidal Tiger Wakizashi", "Mode: [select] only\nShould the bot buy \"Tidal Tiger Wakizashi\" ?", false),
        new Option<bool>("67863", "Tidal Tiger Wakizashis", "Mode: [select] only\nShould the bot buy \"Tidal Tiger Wakizashis\" ?", false),
        new Option<bool>("67864", "Tidal Tiger's Fan", "Mode: [select] only\nShould the bot buy \"Tidal Tiger's Fan\" ?", false),
        new Option<bool>("67865", "Tidal Tiger's Fans", "Mode: [select] only\nShould the bot buy \"Tidal Tiger's Fans\" ?", false),
        new Option<bool>("67866", "Grasps of the Tiger", "Mode: [select] only\nShould the bot buy \"Grasps of the Tiger\" ?", false),
        new Option<bool>("67867", "Tidal Tiger's Claws", "Mode: [select] only\nShould the bot buy \"Tidal Tiger's Claws\" ?", false),
        new Option<bool>("67868", "Tidal Warrior of Prosperity", "Mode: [select] only\nShould the bot buy \"Tidal Warrior of Prosperity\" ?", false),
        new Option<bool>("67869", "Tidal Warrior of Prosperity's Locks", "Mode: [select] only\nShould the bot buy \"Tidal Warrior of Prosperity's Locks\" ?", false),
        new Option<bool>("67870", "Tidal Warrior of Prosperity Wakizashi", "Mode: [select] only\nShould the bot buy \"Tidal Warrior of Prosperity Wakizashi\" ?", false),
        new Option<bool>("67871", "Tidal Warrior of Prosperity Wakizashis", "Mode: [select] only\nShould the bot buy \"Tidal Warrior of Prosperity Wakizashis\" ?", false),
        new Option<bool>("67872", "Tidal Warrior of Prosperity's Fan", "Mode: [select] only\nShould the bot buy \"Tidal Warrior of Prosperity's Fan\" ?", false),
        new Option<bool>("67873", "Tidal Warrior of Prosperity's Fans", "Mode: [select] only\nShould the bot buy \"Tidal Warrior of Prosperity's Fans\" ?", false),
        new Option<bool>("67874", "Tidal Warrior of Prosperity's Claws", "Mode: [select] only\nShould the bot buy \"Tidal Warrior of Prosperity's Claws\" ?", false),
        new Option<bool>("67884", "Grey Tidal Tiger Warrior", "Mode: [select] only\nShould the bot buy \"Grey Tidal Tiger Warrior\" ?", false),
        new Option<bool>("67885", "Grey Tidal Tiger Warrior's Hair", "Mode: [select] only\nShould the bot buy \"Grey Tidal Tiger Warrior's Hair\" ?", false),
        new Option<bool>("67886", "Grey Tidal Tiger Warrior's Locks", "Mode: [select] only\nShould the bot buy \"Grey Tidal Tiger Warrior's Locks\" ?", false),
        new Option<bool>("67887", "Grey Tidal Tiger Warrior Wakizashi", "Mode: [select] only\nShould the bot buy \"Grey Tidal Tiger Warrior Wakizashi\" ?", false),
        new Option<bool>("67888", "Grey Tidal Tiger Warrior Wakizashis", "Mode: [select] only\nShould the bot buy \"Grey Tidal Tiger Warrior Wakizashis\" ?", false),
        new Option<bool>("67889", "Grey Tidal Tiger Warrior's Fan", "Mode: [select] only\nShould the bot buy \"Grey Tidal Tiger Warrior's Fan\" ?", false),
        new Option<bool>("67890", "Grey Tidal Tiger Warrior's Fans", "Mode: [select] only\nShould the bot buy \"Grey Tidal Tiger Warrior's Fans\" ?", false),
        new Option<bool>("67891", "Grasps of the Grey Tiger", "Mode: [select] only\nShould the bot buy \"Grasps of the Grey Tiger\" ?", false),
        new Option<bool>("67892", "Grey Tidal Tiger Warrior's Claws", "Mode: [select] only\nShould the bot buy \"Grey Tidal Tiger Warrior's Claws\" ?", false),
        new Option<bool>("67917", "Lady Lua's Fans", "Mode: [select] only\nShould the bot buy \"Lady Lua's Fans\" ?", false),
        new Option<bool>("67911", "Regal Lady Lua", "Mode: [select] only\nShould the bot buy \"Regal Lady Lua\" ?", false),
        new Option<bool>("67912", "Lady Lua", "Mode: [select] only\nShould the bot buy \"Lady Lua\" ?", false),
        new Option<bool>("67913", "Lady Lua's Morph", "Mode: [select] only\nShould the bot buy \"Lady Lua's Morph\" ?", false),
        new Option<bool>("67914", "Lady Lua's Obi", "Mode: [select] only\nShould the bot buy \"Lady Lua's Obi\" ?", false),
        new Option<bool>("67915", "Lady Lua's Cloak", "Mode: [select] only\nShould the bot buy \"Lady Lua's Cloak\" ?", false),
    };
}
