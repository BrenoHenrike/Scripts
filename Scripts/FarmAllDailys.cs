//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailys.cs
using RBot;

public class FarmAllDailys
{
	public CoreBots Core = new CoreBots();
	public CoreDailys Dailys = new CoreDailys();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Dailys.DoAllDailys();

		Core.SetOptions(false);
	}
}