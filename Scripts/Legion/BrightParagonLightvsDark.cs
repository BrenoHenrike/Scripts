//cs_include Scripts/CoreBots.cs
using RBot;

public class BrightParagonLightvsDark
{
	public CoreBots Core = new CoreBots();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Core.AddDrop("Legion Token", "Legion Token Pile");

		while(!Core.CheckInventory("Legion Token", 25000))
		{
			Core.EnsureAccept(4704);

			Core.KillMonster("brightfortress", "r3", "Right", "*", "Badge of Loyalty", 10);
			Core.KillMonster("brightfortress", "r3", "Right", "*", "Badge of Corruption", 8);
			Core.KillMonster("brightfortress", "r3", "Right", "*", "Twisted Light Token", 6);

			Core.EnsureComplete(4704);
		}

		Core.SetOptions(false);
	}
}