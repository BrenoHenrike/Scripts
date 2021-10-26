//cs_include Scripts/CoreBots.cs
using RBot;

public class FirstClassEntertainment
{
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core = new CoreBots();
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Core.AddDrop("Legion Token");

		Core.CheckInventory("Legion Token");

		if (bot.Map.Name != "legionarena")
			bot.Player.Join("legionarena");
		bot.Player.Jump("Boss", "Left");

		while (!bot.ShouldExit())
		{
			Core.EnsureAccept(6743);
			Core.HuntMonster("legionarena", "Legion Fiend Rider", "Axeros' Brooch");
			Core.EnsureComplete(6743);
			bot.Player.Pickup("Legion Token");
		}

		Core.SetOptions(false);
	}
}
