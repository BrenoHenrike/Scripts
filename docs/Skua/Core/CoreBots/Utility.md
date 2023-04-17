# Utility

As with any [property](#properties) or [method](#methods) from the `CoreBots.cs` file, you can call upon them by starting with `Core.` (*Core Dot*)

- [Utility](#utility)
  - [Properties](#properties)
  - [Methods](#methods)

## Properties

| Property          |         Type         | Description                                                                                                                                 |
| ----------------- | :------------------: | ------------------------------------------------------------------------------------------------------------------------------------------- |
| `IsMember`        |        *bool*        | Whether the player is Member, shorthandle for `Bot.Player.IsMember`.                                                                        |
| `DL_CallerFilter` |      *string?*       | Used in combination with the DebugLogger<br>If set, DebugLogger will only output the DebugLoggers that have the same Caller as this filter. |
| `DL_MarkerFilter` |      *string?*       | Used in combination with the DebugLogger<br>If set, DebugLogger will only output the DebugLoggers that have the same Marker as this filter. |
| `SkipOptions`     | *Option&lt;bool&gt;* | A Option to be used in scripts that want to do Script Options but have the option to skip the setup window                                  |

## Methods

| Method Definition                                                                                                                          | Return Type | Description                                                                                                                           |
| ------------------------------------------------------------------------------------------------------------------------------------------ | :---------: | ------------------------------------------------------------------------------------------------------------------------------------- |
| `Logger(string message = "", [CallerMemberName] string caller = "", bool messageBox = false, bool stopBot = false)`                        |   *void*    | Logs a line of text to the script log with time, method from where it's called and a message                                          |
| `FarmingLogger(string item, int quant, [CallerMemberName] string caller = "")`                                                             |   *void*    | Will output a specific logger<br>Example: `[00:00:00] (MethodName) Farming ITEM (69/420)`                                             |
| `DebugLogger(object _this, string marker = "Checkpoint", [CallerMemberName] string? caller = null, [CallerLineNumber] int lineNumber = 0)` |   *void*    | Will tell you the exact line number that it's on, allowing you to use it as check points to see where something is broken.            |
| `DL_Enable()`                                                                                                                              |   *void*    | Requiered to make Debug Logger work, if absent, Debug Logger will be disabled.                                                        |
| `Message(string message, string caption)`                                                                                                  |   *void*    | Creates a Message Box with the desired text and caption                                                                               |
| `SendPackets(string packet, int times = 1, bool toClient = false)`                                                                         |   *void*    | Send a packet to the server the desired amount of times                                                                               |
| `Rest()`                                                                                                                                   |   *void*    | Rest the player until full if ShouldRest = true                                                                                       |
| `Relogin()`                                                                                                                                |   *void*    | Logs the player out and then in again to the same server. Disables Options.AutoRelogin temporarily                                    |
| `EquipClass(ClassType classToUse)`                                                                                                         |   *void*    | Equips either the FarmClass or SoloClass from the CanChange section at the top of CoreBots                                            |
| `Equip(params string[] gear)`                                                                                                              |   *void*    | Equips one or multiple pieces of gear.                                                                                                |
| `ChangeAlignment(Alignment Side)`                                                                                                          |   *void*    | Switches the player's Alignment to the input Alignment type                                                                           |
| `HasAchievement(int ID, string ia = "ia0")`                                                                                                |   *void*    | Checks if a player has a certain achievement                                                                                          |
| `SetAchievement(int ID, string ia = "ia0")`                                                                                                |   *void*    | Sets an achievement for a player *(doens't work in many cases, only use it if you're certain)*                                        |
| `SavedState(bool on = true)`                                                                                                               |   *void*    | Currently Disabled<br>~~Will do a random message every 3~5 minutes so that the game saves some data that isn't saved automatically.~~ |
| `RunCore()`                                                                                                                                |   *void*    | This method is placed in Core files, so that the user gets a MessageBox explaining them that Core files are not meant to be run.      |

---------
<center>
    <a href="Kill" title="Kill">◄ Previous</a> 
    — <a href="index" title="Back to Index">Index</a> — 
    <a href="Map" title="Map">Next ►</a>
</center>