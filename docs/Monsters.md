# Monsters

Information about monsters in the current map and cell can be obtained through `ScriptInterface#Monsters`. This is useful for testing if monsters exist or exist with specific conditions about their health or other properties.

## Properties

The class has the following properties:

| Property | Type | Description |
|---|:---:|---|
| `CurrentMonsters` | *List\<RBot.Monsters.Monster>* | A list of monsters in the current cell. This includes monsters that are dead. |
| `MapMonsters` | *List\<Monster>* | A list of monsters in the current map. |
| `HuntCellBlacklist` | *List\<string>* | A list of cells that are ignored when hunting for monsters. This is typically unneeded. |

## Methods

The class has the following methods (return values will be according to the current Map):

| Method Definition | Return Type | Description |
|---|---|---|
| `Exists(string name)` | *bool* | Checks if a monster with the specified name exists (and is alive) in the current cell. |
| `GetCellMonsters()` | *Dictionary\<string, List\<RBot.Monsters.Monster>>* | Gets a dictionary mapping cell names to the monsters in that cell. |
| `GetMonstersByCell(string cell)` | *List\<Monster>* | Gets a list of monsters in the given cell. |
| `GetMonsterCells(string name)` | *List\<string>* | Gets a list of cells that contain a monster with the given name. |
| `GetLivingMonsterCells(string name)` | *List\<string>* | Gets a list of cells that contain a living monster with the given name. |
| `TryGetMonster(string name, out RBot.Monsters.Monster monster)` | *bool; out RBot.Monsters.Monster* | Tries to get the monster with the given name, if true will assign it to the monster variable. |

Later, when attacking monsters is explained, it will be made clear how to query monster lists and target monsters using these properties.

---------
<center><a href="/Rbot-Scripts/Timings and Handlers" title="Timings & Handlers">◄ Previous</a> — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — <a href="/Rbot-Scripts/Skills" title="Skills">Next ►</a></center>
