//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/Revenant/CoreLR.cs
//cs_include Scripts/Legion/InfiniteLegionDarkCaster.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
//cs_include Scripts/Legion/LegionExcercise/LegionExercise3.cs
//cs_include Scripts/Legion/LegionExcercise/LegionExercise4.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem}.cs


using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class UndeadLegionMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreDailies Daily = new();
    public CoreLegion Legion = new();
    public CoreLR LR = new CoreLR();
    public LegionExercise3 LegionExercise3 = new();
    public LegionExercise4 LegionExercise4 = new();
    public DragonBladeofNulgath DBoN = new DragonBladeofNulgath();




    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Legion Token", "Frosted Falchion", "Judgement Scythe", "Judgement Hammer", "Cursed Scimitar", "Essence of the Undead Legend", "Shadow Shroud", "DragonBlade of Nulgath", "Fallen MonsterHunter", "Fallen MonsterHunter Helm", "Fallen MonsterHunter Cape", "Fallen MonsterHunter Sword", "Exalted Crown " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("underworld", 238, findIngredients);

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

                case "Legion Token":
                    Legion.FarmLegionToken(quant);
                    break;

                case "Frosted Falchion":
                    Adv.BuyItem("BlindingSnow", 236, req.Name);
                    break;

                case "Judgement Scythe":
                    LegionExercise4.Exercise(new[] { "Judgement Scythe", "Legion Token" });
                    break;

                case "Judgement Hammer":
                    LegionExercise3.Exercise(new[] { "Judgement Hammer", "Legion Token" });
                    break;

                case "Cursed Scimitar":
                    Adv.BuyItem("SandSea", 242, req.Name);
                    break;

                case "Essence of the Undead Legend":
                    Core.Logger($"{req.Name} Is seasonal item from Dage's Dark Birthday Shop");
                    Adv.BuyItem("DarkBirthday", 376, req.Name);
                    break;

                case "Shadow Shroud":
                    Daily.ShadowShroud();
                    if (!Core.CheckInventory(req.Name, quant))
                        Core.Logger($"Not enough \"Shadow Shroud\", please do the daily {15 - Bot.Inventory.GetQuantity("Shadow Shroud")} more times (not today)", messageBox: true);
                    break;

                case "DragonBlade of Nulgath":
                    DBoN.GetDragonBlade();
                    break;

                case "Fallen MonsterHunter":
                case "Fallen MonsterHunter Helm":
                case "Fallen MonsterHunter Cape":
                case "Fallen MonsterHunter Sword":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("DeepForest", "Aberrant Horror", req.Name, isTemp: false);
                    break;

                case "Exalted Crown":
                    LR.ExaltedCrown();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("6522", "Frostbite", "Mode: [select] only\nShould the bot buy \"Frostbite\" ?", false),
        new Option<bool>("6519", "Undead Legion OverLord", "Mode: [select] only\nShould the bot buy \"Undead Legion OverLord\" ?", false),
        new Option<bool>("2188", "Zealith Reavers", "Mode: [select] only\nShould the bot buy \"Zealith Reavers\" ?", false),
        new Option<bool>("6921", "Blade Master", "Mode: [select] only\nShould the bot buy \"Blade Master\" ?", false),
        new Option<bool>("6948", "The Guiltius", "Mode: [select] only\nShould the bot buy \"The Guiltius\" ?", false),
        new Option<bool>("8586", "Undead Legend Crown", "Mode: [select] only\nShould the bot buy \"Undead Legend Crown\" ?", false),
        new Option<bool>("8585", "Undead Legend Face", "Mode: [select] only\nShould the bot buy \"Undead Legend Face\" ?", false),
        new Option<bool>("8575", "Undead Legend Cape", "Mode: [select] only\nShould the bot buy \"Undead Legend Cape\" ?", false),
        new Option<bool>("8574", "Undead Legend", "Mode: [select] only\nShould the bot buy \"Undead Legend\" ?", false),
        new Option<bool>("9922", "Legion Judge", "Mode: [select] only\nShould the bot buy \"Legion Judge\" ?", false),
        new Option<bool>("9921", "Undead Assassin", "Mode: [select] only\nShould the bot buy \"Undead Assassin\" ?", false),
        new Option<bool>("9969", "Undead Assassin Swords", "Mode: [select] only\nShould the bot buy \"Undead Assassin Swords\" ?", false),
        new Option<bool>("10597", "Darkside Helm", "Mode: [select] only\nShould the bot buy \"Darkside Helm\" ?", false),
        new Option<bool>("10740", "Darkside Staff", "Mode: [select] only\nShould the bot buy \"Darkside Staff\" ?", false),
        new Option<bool>("11359", "Legion Titan", "Mode: [select] only\nShould the bot buy \"Legion Titan\" ?", false),
        new Option<bool>("11360", "Legion Titan Crown", "Mode: [select] only\nShould the bot buy \"Legion Titan Crown\" ?", false),
        new Option<bool>("11361", "Legion Titan Cloak", "Mode: [select] only\nShould the bot buy \"Legion Titan Cloak\" ?", false),
        new Option<bool>("18295", "Exalted Legion Champion", "Mode: [select] only\nShould the bot buy \"Exalted Legion Champion\" ?", false),
        new Option<bool>("18333", "Legion Champion Horns", "Mode: [select] only\nShould the bot buy \"Legion Champion Horns\" ?", false),
        new Option<bool>("18334", "Legion Champion Helm", "Mode: [select] only\nShould the bot buy \"Legion Champion Helm\" ?", false),
        new Option<bool>("18795", "Legion Archer Armor", "Mode: [select] only\nShould the bot buy \"Legion Archer Armor\" ?", false),
        new Option<bool>("18791", "Otherworldly Legion Deathstare Helm", "Mode: [select] only\nShould the bot buy \"Otherworldly Legion Deathstare Helm\" ?", false),
        new Option<bool>("18792", "Legion Deathstare Helm", "Mode: [select] only\nShould the bot buy \"Legion Deathstare Helm\" ?", false),
        new Option<bool>("18803", "Legion Deathdealer Blade and Shield", "Mode: [select] only\nShould the bot buy \"Legion Deathdealer Blade and Shield\" ?", false),
        new Option<bool>("18802", "Gutripper Daggers", "Mode: [select] only\nShould the bot buy \"Gutripper Daggers\" ?", false),
        new Option<bool>("26297", "Legion Skull and Goggles", "Mode: [select] only\nShould the bot buy \"Legion Skull and Goggles\" ?", false),
        new Option<bool>("26279", "Legion DeathMask", "Mode: [select] only\nShould the bot buy \"Legion DeathMask\" ?", false),
        new Option<bool>("26278", "Legion Apocalypse Skull", "Mode: [select] only\nShould the bot buy \"Legion Apocalypse Skull\" ?", false),
        new Option<bool>("26277", "Dark Flaming Legion Skull", "Mode: [select] only\nShould the bot buy \"Dark Flaming Legion Skull\" ?", false),
        new Option<bool>("28835", "Ultimate Lich King", "Mode: [select] only\nShould the bot buy \"Ultimate Lich King\" ?", false),
        new Option<bool>("28836", "Ultimate Lich King Helm", "Mode: [select] only\nShould the bot buy \"Ultimate Lich King Helm\" ?", false),
        new Option<bool>("28837", "Original Paragon Helm", "Mode: [select] only\nShould the bot buy \"Original Paragon Helm\" ?", false),
        new Option<bool>("34143", "Legion Castle", "Mode: [select] only\nShould the bot buy \"Legion Castle\" ?", false),
        new Option<bool>("43102", "Exalted Soul Cleaver", "Mode: [select] only\nShould the bot buy \"Exalted Soul Cleaver\" ?", false),
        new Option<bool>("43237", "Smiling Dage Helm", "Mode: [select] only\nShould the bot buy \"Smiling Dage Helm\" ?", false),
        new Option<bool>("47465", "Infinite Legion Dark Caster", "Mode: [select] only\nShould the bot buy \"Infinite Legion Dark Caster\" ?", false),
        new Option<bool>("48128", "Blade of the Loyal Legion Warrior", "Mode: [select] only\nShould the bot buy \"Blade of the Loyal Legion Warrior\" ?", false),
        new Option<bool>("53837", "SwordMaster", "Mode: [select] only\nShould the bot buy \"SwordMaster\" ?", false),
        new Option<bool>("53871", "Draconic Paragon Plate", "Mode: [select] only\nShould the bot buy \"Draconic Paragon Plate\" ?", false),
        new Option<bool>("53872", "Draconic Paragon Hood", "Mode: [select] only\nShould the bot buy \"Draconic Paragon Hood\" ?", false),
        new Option<bool>("53873", "Draconic Paragon Horned Hood", "Mode: [select] only\nShould the bot buy \"Draconic Paragon Horned Hood\" ?", false),
        new Option<bool>("53874", "Draconic Paragon Spiked Hood", "Mode: [select] only\nShould the bot buy \"Draconic Paragon Spiked Hood\" ?", false),
        new Option<bool>("53875", "Draconic Paragon Helmet", "Mode: [select] only\nShould the bot buy \"Draconic Paragon Helmet\" ?", false),
        new Option<bool>("53876", "Draconic Paragon Guard", "Mode: [select] only\nShould the bot buy \"Draconic Paragon Guard\" ?", false),
        new Option<bool>("53877", "Draconic Paragon Cape", "Mode: [select] only\nShould the bot buy \"Draconic Paragon Cape\" ?", false),
        new Option<bool>("53879", "Draconic Reavers", "Mode: [select] only\nShould the bot buy \"Draconic Reavers\" ?", false),
        new Option<bool>("53880", "Draconic Rhongomyniad", "Mode: [select] only\nShould the bot buy \"Draconic Rhongomyniad\" ?", false),
        new Option<bool>("53863", "Yami no Ronin", "Mode: [select] only\nShould the bot buy \"Yami no Ronin\" ?", false),
        new Option<bool>("53864", "Yami no Ronin Helm", "Mode: [select] only\nShould the bot buy \"Yami no Ronin Helm\" ?", false),
        new Option<bool>("53865", "Yami no Ronin Sheathed Katana", "Mode: [select] only\nShould the bot buy \"Yami no Ronin Sheathed Katana\" ?", false),
        new Option<bool>("53866", "Yami no Ronin Shuriken", "Mode: [select] only\nShould the bot buy \"Yami no Ronin Shuriken\" ?", false),
        new Option<bool>("53868", "Yami no Ronin Blade", "Mode: [select] only\nShould the bot buy \"Yami no Ronin Blade\" ?", false),
        new Option<bool>("53869", "Yami no Ronin Dual Blades", "Mode: [select] only\nShould the bot buy \"Yami no Ronin Dual Blades\" ?", false),
        new Option<bool>("53870", "Yami no Ronin Shurikens", "Mode: [select] only\nShould the bot buy \"Yami no Ronin Shurikens\" ?", false),
        new Option<bool>("59972", "Undead Legion Healer", "Mode: [select] only\nShould the bot buy \"Undead Legion Healer\" ?", false),
        new Option<bool>("59973", "Undead Legion Healer Helm", "Mode: [select] only\nShould the bot buy \"Undead Legion Healer Helm\" ?", false),
        new Option<bool>("59974", "Undead Legion Warrior", "Mode: [select] only\nShould the bot buy \"Undead Legion Warrior\" ?", false),
        new Option<bool>("59975", "Undead Legion Warrior Helm", "Mode: [select] only\nShould the bot buy \"Undead Legion Warrior Helm\" ?", false),
        new Option<bool>("59976", "Undead Legion Mage", "Mode: [select] only\nShould the bot buy \"Undead Legion Mage\" ?", false),
        new Option<bool>("59977", "Undead Legion Mage Hat", "Mode: [select] only\nShould the bot buy \"Undead Legion Mage Hat\" ?", false),
        new Option<bool>("59978", "Undead Legion Mage Hat+Mask", "Mode: [select] only\nShould the bot buy \"Undead Legion Mage Hat+Mask\" ?", false),
        new Option<bool>("59979", "Undead Legion Rogue", "Mode: [select] only\nShould the bot buy \"Undead Legion Rogue\" ?", false),
        new Option<bool>("59980", "Undead Legion Rogue Mask + Locks", "Mode: [select] only\nShould the bot buy \"Undead Legion Rogue Mask + Locks\" ?", false),
        new Option<bool>("59981", "Undead Legion Rogue Skull", "Mode: [select] only\nShould the bot buy \"Undead Legion Rogue Skull\" ?", false),
        new Option<bool>("60102", "Legion DragonBlade of Nulgath", "Mode: [select] only\nShould the bot buy \"Legion DragonBlade of Nulgath\" ?", false),
        new Option<bool>("60878", "Legion MonsterHunter", "Mode: [select] only\nShould the bot buy \"Legion MonsterHunter\" ?", false),
        new Option<bool>("60879", "Legion MonsterHunter's Helm", "Mode: [select] only\nShould the bot buy \"Legion MonsterHunter's Helm\" ?", false),
        new Option<bool>("60880", "Legion MonsterHunter's Cape", "Mode: [select] only\nShould the bot buy \"Legion MonsterHunter's Cape\" ?", false),
        new Option<bool>("60881", "Legion MonsterHunter's Sword", "Mode: [select] only\nShould the bot buy \"Legion MonsterHunter's Sword\" ?", false),
        new Option<bool>("51507", "Legion Revenant Armor", "Mode: [select] only\nShould the bot buy \"Legion Revenant Armor\" ?", false),
    };
}
