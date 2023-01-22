/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class WaterWarMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetItems();

        Core.SetOptions(false);
    }

    public void GetItems(int Quant = 600)
    {
        //Needed AddDrop
        Core.AddDrop("Water Drop", "Solar Badge");

        Core.EquipClass(ClassType.Farm);

        if (!Bot.Quests.IsUnlocked(6816))
            Story.KillQuest(6814, "WaterWar", "Solar Elemental");

        //Water Drop - Sploosh Some Solars! / Sploosh Some Bigger Solars!
        while (!Bot.ShouldExit && !Core.CheckInventory("Water Drop", Quant))
        {
            Core.EnsureAccept(6814);
            Core.EnsureAccept(6816);

            Core.HuntMonster("WaterWar", "Solar Elemental", "Solar Sploosh", 30);
            Core.HuntMonster("WaterWar", "Solar Elemental", "Mega Solar Sploosh", 18);

            while (!Bot.ShouldExit && Core.CheckInventory("Solar Sploosh", 5))
                Core.EnsureComplete(6814);

            while (!Bot.ShouldExit && Core.CheckInventory("Mega Solar Sploosh", 3))
                Core.EnsureComplete(6816);
        }

        //Solar Badge - Killing Easier Creatures in Map (Aloe)
        Core.HuntMonster("WaterWar", "Aloe", "Solar Badge", Quant, false);
    }
}
