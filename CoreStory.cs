using RBot;
using RBot.Items;
using RBot.Quests;

public class CoreStory
{
    // [Can Change]
    // True = Bot only does its smart checks on quests with Once: True 
    // False = Bot does it's smart checks on all quest
    // Recommended: false
    // Used for testing bots, dont toggle this as a user
    public bool TestBot { get; set; } = false;
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    /// <summary>
    /// Kills a monster for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one.
    /// </summary>
    /// <param name="QuestID">ID of the quest</param>
    /// <param name="MapName">Map where the <paramref name="MonsterName"/> are</param>
    /// <param name="MonsterName">Monster to kill</param>
    /// <param name="GetReward">Whether or not the <paramref name="Reward"/> should be added with AddDrop</param>
    /// <param name="Reward">What item should be added with AddDrop</param>
    /// <param name="AutoCompleteQuest">If the method should turn in the quest for you when the quest can be completed</param>
    public void KillQuest(int QuestID, string MapName, string MonsterName, bool GetReward = true, string Reward = "All", bool AutoCompleteQuest = true)
    {
        Quest QuestData = Core.EnsureLoad(QuestID);
        ItemBase[] Requirements = QuestData.Requirements.ToArray();

        if (QuestProgression(QuestID, GetReward, Reward))
            return;

        Core.SmartKillMonster(QuestID, MapName, MonsterName, 50, Requirements[0].Coins);
        if (Bot.Quests.CanComplete(QuestID))
        {
            if (AutoCompleteQuest)
                Core.EnsureComplete(QuestID);
            Core.Logger($"Completed \"{QuestData.Name}\" [{QuestID}]");
        }
    }

    /// <summary>
    /// Kills an array of monsters for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one.
    /// </summary>
    /// <param name="QuestID">ID of the quest</param>
    /// <param name="MapName">Map where the <paramref name="MonsterName"/> are</param>
    /// <param name="MonsterName">Monster to kill</param>
    /// <param name="GetReward">Whether or not the <paramref name="Reward"/> should be added with AddDrop</param>
    /// <param name="Reward">What item should be added with AddDrop</param>
    /// <param name="AutoCompleteQuest">If the method should turn in the quest for you when the quest can be completed</param>
    public void KillQuest(int QuestID, string MapName, string[] MonsterNames, bool GetReward = true, string Reward = "All", bool AutoCompleteQuest = true)
    {
        Quest QuestData = Core.EnsureLoad(QuestID);
        ItemBase[] Requirements = QuestData.Requirements.ToArray();

        if (QuestProgression(QuestID, GetReward, Reward))
            return;

        Core.SmartKillMonster(QuestID, MapName, MonsterNames, 50, Requirements[0].Coins);
        if (Bot.Quests.CanComplete(QuestID))
        {
            if (AutoCompleteQuest)
                Core.EnsureComplete(QuestID);
            Core.Logger($"Completed \"{QuestData.Name}\" [{QuestID}]");
        }
    }

    /// <summary>
    /// Gets a MapItem X times for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one.
    /// </summary>
    /// <param name="QuestID">ID of the quest</param>
    /// <param name="MapName">Map where the <paramref name="MonsterName"/> are</param>
    /// <param name="MapItemID">ID of the item</param>
    /// <param name="Amount">The amount of <paramref name="MapItemID"/> it grabs</param>
    /// <param name="GetReward">Whether or not the <paramref name="Reward"/> should be added with AddDrop</param>
    /// <param name="Reward">What item should be added with AddDrop</param>
    /// <param name="AutoCompleteQuest">If the method should turn in the quest for you when the quest can be completed</param>
    public void MapItemQuest(int QuestID, string MapName, int MapItemID, int Amount = 1, bool GetReward = true, string Reward = "All", bool AutoCompleteQuest = true)
    {
        Quest QuestData = Core.EnsureLoad(QuestID);

        if (QuestProgression(QuestID, GetReward, Reward))
            return;

        Core.EnsureAccept(QuestID);
        Core.GetMapItem(MapItemID, Amount, MapName);
        if (Bot.Quests.CanComplete(QuestID))
        {
            if (AutoCompleteQuest)
                Core.EnsureComplete(QuestID);
            Core.Logger($"Completed \"{QuestData.Name}\" [{QuestID}]");
        }
    }

