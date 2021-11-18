//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailys.cs
using RBot;

public class FarmAllDailys
{
	public CoreBots Core => CoreBots.Instance;
	public CoreDailys Dailys = new CoreDailys();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Dailys.DoAllDailys();

		Core.SetOptions(false);
	}
}