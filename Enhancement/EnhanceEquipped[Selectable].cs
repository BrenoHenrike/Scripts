//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class EnhanceEquippedSelect
{
    public IScriptInterface Bot { get; set; }
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();

    public string OptionsStorage = "EnhanceEquipSelect";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<EnhancementType>("type", "Enhancement Type", "The type of enhancement to use", EnhancementType.Lucky),
        new Option<CapeSpecial>("cSpecial", "Cape Special", "The special enhancement to use on the cape", CapeSpecial.None),
        new Option<WeaponSpecial>("wSpecial", "Weapon Special", "The special enhancement to use on the weapon", WeaponSpecial.None)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions(disableClassSwap: true);

        Adv.EnhanceEquipped(Bot.Config.Get<EnhancementType>("type"), Bot.Config.Get<CapeSpecial>("cSpecial"), Bot.Config.Get<WeaponSpecial>("wSpecial"));

        Core.SetOptions(false);
    }
}
