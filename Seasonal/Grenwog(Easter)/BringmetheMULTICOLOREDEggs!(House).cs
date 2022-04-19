//cs_include Scripts/CoreBots.cs
using RBot;

public class EasterEggHouse
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetMulticoloredEggs();

        Core.SetOptions(false);
    }

    public void GetMulticoloredEggs()
    {
        if (Bot.Inventory.ContainsHouseItem("Easter Egg House"))
        {
            Core.Logger("You already own this House, Stopping Bot.");
            return;
        }

        Core.AddDrop("Multicolored Grenwog Egg");

        Core.EnsureAccept(5788);
        Core.HuntMonster("DarkoviaGrave", "Rainbow Chick", "Rainbow Chick");
        Core.GetMapItem(5226, 20, "DarkoviaGrave");
        Core.EnsureCompleteChoose(5788);
    }
}



