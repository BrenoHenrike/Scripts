//cs_include Scripts/CoreBots.cs
using RBot;

public class ZorbasPalaceMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Woopee();

        Core.SetOptions(false);
    }
    public void Woopee(int quant = 300)
    {
        if (Core.CheckInventory("Woopee", quant))
            return;

        Core.EquipClass(ClassType.Solo);
        Core.KillMonster("zorbaspalace", "r6", "Bottom", "Lem-or", "Furry Egg", 12, false);
        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("zorbaspalace", "r4", "Bottom", "*", "Woopee", quant, false);
    }
}
