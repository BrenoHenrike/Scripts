//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/MustyCave.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class GrimDarkSet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public MustyCave Cave = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        string[] rewards = Core.EnsureLoad(7049).Rewards.Select(i => i.Name).ToArray();

        if (Core.CheckInventory(rewards))
            return;

        int count = 0;

        Core.CheckSpaces(ref count, rewards);
        Core.AddDrop(rewards);

        Cave.Storyline();

        Core.RegisterQuests(7049);
        Bot.Events.ItemDropped += ItemDropped;
        Core.Logger($"Farm for the DarkMage set started. Farming to get {rewards.Count() - count} more item" + ((rewards.Count() - count) > 1 ? "s" : ""));

        while (!Bot.ShouldExit && !Core.CheckInventory(rewards))
        {
            Core.HuntMonster("mustycave", "Mogdring", "Golden Gear", 5, false);
            Core.HuntMonster("mustycave", "Spy Drone", "Aura Core", 25, false);
            Core.HuntMonster("mustycave", "Guard Drone", "Dimension Stabilizer", 35, false);
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
