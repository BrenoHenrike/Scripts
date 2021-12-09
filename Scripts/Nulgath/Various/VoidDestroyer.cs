//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class VoidDestroyer
{
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core => CoreBots.Instance;
	public CoreFarms Farm = new CoreFarms();
	public CoreNulgath Nulgath = new CoreNulgath();

	public readonly string[] Rewards =
	{
		"Void Destroyer",
		"Void Destruction Blade",
		"Void Spear of War",
		"Horned Void War Helm",
		"Crested Void War Helm",
		"Wrap of the Void",
		"Tainted Destruction Blade",
		"Toxic Void Katana",
		"Dual Toxic Void Katanas"
	};
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		int i = 1;
		while(!Core.CheckInventory(Rewards, toInv: false))
		{
			Nulgath.Supplies("Unidentified 4");
			Nulgath.SwindleBulk(1);
			Nulgath.FarmDarkCrystalShard(1);
			Nulgath.EssenceofNulgath(1);
			Nulgath.FarmGemofNulgath(1);
			
			Core.ChainComplete(5661);
			bot.Player.Pickup(Rewards);
			Core.Logger($"Completed x{i++}");
		}

		Core.SetOptions(false);
	}
}