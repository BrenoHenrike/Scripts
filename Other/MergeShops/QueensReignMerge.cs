//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class QueensReignMerge
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
        Core.BankingBlackList.AddRange(new[] { "Ancient Hourglass", "LightningLord", "LightningLord Helm", "LightningLord Locks", "LightningLord Rune "});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("queenreign", 2058, findIngredients);

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

                case "Ancient Hourglass":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8326);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("queenreign", "Sa-Laatan", "Sa-Lataan Defeated");
                        Core.HuntMonster("queenreign", "Grou'luu", "Grou'luu Defeated");
                        Core.HuntMonster("queenreign", "Extriki", "Extriki Defeated");
                        Core.HuntMonster("queenreign", "Jaaku", "Jaaku Defeated");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "LightningLord":
                case "LightningLord Helm":
                case "LightningLord Locks":
                case "LightningLord Rune":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("queenreign", "Extriki", req.Name);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("64192", "Geomancer Armor", "Mode: [select] only\nShould the bot buy \"Geomancer Armor\" ?", false),
        new Option<bool>("64193", "Geomancer Hair", "Mode: [select] only\nShould the bot buy \"Geomancer Hair\" ?", false),
        new Option<bool>("64194", "Geomancer Cape", "Mode: [select] only\nShould the bot buy \"Geomancer Cape\" ?", false),
        new Option<bool>("64195", "Geomancer Dagger", "Mode: [select] only\nShould the bot buy \"Geomancer Dagger\" ?", false),
        new Option<bool>("64248", "Prismatic LightningLord", "Mode: [select] only\nShould the bot buy \"Prismatic LightningLord\" ?", false),
        new Option<bool>("64249", "Prismatic LightningLord Helm", "Mode: [select] only\nShould the bot buy \"Prismatic LightningLord Helm\" ?", false),
        new Option<bool>("64250", "Prismatic LightningLord Locks", "Mode: [select] only\nShould the bot buy \"Prismatic LightningLord Locks\" ?", false),
        new Option<bool>("64251", "Prismatic LightningLord Rune", "Mode: [select] only\nShould the bot buy \"Prismatic LightningLord Rune\" ?", false),
        new Option<bool>("64258", "Magistral Aeromancer", "Mode: [select] only\nShould the bot buy \"Magistral Aeromancer\" ?", false),
        new Option<bool>("64260", "Magistral Sage's Hood", "Mode: [select] only\nShould the bot buy \"Magistral Sage's Hood\" ?", false),
        new Option<bool>("64261", "Magistral's Velificatio", "Mode: [select] only\nShould the bot buy \"Magistral's Velificatio\" ?", false),
        new Option<bool>("64262", "Magistral's Divine Velificatio", "Mode: [select] only\nShould the bot buy \"Magistral's Divine Velificatio\" ?", false),
        new Option<bool>("64263", "Magistral's Longbow", "Mode: [select] only\nShould the bot buy \"Magistral's Longbow\" ?", false),
        new Option<bool>("64264", "Magistral's Bow + Arrow", "Mode: [select] only\nShould the bot buy \"Magistral's Bow + Arrow\" ?", false),
        new Option<bool>("64265", "Magistral's Weather Staff", "Mode: [select] only\nShould the bot buy \"Magistral's Weather Staff\" ?", false),
        new Option<bool>("64359", "Drenched Monk", "Mode: [select] only\nShould the bot buy \"Drenched Monk\" ?", false),
        new Option<bool>("64365", "Liquified Hood", "Mode: [select] only\nShould the bot buy \"Liquified Hood\" ?", false),
        new Option<bool>("64361", "Drenched Pigtails", "Mode: [select] only\nShould the bot buy \"Drenched Pigtails\" ?", false),
        new Option<bool>("64360", "Drenched Cut", "Mode: [select] only\nShould the bot buy \"Drenched Cut\" ?", false),
        new Option<bool>("64367", "Drenched Ringstaff", "Mode: [select] only\nShould the bot buy \"Drenched Ringstaff\" ?", false),
    };
}
