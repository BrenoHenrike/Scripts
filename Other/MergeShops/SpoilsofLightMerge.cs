//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SpoilsofLightMerge
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
        Core.BankingBlackList.AddRange(
            new[]
            {
                "Apprentice of the Light",
                "Medal of Light",
                "Furred Ruff of the Light",
                "Apprentice of the Light Hair",
                "Apprentice of the Light Locks",
                "Medal of Honor",
                "Citadel's Light Blade",
                "Medal of Justice "
            }
        );
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("lightguardwar", 1643, findIngredients);

        #region Dont edit this part
        void findIngredients()
        {
            ItemBase req = Adv.externalItem;
            int quant = Adv.externalQuant;
            int currentQuant = req.Temp
                ? Bot.TempInv.GetQuantity(req.Name)
                : Bot.Inventory.GetQuantity(req.Name);
            if (req == null)
            {
                Core.Logger("req is NULL");
                return;
            }

            switch (req.Name)
            {
                default:
                    bool shouldStop = Adv.matsOnly ? !dontStopMissingIng : true;
                    Core.Logger(
                        $"The bot hasn't been taught how to get {req.Name}."
                            + (shouldStop ? " Please report the issue." : " Skipping"),
                        messageBox: shouldStop,
                        stopBot: shouldStop
                    );
                    break;
                #endregion

                case "Apprentice of the Light":
                case "Furred Ruff of the Light":
                case "Apprentice of the Light Hair":
                case "Apprentice of the Light Locks":
                case "Citadel's Light Blade":
                case "Medal of Light":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6560, 6561);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster(
                            "lightguardwar",
                            "Citadel Crusader|Lightguard Cast",
                            "Lightguard Medals",
                            5
                        );
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
                   
                case "Medal of Honor":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6562, 6563);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster(
                            "lightguardwar",
                            "Citadel Crusader|Lightguard Cast",
                            "Bone Marrow",
                            3
                        );
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Medal of Justice":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6566);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster(
                            "lightguardwar",
                            "Citadel Crusader|Lightguard Paladin",
                            "Gunpowder",
                            3
                        );
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select =
        new()
        {
            new Option<bool>("45396","Crusader of the Light","Mode: [select] only\nShould the bot buy \"Crusader of the Light\" ?",false),
            new Option<bool>("45399","Furred Cape of the Light","Mode: [select] only\nShould the bot buy \"Furred Cape of the Light\" ?",false),
            new Option<bool>("45397","Runed Crusader Morph","Mode: [select] only\nShould the bot buy \"Runed Crusader Morph\" ?",false),
            new Option<bool>("45398","Runed Crusader Locks","Mode: [select] only\nShould the bot buy \"Runed Crusader Locks\" ?",false),
            new Option<bool>("45400","High Crusader","Mode: [select] only\nShould the bot buy \"High Crusader\" ?",false),
            new Option<bool>("45401","High Crusader Morph","Mode: [select] only\nShould the bot buy \"High Crusader Morph\" ?",false),
            new Option<bool>("45402","High Crusader Locks","Mode: [select] only\nShould the bot buy \"High Crusader Locks\" ?",false),
            new Option<bool>("45404","High Crusader Light Blade","Mode: [select] only\nShould the bot buy \"High Crusader Light Blade\" ?",false),
            new Option<bool>("45403","Blade + Ruff of the Light","Mode: [select] only\nShould the bot buy \"Blade + Ruff of the Light\" ?",false),
            new Option<bool>("45405","Inquisitor of the Light","Mode: [select] only\nShould the bot buy \"Inquisitor of the Light\" ?",false),
            new Option<bool>("45406","Inquisitor's Helm of the Light","Mode: [select] only\nShould the bot buy \"Inquisitor's Helm of the Light\" ?",false),
            new Option<bool>("45407","Inquisitor's Locks of the Light","Mode: [select] only\nShould the bot buy \"Inquisitor's Locks of the Light\" ?",false),
            new Option<bool>("45409","Inquisitor's Bright Blade","Mode: [select] only\nShould the bot buy \"Inquisitor's Bright Blade\" ?",false),
            new Option<bool>("45408","Ruff, Blade and Cape of the Light","Mode: [select] only\nShould the bot buy \"Ruff, Blade and Cape of the Light\" ?",false),
        };
}
