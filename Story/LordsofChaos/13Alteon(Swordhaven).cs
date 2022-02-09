//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Story/Core13LoC.cs
using RBot;

public class SagaSwordhaven
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public Core13LoC LOC => new Core13LoC();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Core.AcceptandCompleteTries = 5;

        LOC.Alteon();

        Core.SetOptions(false);
    }
}
