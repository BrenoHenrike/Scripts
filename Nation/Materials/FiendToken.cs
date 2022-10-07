//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/HanzoOrbQuest.cs
using Skua.Core.Interfaces;

public class FiendToken
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();
    public HanzoOrbQuest HanzoOrbQuest = new();
    public VoidKnightSword VoidKnightSword = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Nation.FarmFiendToken();
        HanzoOrbQuest.HanzoOrb("FiendToken, 30");

        Core.SetOptions(false);
    }
}