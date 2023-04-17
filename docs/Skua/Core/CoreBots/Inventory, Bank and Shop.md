# Inventory, Bank and Shop

As with any [property](#properties) or [method](#methods) from the `CoreBots.cs` file, you can call upon them by starting with `Core.` (*Core Dot*)

- [Inventory, Bank and Shop](#inventory-bank-and-shop)
  - [Methods](#methods)

## Methods

| Method Definition                                                                                        |      Return Type       | Description                                                                                                                                          |
| -------------------------------------------------------------------------------------------------------- | :--------------------: | ---------------------------------------------------------------------------------------------------------------------------------------------------- |
| `CheckInventory(string item, int quant = 1, bool toInv = true)`                                          |         *bool*         | Check the Bank, Inventory and Temp Inventory for the item by name                                                                                            |
| `CheckInventory(int itemID, int quant = 1, bool toInv = true)`                                           |         *bool*         | Check the Bank, Inventory and Temp Inventory for the item by ID                                                                                              |
| `CheckInventory(string[] itemNames, int quant = 1, bool any = false, bool toInv = true)`                 |         *bool*         | Check the Bank, Inventory and Temp Inventory for the items by name                                                                                       |
| `CheckInventory(int[] itemIDs, int quant = 1, bool any = false, bool toInv = true)`                      |         *bool*         | Check the Bank, Inventory and Temp Inventory for the items by ID                                                                                        |
| `CheckSpaces(ref int counter, params string[] items)`                                                    |         *void*         | Checks an array of string item names to see if you have enough spaces to run through something, and will warn the player and stop the bot if need be |
| `Unbank(params string[] items)`                                                                          |         *void*         | Move items from bank to inventory                                                                                                                    |
| `Unbank(params int[] items)`                                                                          |         *void*         | Move items from bank to inventory                                                                                                                    |
| `ToBank(params string[] items)`                                                                          |         *void*         | Move items from inventory to bank                                                                                                                    |
| `ToBank(params int[] items)`                                                                          |         *void*         | Move items from inventory to bank                                                                                                                    |
| `BuyItem(string map, int shopID, string itemName, int quant = 1, int shopQuant = 1, int shopItemID = 0)` |         *void*         | Buys a item till you have the desired quantity, will also inform the user if something is missing which could cause the buy to fail                                                                                                       |
| `BuyItem(string map, int shopID, int itemID, int quant = 1, int shopQuant = 1, int shopItemID = 0)`      |         *void*         | Buys a item till you have the desired quantity, will also inform the user if something is missing which could cause the buy to fail                                                                                                        |
| `SellItem(string itemName, int quant = 0, bool all = false)`                                             |         *void*         | Sells a item till you have the desired quantity                                                                                                      |
| `GetShopItems(string map, int shopID)`                                                                   | *List&lt;ShopItem&gt;* | Loads the shop and returns the shop item Data                                                                                                        |
| `parseShopItem(List<ShopItem> shopItem, int shopID, string itemNameID, int shopItemID = 0)`              |       *ShopItem*       | Parses a singular ShopItem from a list, based on either the item Name/ID or ShopItemID                                                               |
| `TrashCan(params string[] items)`              |       *void*       | Removes the specified items from players inventory (Banks AC items)                                                               |

---------
<center>
    <a href="Start and Stop" title="Start/Stop">◄ Previous</a> 
    — <a href="index" title="Back to Index">Index</a> — 
    <a href="Drops" title="Drops">Next ►</a>
</center>