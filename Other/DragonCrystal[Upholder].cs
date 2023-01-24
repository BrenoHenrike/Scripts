/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/DragonRoad[Upholder].cs
using Skua.Core.Interfaces;

public class DragonCristal
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public DragonRoad DragonRoad = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Quantity();

        Core.SetOptions(false);
    }

    public void Quantity(int quant = 5000)
    {
        DragonRoad.StoryLine();

        Core.RegisterQuests(4549);
        while (!Bot.ShouldExit && !Core.CheckInventory("Dragon Crystal", quant))
        {
            //Gather Energy Beans 4549
            Core.EquipClass(ClassType.Farm);
            Core.GetMapItem(3760, 4, "DragonRoad");
            Core.HuntMonster("DragonRoad", "Desert Wolf Bandit", "Energy Bean", 3);
        }
        Core.CancelRegisteredQuests();

    }
}
