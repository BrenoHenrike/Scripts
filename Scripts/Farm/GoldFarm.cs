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

		//Core.ActionDelay = 1000; //if script is having issues turning in or is slow; un // this line ^_^

		//Farm.UseBoost(BoostIDs.Gold20, RBot.Items.BoostType.Gold);

		Farm.Gold();

		Core.SetOptions(false);
	}
}
