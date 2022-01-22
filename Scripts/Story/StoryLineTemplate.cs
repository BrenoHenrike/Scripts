//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs


using RBot;

public class NextStoryLine
{
    public Scriperface Bot => Scriperface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();
    public CoreNulgath Nulgath = new CoreNulgath();
    public[] InsertStringNameHere ={ "Insertdropshere", "Insertdropshere", "Insertdropshere" };


    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        DoAll();

        Core.SetOptions(false);
    }
    //Function storage;
    //Core.KillQuest(QuestID: , MapName: , MonsterName: , GetReward: true, Reward: "All", hasFollowup: true, FollowupIDOverwrite: 0);
    //Core.MapItemQuest(QuestID: , MapName: , MapItemID: , Amount: , GetReward: true, Reward: "All", hasFollowup: true, FollowupIDOverwrite: 0);

    
    //last killquest/mapitemquest should end as such; 
    //Core.KillQuest(QuestID: , MapName: , MonsterName: , GetReward: true, Reward: "All", hasFollowup: false);
    //Core.MapItemQuest(QuestID: , MapName: , MapItemID: , Amount: , GetReward: true, Reward: "All", hasFollowup: false);
    //Core.FactionNameRep();
    //Farm.Experience(InsertLevelHere);
    //Nulgath.InsertFunctFromCoreNulgathHere();

    //Notes;
    //Amount, GetReward, Reward, hasfollowup, and FollowupIDOverwrite, can all be taken out if the value isnt changed.
    //"FollowupIDOverwrite", is for if the next quest in the storyline, isnt the direct +1 of the current Ex: QuestID: 1, FollowupIDOverwrite: 3 (would go dirrectly to quest 3)

    public void DoAll()
    {

    }

    public void StoryLine1()
    {
        if (Bot.Quests.IsUnlocked(FirstQuestIDFromSToryLine1))
        return;
        
    }
    
    public void StoryLine2()
    {
        if (Bot.Quests.IsUnlocked(FirstQuestIDFromSToryLine2))
        return;
    }
    
    public void StoryLine3()
    {
        
    }
    
}