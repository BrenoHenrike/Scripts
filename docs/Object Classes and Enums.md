# Object Classes & Enums

While scripting you might stumble uppon different classes and enums that are required for some methods, in this page they all will be listed.

- [Object Classes & Enums](#object-classes--enums)
  - [Object Classes](#object-classes)
    - [PlayerInfo](#playerinfo)
    - [Monster](#monster)
    - [Quest](#quest)
    - [ItemBase](#itembase)
    - [InventoryItem : ItemBase](#inventoryitem--itembase)
    - [ShopItem : ItemBase](#shopitem--itembase)
    - [MergeItem : ShopItem](#mergeitem--shopitem)
    - [SimpleReward : ItemBase](#simplereward--itembase)
    - [Server](#server)
  - [Enums](#enums)
    - [HuntPriority](#huntpriority)
    - [BoostType](#boosttype)
    - [ItemCategory](#itemcategory)

## Object Classes

You might have noted that some methods can either accept or return a class object or even a list of them, for example `ScriptInterface#Map.GetPlayer("name")` will return a `PlayerInfo` object. With those objects you can read, set and use all the available information inside your scripts. The object classes you will find in RBot are listed bellow with their respective namespace and inheritances.

### PlayerInfo

> From `RBot.Players`.

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

You can use the method `PlayerInfo#ToString()` to return a string formatted like `{EntID}: {Name}` with each respective variable filled with the player info.

### Monster

> From `RBot.Monsters`.

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

You can use the method `Monster#ToString()` to return a string formatted like `{Name} [{ID}] [{MapID}, {Cell}]` with each respective variable filled with the monster info.

### Quest

> From `RBot.Quests`.

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
| `AcceptRequirements` | *List\<RBot.Items.ItemBase>* | The items required in the player's inventory to accept the quest. |
| `Requirements` | *List\<RBot.Items.ItemBase>* | The required items/temp items to turn in the quest. |
| `Rewards` | *List\<RBot.Items.ItemBase>* | The items given as a reward for completing the quest. |
| `SimpleRewards` | *List\<RBot.Items.SimpleReward>* | Item drop rates are mapped to their IDs in this list. |

You can use the method `Quest#ToString()` to return a string formatted like `{Name} [{ID}]` with each respective variable filled with the quest info. These properties can be used as a trustful source of information about the quest, as they are acquired on the run preventing typos that can be found in the wiki and possible future name changes.

### ItemBase

> From `RBot.Items`.

| Property | Type | Description |
|---|:---:|---|
| `ID` | *int* | The ID of the item. |
| `Name` | *string* | The name of the item. |
| `Description` | *string* | The description of the item. |
| `Quantity` | *int* | The quantity of the item in this stack. |
| `MaxStack` | *int* | The maximum stack size of this item. |
| `Upgrade` | *bool* | Wether it is a member/legend only item. |
| `Coins` | *bool* | Wether it is an AC item. |
| `Category` | *ItemCategory* | The category of the item. (Sword, Armor, etc.) |
| `Temp` | *bool* | Whether it is a temporary item. |

The item base class have commom properties through some other item objects that inherit from it.

### InventoryItem : ItemBase

> From `RBot.Items`.

| Property | Type | Description |
|---|:---:|---|
| `CharItemID` | *int* | The character (instance) ID of this item. |
| `Equipped` | *bool* | Whether it is equipped by the player. |
| `Meta` | *string* | The meta value of the item. This is where the boosts (XP, REP, etc.) are shown. |
| `Level` | *int* | The level of the item. |
| `EnhancementLevel` | *int* | The enhancement level of the item. |

### ShopItem : ItemBase

> From `RBot.Items`.

| Property | Type | Description |
|---|:---:|---|
| `ShopItemID` | *int* | The shop specific item ID of the item. |
| `Cost` | *int* | The cost of the item. |
| `Level` | *int* | The level of the item. |

### MergeItem : ShopItem

> From `RBot.Items`.

| Property | Type | Description |
|---|:---:|---|
| `Requirements` | *List\<RBot.Items.ItemBase>* | A list of all there required items to merge the item. |

### SimpleReward : ItemBase

> From `RBot.Quests`.

| Property | Type | Description |
|---|:---:|---|
| `Rate` | *double* | The drop rate of the item. |
| `Type` | *int* | An integer that defines the type of the reward (0 - Guaranteed; 1 - May receive; 2 - Choose). |

### Server

> From `RBot.Servers`.

| Property | Type | Description |
|---|---|---|
| `Name` | *string* | The name of the game server. |
| `IP` | *string* | The IP address of the game server. |
| `ChatLevel` | *int* | The chat level of the server (canned = 0, free = 2). |
| `Port` | *int* | The port the server listens on. |
| `Online` | *bool* | Whether the server is online. |
| `Lang` | *string* | The language of the server. |
| `PlayerCount` | *int* | The number of players currently on the server. |
| `Upgrade` | *bool* | Whether the server is member only. |

You can use the method `Server#ToString()` to return a string formatted like `{Name} - {PlayerCount}` with each respective variable filled with the quest info.

## Enums

Enumerated types are a way to define named constant integer values used mainly as parameters. The usage is `EnumName.EnumMember`, but note that you may need to include the namespace by either `using Namespace` keyword at the beginning of the script or writing it all out like `Namespace.EnumName.EnumMember`.

### HuntPriority

> From `RBot`.

- None,  *No priority*
- LowHP, *Prioritises monsters with the lowest HP.*
- HighHP, *Prioritises monsters with the highest HP.*
- Close  *Prioritises monsters which are in the same cell.*

Used to define the hunting priority of the hunting methods in `ScriptInterface#Options.HuntPriority`.

### BoostType

> From `RBot.Items`.

- Gold,
- Class,
- Reputation,
- Experience

Used in the method `ScriptInterface#Player.IsBoostActive(BoostType boost)`.

### ItemCategory

> From `RBot.Items`.

- Sword,
- Axe,
- Dagger,
- Gun,
- HandGun,
- Rifle,
- Bow,
- Mace,
- Gauntlet,
- Polearm,
- Staff,
- Wand,
- Whip,
- Class,
- Armor,
- Helm,
- Cape,
- Pet,
- Amulet,
- Necklace,
- Note,
- Resource,
- Item,
- QuestItem,
- ServerUse,
- House,
- WallItem,
- FloorItem,
- Enhancement,
- Unknown

May be useful if 2 items in the inventory share the same name but not the same category.

---------
<center><a href="/Rbot-Scripts/Script Options" title="Script Options">◄ Previous</a> — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — <a href="/Rbot-Scripts/Plugins" title="Plugins">Next ►</a></center>