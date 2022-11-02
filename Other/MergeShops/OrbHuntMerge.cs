//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/OrbHunt.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class OrbHuntMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public OrbHunt OH = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        OH.OrbHuntSaga();
        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("orbhunt", 2060, findIngredients);

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

                case "Ancient Astrolabe":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(8349);
                        Core.HuntMonster("orbhunt", "Chamat", "Chamat Defeated");
                        Core.HuntMonster("orbhunt", "Horothotep", "Horothotep Defeated");
                        Core.HuntMonster("orbhunt", "Kolyaban", "Kolyaban Defeated");
                        Core.HuntMonster("orbhunt", "Quetzal", "Quetzal Defeated");
                        Core.EnsureComplete(8349);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("64276", "Luminomancer", "Mode: [select] only\nShould the bot buy \"Luminomancer\" ?", false),
        new Option<bool>("64277", "Luminomancer's Helm", "Mode: [select] only\nShould the bot buy \"Luminomancer's Helm\" ?", false),
        new Option<bool>("64278", "Luminomancer's Rune Cape", "Mode: [select] only\nShould the bot buy \"Luminomancer's Rune Cape\" ?", false),
        new Option<bool>("64279", "Luminous Beam Blade", "Mode: [select] only\nShould the bot buy \"Luminous Beam Blade\" ?", false),
        new Option<bool>("64639", "Dual Luminous Beam Blades", "Mode: [select] only\nShould the bot buy \"Dual Luminous Beam Blades\" ?", false),
        new Option<bool>("64280", "Tenebral Warrior", "Mode: [select] only\nShould the bot buy \"Tenebral Warrior\" ?", false),
        new Option<bool>("64281", "Tenebral Warrior's Helm", "Mode: [select] only\nShould the bot buy \"Tenebral Warrior's Helm\" ?", false),
        new Option<bool>("64282", "Tenebral Warrior's Rune Cape", "Mode: [select] only\nShould the bot buy \"Tenebral Warrior's Rune Cape\" ?", false),
        new Option<bool>("64283", "Tenebral Beam Sword", "Mode: [select] only\nShould the bot buy \"Tenebral Beam Sword\" ?", false),
        new Option<bool>("64640", "Dual Tenebral Beam Swords", "Mode: [select] only\nShould the bot buy \"Dual Tenebral Beam Swords\" ?", false),
        new Option<bool>("64506", "Pyroclastic Mage", "Mode: [select] only\nShould the bot buy \"Pyroclastic Mage\" ?", false),
        new Option<bool>("64507", "Pyroclastic Mage Hair", "Mode: [select] only\nShould the bot buy \"Pyroclastic Mage Hair\" ?", false),
        new Option<bool>("64508", "Pyroclastic Mage Hood", "Mode: [select] only\nShould the bot buy \"Pyroclastic Mage Hood\" ?", false),
        new Option<bool>("64509", "Pyroclastic Guardian Cape", "Mode: [select] only\nShould the bot buy \"Pyroclastic Guardian Cape\" ?", false),
        new Option<bool>("64510", "Pyroclastic Mage Staff", "Mode: [select] only\nShould the bot buy \"Pyroclastic Mage Staff\" ?", false),
        new Option<bool>("64511", "Dual Pyroclastic Mage Staves", "Mode: [select] only\nShould the bot buy \"Dual Pyroclastic Mage Staves\" ?", false),
        new Option<bool>("64512", "Pyroclastic Blaze Hands", "Mode: [select] only\nShould the bot buy \"Pyroclastic Blaze Hands\" ?", false),
        new Option<bool>("64645", "Elegant Eternal Ice Outfit", "Mode: [select] only\nShould the bot buy \"Elegant Eternal Ice Outfit\" ?", false),
        new Option<bool>("64648", "Elegant Eternal Ice Crown", "Mode: [select] only\nShould the bot buy \"Elegant Eternal Ice Crown\" ?", false),
        new Option<bool>("64649", "Elegant Ice Crown + Locks", "Mode: [select] only\nShould the bot buy \"Elegant Ice Crown + Locks\" ?", false),
        new Option<bool>("64650", "Elegant Ice Veil + Locks", "Mode: [select] only\nShould the bot buy \"Elegant Ice Veil + Locks\" ?", false),
        new Option<bool>("64652", "The Cold Shoulder", "Mode: [select] only\nShould the bot buy \"The Cold Shoulder\" ?", false),
        new Option<bool>("64653", "Frozen Ring Wand", "Mode: [select] only\nShould the bot buy \"Frozen Ring Wand\" ?", false),
        new Option<bool>("64646", "Elegant Ice Cut", "Mode: [select] only\nShould the bot buy \"Elegant Ice Cut\" ?", false),
        new Option<bool>("64647", "Elegant Ice Locks", "Mode: [select] only\nShould the bot buy \"Elegant Ice Locks\" ?", false),
    };
}
