//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class MmmmMeatyQuest
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.Add("Meaty Shard");

        Core.SetOptions();

        CompleteQuests();

        Core.SetOptions(false);
    }

    public void CompleteQuests()
    {
        List<RBot.Items.ItemBase> RewardOptions = Core.EnsureLoad(8613).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (RBot.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        Core.AddDrop(Rewards);
        Core.AddDrop(new[] { "Meaty Shard", "Meateor Shard" });

        Core.EquipClass(ClassType.Solo);

        if (!Story.QuestProgression(8612))
        {
            Core.EnsureAccept(8612);
            Core.HuntMonster("MeateorTown", "Giant ChickenCow", "ChickenCow Tamed", publicRoom: true);
            Core.EnsureComplete(8612);
        }

        Core.Join("MeateorTown", publicRoom: true);
        if (Bot.Map.PlayerCount > 6)
        {
            while (!Bot.ShouldExit() && (!Core.CheckInventory(Rewards)))
            {
                Core.EnsureAccept(8613);
                Core.HuntMonster("MeateorTown", "Giant ChickenCow", "Meaty Shard", publicRoom: true);
                Core.EnsureComplete(8613);
                Bot.Wait.ForPickup("*");
            }

        }

        else
        {
            Core.Join("Battleon");
            while (!Bot.ShouldExit() && (!Core.CheckInventory(Rewards)))
            {
                Core.EnsureAccept(8613);
                Core.HuntMonster("MeateorTown", "ChickenCow", "Meaty Shard");
                Core.EnsureComplete(8613);
                Bot.Wait.ForPickup("*");
            }
        }
    }
}