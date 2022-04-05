//cs_include Scripts/CoreBots.cs
using RBot;

public class GardenQuestMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.Add("Vegetable Token");

        Core.SetOptions();

        GetVegetableTokens();

        Core.SetOptions(false);
    }

    public void GetVegetableTokens()
    {
        Core.AddDrop("Vegetable Token");
        Core.Logger($"Hunting For: Vegetable Token, {Bot.Inventory.GetQuantity("Vegetable Token")}/300");
        while (!Core.CheckInventory("Vegetable Token", 300))
        {
            
            Core.EnsureAccept(8002);
            Core.HuntMonster("GardenQuest", "Angry Broccoli", "Roasted Broccoli", log: false);
            Core.EnsureComplete(8002);
            Core.Logger($"Vegetable Token: {Bot.Inventory.GetQuantity("Vegetable Token")}/300");
        }
    }
}