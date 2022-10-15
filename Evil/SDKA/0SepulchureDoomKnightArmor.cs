//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class SepulchureDoomKnightArmor
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSDKA SDKA = new();
    public static CoreSDKA sSDKA = new();

    public string OptionsStorage = sSDKA.OptionsStorage;
    public bool DontPreconfigure = true;
    public List<IOption> Options = sSDKA.Options;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SDKA.DoAll();

        Core.SetOptions(false);
    }
}
