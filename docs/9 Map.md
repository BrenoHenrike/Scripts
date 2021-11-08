## Map
Information about the currently loaded map (room) can be obtained through the `ScriptInterface#Map` object.

### Properties

| Property | Type | Description |
|---|:---:|---|
| `Name` | *string* | The name of the map. This does not include the room number. |
| `RoomID` | *int* | The map's area id. This is required in some packets. |
| `PlayerCount` | *int* | The number of players in the currently loaded map. |
| `PlayerNames` | *List\<string>* | The list of player names in the currently loaded map. |
| `Players` | *List\<PlayerInfo>* | The list of players in the current map. |
| `CellPlayers` | *List\<PlayerInfo>* | The list of players in the current cell. |
| `Loaded` | *bool* | Indicates whether a map is currently loaded. |
| `Cells` | *List\<string>* | An list of all the cell names in the map. |

### Methods

| Method Definition | Return Type | Description |
|---|:---:|---|
| `Reload()` | *void* | Reloads the current map. |
| `GetMapItem(int id)` | *void* | Sends a getMapItem packet with the specified id. This is useful for quests that require some sort of map interaction. |
| `PlayerExists(string name)` | *bool* | Checks if the player with the given name exists in the map. |
| `GetPlayer(string username)` | *RBot.Players.PlayerInfo* | Gets information about the player with the given name. The player must be loaded in the current map. |
| `TryGetPlayer(string username)` | *bool; out RBot.Players.PlayerInfo* | Whether the player exists and is loaded in the current map. It `out`s the `PlayerInfo` of the given name. |
| `Reload()` | *void* | Reloads the current map. |

### The 'PlayerInfo' class
Some properties give single or lists of `PlayerInfo` objects. These objects have the following properties:

| Property | Type | Description |
|---|:---:|---|
| `Name` | *string* | The player's name (username). This is always lower case. |
| `HP` | *int* | The player's current HP. |
| `MaxHP` | *int* | The player's maximum HP. |
| `MP` | *int* | The player's current mana. |
| `Stats` | *RBot.Players.PlayerStats* | The player's stats. Only your player will have any stats loaded. This is also incomplete. |
| `AFK` | *bool* | Determines whether the player is AFK or not. |
| `EntID` | *int* | The player's entity ID. |
| `Level` | *int* | The player's level. |
| `Cell` | *string* | The player's cell. |
| `Pad` | *string* | The player's pad. |
| `X` | *float* | The player's X coordinate. |
| `Y` | *float* | The player's Y coordinate; |
| `State` | *int* | The state of the player (0 = dead, 1 = idle, 2 = combat). |

You can use the method `PlayerInfo.ToString()` to return a string formatted like `{EntID}: {Name}` with each respective variable filled with the player info.

---------
<center>
<a href="/Rbot-Scripts/8 Quests" title="8. Quests">◄ Previous</a> — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — <a href="/Rbot-Scripts/10 Inventory and Bank" title="10. Inventory & Bank">Next ►</a></center>