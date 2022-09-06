
# CoreStory

As with any [property](#properties) or [method](#methods) from the `CoreStory.cs` file, you can call upon them by starting with `Story.` (*Story Dot*)

- [CoreStory](#corestory)
  - [Methods](#methods)

## Methods

| Method Definition | Return Type | Description |
| ------------------------------------------------------- | :---: | --- |
| `KillQuest(`<br>&emsp;`int QuestID, string MapName,`<br>&emsp;`string MonsterName,`<br>&emsp;`bool GetReward = true,`<br>&emsp;`string Reward = "All",`<br>&emsp;`bool AutoCompleteQuest = true`<br>`)` | *void*| Kills a monster for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one. |
| `KillQuest(`<br>&emsp;`int QuestID, string MapName,`<br>&emsp;`string[] MonsterNames,`<br>&emsp;`bool GetReward = true,`<br>&emsp;`string Reward = "All",`<br>&emsp;`bool AutoCompleteQuest = true`<br>`)` | *void* | Kills an array of monsters for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one. |
| `MapItemQuest(`<br>&emsp;`int QuestID, string MapName,`<br>&emsp;`int MapItemID, int Amount = 1,`<br>&emsp;`bool GetReward = true,`<br>&emsp;`string Reward = "All",`<br>&emsp;`bool AutoCompleteQuest = true`<br>`)` | *void* | Gets a MapItem X times for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one. |
| `MapItemQuest(`<br>&emsp;`int QuestID, string MapName,`<br>&emsp;`int[] MapItemIDs, int Amount = 1,`<br>&emsp;`bool GetReward = true,`<br>&emsp;`string Reward = "All",`<br>&emsp;`bool AutoCompleteQuest = true`<br>`)` | *void* | Gets a MapItem X times for a Quest, and turns in the quest if possible. Automatically checks if the next quest is unlocked. If it is, it will skip this one. |
| `BuyQuest(`<br>&emsp;`int QuestID, string MapName,`<br>&emsp;`int ShopID, string ItemName,`<br>&emsp;`int Amount = 1,`<br>&emsp;`bool GetReward = true,`<br>&emsp;`string Reward = "All",`<br>&emsp;`bool AutoCompleteQuest = true`<br>`)` | *void* | Accepts a quest and then turns it in again |
| `QuestProgression(`<br>&emsp;`int QuestID,`<br>&emsp;`bool GetReward = true,`<br>&emsp;`string Reward = "All"`<br>`)` | *bool* | Skeleton of KillQuest, MapItemQuest, BuyQuest and ChainQuest. Only needs to be used inside a script if the quest spans across multiple maps |
| `PreLoad()` | *void* | Put this at the start of your story script so that the bot will load all quests that are used in the bot. This will speed up any progression checks tremendiously. |

---------