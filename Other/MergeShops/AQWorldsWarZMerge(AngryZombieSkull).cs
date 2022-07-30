//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class AQWorldsWarZMerge
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
        Core.HuntMonster("doomwar", "Angry Zombie", "Angry Zombie Skull", 500, false);
    }
}