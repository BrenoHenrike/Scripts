//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/MayThe4th/MurderMoonStory.cs
using RBot;

public class MurderMoonMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public MurderMoon Moon = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CyberCrystal();

        Core.SetOptions(false);
    }

    public void CyberCrystal(int quant = 300)
    {
        if (Core.CheckInventory("Cyber Crystal", quant))
            return;

        Moon.MurderMoonStory();

        int currentQuant = Bot.Inventory.GetQuantity("Cyber Crystal");
        Core.Logger($"Farming Cyber Crystals ({currentQuant}/{quant})");
        Core.EquipClass(ClassType.Farm);

        Core.RegisterQuests(8065);
        while (!Core.CheckInventory("Cyber Crystal", quant))
        {
            Core.HuntMonster("murdermoon", "Tempest Soldier", "Tempest Soldier Badge", 5, log: false);
            Bot.Wait.ForPickup("Cyber Crystal");
        }
        Core.CancelRegisteredQuests();
    }
}
