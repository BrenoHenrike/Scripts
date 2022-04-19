//cs_include Scripts/CoreBots.cs
using RBot;

public class TheEggcubator
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetPINKEggs();

        Core.SetOptions(false);
    }

    public void GetPINKEggs()
    {
        if (Core.CheckInventory("The Eggcubator"))
        {
            Core.Logger("You already own this item, Stopping Bot.");
            return;
        }

        Core.EnsureAccept(5784);
        Core.HuntMonster("Mythsong", "Pink Chick", "Pink Chick");
        Core.GetMapItem(5222, 2, "Mythsong");
        Core.EnsureCompleteChoose(5784);
    }
}



