//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
using RBot;

public class Greed
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public Core7DD DD = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DD.Greed();

        Core.SetOptions(false);
    }
}