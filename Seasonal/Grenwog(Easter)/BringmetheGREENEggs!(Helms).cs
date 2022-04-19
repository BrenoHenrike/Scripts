//cs_include Scripts/CoreBots.cs
using RBot;

public class GreenEggs
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetGreenEggs();

        Core.SetOptions(false);
    }

    public void GetGreenEggs()
    {
        List<RBot.Items.ItemBase> RewardOptions = Core.EnsureLoad(5785).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (RBot.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        Core.AddDrop(Rewards);

        if (Core.CheckInventory(Rewards))
        {
            Core.Logger("You already own these items, Stopping Bot.");
            return;
        }

        while (!Bot.ShouldExit() && (!Core.CheckInventory(Rewards)))
        {
            Core.EnsureAccept(5785);
            Core.HuntMonster("GreenGuardWest", "Green Chick", "Green Chick");
            Core.GetMapItem(5223, 4, "GreenGuardWest");
            Core.EnsureCompleteChoose(5785);
            Bot.Wait.ForPickup("*");
        }
    }
}



