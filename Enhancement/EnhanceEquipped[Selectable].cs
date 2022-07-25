//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;
using RBot.Options;

public class EnhanceEquippedSelect
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();

    public string OptionsStorage = "EnhanceEquipSelect";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<EnhancementType>("type", "Enhancement Type", "The type of enhancement to use", EnhancementType.Lucky),
        new Option<WeaponSpecial>("special", "Weapon Special", "The special enhancement to use on weapon", WeaponSpecial.None),
    };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions(disableClassSwap: true);

        Adv.EnhanceEquipped(Bot.Config.Get<EnhancementType>("type"), Bot.Config.Get<WeaponSpecial>("special"));

        Core.SetOptions(false);
    }
}
