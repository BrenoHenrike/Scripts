# Start/Stop

As with any [property](#properties) or [method](#methods) from the `CoreBots.cs` file, you can call upon them by starting with `Core.` (*Core Dot*)

- [Start/Stop](#start/stop)
  - [Properties](#properties)
  - [Methods](#methods)

## Properties

| Property  |   Type   | Description                                        |
| --------- | :------: | -------------------------------------------------- |
| `BankingBlackList` | *List&lt;string&gt;* | Items added to this list before SetOptions is called will be ignored by the 'Bank Misc AC items before starting the bot' system |
| `loadedBot` | *string* | Holds the full path of the loaded bot. Is set during SetOptions. Example: Legion/Various/Caladbolg |

## Methods

| Method Definition                                                 | Return Type | Description                                                                         |
| ----------------------------------------------------------------- | :---------: | ----------------------------------------------------------------------------------- |
| `SetOptions(bool changeTo = true, bool disableClassSwap = false)` |   *void*    | Set common bot options to desired value and starts all eelevant handlers and events |


---------
<center>
    ◄ Previous 
    — <a href="index" title="Back to Index">Index</a> — 
    <a href="Inventory, Bank and Shop" title="Inventory, Bank and Shop">Next ►</a>
</center>