# Shops

It is very simple to buy and sell items using `ScriptInterface#Shops`.

## Properties

| Property | Return Type | Description |
|---|:---:|---|
| `ShopItems` | *List\<RBot.Shops.ShopItem>* | Gets a list of items in the last (or currently) loaded shop. |
| `IsShopLoaded` | *bool* | Checks whether a shop is currently loaded. |
| `ShopID` | *int* | Gets the last (or currently) loaded shop id. |
| `ShopName` | *string* | Gets the last (or currently) loaded shop's name. |
| `MergeItems` | *List\<RBot.Shops.MergeItem>* | Gets a list of items in the last (or currently) loaded merge shop. |

## Methods

| Method Definition | Description |
|---|---|
| `Load(int id)` | Loads the shop with the specified id. |
| `BuyItem(int shopId, string name)` | Buys the item with the specified name from the shop with id `shopId`. This will load the shop first. |
| `BuyItem(int shopId, int itemId)` | Buys the item with the specified id from the shop with id `shopId`. This will load the shop first. |
| `BuyItem(string name)` | Buys the item with the specified name from the last (or currently) loaded shop. |
| `BuyItem(int itemId)` | Buys the item with the specified id from the last (or currently) loaded shop. |
| `SellItem(string name)` | Sells the item with the specified name from the player's inventory. |
| `LoadHairShop(int id)` | Loads the hair shop with the specified id. |
| `LoadArmourCustomizer()` | Loads the armour customizer. |

These methods are very easy to use and should be self-explanatory.

---------
<center><a href="/Rbot-Scripts/Skills" title="Skills">◄ Previous</a> — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — <a href="/Rbot-Scripts/Player" title="Player">Next ►</a></center>