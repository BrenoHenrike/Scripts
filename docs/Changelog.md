# Changelog

- [Changelog](#changelog)
  - [3.6.2](#362)
    - [Removed](#removed)
    - [Changes and fixes](#changes-and-fixes)
    - [House Items](#house-items)
    - [Wait Override](#wait-override)
    - [Jump overlay](#jump-overlay)
    - [Jump window](#jump-window)
    - [Advanced Skills](#advanced-skills)
    - [Multiple Options](#multiple-options)
    - [VSCode Setup for C# scripting](#vscode-setup-for-c-scripting)

## 3.6.2

### Removed

All files related to converting gbots to scripts have been removed as the final product of said conversions are not realiable and easy to edit after converted.

### Changes and fixes

The changes that don't need an explanation are listed bellow.

- Shift + 3 opens the *"Run"* window, it was always there, just documenting it for who don't know yet;
- Logs now are also in the *"Scripts"* window and don't need the window to be opened;
- Events are cleared when the script stops, also added missing events from the clear method;
- Stopping a script will disable the options: LagKiller, AutoRelogin, AggroMonsters, AggroAllMonsters and SkipCutscenes;
- Possible fix for hunting not working for some people;
- Possible fix for *"Start/Stop Script"* button text not updating when stopping the script inside the script;
- ScriptManager.RestartScript() now does nothing. Scripts that may have it will no longer crash the application.

### House Items

House items can be grabbed in the Loaders and scripted, the methods added are:

> From `ScriptInterface#Inventory`

| Method | Return type | Description |
|---|:---:|---|
| `ContainsHouseItem(string item)` | *bool* | Checks if the House Inventory contains the specified item. |
| `HouseItemToBank(string item)` | *void* | Sends the house item to bank. |
| `GetHouseItemByName(string name)` | *RBot.Items.InventoryItem* | Gets a reference to the specified house item in the player's house inventory. |
| `GetHouseItemById(int id)` | *RBot.Items.InventoryItem* | Gets a reference to the specified house item in the player's house inventory. |
| `TryGetHouseItem(int id, out InventoryItem houseItem)` | *bool, out RBot.Items.InventoryItem* | Attempts to get the item by the given id and sets the out parameter to this value. |
| `TryGetHouseItem(string name, out InventoryItem houseItem)` | *bool, out RBot.Items.InventoryItem* | Attempts to get the item by the given name and sets the out parameter to this value. |
| `IsHouseItemEquipped(string name)` | *bool* | Checks if the given house item is equipped. |

Bank versions are not needed as all items go to the same bank inventory and use the same packet to send to inventory.

### Wait Override

You are getting ping spikes and that is breaking you script? Now you can override the timeouts used by SafeTimings setting the property `ScriptInterface#Wait.OverrideTimeout` to `true`. When setted to true the wait method will use the timeouts listed bellow:

> **Note:** The number you see multiplies *WAIT_SLEEP* (250ms), so a ActionTimeout of 10 will wait for 2500ms (2,5 seconds). Please don't set values bellow the default ones as this can cause script/game instability.

```cs
// This will override any action timeout made by the Player:
// "ForPlayerPosition(float, float, int)"
// "ForCombatExit(int)"
public int PlayerActionTimeout { get; set; } = 10;

// This will override any action timeout related to monsters:
// "ForMonsterSpawn(string, int)"
public int MonsterActionTimeout { get; set; } = 10;

// This will override any action timeout related to maps:
// "ForMapLoad(string, int)"
// "ForCellChange(string)"
public int MapActionTimeout { get; set; } = 20;

// This will override any action timeout related to drops:
// "ForPickup(string, int)"
// "ForDrop(string, int)"
public int DropActionTimeout { get; set; } = 10;

// This will override any action timeout related to items:
// "ForItemBuy(int)"
// "ForItemSell(int)"
// "ForItemEquip(string, int)"
// "ForItemEquip(int, int)"
// "ForBankToInventory(string, int)"
// "ForInventoryToBank(string, int)"
public int ItemActionTimeout { get; set; } = 14;

// This will override any action timeout related to quests:
// "ForQuestAccept(int, int)"
// "ForQuestComplete(int, int)"
public int QuestActionTimeout { get; set; } = 14;

// This will override any action timeout related to game actions:
// "ForActionCooldown(GameActions, int)"
// "ForActionCooldown(string, int)"
public int GameActionTimeout { get; set; } = 40;

```

### Jump overlay

Main form now has a *"Jump"* overlay just like the previous Jump window where you can get your current cell or jump to others, with it you can:

- **Ctrl + click _"Get Current"_** to copy to your clipboard a string formatted like `"{MapName}", "{Cell}", "{Pad}"`;
- **Shift + click _"Get Current"_** to copy to your clipboard a string formatted like `"{Cell}", "{Pad}"`.

### Jump window

*"Jump" window* now has predefined fast travels for tercess locations, Binky and Museum Crossroads (Awe Enhancements). You can also add custom fast travels that will be saved in the application options. The usage is simple:

In the text box at the bottom you can add where you wanna go, it must be comma (',') separated for it to be added. The accepted format will look something like this: **`Display name, MapToJoin, Cell, Pad`** where display name can be anything you want to describe the place you are joining.

Four (4) parameters are the maximum accepted but you can add them like:

- **`underworld`** will join underworld;
- **`Join Underworld, battleon`** will also join underworld but the display name will be *"Join Underworld"*);
- **`underworld, s1, Left`** will join underworld and jump to the Dage's room. Note that 3 inputs expect that inputs 2 and 3 are Cell and Pad respectively;
- **`Dage, underworld, s1, Left`** will join underworld and jump too the Dage's room. It's display name will be *"Dage"*.

After adding it you can click the *"Go"* button to join the selected fast travel in the combo box, this button can be used like:

- **Click** to run the selected travel command;
- **Ctrl + click** to run the selected travel command for a **private room**;
- **Alt + click** to remove the selected travel command.

> **Note:** Holding ctrl while clicking any of the predefined fast travels will also join a private room.

### Advanced Skills

Core Skill plugin is now implemented inside RBot and are saved to `RBot folder/Skills/AdvancedSkills.txt`, more info on how to use can be [seen here](http://brenohenrike.github.io/rbot-Scripts/Skills#advanced-skills). This will allow you and bot makers to provide skill sets.

### Multiple Options

It's now possible to create multiple lists of options for your scripts, allowing categorized options to be shown at script start. To set it up you will need to create the option fields [explained here](https://brenohenrike.github.io/Rbot-Scripts/Script%20Options#setting-up) and an aditional field (`public string[] MultiOptions`) that will list the fields you want to be used as options:

```cs
// Like in the default setup, we specify the file name.
public string OptionsStorage = "Multi Option Test";
// This field is optional.
public bool DontPreconfigure = false;

// We set a string array with the names of the options list.
// At runtime underscores will be replaced by spaces in the Option Window that pop up.
public string[] MultiOptions = { "First", "Second_List", "Third_List" };

// You need to match the names listed in the array above so they can be compiled
public List<IOption> First = new List<IOption>()
{
    new Option<string>("name", "Name", "Name of your character", "Rick Astley"),
    new Option<string>("link", "Link", "A not even suspicious link", "https://www.youtube.com/watch?v=dQw4w9WgXcQ")
};
public List<IOption> Second_List = new List<IOption>()
{
    new Option<int>("never", "Never", "Gonna give you up", 1337)
};
public List<IOption> Third_List = new List<IOption>()
{
    new Option<string>("never", "Never", "Never gonna let you down", "Rick Astley but from Third_List")
};

public List<IOption> Options = new List<IOption>()
{
    new Option<string>("normal", "Normal Option", "This is a normal option", "Normal")
};

```

When running a script with the above options, they will appear in the options window pop up like this:

<p align="center"><img src="https://imgur.com/QXsMTQl.png"></p>

To get the values from a multi option storage it's pretty much like a simple option, the only difference is that you need to specify from which list it will come from. This allow us to have multiple options with the same name but from different lists:

```cs
public void ScriptMain(ScriptInterface bot)
{

    bot.Options.SafeTimings = true;
    bot.Options.RestPackets = true;
    bot.Options.InfiniteRange = true;
    bot.Options.ExitCombatBeforeQuest = true;

    // Like getting from a normal option, we call the Get method with the type of the value we are trying to get.
    // The first parameter is the name of the list the option is, the second is the name of the option.
    bot.Log(bot.Config.Get<string>("First", "name"));
    
    // As said before this allow same option names aslong they are not in the same list.
    bot.Log(bot.Config.Get<int>("Second_List", "never").ToString());
    bot.Log(bot.Config.Get<string>("Third_List", "never"));

    // Normal options don't need a list name as they belong to the same place.
    bot.Log(bot.Config.Get<string>("normal"));
}
```

With all that after running the scrip for this example the values of the options will be logged:


<p align="center"><img src="https://imgur.com/dSu8yC5.png"></p>

The file for it will be included in the [3.6.2 release](https://github.com/BrenoHenrike/Rbot/releases)

### VSCode Setup for C# scripting

You edit scripts with the built in Editor but don't like the light theme of it? If you wanna be a hackerman and use IntelliSense/Omnisharp word completion, I'm going to walk you through how to setup VSCode to help you with it.

> **Note:** you might need the [Developer Pack for .NET Framework 4.8 AND 4.7.2](https://docs.microsoft.com/en-us/dotnet/framework/install/guide-for-developers) to be able to open the VSCode-Scripts in VSCode.

1. First, of course, you need to [download VSCode](https://code.visualstudio.com/download).
2. When opening for the first time it will show you somethings that might be useful, but for the time being we don't need them. To continue, click in the **"Extensions"** button *(Ctrl + Shift + X)*:

<p align="center"><img src="https://imgur.com/2cJVdur.png"></p>

3. In the Extensions menu you will need to download the following extensions, to download use the search bar, click the extension and then in *"Install"*:
   - C#;
   - VS Code .csproj;
   - vscode-icons (optional but very handy).
   - Git Pull Requests and Issues (optional, [needs Git](https://git-scm.com/downloads));
   - Git Lens - Git supercharged (optional);

4. With the extensions installed, press **Ctrl + , (comma)** to open the settings UI and click the button marked bellow (it is in the upper right corner):

<p align="center"><img src="https://imgur.com/Kz0joIL.png"></p>

  There you can paste the following settings inside the curly braces:

```json
"files.autoSave": "afterDelay",
"files.autoSaveDelay": 10000,

"csproj.itemType": {
    "*": "Compile",
    ".ts": "TypeScriptCompile"
},
"csproj.silentDeletion": true,
```

The first 2 lines will set autosave to each 10 seconds (optional). The csproj ones will 1. set included files to be compiled (needed for autocomplete) and 2. automatically delete any file you delete from the csproj.

5. Back in the [3.6.2 release](https://github.com/BrenoHenrike/RBot/releases), download the *VSCode-Scripts.zip*;
6. Place the folder anywhere you want, for ease of use you can extract it inside your RBot Scripts folder;
7. Back to VSCode, click File > Open Folder... > Select the folder were the **VSCode-Scripts.sln** is located;
8. When the folder opens, click the *VSCode-Scripts.csproj* and search for \<HintPath> (should be at line 35) and change the path to where **your RBot.exe** is located;
9. You can then click the *"Script.cs"* file, wait a bit so Omnisharp can load all references et voi'la, your script has auto complete, but wait!
10. There is 2 catches:

    - You can't have 2 classes with the same name, normally all scripts have the main class called "Script", so every cs file you create remember to rename the **Script Class** to anything you want;
    - Whenever creating a new file, the .csproj extension you downloaded will prompt you if you want to add the new file to the csproj, so **ATTENTION**, if you need auto complete in the file, you can either right click > csproj: Include in Project or click yes in the prompt:

<p align="center"><img src="https://imgur.com/b87R9bu.png"></p>

Now we are finished, if you don't like the default dark mode of VSCode you can always download themes in the Extensions tab, some nice dark mode themes are [listed here](https://medium.com/for-self-taught-developers/15-best-vscode-themes-for-dark-mode-awesomeness-82e079caf913).
