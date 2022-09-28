//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/QueenReign.cs
using Skua.Core.Interfaces;

public class QueenReignMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public QueenReign QR => new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        AncientHourglass();

        Core.SetOptions(false);
    }

    public void AncientHourglass(int quant = 300)
    {
        if (Core.CheckInventory("Ancient Hourglass", quant))
            return;

        QR.DoAll();

        int currentQuant = Bot.Inventory.GetQuantity("Ancient Hourglass");
        Core.Logger($"Farming Ancient Hourglasses ({currentQuant}/{quant})");
        Core.EquipClass(ClassType.Solo);

        Core.RegisterQuests(8326);
        while (!Bot.ShouldExit && !Core.CheckInventory("Ancient Hourglass", quant))
        {
            Core.HuntMonster("queenreign", "Sa-Laatan", "Sa-Lataan Defeated");
            Core.HuntMonster("queenreign", "Grou'luu", "Grou'luu Defeated");
            Core.HuntMonster("queenreign", "Extriki", "Extriki Defeated");
            Core.HuntMonster("queenreign", "Jaaku", "Jaaku Defeated");
            Bot.Wait.ForPickup("Ancient Hourglass");
        }
        Core.CancelRegisteredQuests();


    }
}
