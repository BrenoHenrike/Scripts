//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class VoidSpartan
{
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core => CoreBots.Instance;
	public CoreFarms Farm = new CoreFarms();
	public CoreNulgath Nulgath = new CoreNulgath();

	public readonly string[] Rewards =
	{
		"Void Spartan",
		"Void Spartan Daggers",
		"Void Spartan Blade and Shield",
		"Void Spartan Spear",
		"Void Spartan Blade",
		"Void Spartan Banners",
		"Void Spartan Cape",
		"Void Spartan Shielded Cape",
		"Void Spartan Spear and Shield",
		"Void Spartan Helm",
		"Void Spartan Helm and Scarf"
	};
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();
		Core.AddDrop(Nulgath.bagDrops);
		Core.AddDrop("Zee's Red Jasper", "Fiend Cloak of Nulgath");

		int i = 1;
		while(!Core.CheckInventory(Rewards, toInv: false))
		{
			Core.EnsureAccept(5982);

			Nulgath.FarmUni13();
			Nulgath.FarmDiamondofNulgath(15);
			Nulgath.FarmGemofNulgath(10);
			Core.HuntMonster("pyrewatch", "Flame Soldier", "Zee's Red Jasper", 1, false);
			Core.JumpWait();
			Farm.Gold(500000);
			Core.JoinTercessuinotlim();
			Core.BuyItem("tercessuinotlim", 68, "Fiend Cloak of Nulgath");

			Core.EnsureComplete(5982);
			bot.Player.Pickup(Rewards);
			Core.Logger($"Completed x{i++}");
		}

		Core.SetOptions(false);
	}
}