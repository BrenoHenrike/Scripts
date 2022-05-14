//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Oddities.cs
using RBot;

public class OdditiesMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public Oddities Odd => new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CursedDollTassel();

        Core.SetOptions(false);
    }

    public void CursedDollTassel(int quant = 300)
    {
        if (Core.CheckInventory("Cursed Doll Tassel", quant))
            return;

        Odd.StoryLine();

        int currentQuant = Bot.Inventory.GetQuantity("Cursed Doll Tassel");
        Core.Logger($"Farming Cursed Doll Tassels ({currentQuant}/{quant})");
        Core.EquipClass(ClassType.Solo);

        Core.RegisterQuests(8667);
        while (!Core.CheckInventory("Cursed Doll Tassel", quant))
        {
            Core.KillMonster("oddities", "r2", "Left", "*", "Chipped Wood", 7);
            Core.KillMonster("oddities", "r6", "Left", "*", "Fuzz Tuff", 7);
            Core.KillMonster("oddities", "r10", "Left", "Cursed Spirit", "Doll Eyes", 7);
            Bot.Wait.ForPickup("Cursed Doll Tassel");
        }
        Core.CancelRegisteredQuests();
    }
}
