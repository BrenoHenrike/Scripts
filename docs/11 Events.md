## Events
The bot can listen for certain events and your script can attach its own listeners to handle these events. This is done through the `ScriptInterface#Events` object.

The following events can be listened for:

| Event Definition | Is triggered when |
|---|---|
| `PlayerDeath()` | Player dies. |
| `MonsterKilled()` | Monster that the player is attacking is killed. |
| `QuestAccepted(int id)` | Quest is accepted by the bot. |
| `QuestTurnedIn(int id)` | Quest is turned in by the bot. |
| `MapChanged(string map)` | Map changes. |
| `CellChanged(string map, string cell, string pad)` | Cell changes. |
| `ReloginTriggered(bool kicked)` | Relogin is triggered. |
| `ExtensionPacketReceived(dynamic packet)` | Extension packet is received from the server. |
| `PlayerAFK()` | Player goes AFK. |
| `TryBuyItem()` | Player attempts to buy an item from a shop. |

Event handlers are cleared when a script stops or starts. To manually clear event handlers use `ScriptEvents#ClearHandlers()`, although this is typically not necessary. **(In current version [3.6] they aren't cleared as intended)**

### Listening for Events

To attach your own listener to an event, you can use the typical C# syntax for adding event handlers. All event handlers take a first argument which is the current instance of the `ScriptInterface` and some take a second argument (shown in the list above). For example:

```csharp
bot.Events.PlayerDeath += b => {
    b.Log("Player died.");
};
```

will log `"Player died"` every time the player dies. Another example:

```csharp
bot.Events.MapChanged += (b, map) => {
    b.Log("Player is in map " + map + ".");
};
```

will log `"Player is in map {map name}."` whenever the map changes.

You can use them with local functions too, this one is used to restart the script by triggering an auto-relogin:

```csharp
bot.Options.AutoRelogin = true;
void AFKHandler(ScriptInterface b)
{
  b.Log("Player AFK, triggering logout");
  // This will unsubscribe the event
  b.Events.PlayerAFK -= AFKHandler;
  b.Player.Logout();
}
bot.Events.PlayerAFK += AFKHandler;
``` 

which will log `"Player AFK, triggering logout"`, unsubscribe the event handler (this is needed because when the script starts again, it will create another handler which can cause memory problems) and then logging out to trigger the auto-relogin feature.

---------
<center><a href="/Rbot-Scripts/10 Inventory and Bank" title="10. Inventory & Bank">◄ Previous</a> — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — <a href="/Rbot-Scripts/12 Packets" title="12. Packets">Next ►</a></center>