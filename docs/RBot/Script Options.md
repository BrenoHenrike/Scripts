# Script Options

You can add configurable options to your script easily by giving it an `Options` field which is of type `List<IOption>`.

- [Script Options](#script-options)
  - [Setting Up](#setting-up)
    - [Adding configurable Options](#adding-configurable-options)
    - [Using configured Options](#using-configured-options)
    - [Enumerated Options](#enumerated-options)
  - [Manually opening Configuration UI](#manually-opening-configuration-ui)

## Setting Up

To add configurable options, your script should have a `OptionsStorage` field which is a string that determines the file system location your script's options are saved at. The value of this field should be **unique to your script** to ensure option's don't clash with other scripts. Your script's options will be stored in a file at `options/{OptionsStorage}.cfg`.

```csharp
using System;
using RBot;

public class Script
{
    public string OptionsStorage = "test_script";

    public void ScriptMain(ScriptInterface bot)
    {
        //... code here
    }
}
```

If no `OptionsStorage` field is found, a value of `"default"` is used instead.

By default, the script configuration window will open when the script is started if no script with the script's `OptionsStorage` is configured already. If you would not like the configuration window to automatically open, add another field to your script called `DontPreconfigure` and set it to `true`:

```csharp
public string OptionsStorage = "test_script";
public bool DontPreconfigure = true;
```

If you would like to force the configuration window to open every time your script starts (including when a relogin occurs, and the script is restarted), set `DontPreconfigure` to `true`, and call `bot.Config.Configure()` at the start of your script (i.e. at the start of `ScriptMain`). This will pause the script's execution until the configuration window is closed.

### Adding configurable Options

Options have a name, display name, description, default value, type and a boolean which determines whether they are transient or not. If an option is marked as transient, its value is not saved. Options must have a type which inherits `IConvertable`. The constructor for an option is as follows:

```csharp
Option(string name, string displayName, string description, T defaultValue = default, bool transient = false)
```

Note: the `defaultValue` and `transient` parameters are both optional and have default values.

To add configurable options to your script, you do the following. Note the added imports to `System.Collections.Generic` and `RBot.Options`:

```csharp
using System;
using System.Collections.Generic;
using RBot;
using RBot.Options;

public class Script
{

    public string OptionsStorage = "test_script";

    public List<IOption> Options = new()
    {
        new Option<bool>("loadBank", "Load Bank", "If enabled, the bank is loaded before the script starts.", true),
        new Option<string>("mapName", "Map Name", "The name of the map to join at the start of the script.", "battleon")
    };

    public void ScriptMain(ScriptInterface bot)
    {
        bot.Options.SafeTimings = true;
        bot.Options.RestPackets = true;

        //...code here
    }
}
```

### Using configured Options

You can get and set the values of script options through `ScriptInterface#Config` using their `Name` (the first parameter to the `Option` constructor):

```csharp
using System;
using System.Collections.Generic;
using RBot;
using RBot.Options;

public class Script
{
    public string OptionsStorage = "test_script";

    public List<IOption> Options = new()
    {
        new Option<bool>("loadBank", "Load Bank", "If enabled, the bank is loaded before the script starts.", true),
        new Option<string>("mapName", "Map Name", "The name of the map to join at the start of the script.", "battleon")
    };

    public void ScriptMain(ScriptInterface bot)
    {
        bot.Options.SafeTimings = true;
        bot.Options.RestPackets = true;

        if(bot.Config.Get<bool>("loadBank"))
            bot.Player.LoadBank();
        
        bot.Player.Join(bot.Config.Get<string>("mapName"));
    }
}
```

### Enumerated Options

If you would like to add options which have a range of selectable values, you can implement an enum in your script and use that as your option type:

```csharp
using System;
using System.Collections.Generic;
using RBot;
using RBot.Options;

public class Script
{

    public string OptionsStorage = "test_script";

    public List<IOption> Options = new()
    {
        new Option<OptionEnum>("test", "Enumerated Option", "This is a test enumerated option.")
    };

    public void ScriptMain(ScriptInterface bot)
    {
        bot.Options.SafeTimings = true;
        bot.Options.RestPackets = true;

        switch(bot.Config.Get<OptionEnum>("test"))
        {
            case OptionEnum.Option1:
                // some code...
                break;
            case OptionEnum.Option2:
                // some other code...
                break;
            case OptionEnum.Option3:
                // some other other code...
                break;
        }
    }
}

public enum OptionEnum
{
    Option1,
    Option2,
    Option3
}
```

## Manually opening Configuration UI

You can manually open the configuration UI by clicking the `Script Options` button on the main form after loading a script (if the script has any configurable option).

---------
<center><a href="/Rbot-Scripts/Packets" title="Packets">◄ Previous</a> — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — <a href="/Rbot-Scripts/Object Classes and Enums" title="Object Classes and Enums">Next ►</a></center>