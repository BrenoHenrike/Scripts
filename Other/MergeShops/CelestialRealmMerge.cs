/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CelestialRealmMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreQOM QOM => new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Infernal Token", "Uncorrupt Spear Feather", "Diabolical Minion's Seed " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("celestialrealm", 132, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Infernal Token":
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Celestialrealm", "Fallen Knight", req.Name, quant, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Uncorrupt Spear Feather":
                    QOM.CompleteEverything();
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(4508);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //Defeat the Diabolical Warlord! 4508
                        Core.HuntMonster("lostruinswar", "Diabolical Warlord", "Diabolical Warlord Defeated!");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Diabolical Minion's Seed":
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("lostruinswar", "Diabolical Warlord", req.Name, quant, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("30942", "Knight of the Fallen", "Mode: [select] only\nShould the bot buy \"Knight of the Fallen\" ?", false),
        new Option<bool>("30943", "Impure Wings", "Mode: [select] only\nShould the bot buy \"Impure Wings\" ?", false),
        new Option<bool>("30944", "Vile Knight Cape", "Mode: [select] only\nShould the bot buy \"Vile Knight Cape\" ?", false),
        new Option<bool>("30947", "Fiery Infernal Helm", "Mode: [select] only\nShould the bot buy \"Fiery Infernal Helm\" ?", false),
        new Option<bool>("30946", "Knight of the Fallen Helm", "Mode: [select] only\nShould the bot buy \"Knight of the Fallen Helm\" ?", false),
        new Option<bool>("30948", "Infernal Helm", "Mode: [select] only\nShould the bot buy \"Infernal Helm\" ?", false),
        new Option<bool>("30949", "Spear of the Fallen", "Mode: [select] only\nShould the bot buy \"Spear of the Fallen\" ?", false),
        new Option<bool>("30950", "Celestial Knight", "Mode: [select] only\nShould the bot buy \"Celestial Knight\" ?", false),
        new Option<bool>("30951", "Celestial Knight Helm", "Mode: [select] only\nShould the bot buy \"Celestial Knight Helm\" ?", false),
        new Option<bool>("30952", "Uncorrupt Celestial Spear", "Mode: [select] only\nShould the bot buy \"Uncorrupt Celestial Spear\" ?", false),
        new Option<bool>("31018", "Exalted Golden Guardian", "Mode: [select] only\nShould the bot buy \"Exalted Golden Guardian\" ?", false),
        // new Option<bool>("13275", "Celestial Amadis", "Mode: [select] only\nShould the bot buy \"Celestial Amadis\" ?", false),
        new Option<bool>("31039", "Celestial Double Wings", "Mode: [select] only\nShould the bot buy \"Celestial Double Wings\" ?", false),
        new Option<bool>("31041", "Prismatic Austere Wings", "Mode: [select] only\nShould the bot buy \"Prismatic Austere Wings\" ?", false),
        new Option<bool>("30264", "Avatar Of Life", "Mode: [select] only\nShould the bot buy \"Avatar Of Life\" ?", false),
        new Option<bool>("31020", "Devout Celestial Hood", "Mode: [select] only\nShould the bot buy \"Devout Celestial Hood\" ?", false),
        new Option<bool>("31021", "Avatar Of Life Hood", "Mode: [select] only\nShould the bot buy \"Avatar Of Life Hood\" ?", false),
        new Option<bool>("31022", "Sanctified Wings of Life", "Mode: [select] only\nShould the bot buy \"Sanctified Wings of Life\" ?", false),
        new Option<bool>("31023", "Radiant Hood of Life", "Mode: [select] only\nShould the bot buy \"Radiant Hood of Life\" ?", false),
        new Option<bool>("31024", "Prismatic Wings of Life", "Mode: [select] only\nShould the bot buy \"Prismatic Wings of Life\" ?", false),
        new Option<bool>("31025", "Gilded Prismatic Life Wings", "Mode: [select] only\nShould the bot buy \"Gilded Prismatic Life Wings\" ?", false),
        new Option<bool>("31043", "Avatar Of Life Aura Wings", "Mode: [select] only\nShould the bot buy \"Avatar Of Life Aura Wings\" ?", false),
        new Option<bool>("13276", "Celestial Amadis Helm", "Mode: [select] only\nShould the bot buy \"Celestial Amadis Helm\" ?", false),
        new Option<bool>("31080", "Avatar Of Life", "Mode: [select] only\nShould the bot buy \"Avatar Of Life\" ?", false),
        // new Option<bool>("31083", "Celestial Amadis", "Mode: [select] only\nShould the bot buy \"Celestial Amadis\" ?", false),
        new Option<bool>("31084", "Celestial Amadis Helm", "Mode: [select] only\nShould the bot buy \"Celestial Amadis Helm\" ?", false),
        new Option<bool>("36438", "Celestial Defender", "Mode: [select] only\nShould the bot buy \"Celestial Defender\" ?", false),
        new Option<bool>("36439", "Celestial Defender Helm", "Mode: [select] only\nShould the bot buy \"Celestial Defender Helm\" ?", false),
        new Option<bool>("36440", "Celestial Accoutrements", "Mode: [select] only\nShould the bot buy \"Celestial Accoutrements\" ?", false),
        new Option<bool>("36475", "High Celestial Priest", "Mode: [select] only\nShould the bot buy \"High Celestial Priest\" ?", false),
        new Option<bool>("36476", "High Celestial Staff", "Mode: [select] only\nShould the bot buy \"High Celestial Staff\" ?", false),
        new Option<bool>("36477", "Haloed Hood of Light", "Mode: [select] only\nShould the bot buy \"Haloed Hood of Light\" ?", false),
        new Option<bool>("36478", "Celestial Faceplate", "Mode: [select] only\nShould the bot buy \"Celestial Faceplate\" ?", false),
        new Option<bool>("36482", "Celestial Defender Mace", "Mode: [select] only\nShould the bot buy \"Celestial Defender Mace\" ?", false),
        new Option<bool>("36479", "Celestial Wings of Guiding", "Mode: [select] only\nShould the bot buy \"Celestial Wings of Guiding\" ?", false),
    };
}
