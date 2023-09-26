/*
name: Leveling
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class Leveling
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public List<IOption> Options = new List<IOption>()
    {
        new Option<string>("level", "Level", "Up to which level would you like to farm?"),
    };

    private int MinLevel = 2;
    private int MaxLevel = 100;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoLeveling();

        Core.SetOptions(false);
    }

    public void DoLeveling()
    {
        //Adv.BestGear(GenericGearBoost.exp);

        string level = Bot.Config!.Get<string>("level");

        if (Int32.TryParse(level.Trim(), out int levelInt)) {
            if (Bot.Player.Level >= levelInt) {
                Core.Logger("Level inputted is greater than or equal to current level. Please input a valid level.");
                return;
            }

            if (levelInt < MinLevel || levelInt > MaxLevel) {
                Core.Logger($"Level inputted is invalid. Valid values: {MinLevel} - {MaxLevel}");
                return;
            }

            Farm.Experience(levelInt);
        } else {
             Core.Logger($"Invalid level input {level}");
        }
    }
}
