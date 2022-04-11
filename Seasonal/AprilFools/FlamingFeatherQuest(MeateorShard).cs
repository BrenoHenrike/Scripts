//cs_include Scripts/CoreBots.cs
using RBot;

public class FlamingFeatherQuest
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.Add("Meateor Shard");

        Core.SetOptions();

        CompleteQuest();

        Core.SetOptions(false);
    }

    public void CompleteQuest()
    {
        List<RBot.Items.ItemBase> RewardOptions = Core.EnsureLoad(8605).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (RBot.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        Core.AddDrop(Rewards);
        Bot.Options.AttackWithoutTarget = true;
        Core.EquipClass(ClassType.Solo);

        while (!Bot.ShouldExit() && (!Core.CheckInventory(Rewards) || !Bot.Inventory.ContainsHouseItem("Altar of Caladbacon")))
        {
            Core.EnsureAccept(8605);
            Core.Join("battleontown", "r9", "Right", publicRoom: true);
            while (!Bot.ShouldExit() && !Core.CheckInventory("Flaming Feather", 25))
            {
                Core.Jump("r9", "Right");
                Bot.Player.Attack("*");
            }
            Core.EnsureComplete(8605);
            Bot.Wait.ForPickup("*");
        }

        Bot.Options.AttackWithoutTarget = false;
    }
}