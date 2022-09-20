//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Dynamic;

public class CoreStory
{
    // [Can Change]
    // True = Bot only does its smart checks on quests with Once: True 
    // False = Bot does it's smart checks on all quest
    // Recommended: false
    // Used for testing bots, dont toggle this as a user
    public bool TestBot { get; set; } = false;
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }


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

        SmartKillMonster(QuestID, MapName, MonsterName, 50, Requirements[0].Coins);
        if (AutoCompleteQuest)
            Bot.Wait.ForPickup(Requirements.ToString());
        TryComplete(QuestData, AutoCompleteQuest);

        void SmartKillMonster(int questID, string map, string monster, int iterations = 20, bool completeQuest = false, bool publicRoom = false)
        {
            Core.EnsureAccept(questID);
            _AddRequirement(questID);
            Core.Join(map, publicRoom: publicRoom);
            _SmartKill(monster, iterations);
            if (completeQuest)
                Core.EnsureComplete(questID);
            CurrentRequirements.Clear();
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

        SmartKillMonster(QuestID, MapName, MonsterNames, 50, Requirements[0].Coins);
        if (AutoCompleteQuest)
            Bot.Wait.ForPickup(Requirements.ToString());
        TryComplete(QuestData, AutoCompleteQuest);

        void SmartKillMonster(int questID, string map, string[] monsters, int iterations = 20, bool completeQuest = false, bool publicRoom = false)
        {
            Core.EnsureAccept(questID);
            _AddRequirement(questID);
            Core.Join(map, publicRoom: publicRoom);
            foreach (string monster in monsters)
                _SmartKill(monster, iterations);
            if (completeQuest)
                Core.EnsureComplete(questID);
            CurrentRequirements.Clear();
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
        TryComplete(QuestData, AutoCompleteQuest);
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
        TryComplete(QuestData, AutoCompleteQuest);
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
        TryComplete(QuestData, AutoCompleteQuest);
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

    private void TryComplete(Quest QuestData, bool AutoCompleteQuest)
    {
        if (!Bot.Quests.CanComplete(QuestData.ID))
            return;

        Bot.Sleep(Core.ActionDelay);
        if (AutoCompleteQuest)
            Core.EnsureComplete(QuestData.ID);
        Bot.Wait.ForQuestComplete(QuestData.ID);
        Core.Logger($"Completed Quest: [{QuestData.ID}] - \"{QuestData.Name}\"");
        Bot.Sleep(1500);
    }

    /// <summary>
    /// Skeleton of KillQuest, MapItemQuest, BuyQuest and ChainQuest. Only needs to be used inside a script if the quest spans across multiple maps
    /// </summary>
    /// <param name="QuestID">ID of the quest</param>
    /// <param name="GetReward">Whether or not the <paramref name="Reward"/> should be added with AddDrop</param>
    /// <param name="Reward">What item should be added with AddDrop</param>
    public bool QuestProgression(int QuestID, bool GetReward = true, string Reward = "All")
    {
        if (QuestID != 0 && PreviousQuestID == QuestID)
            return PreviousQuestState;
        PreviousQuestID = QuestID;

        if (!CBO_Checked)
        {
            if (Core.CBOBool("BCO_Story_TestBot", out bool _TestBot))
                TestBot = _TestBot;
            CBO_Checked = true;
        }

        Quest QuestData = Core.EnsureLoad(QuestID);
        ItemBase[] Rewards = QuestData.Rewards.ToArray();

        if (QuestData == null)
        {
            Core.Logger($"Quest [{QuestID}] doesn't exist", messageBox: true, stopBot: true);
            return true;
        }

        int timeout = 0;
        while (!Bot.Quests.IsUnlocked(QuestID))
        {
            Bot.Sleep(1000);
            timeout++;

            if (timeout > 15)
            {
                int currentValue = Bot.Flash.CallGameFunction<int>("world.getQuestValue", QuestData.Slot);
                if (QuestData.Value - currentValue <= 2)
                {
                    string message1 = $"A server/client desync happened (common) for your quest progress, the bot cannot continue.|" +
                                        "Please relog and restart the bot in order to continue.|" +
                                        "Once Skua is out this popup will be replaced with a automatic relog.|" +
                                        "No report is neccessary at this time.";
                    Core.Logger(message1.Replace("|", " "));
                    Bot.ShowMessageBox(message1.Replace("|", "\n"), "Quest not unlocked", "OK");
                    Bot.Stop(true);
                }
                string message2 = $"Quest \"{QuestData.Name}\" [{QuestID}] is not unlocked.|" +
                                 $"Expected value = [{QuestData.Value - 1}/{QuestData.Slot}], recieved = [{currentValue}/{QuestData.Slot}]|" +
                                  "Please fill in the Skua Scripts Form to report this.|" +
                                  "Do you wish to be brought to the form?";
                Core.Logger(message2.Replace("|", " "));
                if (Bot.ShowMessageBox(message2.Replace("|", "\n"), "Quest not unlocked") == true)
                {
                    string path = Bot.Manager.LoadedScript.Replace(Core.AppPath, "").Replace("\\Scripts\\", "").Replace(".cs", "");
                    Process.Start("explorer", $"\"https://docs.google.com/forms/d/e/1FAIpQLSeI_S99Q7BSKoUCY2O6o04KXF1Yh2uZtLp0ykVKsFD1bwAXUg/viewform?usp=pp_url&" +
                                                 "entry.2118425091=Bug+Report&" +
                                                $"entry.290078150={path}&" +
                                                 "entry.1803231651=I+got+a+popup+saying+a+quest+was+not+unlocked&" +
                                                $"entry.1918245848={QuestData.ID}&" +
                                                $"entry.1809007115={QuestData.Value - 1}/{QuestData.Slot}&" +
                                                $"entry.493943632={currentValue}/{QuestData.Slot}&" +
                                                $"entry.148016785={QuestData.Name}\"");
                }
                Bot.Stop(true);
            }
        }

        if (Core.isCompletedBefore(QuestID) && (TestBot ? QuestData.Once : true))
        {
            if (TestBot)
                Core.Logger($"Skipped (Once = true): [{QuestID}] - \"{QuestData.Name}\"");
            else Core.Logger($"Already Completed: [{QuestID}] - \"{QuestData.Name}\"");
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

        Core.Logger($"Doing Quest: [{QuestID}] - \"{QuestData.Name}\"");
        Core.EquipClass(ClassType.Solo);
        PreviousQuestState = false;
        return false;
    }
    private bool CBO_Checked = false;

    /// <summary>
    /// Put this at the start of your story script so that the bot will load all quests that are used in the bot. This will speed up any progression checks tremendiously.
    /// </summary>
    public void PreLoad(Object _this, [CallerMemberName] string caller = "")
    {
        List<int> QuestIDs = new();
        string[] ScriptSlice = Core.CompiledScript();

        int classStartIndex = Array.IndexOf(ScriptSlice, $"public class {_this}");
        int classEndIndex = Array.IndexOf(ScriptSlice[(classStartIndex)..], "}") + classStartIndex + 1;
        ScriptSlice = ScriptSlice[(classStartIndex)..classEndIndex];

        int methodStartIndex = -1;
        foreach (string p in new string[] { "public", "private" })
        {
            foreach (string s in new string[] { "void", "bool", "string", "int" })
            {
                methodStartIndex = Array.FindIndex(ScriptSlice, l => l.Contains($"{p} {s} {caller}"));
                if (methodStartIndex > -1)
                    break;
            }
            if (methodStartIndex > -1)
                break;
        }
        if (methodStartIndex == -1)
        {
            Core.Logger("Failed to parse methodStartIndex, no quests will be pre-loaded");
            return;
        }

        int methodIndentCount = ScriptSlice[methodStartIndex + 1].IndexOf('{');
        string indent = "";
        for (int i = 0; i < methodIndentCount; i++)
            indent += " ";
        int methodEndIndex = Array.FindIndex(ScriptSlice, methodStartIndex, l => l == indent + "}") + 1;

        ScriptSlice = ScriptSlice[methodStartIndex..methodEndIndex];

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

        foreach (string Line in ScriptSlice)
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

            if (!QuestIDs.Contains(QuestID) && !Bot.Quests.Tree.Exists(x => x.ID == QuestID))
                QuestIDs.Add(QuestID);
        }

        if (QuestIDs.Count() + Bot.Quests.Tree.Count() > Core.LoadedQuestLimit
            && QuestIDs.Count < Core.LoadedQuestLimit)
        {
            Bot.Flash.SetGameObject("world.questTree", new ExpandoObject());
        }
        else if (QuestIDs.Count > (Core.LoadedQuestLimit - Bot.Quests.Tree.Count()))
        {
            Core.Logger($"Found {QuestIDs.Count} Quests, this exceeds the max amount of loaded quests ({Core.LoadedQuestLimit}). No quests will be loaded.");
            return;
        }

        Core.Logger($"Loading {QuestIDs.Count} Quests.");
        if (QuestIDs.Count > 30)
            Core.Logger($"Estimated Loading Time: {Convert.ToInt32(QuestIDs.Count / 30 * 1.6)}s");

        for (int i = 0; i < QuestIDs.Count; i = i + 30)
        {
            Bot.Quests.Load(QuestIDs.ToArray()[i..(QuestIDs.Count > i ? QuestIDs.Count : i + 30)]);
            Bot.Sleep(1500);
        }
    }
    private int PreviousQuestID = 0;
    private bool PreviousQuestState = false;

    private void _SmartKill(string monster, int iterations = 20)
    {
        bool repeat = true;
        for (int j = 0; j < iterations; j++)
        {
            if (CurrentRequirements.Count == 0)
                break;
            if (CurrentRequirements.Count == 1)
            {
                if (_RepeatCheck(ref repeat, 0))
                    break;
                _MonsterHunt(ref repeat, monster, CurrentRequirements[0].Name, CurrentRequirements[0].Quantity, CurrentRequirements[0].Temp, 0);
                break;
            }
            else
            {
                for (int i = CurrentRequirements.Count - 1; i >= 0; i--)
                {
                    if (j == 0 && (Core.CheckInventory(CurrentRequirements[i].Name, CurrentRequirements[i].Quantity)))
                    {
                        CurrentRequirements.RemoveAt(i);
                        continue;
                    }
                    if (j != 0 && Core.CheckInventory(CurrentRequirements[i].Name))
                    {
                        if (_RepeatCheck(ref repeat, i))
                            break;
                        _MonsterHunt(ref repeat, monster, CurrentRequirements[i].Name, CurrentRequirements[i].Quantity, CurrentRequirements[i].Temp, i);
                        break;
                    }
                }
            }
            if (!repeat)
                break;

            Bot.Hunt.Monster(monster);
            Bot.Drops.Pickup(CurrentRequirements.Where(item => !item.Temp).Select(item => item.Name).ToArray());
            Bot.Sleep(Core.ActionDelay);
        }
    }
    private List<ItemBase> CurrentRequirements = new();
    private void _MonsterHunt(ref bool shouldRepeat, string monster, string itemName, int quantity, bool isTemp, int index)
    {
        Bot.Hunt.ForItem(monster, itemName, quantity, isTemp);
        CurrentRequirements.RemoveAt(index);
        shouldRepeat = false;
    }
    private bool _RepeatCheck(ref bool shouldRepeat, int index)
    {
        if (Core.CheckInventory(CurrentRequirements[index].Name, CurrentRequirements[index].Quantity))
        {
            CurrentRequirements.RemoveAt(index);
            shouldRepeat = false;
            return true;
        }
        return false;
    }
    private int lastQuestID;
    private void _AddRequirement(int questID)
    {
        if (questID > 0 && questID != lastQuestID)
        {
            lastQuestID = questID;
            Quest quest = Core.EnsureLoad(questID);
            if (quest == null)
            {
                Core.Logger($"Quest [{questID}] doesn't exist", messageBox: true, stopBot: true);
                return;
            }
            List<string> reqItems = new();
            quest.AcceptRequirements.ForEach(item => reqItems.Add(item.Name));
            quest.Requirements.ForEach(item =>
            {
                if (!CurrentRequirements.Where(i => i.Name == item.Name).Any())
                {
                    if (!item.Temp)
                        reqItems.Add(item.Name);
                    CurrentRequirements.Add(item);
                }
            });
            Core.AddDrop(reqItems.ToArray());
        }
    }
}