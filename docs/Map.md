# Map

Information about the currently loaded map (room) can be obtained through the `ScriptInterface#Map` object.

- [Map](#map)
  - [Properties](#properties)
  - [Methods](#methods)

## Properties

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
| `LastMap` | *string* | The name of the last map joined in the session. |
| `MapFilePath` | *string* | The path of the last map file. |
| `MapFileName` | *string* | The name of the map file, from the MapFilePath. |
| `MapFileTown` | *string* | The town of the map (usually the region of the map, the first part of MapFilePath) |

## Methods

| Method Definition | Return Type | Description |
|---|:---:|---|
| `Reload()` | *void* | Reloads the current map. |
| `GetMapItem(int id)` | *void* | Sends a getMapItem packet with the specified id. This is useful for quests that require some sort of map interaction. |
| `PlayerExists(string name)` | *bool* | Checks if the player with the given name exists in the map. |
| `GetPlayer(string username)` | *RBot.Players.PlayerInfo* | Gets information about the player with the given name. The player must be loaded in the current map. |
| `TryGetPlayer(string username)` | *bool; out RBot.Players.PlayerInfo* | Whether the player exists and is loaded in the current map. It `out`s the `PlayerInfo` of the given name. |
| `Reload()` | *void* | Reloads the current map. |

---------
<center><a href="/Rbot-Scripts/Quests" title="Quests">◄ Previous</a> — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — <a href="/Rbot-Scripts/Inventory and Bank" title="Inventory & Bank">Next ►</a></center>