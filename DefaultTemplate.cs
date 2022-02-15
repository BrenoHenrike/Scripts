//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs

//cs_include Scripts/CoreFile(Or folder)/Filename.cs

using RBot; //LEAVE THIS ALONE

public class Example 
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions(); //LEAVE THIS ALONE

        Core.AddDrop("ExampleItem1", "ExampleItem2", "ExampleItem3", "ExampleItem4");

        //some code goes here probably

        Core.SetOptions(false); //LEAVE THIS ALONE
    }
}