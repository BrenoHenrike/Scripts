//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/TitanAttack.cs
using RBot;
using RBot.Items;
using RBot.Options;

public class TitanGearIIMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public TitanAttackStory TAS = new();

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

        TAS.DoAll();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("titanstrike", 2154, findIngredients);

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

                case "Destroyer Essence":
                case "Titanic Destroyer Blade":
                case "Titanic Destroyer Morph":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("titanstrike", "Titanic Destroyer", req.Name, quant, false);
                    break;

                case "Titanic Tincture":
                    Core.EquipClass(ClassType.Solo);
                    Adv.BoostHuntMonster("titandrakath", "Titan Drakath", req.Name, quant, false);
                    break;

                case "Heroic Titan's Greatsword":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Solo);
                        Core.EnsureAccept(8776);
                        Core.HuntMonster("titanstrike", "Titanic Paladin", "Paladin Punished");
                    Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("titanstrike", "Titanic Doomknight", "Doomknight Decimated");
                        Core.HuntMonster("titanstrike", "Titanic Destroyer", "Destroyer Destroyed");
                        Core.EnsureComplete(8776);
                        Bot.Wait.ForPickup(req.Name);
                    break;

                case "Titan Paladin":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("titanattack", "Chaorrupted Bandit", "AntiTitan Supplies", 100, false);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("titanattack", "Titanic Vindicator", "Titanic Fluid", 40, false);
                    Adv.BuyItem("titanattack", 2149, req.Name);
                    break;

                case "Vindicator Titan XL":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("titanattack", "Chaorrupted Bandit", "AntiTitan Supplies", 100, false);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("titanattack", "Titanic Vindicator", "Titanic Fluid", 40, false);
                    Core.HuntMonster("titanattack", "Titanic Vindicator", "Vindicator Titan", isTemp: false);
                    Adv.BuyItem("titanattack", 2149, req.Name);
                    break;

                case "Vindicator Titan's Axes":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("titanattack", "Chaorrupted Bandit", "AntiTitan Supplies", 50, false);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("titanattack", "Titanic Vindicator", "Titanic Fluid", 20, false);
                    Core.HuntMonster("titanattack", "Titanic Vindicator", "Vindicator Titan's Axe", isTemp: false);
                    Adv.BuyItem("titanattack", 2149, req.Name);
                    break;

                case "Titan Paladin's Blades":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("titanattack", "Chaorrupted Bandit", "AntiTitan Supplies", 50, false);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("titanattack", "Titanic Vindicator", "Titanic Fluid", 20, false);
                    Core.HuntMonster("titanattack", "Titanic Paladin", "Titan Paladin's Blade", isTemp: false);
                    Adv.BuyItem("titanattack", 2149, req.Name);
                    break;

                case "Titan Drakath's Blade":
                    Core.EquipClass(ClassType.Solo);
                    Adv.BoostHuntMonster("titandrakath", "Titan Drakath", req.Name, quant, false);
                    break;

                case "Titan Paladin's Helm":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("titanattack", "Chaorrupted Bandit", "AntiTitan Supplies", 25, false);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("titanattack", "Titanic Vindicator", "Titanic Fluid", 10, false);
                    Adv.BuyItem("titanattack", 2149, req.Name);
                    break;

                case "Vindicator Titan's Helm":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("titanattack", "Chaorrupted Bandit", "AntiTitan Supplies", 25, false);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("titanattack", "Titanic Vindicator", "Titanic Fluid", 10, false);
                    Adv.BuyItem("titanattack", 2149, req.Name);
                    break;

                case "Titan Drakath's Morph":
                    Core.EquipClass(ClassType.Solo);
                    Adv.BoostHuntMonster("titandrakath", "Titan Drakath", req.Name, isTemp: false);
                    break;

                case "Titan Paladin's Cloak":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("titanattack", "Chaorrupted Bandit", "AntiTitan Supplies", 25, false);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("titanattack", "Titanic Vindicator", "Titanic Fluid", 10, false);
                    Adv.BuyItem("titanattack", 2149, req.Name);
                    break;

                case "Vindicator Titan's Cloak":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("titanattack", "Chaorrupted Bandit", "AntiTitan Supplies", 25, false);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("titanattack", "Titanic Vindicator", "Titanic Fluid", 10, false);
                    Adv.BuyItem("titanattack", 2149, req.Name);
                    break;

                case "Chaorrupted AntiTitan Corps":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("titanattack", "AntiTitan Corps", req.Name, quant, false);
                    break;

                case "Titan Hunter":
                    Adv.BuyItem("artistalley", 729, req.Name);
                    break;

            }
        }
    }
}
