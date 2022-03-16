//cs_include Scripts/CoreBots.cs
using RBot;

public class AprilFools2019WarMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetItems();

        Core.SetOptions(false);
    }

    public void GetItems()
    {
        //Needed AddDrop
        Core.AddDrop("Punadin Badge");

        Core.EquipClass(ClassType.Farm);

        Core.HuntMonster("pal9001", "Baby Sharkcaster|Doge The Bounty Hunter|Nekomancer", "Punadin Badge", 600, false);
    }
}