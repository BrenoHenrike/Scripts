/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class SnowballAmberarms
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Snowball();

        Core.SetOptions(false);
    }

    public string[] Loot =
    {
    "TundraWyrm Hunter",
    "WyrmHunter's Cowl",
    "WyrmHunter's Claymore",
    "WyrmHunter Back Claymore"
    };

    public void Snowball()
    {
        if (Core.CheckInventory(Loot) || (!Core.CheckInventory("Snowball Amberarms")))
            return;

        Core.AddDrop(Loot);
        Core.EquipClass(ClassType.Solo);
        Core.RegisterQuests(3508);
        Bot.Quests.UpdateQuest(3484);
        while (!Bot.ShouldExit && (!Core.CheckInventory(Loot)))
        {
            Core.HuntMonster("bosschallenge", "Mutated Void Dragon", "Dread Talon", isTemp: false);
            Core.HuntMonster("towerofdoom6", "Dread Terror", "Dread Tooth", 20, false);
        }
    }
}
