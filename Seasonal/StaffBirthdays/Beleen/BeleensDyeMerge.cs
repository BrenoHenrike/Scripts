/*
name: Beleens Dye Merge
description: This bot will farm the items belonging to the selected mode for the Beleens Dye Merge [1550] in /chateau
tags: beleens, dye, merge, chateau, magentity, mrs, cuddles, pet, on, your, head, fists, fierceness, like, a, pretty, battlemoglin, pink, sockatana, chainsaw, katana, unarmed, derp, llama, overlord, face, rose, ebil, ninja
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/HerosHeartDay/SwaggysChateau.cs
//cs_include Scripts/Other/MergeShops/CyseroMerge.cs
//cs_include Scripts/Other/MergeShops/ArtixWeddingMerge.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BeleensDyeMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CyseroMerge CyseroMerge = new();
    private ArtixWeddingMerge ArtixWeddingMerge = new();
    private SwaggysChateau SwaggysChateau = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Astral Entity", "Pink Potion", "Mr. Cuddles Pet", "Mr. Cuddles on your Head", "Fists of Fire", "Like a Battlemoglin", "Green Sockatana", "Chainsaw Katana", "Unarmed", "Kitty SkyFighter", "BaconCat Force Face", "Ebil Ninja", "Ebil Ninja Hood" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        SwaggysChateau.CompleteStory();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("chateau", 1550, findIngredients, buyOnlyThis, buyMode: buyMode);

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
                    bool shouldStop = !Adv.matsOnly || !dontStopMissingIng;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                case "Astral Entity":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("Ledgermayne", "Ledgermayne", req.Name, isTemp: false);
                    break;

                case "Pink Potion":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("chateau", "Pinky", req.Name, quant, isTemp: false);
                    break;

                case "Mr. Cuddles Pet":
                case "Mr. Cuddles on your Head":
                    Core.HuntMonster("lovelockdown", "Ultra Cuddles", req.Name, isTemp: false);
                    break;

                case "Fists of Fire":
                    Core.HuntMonster("xancave", "Shurpu Ring Guardian", req.Name, isTemp: false);
                    break;

                case "Like a Battlemoglin":
                    Adv.BuyItem("ariapet", 12, req.Name);
                    break;

                case "Green Sockatana":
                    CyseroMerge.BuyAllMerge(req.Name);
                    break;

                case "Chainsaw Katana":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("darkoviahorde", "Zombie Dragon", req.Name, quant, isTemp: false);
                    break;

                case "Unarmed":
                    Adv.BuyItem(Bot.Map.Name, 1536, req.Name);
                    break;

                case "BaconCat Force Face":
                case "Kitty SkyFighter":
                    Adv.BuyItem("baconcatlair", 1260, req.Name);
                    break;

                case "Ebil Ninja":
                case "Ebil Ninja Hood":
                    ArtixWeddingMerge.BuyAllMerge(req.Name);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("42721", "Magentity", "Mode: [select] only\nShould the bot buy \"Magentity\" ?", false),
        new Option<bool>("42722", "Mrs. Cuddles Pet", "Mode: [select] only\nShould the bot buy \"Mrs. Cuddles Pet\" ?", false),
        new Option<bool>("42723", "Mrs. Cuddles on Your Head", "Mode: [select] only\nShould the bot buy \"Mrs. Cuddles on Your Head\" ?", false),
        new Option<bool>("42725", "Fists of Fierceness", "Mode: [select] only\nShould the bot buy \"Fists of Fierceness\" ?", false),
        new Option<bool>("42730", "Like A Pretty Battlemoglin", "Mode: [select] only\nShould the bot buy \"Like A Pretty Battlemoglin\" ?", false),
        new Option<bool>("42732", "Pink Sockatana", "Mode: [select] only\nShould the bot buy \"Pink Sockatana\" ?", false),
        new Option<bool>("42735", "Pink Chainsaw Katana", "Mode: [select] only\nShould the bot buy \"Pink Chainsaw Katana\" ?", false),
        new Option<bool>("42729", "Pink Unarmed", "Mode: [select] only\nShould the bot buy \"Pink Unarmed\" ?", false),
        new Option<bool>("42737", "Derp Llama Overlord", "Mode: [select] only\nShould the bot buy \"Derp Llama Overlord\" ?", false),
        new Option<bool>("42738", "Derp Llama Face", "Mode: [select] only\nShould the bot buy \"Derp Llama Face\" ?", false),
        new Option<bool>("42739", "Rose Ebil Ninja", "Mode: [select] only\nShould the bot buy \"Rose Ebil Ninja\" ?", false),
        new Option<bool>("42741", "Rose Ninja Hood", "Mode: [select] only\nShould the bot buy \"Rose Ninja Hood\" ?", false),
    };
}
