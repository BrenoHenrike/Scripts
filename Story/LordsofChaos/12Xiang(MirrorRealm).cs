//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Story/Core13LoC.cs
using RBot;

public class SagaMirrorRealm
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public Core13LoC LOC => new Core13LoC();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.AcceptandCompleteTries = 5;
        Core.SetOptions();

        LOC.Xiang();

        Core.SetOptions(false);
    }
}