//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class GoldFarm
{
	public CoreBots Core = new CoreBots();
	public CoreFarms Farm = new CoreFarms();
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		//Farm.UseGoldBoost(GoldBoost.Gold20);

		Farm.BattleGroundE();

		Core.SetOptions(false);
	}
}
