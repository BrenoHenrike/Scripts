# Kill

- [Kill](#kill)
  - [Methods](#methods)

## Methods

| Method Definition                                                                                                                                                    | Return Type | Description                                                             |
| -------------------------------------------------------------------------------------------------------------------------------------------------------------------- | :---------: | ----------------------------------------------------------------------- |
| `KillMonster(string map, string cell, string pad, string monster, string? item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)` |   *void*    | Joins a map, jump & set the spawn point and kills the specified monster |
| `KillMonster(string map, string cell, string pad, int monsterID, string? item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)`  |   *void*    | Kills a monster using it's ID                                           |
| `HuntMonster(string map, string monster, string? item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)`                          |   *void*    | Joins a map and hunt for the monster                                    |
| `HuntMonsterMapID(string map, int monsterMapID, string? item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)`                   |   *void*    | Kills a monster using it's MapID                                        |
| `KillEscherion(string? item = null, int quant = 1, bool isTemp = false, bool log = true, bool publicRoom = false)`                                                   |   *void*    | Kill Escherion for the desired item                                     |
| `KillVath(string? item = null, int quant = 1, bool isTemp = false, bool log = true, bool publicRoom = false)`                                                        |   *void*    | Kill Vath for the desired item                                          |
| `KillDoomKitten(string item, int quant = 1, bool isTemp = false, bool log = true, bool publicRoom = false)`                                                          |   *void*    | Kill DoomKitten for the desired item                                    |
| `KillXiang(string item, int quant = 1, bool ultra = false, bool isTemp = false, bool log = true, bool publicRoom = false)`                                           |   *void*    | Kill Xiang for the desired item                                         |

---------
<center>
    <a href="Quest" title="Quest">◄ Previous</a> 
    — <a href="Documentation" title="Back to Index">Index</a> — 
    <a href="Utility" title="Utility">Next ►</a>
</center>