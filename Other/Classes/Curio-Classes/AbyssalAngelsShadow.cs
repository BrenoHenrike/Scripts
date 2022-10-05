//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class AbyssalAngelsShadow
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAbyssal();

        Core.SetOptions(false);
    }

    public void GetAbyssal(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Abyssal Angel Shadow"))
            return;
        if (!Core.CheckInventory("Abyssal Angel"))
        {
            Core.Logger($"This bot requires \"Abyssal Angel\", stopping the bot");
            return;
        }

        Adv.rankUpClass("Abyssal Angel");
        Core.BuyItem("curio", 1245, "Abyssal Angel Shadow");

        if (rankUpClass)
            Adv.rankUpClass("Abyssal Angel Shadow");
    }
}