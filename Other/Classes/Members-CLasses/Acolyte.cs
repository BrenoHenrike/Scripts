//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;
using RBot.Items;

public class Acolyte
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

        GetAcolyte();

        Core.SetOptions(false);
    }

    public void GetAcolyte(bool rankUpClass = true)
    {
        Core.Logger($"Acolyte is locked behind /trainers and is inaccessable during the April Fools event. Stopping bot out of safety", messageBox: true, stopBot: true);
        if (!Core.IsMember)
            return;

        if (Core.CheckInventory("Acolyte"))
            return;

        if (Core.CheckInventory("Healer"))
        {
            Core.BuyItem("trainers", 176, "Healer");
            Adv.rankUpClass("Healer");
            Core.BuyItem("trainers", 177, "Acolyte");
            if (rankUpClass)
                Adv.rankUpClass("Acolyte");
        }
    }
}