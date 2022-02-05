//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;
public class SwagTokenA
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Farm.SwagTokenA();

        Core.SetOptions(false);
    }
}