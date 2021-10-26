//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarm.cs
using RBot;

public class HollowbornREP
{
	public CoreBots Core = new CoreBots();
	public CoreFarms Farm = new CoreFarms();
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		//Farm.UseREPBoost(REPBoost.REP20);

		Core.AddDrop("Hollow Soul");

		while (bot.Player.GetFactionRank("Hollowborn") != 10)
		{
			Core.EnsureAccept(7553);
			Core.EnsureAccept(7555);
			Core.KillMonster("shadowrealm", "r2", "Down", "*", "Darkseed", 8);
			Core.KillMonster("shadowrealm", "r2", "Down", "*", "Shadow Medallion", 5);
			Core.EnsureComplete(7553);
			Core.EnsureComplete(7555);
		}
	}
}