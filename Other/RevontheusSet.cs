/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs

using Skua.Core.Interfaces;
using Skua.Core.Options;

public class RevontheusSet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public bool DontPreconfigure = true;
    public string OptionsStorage = "RevontheusSet";
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("Equip", "Equip the revontheus set?", "", false),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetSet();

        Core.SetOptions(false);
    }

    public void GetSet()
    {
        string[] Set = {
            "Revontheus Visage",
            "Wings of Revontheus",
            "Revontheus"
        };

        if (Core.CheckInventory(Set))
        {
            Core.Logger("You already have the set");
            return;
        }

        Core.AddDrop(Set);
        Core.EquipClass(ClassType.Solo);

        while (!Bot.ShouldExit && !Core.CheckInventory(Set))
            Core.KillMonster("underworld", "r10", "left", "Undead Legend", log: false);

        if (Bot.Config.Get<bool>("Equip"))
            Core.Equip(Set);

    }
}
