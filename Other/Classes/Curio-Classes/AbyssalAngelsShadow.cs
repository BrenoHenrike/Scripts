//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;
using RBot.Items;

public class AbyssalAngelsShadow
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetAbyssal();

        Core.SetOptions(false);
    }

    public void GetAbyssal(string ClassName = "Abyssal Angel", bool rankUpClass = true)
    {
        if (!Core.CheckInventory("Abyssal Angel"))
            Core.Logger($"this Bot Requires {ClassName}, you dont own it, stopping", stopBot: true);

        if (Core.CheckInventory("Abyssal Angel Shadow"))
            return;

        InventoryItem itemInv = Bot.Inventory.Items.First(i => i.Name.ToLower() == ClassName.ToLower() && i.Category == ItemCategory.Class);

        Adv.rankUpClass("Abyssal Angel");
        Core.BuyItem("curio", 1245, "Abyssal Angel Shadow");

        if (rankUpClass)
            Adv.rankUpClass("Abyssal Angel Shadow");
    }
}