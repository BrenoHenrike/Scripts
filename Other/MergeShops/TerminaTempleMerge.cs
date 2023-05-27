/*
name: Termina Temple Merge
description: This bot will farm the items belonging to the selected mode for the Termina Temple Merge [2259] in /terminatemple
tags: termina, temple, merge, terminatemple, dragonlord, good, golden, dragonlords, royal, wrap, evil, tyrannical, doomblade, chaos, chaorrupted, dragonblade, battle, cleric, dragon, clerics, bright, cloak, accoutrements
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Good\BLoD\CoreBLOD.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class TerminaTempleMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CoreBLOD BLOD = new();
    private CoreNSOD NSOD = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Termina Sigil", "Bright Aura", "Void Aura", "Trace of Chaos", "DragonGuard Badge", "Battle Cleric's Draconic Spear", "Bright Dragon Shield" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("terminatemple", 2259, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Bright Dragon Shield":
                case "Battle Cleric's Draconic Spear":
                case "DragonGuard Badge":
                case "Termina Sigil":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9215);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("terminatemple", "Termina Defender", "Defender Sparred With", 8);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Bright Aura":
                    BLOD.BrightAura(quant);
                    break;

                case "Void Aura":
                    NSOD.VoidAuras(quant);
                    break;

                case "Trace of Chaos":
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ultradrakath", "Champion of Chaos", isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("77403", "Dragonlord of Good", "Mode: [select] only\nShould the bot buy \"Dragonlord of Good\" ?", false),
        new Option<bool>("77404", "Golden Dragonlord's Helmet", "Mode: [select] only\nShould the bot buy \"Golden Dragonlord's Helmet\" ?", false),
        new Option<bool>("77405", "Royal Dragonlord's Wrap", "Mode: [select] only\nShould the bot buy \"Royal Dragonlord's Wrap\" ?", false),
        new Option<bool>("77406", "Golden Dragonlord's Blade", "Mode: [select] only\nShould the bot buy \"Golden Dragonlord's Blade\" ?", false),
        new Option<bool>("77407", "Dragonlord of Evil", "Mode: [select] only\nShould the bot buy \"Dragonlord of Evil\" ?", false),
        new Option<bool>("77408", "Tyrannical Dragonlord's Helmet", "Mode: [select] only\nShould the bot buy \"Tyrannical Dragonlord's Helmet\" ?", false),
        new Option<bool>("77409", "Tyrannical Dragonlord's Wrap", "Mode: [select] only\nShould the bot buy \"Tyrannical Dragonlord's Wrap\" ?", false),
        new Option<bool>("77410", "Tyrannical Dragonlord's Doomblade", "Mode: [select] only\nShould the bot buy \"Tyrannical Dragonlord's Doomblade\" ?", false),
        new Option<bool>("77411", "Dragonlord of Chaos", "Mode: [select] only\nShould the bot buy \"Dragonlord of Chaos\" ?", false),
        new Option<bool>("77412", "Chaorrupted Dragonlord's Helmet", "Mode: [select] only\nShould the bot buy \"Chaorrupted Dragonlord's Helmet\" ?", false),
        new Option<bool>("77413", "Chaorrupted Dragonlord's Wrap", "Mode: [select] only\nShould the bot buy \"Chaorrupted Dragonlord's Wrap\" ?", false),
        new Option<bool>("77414", "Chaorrupted Dragonblade", "Mode: [select] only\nShould the bot buy \"Chaorrupted Dragonblade\" ?", false),
        new Option<bool>("77668", "Battle Cleric of the Dragon", "Mode: [select] only\nShould the bot buy \"Battle Cleric of the Dragon\" ?", false),
        new Option<bool>("77669", "Battle Cleric's Helm", "Mode: [select] only\nShould the bot buy \"Battle Cleric's Helm\" ?", false),
        new Option<bool>("77670", "Battle Cleric's Bright Cloak", "Mode: [select] only\nShould the bot buy \"Battle Cleric's Bright Cloak\" ?", false),
        new Option<bool>("77673", "Battle Cleric's Bright Accoutrements", "Mode: [select] only\nShould the bot buy \"Battle Cleric's Bright Accoutrements\" ?", false),
    };
}
