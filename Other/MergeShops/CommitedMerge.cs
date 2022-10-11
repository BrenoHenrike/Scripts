//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Asylum.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CommitedMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public Asylum Asylum = new();


    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Crawler Leg", "Zombie Dragon Scale", "De'Sawed's Stinger " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Asylum.StoryLine();
        Adv.StartBuyAllMerge("asylum", 506, findIngredients);

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

                case "Crawler Leg":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("mirrormaze", "Doom Crawler", req.Name, quant, false);
                    break;

                case "Zombie Dragon Scale":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("mirrormaze", "Zombie Dragon", req.Name, quant, false);
                    break;

                case "De'Sawed's Stinger":
                    Core.EquipClass(ClassType.Solo);
                    Core.KillMonster("catacombs", "Boss2", "Left", "Dr. De'Sawed", req.Name, quant, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("14525", "Mad Doctor", "Mode: [select] only\nShould the bot buy \"Mad Doctor\" ?", false),
        new Option<bool>("14534", "Scorpion Blades", "Mode: [select] only\nShould the bot buy \"Scorpion Blades\" ?", false),
        new Option<bool>("14536", "Mask of the Scorpion", "Mode: [select] only\nShould the bot buy \"Mask of the Scorpion\" ?", false),
        new Option<bool>("14538", "Staff of the Scorpion", "Mode: [select] only\nShould the bot buy \"Staff of the Scorpion\" ?", false),
        new Option<bool>("14539", "Scorpion Scythe", "Mode: [select] only\nShould the bot buy \"Scorpion Scythe\" ?", false),
        new Option<bool>("14545", "Bandaged Face", "Mode: [select] only\nShould the bot buy \"Bandaged Face\" ?", false),
        new Option<bool>("14549", "Mad Surgeon's Mask", "Mode: [select] only\nShould the bot buy \"Mad Surgeon's Mask\" ?", false),
    };
}
