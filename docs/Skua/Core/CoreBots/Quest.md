# Quest

As with any [property](#properties) or [method](#methods) from the `CoreBots.cs` file, you can call upon them by starting with `Core.` (*Core Dot*)

- [Quest](#quest)
  - [Methods](#methods)

## Methods

| Method Definition                                              |     Return Type      | Description                                                                                                                                                                                     |
| -------------------------------------------------------------- | :------------------: | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `RegisterQuests(params int[] questIDs)`                        |        *void*        | This will register quests to be completed while doing something else, i.e. while in combat.<br>If it has quests already registered, it will cancel them first and then register the new quests. |
| `CancelRegisteredQuests()`                                     |        *void*        | Cancels the current registered quests.                                                                                                                                                          |
| `EnsureAccept(int questID)`                                    |        *bool*        | Accepts the quest and AddDrop's the requierments that are not temporary                                                                                                                         |
| `EnsureAccept(params int[] questIDs)`                          |        *void*        | Accepts all the quests given                                                                                                                                                                    |
| `EnsureComplete(int questID, int itemID = -1)`                 |        *void*        | Completes the quest with a choose-able reward item                                                                                                                                              |
| `EnsureComplete(params int[] questIDs)`                        |        *void*        | Completes all the quests given but doesn't support quests with choose-able rewards                                                                                                              |
| `EnsureCompleteChoose(int questID, string[]? itemList = null)` |        *bool*        | Completes a quest and choose any item from it that you don't have (automatically accepts the drop)                                                                                              |
| `EnsureLoad(int questID)`                                      |       *Quest*        | Loads the quest and returns the Quest Object                                                                                                                                                    |
| `EnsureLoad(params int[] questIDs)`                            | *List&lt;Quest&gt; * | Loads the quests and returns multiple Quest Objects                                                                                                                                             |
| `ChainComplete(int questID, int itemID = -1)`                  |        *void*        | Accepts and then completes the quest, used inside a loop                                                                                                                                        |
| `isCompletedBefore(int QuestID)`                               |        *bool*        | Check if a quest that is part of a story line is completed before                                                                                                                               |

---------
<center>
    <a href="Drops" title="Drops">◄ Previous</a> 
    — <a href="index" title="Back to Index">Index</a> — 
    <a href="Kill" title="Kill">Next ►</a>
</center>