//cs_include Scripts/CoreBots.cs
using RBot;
public class DoomKnightWeaponKit
{
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core = new CoreBots();

	public string[] drops =
	{
		"DoomKnight Weapon Kit",
		"Corrupt Spirit Orb",
		"Ominous Aura",
		"Dark Spirit Orb"
	};

	public string[] reqTemp =
	{
		"No. 1337 Blade Oil",
		"Gold Brush",
		"Non-abrasive Power Powder",
		"ShadowDragon Hide",
		"Moganth's Stone Sharpener",
		"Doom Lacquer Finish",
		"Dark Wyvern Hide Travel Case"
	};

	public int[] reqQuant = { 1, 1, 1, 3, 1, 1, 1 };
	
	public string[] mobs =
	{
		"Kitsune",
		"Chaos Sphinx",
		"ProtoSartorium",
		"Shadow Dragon",
		"Moganth",
		"Shadow Nukemichi",
		"Dark Wyvern"
	};
	
	public string[] maps =
	{
		"kitsune",
		"sandcastle",
		"crashsite",
		"necrocavern",
		"dragonplane",
		"akiba",
		"dreamnexus"
	};

	public string[] cells =
	{
		"Boss",
		"r7",
		"Boss",
		"r13",
		"r9",
		"cave4boss",
		"r6"
	};

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Core.Unbank(drops);
		Core.AddDrop(drops);

		while (!bot.Inventory.Contains("Ominous Aura", 10000))
		{
			Core.EnsureAccept(2165);

			if (!Core.CheckInventory("Grumpy Warhammer"))
				Core.KillMonster("boxes", "Boss", "Left", "Sneeviltron", "Grumpy Warhammer", 1, false);

			for (int i = 0; i < reqTemp.Length; i++)
				if (!bot.Inventory.ContainsTempItem(reqTemp[i], reqQuant[i]))
					Core.KillMonster(maps[i], cells[i], "Left", mobs[i], reqTemp[i], reqQuant[i]);

			Core.EnsureComplete(2165);
		}
	}
}