//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class BlackFridayAlphaHunterRogue
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public List<IOption> Options = new()
    {
        new Option<bool>("toBank", "Bank Items", "Bank Items after you're done?", true)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetSets(Bot.Config!.Get<bool>("toBank"));

        Core.SetOptions(false);
    }

    public void GetSets(bool toBank = true)
    {
        var AllRewards = Core.EnsureLoad(6104).Rewards;
        AllRewards.AddRange(Core.EnsureLoad(6106).Rewards);
        var AllRewardsArray = AllRewards.Select(x => x.ID).ToArray();
        if (Core.CheckInventory(AllRewardsArray))
            return;

        Core.EquipClass(ClassType.Solo);

        Core.RegisterQuests(Core.FromTo(6104, 6407));
        while (!Bot.ShouldExit && !Core.CheckInventory(AllRewardsArray))
            Core.KillMonster("blackfridaywar", "r4", "Left", "*", log: false);

        if (toBank)
            Core.ToBank(AllRewardsArray);
    }
}
