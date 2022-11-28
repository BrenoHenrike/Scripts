//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DoomLegacyMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public CoreDoomwood DWp3 = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("stonewood", 1899, findIngredients);

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

                case "Shadowscythe Trooper":
                case "ShadowScythe Trooper's Helm":
                case "ShadowScythe Trooper's Cape":
                case "ShadowScythe Blade":
                    Core.KillMonster("thorngarde", "Enter", "Spawn", 4541, req.Name, isTemp: false);
                    break;

                case "Salvaged Deadtech Node":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.RegisterQuests(7601);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("thorngarde", "CryptHacker|NecroDrone", "Deadtech Power Core", 7);
                        Core.HuntMonster("thorngarde", "CryptHacker", "CryptHacker Circuitry", 15);
                        Core.HuntMonster("thorngarde", "NecroMech", "NecroMech Targeting Systems", 5);

                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("thorngarde", "Zyrus the BioKnight", "BioKnight Engine", 3);

                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "ShadowScythe Rogue":
                case "ShadowScythe Rogue's Helm":
                case "ShadowScythe Rogue's Cape":
                case "ShadowScythe Reversed Daggers":
                case "ShadowScythe Daggers":
                    Core.KillMonster("thorngarde", "r2", "Left", 4541, req.Name, isTemp: false);
                    break;

                case "ShadowScythe Mage":
                case "ShadowScythe Mage's Hat":
                case "ShadowScythe Mage's Hat + Locks":
                case "ShadowScythe Mage's Rune":
                case "ShadowScythe Staff":
                    Core.KillMonster("thorngarde", "r2", "Left", 4542, req.Name, isTemp: false);
                    break;

                case "Zealous Paladin":
                case "Zealous Veil":
                case "Zealous Cherubs":
                case "Cryptborg":
                case "Cryptborg Wrap":
                case "Cryptborg Torpedo":
                case "Cryptborg Blade":
                case "Cryptborg Helm":
                    Core.HuntMonster("techdungeon", "Kalron the Cryptborg", req.Name, isTemp: false);
                    break;

                case "Zealous Badge":
                    Core.FarmingLogger($"{req.Name}", quant);
                    DWp3.DoomwoodPart3();
                    Core.RegisterQuests(7616);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("techdungeon", "Kalron the Cryptborg", "Immutable Dedication", 7);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("techdungeon", "DoomBorg Guard", "Paladin Armor Scraps", 30);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Zealous Rays Of Light":
                case "Zealous Censer":
                    Core.HuntMonster("stonewood", "BioKnight", req.Name, isTemp: false);
                    break;

                case "Deadtech Booster":
                    Core.HuntMonster("stonewood", "Doomwood Treeant", req.Name, quant, isTemp: false);
                    break;

                case "DoomMaster":
                case "DoomMaster Horns":
                case "DoomMaster's Wrap":
                case "DoomMaster's Whip":
                    Bot.Quests.UpdateQuest(7635);
                    Core.HuntMonster("stonewood", "Sir Kut", req.Name, isTemp: false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("55532", "High ShadowScythe Trooper", "Mode: [select] only\nShould the bot buy \"High ShadowScythe Trooper\" ?", false),
        new Option<bool>("55534", "High ShadowScythe Rogue", "Mode: [select] only\nShould the bot buy \"High ShadowScythe Rogue\" ?", false),
        new Option<bool>("55536", "High ShadowScythe Mage", "Mode: [select] only\nShould the bot buy \"High ShadowScythe Mage\" ?", false),
        new Option<bool>("55538", "High ShadowScythe Trooper's Helm", "Mode: [select] only\nShould the bot buy \"High ShadowScythe Trooper's Helm\" ?", false),
        new Option<bool>("55540", "High ShadowScythe Rogue's Helm", "Mode: [select] only\nShould the bot buy \"High ShadowScythe Rogue's Helm\" ?", false),
        new Option<bool>("55542", "High ShadowScythe Hat + Locks", "Mode: [select] only\nShould the bot buy \"High ShadowScythe Hat + Locks\" ?", false),
        new Option<bool>("55544", "High ShadowScythe Mage's Hat", "Mode: [select] only\nShould the bot buy \"High ShadowScythe Mage's Hat\" ?", false),
        new Option<bool>("55546", "High ShadowScythe Mage's Rune", "Mode: [select] only\nShould the bot buy \"High ShadowScythe Mage's Rune\" ?", false),
        new Option<bool>("55548", "High ShadowScythe Rogue's Cape", "Mode: [select] only\nShould the bot buy \"High ShadowScythe Rogue's Cape\" ?", false),
        new Option<bool>("55550", "High ShadowScythe Trooper's Cape", "Mode: [select] only\nShould the bot buy \"High ShadowScythe Trooper's Cape\" ?", false),
        new Option<bool>("55554", "High ShadowScythe Blade", "Mode: [select] only\nShould the bot buy \"High ShadowScythe Blade\" ?", false),
        new Option<bool>("55557", "High ShadowScythe Staff", "Mode: [select] only\nShould the bot buy \"High ShadowScythe Staff\" ?", false),
        new Option<bool>("55561", "High Reversed Shadow Daggers", "Mode: [select] only\nShould the bot buy \"High Reversed Shadow Daggers\" ?", false),
        new Option<bool>("55580", "High ShadowScythe Daggers", "Mode: [select] only\nShould the bot buy \"High ShadowScythe Daggers\" ?", false),
        new Option<bool>("54787", "Zealous Aegis Paladin", "Mode: [select] only\nShould the bot buy \"Zealous Aegis Paladin\" ?", false),
        new Option<bool>("54789", "Zealous Crown", "Mode: [select] only\nShould the bot buy \"Zealous Crown\" ?", false),
        new Option<bool>("54790", "Zealous Great Helm", "Mode: [select] only\nShould the bot buy \"Zealous Great Helm\" ?", false),
        new Option<bool>("54791", "Zealous Miter", "Mode: [select] only\nShould the bot buy \"Zealous Miter\" ?", false),
        new Option<bool>("54792", "Zealous Miter And Stola", "Mode: [select] only\nShould the bot buy \"Zealous Miter And Stola\" ?", false),
        new Option<bool>("54794", "Zealous Seraphim Cape", "Mode: [select] only\nShould the bot buy \"Zealous Seraphim Cape\" ?", false),
        new Option<bool>("54799", "Zealous Light of Fate", "Mode: [select] only\nShould the bot buy \"Zealous Light of Fate\" ?", false),
        new Option<bool>("55826", "Necrodrone Trooper", "Mode: [select] only\nShould the bot buy \"Necrodrone Trooper\" ?", false),
        new Option<bool>("55825", "Necrodrone Rogue", "Mode: [select] only\nShould the bot buy \"Necrodrone Rogue\" ?", false),
        new Option<bool>("55824", "Necrodrone Mage", "Mode: [select] only\nShould the bot buy \"Necrodrone Mage\" ?", false),
        new Option<bool>("55736", "Charged Cryptborg", "Mode: [select] only\nShould the bot buy \"Charged Cryptborg\" ?", false),
        new Option<bool>("55739", "Cryptborg Laser Mount", "Mode: [select] only\nShould the bot buy \"Cryptborg Laser Mount\" ?", false),
        new Option<bool>("55742", "Cryptborg Laser Blade", "Mode: [select] only\nShould the bot buy \"Cryptborg Laser Blade\" ?", false),
        new Option<bool>("55737", "Charged Cryptborg Helm", "Mode: [select] only\nShould the bot buy \"Charged Cryptborg Helm\" ?", false),
        new Option<bool>("55747", "FireMaster", "Mode: [select] only\nShould the bot buy \"FireMaster\" ?", false),
        new Option<bool>("55748", "FireMaster's Horns", "Mode: [select] only\nShould the bot buy \"FireMaster's Horns\" ?", false),
        new Option<bool>("55749", "FireMaster's Cape", "Mode: [select] only\nShould the bot buy \"FireMaster's Cape\" ?", false),
        new Option<bool>("55750", "FireMaster's Whip", "Mode: [select] only\nShould the bot buy \"FireMaster's Whip\" ?", false),
    };
}
