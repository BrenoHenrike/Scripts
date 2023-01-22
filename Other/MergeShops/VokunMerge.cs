/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class VokunMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Unitas Fragment" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("underworld", 980, findIngredients);

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

                case "Unitas Fragment":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Core.IsMember)
                    {
                        Core.RegisterQuests(3760);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("battleundera", "Undead Berserker", "Warrior Claymore Blade", isTemp: false, log: false);
                            Core.HuntMonster("maul", "SlimeSkull", "Dark Crown Axe", isTemp: false, log: false);
                            Farm.BattleUnderB("Undead Energy", 50);
                            Bot.Wait.ForPickup(req.Name);
                        }
                    }
                    else
                    {
                        Core.RegisterQuests(3763);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("graveyard", "Big Jack Sprat", "Bone Axe", isTemp: false, log: false);
                            if (Core.HeroAlignment != 2)
                                Core.ChangeAlignment(Alignment.Evil);
                            Core.BuyItem("shadowfall", 47, "Helm of the Dark Lord");
                            Bot.Wait.ForPickup(req.Name);
                        }
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("25408", "Skuller of Vokun", "Mode: [select] only\nShould the bot buy \"Skuller of Vokun\" ?", false),
        new Option<bool>("25409", "Skuller of Vokun Head", "Mode: [select] only\nShould the bot buy \"Skuller of Vokun Head\" ?", false),
        new Option<bool>("25410", "GateKeeper of Vokun", "Mode: [select] only\nShould the bot buy \"GateKeeper of Vokun\" ?", false),
        new Option<bool>("25411", "GateKeeper Skull of Vokun", "Mode: [select] only\nShould the bot buy \"GateKeeper Skull of Vokun\" ?", false),
        new Option<bool>("25412", "GateKeeper of Vokun Cape", "Mode: [select] only\nShould the bot buy \"GateKeeper of Vokun Cape\" ?", false),
        new Option<bool>("25413", "Beast Of Vokun", "Mode: [select] only\nShould the bot buy \"Beast Of Vokun\" ?", false),
        new Option<bool>("25414", "BeastWings of Vokun", "Mode: [select] only\nShould the bot buy \"BeastWings of Vokun\" ?", false),
        new Option<bool>("25415", "BeastHorns of Vokun", "Mode: [select] only\nShould the bot buy \"BeastHorns of Vokun\" ?", false),
        new Option<bool>("25416", "Beast of Vokun Polearm", "Mode: [select] only\nShould the bot buy \"Beast of Vokun Polearm\" ?", false),
        new Option<bool>("25423", "Assassin of Vokun", "Mode: [select] only\nShould the bot buy \"Assassin of Vokun\" ?", false),
        new Option<bool>("25424", "Assassin Helm of Vokun", "Mode: [select] only\nShould the bot buy \"Assassin Helm of Vokun\" ?", false),
        new Option<bool>("25425", "Katana of Vokun", "Mode: [select] only\nShould the bot buy \"Katana of Vokun\" ?", false),
        new Option<bool>("25430", "BloodBlade of Vokun", "Mode: [select] only\nShould the bot buy \"BloodBlade of Vokun\" ?", false),
    };
}
