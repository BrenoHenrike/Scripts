//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;
using RBot.Items;
using RBot.Options;

public class TitanGearMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

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
        Adv.StartBuyAllMerge("titanattack", 2149, findIngredients);

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

                case "AntiTitan Supplies":
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("titanattack", "r9", "Left", "*", req.Name, quant,  isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Vindicator Titan's Axe":
                case "Vindicator Titan":
                case "Titanic Fluid":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("titanattack", "Titanic Vindicator", req.Name, quant, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Titan Paladin's Blade":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("titanattack", "Titanic Paladin", req.Name, quant, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

            }
        }
    }
}
