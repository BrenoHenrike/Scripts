//cs_include Scripts/CoreBots.cs
using RBot;

public class SagaTemplate
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.AcceptandCompleteTries = 5;
        Core.SetOptions();

        SagaName();

        Core.SetOptions(false);
    }

    public void SagaName()
    {
        if (Core.QuestProgression(000))
            return;

        Core.KillQuest(QuestID: 000, MapName: "mapname", MonsterName: "MonsterName");
        Core.KillQuest(QuestID: 000, MapName: "mapname", MonsterNames: new[] { "Monstername", "Monstername" });
        Core.MapItemQuest(QuestID: 000, MapName: "mapname", MapItemID: 1, Amount: 1);
        Core.ChainQuest(QuestID: 000);
    }
}