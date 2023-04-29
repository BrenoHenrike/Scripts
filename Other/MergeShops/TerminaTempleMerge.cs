/*
name: null
description: null
tags: null
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
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class TerminaTempleMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
public CoreBLOD BLOD = new();
public CoreNSOD NSOD = new();


    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Termina Sigil", "Bright Aura", "Void Aura", "Trace of Chaos"});
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
    };
}
