//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class EnchantedRodofGreasedLightning
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Core.HuntMonster("crashruins", "CluckMoo Idol", "Enchanted Rod of Greased Lightning", isTemp: false);

        Core.SetOptions(false);
    }
}
