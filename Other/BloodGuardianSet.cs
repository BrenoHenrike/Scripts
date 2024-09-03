/*
name: Blood Guardian Set
description: does the 'Blackened Incense' Quest for the blood guardian set.
tags: blood guardian set, blackened incense, set
*/
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Story/BloodMoon.cs
using Skua.Core.Interfaces;

public class BloodGuardianSet
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private BloodMoon BloodMoon = new();


    string[] Set =
    {
        "Ceremonial Assistant Pet",
        "Blood Guardian Armor",
        "Blood Guardian Shag",
        "Blood Guardian's Sword"
    };
    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Example();
        Core.SetOptions(false);
    }

    public void Example()
    {
        if (Core.CheckInventory(Set))
            return;

        BloodMoon.BloodMoonSaga();

        //This is required... or so we think.
        Core.KillMonster("bloodwarvamp", "r2", "Right", "*", "Mega Lycan Medal", 3);
        Core.ChainComplete(6068);
        Core.KillMonster("bloodwarvamp", "r2", "Right", "*", "Lycan Medal", 5);
        Core.ChainComplete(6069);

        Core.AddDrop(Set);
        Core.EquipClass(ClassType.Solo);
        Core.RegisterQuests(6072);
        foreach (string item in Set)
        {
            Core.FarmingLogger(item, 1);
            while (!Bot.ShouldExit && !Core.CheckInventory(item))
                Core.HuntMonster("bloodwarvamp", "Lunar Blazebinder", "Blackened Incense", 5);
            Bot.Wait.ForPickup(item);
        }
        Core.CancelRegisteredQuests();
        Core.ToBank(Set);
    }
}
