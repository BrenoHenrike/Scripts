# Creating a Script
Creating a bot script requires some knowledge of C#, first time seeing it? You can learn from:  
* [W3Schools](https://www.w3schools.com/cs/index.php)  
* [Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/csharp/)  
* [Code Academy (login needed)](https://www.codecademy.com/learn/learn-c-sharp)

I would strongly recommend using the script editor that comes with RBot. This can be opened by clicking 'Edit Script' on the RBot Scripts form, or by running **ScriptEditor.exe** found in the RBot directory.

To create a script, simply open the script editor and you will be presented with a blank template script.

The main class (called `Script` by default) can be called anything, but **must** be the first class declared in the script!

The `ScriptMain` method acts as the entry point for the script. The method must not be enclosed in a class. When the script is started by the bot, the code in this method is run.

Scripts are always run on their own thread, separate from the UI thread. This means you can pause the script thread without bothering the rest of the application.

All the standard .NET APIs can be used in scripts. This means you can use any standard namespace as follows:

```csharp
using RBot;
using System;
using System.Windows.Forms;

public class Script
{
	public void ScriptMain(ScriptInterface bot)
	{
		Console.WriteLine("Hello World");
		MessageBox.Show("Hello World");
	}
}
```

You can also create custom classes and use them in scripts as you would anywhere else in C#:

```csharp
using RBot;
using System.Windows.Forms;

public class Script
{
	public class TestClass
	{
		public string TestString { get; set; }

		public TestClass()
		{
			TestString = "This class has been instantiated";
		}
	}

	public void ScriptMain(ScriptInterface bot)
	{
		TestClass test = new TestClass();
		MessageBox.Show(test.TestString);
	}
}
```

This would show a native windows `MessageBox` containing the message `"This class has been instantiated"`, as you would expect. Custom classes can also be defined outside of the main script class (`Script` in the example above), but they must be declared **after** the main script class.

## The Script Interface
The `ScriptInterface` class provides a multitude of methods and properties for accessing and interacting with the game. An instance of the `ScriptInterface` class is passed as the first (and only) argument to the `ScriptMain` method of every script run by the bot application.

---------
<center>
◄ Previous — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — <a href="/Rbot-Scripts/2 Options and Lite" title="2. Options and Lite">Next ►</a></center>