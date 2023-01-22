/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Farm/BuyScrolls.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs
//cs_include Scripts/Other/Classes/ArchMage/CoreArchMage.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class BookOfAether
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArchMage AM = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "ArchMage";
    public List<IOption> Options = new()
    {
        new Option<bool>("cosmetics", "Get Cosmetics", "Gets the cosmetic rewards (redoes quests if you don't have them, disable to just get ArchMage and the weapon) [On by default]", true),
        CoreBots.Instance.SkipOptions,
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        AM.BookOfAether();

        Core.SetOptions(false);
    }
}
