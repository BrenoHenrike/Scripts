//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class BattleConGear
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
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
        while (!Bot.ShouldExit && !Core.CheckInventory("DeadMog LED", 1000))
        {
            Core.EnsureAccept(4576);
            Core.HuntMonster("arena", "Deadmoglinster", "DeadMoglinster Defeated");
            Core.EnsureComplete(4576);
            Core.Logger($"DeadMog LED: {Bot.Inventory.GetQuantity("DeadMog LED")}/1000");
        }
    }
}