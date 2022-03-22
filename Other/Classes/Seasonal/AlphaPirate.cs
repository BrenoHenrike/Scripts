//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;
using RBot.Items;
using RBot.Quests;
using RBot.Shops;

public class AlphaPirate
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

        GetAlphaPirate();

        Core.SetOptions(false);
    }

    public void GetAlphaPirate(bool rankUpClass = true)
    {
        if (!Core.CheckInventory("Alpha Pirate Class Token"))
            return;

        Core.BuyItem("blazebeard", 108, "Alpha Pirate");

        if (rankUpClass)
            Adv.rankUpClass("Alpha Pirate");
    }
}