    /// <summary>
    /// Gets a MapItem X times for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one.
    /// </summary>
    /// <param name="QuestID">ID of the quest</param>
    /// <param name="MapName">Map where the <paramref name="MonsterName"/> are</param>
    /// <param name="MapItemIDs">ID of the item</param>
    /// <param name="Amount">The amount of <paramref name="MapItemID"/> it grabs</param>
    /// <param name="GetReward">Whether or not the <paramref name="Reward"/> should be added with AddDrop</param>
    /// <param name="Reward">What item should be added with AddDrop</param>
    /// <param name="AutoCompleteQuest">If the method should turn in the quest for you when the quest can be completed</param>
    public void MapItemQuest(int QuestID, string MapName, int[] MapItemIDs, int Amount = 1, bool GetReward = true, string Reward = "All", bool AutoCompleteQuest = true)
    {
        Quest QuestData = Core.EnsureLoad(QuestID);

        if (QuestProgression(QuestID, GetReward, Reward))
            return;

        Core.EnsureAccept(QuestID);
        foreach (int MapItemID in MapItemIDs)
            Core.GetMapItem(MapItemID, Amount, MapName);
        if (Bot.Quests.CanComplete(QuestID))
        {
            if (AutoCompleteQuest)
                Core.EnsureComplete(QuestID);
            Core.Logger($"Completed \"{QuestData.Name}\" [{QuestID}]");
        }
    }

    /// <summary>
    /// Gets a MapItem X times for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one.
    /// </summary>
    /// <param name="QuestID">ID of the quest</param>
    /// <param name="MapName">Map where the <paramref name="MonsterName"/> are</param>
    /// <param name="ItemName">Name of the item</param>
    /// <param name="Amount">The amount of <paramref name="ItemName"/> to buy</param>
    /// <param name="GetReward">Whether or not the <paramref name="Reward"/> should be added with AddDrop</param>
    /// <param name="Reward">What item should be added with AddDrop</param>
    /// <param name="AutoCompleteQuest">If the method should turn in the quest for you when the quest can be completed</param>
    public void BuyQuest(int QuestID, string MapName, int ShopID, string ItemName, int Amount = 1, bool GetReward = true, string Reward = "All", bool AutoCompleteQuest = true)
    {
        Quest QuestData = Core.EnsureLoad(QuestID);

        if (QuestProgression(QuestID, GetReward, Reward))
            return;

        Core.EnsureAccept(QuestID);
        Core.BuyItem(MapName, ShopID, ItemName, Amount);
        if (Bot.Quests.CanComplete(QuestID))
        {
            if (AutoCompleteQuest)
                Core.EnsureComplete(QuestID);
            Core.Logger($"Completed \"{QuestData.Name}\" [{QuestID}]");
        }
    }

    /// <summary>
    /// Accepts a quest and then turns it in again
    /// </summary>
    /// <param name="QuestID">ID of the quest</param>
    /// <param name="GetReward">Whether or not the <paramref name="Reward"/> should be added with AddDrop</param>
    /// <param name="Reward">What item should be added with AddDrop</param>
    /// <param name="AutoCompleteQuest">If the method should turn in the quest for you when the quest can be completed</param>
    public void ChainQuest(int QuestID, bool GetReward = true, string Reward = "All", bool AutoCompleteQuest = true)
    {
        Quest QuestData = Core.EnsureLoad(QuestID);

        if (QuestProgression(QuestID, GetReward, Reward))
            return;

        if (AutoCompleteQuest)
            Core.ChainComplete(QuestID);
        else
        {
            Core.EnsureAccept(QuestID);
            Bot.Sleep(Core.ActionDelay);
        }
        Core.Logger($"Completed \"{QuestData.Name}\" [{QuestID}]");
    }

