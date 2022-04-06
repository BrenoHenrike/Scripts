//cs_include Scripts/CoreBots.cs
using RBot;

public class BattleConGear
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.Add("DeadMog LED");

        Core.SetOptions();

        GetItems();

        Core.SetOptions(false);
    }

    public void GetItems()
    {
        Core.AddDrop("DeadMog LED");

        Core.Logger($"Hunting For: DeadMog LED, {Bot.Inventory.GetQuantity("DeadMog LED")}/1000");
        while (!Core.CheckInventory("DeadMog LED", 1000))
        {
            Core.EnsureAccept(4576);
            Core.HuntMonster("arena", "Deadmoglinster", "DeadMoglinster Defeated");
            Core.EnsureComplete(4576);
            Core.Logger($"DeadMog LED: {Bot.Inventory.GetQuantity("DeadMog LED")}/1000");
        }
    }
}