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

		Farm.YokaiREP();

		Core.SetOptions(false);
	}
}