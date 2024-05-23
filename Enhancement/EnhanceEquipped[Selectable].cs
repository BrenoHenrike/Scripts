/*
name: Enhance Equipped [Selectable]
description: This script will enhance equipped gear with selected enhancements.
tags: enhance, enh, equipped, equip, gear, selectable, select
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class EnhanceEquippedSelect
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public string OptionsStorage = "EnhanceEquipSelect";
    public List<IOption> Options = new()
    {
        new Option<EnhancementType>("type", "Enhancement Type", "The type of enhancement to use", EnhancementType.Lucky),
        new Option<CapeSpecial>("cSpecial", "Cape Special", "The special enhancement to use on the cape", CapeSpecial.None),
        new Option<HelmSpecial>("hSpecial", "Helm Special", "The special enhancement to use on the helm", HelmSpecial.None),
        new Option<WeaponSpecial>("wSpecial", "Weapon Special", "The special enhancement to use on the weapon", WeaponSpecial.None)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions(disableClassSwap: true);

        DoEnhanceEquippedSelect();

        Core.SetOptions(false);
    }

    public void DoEnhanceEquippedSelect()
    {
        if (Core.CBOBool("DisableAutoEnhance", out bool _disableAutoEnhance) && _disableAutoEnhance)
            Core.Logger("This bot requires you to have Auto-Enhance enabled, please enable it in Options > CoreBots", messageBox: true, stopBot: true);

        EnhancementType type = Bot.Config?.Get<EnhancementType>("type") ?? default;
        CapeSpecial cSpecial = Bot.Config?.Get<CapeSpecial>("cSpecial") ?? default;
        HelmSpecial hSpecial = Bot.Config?.Get<HelmSpecial>("hSpecial") ?? default;
        WeaponSpecial wSpecial = Bot.Config?.Get<WeaponSpecial>("wSpecial") ?? default;

        Adv.EnhanceEquipped(type, cSpecial, hSpecial, wSpecial);
    }
}
