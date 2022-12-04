//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/CelestialArena.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CelestialChampion
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CelestialArenaQuests CAQ = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Core.BankingBlackList.AddRange(new[] { "Champion Sash", "Lumin Badge" });
        Adv.BestGear(GearBoost.dmgAll);
        CAQ.Arena1to10();
        CAQ.Arena11to20();
        CAQ.Arena21to29();
        //Bot.Quests.UpdateQuest(6042);
        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("celestialarena", 1474, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                // Add how to get items here
                case "Champion Sash":
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    Core.EquipClass(ClassType.Solo);
                    while (!Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("celestialarenad", "Aranx", "Champion Sash", quant, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Lumin Badge":
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    Core.EquipClass(ClassType.Solo);
                    while (!Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("celestialarenac", "Undead Raxgore Construct", "Lumin Badge", quant, isTemp: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("41454", "Viltusial", "Mode: [select] only\nShould the bot buy \"Viltusial\" ?", false),
        new Option<bool>("41461", "Bow of Viltusial", "Mode: [select] only\nShould the bot buy \"Bow of Viltusial\" ?", false),
        new Option<bool>("41460", "Cape Of Viltusial", "Mode: [select] only\nShould the bot buy \"Cape Of Viltusial\" ?", false),
        new Option<bool>("41457", "Hair Of Viltusial", "Mode: [select] only\nShould the bot buy \"Hair Of Viltusial\" ?", false),
        new Option<bool>("41455", "Helm Of Viltusial", "Mode: [select] only\nShould the bot buy \"Helm Of Viltusial\" ?", false),
        new Option<bool>("41458", "Locks Of Viltusial", "Mode: [select] only\nShould the bot buy \"Locks Of Viltusial\" ?", false),
        new Option<bool>("41456", "Helm of Viltusial + Locks", "Mode: [select] only\nShould the bot buy \"Helm of Viltusial + Locks\" ?", false),
        new Option<bool>("41459", "Wings Of Viltusial", "Mode: [select] only\nShould the bot buy \"Wings Of Viltusial\" ?", false),
        new Option<bool>("41466", "Scythe Of Blessings", "Mode: [select] only\nShould the bot buy \"Scythe Of Blessings\" ?", false),
        new Option<bool>("41465", "Staff Of Blessings", "Mode: [select] only\nShould the bot buy \"Staff Of Blessings\" ?", false),
        new Option<bool>("41447", "Cursed Abezeth", "Mode: [select] only\nShould the bot buy \"Cursed Abezeth\" ?", false),
        new Option<bool>("41450", "Abezeth Cape", "Mode: [select] only\nShould the bot buy \"Abezeth Cape\" ?", false),
        new Option<bool>("41448", "Helm Of Abezeth", "Mode: [select] only\nShould the bot buy \"Helm Of Abezeth\" ?", false),
        new Option<bool>("41449", "Hood Of Abezeth", "Mode: [select] only\nShould the bot buy \"Hood Of Abezeth\" ?", false),
        new Option<bool>("41451", "Wings Of Abezeth", "Mode: [select] only\nShould the bot buy \"Wings Of Abezeth\" ?", false),
        new Option<bool>("41564", "Runes of Viltusial", "Mode: [select] only\nShould the bot buy \"Runes of Viltusial\" ?", false),
        new Option<bool>("41565", "Runed Cape of Viltusial", "Mode: [select] only\nShould the bot buy \"Runed Cape of Viltusial\" ?", false),
        new Option<bool>("50360", "Infernal Slayer", "Mode: [select] only\nShould the bot buy \"Infernal Slayer\" ?", false),
        new Option<bool>("50366", "Infernal Slayer's Wing", "Mode: [select] only\nShould the bot buy \"Infernal Slayer's Wing\" ?", false),
        new Option<bool>("50367", "Infernal Slayer's Wings", "Mode: [select] only\nShould the bot buy \"Infernal Slayer's Wings\" ?", false),
        new Option<bool>("50365", "Infernal Slayer's Rune + Wrap", "Mode: [select] only\nShould the bot buy \"Infernal Slayer's Rune + Wrap\" ?", false),
        new Option<bool>("50362", "Infernal Slayer's Halo", "Mode: [select] only\nShould the bot buy \"Infernal Slayer's Halo\" ?", false),
        new Option<bool>("50361", "Infernal Slayer's Halo + Locks", "Mode: [select] only\nShould the bot buy \"Infernal Slayer's Halo + Locks\" ?", false),
        new Option<bool>("50364", "Infernal Slayer's Hat", "Mode: [select] only\nShould the bot buy \"Infernal Slayer's Hat\" ?", false),
        new Option<bool>("50363", "Infernal Slayer's Hat + Locks", "Mode: [select] only\nShould the bot buy \"Infernal Slayer's Hat + Locks\" ?", false),
        new Option<bool>("50409", "Infernal Slayer's Armored Wing", "Mode: [select] only\nShould the bot buy \"Infernal Slayer's Armored Wing\" ?", false),
        new Option<bool>("50408", "Slayer's Wings", "Mode: [select] only\nShould the bot buy \"Slayer's Wings\" ?", false),
        new Option<bool>("41453", "Burning Blade Of Abezeth", "Mode: [select] only\nShould the bot buy \"Burning Blade Of Abezeth\" ?", false),
    };
}