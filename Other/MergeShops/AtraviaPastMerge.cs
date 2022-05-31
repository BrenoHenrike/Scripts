//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using RBot;
using RBot.Items;
using RBot.Options;

public class AtraviaPastMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreDarkon Darkon = new CoreDarkon();
    public CoreAstravia CoreAstravia = new();

    public List<IOption> Options = sAdv.MergeOptions;
    // [Can Change] This should only be changed by the author.
    //              Just name this the same as the script (without the .cs)
    public string OptionsStorage = "AtraviaPastMerge";
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("astraviapast", 2126, findIngredients);

        #region Dont edit this part
        void findIngredients()
        {
            ItemBase req = Adv.externalItem;
            int quant = Adv.externalQuant;
            int currentQuant = req.Temp ? Bot.Inventory.GetTempQuantity(req.Name) : Bot.Inventory.GetQuantity(req.Name);
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

                // Add how to get items here
                case "Suki's Prestige":
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Core.CheckInventory(req.Name, quant))
                        Darkon.SukisPrestiege(quant);
                    break;

                case "Prince Drago's Attire":
                case "Prince Drago's Hair":
                case "Prince Drago's Dark Attire":
                    Core.HuntMonster("astraviapast", "Forsaken Husk", req.Name, isTemp: false);
                    break;

                case "Suki's Casual Armor":
                    Core.HuntMonster("astraviapast", "Aurola", req.Name, isTemp: false);
                    break;

                case "Regulus' Hair":
                    Core.HuntMonster("astraviapast", "Regulus", req.Name, isTemp: false);
                    break;

                case "Suki's Ponytail":
                    Core.HuntMonster("astraviapast", "Aurola", req.Name, isTemp: false);
                    break;

                case "Titania's Hair":
                    Core.HuntMonster("astraviapast", "Titania", req.Name, isTemp: false);
                    break;
            }
        }
    }
}