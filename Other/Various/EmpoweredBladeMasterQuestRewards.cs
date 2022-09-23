//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/HeadOfTheLegionBeast.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Story/Legion/DageChallengeStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class EmpoweredBladeMaster
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    public CoreLegion Legion = new();
    public DageChallengeStory DageChallenge = new();

    public bool DontPreconfigure = true;

    public string OptionsStorage = "EmpoweredBladeMasterRewards";

    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("skipSetup", "Skip this window next time", "You will be able to return to this screen via [Options] -> [Script Options] if you wish to change anything.", false),
        new Option<bool>("SelectReward", "Choose the Reward?", "Select the Reward the Bot will Get, then stop.", false),
        new Option<bool>("AutoRewardChoice", "Let the bot do it?", "does the quest till you have all the rewards possible.", false),
        new Option<rewards>("RewardSelect", "Choose Your Reward", "", rewards.Empowered_Blade_Master)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        RequiredItems();
        DageChallenge.DageChallengeQuests();
        if (!Bot.Config.Get<bool>("SelectReward"))
            GetRewards();
        else OptionsSelect(8547);

        Core.SetOptions(false);
    }

    void RequiredItems()
    {
        if (Core.CheckInventory(new[] { "BladeMaster's Dual Katanas", "Living BladeMaster", "Dark Unicorn Rib" }))
            return;
        else Core.Logger("Required Items not found, Stopping", stopBot: true);
    }

    public void OptionsSelect(int questID, rewards reward = new())
    {
        if (!Bot.Config.Get<bool>("SkipOption"))
            Bot.Config.Configure();

        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        ItemBase item = Core.EnsureLoad(questID).Rewards.Find(x => x.ID == (int)Bot.Config.Get<rewards>("RewardSelect"));
        if (item == null)
            Core.Logger($"{item.Name} not found in Quest Rewards", stopBot: true);

        if (Core.CheckInventory(item.ID))
            return;

        Core.FarmingLogger(item.Name, 1);

        while (!Core.CheckInventory(item.Name))
        {
            Core.EnsureAccept(questID);
            Farm.Experience(95);
            Legion.FarmLegionToken(15000);
            DageInsignia(30);
            Core.EnsureAccept(questID, item.ID);
        }
    }

    public void GetRewards()
    {
        if (!Bot.Config.Get<bool>("SkipOption"))
            Bot.Config.Configure();

        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8554).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        Core.AddDrop(Rewards);

        Core.RegisterQuests(8554);
        foreach (string item in Rewards)
        {
            InventoryItem RewardChoice = Bot.Inventory.GetItem(item);

            while (!Bot.ShouldExit && !Core.CheckInventory(item))
            {
                Core.FarmingLogger(item, 1);
                DageInsignia(30);
                Core.EnsureAccept(8547);
                Farm.Experience(95);
                Legion.FarmLegionToken(15000);
                DageInsignia(30);
                Core.EnsureAccept(8547, RewardChoice.ID);
            }
            Core.CancelRegisteredQuests();
        }
    }

    public void DageInsignia(int quant)
    {
        //Dage the Evil Insignia
        Bot.Events.RunToArea += Event_RunToArea;
        if (!Core.CheckInventory("Dage the Evil Insignia", 30))
        {
            if (Bot.Quests.IsDailyComplete(8547))
                Core.Logger("Can't accept quest 8547 because the weekly is complete", messageBox: true, stopBot: true);
            Core.EnsureAccept(8547);
            Core.EquipClass(ClassType.Solo);

            Adv.BoostKillMonster("UltraDage", "Boss", "Right", "Dage the Dark Lord", "Dage the Dark Lord Defeated", isTemp: false, publicRoom: false);

            Core.EnsureComplete(8547);
            Bot.Wait.ForPickup("Dage the Evil Insignia");
        }
        Bot.Events.RunToArea -= Event_RunToArea;

        void Event_RunToArea(string zone)
        {
            switch (zone.ToLower())
            {
                case "a":
                    //Move to the left
                    Bot.Player.WalkTo(Bot.Random.Next(40, 175), Bot.Random.Next(400, 410), speed: 8);
                    break;
                case "b":
                    //Move to the right
                    Bot.Player.WalkTo(Bot.Random.Next(760, 930), Bot.Random.Next(410, 415), speed: 8);
                    break;
                default:
                    //Move to the center
                    Bot.Player.WalkTo(Bot.Random.Next(480, 500), Bot.Random.Next(300, 420), speed: 8);
                    break;
            }
        }
    }

    public enum rewards
    {
        Empowered_Blade_Master,
        Empowered_Blade_Masters_Katana,
        Empowered_Dual_Katanas
    };
}