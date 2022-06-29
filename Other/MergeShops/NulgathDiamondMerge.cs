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
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Nation.FarmDiamondofNulgath(quant);
                    break;

                case "Archfiend's Favor":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Nation.ApprovalAndFavor(quant);
                    break;

                case "Nulgath's Approval":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Nation.ApprovalAndFavor(quant);
                    break;

                case "Taro's Manslayer":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Taro.GuardianTaro();
                    break;

                case "Dark Crystal Shard":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Nation.SwindleReturn();
                    break;

                case "Tainted Gem":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Nation.SwindleBulk(quant);
                    break;

                case "Blade of Holy Might":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("northlands", "Aisha's Drake", "Blade of Holy Might", isTemp: false);
                    break;

                case "Blood Gem of the Archfiend":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Nation.FarmBloodGem(quant);
                    break;

                case "Unidentified 13":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Nation.ContractExchange();
                    break;

                case "Iron":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        BLOD.UnlockMineCrafting();
                        Dailies.MineCrafting(new[] { "Iron" });
                    }
                    break;

                case "Gem of Nulgath":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Nation.FarmGemofNulgath();
                    break;

                case "Totem of Nulgath":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Nation.FarmTotemofNulgath(quant);
                    break;

                case "Bone Dust":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Farm.BattleUnderB("Bone Dust", quant);
                    break;

                case "Cloak of Nulgath":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Core.BuyItem("tercessuinotlim", 4667, "Cloak of Nulgath");
                    break;

                case "Staff of Imp Fire":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("bludrut2", "Fire Elemental", "Staff of Imp Fire", isTemp: false);
                    break;

                case "Cool Head":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Core.BuyItem("tercessuinotlim", 4826, "Cool Head");
                    break;

                case "Primal Dread Fang":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Nation.Supplies("Primal Dread Fang", quant);
                    break;

                case "Random Weapon of Nulgath":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Nation.Supplies("Random Weapon of Nulgath", quant);
                    break;

                case "Voucher of Nulgath (non-mem)":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        Nation.FarmVoucher(false);
                    break;

                case "Unidentified 27":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(584);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Nation.Supplies("Unidentified 26");
                        Core.HuntMonster("evilmarsh", "Dark Makai", "Dark Makai Sigil", 1);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Crystal Phoenix Blade of Nulgath":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                        JuggItems.JuggItems();
                    break;

            }
        }
    }
}
