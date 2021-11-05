//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;
public class BaconCatREP
{
	public CoreBots Core = new CoreBots();
	public CoreFarms Farm = new CoreFarms();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		//Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Reputation, false);

		Farm.BaconCatREP();

		Core.SetOptions(false);
	}
}