//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class AprilFools2019WarMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetItems();

        Core.SetOptions(false);
    }

    public void GetItems()
    {
        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("pal9001", "Baby Sharkcaster", "Punadin Badge", 600, false);
    }
}