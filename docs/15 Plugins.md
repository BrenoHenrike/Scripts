## Plugins

Plugins can be used for whatever need you may want to fulfill. In this page the examples will be simple but you certainly can design new Forms to show in RBot using plugins.

#### Creating a Plugin
To create plugin, create a new class library project in Visual Studio (or whatever IDE you use), and add a reference to RBot.exe.

Then create a new class that extends `RBot.Plugins.RPlugin` and override the plugin's properties and methods:

```csharp
using System;
using RBot;
using RBot.Plugins;

public class TestPlugin : RPlugin
{
    public override string Name => "Test Plugin";
    public override string Author => "rodit";
    public override string Description => "This is a test plugin.";

    public override List<IOption> Options => new List<IOption>();

    public override void Load()
    {
        // Called when the plugin is loaded.
        Bot.Log("Test plugin lodaed.");
    }

    public override void Unload()
    {
        // Called when the plugin is unloaded.
        Bot.Log("Test plugin unlodaed.");
    }
}
```

References to the current bot instance and the plugin's container are found at `RPlugin#Bot` and `RPlugin#Container` respectively. The plugin's container is used to get and set the plugin's options.

#### Configurable Plugin Options
Plugins can set options in the exact same way as script options are set (see 14. Script Options). The only difference is the options must be defined as a property, not a field. Options values can be get and set through `Container.Options` in the exact same way as script options:

```csharp
using System;
using System.Collections.Generic;
using RBot;
using RBot.Plugins;

public class TestPlugin : RPlugin
{
    public override string Name => "Test Plugin";
    public override string Author => "rodit";
    public override string Description => "This is a test plugin.";

    public override List<IOption> Options => new List<IOption>(){
        new Option<bool>("test", "Test Option", "This is a test option.", true)
    };

    public override void Load()
    {
        // Called when the plugin is loaded.
        Bot.Log("Test plugin lodaed.");

        if(Container.Options.Get<bool>("test"))
            Bot.Log("Test option is enabled.");
    }

    public override void Unload()
    {
        // Called when the plugin is unloaded.
        Bot.Log("Test plugin unlodaed.");
    }
}
```

Plugin options are configured by opening the plugins window and clicking on the plugin you would like to configure. Plugins in the `plugins` folder next to RBot.exe are automatically loaded on launch. Other plugins can be manually loaded by clicking the `Load` button on the plugins window.

---------
<center><a href="/Rbot-Scripts/14 Script Options" title="14. Script Options">◄ Previous</a> — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — Next ►</center>