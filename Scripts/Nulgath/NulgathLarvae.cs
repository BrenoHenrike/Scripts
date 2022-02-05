//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class NulgathLarvae
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNulgath Nulgath = new CoreNulgath();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Nulgath.NulgathLarvae();

        Core.SetOptions(false);
    }
}