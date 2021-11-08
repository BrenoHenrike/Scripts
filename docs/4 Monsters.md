Monsters
======
Information about monsters in the current map and cell can be obtained through `ScriptInterface#Monsters`. This is useful for testing if monsters exist or exist with specific conditions about their health or other properties.

#### Properties
The class has the following properties:

| Property | Type | Description |
|---|:---:|---|
| `CurrentMonsters` | *List\<RBot.Monsters.Monster>* | A list of monsters in the current cell. This includes monsters that are dead. |
| `MapMonsters` | *List\<Monster>* | A list of monsters in the current map. |
| `HuntCellBlacklist` | *List\<string>* | A list of cells that are ignored when hunting for monsters. This is typically unneeded. |

#### Methods
The class has the following methods (return values will be according to the current Map):

| Method Definition | Return Type | Description |
|---|---|---|
| `Exists(string name)` | *bool* | Checks if a monster with the specified name exists (and is alive) in the current cell. |
| `GetCellMonsters()` | *Dictionary\<string, List\<RBot.Monsters.Monster>>* | Gets a dictionary mapping cell names to the monsters in that cell. |
| `GetMonstersByCell(string cell)` | *List\<Monster>* | Gets a list of monsters in the given cell. |
| `GetMonsterCells(string name)` | *List\<string>* | Gets a list of cells that contain a monster with the given name. |
| `GetLivingMonsterCells(string name)` | *List\<string>* | Gets a list of cells that contain a living monster with the given name. |
| `TryGetMonster(string name, out RBot.Monsters.Monster monster)` | *bool; out RBot.Monsters.Monster* | Tries to get the monster with the given name, if true will assign it to the monster variable. |

### The Monster Class
Many of the above properties return lists containing instances of `Monster`. This class has the following properties:

| Property | Type | Description |
|---|:---:|---|
| `Name` | *string* | The name of the monster. |
| `ID` | *int* | The unique id of the monster. |
| `Race` | *string* | The race of the monster. Can be useful when you want to equip respective damage boost item. |
| `Cell` | *string* | The name of the cell that contains this monster. |
| `MapID` | *int* | The map id of the monster. This can be used to target specific monsters in a room when multiple monsters with the same name exist. |
| `HP` | *int* | The health of the monster. |
| `State` | *int* | The state of the monster. When `State > 0`, the monster is alive, otherwise it is dead. |
| `FileName` | *string* | The SWF file name of the monster. |
| `Alive` | *bool* | Returns whether the monster is Alive by checking its `HP`. |

You can use the method `Monster.ToString()` to return a string formatted like `{Name} [{ID}] [{MapID}, {Cell}]` with each respective property filled with the monster info.

Later, [when attacking monsters is explained](https://brenohenrike.github.io/Rbot-Scripts/), it will be made clear how to query monster lists and target monsters using these properties.

|[◄ Previous](https://brenohenrike.github.io/Rbot-Scripts/3%20Timings%20and%20Handlers "3. Timings & Handlers") ——— [Next ►](https://brenohenrike.github.io/Rbot-Scripts/5%20Skills "5. Skills") |
| :---: |