/*
name: BattleGemsGameGearMerge
description: Farms "Golden Ticket" in /luck to buy items in the BattleGamesGameGearMerge
tags: merge, luck, battlegamesgamegear, merge
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BattleGemsGameGearMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Golden Ticket" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("luck"))
            return;

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("luck", 844, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Golden Ticket":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.Join("luck");
                        Core.SendPackets("%xt%zm%getMapItem%10173%101%");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("34308", "Battle Gems Hyena", "Mode: [select] only\nShould the bot buy \"Battle Gems Hyena\" ?", false),
        new Option<bool>("34309", "Battle Gems Troll", "Mode: [select] only\nShould the bot buy \"Battle Gems Troll\" ?", false),
        new Option<bool>("34310", "Battle Gems SnowWolf", "Mode: [select] only\nShould the bot buy \"Battle Gems SnowWolf\" ?", false),
        new Option<bool>("34311", "Battle Gems Minoootaur", "Mode: [select] only\nShould the bot buy \"Battle Gems Minoootaur\" ?", false),
        new Option<bool>("34312", "Battle Gems Ninetail", "Mode: [select] only\nShould the bot buy \"Battle Gems Ninetail\" ?", false),
        new Option<bool>("34313", "Battle Gems Gorillaphant", "Mode: [select] only\nShould the bot buy \"Battle Gems Gorillaphant\" ?", false),
        new Option<bool>("34314", "Battle Gems Anubus", "Mode: [select] only\nShould the bot buy \"Battle Gems Anubus\" ?", false),
        new Option<bool>("34315", "Battle Gems DeathKnight", "Mode: [select] only\nShould the bot buy \"Battle Gems DeathKnight\" ?", false),
        new Option<bool>("34316", "Battle Gems Dimetrodon", "Mode: [select] only\nShould the bot buy \"Battle Gems Dimetrodon\" ?", false),
        new Option<bool>("34317", "Battle Gems Werewolf", "Mode: [select] only\nShould the bot buy \"Battle Gems Werewolf\" ?", false),
        new Option<bool>("23200", "Citrine Poleaxe", "Mode: [select] only\nShould the bot buy \"Citrine Poleaxe\" ?", false),
        new Option<bool>("23201", "Ruby Poleaxe", "Mode: [select] only\nShould the bot buy \"Ruby Poleaxe\" ?", false),
        new Option<bool>("23202", "Amethyst Poleaxe", "Mode: [select] only\nShould the bot buy \"Amethyst Poleaxe\" ?", false),
        new Option<bool>("23203", "Sapphire Poleaxe", "Mode: [select] only\nShould the bot buy \"Sapphire Poleaxe\" ?", false),
        new Option<bool>("23204", "Emerald Poleaxe", "Mode: [select] only\nShould the bot buy \"Emerald Poleaxe\" ?", false),
        new Option<bool>("23205", "Citrine Axe", "Mode: [select] only\nShould the bot buy \"Citrine Axe\" ?", false),
        new Option<bool>("23206", "Ruby Axe", "Mode: [select] only\nShould the bot buy \"Ruby Axe\" ?", false),
        new Option<bool>("23207", "Amethyst Axe", "Mode: [select] only\nShould the bot buy \"Amethyst Axe\" ?", false),
        new Option<bool>("23208", "Sapphire Axe", "Mode: [select] only\nShould the bot buy \"Sapphire Axe\" ?", false),
        new Option<bool>("23288", "Emerald Axe", "Mode: [select] only\nShould the bot buy \"Emerald Axe\" ?", false),
    };
}
