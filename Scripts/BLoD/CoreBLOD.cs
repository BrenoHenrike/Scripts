using RBot;

public class CoreBLOD
{
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core => CoreBots.Instance;
	public CoreFarms Farm = new CoreFarms();
	public CoreDailys Dailys = new CoreDailys();

	public string[] BLoDItems =
	{
		"Blinding Light of Destiny",
		"Get Your Blinding Light of Destiny",
		// Coppers
		"Copper",
		"Celestial Copper",
		"Celestial Copper of Destiny",
		// Maces
		"Mace of Destiny",
		"Bright Mace of Destiny",
		"Blinding Mace of Destiny",
		// Silvers
		"Silver",
		"Sanctified Silver",
		"Sanctified Silver of Destiny",
		// Bows
		"Bow of Destiny",
		"Bright Bow of Destiny",
		"Blinding Bow of Destiny",
		// Bariums
		"Barium",
		"Blessed Barium",
		"Blessed Barium of Destiny",
		// Blades
		"Blade of Destiny",
		"Bright Blade of Destiny",
		"Blinding Blade of Destiny",
		// Weapon Kits
		"Basic Weapon Kit",
		"Advanced Weapon Kit",
		"Ultimate Weapon Kit",
		// Merge misc.
		"Bone Dust",
		"Undead Essence",
		"Undead Energy",
		"Spirit Orb",
		"Loyal Spirit Orb",
		"Bright Aura",
		"Brilliant Aura",
		"Blinding Aura",
		// Misc.
		"Blinding Light of Destiny Handle",
		"Bonegrinder Medal",
		"Zardman's StoneHammer",
		"WolfClaw Hammer",
		"Great Ornate Warhammer"
	};

	public void UnlockMineCrafting()
	{
		if(Bot.Quests.IsUnlocked(2091))
			return;

		if(!Bot.Quests.IsUnlocked(2067))
		{
			Core.BuyItem("doomwood", 276, "Blinding Light of Destiny Handle");
			Core.ChainComplete(2066);
		}
		if(!Bot.Quests.IsUnlocked(2082))
		{
			Farm.Gold(15000);
			Core.BuyItem("doomwood", 276, "Bonegrinder Medal");
			Core.ChainComplete(2067);
		}
		if(!Bot.Quests.IsUnlocked(2083))
		{
			BattleUnderB("Undead Essence", 25);
			Core.ChainComplete(2082);
		}
		if(!Bot.Quests.IsUnlocked(2084))
		{
			BattleUnderB("Bone Dust", 40);
			Core.ChainComplete(2083);
		}
		if(!Bot.Quests.IsUnlocked(2091))
		{
			SpiritOrb(100);
			Core.EnsureAccept(2084);
			Core.HuntMonster("timevoid", "Ephemerite", "Celestial Compass");
			Core.EnsureComplete(2084);
			Bot.Player.Pickup("Loyal Spirit Orb");
		}
	}

	public void SpiritOrb(int quant = 10500)
	{
		if(Core.CheckInventory("Spirit Orb", quant))
			return;

		Core.EquipClass(ClassType.Farm);
		int i = 1;
		while (!Core.CheckInventory("Spirit Orb", quant))
		{
			BattleUnderB("Undead Essence", 900);
			while (Core.CheckInventory("Undead Essence", 25))
			{
				Core.ChainComplete(2082);
				Core.Logger($"Completed x{i++}");
			}
			while (Core.CheckInventory("Bone Dust", 40))
			{
				Core.ChainComplete(2083);
				Core.Logger($"Completed x{i++}");
			}
			Bot.Player.Pickup("Spirit Orb");
		}
	}

	public void BasicWK(int quant = 1)
	{
		if(Core.CheckInventory("Basic Weapon Kit", quant))
			return;

		int i = 1;
		Core.EquipClass(ClassType.Farm);
		Core.Logger($"Farming {quant} Basic Wepon Kit");
		while (!Core.CheckInventory("Basic Weapon Kit", quant))
		{
			Core.EnsureAccept(2136);

			Core.KillMonster("forest", "Forest3", "Left", "*", "Zardman's StoneHammer", 1, false);
			Core.KillMonster("noobshire", "North", "Left", "Horc Noob", "Noob Blade Oil");
			Core.KillMonster("farm", "Crop1", "Right", "Scarecrow", "Burlap Cloth", 4);

			Core.HuntMonster("pyramid", "Mummy", "Triple Ply Mummy Wrap", 7);
			Core.HuntMonster("pyramid", "Golden Scarab", "Golden Lacquer Finish");
			Core.HuntMonster("lair", "Bronze Draconian", "Bronze Brush");
			Core.HuntMonster("bloodtusk", "Rock", "Rocky Stone Sharpener");

			Core.EnsureComplete(2136);
			Bot.Player.Pickup("Basic Weapon Kit");
			Core.Logger($"Completed x{i++}");
		}
	}

