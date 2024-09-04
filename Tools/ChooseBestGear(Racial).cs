/*
name: Choose Best Gear (Racial)
description: Select a boost type you want to optimize for, and the bot will find and equip the best combination of gear for that boost.
tags: best, gear, boost, damage, dmg, dps, race, racial, racist, chaos, dragonkin, drakath, elemental, human, orc, undead
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Options;
using Skua.Core.Interfaces;

public class ChooseRacialBestGear
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "ChooseBestRacialGear";
    public List<IOption> Options = new()
    {
        new Option<RacialGearBoost>("RacialGearBoost", "Racial Gear Boost", "Choose From dropmenu list what Racial Damage Boost you want to equip ", RacialGearBoost.None),
        new Option<bool>("EnhanceEquipment", "Enhance Equipment", "Specifiy if your racial equipment to be enhanced or not", true),
        CoreBots.Instance.SkipOptions,
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        ChooseItem(Bot.Config!.Get<RacialGearBoost>("RacialGearBoost"), Bot.Config!.Get<bool>("EnhanceEquipment"));

        Core.SetOptions(false);
    }

    public void ChooseItem(RacialGearBoost gearBoost = RacialGearBoost.None, bool EnhanceEquipment = true)
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
