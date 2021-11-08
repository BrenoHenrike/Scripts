## Player
Most of the bot's functionality is called through `ScriptInterface#Player`. There are many methods for movement, combat and properties that can be examined.

### Properties

| Property | Type | Description |
|---|:---:|---|
| `ID` | *int* | The id of the player. This is useful for building packets. |
| `XP` | *int* | The player's current XP. |
| `RequiredXP` | *int* | The XP required for the player to level up. |
| `Cell` | *string* | The name of the cell the player is currently in. |
| `Pad` | *string* | The name of the pad that the player spawned from. |
| `ServerIP` | *string* | The server to which the player is currently connected. |
| `Playing` | *bool* | Indicates whether the player is both logged in and alive. |
| `LoggedIn` | *bool* | Indicates whether the player is logged in. |
| `Username` | *string* | The username of the player. |
| `Password` | *string* | The password of the player. |
| `Kicked` | *bool* | Indicates whether the player has been kicked (disconnected) and is currently at the login screen with the red countdown text. |
| `State` | *int* | The state of the player. `0` is dead, `1` is idle, and `2` is in combat. |
| `InCombat` | *bool* | Indicates whether the player is in combat. |
| `IsMember` | *bool* | Indicates whether the player has an active membership. |
| `Alive` | *bool* | Indicates whether the player is alive. |
| `Health` | *int* | The player's current health. |
| `MaxHealth` | *int* | The player's maximum health. |
| `Mana` | *int* | The player's current mana. |
| `MaxMana` | *int* | The player's maximum mana. |
| `Level` | *int* | The player's level. |
| `Gold` | *int* | The player's gold. |
| `Rank` | *int* | The player's current class rank. |
| `HasTarget` | *bool* | Indicates whether the player currently has a target selected. |
| `Loaded` | *bool* | Indicates whether the player's avatar is loaded. |
| `AccessLevel` | *int* | The player's access level. |
| `Upgrade` | *bool* | Gets/sets whether the player is upgrade or not. |
| `Skills` | *SkillInfo[]* | Gets an array containing information about the player's current skills. |
| `AFK` | *bool* | Indicates whether the player is AFK. |
| `Position` | *PointF* | Gets the player's current position. |
| `X` | *float* | The player's current X coordinate. |
| `Y` | *float* | The player's current Y coordinate. |
| `WalkSpeed` | *int* | Gets or sets the player's walk speed. |
| `Scale` | *int* | This does nothing at the moment. |
| `Factions` | *List\<Faction>* | Gets a list of the player's factions and information about them. |
| `Target` | *Monster* | Gets the player's current target. Returns null if no monster target is selected. |
| `CurrentDrops` | *List\<string>* | Gets a list of item names currently on the drop stack. |
| `CurrentDropInfos` | *List\<DropInfo>* | Gets a list of drops available with their counts. |

### Methods

