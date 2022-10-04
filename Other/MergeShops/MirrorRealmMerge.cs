//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class MirrorRealmMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public Core13LoC LoC = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Mirror Realm Token", "Undead Paladin Token", "Chaos Shifter", "Purification Orb " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("mirrorportal", 618, findIngredients);

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

                case "Mirror Realm Token":
                    LoC.Xiang();
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(3188);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonsterMapID("mirrorportal", 1);
                    Core.CancelRegisteredQuests();
                    break;

                case "Undead Paladin Token":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("OverWorld", "Undead Artix", req.Name, quant, isTemp: false);
                    break;

                case "Chaos Shifter":
                    Core.FarmingLogger(req.Name, quant);
                    Adv.BuyItem("mountdoomskull", 776, req.Name);
                    break;

                case "Purification Orb":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("Doomwood", "Undead Paladin", req.Name, quant, isTemp: false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("17412", "Life Taker", "Mode: [select] only\nShould the bot buy \"Life Taker\" ?", false),
        new Option<bool>("17413", "Assault Pack", "Mode: [select] only\nShould the bot buy \"Assault Pack\" ?", false),
        new Option<bool>("17414", "Life Taker Hair", "Mode: [select] only\nShould the bot buy \"Life Taker Hair\" ?", false),
        new Option<bool>("17415", "Life Taker Locks", "Mode: [select] only\nShould the bot buy \"Life Taker Locks\" ?", false),
        new Option<bool>("17418", "Blades of Life-Taking", "Mode: [select] only\nShould the bot buy \"Blades of Life-Taking\" ?", false),
        new Option<bool>("17434", "Blade of Life-Taking", "Mode: [select] only\nShould the bot buy \"Blade of Life-Taking\" ?", false),
        new Option<bool>("17488", "ShadowReaper Of Doom", "Mode: [select] only\nShould the bot buy \"ShadowReaper Of Doom\" ?", false),
        new Option<bool>("17489", "Undead Paladin", "Mode: [select] only\nShould the bot buy \"Undead Paladin\" ?", false),
        new Option<bool>("17490", "Paladin's Curse", "Mode: [select] only\nShould the bot buy \"Paladin's Curse\" ?", false),
        new Option<bool>("17504", "Mind Expulsion Blade", "Mode: [select] only\nShould the bot buy \"Mind Expulsion Blade\" ?", false),
        new Option<bool>("17427", "Underfriend Blade of Nulgath", "Mode: [select] only\nShould the bot buy \"Underfriend Blade of Nulgath\" ?", false),
        new Option<bool>("17429", "Blade of Ashes", "Mode: [select] only\nShould the bot buy \"Blade of Ashes\" ?", false),
        new Option<bool>("17505", "Cleric Of Nulgath", "Mode: [select] only\nShould the bot buy \"Cleric Of Nulgath\" ?", false),
        new Option<bool>("17506", "ArchAngel's Protection", "Mode: [select] only\nShould the bot buy \"ArchAngel's Protection\" ?", false),
        new Option<bool>("17507", "Hood of the ArchAngel", "Mode: [select] only\nShould the bot buy \"Hood of the ArchAngel\" ?", false),
        new Option<bool>("17508", "Enticing Hood of the ArchAngel", "Mode: [select] only\nShould the bot buy \"Enticing Hood of the ArchAngel\" ?", false),
        new Option<bool>("17509", "Intimidating Hood of the ArchAngel", "Mode: [select] only\nShould the bot buy \"Intimidating Hood of the ArchAngel\" ?", false),
        new Option<bool>("17510", "Destiny Kitten", "Mode: [select] only\nShould the bot buy \"Destiny Kitten\" ?", false),
        new Option<bool>("20760", "Salvage", "Mode: [select] only\nShould the bot buy \"Salvage\" ?", false),
        new Option<bool>("20761", "Force Of Evil", "Mode: [select] only\nShould the bot buy \"Force Of Evil\" ?", false),
        new Option<bool>("20762", "Evilnator", "Mode: [select] only\nShould the bot buy \"Evilnator\" ?", false),
        new Option<bool>("20763", "Dual Sunlight Daggers", "Mode: [select] only\nShould the bot buy \"Dual Sunlight Daggers\" ?", false),
        new Option<bool>("20810", "Dishonored Sword", "Mode: [select] only\nShould the bot buy \"Dishonored Sword\" ?", false),
        new Option<bool>("20811", "Holy Blessing Spear", "Mode: [select] only\nShould the bot buy \"Holy Blessing Spear\" ?", false),
        new Option<bool>("20764", "Duality Mage", "Mode: [select] only\nShould the bot buy \"Duality Mage\" ?", false),
        new Option<bool>("20765", "Duality Mage Runecape", "Mode: [select] only\nShould the bot buy \"Duality Mage Runecape\" ?", false),
        new Option<bool>("20766", "Bearded Duality Mage Morph", "Mode: [select] only\nShould the bot buy \"Bearded Duality Mage Morph\" ?", false),
        new Option<bool>("20769", "Female Duality Mage Morph", "Mode: [select] only\nShould the bot buy \"Female Duality Mage Morph\" ?", false),
        new Option<bool>("20770", "Duality Mage Staff", "Mode: [select] only\nShould the bot buy \"Duality Mage Staff\" ?", false),
        new Option<bool>("20843", "Duality Mage - Good", "Mode: [select] only\nShould the bot buy \"Duality Mage - Good\" ?", false),
        new Option<bool>("20844", "Duality Mage - Evil", "Mode: [select] only\nShould the bot buy \"Duality Mage - Evil\" ?", false),
        new Option<bool>("20853", "Duality Mage Good Runecape", "Mode: [select] only\nShould the bot buy \"Duality Mage Good Runecape\" ?", false),
        new Option<bool>("20852", "Duality Mage Evil Runecape", "Mode: [select] only\nShould the bot buy \"Duality Mage Evil Runecape\" ?", false),
        new Option<bool>("20851", "Duality Mage Evil Beard", "Mode: [select] only\nShould the bot buy \"Duality Mage Evil Beard\" ?", false),
        new Option<bool>("20849", "Duality Mage Good Morph", "Mode: [select] only\nShould the bot buy \"Duality Mage Good Morph\" ?", false),
        new Option<bool>("20848", "Duality Mage Evil Morph", "Mode: [select] only\nShould the bot buy \"Duality Mage Evil Morph\" ?", false),
        new Option<bool>("20847", "Duality Mage Evil Staff", "Mode: [select] only\nShould the bot buy \"Duality Mage Evil Staff\" ?", false),
        new Option<bool>("20846", "Duality Mage Good Staff", "Mode: [select] only\nShould the bot buy \"Duality Mage Good Staff\" ?", false),
        new Option<bool>("20797", "REALLY Undead Paladin", "Mode: [select] only\nShould the bot buy \"REALLY Undead Paladin\" ?", false),
        new Option<bool>("20798", "BONE-afied Paladin", "Mode: [select] only\nShould the bot buy \"BONE-afied Paladin\" ?", false),
        new Option<bool>("20918", "Lunar Sand Axe", "Mode: [select] only\nShould the bot buy \"Lunar Sand Axe\" ?", false),
        new Option<bool>("20919", "Lunar Sand Daggers", "Mode: [select] only\nShould the bot buy \"Lunar Sand Daggers\" ?", false),
        new Option<bool>("21166", "Cloud Shifter", "Mode: [select] only\nShould the bot buy \"Cloud Shifter\" ?", false),
    };
}
