//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class TheLeeryContract
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    public CoreNation Nation = new();

    public bool DontPreconfigure = true;

    public string OptionsStorage = "The Leery Contract";

    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<RewardsSelection>("RewardSelect", "Choose Your Quest Reward", "Select Your Quest Reward for The Leary Contract.", RewardsSelection.Godly_Golden_Dragon_Axe)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        Core.BankingBlackList.AddRange(Rewards);
        QuestItems();

        Core.SetOptions(false);
    }

    public void QuestItems(RewardsSelection reward = RewardsSelection.All)
    {
        if (!Core.IsMember)
            return;

        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(554).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        ItemBase item = Core.EnsureLoad(554).Rewards.Find(x => x.ID == (int)Bot.Config.Get<RewardsSelection>("RewardSelect"));

        var Count = 0;
        int x = 1;

        if (item == null)
        {
            Core.Logger($"{item.Name} not found in Quest Rewards");
            return;
        }


        if (Core.CheckInventory(item.Name))
            return;

        while (!Core.CheckInventory(item.Name))
        {
            if (Bot.Config.Get<RewardsSelection>("RewardsSelection") == RewardsSelection.All)
                Core.Logger($"Farming All {x++}/{Count}");
            Core.Logger($"... {Bot.Config.Get<RewardsSelection>("RewardsSelection")} ...");

            Core.EnsureAccept(554);
            Nation.FarmUni13(1);
            Core.HuntMonster("EvilWarNul", "Undead Legend", "Undead Legend Rune");

            if (Bot.Config.Get<RewardsSelection>("RewardsSelection") != RewardsSelection.All)
                Core.EnsureComplete(554, item.ID);
            else Core.EnsureCompleteChoose(554);
        }
    }
    public readonly string[] Rewards = { "Ddog Sea Serpent Sword", "Godly Golden Dragon Axe", "Corpse Maker of Nulgath" };
    public enum RewardsSelection
    {
        Ddog_Sea_Serpent_Sword = 4766,
        Godly_Golden_Dragon_Axe = 4724,
        Corpse_Maker_of_Nulgath = 4764,


        All
    };
}