//cs_include Scripts/CoreBots.cs

using System;
using RBot;
using System.Collections.Generic;

public class SagaTemplate
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Core.AcceptandCompleteTries = 5;
        CompleteSaga();

        Core.SetOptions(false);
    }
    public void CompleteSaga()
    {
        if (Core.QuestProgression(1))
            return;
         
        // Core.KillQuest(QuestID: 1, MapName: "mapname", MonsterName: "MonsterName");
        // Core.KillQuest(QuestID: 1, MapName: "mapname", MonsterNames: new[] { "Monstername", "Monstername" });
        // Core.MapItemQuest(QuestID: 1, MapName: "mapname", MapItemID: 1, Amount: 1); 
        // Core.ChainQuest(QuestID: 1);
    }
}