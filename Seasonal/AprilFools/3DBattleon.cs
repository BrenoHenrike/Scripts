/*
name: 3D Battleon Storyline
description: This script will complete Cysero's quests in aqw3d.
tags: april fools, seasonal, 3d battleon, aqw3d, cysero, story
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Battleon3D
{
    private IScriptInterface Bot => IScriptInterface.Instance;
private CoreBots Core => CoreBots.Instance;
private CoreStory Story = new();

public void ScriptMain(IScriptInterface Bot)
{
    Core.BankingBlackList.AddRange(new[] { "Fools Token I", "Fools Token II", "Fools Token III",
                                               "Fools Token IV", "Fools Token V", "Fools Token VI",
                                               "Fools Token VII", "Fools Token VIII" });
    Core.SetOptions();

    StoryLine();
    Core.SetOptions(false);
}

public void StoryLine()
{
    if (Core.CheckInventory("Fools Token VIII") || !Core.isSeasonalMapActive("aqw3d"))
        return;

    Story.LegacyQuestManager(QuestLogic, 4943, 4944, 4945, 4946, 4947, 4948, 4949, 4951);

    void QuestLogic()
    {
        switch (Story.LegacyQuestID)
        {
            case 4943: // Scan the Perimeter! 4943
                Core.GetMapItem(4321, 1, "aqw3d");
                Core.GetMapItem(4322, 1, "aqw3d");
                Core.GetMapItem(4323, 1, "aqw3d");
                Core.GetMapItem(4324, 1, "aqw3d");
                break;

            case 4944: // Clackity Clackity 4944
                Core.HuntMonster("aqw3d", "Slime", "Smack All the Monsters", 5);
                Core.HuntMonster("aqw3d", "Clawg", "Especially the Clawgs", 5);
                break;

            case 4945: // Calibrations 4945
                Core.HuntMonster("aqw3d", "3D Flying Eye", "Sp-Eye Sample", 2);
                Core.HuntMonster("aqw3d", "Clawg", "Clawg Sample", 2);
                Core.HuntMonster("aqw3d", "Slime", "Slime Sample", 2);
                Core.HuntMonster("aqw3d", "Frogzard", "Frogzard Sample", 2);
                break;

            case 4946: // Sp-Eye Power 4946
                Core.HuntMonster("aqw3d", "3D Flying Eye", "Sp-Eye Beams", 6);
                break;

            case 4947: // It's All ADDING Up 4947
                Core.HuntMonster("aqw3d", "Horc Warrior", "Armor Plating", 4);
                Core.HuntMonster("aqw3d", "Slime", "Nuts &amp; Bolts", 6);
                break;

            case 4948: // Clawg Claws 4948
                Core.HuntMonster("aqw3d", "Clawg", "Clawg Claws", 8);
                break;

            case 4949: // A Good Case Of Con Rot 4949
                Core.HuntMonster("artistalley", "Con Rot", "Infected With Con Rot", 6);
                break;

            case 4951: // Trolluk! 4951
                Core.HuntMonster("aqw3d", "Trolluk", "ADDING Machine");
                break;
        }
    }
}
}
