//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class CoinCollectorSet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetItems();

        Core.SetOptions(false);
    }

    public void GetItems()
    {
        Armor();
        Weapon();
        Helm();
    }

    public void Armor()
    {
        if (Core.CheckInventory("Coin Collector", toInv: false))
            return;

        Adv.BuyItem("hyperspace", 194, "Le Chocolat");
        Core.BuyItem("hollowhalls", 335, "Coin Collector");
        Core.ToBank("Coin Collector");
    }

    public void Weapon()
    {
        if (Core.CheckInventory("Coin Collector Gun", toInv: false))
            return;

        Adv.BuyItem("hyperspace", 194, "Chocolate Doubloon");
        Core.BuyItem("hollowhalls", 335, "Coin Collector Gun");
        Core.ToBank("Coin Collector Gun");
    }

    public void Helm()
    {
        if (Core.CheckInventory("Coin Collector Helmet", toInv: false))
            return;

        Adv.BuyItem("hyperspace", 194, "Chocolate Loonie");
        Core.BuyItem("hollowhalls", 335, "Coin Collector Helmet");
        Core.ToBank("Coin Collector Helmet");
    }
}