    /// <summary>
    /// Skeleton of KillQuest, MapItemQuest, BuyQuest and ChainQuest. Only needs to be used inside a script if there isnt a solid QuestID tied to an event. See Core13LoC for an example
    /// </summary>
    /// <param name="QuestID">ID of the quest</param>
    /// <param name="QuestData">If you saved the QuestData here somewhere, it can be passed on to this function. Only for internal use</param>
    /// <param name="GetReward">Whether or not the <paramref name="Reward"/> should be added with AddDrop</param>
    /// <param name="Reward">What item should be added with AddDrop</param>
    public bool QuestProgression(int QuestID, bool GetReward = true, string Reward = "All")
    {
        if (QuestID != 0 && PreviousQuestID == QuestID)
            return PreviousQuestState;

        PreviousQuestID = QuestID;

        Quest QuestData = Core.EnsureLoad(QuestID);
        ItemBase[] Rewards = QuestData.Rewards.ToArray();

        if (QuestData == null)
        {
            Core.Logger($"Quest [{QuestID}] doesn't exist", messageBox: true, stopBot: true);
            return true;
        }

        if (!Bot.Quests.IsUnlocked(QuestID))
            Core.Logger($"Quest \"{QuestData.Name}\" [{QuestID}] is not unlocked, is your bot setup correctly?", messageBox: true, stopBot: true);

        if (Core.isCompletedBefore(QuestID) && (TestBot ? QuestData.Once : true))
        {
            if (TestBot)
                Core.Logger($"\"{QuestData.Name}\" [{QuestID}] skipped, TestBot with Once = true");
            else Core.Logger($"\"{QuestData.Name}\" [{QuestID}] already completed, skipping it.");
            PreviousQuestState = true;
            return true;
        }

        if (Reward != "All")
        {
            if (Core.CheckInventory(Reward))
            {
                Core.Logger($"You already have {Reward}, skipping quest");
                PreviousQuestState = true;
                return true;
            }
            Core.AddDrop(Reward);
        }
        else
            foreach (ItemBase Item in Rewards)
                Core.AddDrop(Item.Name);

        Core.Logger($"Doing \"{QuestData.Name}\" [{QuestID}]");
        Core.EquipClass(ClassType.Solo);
        PreviousQuestState = false;
        return false;
    }

    /// <summary>
    /// Send a Client-side packet that makes the game think you have completed a questline up to a certain point
    /// </summary>
    /// <param name="Value">Value property of the quest you want it to think you have completed</param>
    /// <param name="Slot">Slot property of the questline you want it to think you have progressed</param>
    public void UpdateQuest(int Value, int Slot)
    {
        if ((Slot < 0 || Bot.CallGameFunction<int>("world.getQuestValue", Slot) >= Value))
            return;

        Bot.SendClientPacket("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"updateQuest\",\"iValue\":" + Value + ",\"iIndex\":" + Slot + "}}}", "json");
    }

    /// <summary>
    /// Send a Client-side packet that makes the game think you have completed a questline up to a certain point
    /// </summary>
    /// <param name="QuestID">Quest ID of the quest you want the game to think you have compelted</param>
    public void UpdateQuest(int QuestID)
    {
        if (Core.isCompletedBefore(QuestID))
            return;

        Quest QuestData = Core.EnsureLoad(QuestID);
        Bot.SendClientPacket("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"updateQuest\",\"iValue\":" + QuestData.Value + ",\"iIndex\":" + QuestData.Slot + "}}}", "json");
    }

    private int PreviousQuestID = 0;
    private bool PreviousQuestState = false;
}