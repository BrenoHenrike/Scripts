# Timings & Handlers

If you enable `SafeTimings` as recommended, you can ignore most of this document. The `ScriptWait` class has many methods useful for pausing the bot's execution until a desired condition is met.

- [Timings & Handlers](#timings--handlers)
  - [Sleeping](#sleeping)
  - [Waiting](#waiting)
    - [Examples](#examples)
  - [Handlers and Scheduling](#handlers-and-scheduling)
    - [Handlers](#handlers)
    - [Scheduling](#scheduling)

## Sleeping

If you would simply like to pause exeuction for a specific length of time, you can do this easily using:

```csharp
Core.Sleep(time);
```

where `time` is the time in milliseconds to sleep. For example,

```csharp
Core.Sleep(1000);
```

will sleep the bot for 1000 milliseconds (1 second).

## Waiting

There are also many actions that can be waiting for using `ScriptInterface#Wait`. All of these methods have a default `timeout` parameter ommited as this typically does not need to be modified. The return types of these methods are mostly irellevant so they have also been ommited.

If `SafeTimings` is enabled, these methods are used by default to wait for actions to complete.

| Method Definition | Description |
|---|---|
| `ForPlayerPosition(float x, float y)` | Waits for the player to move to the given position. |
| `ForSkillCooldown(int index)` | Waits for the specified skill be cooled down (index goes from 1 to 4). |
| `ForCombatExit()` | Waits for the player to be out of combat. |
| `ForMonsterDeath()` | Waits for the player to have no target. |
| `ForMonsterSpawn(string name)` | Waits for a monster with the specified name to exist (and be alive) in the current cell. |
| `ForFullyRested()` | Waits for the player to have full health and mana. |
| `ForMapLoad(string map)` | Waits for the current map name to match `map`. This map name excludes any room numbers. |
| `ForCellChange(string cell)` | Waits for the current cell name to match `cell`. |
| `ForPickup(string item)` | Waits for the drop with name `item` to be picked up. This actually waits until that drop no longer exists, which is equivalent. |
| `ForDrop(string item)` | Waits for the drop with name `item` to exist. |
| `ForItemBuy()` | Waits for any item to be bought. |
| `ForItemSell()` | Waits for any item to be sold. |
| `ForItemEquip(int id)` | Waits for the item with the specified id to be equipped. |
| `ForItemEquip(string item)` | Waits for the item with the specified name to be equipped. |
| `ForBankLoad()` | Waits for the bank to be loaded. |
| `ForBankToInventory(string item)` | Waits for the specified item to be moved from the player's bank to their inventory. This actually waits for the item to no longer exist in the bank. |
| `ForInventoryToBank(string item)` | Waits for the specified item to be moved from the player's inventory to their bank. This actually waits for the itme to no longer exist in the player's inventory. |
| `ForQuestAccept(int id)` | Waits for the quest with the specified id to be accepted. |
| `ForQuestComplete(int id)` | Waits for the quest with the specified id to be turned in (no longer accepted). |
| `For(Func func, object val)` | Waits for the function `func` to return `val`. |
| `ForTrue(Func func)` | Waits for `func` to return `true`. |
| `ForActionCooldown(GameActions action)` | Waits for the specified game action to cool down. |

### Examples

Even when using `SafeTimings`, it is sometimes suitable to use these wait methods. For example, when turning in a quest, it is recommended you do the following:

```csharp
bot.Quests.EnsureComplete(id);
bot.Wait.ForDrop(reward);
bot.Player.Pickup(reward);
```

where `id` is the quest id you are turning in and `reward` is the name of the reward from the quest you would like to obtain. This ensures that the quest reward is actually obtained as there can be a small delay between turning in a quest and its reward appearing.

Also, if you are not used to see [delegates](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/) (like [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-1?view=net-5.0), [Action](https://docs.microsoft.com/en-us/dotnet/api/system.action?view=net-5.0) and [Predicate](https://docs.microsoft.com/en-us/dotnet/api/system.predicate-1?view=net-5.0)), they are, putting it simply, reference of methods. They allow you to use an anonymous method as a parameter for another method, you can use them in `For` and `ForTrue` methods but as most of `Wait` methods already cover essential parts of game mechanics, you will see them beign more used for `Handlers` in the next topic.

## Handlers and Scheduling

### Handlers

If you would like to run code repeatedly alongside your script at a fixed interval, you can do so using a `ScriptHandler`. The best way to do this is through the `ScriptInterface#RegisterHandler(int ticks, Action<ScriptInterface> func)` method. This method takes a number of `ticks` (the interval in units of **250ms *(WAIT_SLEEP)***), and the function, `func`, to be run.

For example,

```csharp
bot.RegisterHandler(2, b => {
    b.Log("Test");
});
```

will log `"Test"` every 500ms.

Handlers are cleared when a script stops or when a new script starts running.

### Scheduling

If you want to schedule some code to run **once** after a set period of time, you can use `ScriptInterface#Schedule(int delay, Action<ScriptInterface> func)` where `delay` is the time in ms after the code is run, and `func` is the function to run.

For example,

```csharp
bot.Schedule(500, b => {
    b.Log("Test");
});
```

will log `"Test"` once after 500ms.

---------
<center><a href="/Rbot-Scripts/Options and Lite" title="Options & Lite">◄ Previous</a> — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — <a href="/Rbot-Scripts/Monsters" title="Monsters">Next ►</a></center>