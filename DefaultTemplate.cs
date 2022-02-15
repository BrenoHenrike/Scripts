//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class Example
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        YourFunctionName();

        Core.SetOptions(false);
    }

    public void YourFunctionName()
    {
        //if (something)
        //    return;

        Core.AddDrop("ExampleItem1", "ExampleItem2", "ExampleItem3", "ExampleItem4");

        //Your code here
    }
}