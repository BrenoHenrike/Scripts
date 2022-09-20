//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/VasalkarLairWar.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class VoidBattleMageSet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public LairWar War = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetSet();

        Core.SetOptions(false);
    }

    public void GetSet()
    {
        string[] rewards = Core.EnsureLoad(6694).Rewards.Select(i => i.Name).ToArray();
        if (Core.CheckInventory(rewards))
            return;

        int count = 0;
        Core.CheckSpaces(ref count, rewards);
        Core.AddDrop(rewards);
        War.Attack();

        Core.RegisterQuests(6694);
        Bot.Events.ItemDropped += ItemDropped;
        Core.Logger($"Farm for Void BattleMage set started. Farming to get {rewards.Count() - count} more item" + ((rewards.Count() - count) > 1 ? "s" : ""));

        while (!Core.CheckInventory(rewards))
        {
            Core.KillMonster("lairattack", "Eggs", "Left", "Flame Dragon General", log: false);
            Bot.Wait.ForPickup("*");
        }

        Bot.Events.ItemDropped -= ItemDropped;
        Core.CancelRegisteredQuests();

        void ItemDropped(ItemBase item, bool addedToInv, int quantityNow)
        {
            if (rewards.Contains(item.Name))
            {
                count++;
                Core.Logger($"Got {item.Name}, {rewards.Length - count} items to go");
            }
        }
    }
}
