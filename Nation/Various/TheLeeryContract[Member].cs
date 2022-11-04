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

        var RewardOptions = Core.EnsureLoad(554).Rewards.Select(x => x.Name).ToArray();
        Core.AddDrop(RewardOptions);
        bool getAll = (int)Bot.Config.Get<RewardsSelection>("RewardSelect") == 9999;

        ItemBase item = null;
        if (!getAll)
        {
            item = Core.EnsureLoad(554).Rewards.Find(x => x.ID == (int)Bot.Config.Get<RewardsSelection>("RewardSelect"));
            if (item == null)
            {
                Core.Logger($"{item.Name} not found in Quest Rewards");
                return;
            }
            if (Core.CheckInventory(item.Name))
                return;
            Core.FarmingLogger(item.Name, 1);
        }

        while (getAll ? !Core.CheckInventory(RewardOptions) : !Core.CheckInventory(item.Name))
        {
            Core.EnsureAccept(554);
            Nation.FarmUni13(1);
            Core.HuntMonster("EvilWarNul", "Undead Legend", "Undead Legend Rune");

            if (!getAll)
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


        All = 9999,
    };
}