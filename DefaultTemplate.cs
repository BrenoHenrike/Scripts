//cs_include Scripts/CoreBots.cs  //LEAVE THIS ALONE
//cs_include Scripts/CoreFarms.cs //LEAVE THIS ALONE

//cs_include Scripts/CoreFile(Or folder)/Filename.cs

using RBot; //LEAVE THIS ALONE

public class Example //you can rename this anything you want it will be the "Class" you refference elsewhere
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();
    //public Classsname Field = new Classname();
    
    //"Classsname" Is the Class name from "Included script you entered above"
    //"Field" is what you are going to use below
    //"New = Classname();" is .. idk its just needed ._. it should be the same as the classname but with " (); " at the end

    public void ScriptMain(ScriptInterface bot) //DO NOT RENAME THIS
    {
        Core.SetOptions(); //LEAVE THIS ALONE

        Core.AddDrop("ExampleItem1", "ExampleItem2", "ExampleItem3", "ExampleItem4"); // insert drops you want to be picked up inside the "" 's,  "" 's MUST be seperated by a comma

        //some code goes here probably

        Core.SetOptions(false); //LEAVE THIS ALONE
    }
}