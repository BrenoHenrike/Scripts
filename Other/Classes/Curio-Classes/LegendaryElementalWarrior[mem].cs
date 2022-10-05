//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class LegendaryElementalWarrior
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetLEW();

        Core.SetOptions(false);
    }

    public void GetLEW(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Legendary Elemental Warrior"))
            return;
        if (!Core.IsMember)
        {
            Core.Logger("Class requires member to buy without ACs.");
            return;
        }

        Adv.BuyItem("Curio", 807, 22190);
        Adv.BuyItem("Curio", 807, 22191);
        Adv.BuyItem("Curio", 807, 22192);
        Adv.BuyItem("Curio", 807, 22193);
        Adv.BuyItem("Curio", 807, 22194);
        Adv.BuyItem("Curio", 807, 22187);
        Adv.BuyItem("Curio", 807, 22188);
        Adv.BuyItem("Curio", 807, 22189);

        Core.BuyItem("Curio", 809, "Legendary Elemental Warrior", shopItemID: 2412);

        if (rankUpClass)
            Adv.rankUpClass("Legendary Elemental Warrior");
    }
}
