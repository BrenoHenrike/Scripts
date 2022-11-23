//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class TendurrrTheAssistantQuests
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();


    public string OptionsStorage = "Reward Select";
    public bool DontPreconfigure = false;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<RewardsSelection>("RewardsSelection", "Select Your Quest Reward", "Select Your Quest Reward for The Tendurr items of Nulgath quest.", RewardsSelection.Tendurrr_on_your_Back),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.AddRange(Rewards);
        Core.SetOptions();

        TendurrItems(Bot.Config.Get<RewardsSelection>("RewardsSelection"));

        Core.SetOptions(false);
    }

    public void TendurrItems(RewardsSelection reward = RewardsSelection.All)
    {
        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop(Rewards);

        if (!Core.CheckInventory("Tendurrr The Assistant"))
            Core.HuntMonster("tercessuinotlim", "Dark Makai", "Tendurrr The Assistant", 1, false);

        Nation.FarmUni13();


        var Count = 0;
        int x = 1;

        List<ItemBase> RewardOptions = Core.EnsureLoad(837).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);
        Count = RewardsList.Count();

        var rewards = Core.EnsureLoad(5814).Rewards;
        ItemBase item = rewards.Find(x => x.ID == (int)reward) ?? null;

        while (!Bot.ShouldExit &&
                (reward == RewardsSelection.All ?
                    Core.CheckInventory(rewards.Select(x => x.Name).ToArray(), toInv: false) :
                    !Core.CheckInventory(item.ID, toInv: false)
                )
              )
        {
            if (reward == RewardsSelection.All)
                Core.Logger($"Farming All {x++}/{Count}");
            else Core.Logger($"... {reward} ...");

            Core.EnsureAccept(5814);
            Nation.ApprovalAndFavor(500, 500);
            Nation.EssenceofNulgath(20);
            Nation.FarmGemofNulgath(30);
            Nation.FarmDarkCrystalShard(30);
            Nation.SwindleBulk(30);
            Nation.FarmDiamondofNulgath(30);
            Nation.FarmVoucher(member: true);
            Nation.FarmBloodGem(10);
            Nation.FarmTotemofNulgath(3);

            if (Bot.Config.Get<RewardsSelection>("RewardsSelection") == RewardsSelection.All)
                Core.EnsureCompleteChoose(5814);
            else Core.EnsureComplete(5814, (int)reward);
        }
    }

    public readonly string[] Rewards =
    {
        "Tendurrr on your Back",
        "Asuka's Flaming Stick",
        "Tendurrr's Flaming Stick",
        "Tendurrr Morph"
    };

    public enum RewardsSelection
    {
        Tendurrr_on_your_Back = 0,
        Asuka_Flaming_Stick = 1,
        Tendurrr_Flaming_Stick = 2,
        Tendurrr_Morph = 3,
        All
    };
}