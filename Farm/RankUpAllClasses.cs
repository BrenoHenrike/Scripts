/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models.Items;

public class RankUpAll
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public List<IOption> Options = new()
    {
        new Option<bool>("inclBank", "Include Bank", "If True, will also rank up all unranked classes in the bank", false),
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        RankUpAllClasses(Bot.Config.Get<bool>("inclBank"));

        Core.SetOptions(false);
    }

    public void RankUpAllClasses(bool includeBank = false)
    {
        List<string> SelectedClasses = new();
        List<string> BankClasses = new();

        SelectedClasses.AddRange(Bot.Inventory.Items.Where(c => c.Category == ItemCategory.Class && c.Quantity < 302500).Select(x => x.Name));

        if (includeBank)
        {
            BankClasses = Bot.Bank.Items.Where(c => c.Category == ItemCategory.Class && c.Quantity < 302500).Select(x => x.Name).ToList();
            SelectedClasses.AddRange(BankClasses);
        }

        if (Core.CBOBool("DisableAutoEnhance", out bool _disableAutoEnhance) && _disableAutoEnhance)
            Core.Logger("This bot requiers Smart Enhance to work properly, please modify your CBO settings", messageBox: true, stopBot: true);

        Adv.GearStore();

        foreach (string Class in SelectedClasses)
        {
            if (Core.CheckInventory(Class))
                Adv.rankUpClass(Class, false);

            if (BankClasses.Contains(Class))
                Core.ToBank(Class);
        }

        Adv.GearStore(true);
    }
}
