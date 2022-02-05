//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class BladeOfAwe
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetBoA();

        Core.SetOptions(false);
    }

    public void GetBoA()
    {
        if (Core.CheckInventory("Blade of Awe"))
            return;
        else Core.BuyItem("museum", 631, "Blade of Awe");
        if (Core.CheckInventory("Blade of Awe"))
            return;
            
        Farm.BladeofAweREP(6, true);
    }
}