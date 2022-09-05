# Map

As with any [property](#properties) or [method](#methods) from the `CoreBots.cs` file, you can call upon them by starting with `Core.` (*Core Dot*)

- [Map](#map)
  - [Methods](#methods)

## Methods

| Method Definition                                                                                                  | Return Type | Description                                                                                               |
| ------------------------------------------------------------------------------------------------------------------ | :---------: | --------------------------------------------------------------------------------------------------------- |
| `Jump(string cell = "Enter", string pad = "Spawn", bool ignoreCheck = false)`                                      |   *void*    | Jumps to the desired cell and set spawn point                                                             |
| `JumpWait()`                                                                                                       |   *void*    | Searches for a cell without monsters and jumps to it. If non is found it jumps twice in its current cell. |
| `Join(string map, string cell = "Enter", string pad = "Spawn", bool publicRoom = false, bool ignoreCheck = false)` |   *void*    | Joins a map and does bonus steps for said map if needed                                                   |
| `GetMapItem(int itemID, int quant = 1, string? map = null)`                                                        |   *void*    | Sends a getMapItem packet for the specified item                                                          |
| `PvPMove(int mtcid, string cell, int moveX = 828, int moveY = 276)`                                                |   *void*    | This method is used to move between PvP rooms                                                             |
| `inPublicRoom()`                                                                                                   |   *bool*    | Checks if the room you're in is a public room or not                                                      |

---------
<center>
    <a href="Utility" title="Utility">◄ Previous</a> 
    — <a href="index" title="Back to Index">Index</a> — 
    <a href="Using Local Files" title="Using Local Files">Next ►</a>
</center>