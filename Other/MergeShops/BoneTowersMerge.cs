//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs

using RBot;
using RBot.Items;
using RBot.Options;

public class TowersMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreToD TOD = new();

    public List<IOption> Options = sAdv.MergeOptions;
    // [Can Change] This should only be changed by the author.
    //              Just name this the same as the script (without the .cs)
    public string OptionsStorage = "testMerge";
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
        TOD.BoneTowerAll();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("towersilver", 1243, findIngredients);

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
                case "SilverSkull Amulet":
                    Core.RegisterQuests(5009);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("towersilver", "Fallen DeathKnight", "Chef Ramskull's Apron");
                        Core.HuntMonster("towersilver", "Undead Knight", "Chef Ramskull's Hat");
                        Core.HuntMonster("towersilver", "Undead Warrior", "Chef Ramskull's Cookbook");
                        Core.HuntMonster("towersilver", "Ghoul", "Chef Ramskull's Spatula");
                        Core.HuntMonster("towersilver", "Undead Guard", "Chef Ramskull's Skillet");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "GoldSkull Amulet":
                    Core.RegisterQuests(5023);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("towergold", "Book Maggot", "Book Pages", 10);
                        Core.HuntMonster("towergold", "Vampire Bat", "Batwing Leather");
                        Core.HuntMonster("towergold", "Skullspider", "Skullspider Silk", 3);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Bonecastle Amulet":
                    Core.RegisterQuests(4993);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("bonecastle", "Green Rat", "Gamey Rat Meat", 3);
                        Core.HuntMonster("bonecastle", "Undead Waiter", "Waiter's Notepad");
                        Core.HuntMonster("bonecastle", "Turtle", "Turtle's Eggs", 6);
                        Core.HuntMonster("bonecastle", "Ghoul", "Ghoul \"Vinegar\"", 6);
                        Core.HuntMonster("bonecastle", "Grateful Undead", "Spices", 2);
                        Core.HuntMonster("bonecastle", "The Butcher", "Bag of Bone Flour");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "DeathKnight Lord Gauntlets":
                    Core.AddDrop("DeathKnight Lord Gauntlets", "DeathKnight Lord Greaves", "DeathKnight Lord Chest Plate", "DeathKnight Lord Hauberk", "DeathKnight Lord Boots");
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("bonecastle", "Vaden", "DeathKnight Lord Gauntlets");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "DeathKnight Lord Greaves":
                    Core.AddDrop("DeathKnight Lord Gauntlets", "DeathKnight Lord Greaves", "DeathKnight Lord Chest Plate", "DeathKnight Lord Hauberk", "DeathKnight Lord Boots");
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("bonecastle", "Vaden", "DeathKnight Lord Greaves");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "DeathKnight Lord Chest Plate":
                    Core.AddDrop("DeathKnight Lord Gauntlets", "DeathKnight Lord Greaves", "DeathKnight Lord Chest Plate", "DeathKnight Lord Hauberk", "DeathKnight Lord Boots");
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("bonecastle", "Vaden", "DeathKnight Lord Chest Plate");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "DeathKnight Lord Hauberk":
                    Core.AddDrop("DeathKnight Lord Gauntlets", "DeathKnight Lord Greaves", "DeathKnight Lord Chest Plate", "DeathKnight Lord Hauberk", "DeathKnight Lord Boots");
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("bonecastle", "Vaden", "DeathKnight Lord Hauberk");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "DeathKnight Lord Boots":
                    Core.AddDrop("DeathKnight Lord Gauntlets", "DeathKnight Lord Greaves", "DeathKnight Lord Chest Plate", "DeathKnight Lord Hauberk", "DeathKnight Lord Boots");
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("bonecastle", "Vaden", "DeathKnight Lord Boots");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Gold DeathKnight Lord Gauntlets":
                    Core.AddDrop("Gold DeathKnight Lord Gauntlets", "Gold DeathKnight Lord Greaves", "Gold DeathKnight Lord Chest Plate", "Gold DeathKnight Lord Hauberk", "Gold DeathKnight Lord Boots");
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Towergold", "Yurrod the Gold", "Gold DeathKnight Lord Gauntlets");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Gold DeathKnight Lord Greaves":
                    Core.AddDrop("Gold DeathKnight Lord Gauntlets", "Gold DeathKnight Lord Greaves", "Gold DeathKnight Lord Chest Plate", "Gold DeathKnight Lord Hauberk", "Gold DeathKnight Lord Boots");
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Towergold", "Yurrod the Gold", "Gold DeathKnight Lord Greaves");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Gold DeathKnight Lord Chest Plate":
                    Core.AddDrop("Gold DeathKnight Lord Gauntlets", "Gold DeathKnight Lord Greaves", "Gold DeathKnight Lord Chest Plate", "Gold DeathKnight Lord Hauberk", "Gold DeathKnight Lord Boots");
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Towergold", "Yurrod the Gold", "Gold DeathKnight Lord Chest Plate");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Gold DeathKnight Lord Hauberk":
                    Core.AddDrop("Gold DeathKnight Lord Gauntlets", "Gold DeathKnight Lord Greaves", "Gold DeathKnight Lord Chest Plate", "Gold DeathKnight Lord Hauberk", "Gold DeathKnight Lord Boots");
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Towergold", "Yurrod the Gold", "Gold DeathKnight Lord Hauberk");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Gold DeathKnight Lord Boots":
                    Core.AddDrop("Gold DeathKnight Lord Gauntlets", "Gold DeathKnight Lord Greaves", "Gold DeathKnight Lord Chest Plate", "Gold DeathKnight Lord Hauberk", "Gold DeathKnight Lord Boots");
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Towergold", "Yurrod the Gold", "Gold DeathKnight Lord Boots");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Silver DeathKnight Lord Gauntlets":
                    Core.AddDrop("Silver DeathKnight Lord Gauntlets", "Silver DeathKnight Lord Greaves", "Silver DeathKnight Lord Chest Plate", "Silver DeathKnight Lord Hauberk", "Silver DeathKnight Lord Boots");
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Towersilver", "Flester the Silver", "Gold DeathKnight Lord Gauntlets");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Silver DeathKnight Lord Greaves":
                    Core.AddDrop("Silver DeathKnight Lord Gauntlets", "Silver DeathKnight Lord Greaves", "Silver DeathKnight Lord Chest Plate", "Silver DeathKnight Lord Hauberk", "Silver DeathKnight Lord Boots");
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Towersilver", "Flester the Silver", "Gold DeathKnight Lord Greaves");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Silver DeathKnight Lord Chest Plate":
                    Core.AddDrop("Silver DeathKnight Lord Gauntlets", "Silver DeathKnight Lord Greaves", "Silver DeathKnight Lord Chest Plate", "Silver DeathKnight Lord Hauberk", "Silver DeathKnight Lord Boots");
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Towersilver", "Flester the Silver", "Gold DeathKnight Lord Chest Plate");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Silver DeathKnight Lord Hauberk":
                    Core.AddDrop("Gold DeathKnight Lord Gauntlets", "Silver DeathKnight Lord Greaves", "Silver DeathKnight Lord Chest Plate", "Silver DeathKnight Lord Hauberk", "Silver DeathKnight Lord Boots");
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Towersilver", "Flester the Silver", "Silver DeathKnight Lord Hauberk");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "iSilver DeathKnight Lord Bootstem":
                    Core.AddDrop("Silver DeathKnight Lord Gauntlets", "Silver DeathKnight Lord Greaves", "Silver DeathKnight Lord Chest Plate", "Silver DeathKnight Lord Hauberk", "Silver DeathKnight Lord Boots");
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("Towersilver", "Flester the Silver", "Silver DeathKnight Lord Boots");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }
}