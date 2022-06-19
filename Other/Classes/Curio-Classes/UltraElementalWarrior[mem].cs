//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class UltraElementalWarrior
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetUEW();

        Core.SetOptions(false);
    }

    public void GetUEW()
    {
        if (!Core.IsMember)
        {
            Core.Logger("Class Requires member to buy without acs, sorry.");
            return;
        }
        
        if (Core.CheckInventory("Ultra Elemental Warrior"))
            return;

        Adv.BuyItem("Curio", 807, 22190);
        Adv.BuyItem("Curio", 807, 22191);
        Adv.BuyItem("Curio", 807, 22192);
        Adv.BuyItem("Curio", 807, 22193);
        Adv.BuyItem("Curio", 807, 22194);
        Adv.BuyItem("Curio", 807, 22187);
        Adv.BuyItem("Curio", 807, 22188);
        Adv.BuyItem("Curio", 807, 22189);
        Bot.Wait.ForItemBuy();

        Core.BuyItem("Curio", 810, "Ultra Elemental Warrior");
        Bot.Wait.ForItemBuy();

        Adv.rankUpClass("Ultra Elemental Warrior");
    }
}