	public void AdvancedWK(int quant = 1)
	{
		if(Core.CheckInventory("Advanced Weapon Kit", quant))
			return;

		int i = 1;
		Core.Logger($"Farming {quant} Advanced Wepon Kit");
		while (!Core.CheckInventory("Advanced Weapon Kit", quant))
		{
			Core.EnsureAccept(2162);

			Core.EquipClass(ClassType.Solo);
			Core.HuntMonster("hachiko", "Dai Tengu", "Superior Blade Oil");
			Core.HuntMonster("vordredboss", "Lightning Ball", "Shining Lacquer Finish");
			Core.HuntMonster("faerie", "Cyclops Warlord", "Brass Awl");
			Core.HuntMonster("darkovia", "Lich of the Stone", "Slate Stone Sharpener");

			Core.EquipClass(ClassType.Farm);
			Core.KillMonster("safiria", "c3", "Left", "Chaos Lycan", "WolfClaw Hammer", 1, false);
			Core.KillMonster("lycan", "r4", "Left", "Chaos Vampire Knight", "Silver Brush");
			Core.KillMonster("sandport", "r3", "Right", "Tomb Robber", "Leather Case");
			Core.KillMonster("pines", "Path1", "Left", "Leatherwing", "Leatherwing Hide", 10);

			Core.EnsureComplete(2162);
			Bot.Player.Pickup("Advanced Weapon Kit");
			Core.Logger($"Completed x{i++}");
		}
	}

	public void UltimateWK(string item = "Ultimate Weapon Kit", int quant = 1)
	{
		if(Core.CheckInventory(item, quant))
			return;

		int i = 1;
		Core.Logger($"Farming {quant} item");
		while (!Core.CheckInventory(item, quant))
		{
			Core.EnsureAccept(2163);

			Core.EquipClass(ClassType.Farm);
			Core.KillMonster("dragonplane", "r2", "Right", "Earth Elemental", "Great Ornate Warhammer", 1, false);

			Core.EquipClass(ClassType.Solo);
			Core.KillMonster("greendragon", "Boss", "Left", "Greenguard Dragon", "Greenguard Dragon Hide", 3);
			Core.KillMonster("sandcastle", "r7", "Left", "Chaos Sphinx", "Gold Brush");
			Core.KillMonster("crashsite", "Boss", "Left", "ProtoSartorium", "Non-abrasive Power Powder");
			Core.KillMonster("kitsune", "Boss", "Left", "Kitsune", "No. 1337 Blade Oil");
			Core.KillMonster("citadel", "m14", "Left", "Grand Inquisitor", "Blinding Lacquer Finish");
			Core.HuntMonster("djinn", "Harpy", "Suede Travel Case");
			Core.KillMonster("roc", "Enter", "Spawn", "Rock Roc", "Sharp Stone Sharpener");

			Core.EnsureComplete(2163);
			Bot.Player.Pickup(item);
			Core.Logger($"Completed x{i++}");
		}
	}

	public void BattleUnderB(string item = "Bone Dust", int quant = 1) 
		=> Core.KillMonster("battleunderb", "Enter", "Spawn", "*", item, quant, false);

	public void LightMerge(string item, int quant = 1) 
		=> Core.BuyItem("necropolis", 422, item, quant);

