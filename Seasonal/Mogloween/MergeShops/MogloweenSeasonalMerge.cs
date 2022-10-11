//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class MogloweenSeasonalMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreLegion Legion = new CoreLegion();
    public CoreNation Nation = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Candy Corn", "Box-o-Chocolates", "Lol-E-Pop", "Ghostly Cape", "Cursed Bone Club", "Ivy Blade", "Blister's Chainsaw", "Medusa Curse", "Sinister Pumpkin Sickles", "Great Pumpkin King Sword", "Legion Token", "Diamond of Nulgath " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        if (!Core.isSeasonalMapActive("mogloween"))
            return;
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("mogloween", 223, findIngredients);

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

                case "Lol-E-Pop":
                case "Box-o-Chocolates":
                case "Candy Corn":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("candycorn", "r2", "Right", "*", req.Name, quant, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Ghostly Cape":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("mogloween", "Ghostly Sheet", req.Name, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Cursed Bone Club":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("candycorn", "Stalkwalker", req.Name, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Ivy Blade":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("mogloween", "Pumpkinhead Fred", req.Name, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Blister's Chainsaw":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("mogloween", "Blister", req.Name, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    Core.CancelRegisteredQuests();
                    break;

                case "Medusa Curse":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.BuyItem("mogloween", 30, req.Name);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Sinister Pumpkin Sickles":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("candycorn", "Field Guardian", req.Name, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Great Pumpkin King Sword":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("mogloween", "Great Pumpkin King", req.Name, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Legion Token":
                    Legion.FarmLegionToken(quant);
                    break;

                case "Diamond of Nulgath":
                    Nation.FarmDiamondofNulgath(quant);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("36738", "Witch's Hat", "Mode: [select] only\nShould the bot buy \"Witch's Hat\" ?", false),
        new Option<bool>("36739", "Vampire Cape", "Mode: [select] only\nShould the bot buy \"Vampire Cape\" ?", false),
        new Option<bool>("36740", "Sad Jack o' Face", "Mode: [select] only\nShould the bot buy \"Sad Jack o' Face\" ?", false),
        new Option<bool>("36741", "Mummy Face", "Mode: [select] only\nShould the bot buy \"Mummy Face\" ?", false),
        new Option<bool>("36742", "Happy Jack o' Face", "Mode: [select] only\nShould the bot buy \"Happy Jack o' Face\" ?", false),
        new Option<bool>("36743", "Fiery Jack o' Face", "Mode: [select] only\nShould the bot buy \"Fiery Jack o' Face\" ?", false),
        new Option<bool>("36744", "Field Guardian", "Mode: [select] only\nShould the bot buy \"Field Guardian\" ?", false),
        new Option<bool>("36745", "Field Guardian Hat", "Mode: [select] only\nShould the bot buy \"Field Guardian Hat\" ?", false),
        new Option<bool>("36746", "Field Guardian Straw Hat", "Mode: [select] only\nShould the bot buy \"Field Guardian Straw Hat\" ?", false),
        new Option<bool>("36747", "Rust Man", "Mode: [select] only\nShould the bot buy \"Rust Man\" ?", false),
        new Option<bool>("36748", "Territorial Lion Morph", "Mode: [select] only\nShould the bot buy \"Territorial Lion Morph\" ?", false),
        new Option<bool>("36749", "Territorial Lion", "Mode: [select] only\nShould the bot buy \"Territorial Lion\" ?", false),
        new Option<bool>("36750", "Gourdian", "Mode: [select] only\nShould the bot buy \"Gourdian\" ?", false),
        new Option<bool>("36751", "Flying Marmoset", "Mode: [select] only\nShould the bot buy \"Flying Marmoset\" ?", false),
        new Option<bool>("36752", "Helm of the Gourdian", "Mode: [select] only\nShould the bot buy \"Helm of the Gourdian\" ?", false),
        new Option<bool>("36753", "Tainted Wings", "Mode: [select] only\nShould the bot buy \"Tainted Wings\" ?", false),
        new Option<bool>("36754", "Putrid Meaty Club", "Mode: [select] only\nShould the bot buy \"Putrid Meaty Club\" ?", false),
        new Option<bool>("36755", "Toxic Ivy Blade", "Mode: [select] only\nShould the bot buy \"Toxic Ivy Blade\" ?", false),
        new Option<bool>("36756", "Golden DeathSaw", "Mode: [select] only\nShould the bot buy \"Golden DeathSaw\" ?", false),
        new Option<bool>("36757", "Dorian and Dorothy", "Mode: [select] only\nShould the bot buy \"Dorian and Dorothy\" ?", false),
        new Option<bool>("36758", "Cute lil Dorothy Morph", "Mode: [select] only\nShould the bot buy \"Cute lil Dorothy Morph\" ?", false),
        new Option<bool>("36759", "Sinister PumpKing Blade", "Mode: [select] only\nShould the bot buy \"Sinister PumpKing Blade\" ?", false),
        new Option<bool>("36760", "Tododo", "Mode: [select] only\nShould the bot buy \"Tododo\" ?", false),
        new Option<bool>("36761", "Mummy", "Mode: [select] only\nShould the bot buy \"Mummy\" ?", false),
        new Option<bool>("36762", "Pumpkin Slicer", "Mode: [select] only\nShould the bot buy \"Pumpkin Slicer\" ?", false),
        new Option<bool>("36763", "Hekyll and Jyde", "Mode: [select] only\nShould the bot buy \"Hekyll and Jyde\" ?", false),
        new Option<bool>("36764", "Hanging Lantern Staff", "Mode: [select] only\nShould the bot buy \"Hanging Lantern Staff\" ?", false),
        new Option<bool>("36765", "Pink Gummi Were-bear and Lolly", "Mode: [select] only\nShould the bot buy \"Pink Gummi Were-bear and Lolly\" ?", false),
        new Option<bool>("36766", "Jack O Scythe", "Mode: [select] only\nShould the bot buy \"Jack O Scythe\" ?", false),
        new Option<bool>("36767", "Jack O Stave", "Mode: [select] only\nShould the bot buy \"Jack O Stave\" ?", false),
        new Option<bool>("36768", "Masquerade Vesture", "Mode: [select] only\nShould the bot buy \"Masquerade Vesture\" ?", false),
        new Option<bool>("36769", "Masquerade Mask", "Mode: [select] only\nShould the bot buy \"Masquerade Mask\" ?", false),
        new Option<bool>("36770", "Alea Masquerade Mask", "Mode: [select] only\nShould the bot buy \"Alea Masquerade Mask\" ?", false),
        new Option<bool>("36771", "SoulSucker Gourdhead", "Mode: [select] only\nShould the bot buy \"SoulSucker Gourdhead\" ?", false),
        new Option<bool>("36772", "Gourd-O Mask", "Mode: [select] only\nShould the bot buy \"Gourd-O Mask\" ?", false),
        new Option<bool>("36773", "CandyCorn Daggers", "Mode: [select] only\nShould the bot buy \"CandyCorn Daggers\" ?", false),
        new Option<bool>("36774", "Candy Sack", "Mode: [select] only\nShould the bot buy \"Candy Sack\" ?", false),
        new Option<bool>("36775", "Pumpkin Vine Cape", "Mode: [select] only\nShould the bot buy \"Pumpkin Vine Cape\" ?", false),
        new Option<bool>("36776", "Evolved PumpkinLord Armor", "Mode: [select] only\nShould the bot buy \"Evolved PumpkinLord Armor\" ?", false),
        new Option<bool>("36777", "Mummy Mask", "Mode: [select] only\nShould the bot buy \"Mummy Mask\" ?", false),
        new Option<bool>("31955", "Twisted Legion PumpkinLord Blade", "Mode: [select] only\nShould the bot buy \"Twisted Legion PumpkinLord Blade\" ?", false),
        new Option<bool>("31954", "Creeper Legion Helm", "Mode: [select] only\nShould the bot buy \"Creeper Legion Helm\" ?", false),
        new Option<bool>("31953", "Creeper Flame Legion Helm", "Mode: [select] only\nShould the bot buy \"Creeper Flame Legion Helm\" ?", false),
        new Option<bool>("31952", "Flaming Legion Helm", "Mode: [select] only\nShould the bot buy \"Flaming Legion Helm\" ?", false),
        new Option<bool>("31951", "Evil Legion Helm", "Mode: [select] only\nShould the bot buy \"Evil Legion Helm\" ?", false),
        new Option<bool>("31950", "Barbed Legion PumpkinLord Shroud", "Mode: [select] only\nShould the bot buy \"Barbed Legion PumpkinLord Shroud\" ?", false),
        new Option<bool>("31949", "Battered Legion PumpkinLord Shroud", "Mode: [select] only\nShould the bot buy \"Battered Legion PumpkinLord Shroud\" ?", false),
        new Option<bool>("31948", "Barbed Legion PumpkinLord Vines", "Mode: [select] only\nShould the bot buy \"Barbed Legion PumpkinLord Vines\" ?", false),
        new Option<bool>("31947", "Barbed Legion PumpkinLord Cape", "Mode: [select] only\nShould the bot buy \"Barbed Legion PumpkinLord Cape\" ?", false),
        new Option<bool>("31946", "Luminous Legion PumpkinLord Cape", "Mode: [select] only\nShould the bot buy \"Luminous Legion PumpkinLord Cape\" ?", false),
        new Option<bool>("36890", "Legion PumpkinLord", "Mode: [select] only\nShould the bot buy \"Legion PumpkinLord\" ?", false),
        new Option<bool>("46041", "Legion Pumpking Overlord", "Mode: [select] only\nShould the bot buy \"Legion Pumpking Overlord\" ?", false),
        new Option<bool>("46042", "Legion Pumpking Morph", "Mode: [select] only\nShould the bot buy \"Legion Pumpking Morph\" ?", false),
        new Option<bool>("46043", "Blade of the Pumpking Legion", "Mode: [select] only\nShould the bot buy \"Blade of the Pumpking Legion\" ?", false),
        new Option<bool>("46044", "A Gourdly Legion Spirit", "Mode: [select] only\nShould the bot buy \"A Gourdly Legion Spirit\" ?", false),
        new Option<bool>("50804", "Magical Baby Dragon Nulgath", "Mode: [select] only\nShould the bot buy \"Magical Baby Dragon Nulgath\" ?", false),
        new Option<bool>("50796", "Baby Dragon Dage", "Mode: [select] only\nShould the bot buy \"Baby Dragon Dage\" ?", false),
        new Option<bool>("56942", "Spooky Kid", "Mode: [select] only\nShould the bot buy \"Spooky Kid\" ?", false),
        new Option<bool>("56943", "Spooky Girl's Locks", "Mode: [select] only\nShould the bot buy \"Spooky Girl's Locks\" ?", false),
        new Option<bool>("56944", "Spooky Girl's Locks + Hat", "Mode: [select] only\nShould the bot buy \"Spooky Girl's Locks + Hat\" ?", false),
        new Option<bool>("56945", "Spooky Boy's Hair", "Mode: [select] only\nShould the bot buy \"Spooky Boy's Hair\" ?", false),
        new Option<bool>("56946", "Spooky Boy's Hair + Hat", "Mode: [select] only\nShould the bot buy \"Spooky Boy's Hair + Hat\" ?", false),
        new Option<bool>("56947", "Spooky Axe", "Mode: [select] only\nShould the bot buy \"Spooky Axe\" ?", false),
        new Option<bool>("56948", "Spooky Goth Outfit", "Mode: [select] only\nShould the bot buy \"Spooky Goth Outfit\" ?", false),
        new Option<bool>("56949", "Spooky Pigtails Hat + Morph", "Mode: [select] only\nShould the bot buy \"Spooky Pigtails Hat + Morph\" ?", false),
        new Option<bool>("56950", "Spooky Pigtails Morph", "Mode: [select] only\nShould the bot buy \"Spooky Pigtails Morph\" ?", false),
        new Option<bool>("56951", "Spooky Casual Morph", "Mode: [select] only\nShould the bot buy \"Spooky Casual Morph\" ?", false),
        new Option<bool>("56952", "Spooky Casual Hat + Morph", "Mode: [select] only\nShould the bot buy \"Spooky Casual Hat + Morph\" ?", false),
        new Option<bool>("56953", "Poison Bottle Sword", "Mode: [select] only\nShould the bot buy \"Poison Bottle Sword\" ?", false),
    };
}
