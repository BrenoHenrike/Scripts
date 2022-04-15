using RBot;
using RBot.Items;
using RBot.Quests;
using System.Diagnostics;
using System.Windows.Forms;

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
            Bot.Sleep(Core.ActionDelay);
            if (AutoCompleteQuest)
                Core.EnsureComplete(QuestID);
            Bot.Wait.ForQuestComplete(QuestID);
            Core.Logger($"Completed \"{QuestData.Name}\" [{QuestID}]");
            Bot.Sleep(Core.ActionDelay);
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
            Bot.Sleep(Core.ActionDelay);
            if (AutoCompleteQuest)
                Core.EnsureComplete(QuestID);
            Bot.Wait.ForQuestComplete(QuestID);
            Core.Logger($"Completed \"{QuestData.Name}\" [{QuestID}]");
            Bot.Sleep(Core.ActionDelay);
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
            Bot.Sleep(Core.ActionDelay);
            if (AutoCompleteQuest)
                Core.EnsureComplete(QuestID);
            Bot.Wait.ForQuestComplete(QuestID);
            Core.Logger($"Completed \"{QuestData.Name}\" [{QuestID}]");
            Bot.Sleep(Core.ActionDelay);
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
            Bot.Sleep(Core.ActionDelay);
            if (AutoCompleteQuest)
                Core.EnsureComplete(QuestID);
            Bot.Wait.ForQuestComplete(QuestID);
            Core.Logger($"Completed \"{QuestData.Name}\" [{QuestID}]");
            Bot.Sleep(Core.ActionDelay);
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
            Bot.Sleep(Core.ActionDelay);
            if (AutoCompleteQuest)
                Core.EnsureComplete(QuestID);
            Bot.Wait.ForQuestComplete(QuestID);
            Core.Logger($"Completed \"{QuestData.Name}\" [{QuestID}]");
            Bot.Sleep(Core.ActionDelay);
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

        Bot.Sleep(Core.ActionDelay);
        if (AutoCompleteQuest)
            Core.ChainComplete(QuestID);
        else
        {
            Core.EnsureAccept(QuestID);
        }
        Bot.Wait.ForQuestComplete(QuestID);
        Core.Logger($"Completed \"{QuestData.Name}\" [{QuestID}]");
        Bot.Sleep(Core.ActionDelay);
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

        if (!CBO_Checked)
        {
            if (Core.CBO_Active)
                TestBot = Core.CBOBool("BCO_Story_TestBot");
            CBO_Checked = true;
        }

        PreviousQuestID = QuestID;

        Quest QuestData = Core.EnsureLoad(QuestID);
        ItemBase[] Rewards = QuestData.Rewards.ToArray();

        if (QuestData == null)
        {
            Core.Logger($"Quest [{QuestID}] doesn't exist", messageBox: true, stopBot: true);
            return true;
        }

        if (!Bot.Quests.IsUnlocked(QuestID))
        {
            Core.Logger($"Quest \"{QuestData.Name}\" [{QuestID}] is not unlocked, please fill in the RBot Scripts Form to report this. Do you wish to be brought to the form?");
            DialogResult response = MessageBox.Show($"Quest \"{QuestData.Name}\" [{QuestID}] is not unlocked, please fill in the RBot Scripts Form to report this.\nDo you wish to be brought to the form?", "Quest not unlocked", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (response == DialogResult.Yes)
                Process.Start("explorer", "https://forms.gle/sbp57LBQP5WvCH2B9");
            Core.StopBot();
        }

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
    private bool CBO_Checked = false;

    public void PreLoad()
    {
        if (PreLoaded)
            return;

        List<int> QuestIDs = new();
        List<string> SelectedLines = new();
        List<string> CSIncFiles = new();

        List<string> CSFile = File.ReadAllLines(ScriptManager.LoadedScript).ToList();
        string[] SearchParam = {
            "Story.KillQuest",
            "Story.MapItemQuest",
            "Story.BuyQuest",
            "Story.ChainQuest",
            "Story.QuestProgression",
            "Core.EnsureAccept",
            "Core.EnsureComplete",
            "Core.EnsureCompleteChoose",
            "Core.ChainComplete"
        };

        List<string> CSIncludes = CSFile.Where(x => x.Contains("//cs_include ") && (x.Contains("Core13LoC") || !x.Contains("Core"))).ToList();

        foreach (string Include in CSIncludes)
            CSIncFiles.AddRange(File.ReadAllLines(Include.Replace("//cs_include ", "")));

        SelectedLines.AddRange(CSFile.Where(x => SearchParam.Any(y => x.Contains(y))).ToList());
        SelectedLines.AddRange(CSIncFiles.Where(x => SearchParam.Any(y => x.Contains(y))).ToList());

        Core.Logger($"Scanning {CSIncludes.Count + 1} Files ({SelectedLines.Count} Lines)");

        List<Quest> QuestTree = Bot.Quests.QuestTree;
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        int t = 0;

        foreach (string Line in SelectedLines)
        {
            if (!Line.Any(char.IsDigit))
                continue;

            string EdittedLine = Line
                                    .Replace(" ", "")
                                    .Replace("!", "")
                                    .Replace("(", "")
                                    .Replace("if", "")
                                    .Replace("else", "");

            if (!SearchParam.Any(x => EdittedLine.StartsWith(x)))
                continue;

            var digits = Line.SkipWhile(c => !Char.IsDigit(c)).TakeWhile(Char.IsDigit).ToArray();
            string sQuestID = new string(digits);
            int QuestID = int.Parse(sQuestID);

            if (!QuestIDs.Contains(QuestID) && !QuestTree.Exists(x => x.ID == QuestID))
                QuestIDs.Add(QuestID);

            if (t < 31)
                t++;
            if (t == 30)
            {
                stopWatch.Stop();
                TimeSpan sw = stopWatch.Elapsed;
                TimeSpan ts = TimeSpan.FromSeconds(Convert.ToInt32((SelectedLines.Count / (30 / sw.TotalSeconds)) - sw.TotalSeconds));
                string Estimate;
                if (ts.TotalSeconds > 60)
                    Estimate = string.Format("{0:D2}m{1:D2}s", ts.Minutes, ts.Seconds);
                else Estimate = string.Format("{0:D2}s", ts.Seconds);
                Core.Logger($"Estimated Scanning Time: {Estimate}");
            }
        }
        if (stopWatch.IsRunning)
            stopWatch.Stop();

        Core.Logger($"Loading {QuestIDs.Count} Quests");
        if (QuestIDs.Count > 30)
            Core.Logger($"Estimated Loading Time: {Convert.ToInt32(QuestIDs.Count / 30 * 1.6)}s");

        for (int i = 0; i < QuestIDs.Count; i = i + 30)
        {
            Bot.Quests.Load(QuestIDs.ToArray()[i..(QuestIDs.Count > i ? QuestIDs.Count : i + 30)]);
            Bot.Sleep(1500);
        }

        PreLoaded = true;
    }
    private bool PreLoaded = false;

    private int PreviousQuestID = 0;
    private bool PreviousQuestState = false;
}