//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class ScriptTemplate
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        YourFunctionName();

        Core.SetOptions(false);
    }

    public void YourFunctionName()
    {
        //if (something)
        //   return;

        Core.AddDrop("ExampleItem1", "ExampleItem2", "ExampleItem3", "ExampleItem4");

        //Your code here
    }
}
