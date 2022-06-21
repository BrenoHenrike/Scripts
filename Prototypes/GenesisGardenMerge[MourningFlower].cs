//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Prototypes/GenesisGarden.cs
using RBot;

public class GenesisMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public GenesisGarden GG => new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        MourningFlower();

        Core.SetOptions(false);
    }

    public void MourningFlower(int quant = 1000)
    {
        if (Core.CheckInventory("Mourning Flower", quant))
            return;

        GG.DoAll();

        int currentQuant = Bot.Inventory.GetQuantity("Mourning Flower");
        Core.Logger($"Farming Mourning Flowers ({currentQuant}/{quant})");
        Core.EquipClass(ClassType.Solo);
        Core.AddDrop("Mourning Flower");

        Core.RegisterQuests(8688);
        while (!Bot.ShouldExit() && !Core.CheckInventory("Mourning Flower", quant))
        {
            Core.KillMonster("genesisgarden", "r6", "Left", "*", "Beast Subject", 7);
            Core.KillMonster("genesisgarden", "r9", "Left", "*", "Humanoid Subject", 7);
            Core.KillMonster("genesisgarden", "r11", "Left", "Ancient Mecha", "Replacement Parts", 7);
            Bot.Wait.ForPickup("Mourning Flower");
        }
        Core.CancelRegisteredQuests();
    }
}
