//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs

using Skua.Core.Interfaces;

public class MindBreakingItIn
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetRewards();

        Core.SetOptions(false);
    }

    public void GetRewards()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(7672).Rewards;
        List<string> RewardsList = new List<string>();
        List<string> RewardList = RewardOptions.Select(x => x.Name).ToList();
        string[] Rewards = RewardList.ToArray();

        if (Core.CheckInventory(Rewards))
            return;

        if (!Core.CheckInventory("MindBreaker"))
        {
            //Member Bonus 12k AC Shop will check if the player have the achievment
            Adv.BuyItem("Battleon", 373, "MindBreaker");
            Adv.rankUpClass("MindBreaker");
        }

        for (int i = 0; i < Rewards.Count(); i++)
        {
            if (!Core.CheckInventory(Rewards[i]))
            {
                Core.Logger($"Getting {Rewards[i]}");
                //Mind-Breaking It In 7672
                Core.EnsureAccept(7672);
                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("Somnia", "Deorysa|Devourax|Subrysa", "Dream Devourers Vanquished", 50, false);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("Somnia", "NightWyrm", "Nightwyrm Vanquished");
                Core.EnsureCompleteChoose(7672, new[] { Rewards[i] });
            }
        }
        Core.ToBank(Rewards);

    }
}
