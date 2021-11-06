//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;
public class GetAllRanks
{
	public CoreBots Core = new CoreBots();
	public CoreFarms Farm = new CoreFarms();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		//Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Reputation, true);

		Farm.GetAllRanks();

		Core.SetOptions(false);
	}
}