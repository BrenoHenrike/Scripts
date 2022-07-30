//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Ruinedcrown.cs
//cs_include Scripts/Story/UltraTyndariusPrereqs.cs
using RBot;
using RBot.Items;
using RBot.Options;

public class ShadowflameFinaleMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public RuinedCrown RC = new();

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
        RC.DoAll();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("ruinedcrown", 2156, findIngredients);

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

                case "Willpower":
                    Core.AddDrop("ShadowFlame Healer", "ShadowFlame Mage");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(8788);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("ruinedcrown", "Calamitous Warlic", "Warlic’s Favor");
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("ruinedcrown", "Frenzied Mana", "Mana Residue", 8);
                        Core.HuntMonster("ruinedcrown", "Mana-Burdened Mage", "Mage’s Blood Sample", 8);
                        Core.EnsureComplete(8788);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "ShadowFlame Warrior":
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ruinedcrown", "Mana-Burdened Knight", req.Name, isTemp: false);
                    }
                    break;

                case "ShadowFlame Mage":
                case "ShadowFlame Healer":
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ruinedcrown", "Mana-Burdened Mage", req.Name, isTemp: false);
                    }
                    break;

                case "ShadowFlame Rogue":
                case "ShadowFlame Rogue’s Mask":
                case "ShadowFlame Rogue’s Mortal Locks":
                case "ShadowFlame Rogue’s Locks":
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ruinedcrown", "Mana-Burdened Minion", req.Name, isTemp: false);
                    }
                    break;

            }
        }
    }
}
