//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class RewardSelectTemplate
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();

    public bool DontPreconfigure = true;

    public string OptionsStorage = "EmpoweredBladeMasterRewardsTemplate";

    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("SelectReward", "Choose the Reward?", "Select the Reward the Bot will Get, then stop.", false),
        new Option<bool>("AutoRewardChoice", "Let the bot do it?", "does the quest till you have all the rewards possible.", false),
        new Option<Template>("RewardSelect", "Choose Your Reward", "", Template.All)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        RequiredItems();
        QuestsIfNeededSelect();
        if (!Bot.Config.Get<bool>("SelectReward"))
            ForeachSelect(0000);
        else OptionsSelect(Bot.Config.Get<Template>("RewardSelect"));

        QuestsIfNeededSelect();
        OptionsSelect(Bot.Config.Get<Template>("RewardSelect"));

        Core.SetOptions(false);
    }

    public void QuestsIfNeededSelect()
    {
        //Import from other scritps as you normaly would.
        // Classname.Import
    }


    void RequiredItems()
    {
        if (Core.CheckInventory(new[] { "item", "item", "etc" }))
            return;
        else Core.Logger("Required Items not found, Stopping", stopBot: true);
    }

    public void OptionsSelect(Template reward = new(), int questID = 000)
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        ItemBase item = Core.EnsureLoad(questID).Rewards.Find(x => x.ID == (int)Bot.Config.Get<Template>("RewardSelect"));

        if (item == null)
            Core.Logger($"{item.Name} not found in Quest Rewards", stopBot: true);

        if (Core.CheckInventory(item.Name))
            return;

        while (!Core.CheckInventory(item.Name))
        {
            Core.EnsureAccept(questID);
            //Questing stuff here --
            if (Bot.Config.Get<Template>("RewardsSelection") != Template.All)
                Core.EnsureComplete(554, item.ID);
            else Core.EnsureComplete(questID, item.ID);
        }
    }

    public void ForeachSelect(int questID)
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        Core.AddDrop(Rewards);

        Core.RegisterQuests(questID);
        foreach (string item in Rewards)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item))
            {
                //Questing stuff here --
            }
            Core.CancelRegisteredQuests();
        }
    }

    public enum Template
    {
        All,
        Reward1 = 1,
        Reward2 = 2,
        Reward3 = 3,
        Reward4 = 4,
    };
}