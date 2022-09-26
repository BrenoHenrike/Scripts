//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/BlazeBeardStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BlazeBeardMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public BlazeBeard BlazeBeard = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Pirate Mage Token", "Explorer Pistol", "Blaze Gem", "Pirate Class Token", "Alpha Pirate Class Token " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("blazebeard", 108, findIngredients);

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

                case "Pirate Mage Token":
                    BlazeBeard.TokenQuests();
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(Core.IsMember ? 4531 : 4530);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        if (Core.IsMember)
                        {
                            //Undead Pirate Hordes 4531 [Member]
                            Core.HuntMonster("Blazebeard", "Pirate Crew", "Cursed Medallion");
                            Bot.Wait.ForPickup(req.Name);
                        }
                        else
                        {
                            //Pirate Caster Hunting 4530
                            Core.HuntMonster("ManaCannon", "Pirate Caster", "Pirate Caster Beaten", 10);
                            Core.HuntMonster("ManaCannon", "Pirate Caster", "Pirate Caster Research Clue ");
                            Bot.Wait.ForPickup(req.Name);
                        }
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Explorer Pistol":
                case "Blaze Gem":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("ManaCannon", "Blazebeard", req.Name, isTemp: false);
                    break;

                case "Pirate Class Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Core.CheckInventory("Classic Pirate"))
                    {
                        //Map Recovery 31
                        Core.AddDrop("Classic Pirate");
                        Core.EnsureAccept(31);
                        Core.HuntMonster("Pirates", "Fishwing", "Map Fragment", 5);
                        Core.EnsureComplete(31);
                        Adv.rankUpClass("Classic Pirate");
                    }
                    Core.RegisterQuests(4551);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        //New Pirate Class 4551
                        Core.HuntMonster("Blazebeard", "Undead Pirate", "Rusty Nail");
                    Core.CancelRegisteredQuests();
                    break;

                case "Alpha Pirate Class Token":
                    if (!Core.CheckInventory("Classic Alpha Pirate"))
                    {
                        Core.Logger($"\"{req.Name}\" requires you to have \"Classic Alpha Pirate\" which is rare. Skipping this item.");
                        return;
                    }
                    else
                    {
                        Core.FarmingLogger(req.Name, quant);
                        Core.EquipClass(ClassType.Farm);
                        Core.RegisterQuests(4552);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                            //New Alpha Pirate Class 4552
                            Core.HuntMonster("Blazebeard", "Undead Pirate", "Rusty Nail");
                        Core.CancelRegisteredQuests();
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("31158", "Crimson Pirate Mage", "Mode: [select] only\nShould the bot buy \"Crimson Pirate Mage\" ?", false),
        new Option<bool>("31159", "Crimson Pirate Mage Hood", "Mode: [select] only\nShould the bot buy \"Crimson Pirate Mage Hood\" ?", false),
        new Option<bool>("31160", "Crimson Pirate Mage Tricorn", "Mode: [select] only\nShould the bot buy \"Crimson Pirate Mage Tricorn\" ?", false),
        new Option<bool>("31161", "Crimson Pirate Mage Tricorn Locks", "Mode: [select] only\nShould the bot buy \"Crimson Pirate Mage Tricorn Locks\" ?", false),
        new Option<bool>("31162", "Crimson Pirate Mage Hat", "Mode: [select] only\nShould the bot buy \"Crimson Pirate Mage Hat\" ?", false),
        new Option<bool>("31164", "Crimson Pirate Mage Locks", "Mode: [select] only\nShould the bot buy \"Crimson Pirate Mage Locks\" ?", false),
        new Option<bool>("31165", "Crimson Pirate Mage Weapons", "Mode: [select] only\nShould the bot buy \"Crimson Pirate Mage Weapons\" ?", false),
        new Option<bool>("31166", "Pirate Mage Side Cutlass", "Mode: [select] only\nShould the bot buy \"Pirate Mage Side Cutlass\" ?", false),
        new Option<bool>("31167", "Pirate Mage Skull Staff", "Mode: [select] only\nShould the bot buy \"Pirate Mage Skull Staff\" ?", false),
        new Option<bool>("31168", "Pirate Mage Harpoon", "Mode: [select] only\nShould the bot buy \"Pirate Mage Harpoon\" ?", false),
        new Option<bool>("31169", "Pirate Mage Flintlock Staff", "Mode: [select] only\nShould the bot buy \"Pirate Mage Flintlock Staff\" ?", false),
        new Option<bool>("31170", "Pirate Mage Flintlock", "Mode: [select] only\nShould the bot buy \"Pirate Mage Flintlock\" ?", false),
        new Option<bool>("31171", "Pirate Mage Cutlass", "Mode: [select] only\nShould the bot buy \"Pirate Mage Cutlass\" ?", false),
        new Option<bool>("31172", "Pirate Mage SpellBook", "Mode: [select] only\nShould the bot buy \"Pirate Mage SpellBook\" ?", false),
        new Option<bool>("31178", "Dual Pirate Mage Flintlock", "Mode: [select] only\nShould the bot buy \"Dual Pirate Mage Flintlock\" ?", false),
        new Option<bool>("31179", "Dual Pirate Mage Cutlasses", "Mode: [select] only\nShould the bot buy \"Dual Pirate Mage Cutlasses\" ?", false),
        new Option<bool>("31180", "Pirate Mage Flintlock and Cutlass", "Mode: [select] only\nShould the bot buy \"Pirate Mage Flintlock and Cutlass\" ?", false),
        new Option<bool>("31177", "Alpha Ferret Pirate", "Mode: [select] only\nShould the bot buy \"Alpha Ferret Pirate\" ?", false),
        new Option<bool>("31224", "Golden Explorer Pistol", "Mode: [select] only\nShould the bot buy \"Golden Explorer Pistol\" ?", false),
        new Option<bool>("31225", "Great Golden Explorer Pistol", "Mode: [select] only\nShould the bot buy \"Great Golden Explorer Pistol\" ?", false),
        new Option<bool>("31227", "Dual Great Golden Pistols", "Mode: [select] only\nShould the bot buy \"Dual Great Golden Pistols\" ?", false),
        new Option<bool>("31176", "Pirate", "Mode: [select] only\nShould the bot buy \"Pirate\" ?", false),
        new Option<bool>("31281", "Alpha Pirate", "Mode: [select] only\nShould the bot buy \"Alpha Pirate\" ?", false),
        new Option<bool>("35079", "Platinum Pirate Mage", "Mode: [select] only\nShould the bot buy \"Platinum Pirate Mage\" ?", false),
        new Option<bool>("35081", "Platinum Pirate Mage Tricorn Locks", "Mode: [select] only\nShould the bot buy \"Platinum Pirate Mage Tricorn Locks\" ?", false),
        new Option<bool>("35080", "Platinum Pirate Mage Tricorn", "Mode: [select] only\nShould the bot buy \"Platinum Pirate Mage Tricorn\" ?", false),
    };
}
