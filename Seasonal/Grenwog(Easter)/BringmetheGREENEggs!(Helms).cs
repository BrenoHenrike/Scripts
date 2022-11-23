//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class GreenEggs
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetGreenEggs();

        Core.SetOptions(false);
    }

    public void GetGreenEggs()
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(5785).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        Core.AddDrop(Rewards);

        if (Core.CheckInventory(Rewards))
        {
            Core.Logger("You already own these items, Stopping Bot.");
            return;
        }

        while (!Bot.ShouldExit && (!Core.CheckInventory(Rewards)))
        {
            Core.EnsureAccept(5785);
            Core.HuntMonster("GreenGuardWest", "Green Chick", "Green Chick");
            Core.GetMapItem(5223, 4, "GreenGuardWest");
            Core.EnsureCompleteChoose(5785);
            Bot.Wait.ForPickup("*");
        }
    }
}



