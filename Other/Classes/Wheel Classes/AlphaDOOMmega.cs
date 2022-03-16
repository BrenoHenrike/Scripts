//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;
using RBot.Items;
using RBot.Quests;
using RBot.Shops;

public class AlphaDOOMmega
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailys Dailys = new CoreDailys();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetAlphaDOOMmega();

        Core.SetOptions(false);
    }

    public void GetAlphaDOOMmega(bool rankUpClass = true, bool UsePots = false)
    {
        if (Core.CheckInventory("Alpha DOOMmega"))
            return;

        // if (Core.CheckInventory("Treasure Potion", 30))
        // {
        //     if (UsePots)
        //         Core.BuyItem("doom", 712, "Alpha DOOMmega");
        // } 
        //this can also be gotten with Treasure Pots but idt anyone wants todo that -- remove lines 34 - 39. if included change the false in the void to true.

        Dailys.WheelofDoom();
        if (!Core.CheckInventory("Alpha DOOMmega"))
            Core.Logger("You're not Lucky, Sorry");

        if (Core.CheckInventory("Alpha DOOMmega"))
        {
            if (rankUpClass)
                Adv.rankUpClass("Alpha DOOMmega");
        }
    }
}