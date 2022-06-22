//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using RBot;

public class HireNulgathLarvae
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();
    public CoreFarms Farm = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        getthething();

        Core.SetOptions(false);
    }

    public void getthething()
    {
        if (Core.CheckInventory("Nulgath Larvae"))
            return;

        Nation.HireNulgathLarvae();
    }
}