	public void BlindingMace()
	{
		if(Core.CheckInventory("Blinding Mace of Destiny"))
			return;

		if(!Core.CheckInventory(new[] {"Mace of Destiny", "Bright Mace of Destiny", "Blinding Mace of Destiny"}, any: true))
		{
			if (!Core.CheckInventory(new[] { "Celestial Copper of Destiny", "Celestial Copper", "Bright Aura" }, any: true)
				&& !Core.CheckInventory("Loyal Spirit Orb", 105))
				SpiritOrb();

			if (!Core.CheckInventory(new[] { "Celestial Copper of Destiny", "Celestial Copper" }, any: true)
				&& !Core.CheckInventory("Loyal Spirit Orb", 105)
				&& !Core.CheckInventory("Bright Aura", 2))
				LightMerge("Loyal Spirit Orb", 105);

			if (!Core.CheckInventory(new string[] { "Celestial Copper of Destiny", "Celestial Copper" }, any: true)
				&& !Core.CheckInventory("Bright Aura", 2))
				LightMerge("Bright Aura", 2);

			if (!Core.CheckInventory("Celestial Copper of Destiny"))
			{
				if (!Core.CheckInventory("Celestial Copper"))
				{
					Core.EnsureAccept(2107);
					BattleUnderB("Undead Energy", 25);
					Dailys.MineCrafting(new[] { "Copper" });
					if (!Core.CheckInventory("Copper"))
						Core.Logger("Can't complete Celestial Copper Enchantment (Missing Copper).", messageBox: true, stopBot: true);
					SpiritOrb(5);
					Core.HuntMonster("arcangrove", "Seed Spitter", "Paladaffodil", 25);
					Core.EnsureComplete(2107);
				}
				Core.BuyItem("dwarfhold", 434, "Celestial Copper of Destiny");
			}

			if (!Bot.Quests.IsUnlocked(2136))
			{
				Core.EnsureAccept(2133);
				Core.KillMonster("dwarfhold", "Enter", "Spawn", "Albino Bat", "Forge Key", 1, false);
				Core.EnsureComplete(2133);
			}

			BattleUnderB("Undead Energy", 7);
			BasicWK();
			AdvancedWK();
			UltimateWK("Loyal Spirit Orb");
			UltimateWK("Spirit Orb", 20);
			LightMerge("Mace of Destiny");
		}

		if(Core.CheckInventory("Mace of Destiny"))
		{
			AdvancedWK();
			UltimateWK("Bright Aura", 2);
			LightMerge("Bright Mace of Destiny");
		}

		if(Core.CheckInventory("Bright Mace of Destiny"))
		{
			UltimateWK();
			LightMerge("Bliding Mace of Destiny");
		}
	}

	public void BlindingBow()
	{
		if(Core.CheckInventory("Blinding Bow of Destiny"))
			return;
		if(!Core.CheckInventory("Bliding Mace of Destiny"))
			BlindingMace();

		if (!Core.CheckInventory(new[] { "Blinding Bow of Destiny", "Bright Bow of Destiny", "Bow of Destiny" }, any: true))
		{
			if (!Core.CheckInventory("Sanctified Silver of Destiny"))
			{
				if (!Core.CheckInventory("Sanctified Silver"))
				{
					Core.EnsureAccept(2108);
					BattleUnderB("Undead Energy", 25);
					Dailys.MineCrafting(new[] { "Silver" });
					if (!Core.CheckInventory("Silver"))
						Core.Logger("Can't complete Sanctified Silver Enchantment (Missing Silver).", messageBox: true, stopBot: true);
					UltimateWK("Spirit Orb", 5);
					Core.HuntMonster("arcangrove", "Seed Spitter", "Paladaffodil", 25);
					Core.EnsureComplete(2108);
				}

				UltimateWK("Loyal Spirit Orb", 5);
				UltimateWK("Bright Aura", 2);
				Core.BuyItem("dwarfhold", 434, "Sanctified Silver of Destiny");
			}

			FindingFragmentsMace();
			BattleUnderB("Undead Energy", 17);
			UltimateWK("Loyal Spirit Orb");
			UltimateWK("Spirit Orb", 13);
			BasicWK();
			LightMerge("Bow of Destiny");
		}

		if (Core.CheckInventory("Bow of Destiny"))
		{
			UltimateWK("Loyal Spirit Orb", 3);
			AdvancedWK();
			LightMerge("Bright Bow of Destiny");
		}

		if (Core.CheckInventory("Bright Bow of Destiny"))
		{
			UltimateWK();
			LightMerge("Blinding Bow of Destiny");
		}

	}

