//cs_include Scripts/CoreBots.cs

using Rbot;

public class SagaTemplate
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Core.AcceptandCompleteTries = 5;
        StoryLine();

        Core.SetOptions(false);
    }
    public void StoryLine()
    {
        if (Core.QuestProgression(QuestID))
            return;
         
        Core.KillQuest(QuestID: , MapName: "", MonsterName: "");
        Core.KillQuest(QuestID: , MapName: "", MonsterNames: new[] { "Monstername", "Monstername" });
        Core.MapItemQuest(QuestID: , MapName: "", MapItemID: , Amount: ); 
        Core.ChainQuest(QuestID: );
    }
}