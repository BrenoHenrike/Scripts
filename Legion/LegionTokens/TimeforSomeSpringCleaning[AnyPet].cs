/*
name: TimeforSomeSpringCleaning[AnyPet]
description: Uses the appropriate pet to farm Legion Tokens
tags: legion, legion token
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
public class TimeforSomeSpringCleaning_AnyPet_
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreLegion Legion = new();
    public CoreAdvanced Adv = new();


    public string OptionsStorage = "FarmerJoePet";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<bool>("DoClearaPath", "Do the \"Clear a Path\" Quest", "toggles doing the \"Clear a Path\" Quest. during the LT farm", false),
        new Option<bool>("Enable Logger?", "Toggle Farm Logging", "toggles the \"Farming item (x/x)\"", false),
        CoreBots.Instance.SkipOptions,
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DOthething();

        Core.SetOptions(false);
    }

    public void DOthething()
    {
        Legion.LTShogunParagon(DoClearaPath: Bot.Config!.Get<bool>("DoClearaPath"), Logger: Bot.Config!.Get<bool>("Enable Logger?"));
    }

}
