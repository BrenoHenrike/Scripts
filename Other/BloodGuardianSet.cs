/*
name: Blood Guardian Set
description: does the 'Blackened Incense' Quest for the blood guardian set.
tags: blood guardian set, Blackened Incense, set
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
