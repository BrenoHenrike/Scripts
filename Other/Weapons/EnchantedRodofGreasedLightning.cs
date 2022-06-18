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

        Wep();

        Core.SetOptions(false);
    }

    public void Wep()
    {
        if (Core.CheckInventory("Enchanted Rod of Greased Lightning"))
            return;

        Core.HuntMonster("crashruins", "CluckMoo Idol", "Enchanted Rod of Greased Lightning");
    }
}