| Method Definition | Return Type | Description |
|---|:---:|---|
| `Pickup(params string[] items)` | *void* | Picks up the specified items. |
| `PickupFast(params string[] items)` | *void* | Picks up the specified items without waiting for them to be picked up (equivalent to `Pickup` without `SafeTimings`). |
| `RejectExcept(params string[] items)` | *void* | Rejects all drops except for the given ones. |
| `RejectExceptFast(params string[] items)` | *void* | Rejects all drops except for the given ones without waiting for the items to be picked up (equivalent to `Pickup` without `SafeTimings`). |
| `DropExists(string name)` | *bool* | Checks whether the specified drop exists. |
| `PickupAll(bool skipWait = false)` | *void* | Picks up all available drops (If `skipWait = true`, will ignore`SafeTimings` ). |
| `RejectAll(bool skipWait = false)` | *void* | Rejects all available drops (If `skipWait = true`, will ignore `SafeTimings` ). |
| `WalkTo(float x, float y)` | *void* | Walks to the specified point. |
| `SetSpawnPoint()` | *void* | Sets the player's spawn point to the current cell. |
| `SetSpawnPoint(string cell, string pad)` | *void* | Sets the player's spawn point to the given cell and pad. |
| `LoadBank(bool waitForLoad = true)` | *void* | Loads the player's bank. If `waitForLoad = true`, will wait for the bank to load. |
| `Login(string username, string password)` | *void* | Logs into the game with the specified username and password. |
| `Connect(string serverName)` | *void* | Connects to the game server with the specified name. |
| `Connect(RBot.Servers.Server server)` | *void* | Connects to the specified game server. |
| `ConnectIP(string ip)` | *void* | Connects to the game server with the specified IP address. |
| `Recconect(string serverName, int loginDelay = 2000)` | *void* | Logs in and connects to the specified server. Will wait the specified `loginDelay`(in milliseconds) to connect to the server. |
| `Logout()` | *void* | Logs out of the game. Useful to trigger an auto-relogin. |
| `UntargetSelf()` | *void* | Untargets the player if they are currently targeted. |
| `CancelTarget()` | *void* | Untargets the currently targeted player/monster. |
| `ApproachTarget()` | *void* | Walks towards (approaches) the currently selected target. |
| `Attack(string name)` | *void* | Attacks the monster with the specified name. This will not wait until it kills the target. |
| `Attack(RBot.Monsters.Monster monster)` | *void* | Attacks the specified instance of monster. This will not wait until it kills the target. |
| `Attack(int id)` | *void* | Attacks the monster with the specified **map** id. This will not wait until it kills the target. |
| `Kill(string name)` | *void* | Attacks the specified monster until it is dead. |
| `Kill(Monster monster)` | *void* | Attacks the specified monster instance until it is dead. |
| `KillForItem(string name, string item, int quantity, bool tempItem = false, bool rejectElse = true)` | *void* | Kills the specified monster until the desired item is collected in the desired quantity. |
| `KillForItems(string name, string[] items, int[] quantities, bool tempItems = false, bool rejectElse = true)` | *void* | Kills the specified monster until the desired items are collected in the desired quantities. |
| `Hunt(string name)` | *void* | Searches the current room for a monster with the given name and jumps to that monster's cell to kill it. To specify multiple monster names, separate them in `name` with a `'\|'` character. This method disregards `ScriptInterface#HuntPriority`. |
| `HuntWithPriority(string name, HuntPriorities priority)` | *void* | Hunts monsters with a priority. If there is no priority, this has the same behaviour as just Hunt. If a priority is specified, monsters in the map are sorted by the given priority. Once sorted, the monster in the current cell which best matches the priority is killed. Otherwise, a cell jump is awaited and done based on `ScriptOptions#HuntDelay`. |
| `HuntForItem(string name, string item, int quantity, bool tempItem = false, bool rejectElse = true)` | *void* | Hunts the specified monster(s) until the specified quantity of an item is obtained. |
| `HuntForItem(string[] names, string item, int quantity, bool tempItem = false, bool rejectElse = true)` | *void* | Overload for `HuntForItem` where an array of names can be passed. This is equivalent to separating monster names with a `'\|'`. |
| `HuntForItem(List<string> names, string item, int quantity, bool tempItem = false, bool rejectElse = true)` | *void* | Another overload for `HuntForItem` where a list of monster names can be passed. |
| `HuntForItems(string name, string[] items, int[] quantities, bool tempItems = false, bool rejectElse = true)` | *void* | Hunts the specified monster(s) until the specified quantities of items are obtained. **The same overloads that exist for `HuntForItem` exist for this method.** |
| `AttackPlayer(string name)` | *void* | Attacks the specified player. |
| `void KillPlayer(string name)` | *void* | Attacks the specified player until they are dead (if `SafeTimings` are enabled). This should only be used in PVP. |
| `void UseSkill(int index)` | *void* | Uses the skill at the given index (1-4). |
| `CanUseSkill(int index)` | *bool* | Checks if the given skill has cooled down. |
| `Jump(string cell, string pad, bool clientOnly = false)` | *bool* | Jumps the player to the specified cell and pad. If `clientOnly = true` this will not send a packet to the server.   |
| `Join(string map, string cell = "Enter", string pad = "Spawn", bool ignoreCheck = false)` | *void* | Joins the map and jumps to the specified cell and pad (you don't need specify the cell and pad as they have default values). This method will keep attempting to join the specified map with a 2.5 second delay until it succeeds. If `ignoreCheck = true` the bot will not check if the player is in the room already. |
| `JoinIgnore(string map)` | *void* | Joins the specified map, ignoring whether or not you are in that map. |
| `JoinGlitched(string map, string cell = "Spawn", string pad = "Enter")` | *void* | **Patched**, will work like `Join(string map, string cell = "Spawn", string pad = "Enter")`. |
| `Goto(string name)` | *void* | Goes to the specified player. |
| `EquipItem(string item)` | *void* | Equips the item with the given name. |
| `EquipItem(int id)` | *void* | Equips the item with the specified id. |
| `OpenBank()` | *void* | Opens the player's bank. |
| `Rest(bool full = false, int timeout = -1)` | *void* | Rests the player. If `full` is true, the bot will wait until the player's health and mana are full or until the specified `timeout` runs out. |
| `IsBoostActive(RBot.Item.BoostTypes boost)` | *bool* | Checks if the specified boost is active. |
| `UseBoost(int id)` | *void* | Uses the boost with the given id. |
| `GetFactionRank(string name)` | *int* | Gets the players rank of the given faction. |

### Examples
The main use of `ScriptInterface#Player` is movement and combat. This is especially useful in fulfilling quest requirements. <a href="13 Examples">Examples can be found at the end of the documentation</a>.

---------
<center>
<a href="/Rbot-Scripts/6 Shops" title="6. Shops">◄ Previous</a> — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — <a href="/Rbot-Scripts/8 Quests" title="8. Quests">Next ►</a></center>