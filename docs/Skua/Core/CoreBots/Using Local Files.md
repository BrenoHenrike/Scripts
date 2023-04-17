# Using Local Files

As with any [property](#properties) or [method](#methods) from the `CoreBots.cs` file, you can call upon them by starting with `Core.` (*Core Dot*)

- [Using Local Files](#using-local-files)
  - [Properties](#properties)
  - [Methods](#methods)

## Properties

| Property  |   Type   | Description                                        |
| --------- | :------: | -------------------------------------------------- |
| `AppPath` | *string* | Returns the file path the user has Skua stored in. |


## Methods

| Method Definition                           | Return Type | Description                                                    |
| ------------------------------------------- | :---------: | -------------------------------------------------------------- |
| `CBO_Active()`                              |   *bool*    | Returns if the user has setup their Options>CoreBots file yet. |
| `CBOString(string Name, out string output)` |   *bool*    | Tries to parse a Options>CoreBots string.                      |
| `CBOBool(string Name, out bool output)`     |   *bool*    | Tries to parse a Options>CoreBots bool.                        |
| `CBOInt(string Name, out int output)`       |   *bool*    | Tries to parse a Options>CoreBots integer.                     |

---------
<center>
    <a href="Map" title="Map">◄ Previous</a> 
    — <a href="index" title="Back to Index">Index</a> — 
    Next ►
</center>