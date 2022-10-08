//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/IsleOfFotia/CoreIsleOfFotia.cs
//cs_include Scripts/Legion/CoreLegion.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CharonsPurgeMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public CoreIsleOfFotia CoreIsleOfFotia = new();
    public CoreLegion Legion = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Molten Sword", "Magitech Plating", "Ancient Undead Helm", "Ash Priest Hood", "The Scythe of Lost Hope", "Priest of the Ashes", "Legion Beast Within", "Psyche", "Molten Staff " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        CoreIsleOfFotia.CompleteALL();
        Adv.AltFarmItems.Add("Psyche");

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("styx", 670, findIngredients);

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

                case "Molten Sword":
                case "Ash Priest Hood":
                case "Priest of the Ashes":
                    Adv.BuyItem("Fotia", 649, req.Name);
                    break;

                case "Magitech Plating":
                    Adv.BuyItem("UnderRealm", 660, req.Name);
                    break;

                case "Ancient Undead Helm":
                    Legion.JoinLegion();
                    Adv.BuyItem("RavenScar", 615, req.Name);
                    break;

                case "The Scythe of Lost Hope":
                    Core.FarmingLogger(req.Name, quant);
                    Legion.ApprovalAndFavor(0, 50);
                    Adv.BuyItem("UnderWorld", 454, req.Name);
                    break;

                case "Legion Beast Within":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);

                    Legion.ObsidianRock(60);
                    Legion.SoulForgeHammer();

                    //Soul Forgery 2743
                    Core.RegisterQuests(2743);
                    while (!Core.CheckInventory("Solidified Soul", 50))
                    {
                        Core.HuntMonster("ShadowFallInvasion", "Bone Creeper", "Shards of a Soul", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();

                    Legion.FarmLegionToken(50);
                    Adv.BuyItem("underworld", 577, req.Name);
                    break;

                case "Golden Bough":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);

                    Core.EnsureAccept(3010);
                    Core.HuntMonster("UnderRealm", "Underworld Soul", "Souls Released", 8);
                    Core.EnsureComplete(3010);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Molten Staff":
                    Core.HuntMonster("Fotia", "Fotia Elemental", req.Name, isTemp: false);
                    break;

                case "Psyche":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);

                    //Judged on Allegiance to Dage 3041
                    Core.RegisterQuests(3041);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Judgement", "Aeacus", "Aeacus' Permission");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("17976", "Burning Freeze", "Mode: [select] only\nShould the bot buy \"Burning Freeze\" ?", false),
        new Option<bool>("17977", "Glacier Cudgel", "Mode: [select] only\nShould the bot buy \"Glacier Cudgel\" ?", false),
        new Option<bool>("18413", "Cerberus", "Mode: [select] only\nShould the bot buy \"Cerberus\" ?", false),
        new Option<bool>("18445", "Dark Ferryman's Hood", "Mode: [select] only\nShould the bot buy \"Dark Ferryman's Hood\" ?", false),
        new Option<bool>("18446", "Charon's Skull Mask", "Mode: [select] only\nShould the bot buy \"Charon's Skull Mask\" ?", false),
        new Option<bool>("18414", "Cerberus Helm", "Mode: [select] only\nShould the bot buy \"Cerberus Helm\" ?", false),
        new Option<bool>("18415", "Cerberus Morph Helm", "Mode: [select] only\nShould the bot buy \"Cerberus Morph Helm\" ?", false),
        new Option<bool>("18447", "Dual Cerberus Axes", "Mode: [select] only\nShould the bot buy \"Dual Cerberus Axes\" ?", false),
        new Option<bool>("18461", "Charon's Oar", "Mode: [select] only\nShould the bot buy \"Charon's Oar\" ?", false),
        new Option<bool>("18444", "Dage's Ferryman", "Mode: [select] only\nShould the bot buy \"Dage's Ferryman\" ?", false),
        new Option<bool>("17978", "Inferno Bow", "Mode: [select] only\nShould the bot buy \"Inferno Bow\" ?", false),
        new Option<bool>("18417", "Cerberus Axe", "Mode: [select] only\nShould the bot buy \"Cerberus Axe\" ?", false),
        new Option<bool>("18504", "Charon", "Mode: [select] only\nShould the bot buy \"Charon\" ?", false),
        new Option<bool>("18505", "Boatman's Hood", "Mode: [select] only\nShould the bot buy \"Boatman's Hood\" ?", false),
    };
}
