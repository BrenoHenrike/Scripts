//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Other/Materials/DarknessShard.cs
//cs_include Scripts/CoreDailies.cs
using RBot;
using RBot.Items;
using RBot.Options;

public class LegionPyromancerMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreLegion Legion = new CoreLegion();
    public DarknessShard DShard = new();
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
        Adv.StartBuyAllMerge("underworld", 1734, findIngredients);

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

                case "Crystallized Blood":
                    Core.FarmingLogger($"{req.Name}", quant);
                    if (!Bot.Quests.IsAvailable(793))
                        return;
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6976);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("underworld", "Bloodfiend", "Fiend Blood", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Legion Token":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Legion.FarmLegionToken(quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Flaming Skull":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("underworld", "Frozen Pyromancer", req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Darkness Shard":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        DShard.GetShard(quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Flame-Forged Metal":
                    if (!Bot.Quests.IsAvailable(793))
                        return;
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6975);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("underworld", "Frozen Pyromancer", "Stolen Flame");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Soul-Forged Metal":
                    if (!Bot.Quests.IsAvailable(793))
                        return;
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(6977);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("underworld", "Frozen Pyromancer", "Pyromancer Soul Shard");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }
}
