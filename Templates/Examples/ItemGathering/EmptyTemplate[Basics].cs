/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class DefaultTemplate
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    private CoreAdvanced Adv = new();
    private CoreFarms Farm = new();
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Example();

        Core.SetOptions(false);
    }

    public void Example()
    {
        if (Core.CheckInventory("item", 1))
            return;


        Core.RegisterQuests(000);
        while (!Bot.ShouldExit && Core.CheckInventory("item", 1))
        {
            Core.HuntMonster("map", "mob", "item", 1, isTemp: false, log: false);
            Core.KillMonster("map", "cell", "pad", "mob", "item", 1, isTemp: false, log: false);
        }
        Core.CancelRegisteredQuests();
    }
}
