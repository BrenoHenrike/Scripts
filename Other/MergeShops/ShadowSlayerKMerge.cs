//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Story/GiantTaleStory.cs
//cs_include Scripts/Story/ShadowSlayerK.cs
//cs_Include Scripts/Farm/BuyScrolls.cs
using Skua.Core.Interfaces;
using Skua.Core.Models;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShadowSlayerKMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public ShadowSlayerK SSK = new();
    public static CoreAdvanced sAdv = new();

    public CoreDailies Dailies = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SSK.Storyline();
        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        DialogResult result = Bot.ShowMessageBox("Items containing the word \"Shadow\" in this shop cost 1 Elders' Blood per piece, " +
                                                "which is a valuable resource used to get Void Highlord. Are you sure you want to continue?",
                                                "Warning: Elders' Blood Usage", "Yes", "No");
        if (result.Text != "Yes")
            return;

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("safiria", 2044, findIngredients);

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

                case "Meat Ration":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8263);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("cellar", "GreenRat", "Green Mystery Meat", 10, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Grain Ration":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8264);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("safiria", "Blood Maggot", "Bundle of Rice", 3, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Dairy Ration":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8265);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("odokuro", "Boss", "Right", "O-dokuro", "Bone Hurt Juice", 5, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Shadowslayer Apprentice Badge":
                    Core.FarmingLogger($"{req.Name}", quant);
                    if (!Core.CheckInventory("Chibi Eldritch Yume"))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("chaosbeast", "Kathool", "Chibi Eldritch Yume", isTemp: false);
                    }
                    Core.RegisterQuests(8266);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        if (!Core.CheckInventory("Holy Wasabi"))
                        {
                            Core.AddDrop("Holy Wasabi");
                            Core.EnsureAccept(1075);

                            Core.EquipClass(ClassType.Farm);
                            Core.HuntMonster("doomwood", "Doomwood Ectomancer", "Dried Wasabi Powder", 4);
                            Core.GetMapItem(428, 1, "lightguard");

                            Core.EnsureComplete(1075);
                            Bot.Wait.ForPickup("Holy Wasabi");
                        }
                        Adv.BuyItem("alchemyacademy", 2036, "Sage Tonic", 3);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("Sloth", "Phlegnn", "Unnatural Ooze", 8);
                        Core.HuntMonster("beehive", "Killer Queen Bee", "Sleepy Honey");

                        Dailies.EldersBlood();
                        if (!Core.CheckInventory("Elders' Blood"))
                            Core.Logger("You ran out Elders' Blood, run the bot again at a later date.", messageBox: true, stopBot: true);

                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("62993", "Imperial Infantry", "Mode: [select] only\nShould the bot buy \"Imperial Infantry\" ?", false),
        new Option<bool>("62994", "Imperial Morion", "Mode: [select] only\nShould the bot buy \"Imperial Morion\" ?", false),
        new Option<bool>("62995", "Imperial Arsenal", "Mode: [select] only\nShould the bot buy \"Imperial Arsenal\" ?", false),
        new Option<bool>("62996", "Imperial Shield Cape", "Mode: [select] only\nShould the bot buy \"Imperial Shield Cape\" ?", false),
        new Option<bool>("62997", "Imperial Gladius", "Mode: [select] only\nShould the bot buy \"Imperial Gladius\" ?", false),
        new Option<bool>("62999", "Imperial Longcast Rifle", "Mode: [select] only\nShould the bot buy \"Imperial Longcast Rifle\" ?", false),
        new Option<bool>("62998", "Imperial Gladius + Pavise", "Mode: [select] only\nShould the bot buy \"Imperial Gladius + Pavise\" ?", false),
        new Option<bool>("59117", "Surtara Myrmidon", "Mode: [select] only\nShould the bot buy \"Surtara Myrmidon\" ?", false),
        new Option<bool>("59118", "Surtara Armet", "Mode: [select] only\nShould the bot buy \"Surtara Armet\" ?", false),
        new Option<bool>("59119", "Surtara Kabuto", "Mode: [select] only\nShould the bot buy \"Surtara Kabuto\" ?", false),
        new Option<bool>("59120", "Surtara Mantle", "Mode: [select] only\nShould the bot buy \"Surtara Mantle\" ?", false),
        new Option<bool>("59121", "Surtara Rings", "Mode: [select] only\nShould the bot buy \"Surtara Rings\" ?", false),
        new Option<bool>("59122", "Surtara Wings", "Mode: [select] only\nShould the bot buy \"Surtara Wings\" ?", false),
        new Option<bool>("59123", "Surtara Gladius", "Mode: [select] only\nShould the bot buy \"Surtara Gladius\" ?", false),
        new Option<bool>("59124", "Surtara Maul", "Mode: [select] only\nShould the bot buy \"Surtara Maul\" ?", false),
        new Option<bool>("59125", "Surtara Nagamaki", "Mode: [select] only\nShould the bot buy \"Surtara Nagamaki\" ?", false),
        new Option<bool>("63034", "ShadowSlayer's Apprentice", "Mode: [select] only\nShould the bot buy \"ShadowSlayer's Apprentice\" ?", false),
        new Option<bool>("59115", "Chibi ShadowSlayer K", "Mode: [select] only\nShould the bot buy \"Chibi ShadowSlayer K\" ?", false),
        new Option<bool>("56038", "Antiquated Shadowslayer", "Mode: [select] only\nShould the bot buy \"Antiquated Shadowslayer\" ?", false),
        new Option<bool>("56039", "Antiquated Shadow Hair", "Mode: [select] only\nShould the bot buy \"Antiquated Shadow Hair\" ?", false),
        new Option<bool>("56040", "Antiquated Shadow Hat", "Mode: [select] only\nShould the bot buy \"Antiquated Shadow Hat\" ?", false),
        new Option<bool>("56041", "Antiquated Shadow Locks", "Mode: [select] only\nShould the bot buy \"Antiquated Shadow Locks\" ?", false),
        new Option<bool>("56042", "Antiquated Shadow Hat + Locks", "Mode: [select] only\nShould the bot buy \"Antiquated Shadow Hat + Locks\" ?", false),
        new Option<bool>("56043", "Antiquated Shadow Cloak", "Mode: [select] only\nShould the bot buy \"Antiquated Shadow Cloak\" ?", false),
        new Option<bool>("56044", "Antiquated Shadow Sabre", "Mode: [select] only\nShould the bot buy \"Antiquated Shadow Sabre\" ?", false),
        new Option<bool>("56045", "Antiquated Shadow Swordstaff", "Mode: [select] only\nShould the bot buy \"Antiquated Shadow Swordstaff\" ?", false),
        new Option<bool>("56046", "Antiquated Shadow Pistolsword", "Mode: [select] only\nShould the bot buy \"Antiquated Shadow Pistolsword\" ?", false),
    };
}
