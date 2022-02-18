//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class EnhanceEquippedLucky
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Adv.EnhanceEquipped(EnhancementType.Lucky, Adv.CurrentWeaponSpecial());

        Core.SetOptions(false);
    }
}
