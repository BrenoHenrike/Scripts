//cs_include Scripts/CoreBots.cs
using RBot;

public class SteampunkEggsplorer
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetPurpleEggs();

        Core.SetOptions(false);
    }

    public void GetPurpleEggs()
    {
        if (Core.CheckInventory("Steampunk Eggsplorer"))
        {
            Core.Logger("You already own this item, Stopping Bot.");
            return;
        }

        Core.AddDrop("Purple Grenwog Egg");

        Core.EnsureAccept(5787);
        Core.HuntMonster("WaterStorm", "Purple Chick", "Purple Chick");
        Core.GetMapItem(5225, 10, "WaterStorm");
        Core.EnsureCompleteChoose(5787);
    }
}



