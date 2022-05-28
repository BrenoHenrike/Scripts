//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class RandomWepofNul
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNulgath Nulgath = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetWep();

        Core.SetOptions(false);
    }

    public void GetWep()
    {
        if (Core.CheckInventory("Random Weapon of Nulgath"))
            return;

        Nulgath.Supplies("Random Weapon of Nulgath");
    }
}
