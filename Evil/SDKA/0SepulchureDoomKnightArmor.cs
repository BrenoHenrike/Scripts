//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
using RBot;

public class SepulchureDoomKnightArmor
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreSDKA SDKA = new CoreSDKA();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        SDKA.DoAll();

        Core.SetOptions(false);
    }
}
