/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Quests;
using System.Diagnostics;
using System.Dynamic;
using System.Runtime.CompilerServices;

public class CoreStory
{
    // [Can Change]
    // True = Bot only does its smart checks on quests with Once: True 
    // False = Bot does it's smart checks on all quest
    // Recommended: false
    // Used for testing bots, dont toggle this as a user
    public bool TestBot { get; set; } = false;

    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
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
        Core.DebugLogger(this);
        Quest QuestData = Core.EnsureLoad(QuestID);
        if (QuestProgression(QuestID, GetReward, Reward))
        {
            Core.DebugLogger(this);
            return;
        }
        Core.DebugLogger(this);

        SmartKillMonster(QuestID, MapName, MonsterName);
        Core.DebugLogger(this);
        if (AutoCompleteQuest)
        {
            Core.DebugLogger(this);
            foreach (ItemBase item in QuestData.Requirements)
            {
                Core.DebugLogger(this);
                Bot.Wait.ForPickup(item.ID);
            }
        }
        Core.DebugLogger(this);
        TryComplete(QuestData, AutoCompleteQuest);
        Core.DebugLogger(this);

        void SmartKillMonster(int questID, string map, string monster)
        {
            Core.DebugLogger(this);
            Core.EnsureAccept(questID);
            Core.DebugLogger(this);
            _AddRequirement(questID);
            Core.DebugLogger(this);
            Core.Join(map);
            Core.DebugLogger(this);
            _SmartKill(monster, 20);
            Core.DebugLogger(this);
            CurrentRequirements.Clear();
        }
    }
    /// <summary>
    /// Kills an array of monsters for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one.
    /// </summary>
    /// <param name="QuestID">ID of the quest</param>
    /// <param name="MapName">Map where the monsters are</param>
    /// <param name="MonsterNames">Array of monster names to kill</param>
    /// <param name="GetReward">Whether or not the <paramref name="Reward"/> should be added with AddDrop</param>
    /// <param name="Reward">What item should be added with AddDrop</param>
    /// <param name="AutoCompleteQuest">If the method should turn in the quest for you when the quest can be completed</param>
    public void KillQuest(int QuestID, string MapName, string[] MonsterNames, bool GetReward = true, string Reward = "All", bool AutoCompleteQuest = true)
    {

        Quest QuestData = Core.EnsureLoad(QuestID);
        if (QuestProgression(QuestID, GetReward, Reward))
        {
            return;
        }

        SmartKillMonster(QuestID, MapName, MonsterNames);
        if (AutoCompleteQuest)
        {
            foreach (ItemBase item in QuestData.Requirements)
            {
                Bot.Wait.ForPickup(item.ID);
            }
        }
        TryComplete(QuestData, AutoCompleteQuest);

        void SmartKillMonster(int questID, string map, string[] monsters)
        {
            Core.EnsureAccept(questID);
            _AddRequirement(questID);
            Core.Join(map);
            foreach (string monster in monsters)
            {
                _SmartKill(monster, 20);
            }
            CurrentRequirements.Clear();
        }
    }

    /// <summary>
    /// Gets a MapItem X times for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one.
    /// </summary>
    /// <param name="QuestID">ID of the quest</param>
    /// <param name="MapName">Map where the items are</param>
    /// <param name="MapItemID">ID of the item</param>
    /// <param name="Amount">The amount of <paramref name="MapItemID"/> to grab</param>
    /// <param name="GetReward">Whether or not the <paramref name="Reward"/> should be added with AddDrop</param>
    /// <param name="Reward">What item should be added with AddDrop</param>
    /// <param name="AutoCompleteQuest">If the method should turn in the quest for you when the quest can be completed</param>
    public void MapItemQuest(int QuestID, string MapName, int MapItemID, int Amount = 1, bool GetReward = true, string Reward = "All", bool AutoCompleteQuest = true)
    {
        Quest QuestData = Core.EnsureLoad(QuestID);

        if (QuestProgression(QuestID, GetReward, Reward))
        {
            return;
        }

        Core.EnsureAccept(QuestID);
        Core.GetMapItem(MapItemID, Amount, MapName);
        TryComplete(QuestData, AutoCompleteQuest);
    }

    /// <summary>
    /// Gets a MapItem X times for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one.
    /// </summary>
    /// <param name="QuestID">ID of the quest</param>
    /// <param name="MapName">Map where the items are</param>
    /// <param name="MapItemIDs">ID of the items</param>
    /// <param name="Amount">The amount of <paramref name="MapItemIDs"/> to grab</param>
    /// <param name="GetReward">Whether or not the <paramref name="Reward"/> should be added with AddDrop</param>
    /// <param name="Reward">What item should be added with AddDrop</param>
    /// <param name="AutoCompleteQuest">If the method should turn in the quest for you when the quest can be completed</param>
    public void MapItemQuest(int QuestID, string MapName, int[] MapItemIDs, int Amount = 1, bool GetReward = true, string Reward = "All", bool AutoCompleteQuest = true)
    {
        Quest QuestData = Core.EnsureLoad(QuestID);

        if (QuestProgression(QuestID, GetReward, Reward))
        {
            return;
        }

        Core.EnsureAccept(QuestID);
        foreach (int MapItemID in MapItemIDs)
        {
            Core.GetMapItem(MapItemID, Amount, MapName);
        }
        TryComplete(QuestData, AutoCompleteQuest);
    }

    /// <summary>
    /// Gets multiple MapItems from different maps for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one.
    /// </summary>
    /// <param name="QuestID">ID of the quest</param>
    /// <param name="MapItems">Array of tuples where each tuple contains the MapItemID, amount, and MapName</param>
    /// <param name="GetReward">Whether or not the <paramref name="Reward"/> should be added with AddDrop</param>
    /// <param name="Reward">What item should be added with AddDrop</param>
    /// <param name="AutoCompleteQuest">If the method should turn in the quest for you when the quest can be completed</param>
    public void MapItemQuest(int QuestID, (int MapItemID, int Amount, string MapName)[] MapItems, bool GetReward = true, string Reward = "All", bool AutoCompleteQuest = true)
    {
        Quest QuestData = Core.EnsureLoad(QuestID);

        if (QuestProgression(QuestID, GetReward, Reward))
        {
            return;
        }

        Core.EnsureAccept(QuestID);
        foreach (var (MapItemID, Amount, MapName) in MapItems)
        {
            Core.GetMapItem(MapItemID, Amount, MapName);
        }
        TryComplete(QuestData, AutoCompleteQuest);
    }

    /// <summary>
    /// Buys an item X times for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one.
    /// </summary>
    /// <param name="QuestID">ID of the quest</param>
    /// <param name="MapName">Map where the shop is located</param>
    /// <param name="ShopID">ID of the shop</param>
    /// <param name="ItemName">Name of the item to buy</param>
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

        Core.Sleep();
        if (AutoCompleteQuest)
            Core.ChainComplete(QuestID);
        else
        {
            Core.EnsureAccept(QuestID);
        }
        Bot.Wait.ForQuestComplete(QuestID);
        Core.Logger($"Completed \"{QuestData.Name}\" [{QuestID}]");
        Core.Sleep();
    }

    public void QuestComplete(int questID) => TryComplete(Core.EnsureLoad(questID), true);

    private void TryComplete(Quest questData, bool autoCompleteQuest)
    {
        if (!Bot.Quests.CanComplete(questData.ID))
        {
            return;
        }

        Core.Sleep();

        if (questData.Once && !QuestProgression(questData.ID) || autoCompleteQuest)
        {
            Core.EnsureComplete(questData.ID);
        }

        Bot.Wait.ForQuestComplete(questData.ID);
        Core.Logger($"Completed Quest: [{questData.ID}] - \"{questData.Name}\"", "QuestProgression");
        Core.Sleep();
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

        int timeout = 0;
        while (!Bot.Quests.IsUnlocked(QuestID))
        {
            Core.Sleep(1000);
            timeout++;

            if (timeout > 15)
            {
                int currentValue = Bot.Flash.CallGameFunction<int>("world.getQuestValue", QuestData.Slot);
                Quest? prevQuest = Bot.Quests.Tree.Find(q => q.Slot == QuestData.Slot && q.Value == (currentValue + 1));

                prevQuestReq ??=
                    prevQuest == null || prevQuest.Requirements.All(r => Core.CheckInventory(r.ID, r.Quantity)) ?
                        null :
                        string.Join(',', prevQuest.Requirements.Where(r => !Core.CheckInventory(r.ID, r.Quantity)).Select(i => i.Name));
                prevQuestAReq ??=
                    prevQuest == null || prevQuest.AcceptRequirements.All(r => Core.CheckInventory(r.ID, r.Quantity)) ?
                        null :
                        string.Join(',', prevQuest.Requirements.Where(r => !Core.CheckInventory(r.ID, r.Quantity)).Select(i => i.Name));
                prevQuestExplain ??=
                    prevQuest == null ?
                        string.Empty :
                        $"Quest \"{prevQuest.Name}\" [{prevQuest.ID}] appears to have failed to turn in somehow.|" +
                        (prevQuestReq == null ?
                            string.Empty :
                            $"Missing QuestItems: {prevQuestReq}|") +
                        (prevQuestAReq == null ?
                            string.Empty :
                            $"Missing AcceptRequirements: {prevQuestAReq}|");

                if (lastFailedQuestID != QuestData.ID)
                {
                    if (prevQuest != null && prevQuest.Status == "c")
                    {
                        TryComplete(prevQuest, true);
                        timeout = 0;
                    }
                    else if (QuestData.Value - currentValue <= 2)
                    {
                        Core.Logger("A server/client desync happened (common) for your quest progress, the bot will now restart");
                        lastFailedQuestID = QuestData.ID;
                        timeout = 0;
                        Core.Relogin();
                    }
                }
                else
                {
                    string message2 = $"Quest \"{QuestData.Name}\" [{QuestID}] is not unlocked.|" +
                                     $"Expected value = [{QuestData.Value - 1}/{QuestData.Slot}], recieved = [{currentValue}/{QuestData.Slot}]|" +
                                      prevQuestExplain +
                                      "Please fill in the Skua Scripts Form to report this.|" +
                                      "Do you wish to be brought to the form?";
                    Core.Logger(message2.Replace("|", " "));
                    if (Bot.ShowMessageBox(message2.Replace("|", "\n"), "Quest not unlocked", true) == true)
                    {
                        string url =
                            $"\"https://docs.google.com/forms/d/e/1FAIpQLSeI_S99Q7BSKoUCY2O6o04KXF1Yh2uZtLp0ykVKsFD1bwAXUg/viewform?usp=pp_url&" +

                            "entry.209396189=Skua&" +
                            "entry.2118425091=Bug+Report&" +
                            $"entry.290078150={Core.loadedBot}&" +
                            "entry.1803231651=I+got+a+popup+saying+a+quest+was+not+unlocked&" +

                            $"entry.1918245848={QuestData.ID}&" +
                            $"entry.1809007115={QuestData.Value - 1}/{QuestData.Slot}&" +
                            $"entry.493943632={currentValue}/{QuestData.Slot}&" +
                            $"entry.148016785={QuestData.Name}";

                        if (prevQuest != null)
                            url +=
                                $"&entry.77289389={prevQuest.ID}&" +
                                $"entry.2130921787={prevQuest.Name}&" +
                                $"entry.1966808403={prevQuestReq ?? string.Empty}&" +
                                $"entry.914792808={prevQuestAReq ?? string.Empty}";
                        url += "\"";

                        Process p = new();
                        p.StartInfo.FileName = "rundll32";
                        p.StartInfo.Arguments = "url,OpenURL " + url;
                        p.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System).Split('\\').First() + "\\";
                        p.Start();
                    }
                    Bot.Stop(true);
                }
            }
        }

        if (Core.isCompletedBefore(QuestID) && (!TestBot || QuestData.Once))
        {
            if (TestBot)
                Core.Logger($"Skipped (Once = true): [{QuestID}] - \"{QuestData.Name}\"");
            else Core.Logger($"Already Completed: [{QuestID}] - \"{QuestData.Name}\"");
            PreviousQuestState = true;
            return true;
        }

        if (GetReward)
        {
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
            else Core.AddDrop(Core.QuestRewards(QuestID));
        }

        Core.Logger($"Doing Quest: [{QuestID}] - \"{QuestData.Name}\"");
        // disabled force-solo as _monsterHunt should be good enough to handle already having the item if u get multiple or have them.
        // Core.EquipClass(ClassType.Solo);
        PreviousQuestState = false;
        return false;
    }
    private bool CBO_Checked = false;
    private int lastFailedQuestID = 0;
    private string? prevQuestExplain;
    private string? prevQuestReq;
    private string? prevQuestAReq;

    public void LegacyQuestManager(Action questLogic, params int[] questIDs)
    {
        List<Quest> questData = Core.EnsureLoad(questIDs);
        List<LegacyQuestObject> whereToGet = new();

        //Core.DL_Enable();
        Core.DebugLogger(this, "-------------\t");
        foreach (Quest quest in questData)
        {
            List<ItemBase> desiredQuestReward = quest.Rewards.Where(r => questData.Any(q => q.AcceptRequirements.Any(a => a.ID == r.ID || a.Name == r.Name))).ToList();
            int requiredQuestID = questData.Find((q => q.Rewards.Any(r => quest.AcceptRequirements != null && quest.AcceptRequirements.Any(a => a.ID == r.ID || a.Name == r.Name))))?.ID ?? 0;
            List<ItemBase>? requiredQuestReward = quest.AcceptRequirements?.Where(r => questData.Any(q => q.Rewards.Any(a => a.ID == r.ID || a.Name == r.Name)))?.ToList();

            Core.DebugLogger(this, $"{quest.ID}\t\t");
            Core.DebugLogger(this, $"{desiredQuestReward.FirstOrDefault()?.Name}\t");
            Core.DebugLogger(this, $"{requiredQuestID}\t\t");
            Core.DebugLogger(this, $"{requiredQuestReward?.FirstOrDefault()?.Name}\t");
            Core.DebugLogger(this, "-------------\t");

            if (requiredQuestReward?.Count == 0 && quest.AcceptRequirements?.Count > 0)
            {
                Core.Logger("The managed failed to find the location of \"" +
                string.Join("\" + \"", quest.AcceptRequirements.Select(a => a.Name)) +
                $"\" for Quest ID {quest.ID}, is the function missing a Quest ID?",
                messageBox: true);
                return;
            }

            whereToGet.Add(new(quest.ID, desiredQuestReward, requiredQuestID, requiredQuestReward));
        }

        if (whereToGet.All(x => x.desiredQuestReward.Count == 0) || whereToGet.All(x => x.requiredQuestReward?.Count == 0))
        {
            Core.Logger("None of Quest IDs filled in are supposed to be used in the LegacyQuestManager, " +
                        "please report to the bot makers that they must make this story line in the normal way.",
                        messageBox: true);
            return;
        }

        var finalItemQuest = whereToGet.Find(x => x.desiredQuestReward.Count == 0);
        if (finalItemQuest == null || finalItemQuest.desiredQuestID <= 0)
        {
            Core.Logger("Could not find the Quest ID of the last quest in the item chain");
            return;
        }

        Core.Logger($"Final quest in Legacy Quest Chain: [{finalItemQuest.desiredQuestID}] \"{Core.EnsureLoad(finalItemQuest.desiredQuestID).Name}\"");

        runQuest(finalItemQuest.desiredQuestID);

        foreach (LegacyQuestObject l in whereToGet)
            if (l.requiredQuestReward != null)
                Core.ToBank(l.requiredQuestReward.Select(i => i.ID).ToArray());

        void runQuest(int questID)
        {
            LegacyQuestObject? runQuestData = whereToGet.Find(d => d.desiredQuestID == questID);

            if (runQuestData == null)
            {
                Core.Logger("runQuestData is NULL");
                return;
            }
            Quest questData = Core.EnsureLoad(questID);

            int[] requiredReward = runQuestData.requiredQuestReward!.Select(i => i.ID).ToArray();
            if (runQuestData.desiredQuestReward.Count == 0 && questID != finalItemQuest.desiredQuestID)
            {
                if (!Core.CheckInventory(requiredReward))
                    runQuest(runQuestData.requiredQuestID);
                return;
            }

            int[] desiredReward = runQuestData.desiredQuestReward.Select(i => i.ID).ToArray();
            if (questID != finalItemQuest.desiredQuestID ? Core.CheckInventory(desiredReward) : Core.CheckInventory(Core.EnsureLoad(finalItemQuest.desiredQuestID).Rewards.Select(x => x.ID).ToArray()))
            {
                Core.Logger($"Already Completed: [{questID}] - \"{questData.Name}\"", "QuestProgression");
                return;
            }

            if (!Core.CheckInventory(requiredReward))
                runQuest(runQuestData.requiredQuestID);

            if (_LegacyQuestStop)
                return;

            Core.Logger($"Doing Quest: [{questID}] - \"{questData.Name}\"", "QuestProgression");
            Core.EnsureAccept(questID);
            Core.AddDrop(desiredReward);

            LegacyQuestID = questID;
            questLogic();

            TryComplete(questData, LegacyQuestAutoComplete);
            foreach (int i in desiredReward)
                Bot.Wait.ForPickup(i);
            if (questID == finalItemQuest.desiredQuestID)
                Bot.Drops.Pickup(Core.EnsureLoad(finalItemQuest.desiredQuestID).Rewards.Select(x => x.ID).ToArray());

            LegacyQuestAutoComplete = true;
        }
    }
    private class LegacyQuestObject
    {
        public int desiredQuestID { get; set; } // In order to do ....
        public List<ItemBase> desiredQuestReward { get; set; } // And obtain ...
        public int requiredQuestID { get; set; } // You must do ...
        public List<ItemBase>? requiredQuestReward { get; set; } // And obtain ...

        public LegacyQuestObject(int desiredQuestID, List<ItemBase> desiredQuestReward, int requiredQuestID, List<ItemBase>? requiredQuestReward)
        {
            this.desiredQuestID = desiredQuestID;
            this.desiredQuestReward = desiredQuestReward;
            this.requiredQuestID = requiredQuestID;
            this.requiredQuestReward = requiredQuestReward;
        }
    }
    public int LegacyQuestID = -1;
    public bool LegacyQuestAutoComplete = true;
    private bool _LegacyQuestStop = false;
    public void LegacyQuestStop() => _LegacyQuestStop = true;

    /// <summary>
    /// Put this at the start of your story script so that the bot will load all quests that are used in the bot. This will speed up any progression checks tremendiously.
    /// </summary>
    public void PreLoad(object _this, [CallerMemberName] string caller = "")
    {
        List<int> QuestIDs = new();
        string[] ScriptSlice = Core.CompiledScript();
        if (ScriptSlice.Length == 0)
        {
            Core.Logger("PreLoad failed, cannot read Compiled Script. You might not be on the latest version of Skua");
            return;
        }

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

            char[] digits = Line.SkipWhile(c => !char.IsDigit(c)).TakeWhile(char.IsDigit).ToArray();
            string sQuestID = new(digits);
            int QuestID = int.Parse(sQuestID);

            if (!QuestIDs.Contains(QuestID) && !Bot.Quests.Tree.Exists(x => x.ID == QuestID))
                QuestIDs.Add(QuestID);
        }

        if (QuestIDs.Count + Bot.Quests.Tree.Count > Core.LoadedQuestLimit
            && QuestIDs.Count < Core.LoadedQuestLimit)
        {
            Bot.Flash.SetGameObject("world.questTree", new ExpandoObject());
        }
        else if (QuestIDs.Count > (Core.LoadedQuestLimit - Bot.Quests.Tree.Count))
        {
            Core.Logger($"Found {QuestIDs.Count} Quests, this exceeds the max amount of loaded quests ({Core.LoadedQuestLimit}). No quests will be loaded.");
            return;
        }

        Core.Logger($"Loading {QuestIDs.Count} Quests.");
        if (QuestIDs.Count > 30)
            Core.Logger($"Estimated Loading Time: {Convert.ToInt32(QuestIDs.Count / 30 * 1.6)}s");

        for (int i = 0; i < QuestIDs.Count; i += 30)
        {
            Bot.Quests.Load(QuestIDs.ToArray()[i..(QuestIDs.Count > i ? QuestIDs.Count : i + 30)]);
            Core.Sleep(1500);
        }
    }
    private int PreviousQuestID = 0;
    private bool PreviousQuestState = false;

    private void _SmartKill(string monster, int iterations = 20)
    {
        if (monster == null)
        {
            Core.DebugLogger(this);
            Core.Logger("ERROR: monster is null, please report", stopBot: true);
            Core.DebugLogger(this);
            return;
        }

        Core.DebugLogger(this);
        bool repeat = true;
        Core.DebugLogger(this);
        for (int j = 0; j < iterations; j++)
        {
            Core.DebugLogger(this);
            if (CurrentRequirements.Count == 0)
            {
                Core.DebugLogger(this);
                break;
            }
            if (CurrentRequirements.Count == 1)
            {
                Core.DebugLogger(this);
                if (_RepeatCheck(ref repeat, 0))
                {
                    Core.DebugLogger(this);
                    break;
                }
                Core.DebugLogger(this);
                _MonsterHunt(ref repeat, monster, CurrentRequirements[0].Name, CurrentRequirements[0].Quantity, CurrentRequirements[0].Temp, 0);
                Core.DebugLogger(this);
                break;
            }
            else
            {
                Core.DebugLogger(this);
                for (int i = CurrentRequirements.Count - 1; i >= 0; i--)
                {
                    Core.DebugLogger(this);
                    if (j == 0 && Core.CheckInventory(CurrentRequirements[i].Name, CurrentRequirements[i].Quantity))
                    {
                        Core.DebugLogger(this);
                        CurrentRequirements.RemoveAt(i);
                        Core.DebugLogger(this);
                        continue;
                    }
                    if (j != 0 && Core.CheckInventory(CurrentRequirements[i].Name))
                    {
                        Core.DebugLogger(this);
                        if (_RepeatCheck(ref repeat, i))
                        {
                            Core.DebugLogger(this);
                            break;
                        }
                        Core.DebugLogger(this);
                        _MonsterHunt(ref repeat, monster, CurrentRequirements[i].Name, CurrentRequirements[i].Quantity, CurrentRequirements[i].Temp, i);
                        break;
                    }
                }
            }
            if (!repeat)
            {
                Core.DebugLogger(this);
                break;
            }

            Core.DebugLogger(this);
            Bot.Hunt.Monster(monster);
            Bot.Drops.Pickup(CurrentRequirements.Where(item => !item.Temp).Select(item => item.Name).ToArray());
            Core.Sleep();
        }
    }
    private readonly List<ItemBase> CurrentRequirements = new();
    private void _MonsterHunt(ref bool shouldRepeat, string monster, string itemName, int quantity, bool isTemp, int index)
    {
        // Check if the item is already in inventory
        bool itemInInventory = itemName != null && (isTemp ? Bot.TempInv.Contains(itemName, quantity) : Core.CheckInventory(itemName, quantity));
        if (itemInInventory)
        {
            Core.DebugLogger(this);
            CurrentRequirements.RemoveAt(index);
            shouldRepeat = false;
            return;
        }

        // Find the target monster by name
        Monster? FindMonster()
        {
            return Bot.Monsters.MapMonsters.Find(x => x.Name.FormatForCompare() == monster.FormatForCompare());
        }

        // Ensure player is in the correct cell and not in a cutscene or initialization state
        void EnsurePlayerInCorrectCell(Monster targetMonster)
        {
            while (!Bot.ShouldExit && (Bot.Player.Cell != targetMonster.Cell || Bot.Player.Cell.StartsWith("Cut") || Bot.Player.Cell.StartsWith("init")))
            {
                Core.Jump(targetMonster.Cell, Bot.Player.Pad);
                Bot.Wait.ForCellChange(targetMonster.Cell);
            }
        }

        // Find the target monster
        Monster? targetMonster = FindMonster();
        if (targetMonster == null)
        {
            Core.DebugLogger(this);
            shouldRepeat = false;
            return;
        }

        // Main loop for hunting the monster until the item is acquired
        while (!Bot.ShouldExit && !itemInInventory)
        {
            Core.DebugLogger(this);
            EnsurePlayerInCorrectCell(targetMonster);
            Bot.Combat.Attack(targetMonster);
            Core.Sleep();

            // Update itemInInventory status after attempting to get the item
            itemInInventory = itemName != null && (isTemp ? Bot.TempInv.Contains(itemName, quantity) : Core.CheckInventory(itemName, quantity));
            if (itemInInventory)
                break;
        }

        // Handle item pickup if not temporary
        if (!isTemp)
            Bot.Wait.ForPickup(itemName!);

        Core.DebugLogger(this);
        CurrentRequirements.RemoveAt(index);
        Core.DebugLogger(this);
        shouldRepeat = false;
        Core.DebugLogger(this);
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

            List<string> reqItems = new();
            quest.AcceptRequirements.ForEach(item => reqItems.Add(item.Name));
            quest.Requirements.ForEach(item =>
            {
                if (!CurrentRequirements.Where(i => i.Name == item.Name).Any())
                {
                    if (!item.Temp)
                    {
                        reqItems.Add(item.Name);
                    }
                    CurrentRequirements.Add(item);
                }
            });
            Core.AddDrop(reqItems.ToArray());
        }
    }
}
