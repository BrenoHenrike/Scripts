//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;
using RBot.Items;
using RBot.Options;

public class CraggleRockMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Options = sAdv.MergeOptions;
    public string OptionsStorage = sAdv.OptionsStorage;
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
        Adv.StartBuyAllMerge("cragglerock", 1819, findIngredients);

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

                case "Empowered Voidstone":
                    Core.RegisterQuests(7277);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("wanders", "Kalestri Worshiper", "Star of the Sandsea");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Ice Diamond":
                    Core.RegisterQuests(7279);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("kingcoal", "Snow Golem", "Frozen Coal", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Dark Bloodstone":
                    Core.RegisterQuests(7281);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("safiria", "Blood Maggot", "Blood Gem", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Butterfly Sapphire":
                    Core.RegisterQuests(7287);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("bloodtusk", "Trollola Plant", "Butterfly Bloom", 15);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Understone":
                    Core.RegisterQuests(7289);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("battleunderc", "Enter", "Spawn", "*", "Fluorite Shard", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Rainbow Moonstone":
                    Core.RegisterQuests(7291);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("earthstorm", "Diamond Golem", "Chip of Diamond");
                        Core.HuntMonster("earthstorm", "Emerald Golem", "Chip of Emerald");
                        Core.HuntMonster("earthstorm", "Ruby Golem", "Chip of Ruby");
                        Core.HuntMonster("earthstorm", "Sapphire Golem", "Chip of Sapphire");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }
}
