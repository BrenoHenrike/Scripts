//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Oddities[Mem].cs
using RBot;

public class OdditiesMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public Oddities Odd => new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        SpookyFabricScrap_EerieEmbellishment();

        Core.SetOptions(false);
    }

    public void SpookyFabricScrap_EerieEmbellishment(int quant = 300)
    {
        if (Core.CheckInventory("Spooky Fabric Scrap", quant) && Core.CheckInventory("Eerie Embellishment", quant))
            return;

        Odd.StoryLine();

        int scrapQuant = Bot.Inventory.GetQuantity("Spooky Fabric Scrap");
        int eerieQuant = Bot.Inventory.GetQuantity("Eerie Embellishment");
        Core.Logger($"Farming Spooky Fabric Scraps ({scrapQuant}/{quant}) and Eerie Embellishments ({eerieQuant}/{quant})");
        Core.EquipClass(ClassType.Farm);
        Core.AddDrop("Spooky Fabric Scrap", "Eerie Embellishment");

        Core.RegisterQuests(8676, 8677);
        while (!Core.CheckInventory("Spooky Fabric Scrap", quant) && !Core.CheckInventory("Eerie Embellishment", quant))
        {
            Core.KillMonster("oddities", "r4", "Left", "*", "Cursed Cloth Roll", 13);
            Bot.Wait.ForPickup("Spooky Fabric Scrap");
            Core.KillMonster("oddities", "r4", "Left", "*", "Freaky Fripperies", 13);
            Bot.Wait.ForPickup("Eerie Embellishment");
        }
        Core.CancelRegisteredQuests();
    }
}
