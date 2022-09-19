//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DreadspaceReplicatorMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreSummer Dread = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Red Space Fabric", "Blue Space Fabric", "Yellow Space Fabric", "Star Scrap Metal", "Daimyo", "Antimatter dye", "Cyber Brain Core", "Blinding Light of Dread Space", "Unstable Isotope " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        Dread.DreadSpace(true);
        Adv.StartBuyAllMerge("dreadspace", 527, findIngredients);

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

                case "Red Space Fabric":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadspace", "Red Trobble", req.Name, quant, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Blue Space Fabric":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadspace", "Trobble", req.Name, quant, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Yellow Space Fabric":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadspace", "Troblor", req.Name, quant, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Scrap Metal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadspace", "Undead Space Marine|Undead Space Warrior", req.Name, quant, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Antimatter dye":
                case "Star Scrap Metal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(4289);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadspace", "Undead Space Marine|Undead Space Warrior", "Golden Spork of Justice");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Daimyo":
                    if (!Core.IsMember)
                    {
                        Core.Logger("Space Daimyo requires membership, unselect it to proceed.");
                        return;
                    }
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Adv.BuyItem("necropolis", 422, "Daimyo");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Cyber Brain Core":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadspace", "Dread Space", req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Unstable Isotope":
                case "Blinding Light of Dread Space":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(4294);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("dreadspace", "Dra'gorn", "Space Dragon Balls", 7, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("29982", "Red Space Crew Shirt", "Mode: [select] only\nShould the bot buy \"Red Space Crew Shirt\" ?", false),
        new Option<bool>("29986", "Blue Space Crew Shirt", "Mode: [select] only\nShould the bot buy \"Blue Space Crew Shirt\" ?", false),
        new Option<bool>("29984", "Yellow Space Crew Shirt", "Mode: [select] only\nShould the bot buy \"Yellow Space Crew Shirt\" ?", false),
        new Option<bool>("29731", "Space Daimyo", "Mode: [select] only\nShould the bot buy \"Space Daimyo\" ?", false),
        new Option<bool>("29985", "Green Space Crew Shirt", "Mode: [select] only\nShould the bot buy \"Green Space Crew Shirt\" ?", false),
        new Option<bool>("29988", "Pink Space Crew Shirt", "Mode: [select] only\nShould the bot buy \"Pink Space Crew Shirt\" ?", false),
        new Option<bool>("29987", "Purple Space Crew Shirt", "Mode: [select] only\nShould the bot buy \"Purple Space Crew Shirt\" ?", false),
        new Option<bool>("29990", "Black Space Crew Shirt", "Mode: [select] only\nShould the bot buy \"Black Space Crew Shirt\" ?", false),
        new Option<bool>("29989", "White Space Crew Shirt", "Mode: [select] only\nShould the bot buy \"White Space Crew Shirt\" ?", false),
        new Option<bool>("30000", "Black Space Paladin", "Mode: [select] only\nShould the bot buy \"Black Space Paladin\" ?", false),
        new Option<bool>("29999", "White Space Paladin", "Mode: [select] only\nShould the bot buy \"White Space Paladin\" ?", false),
        new Option<bool>("29992", "Red Space Paladin", "Mode: [select] only\nShould the bot buy \"Red Space Paladin\" ?", false),
        new Option<bool>("29993", "Orange Space Paladin", "Mode: [select] only\nShould the bot buy \"Orange Space Paladin\" ?", false),
        new Option<bool>("29994", "Yellow Space Paladin", "Mode: [select] only\nShould the bot buy \"Yellow Space Paladin\" ?", false),
        new Option<bool>("29995", "Green Space Paladin", "Mode: [select] only\nShould the bot buy \"Green Space Paladin\" ?", false),
        new Option<bool>("29997", "Purple Space Paladin", "Mode: [select] only\nShould the bot buy \"Purple Space Paladin\" ?", false),
        new Option<bool>("29996", "Blue Space Paladin", "Mode: [select] only\nShould the bot buy \"Blue Space Paladin\" ?", false),
        new Option<bool>("29998", "Pink Space Paladin", "Mode: [select] only\nShould the bot buy \"Pink Space Paladin\" ?", false),
        new Option<bool>("29933", "Starship House", "Mode: [select] only\nShould the bot buy \"Starship House\" ?", false),
        new Option<bool>("29932", "Space Station House", "Mode: [select] only\nShould the bot buy \"Space Station House\" ?", false),
        new Option<bool>("30031", "Blackhole Light of Dread Space", "Mode: [select] only\nShould the bot buy \"Blackhole Light of Dread Space\" ?", false),
        new Option<bool>("29983", "Orange Space Crew Shirt", "Mode: [select] only\nShould the bot buy \"Orange Space Crew Shirt\" ?", false),
    };
}
