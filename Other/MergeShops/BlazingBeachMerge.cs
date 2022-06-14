//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;
using RBot.Items;
using RBot.Options;

public class BlazingBeachMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Options = sAdv.MergeOptions;
    // [Can Change] This should only be changed by the author.
    //              Just name this the same as the script (without the .cs)
    public string OptionsStorage = "Blazing Beach Merge";
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;


    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Quests();
        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void Quests()
    {        
        Story.PreLoad();
        Core.EquipClass(ClassType.Farm);
        // (Volca)No Trespassing
        Story.KillQuest(8702, "blazingbeach", "Burning Bombadier");

        // Piracy for Pyromancers
        Story.KillQuest(8703, "blazingbeach", new[] { "Magma Pirate", "Burning Bombadier", "Red-Hot Raider" });

        // Canned Heat
        Story.KillQuest(8704, "blazingbeach", "Burning Bombadier");
        Story.MapItemQuest(8704, "blazingbeach", 10252);

        // Dau Go
        if (!Story.QuestProgression(8705))
        {
            Core.EnsureAccept(8705);
            Core.HuntMonster("blazingbeach", "Dao Treeant", "Cavern Wood", 12, log: false);
            Core.HuntMonster("blazingbeach", "Burning Bombadier", "Redistributed Loot", 12, log: false);
            Core.HuntMonster("burningbeach", "Water Goblin", "Goblin Canteen", 5, log: false);
            Core.EnsureComplete(8705);
        }
        // Bãi Cháy
        if (!Story.QuestProgression(8706))
        {
            Core.EnsureAccept(8706);
            Core.HuntMonster("burningbeach", "Lavazard", "Lizard Lava", 5, log: false);
            Core.HuntMonster("burningbeach", "Lava Guardian", "Mage Magma", 5, log: false);
            Core.HuntMonster("blazingbeach", "Ruby Golem", "Flame Ruby", 3, log: false);

            Core.EnsureComplete(8706);
        }

        // Sung Sot
        Story.MapItemQuest(8707, "blazingbeach", 10253);
        Story.KillQuest(8707, "burningbeach", new[] { "Maladrite", "Shark" });

        Core.EquipClass(ClassType.Solo);
        // Ha Long   
        Story.KillQuest(8708, "blazingbeach", "Magma Blazebeard");
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("blazingbeach", 2138, findIngredients);

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

                case $"Mother Dragon’s Gift":
                    Core.RegisterQuests(8709);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    Core.EquipClass(ClassType.Farm);
                    while (!Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("blazingbeach", "Red-Hot Raider", "Raider Repelled", 10, log: false);
                        Core.HuntMonster("blazingbeach", "Scalding Shooter", "Sharpshooter Shooed", 8, log: false);
                        Core.HuntMonster("blazingbeach", "Burning Bombadier", "Bomber Bye-Byed", 6, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }
}