//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs
using RBot;
using RBot.Items;
using RBot.Options;

public class ArchFiendWarlordMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreNation Nation = new();
    public CoreHollowborn HB = new();
    public WillpowerExtraction WPE = new();
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
        Adv.StartBuyAllMerge("tercessuinotlim", 1820, findIngredients);

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

                case "Unidentified 36":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    HB.FreshSouls(1, 0);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Diamond of Nulgath":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Nation.FarmDiamondofNulgath(quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Bone Dust":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Farm.BattleUnderB("Bone Dust", quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Gem of Nulgath":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Nation.FarmGemofNulgath(quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Blood Gem of the Archfiend":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Nation.FarmBloodGem(quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Fresh Soul":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    HB.FreshSouls(0, quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Unidentified 13":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Nation.FarmUni13(quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Unidentified 34":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    WPE.Unidentified34(quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Voucher of Nulgath (non-mem)":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Nation.FarmVoucher(false);
                    Bot.Wait.ForPickup(req.Name);
                    break;
                    
                case "Voucher of Nulgath":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Nation.FarmVoucher(true);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Dark Crystal Shard":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Nation.FarmDarkCrystalShard(quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Totem of Nulgath":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Nation.FarmTotemofNulgath(quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Strand of Vath's Hair":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.KillVath(req.Name, quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Unidentified 25":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (Bot.Player.Gold >= 15000000)
                        Core.BuyItem("tercessuinotlim", 1951, "Unidentified 25");
                    else Nation.TheAssistant(req.Name, quant); //low drop rate may take awhile.
                    Bot.Wait.ForPickup(req.Name);
                    break;

            }
        }
    }
}