	public void BlindingBlade()
	{
		if(Core.CheckInventory("Blinding Blade of Destiny"))
			return;
		if(!Core.CheckInventory("Blinding Bow of Destiny"))
			BlindingBow();

		if (!Core.CheckInventory(new[] { "Blinding Blade of Destiny", "Bright Blade of Destiny", "Blade of Destiny" }, any: true))
		{
			if (!Core.CheckInventory("Blessed Barium of Destiny"))
			{
				if (!Core.CheckInventory("Blessed Barium"))
				{
					Core.EnsureAccept(2104);
					BattleUnderB("Undead Energy", 25);
					Dailys.MineCrafting(new[] { "Barium" });
					if (!Core.CheckInventory("Barium"))
						Core.Logger("Can't complete Sanctified Barium Enchantment (Missing Barium).", messageBox: true, stopBot: true);
					UltimateWK("Spirit Orb", 5);
					Core.HuntMonster("arcangrove", "Seed Spitter", "Paladaffodil", 25);
					Core.EnsureComplete(2104);
				}

				FindingFragmentsBow(2);
				UltimateWK("Loyal Spirit Orb", 5);
				Core.BuyItem("dwarfhold", 434, "Blessed Barium of Destiny");
			}

			FindingFragmentsMace();
			UltimateWK("Loyal Spirit Orb");
			UltimateWK("Spirit Orb", 15);
			BasicWK();
			LightMerge("Blade of Destiny");
		}

		if (Core.CheckInventory("Blade of Destiny"))
		{
			FindingFragmentsMace();
			AdvancedWK();
			LightMerge("Bright Blade of Destiny");
		}

		if (Core.CheckInventory("Bright Blade of Destiny"))
		{
			UltimateWK();
			LightMerge("Blinding Blade of Destiny");
		}
	}

	public void TheBlindingLightofDestiny()
	{
		if(Core.CheckInventory("Blinding Light of Destiny"))
			return;

		FindingFragmentsBow(125);
		FindingFragmentsMace(75);
		FindingFragmentsBlade(500, 250);
		int i = 1;
		Core.Logger(Core.CheckInventory("Blinding Aura") ? "Blinding Aura found." : "Farming for Blinding Aura");
		while (!Core.CheckInventory("Blinding Aura"))
		{
			FindingFragments(2174);
			Bot.Player.Pickup("Blinding Aura");
			Core.Logger($"Completed x{i}");
		}
		UltimateWK();
		Core.ChainComplete(2180);
		Bot.Player.Pickup("Get Your Blinding Light of Destiny");
	}



	public void FindingFragmentsMace(int quant = 1)
	{
		if(Core.CheckInventory("Brilliant Aura", quant))
			return;
		if(!Core.CheckInventory("Blinding Mace of Destiny"))
			BlindingMace();

		int i = 1;
		Core.EquipClass(ClassType.Farm);
		while (!Core.CheckInventory("Brilliant Aura", quant))
		{
			FindingFragments(2176);
			Bot.Player.Pickup("Brilliant Aura");
			Core.Logger($"Completed x{i++}");
		}
	}

	public void FindingFragmentsBow(int quant = 1)
	{
		if (Core.CheckInventory("Bright Aura", quant))
			return;
		if (!Core.CheckInventory("Blinding Bow of Destiny"))
			BlindingBow();

		int i = 1;
		Core.EquipClass(ClassType.Farm);
		while (!Core.CheckInventory("Bright Aura", quant))
		{
			FindingFragments(2174);
			Bot.Player.Pickup("Bright Aura");
			Core.Logger($"Completed x{i++}");
		}
	}

	public void FindingFragmentsBlade(int quantSO, int quantLSO)
	{
		if (Core.CheckInventory("Spirit Orb", quantSO) && Core.CheckInventory("Loyal Spirit Orb", quantLSO))
			return;
		if (!Core.CheckInventory("Blinding Blade of Destiny"))
			BlindingBlade();

		int i = 1;
		Core.EquipClass(ClassType.Farm);
		while (!Core.CheckInventory("Spirit Orb", quantSO) || !Core.CheckInventory("Loyal Spirit Orb", quantLSO))
		{
			FindingFragments(2179);
			Bot.Player.Pickup("Spirit Orb", "Loyal Spirit Orb");
			Core.Logger($"Completed x{i++}");
		}
	}

	public void FindingFragments(int quest)
	{
		Core.EnsureAccept(quest);
		BattleUnderB("Blinding Light Framents", 10);
		Core.EnsureComplete(quest);
	}
}