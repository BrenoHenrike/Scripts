//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class ShadowedWarMageSet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        string[] rewards = {
            "Dual Shadow War-Mage Blades",
            "Shadowed War-Mage",
            "Shadowed War-Mage Back-blade",
            "Shadowed War-Mage Blade",
            "Shadowed War-Mage Hat",
            "Shadowed War-Mage Scarf",
            "Shadowed War-Mage Scarf + Hat",
            "Shadowed War-Mage Staff",
            "Shadowed War-Mage Staff Cape"
        };
        if (Core.CheckInventory(rewards))
            return;

        int count = 0;

        Core.CheckSpaces(ref count, rewards);
        Core.AddDrop(rewards);

        Core.EquipClass(ClassType.Solo);

        Bot.Events.ItemDropped += ItemDropped;
        Core.Logger($"Farm for the Shadowed War-Mage set started. Farming to get {rewards.Count() - count} more item" + ((rewards.Count() - count) > 1 ? "s" : ""));
        while (!Bot.ShouldExit && !Core.CheckInventory(rewards))
        {
            Core.HuntMonster("timestream", "ShadowKnight Gar", "*");
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
