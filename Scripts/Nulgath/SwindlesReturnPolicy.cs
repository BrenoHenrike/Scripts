//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class SwindlesReturnPolicy
{
	public CoreBots Core = new CoreBots();
	public CoreFarms Farm = new CoreFarms();
	public CoreNulgath Nulgath = new CoreNulgath();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Core.AddDrop(Nulgath.bagDrops);
		Core.AddDrop("Unidentified 1", "Unidentified 6",
			"Unidentified 9", "Unidentified 16",
			"Unidentified 20", "Receipt of Swindle");

		Core.Unbank("Tainted Gem", "Dark Crystal Shard",
			"Diamond of Nulgath", "Gem of Nulgath",
			"Blood Gem of the Archfiend",
			"Unidentified 1", "Unidentified 6",
			"Unidentified 9", "Unidentified 16",
			"Unidentified 20", "Receipt of Swindle");

		while(!Core.CheckInventory("Receipt of Swindle", 100))
		{
			Core.EnsureAccept(7551);
			if (Core.CheckInventory(Nulgath.CragName))
			{
				Nulgath.BambloozevsDrudgen("Unidentified 1");
				Nulgath.BambloozevsDrudgen("Unidentified 6");
				Nulgath.BambloozevsDrudgen("Unidentified 9");
				Nulgath.BambloozevsDrudgen("Unidentified 16");
				Nulgath.BambloozevsDrudgen("Unidentified 20");
			}
			else
			{
				Nulgath.Supplies("Unidentified 1");
				Nulgath.Supplies("Unidentified 6");
				Nulgath.Supplies("Unidentified 9");
				Nulgath.Supplies("Unidentified 16");
				Nulgath.Supplies("Unidentified 20");
			}
			Core.HuntMonster("evilmarsh", "Dark Makai", "Dark Makai Rune");

			if (!Core.CheckInventory("Tainted Gem", 1000))
				Core.EnsureComplete(7551, 4769);
			if (!Core.CheckInventory("Dark Crystal Shard", 1000))
				Core.EnsureComplete(7551, 4770);
			if (!Core.CheckInventory("Diamond of Nulgath", 1000))
				Core.EnsureComplete(7551, 4771);
			if (!Core.CheckInventory("Gem of Nulgath", 300))
				Core.EnsureComplete(7551, 6136);
			if (!Core.CheckInventory("Blood Gem of the Archfiend", 100))
				Core.EnsureComplete(7551, 22332);
		}

		Core.SetOptions(false);
	}
}