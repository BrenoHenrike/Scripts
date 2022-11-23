//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class LoreMasterUniversity
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        University();

        Core.SetOptions(false);
    }

    public void University()
    {
        if (Core.CheckInventory("Loremaster University"))
            return;

        Library();
        getHouse("University", 6331, 5, 40, 50, 9, 6);
    }

    public void Library()
    {
        if (Core.CheckInventory("Loremaster Library"))
            return;

        Cottage();
        getHouse("Library", 6330, 2, 30, 40, 6, 4);
    }

    public void Cottage()
    {
        if (Core.CheckInventory("Loremaster Cottage"))
            return;

        Hutch();
        getHouse("Cottage", 6329, 1, 20, 30, 3, 2);
    }

    public void Hutch()
    {
        if (Core.CheckInventory("Loremaster Hutch"))
            return;

        Tent();
        getHouse("Hutch", 6328, 1, 10, 20, 2);
    }

    public void Tent()
    {
        if (Core.CheckInventory("Loremaster Tent"))
            return;

        Core.AddDrop("Loremaster Tent");
        Core.FarmingLogger("Loremaster Tent", 1);
        Mats(1, 5, 10);
        Core.BuyItem("librarium", 1403, "Loremaster Tent");
    }

    private void getHouse(string type, int quest, int fabric, int board, int nail, int paint = 0, int varnish = 0)
    {
        Core.AddDrop("Loremaster " + type);
        Core.FarmingLogger("Loremaster " + type, 1);
        Farm.LoremasterREP(Core.RepCPLevel.First(x => x.Key == Core.EnsureLoad(quest).RequiredFactionRep).Value);
        Core.EnsureAccept(quest);
        Mats(fabric, board, nail, paint, varnish);
        Core.EnsureComplete(quest);
        Bot.Wait.ForPickup("Loremaster " + type);
    }

    private void Mats(int fabric, int board, int nail, int paint = 0, int varnish = 0)
    {
        Core.HuntMonster("greenguardeast", "Wolf", "Fabric", fabric, false);
        Core.HuntMonster("greenguardeast", "Toxic Treeant", "Board", board, false);
        Core.HuntMonster("greenguardeast", "Gurushroom", "Varnish", varnish, false);
        Core.HuntMonster("greenguardwest", "Black Knight", "Nail", nail, false);
        Core.HuntMonster("greenguardwest", "Slime", "Paint", paint, false);
    }
}