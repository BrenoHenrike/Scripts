//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class DefaultTemplate
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
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
