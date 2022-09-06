
# CoreStory

As with any [property](#properties) or [method](#methods) from the `CoreStory.cs` file, you can call upon them by starting with `Story.` (*Story Dot*)

- [CoreStory](#corestory)
  - [Methods](#methods)

## Methods

| Method Definition | Return Type | Description |
| --- | :---: | --- |
| `KillQuest(int QuestID, string MapName, string MonsterName, bool GetReward = true, string Reward = "All", bool AutoCompleteQuest = true)` | *void*| Kills a monster for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one. |
| `KillQuest(int QuestID, string MapName, string[] MonsterNames, bool GetReward = true, string Reward = "All", bool AutoCompleteQuest = true)` | *void* | Kills an array of monsters for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one. |
| `MapItemQuest(int QuestID, string MapName, int MapItemID, int Amount = 1, bool GetReward = true, string Reward = "All", bool AutoCompleteQuest = true)` | *void* | Gets a MapItem X times for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one. |
| `MapItemQuest(int QuestID, string MapName, int[] MapItemIDs, int Amount = 1, bool GetReward = true, string Reward = "All", bool AutoCompleteQuest = true)` | *void* | Gets a MapItem X times for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one. |
| `BuyQuest(int QuestID, string MapName, int ShopID, string ItemName, int Amount = 1, bool GetReward = true, string Reward = "All", bool AutoCompleteQuest = true)` | *void* | Accepts a quest and then turns it in again |
| `QuestProgression(int QuestID, bool GetReward = true, string Reward = "All")` | *bool* | Skeleton of KillQuest, MapItemQuest, BuyQuest and ChainQuest. Only needs to be used inside a script if the quest spans across multiple maps |
| `PreLoad()` | *void* | Put this at the start of your story script so that the bot will load all quests that are used in the bot. This will speed up any progression checks tremendiously. |

---------