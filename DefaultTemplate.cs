//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class DefaultTemplate
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Example();

        Core.SetOptions(false);
    }
    public void Example()
    {
        if (Core.CheckInventory("item"))
            return;

        //INSERT CODE HERE      


    }
}
