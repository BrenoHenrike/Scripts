//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarm.cs
using RBot;

public class YokaiREP
{
	public CoreBots Core = new CoreBots();
	public CoreFarms Farm = new CoreFarms();
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		//Farm.UseREPBoost(REPBoost.REP20);

		while (bot.Player.GetFactionRank("Yokai") != 10)
		{
			Core.EnsureAccept(383);
			Core.KillMonster("dragonkoiz-111111", "t1", "Left", "Pockey Chew", "Piece of Pockey", 3);
			Core.EnsureComplete(383);
		}

		Core.SetOptions(false);
	}
}