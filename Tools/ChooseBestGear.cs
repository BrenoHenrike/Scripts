/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Options;
using Skua.Core.Interfaces;

public class ChooseBestGear
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "ChooseBestRacialGear";
    public List<IOption> Options = new()
    {

        new Option<GearBoost>("RacialGearBoost", "Racial Gear Boost", "Choose From dropmenu list what Racial Damage Boost you want to equip ", GearBoost.dmgAll),
        new Option<bool>("EnhanceEquipment", "Enhance Equipment", "Specifiy if your your racial equipment to be enhanced or not", true),
        CoreBots.Instance.SkipOptions,
    };


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        ChooseItem(Bot.Config.Get<GearBoost>("RacialGearBoost"), Bot.Config.Get<bool>("EnhanceEquipment"));

        Core.SetOptions(false);
    }

    public void Choose()
    {

    }

    public void ChooseItem(GearBoost gearBoost = GearBoost.dmgAll, bool EnhanceEquipment = true)
    {
        if (Core.CBOBool("DisableBestGear", out bool _DisableBestGear) && _DisableBestGear)
            Core.Logger("This bot requiers you to have Best Gear enabled, please enable it in Options > CoreBots", messageBox: true, stopBot: true);

        if (EnhanceEquipment && Core.CBOBool("DisableAutoEnhance", out bool _disableAutoEnhance) && _disableAutoEnhance)
            Core.Logger("This bot requiers you to have Auto-Enhance enabled, please enable it in Options > CoreBots", messageBox: true, stopBot: true);

        string[] GearBoostItems = Adv.BestGear(Bot.Config.Get<GearBoost>("RacialGearBoost"));
        Core.Equip(GearBoostItems);
        if (EnhanceEquipment)
            Adv.SmartEnhance(Bot.Player.CurrentClass.Name);
    }

}
