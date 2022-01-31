//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;
public class KisstheVoid
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNulgath Nulgath = new CoreNulgath();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Core.AddDrop("Blood Gem of the Archfiend");
        // Uncomment to pick up the Betrayal Blades
        //Core.AddDrop(Nulgath.betrayalBlades);

        Nulgath.KisstheVoid();

        Core.SetOptions(false);
    }
}