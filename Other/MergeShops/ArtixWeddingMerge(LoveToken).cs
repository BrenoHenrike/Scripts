//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class ArtixWeddinMerge
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
        Core.HuntMonster("grimskullannex", "Grim Mage", "Love Token", 1000, false);
    }
}