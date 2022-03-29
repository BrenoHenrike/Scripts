//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class EnhanceEquippedHealer
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions(disableClassSwap: true);

        Adv.EnhanceEquipped(EnhancementType.Healer, Adv.CurrentWeaponSpecial());

        Core.SetOptions(false);
    }
}
