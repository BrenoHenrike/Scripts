# Quests

Quests can be loaded, accepted and completed through `ScriptInterface#Quests`.

- [Quests](#quests)
  - [Properties](#properties)
  - [Methods](#methods)

## Properties

| Properties | Type | Description |
|---|:---:|---|
| `ActiveQuests` | *List\<RBot.Quests.Quest>* | A list of currently accepted quests. |
| `CompletedQuests` | *List\<RBot.Quests.Quest>* | A list of currently accepted quests which are ready to turn in. |
| `QuestTree` | *List\<RBot.Quests.Quest>* | A list of all quests that have been loaded in the current session. |

## Methods

| Method Definition | Return Type | Description |
|---|:---:|---|
| `Load(int id)` | *void* | Loads the quest with the given id. |
| `EnsureLoad(int id)` | *RBot.Quests.Quest* | Loads the quest with the specified id and waits until it's in the quest tree. |
| `TryGetQuest(int id, out Quest quest)` | *bool, out RBot.Quests.Quest* | Tries to get the quest with the given ID if it is loaded. |
| `Accept(int id)` | *void* | Accepts the quest with the specified id. This requires the quest to be loaded first. `EnsureAccept` should always be used instead of this method (see below). |
| `EnsureAccept(int id, int tries = 100)` | *bool* | Attempts to accept the quest with the specified id until it is successful. This does not require the quest to be loaded first. `tries` is the amount of times it will try to accept the quest until giving up. |
| `Complete(int id, int itemId = -1, bool special = false)` | *void* | Completes the quest with the specified id. This requires the quest to be loaded first. `itemId` is the id of the item to obtain for quests with optional rewards. I don't really know what a `special` quest is but maybe you do. Again, `EnsureComplete` should always be used instead of this. |
| `EnsureComplete(int id, int itemId = -1, bool special = false, int tries = 100)` | *bool* | Attempts to turn in the quest with the specified id until it is successful. This does not require the quest to be loaded first. `tries` is the amount of time it will try to accept the quest until giving up. |
| `IsInProgress(int id)` | *bool* | Checks whether the specified quest is currently accepted. |
| `CanComplete(int id)` | *bool* | Checks whether the given quest can be turned in. |
| `IsDailyComplete(int id)` | *bool* | Checks whether the given quest is a daily quest and that the quest has already been completed today. |
| `IsUnlocked(int id)` | *bool* | Checks if a storyline quest is unlocked. |
| `IsAvailable(int id)` | *bool* | Performs all checks to see if a quest can be accepted/turned in. |

---------
<center><a href="/Rbot-Scripts/Player" title="Player">◄ Previous</a> — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — <a href="/Rbot-Scripts/Map" title="Map">Next ►</a></center>