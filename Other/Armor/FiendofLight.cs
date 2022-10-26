//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/SepulchureSaga/03SepulchuresRise.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FiendofLight
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    public SepulchuresRise SepulchureSaga = new();

    public bool DontPreconfigure = true;

    public string OptionsStorage = "RewardsSelect";

    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("SelectReward", "Choose the Reward?", "Select the Reward the Bot will Get, then stop.", false),
        new Option<bool>("AutoRewardChoice", "Let the bot do it?", "does the quest till you have all the rewards possible.", false),
        new Option<RewardsSelection>("RewardSelect", "Choose Your Reward", "", RewardsSelection.All)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();


        SepulchureSaga.StoryLine();
        if (!Bot.Config.Get<bool>("SelectReward"))
            ForeachSelect(6408);
        else OptionsSelect(Bot.Config.Get<RewardsSelection>("RewardSelect"), 6408);

        Core.SetOptions(false);
    }

    public void OptionsSelect(RewardsSelection reward = new(), int questID = 000)
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        ItemBase item = Core.EnsureLoad(questID).Rewards.Find(x => x.ID == (int)Bot.Config.Get<RewardsSelection>("RewardSelect"));

        if (item == null)
        {
            Core.Logger($"{item.Name} not found in Quest Rewards");
            return;
        }

        if (Core.CheckInventory(item.Name))
            return;

        while (!Core.CheckInventory(item.Name))
        {
            Core.EnsureAccept(questID);
            if (Bot.Config.Get<RewardsSelection>("RewardsSelection") != RewardsSelection.All)
                Core.EnsureComplete(questID, item.ID);
            else Core.EnsureComplete(questID);
        }
    }

    public void ForeachSelect(int questID)
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        Core.AddDrop(Rewards);

        Core.RegisterQuests(questID);
        foreach (string item in Rewards)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item))
            {
                while (!Bot.ShouldExit && !Core.CheckInventory(Rewards))
                    Core.HuntMonster("darkplane", "*", "Crystallized Memory", 10);
            }
            Core.CancelRegisteredQuests();
        }
    }

    public enum RewardsSelection
    {
        All,
        Fiend_of_Light = 44276,
        Fiend_of_Light_Helm = 44278,
        iend_of_Light_Hair = 44279,
        Fiend_of_Light_Winged_Hair = 44280,
        Fiend_of_Light_Blinded_Hair = 44281,
        Fiend_of_Light_Locks = 44282,
        Fiend_of_Light_Helm_Locks = 44283,
        Fiend_of_Light_Backblades = 44284,
        Fiend_of_Light_Wings = 44285,
        Fiend_of_Light_Wings_Tail = 44286,
        Fiend_of_Light_Tail = 44287,
        Fiend_of_Light_Blade = 44288,
        Doomed_Fiend_of_Light_Blade = 44289,
        Fiend_of_Light_Blades = 44290
    };
}