//cs_include Scripts/CoreBots.cs
using RBot;

public class BlueEggs
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetBlueEggs();

        Core.SetOptions(false);
    }

    public void GetBlueEggs()
    {
        List<RBot.Items.ItemBase> RewardOptions = Core.EnsureLoad(5786).Rewards;
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
            Core.EnsureAccept(5786);
            Core.HuntMonster("EarthStorm", "Blue Chick", "Blue Chick");
            Core.GetMapItem(5224, 6, "EarthStorm");
            Core.EnsureCompleteChoose(5786);
            Bot.Wait.ForPickup("*");
        }
    }
}



