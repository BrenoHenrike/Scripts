//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
public class KisstheVoid
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        // Uncomment to pick up the Betrayal Blades
        //Core.AddDrop(Nation.betrayalBlades);

        Nation.KisstheVoid();

        Core.SetOptions(false);
    }
}