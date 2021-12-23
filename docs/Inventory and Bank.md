# Inventory and Bank

The player's inventory and bank can be managed through `ScriptInterface#Inventory` and `ScriptInterface#Bank` respectively.

- [Inventory and Bank](#inventory-and-bank)
  - [Inventory](#inventory)
    - [Inventory Properties](#inventory-properties)
    - [Inventory Methods](#inventory-methods)
  - [Bank](#bank)
    - [Bank Properties](#bank-properties)
    - [Bank Methods](#bank-methods)
  - [Notes](#notes)

## Inventory

You can query and manage the player's inventory through `ScriptInterface#Inventory`.

### Inventory Properties

| Property | Type | Description |
|---|:---:|---|
| `Items` | *List\<RBot.Items.InventoryItem>* | A list of the items in the player's inventory. |
| `TempItems` | *List\<RBot.Items.ItemBase>* | A list of items in the player's temporary inventory. |
| `CurrentClass` | *RBot.Items.InventoryItem* | The player's current equipped class. |
| `Slots` | *int* | The total number of inventory slots the player has. |
| `UsedSlots` | *int* | The number of inventory slots that are currently in use. |
| `FreeSlots` | *int* | The number of free inventory slots the player has. |

### Inventory Methods

| Method Definition | Return Type | Description |
|---|:---:|---|
| `Contains(string item, int quantity = 1)` | *bool* | Checks whether the player's inventory contains the specified item in the specified quantity. |
| `ContainsTempItem(string item, int quantity = 1)` | *bool* | Checks whether the player's inventory contains the specified temp item in the specified quantity. |
| `ToBank(string item)` | *void* | Moves the specified item from the player's inventory to their bank. |
| `ToBank(RBot.Items.InventoryItem item)` | *void* | Moves the specified item from the player's inventory to their bank. |
| `GetQuantity(string item)` | *void* | Gets the specified item's quantity in the player's inventory. |
| `GetItemByName(string name)` | *RBot.Items.InventoryItem* | Gets the `InventoryItem` instance for the item in the player's inventory with the given name. |
| `GetItemById(int id)` | *RBot.Items.InventoryItem* | Gets the `InventoryItem` instance for the item in the player's inventory with the given id. |
| `TryGetItem(string name, out RBot.Items.InventoryItem item)` | *bool* | Attempts to get the item by the given name and sets the `out` parameter to its value. |
| `TryGetItem(int id, out RBot.Items.InventoryItem item)` | *bool* | Attempts to get the item by the given id and sets the `out` parameter to its value. |
| `GetTempQuantity(string name)` | *int* | Gets the specified temp item's quantity in the player's temporary inventory. |
| `GetTempItemByName(string name)` | *RBot.Items.ItemBase* | Gets the `ItemBase` instance for the temp item in the player's temporary inventory with the given name. |
| `TryGetTempItem(string name, out RBot.Items.ItemBase)` | *bool* | Attempts to get the temporary item by the given name and sets the `out` parameter to its value. |
| `TryGetTempItem(int id, out RBot.Items.ItemBase)` | *bool* | Attempts to get the temporary item by the given id and sets the `out` parameter to its value. |
| `IsEqquiped(string name)` | *bool* | Checks if the given item is equipped. |
| `IsMaxStack(string name)` | *bool* | Checks if the given item is at its maximum stack. |
| `BankAllCoinItems()` | *void* | Transfers all AC items to the bank from the player's inventory. This is useful at the start of a script to free up inventory space (Be aware that it will bank *EVERY* AC tagged item). |

## Bank

The player's bank can also be managed through `ScriptInterface#Bank`. However, before using this object, you should load the player's bank through `ScriptPlayer#LoadBank`. The client will otherwise think the bank is empty. The bank should typically be loaded at the start of the script.

### Bank Properties

| Property | Type | Description |
|---|:---:|---|
| `BankItems` | *List\<RBot.Items.InventoryItem>* | A list of all of the items in the player's bank. |
| `Slots` | *int* | The total number of bank slots the player has. |
| `UsedSlots` | *int* | The number of bank slots that are currently in use. |
| `FreeSlots` | *int* | The number of free bank slots the player has. Calculates `Slots - UsedSlots` |

### Bank Methods

| Method Definition | Return Type | Description |
|---|:---:|---|
| `Contains(string item, int quantity = 1)` | *bool* | Checks if the bank contains the given item in the given quantity. |
| `GetItemByName(string name)` | *RBot.Items.InventoryItem* | Gets the bank item with the specified name. |
| `TryGetItem(string name, out RBot.Items.InventoryItem item)` | *bool* | Attempts to get the item by the given name and sets the `out` parameter to its value. |
| `Swap(string invItem, string bankItem)` | *void* | Swaps the item with name `invItem` in the player's inventory with the item with name `bankItem` in the player's bank. |
| `ToInventory(string name)` | *void* | Moves the given item from the player's bank to their inventory. |

## Notes

Inventory and bank management is typically done at the start of a script where options and skills are set up. It can also be done as quests are being completed or as drops are being picked up if inventory space is very limited. **Remember to load the bank before attempting to transfer items from/to it.**

---------
<center>
<a href="/Rbot-Scripts/Map" title="Map">◄ Previous</a> — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — <a href="/Rbot-Scripts/Events" title="Events">Next ►</a></center>