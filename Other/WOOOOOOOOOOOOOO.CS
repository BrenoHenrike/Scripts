/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class YeeaaahhBabbyyThaaatsWhatImTalkingAbout
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public string OptionsStorage = "WOOO";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("AConly", "AC Only", "True = Only get AC items.\nFalse = Get both AC and non-AC items.", true),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        WOOOOOOOOOOOOOOO();

        Core.SetOptions(false);
    }

    public void WOOOOOOOOOOOOOOO()
    {
        string[] WOOOOODropsAC =
        {
            "Cr1tikal Hair",
            "Cr1tikal Hair + Beard",
            "Cr1tikal Hat",
            "Cr1tikal Hat + Beard",
            "Cr1tikal Hero",
            "Cr1tikal Meme Poster",
            "2D Moistverse House",
            "GIANT Mushroom Guard (R)",
            "GIANT Mushroom Guard (L)",
            "Major Mushroom Guard (L)",
            "Major Mushroom Guard (R)",
            "Treasure Chest"
        };
        string[] WOOOOODropsNonAC =
        {
            "YugiWOAH Card",
            "ARTX 3090 Controller",
            "ARTX Dome",
            "ARTX Shelf",
            "ARTX Twilly Pet",
            "CCCG Set",
            "Major Mushroom Pet",
            "Moist Water Bottle",
        };

        string[] FocussedArray = Bot.Config.Get<bool>("AConly") ? WOOOOODropsAC : WOOOOODropsAC.Concat(WOOOOODropsNonAC).ToArray();

        Core.RegisterQuests(8497);
        while (!Bot.ShouldExit && !Core.CheckInventory(FocussedArray))
        {
            Core.HuntMonster("Mverse", "Major Mushroom", "Dark Caster Card", 1, isTemp: true);
            Core.HuntMonster("Mverse", "Major Mushroom", "Green-Eyes Red Dragon Card", 1, isTemp: true);
            Core.HuntMonster("Mverse", "Major Mushroom", "Mirror Realm's Force Card", 1, isTemp: true);
            Core.HuntMonster("Mverse", "Major Mushroom", "Drakath The Outlawed One Card", 1, isTemp: true);
        }
        Core.CancelRegisteredQuests();
    }
}
