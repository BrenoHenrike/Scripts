//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class GoldFarm
{
	public CoreBots Core => CoreBots.Instance;
	public CoreFarms Farm = new CoreFarms();
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		//Farm.UseBoost(BoostIDs.Gold20, RBot.Items.BoostType.Gold);

		Farm.Gold();

		Core.SetOptions(false);
	}
}
