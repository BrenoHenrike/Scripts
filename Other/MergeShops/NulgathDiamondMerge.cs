//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Nation/Various/JuggernautItems.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Nation/Various/TarosManslayer.cs
//cs_include Scripts/Nation/Various/PurifiedClaymoreOfDestiny.cs
using RBot;
using RBot.Items;
using RBot.Options;

public class NulgathDiamondMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreDailies Dailies = new();
    public static CoreAdvanced sAdv = new();
    public CoreNation Nation = new();
    public TarosManslayer Taro = new();
    public JuggernautItemsofNulgath JuggItems = new();
    public CoreBLOD BLOD = new();
    public PurifiedClaymoreOfDestiny Claymore = new();

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
        Adv.StartBuyAllMerge("evilwarnul", 456, findIngredients);

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

                case "Diamond of Nulgath":
                    Nation.FarmDiamondofNulgath(quant);
                    break;

                case "Archfiend's Favor":
                    Nation.ApprovalAndFavor(0, quant);
                    break;

                case "Nulgath's Approval":
                    Nation.ApprovalAndFavor(quant, 0);
                    break;

                case "Taro's Manslayer":
                    Taro.GuardianTaro();
                    break;

                case "Dark Crystal Shard":
                    Nation.FarmDarkCrystalShard(quant);
                    break;

                case "Tainted Gem":
                    Nation.SwindleBulk(quant);
                    break;

                case "Blade of Holy Might":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("northlands", "Aisha's Drake", "Blade of Holy Might", isTemp: false);
                    break;

                case "Blood Gem of the Archfiend":
                    Nation.FarmBloodGem(quant);
                    break;

                case "Unidentified 13":
                    Nation.FarmUni13(quant);
                    break;

                case "Iron":
                    BLOD.UnlockMineCrafting();
                    Dailies.MineCrafting(new[] { "Iron" }, quant);
                    break;

                case "Gem of Nulgath":
                    Nation.FarmGemofNulgath(quant);
                    break;

                case "Totem of Nulgath":
                    Nation.FarmTotemofNulgath(quant);
                    break;

                case "Bone Dust":
                    Farm.BattleUnderB("Bone Dust", quant);
                    break;

                case "Cloak of Nulgath":
                    Core.BuyItem("tercessuinotlim", 4667, "Cloak of Nulgath");
                    break;

                case "Staff of Imp Fire":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("bludrut2", "Fire Elemental", "Staff of Imp Fire", isTemp: false);
                    break;

                case "Cool Head":
                    Core.BuyItem("tercessuinotlim", 4826, "Cool Head");
                    break;

                case "Primal Dread Fang":
                    Nation.Supplies("Primal Dread Fang", quant);
                    break;

                case "Random Weapon of Nulgath":
                    Nation.Supplies("Random Weapon of Nulgath", quant);
                    break;

                case "Voucher of Nulgath (non-mem)":
                    Nation.FarmVoucher(false);
                    break;

                case "Unidentified 27":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EnsureAccept(584);
                    Nation.Supplies("Unidentified 26");
                    Core.HuntMonster("evilmarsh", "Dark Makai", "Dark Makai Sigil", 1);
                    Core.EnsureComplete(584);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Crystal Phoenix Blade of Nulgath":
                    Core.FarmingLogger($"{req.Name}", quant);

                    Nation.FarmDiamondofNulgath(13);
                    Nation.FarmDarkCrystalShard(50);
                    Nation.FarmTotemofNulgath(3);
                    Nation.FarmGemofNulgath(20);
                    Nation.FarmVoucher(false);
                    Nation.SwindleBulk(50);

                    Core.EnsureAccept(837);
                    Core.HuntMonster("underworld", "Undead Bruiser", "Undead Bruiser Rune");
                    Core.EnsureComplete(837, req.ID);
                    Bot.Wait.ForPickup(req.Name);
                    break;
            }
        }
    }
}
