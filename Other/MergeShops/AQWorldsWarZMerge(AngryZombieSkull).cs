//cs_include Scripts/CoreBots.cs
using RBot;

public class AQWorldsWarZMerge
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
        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("doomwar", "Angry Zombie", "Angry Zombie Skull", 500, false);
    }
}