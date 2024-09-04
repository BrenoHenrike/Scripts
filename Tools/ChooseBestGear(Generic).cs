/*
name: Choose Best Gear
description: Select a boost type you want to optimize for, and the bot will find and equip the best combination of gear for that boost.
tags: best, gear, boost, damage, dmg, dps, exp, gold, reputation, class points
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Options;
using Skua.Core.Interfaces;

public class ChooseGenericBestGear
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "ChooseBestGenericGear";
    public List<IOption> Options = new()
    {
        new Option<GenericGearBoost>("GenericGearBoost", "Generic Gear Boost", "Choose From dropmenu list what Generic Gear Boost you want to equip ", GenericGearBoost.dmgAll),
        new Option<bool>("EnhanceEquipment", "Enhance Equipment", "Specifiy if your generic equipment to be enhanced or not", true),
        CoreBots.Instance.SkipOptions,
    };


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        ChooseItem(Bot.Config!.Get<GenericGearBoost>("GenericGearBoost"), Bot.Config!.Get<bool>("EnhanceEquipment"));

        Core.SetOptions(false);
    }

    public void ChooseItem(GenericGearBoost gearBoost = GenericGearBoost.dmgAll, bool EnhanceEquipment = true)
    {
        if (Core.CBOBool("DisableBestGear", out bool _DisableBestGear) && _DisableBestGear)
            Core.Logger("This bot requires you to have Best Gear enabled, please enable it in Options > CoreBots", messageBox: true, stopBot: true);
        if (EnhanceEquipment && Core.CBOBool("DisableAutoEnhance", out bool _disableAutoEnhance) && _disableAutoEnhance)
            Core.Logger("This bot requires you to have Auto-Enhance enabled, please enable it in Options > CoreBots", messageBox: true, stopBot: true);

        //Adv.BestGear(gearBoost);
        if (EnhanceEquipment && Bot.Player.CurrentClass != null)
            Adv.SmartEnhance(Bot.Player.CurrentClass.Name);
    }
}
