# Plugins

Plugins can be used for whatever need you may want to fulfill. In this page the examples will be simple but you certainly can design new Forms to show in RBot using plugins.

## Creating a Plugin

To create plugin, create a new class library project in Visual Studio (or whatever IDE you use) for .NET Framework 4.7.2, and add a reference to RBot.exe.

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

## Configurable Plugin Options

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
        Bot.Log("Test plugin loaded.");

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

## User Interface plugins

If you want your plugin to do more than just set options and instead show a user interface that will do whatever you have in mind, first you will need some knowledge in Visual Studio's [WinForms](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/?view=netframeworkdesktop-4.8 "WinForms Documentation").

To start your plugin follow the ["Create a Plugin"](#creating-a-plugin). When all is set up you can now add a Form to your project, press **F7** to go to the code behind the Design of your form and make it inherit from **_RBot.HideForm_** and create an instance for it:

```csharp
// YourFormName inherits from HideForm which inherits from Form too
public partial class YourFormName : RBot.HideForm
{
    // Instance of your form
    public static YourFormName Instance { get; } = new YourFormName();
    public YourFormName()
    {
        InitializeComponent();
    }
}
```

The instance will allow you to reference it in other places and the HideForm inheritance will allow your form to not be disposed when closing while using it in RBot. Now back to **_TestPlugin.cs_** we can make it create a button in the RBot menu when loaded and remove it when unloaded:

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

    // This will be the button we will show in the RBot main menu
    private ToolStripItem menuItem;

    public override void Load()
    {
        // We set and add it to RBot main menu, 
        // the button text will be "Test Plugin"
        menuItem = RBot.Forms.Main.MainMenu.Items.Add("Test Plugin");
        // When we click in the button it will call MenuItem_Click method
		menuItem.Click += MenuItem_Click;
    }

    private void MenuItem_Click(object sender, EventArgs e)
    {
        // When you click the button we check if the form is visible
        bool visible = YourFormName.Instance.Visible;
        // If your form is visible...
        if (visible)
        {
            // We hide it
            YourFormName.Instance.Hide();
        }
        else
        {
            // If not we show it and bring it in front of everything
            YourFormName.Instance.Show();
            YourFormName.Instance.BringToFront();
        }
    }

    public override void Unload()
    {
        // When unloading we remove the event handler
        menuItem.Click -= MenuItem_Click;
        // Remove it from the main menu
        RBot.Forms.Main.MainMenu.Items.Remove(menuItem);
    }
}
```

After that you can build your project (**Ctrl+Shift+B**), it will give you a warning that libraries don't have an executable, which is fine. To test it in RBot you can load it using the **Plugins** tab and find it in **_"*Directory-of-the-Project/bin/Debug/Name-of-the-project.dll"_**.

After that it will show a blank page but with the basics up and running you can add any controls and code you want to your user interface.

---------
<center><a href="/Rbot-Scripts/Script Options" title="Script Options">◄ Previous</a> — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — Next ►</center>