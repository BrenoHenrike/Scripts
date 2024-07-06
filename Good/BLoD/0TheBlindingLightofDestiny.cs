/*
name: Blinding Light of Destiny (Full)
description: This bot will do the entire farm for the Blinding Light of Destiny *Note*: it uses dailies!
tags: BLOD, blinding, light, destiny, undead, 75, damage, good, soul searching, minecrafting, metals, spirit orb, aura, loyal, blinding, bright, brilliant, weapon kit, finding fragments, bone some dust, essential essences, light merge, blinding light fragments
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/BattleUnder.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class TheBlindingLightofDestiny
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreBLOD BLOD = new();

    public string OptionsStorage = "BLOD_options";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<BLODMethod>("BLODMethod", "Method", "Which method do you want the bot to farm BLOD?" +
            "\nFewest Dailies:\tBlinding Blade of Destiny" +
            "\nOptimized:\tBlinding Daggers & Mace of Destiny" +
            "\nFewest Hours:\tBlinding Mace, Bow & Blade of Destiny", BLODMethod.Optimized),
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        BLOD.BlindingLightOfDestiny(Bot.Config!.Get<BLODMethod>("BLODMethod"));

        Core.SetOptions(false);
    }
}
