//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/WarfuryTraining.cs
using RBot;
using RBot.Items;
using RBot.Options;

public class FirePlaneWarMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public WarTraining WT = new();
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
        Adv.StartBuyAllMerge("fireplanewar", 2006, findIngredients);

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

                case "Elemental Embers":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8125, 8126);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("fireplanewar", "r5", "Right", "*", "War Medal", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Burnt Cinders":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    WT.StoryLine();
                    Core.RegisterQuests(8131);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("fireplanewar", "ShadowClaw", "ShadowClaw Defeated", quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Seared Ashes":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(0000);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("fireplanewar", "ShadowFlame Phedra", req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "ShadowFlame Flamberge":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(0000);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("fireplanewar", 2007, req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Refulgent Flamberge":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(0000);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("fireplanewar", "Shadowflame Soldier", req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "ShadowFlame Great Harp":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(0000);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.BuyItem("fireplanewar", 2007, req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Vulcan Great Harp":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(0000);
                    while (!Bot.ShouldExit() && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("fireplanewar", "Shadefire Onslaught", req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }
}
