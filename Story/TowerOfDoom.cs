/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class TowerOfDoom
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        TowerProgress();

        Core.SetOptions(false);
    }

    public void TowerProgress(int Floor = 10)
    {
        if (!Bot.Quests.Tree.Any(x => x.ID == 3474 + Floor))
            Bot.Quests.Load(3475, 3476, 3477, 3478, 3479, 3480, 3481, 3482, 3483, 3484);
        if (Core.isCompletedBefore(3474 + Floor))
            return;

        if (Floor != 1 && !Core.isCompletedBefore(3474 + (Floor - 1)))
            TowerProgress(Floor - 1);

        Bot.Quests.UpdateQuest(3474 + Floor);
        Core.Logger($"Tower of Doom {Floor}");

        Core.EquipClass(ClassType.Solo);
        Core.EnsureAccept(3474 + Floor);
        Core.KillMonster(Floor == 1 ? "towerofdoom" : "towerofdoom" + Floor.ToString(), "r10", "Left", "*", publicRoom: true);
        Core.EnsureComplete(3474 + Floor);
    }
}
