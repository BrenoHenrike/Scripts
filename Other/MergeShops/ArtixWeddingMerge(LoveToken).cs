//cs_include Scripts/CoreBots.cs
using RBot;

public class ArtixWeddinMerge
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
        Core.HuntMonster("grimskullannex", "Grim Mage|Grim Soldier|Grim Fighter", "Love Token", 1000, false);
    }
}