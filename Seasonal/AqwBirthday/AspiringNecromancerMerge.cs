//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class AspiringNecromancerMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Necromancer’s Pride", "Necromancer’s Joy", "Necromancer’s Insanity " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        if (!Core.isSeasonalMapActive("birthday"))
            return;

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("battleontown", 1924, findIngredients);

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

                case "Necromancer’s Pride":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    //I Want to Be The Very Best Necromancer 7751
                    Core.RegisterQuests(7751);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("BattleunderA", "Skeletal Warrior", "Skeleton Captured", 10, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Necromancer’s Joy":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    //Like No One Ever Was 7752
                    Core.RegisterQuests(7752);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("DoomWood", "Doomwood Bonemuncher", "Bones Collected", 15, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Necromancer’s Insanity":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    //To Raise Them is my Real Quest 7753
                    Bot.Quests.UpdateQuest(2060);
                    Bot.Quests.UpdateQuest(3019);
                    Core.RegisterQuests(7753);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("necrodungeon", "r22", "Down", "*", "Dracolich Head", log: false, publicRoom: true);
                        Core.KillMonster("necrodungeon", "r22", "Down", "*", "Yet Another Dracolich Head", log: false, publicRoom: true);
                        Core.KillMonster("necrodungeon", "r22", "Down", "*", "More Dracolich Heads", log: false, publicRoom: true);
                        Core.HuntMonster("underrealm", "Agony", "Fresh Agony Wraps", 5, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("56830", "Ambitious Necromancer", "Mode: [select] only\nShould the bot buy \"Ambitious Necromancer\" ?", false),
        new Option<bool>("56831", "Ambitious Necromancer Hair", "Mode: [select] only\nShould the bot buy \"Ambitious Necromancer Hair\" ?", false),
        new Option<bool>("56832", "Ambitious Necromancer Locks", "Mode: [select] only\nShould the bot buy \"Ambitious Necromancer Locks\" ?", false),
        new Option<bool>("56833", "Ambitious Necromancer Hair + Scarf", "Mode: [select] only\nShould the bot buy \"Ambitious Necromancer Hair + Scarf\" ?", false),
        new Option<bool>("56834", "Ambitious Necromancer Locks + Scarf", "Mode: [select] only\nShould the bot buy \"Ambitious Necromancer Locks + Scarf\" ?", false),
        new Option<bool>("56835", "Ambitious Necromancer Hat + Locks", "Mode: [select] only\nShould the bot buy \"Ambitious Necromancer Hat + Locks\" ?", false),
        new Option<bool>("56836", "Ambitious Necromancer Hat", "Mode: [select] only\nShould the bot buy \"Ambitious Necromancer Hat\" ?", false),
        new Option<bool>("56837", "Ambitious Necro Lantern on your back", "Mode: [select] only\nShould the bot buy \"Ambitious Necro Lantern on your back\" ?", false),
        new Option<bool>("56838", "Ambitious Necro Dagger", "Mode: [select] only\nShould the bot buy \"Ambitious Necro Dagger\" ?", false),
        new Option<bool>("56840", "Ambitious Necro Lantern + Dagger", "Mode: [select] only\nShould the bot buy \"Ambitious Necro Lantern + Dagger\" ?", false),
        new Option<bool>("56841", "Deaddron", "Mode: [select] only\nShould the bot buy \"Deaddron\" ?", false),
    };
}
