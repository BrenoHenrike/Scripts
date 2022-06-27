//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Doomwood/DoomwoodPart3.cs
using RBot;
using RBot.Items;
using RBot.Options;

public class DoomLegacyMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public DoomwoodPart3 DWp3 = new();

    public List<IOption> Options = sAdv.MergeOptions;
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(ScriptInterface bot)
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
            int currentQuant = req.Temp ? Bot.Inventory.GetTempQuantity(req.Name) : Bot.Inventory.GetQuantity(req.Name);
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
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
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
                    DWp3.StoryLine();
                    Core.RegisterQuests(7616);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
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
}
