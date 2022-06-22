//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using RBot;

public class SwindlesReturnPolicy
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();


    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Nation.SwindleReturn();

        Core.SetOptions(false);
    }
}