//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class DiabolicalWarden
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetDrops();

        Core.SetOptions(false);
    }

    public void GetDrops()
    {
        string[] rewards = {
            "Diabolical Warden",
            "Diabolical Warden's Hair",
            "Diabolical Warden's Twintails",
            "Diabolical Warden's Visage",
            "Diabolical Warden's Visage + Locks",
            "Diabolical Zealot's Locks",
            "Diabolical Zealot's Ponytail"
        };

        if (Core.CheckInventory(rewards))
        {
            Core.Logger("You already have all of the items.");
            return;
        }

        int count = 0;
        Core.CheckSpaces(ref count, rewards);
        Core.AddDrop(rewards);

        Core.EquipClass(ClassType.Solo);

        Bot.Quests.UpdateQuest(9044);
        Bot.Events.ItemDropped += ItemDropped;

        Core.Logger($"Farm for the Diabolical Warden set started. Farming to get {rewards.Count() - count} more item" + ((rewards.Count() - count) > 1 ? "s" : ""));
        while (!Bot.ShouldExit && !Core.CheckInventory(rewards))
        {
            Core.HuntMonster("brokenwoods", "Eldritch Amalgamation", "*", isTemp: false);
            Bot.Wait.ForPickup("*");
        }

        Bot.Events.ItemDropped -= ItemDropped;

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