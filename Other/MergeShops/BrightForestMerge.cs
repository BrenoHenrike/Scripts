//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfChaos/CoreSoC.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BrightForestMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreSoC SoC = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "ShadowFlame Armor Scrap " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("brightforest", 1911, findIngredients);

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

                case "ShadowFlame Armor Scrap":
                    SoC.CompleteCoreSoC();
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(7768);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //The Shadows Recede 7768
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("BrightForest", "Shadowflame Scout", "ShadowFlame Troops \"Informed\"", 30);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("BrightForest", "ShadowFlame Dragon", "ShadowFlame Dragon \"Informed\"", 30);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("56862", "ShadowFlame Empress", "Mode: [select] only\nShould the bot buy \"ShadowFlame Empress\" ?", false),
        new Option<bool>("56863", "Shadowed Empress", "Mode: [select] only\nShould the bot buy \"Shadowed Empress\" ?", false),
        new Option<bool>("56864", "ShadowFlame Empress Hair", "Mode: [select] only\nShould the bot buy \"ShadowFlame Empress Hair\" ?", false),
        new Option<bool>("56865", "ShadowFlame Empress Hair + Morph", "Mode: [select] only\nShould the bot buy \"ShadowFlame Empress Hair + Morph\" ?", false),
        new Option<bool>("56866", "ShadowFlame Empress Locks", "Mode: [select] only\nShould the bot buy \"ShadowFlame Empress Locks\" ?", false),
        new Option<bool>("56867", "ShadowFlame Empress Locks + Morph", "Mode: [select] only\nShould the bot buy \"ShadowFlame Empress Locks + Morph\" ?", false),
        new Option<bool>("56868", "ShadowFlame Empress Cape", "Mode: [select] only\nShould the bot buy \"ShadowFlame Empress Cape\" ?", false),
        new Option<bool>("56869", "ShadowCharged Empress Blade", "Mode: [select] only\nShould the bot buy \"ShadowCharged Empress Blade\" ?", false),
        new Option<bool>("56870", "ShadowFlame Empress Blade", "Mode: [select] only\nShould the bot buy \"ShadowFlame Empress Blade\" ?", false),
        new Option<bool>("56967", "ShadowFlame DoomLight", "Mode: [select] only\nShould the bot buy \"ShadowFlame DoomLight\" ?", false),
        new Option<bool>("56968", "ShadowFlame DoomLight Helm", "Mode: [select] only\nShould the bot buy \"ShadowFlame DoomLight Helm\" ?", false),
        new Option<bool>("56969", "ShadowFlame DoomLight Cloak", "Mode: [select] only\nShould the bot buy \"ShadowFlame DoomLight Cloak\" ?", false),
        new Option<bool>("56970", "ShadowFlame DoomLight Blade", "Mode: [select] only\nShould the bot buy \"ShadowFlame DoomLight Blade\" ?", false),
    };
}
