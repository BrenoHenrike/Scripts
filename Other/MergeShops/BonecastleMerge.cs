//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BonecastleMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreDailies Daily = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Bonecastle Token", "Vaden Helm Token", "Shadow Skull " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("bonecastle", 1242, findIngredients);

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

                case "Bonecastle Token":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("bonecastlec", "Undead Golden Knight", req.Name, quant, false);
                    break;

                case "Vaden Helm Token":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("bonecastlec", "Vaden", req.Name, quant, false);
                    break;

                case "Shadow Skull":
                    Daily.DeathKnightLord();
                    if (!Core.CheckInventory(req.Name, quant))
                        Core.Logger($"Not enough \"Shadow Skull\", please do the daily {30 - Bot.Inventory.GetQuantity("Shadow Skull")} more times (not today)", messageBox: true);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("34577", "The Piercer Plate", "Mode: [select] only\nShould the bot buy \"The Piercer Plate\" ?", false),
        new Option<bool>("34578", "Face Of The Piercer", "Mode: [select] only\nShould the bot buy \"Face Of The Piercer\" ?", false),
        new Option<bool>("34579", "Hood Of The Piercer", "Mode: [select] only\nShould the bot buy \"Hood Of The Piercer\" ?", false),
        new Option<bool>("34580", "The Piercer Accoutrements", "Mode: [select] only\nShould the bot buy \"The Piercer Accoutrements\" ?", false),
        new Option<bool>("34581", "Axe Of The Piercer", "Mode: [select] only\nShould the bot buy \"Axe Of The Piercer\" ?", false),
        new Option<bool>("34582", "Tomb of The Piercer", "Mode: [select] only\nShould the bot buy \"Tomb of The Piercer\" ?", false),
        new Option<bool>("34583", "The Piercer Tombstone", "Mode: [select] only\nShould the bot buy \"The Piercer Tombstone\" ?", false),
        new Option<bool>("34655", "Vaden's Helm", "Mode: [select] only\nShould the bot buy \"Vaden's Helm\" ?", false),
        new Option<bool>("34571", "Enchanted DeathKnight", "Mode: [select] only\nShould the bot buy \"Enchanted DeathKnight\" ?", false),
        new Option<bool>("34572", "Enchanted DeathKnight Plate", "Mode: [select] only\nShould the bot buy \"Enchanted DeathKnight Plate\" ?", false),
        new Option<bool>("34573", "Enchanted DeathKnight Cape", "Mode: [select] only\nShould the bot buy \"Enchanted DeathKnight Cape\" ?", false),
        new Option<bool>("34574", "Enchanted DeathKnight Axe", "Mode: [select] only\nShould the bot buy \"Enchanted DeathKnight Axe\" ?", false),
        new Option<bool>("34718", "DeathKnight's Hood", "Mode: [select] only\nShould the bot buy \"DeathKnight's Hood\" ?", false),
        new Option<bool>("34740", "Royal Deathbringer", "Mode: [select] only\nShould the bot buy \"Royal Deathbringer\" ?", false),
        new Option<bool>("34741", "Royal Deathbringer Cloak", "Mode: [select] only\nShould the bot buy \"Royal Deathbringer Cloak\" ?", false),
        new Option<bool>("34742", "Royal Deathbringer Helm", "Mode: [select] only\nShould the bot buy \"Royal Deathbringer Helm\" ?", false),
        new Option<bool>("34743", "Royal Deathbringer Sabre", "Mode: [select] only\nShould the bot buy \"Royal Deathbringer Sabre\" ?", false),
        new Option<bool>("34666", "Pile of Bone Skulls", "Mode: [select] only\nShould the bot buy \"Pile of Bone Skulls\" ?", false),
        new Option<bool>("34665", "Pile of Silver Skulls", "Mode: [select] only\nShould the bot buy \"Pile of Silver Skulls\" ?", false),
        new Option<bool>("34664", "Pile of Golden Skulls", "Mode: [select] only\nShould the bot buy \"Pile of Golden Skulls\" ?", false),
        new Option<bool>("34723", "Bone Castle House", "Mode: [select] only\nShould the bot buy \"Bone Castle House\" ?", false),
        new Option<bool>("34780", "DeathKnight Lord", "Mode: [select] only\nShould the bot buy \"DeathKnight Lord\" ?", false),
    };
}
