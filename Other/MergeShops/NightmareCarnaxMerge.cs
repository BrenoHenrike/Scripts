//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Story/DarkCarnax.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Nation/Various/Archfiend.cs
//cs_include Scripts/Good/BLoD/2UltimateBlindingLightofDestiny.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class NightmareCarnaxMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreStory Story = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    private DarkCarnaxStory DarkCarnax = new();
    private CoreNSOD NSOD = new();
    private ArchFiend AF = new();
    private UltimateBLoD uBLOD = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Synthetic Viscera", "Carnax Essence", "Perfect Orochi Scales", "Energized Aura", "Abyssal Contract", "Purified Undead Dragon Essence", "Overwhelmed Axe " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        DarkCarnax.Storyline();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("darkcarnax", 2170, findIngredients);

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

                case "Synthetic Viscera":
                    DarkCarnax.SyntheticViscera(quant);
                    break;

                case "Carnax Essence":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("aqlesson", "Carnax", req.Name, quant, false);
                    break;

                case "Perfect Orochi Scales":
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("shadowfortress", "r12", "Bottom", "*", req.Name, quant, false);
                    break;

                case "Energized Aura":
                    NSOD.EnergizedAura();
                    break;

                case "Abyssal Contract":
                    AF.AbyssalContract();
                    break;

                case "Purified Undead Dragon Essence":
                    uBLOD.PurifiedUndeadDragonEssence();
                    break;

                case "Overwhelmed Axe":
                    uBLOD.OverwhelmedAxe();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("70901", "Disciple of Carnax", "Mode: [select] only\nShould the bot buy \"Disciple of Carnax\" ?", false),
        new Option<bool>("70902", "Devotee of Carnax", "Mode: [select] only\nShould the bot buy \"Devotee of Carnax\" ?", false),
        new Option<bool>("70903", "Carnax Devotee's Morph", "Mode: [select] only\nShould the bot buy \"Carnax Devotee's Morph\" ?", false),
        new Option<bool>("70904", "Carnax Devotee's Helm", "Mode: [select] only\nShould the bot buy \"Carnax Devotee's Helm\" ?", false),
        new Option<bool>("70905", "Carnax Disciple's Hood", "Mode: [select] only\nShould the bot buy \"Carnax Disciple's Hood\" ?", false),
        new Option<bool>("70906", "Carnax Disciple's Horned Hood", "Mode: [select] only\nShould the bot buy \"Carnax Disciple's Horned Hood\" ?", false),
        new Option<bool>("70907", "Temple of Carnax", "Mode: [select] only\nShould the bot buy \"Temple of Carnax\" ?", false),
        new Option<bool>("70908", "Dragon Guardians of Carnax", "Mode: [select] only\nShould the bot buy \"Dragon Guardians of Carnax\" ?", false),
        new Option<bool>("70909", "Summoning Portal of Carnax", "Mode: [select] only\nShould the bot buy \"Summoning Portal of Carnax\" ?", false),
        new Option<bool>("70910", "Carnax Devotee's BattleAxe", "Mode: [select] only\nShould the bot buy \"Carnax Devotee's BattleAxe\" ?", false),
        new Option<bool>("70911", "Carnax Disciple's Dagger", "Mode: [select] only\nShould the bot buy \"Carnax Disciple's Dagger\" ?", false),
        new Option<bool>("70912", "Carnax Disciple's Daggers", "Mode: [select] only\nShould the bot buy \"Carnax Disciple's Daggers\" ?", false),
        new Option<bool>("70913", "Carnax Devotee's Claws", "Mode: [select] only\nShould the bot buy \"Carnax Devotee's Claws\" ?", false),
        new Option<bool>("72799", "Nightmare Carnax Disciple", "Mode: [select] only\nShould the bot buy \"Nightmare Carnax Disciple\" ?", false),
        new Option<bool>("72800", "Nightmare Carnax Devotee", "Mode: [select] only\nShould the bot buy \"Nightmare Carnax Devotee\" ?", false),
        new Option<bool>("72803", "Nightmare Carnax's Morph", "Mode: [select] only\nShould the bot buy \"Nightmare Carnax's Morph\" ?", false),
        new Option<bool>("72802", "Nightmare Carnax's Horned Hood", "Mode: [select] only\nShould the bot buy \"Nightmare Carnax's Horned Hood\" ?", false),
        new Option<bool>("72804", "Nightmare Carnax's Temple", "Mode: [select] only\nShould the bot buy \"Nightmare Carnax's Temple\" ?", false),
        new Option<bool>("72805", "Nightmare Carnax's Guardians", "Mode: [select] only\nShould the bot buy \"Nightmare Carnax's Guardians\" ?", false),
        new Option<bool>("72806", "Nightmare Carnax's BattleAxe", "Mode: [select] only\nShould the bot buy \"Nightmare Carnax's BattleAxe\" ?", false),
        new Option<bool>("72807", "Nightmare Carnax's Dagger", "Mode: [select] only\nShould the bot buy \"Nightmare Carnax's Dagger\" ?", false),
        new Option<bool>("72808", "Nightmare Carnax's Daggers", "Mode: [select] only\nShould the bot buy \"Nightmare Carnax's Daggers\" ?", false),
        new Option<bool>("72809", "Nightmare Carnax's Claws", "Mode: [select] only\nShould the bot buy \"Nightmare Carnax's Claws\" ?", false),
    };
}
