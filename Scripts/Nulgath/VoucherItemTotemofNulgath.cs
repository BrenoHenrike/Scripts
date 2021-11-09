//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class VoucherItemTotemofNulgath
{
	public CoreBots Core = new CoreBots();
	public CoreNulgath Nulgath = new CoreNulgath();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Core.AddDrop(Nulgath.bagDrops);

		while (!bot.ShouldExit())
		{
			Nulgath.VoucherItemTotemofNulgath(ChooseReward.TotemofNulgath);
			// Comment the line above and uncomment the line bellow to farm Gem of Nulgath
			//Nulgath.VoucherItemTotemofNulgath(ChooseReward.GemofNulgath);
		}

		Core.SetOptions(false);
	}
}