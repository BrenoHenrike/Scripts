//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;
public class ElementalMasterREP
{
	public CoreBots Core = new CoreBots();
	public CoreFarms Farm = new CoreFarms();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		//Farm.UseREPBoost(REPBoost.REP20);

		Farm.ElementalMasterREP();

		Core.SetOptions(false);
	}
}