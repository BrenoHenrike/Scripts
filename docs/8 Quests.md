Quests
======
Quests can be loaded, accepted and completed through `ScriptInterface#Quests`.

#### Properties
| Properties | Type | Description |
|---|:---:|---|
| `ActiveQuests` | *List\<RBot.Quests.Quest>* | A list of currently accepted quests. |
| `CompletedQuests` | *List\<RBot.Quests.Quest>* | A list of currently accepted quests which are ready to turn in. |
| `QuestTree` | *List\<RBot.Quests.Quest>* | A list of all quests that have been loaded in the current session. |

#### Methods
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

#### The Quest Class
Some properties give lists of `Quest` objects. These objects have the following properties:

| Property | Type | Description |
|---|:---:|---|
| `Name` | *string* | The name of the quest. |
| `ID` | *int* | The id of the quest. |
| `Slot` | *int* | The slot of the quest. Used to check if it's unlocked (`< 0`) in a history. |
| `Value` | *int* | The value of the quest. Used to check if it's unlocked (`Slot >= Value - 1`) in a history. |
| `Description` | *string* | The description of the quest. |
| `EndText` | *string* | The description of the quest after completion. |
| `Once` | *bool* | Whether this quest can only be completed once. |
| `Field` | *string* | The field of the quest. Used to check if a daily quest is completed (`!= null`) |
| `Index` | *int* | The index of the quest. Used to check if a daily quest is completed (`!= 0`) |
| `Upgrade` | *bool* | Whether this quest requires upgrade. |
| `Level` | *int* | The level required to accept the quest. |
| `RequiredClassID` | *int* | The id of the class required to accept the quest. |
| `RequiredClassPoints` | *int* | The class points required to accept the quest. |
| `RequiredFactionId` | *int* | The faction required to accept the quest. |
| `RequiredFactionRep` | *int* | The required faction rep to accept the quest. |
| `Gold` | *int* | The amount of gold this quest gives as a reward. |
| `XP` | *int* | The amount of XP this quest gives as a reward. |
| `Status` | *string* | The status of the quest. If not equals `null`, the quest is active; if equals `c`, the quest is completed. |
| `Active` | *bool* | Indicates whether the quest is active or not. |
| `AcceptRequirements` | *RBot.Items.ItemBase* | The items required in the player's inventory to accept the quest. |
| `Requirements` | *RBot.Items.ItemBase* | The required items/temp items to turn in the quest. |
| `Rewards` | *RBot.Items.ItemBase* | The items given as a reward for completing the quest. |
| `SimpleRewards` | *RBot.Items.SimpleReward* | Item drop rates are mapped to their IDs in this list. |

You can use the method `Quest.ToString()` to return a string formatted like `{Name} [{ID}]` with each respective property filled with the quest info.

These properties can be used as a trustful source of information about the quest, preventing typos that can be found in the wiki.

|[◄ Previous](/docs/7%20Player "7. Player") ——— [Next ►]() |
| :---: |