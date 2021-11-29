using RBot;

public class CoreSDKA
{
	// [Can Change]
	// True = does the Dark Spirit Orbs quest
	// False = does the Penny for your Foughts quest
	public bool DSOMethod = false;
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core => CoreBots.Instance;
	public CoreFarms Farm = new CoreFarms();
	public CoreDailys Dailys = new CoreDailys();
	public string[] SDKAItems =
	{
        "Sepulchure's DoomKnight Armor",
		// Arsenics
		"Arsenic",
		"Accursed Arsenic",
		"Accursed Arsenic of Doom",
		// Daggers
		"Daggers of Destruction",
		"Shadow Daggers of Destruction",
		"Necrotic Daggers of Destruction",
		// Chromiums
		"Chromium",
        "Calamitous Chromium",
        "Calamitous Chromium of Doom",
		// Broadswords
		"Broadsword of Bane",
		"Shadow Broadsword of Bane",
		"Necrotic Broadsword of Bane",
		// Rhodiums
		"Rhodium",
        "Reprehensible Rhodium",
        "Reprehensible Rhodium of Doom",
		// Bows
		"Bow to the Shadows",
		"ShadowBow of the Shadows",
		"Necrotic Bow of the Shadow",
		// Merge misc.
		"Experimental Dark Item",
		"Dark Energy",
		"Dark Spirit Orb",
		"Corrupt Spirit Orb",
		"Ominous Aura",
        "Diabolical Aura",
		"Doom Aura",
		// Weapon kits
		"DoomSquire Weapon Kit",
		"DoomSoldier Weapon Kit",
		"DoomKnight Weapon Kit",
		// Misc.
		"DoomKnight Hood",
		"Elders' Blood",
		"Undead Energy",
		"Iron Hammer",
		"War Mummy Wrap",
		"Stone Hammer",
		"Grumpy Warhammer",
		"Shadow Terror Axe",
		"DoomCoin",
		"Dark Skull",
		"Shadow Creeper Enchant",
		"Shadow Serpent Scythe"
	};

	public void UnlockHardCoreMetals()
	{
		if(Bot.Quests.IsUnlocked(2098))
			return;

		if (!Bot.Quests.IsUnlocked(2087))
		{
			DSO(40);
			Core.BuyItem("shadowfall", 100, "DoomKnight Hood");
			Core.ChainComplete(2069);
            Bot.Player.Pickup("Experimental Dark Item");
            Core.ToBank("Experimental Dark Item");
        }
		if (!Bot.Quests.IsUnlocked(2088))
		{
			if (!Core.CheckInventory(2083))
				Core.Logger("You don't have the DoomKnight Class.", messageBox: true, stopBot: true);
			if (Core.CheckInventory(2083) && Bot.Inventory.GetQuantity("DoomKnight") != 302500)
			{
				Bot.Player.EquipItem(2083);
				Farm.IcestormArena(rankUpClass: true);
			}
			Core.ChainComplete(2087);
            Core.ToBank("DoomKnight");
        }
		if (!Bot.Quests.IsUnlocked(2089))
		{
			Dailys.EldersBlood();
			if(!Core.CheckInventory("Elders' Blood"))
                Core.Logger("No Elders' Blood available.", messageBox: true, stopBot: true);
            Core.HuntMonster("battleundera", "Bone Terror", "Shadow Terror Axe", 1, false);
			Core.ChainComplete(2088);
            Core.ToBank("Elders' Blood");
        }
		if (!Bot.Quests.IsUnlocked(2090))
			Penny(oneTime: true);
		if (!Bot.Quests.IsAvailable(2098))
		{
			Core.EnsureAccept(2090);
			DSO(100);
			Core.HuntMonster("necrocavern", "Shadow Imp", "Dark Skull", 1, false);
			Core.EnsureComplete(2090);
		}
	}

	public void FarmDSO(int quant = 10500)
	{
		if(DSOMethod)
			DSO(quant);
		else
			Penny(quant);
	}

	public void Penny(int quant = 10500, bool oneTime = false)
	{
		if (Core.CheckInventory("Dark Spirit Orb", quant) && !oneTime)
			return;

		if(!oneTime)
		{
			Core.Logger($"Farming {quant} DSOs");
			Core.EquipClass(ClassType.Farm);
		}
		int i = 1;
		while (!Core.CheckInventory("Dark Spirit Orb", quant))
		{
			Core.EnsureAccept(2089);
			Core.KillMonster("maul", "r7", "Left", "*", "DoomCoin", oneTime ? 20 : 80, false);
			while (Bot.Inventory.Contains("DoomCoin", 20))
			{
				Core.EnsureComplete(2089);
				Core.Logger($"Completed {i++}");
			}
			if (oneTime)
				break;
		}
	}

	public void DSO(int quant = 10500)
	{
		if (Core.CheckInventory("Dark Spirit Orb", quant))
			return;
		Core.Logger($"Farming {quant} DSOs");
		Core.EquipClass(ClassType.Farm);
		int i = 1;
		while (!Core.CheckInventory("Dark Spirit Orb", quant))
		{
			Core.EnsureAccept(2085);

			Core.HuntMonster("bludrut2", "Shadow Creeper", "Shadow Creeper Enchant", 1, false);
			Core.HuntMonster("bludrut4", "Shadow Serpent", "Shadow Serpent Scythe", 1, false);
			Core.HuntMonster("ruins", "Dark Witch", "Shadow Whiskers", 6);

			Core.EnsureComplete(2085);
			Core.Logger($"Completed {i++}");
		}
	}

	public void DoomSquireWK(int quant = 1)
	{
		if(Core.CheckInventory("DoomSquire Weapon Kit", quant))
			return;

		int i = 1;
		Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} DoomSquire Weapon Kit");
        while(!Core.CheckInventory("DoomSquire Weapon Kit", quant))
		{
			Core.EnsureAccept(2144);
			
			Core.BuyItem("swordhaven", 179, "Iron Hammer");

			Core.KillMonster("sandcastle", "r5", "Left", "War Mummy", "War Mummy Wrap", 1, false);
			Core.KillMonster("noobshire", "North", "Left", "Horc Noob", "Noob Blade Oil");
			Core.KillMonster("farm", "Crop1", "Right", "Scarecrow", "Burlap Cloth", 4);

			Core.HuntMonster("lair", "Bronze Draconian", "Bronze Brush");
			Core.HuntMonster("bludrut", "Rock Elemental", "Elemental Stone Sharpener");
			Core.HuntMonster("nulgath", "Dark Makai", "Dark Makai Lacquer Finish");

			Core.EnsureComplete(2144);
            Bot.Player.Pickup("DoomSquire Weapon Kit");
            Core.Logger($"Completed x{i++}");
		}
	}

	public void DoomSoldierWK(int quant = 1)
	{
        if (Core.CheckInventory("DoomSoldier Weapon Kit", quant))
            return;

        int i = 1;
        Core.Logger($"Farming {quant} DoomSoldier Weapon Kit");
        while (!Core.CheckInventory("DoomSoldier Weapon Kit", quant))
        {
            Core.EnsureAccept(2164);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("cornelis", "Stone Golem", "Stone Hammer", 1, false);
            Core.HuntMonster("hachiko", "Dai Tengu", "Superior Blade Oil");
            Core.HuntMonster("vordredboss", "Shadow Vordred", "Shadow Lacquer Finish");
            Core.HuntMonster("anders", "Copper Sky Pirate", "Copper Awl");
            Core.HuntMonster("necrocavern", "Shadow Imp", "Shadowstone Sharpener");

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("lycan", "r4", "Left", "Chaos Vampire Knight", "Silver Brush");
            Core.KillMonster("sandport", "r3", "Right", "Tomb Robber", "Leather Case");
            Core.KillMonster("pines", "Path1", "Left", "Leatherwing", "Leatherwing Hide", 10);

            Core.EnsureComplete(2164);
            Bot.Player.Pickup("DoomSoldier Weapon Kit");
            Core.Logger($"Completed x{i++}");
        }
	}

	public void DoomKnightWK(string item = "DoomKnight Weapon Kit", int quant = 1)
	{
		if(Core.CheckInventory(item, quant))
            return;

        int i = 1;
        Core.EquipClass(ClassType.Solo);
        Core.Logger($"Farming {quant} {item}");
		while(!Core.CheckInventory(item, quant))
		{
            Core.EnsureAccept(2165);

            Core.KillMonster("boxes", "Boss", "Left", "Sneeviltron", "Grumpy Warhammer", 1, false);
            Core.KillMonster("kitsune", "Boss", "Left", "Kitsune", "No. 1337 Blade Oil");
            Core.KillMonster("sandcastle", "r7", "Left", "Chaos Sphinx", "Gold Brush");
            Core.KillMonster("crashsite", "Boss", "Left", "ProtoSartorium", "Non-abrasive Power Powder");
            Core.KillMonster("necrocavern", "r13", "Left", "Shadow Dragon", "ShadowDragon Hide", 3);
            Core.KillMonster("dragonplane", "r9", "Left", "Moganth", "Moganth's Stone Sharpener");
            Core.KillMonster("akiba", "cave4boss", "Left", "Shadow Nukemichi", "Doom Lacquer Finish");
            Core.KillMonster("dreamnexus", "r6", "Left", "Dark Wyvern", "Dark Wyvern Hide Travel Case");

            Core.EnsureComplete(2165);
            Bot.Player.Pickup(item);
            Core.Logger($"Completed x{i++}");
        }
    }

	public void NecroticDaggers()
	{
		if(Core.CheckInventory("Necrotic Daggers of Destruction"))
			return;

		if (!Core.CheckInventory(new[] { "Necrotic Daggers of Destruction", "Shadow Daggers of Destruction", "Daggers of Destruction" }, any: true))
		{
			if (!Core.CheckInventory(new[] { "Accursed Arsenic of Doom", "Accursed Arsenic", "Ominous Aura" }, any: true) 
				&& !Core.CheckInventory("Corrupt Spirit Orb", 105))
				FarmDSO();

			if (!Core.CheckInventory(new[] { "Accursed Arsenic of Doom", "Accursed Arsenic" }, any: true) 
				&& !Core.CheckInventory("Corrupt Spirit Orb", 105) 
				&& !Core.CheckInventory("Ominous Aura", 2))
                Core.BuyItem("necropolis", 296, "Corrupt Spirit Orb", 105);

            if (!Core.CheckInventory(new string[] { "Accursed Arsenic of Doom", "Accursed Arsenic" }, any: true) 
				&& !Core.CheckInventory("Ominous Aura", 2))
                Core.BuyItem("necropolis", 296, "Ominous Aura", 2);

            if (!Core.CheckInventory("Accursed Arsenic of Doom"))
			{
				if (!Core.CheckInventory("Accursed Arsenic"))
				{
					Core.EnsureAccept(2110);
					Core.HuntMonster("bludrut4", "Shadow Serpent", "Dark Energy", 26, false);
					Dailys.HardCoreMetals(new[] { "Arsenic" });
					if (!Core.CheckInventory("Arsenic"))
						Core.Logger("Can't complete Accursed Arsenic Hex (Missing Arsenic).", messageBox: true, stopBot: true);
					DSO(6);
					Core.HuntMonster("arcangrove", "Seed Spitter", "Deadly Knightshade", 16);
					Core.EnsureComplete(2110);
				}
				Core.BuyItem("dwarfhold", 434, "Accursed Arsenic of Doom");
			}

			if(!Bot.Quests.IsUnlocked(2144))
			{
				Core.EnsureAccept(2137);
				Core.KillMonster("dwarfhold", "Enter", "Spawn", "Albino Bat", "Forge Key", 1, false);
				Core.EnsureComplete(2137);
			}

			DoomSquireWK();
			Core.BuyItem("necropolis", 296, "Daggers of Destruction");
		}

        if(Core.CheckInventory("Daggers of Destruction"))
		{
            DoomSoldierWK();
            DoomKnightWK("Ominous Aura");
            Core.BuyItem("necropolis", 296, "Shadow Daggers of Destruction");
        }

		if(Core.CheckInventory("Shadow Daggers of Destruction"))
		{
            DoomKnightWK();
            Core.BuyItem("necropolis", 296, "Necrotic Daggers of Destruction");
        }
    }

	public void NecroticBroadsword()
	{
        if(Core.CheckInventory("Necrotic Broadsword of Bane"))
            return;
		if(!Core.CheckInventory("Necrotic Daggers of Destruction"))
            NecroticDaggers();

        if (!Core.CheckInventory(new[] { "Necrotic Broadsword of Bane", "Shadow Broadsword of Bane", "Broadsword of Bane" }, any: true))
		{
			if(!Core.CheckInventory("Calamitous Chromium of Doom"))
			{
	            if(!Core.CheckInventory("Calamitous Chromium"))
				{
					Core.EnsureAccept(2112);
					Core.HuntMonster("bludrut4", "Shadow Serpent", "Dark Energy", 26, false);
					Dailys.HardCoreMetals(new[] { "Chromium" });
					if (!Core.CheckInventory("Chromium"))
						Core.Logger("Can't complete Calamitous Chromium Hex (Missing Chromium).", messageBox: true, stopBot: true);
					DSO(6);
					Core.HuntMonster("arcangrove", "Seed Spitter", "Deadly Knightshade", 16);
					Core.EnsureComplete(2112);
				}

	            PinpointDaggers();
	            DoomKnightWK("Corrupt Spirit Orb", 5);
	            Core.BuyItem("dwarfhold", 434, "Calamitous Chromium of Doom");
	        }
			if(!Core.CheckInventory("Diabolical Aura"))
			{
                PinpointDaggers(25);
                Core.BuyItem("necropolis", 296, "Diabolical Aura");
            }

            DoomKnightWK("Corrupt Spirit Orb");
            DoomKnightWK("Dark Spirit Orb", 20);
            DoomSquireWK();
            Core.BuyItem("necropolis", 296, "Broadsword of Bane");
		}

		if(Core.CheckInventory("Broadsword of Bane"))
		{
            DoomKnightWK("Corrupt Spirit Orb");
            PinpointDaggers(1);
            DoomSoldierWK();
            Core.BuyItem("necropolis", 296, "Shadow Broadsword of Bane");
        }

		if(Core.CheckInventory("Shadow Broadsword of Bane"))
		{
            DoomKnightWK();
            Core.BuyItem("necropolis", 296, "Necrotic Broadsword of Bane");
        }
    }

	public void NecroticBow()
	{
        if (Core.CheckInventory("Necrotic Bow of the Shadow"))
            return;
        if (!Core.CheckInventory("Necrotic Broadsword of Bane"))
            NecroticBroadsword();

        if (!Core.CheckInventory(new[] { "Necrotic Bow of the Shadow", "ShadowBow of the Shadows", "Bow to the Shadows" }, any: true))
        {
            if (!Core.CheckInventory("Reprehensible Rhodium of Doom"))
            {
                if (!Core.CheckInventory("Reprehensible Rhodium"))
                {
                    Core.EnsureAccept(2114);
                    Core.HuntMonster("bludrut4", "Shadow Serpent", "Dark Energy", 26, false);
                    Dailys.HardCoreMetals(new[] { "Rhodium" });
                    if (!Core.CheckInventory("Rhodium"))
                        Core.Logger("Can't complete Reprehensible Rhodium Hex (Missing Rhodium).", messageBox: true, stopBot: true);
                    DSO(6);
                    Core.HuntMonster("arcangrove", "Seed Spitter", "Deadly Knightshade", 16);
                    Core.EnsureComplete(2114);
                }

                PinpointDaggers();
                DoomKnightWK("Corrupt Spirit Orb", 5);
                Core.BuyItem("dwarfhold", 434, "Reprehensible Rhodium of Doom");
            }

            DoomSquireWK();
            DoomKnightWK("Corrupt Spirit Orb");
            DoomKnightWK("Dark Spirit Orb", 13);
            PinpointBroadsword();
            Core.KillMonster("battleunderb", "Enter", "Spawn", "*", "Undead Energy", 17, false);
            Core.BuyItem("necropolis", 296, "Bow to the Shadows");
        }

		if(Core.CheckInventory("Bow to the Shadows"))
		{
            DoomSoldierWK();
            DoomKnightWK("Corrupt Spirit Orb");
            Core.HuntMonster("bludrut4", "Shadow Serpent", "Dark Energy", 50, false);
            Core.BuyItem("necropolis", 296, "ShadowBow of the Shadows");
        }

		if(Core.CheckInventory("ShadowBow of the Shadows"))
		{
            DoomKnightWK();
            Core.BuyItem("necropolis", 296, "Necrotic Bow of the Shadow");
        }
    }
	
	public void SummoningSepulchureArmor()
	{
        PinpointBow(500, 250);
        PinpointDaggers(125);
        PinpointBroadsword(75);
        int i = 1;
        Core.Logger(Core.CheckInventory("Doom Aura") ? "Doom Aura found." : "Farming for Doom Aura");
        while(!Core.CheckInventory("Doom Aura"))
		{
            PinpointthePieces(2181);
            Bot.Player.Pickup("Doom Aura");
            Core.Logger($"Completed x{i}");
        }
        if(!Core.CheckInventory("Experimental Dark Item"))
		{
            DSO(40);
            Core.BuyItem("shadowfall", 100, "DoomKnight Hood");
            Core.ChainComplete(2069);
            Bot.Player.Pickup("Experimental Dark Item");
		}
        DoomKnightWK();
        Core.EnsureAccept(2187);
        Core.HuntMonster("ruins", "Dark Elemental", "Heart of Darkness");
        Core.EnsureComplete(2187);
        Bot.Player.Pickup("Sepulchure's DoomKnight Armor");

    }
	
	public void PinpointDaggers(int quant = 5)
	{
		if(Core.CheckInventory("Ominous Aura", quant))
            return;
        if(!Core.CheckInventory("Necrotic Daggers of Destruction"))
            NecroticDaggers();
		
        int i = 1;
        Core.EquipClass(ClassType.Farm);
        while(!Core.CheckInventory("Ominous Aura", quant))
		{
            PinpointthePieces(2181);
            Bot.Player.Pickup("Ominous Aura");
            Core.Logger($"Completed x{i}");
        }
    }

	public void PinpointBroadsword(int quant = 1)
	{
        if (Core.CheckInventory("Diabolical Aura", quant))
            return;
        if (!Core.CheckInventory("Necrotic Broadsword of Bane"))
            NecroticBroadsword();

        int i = 1;
        Core.EquipClass(ClassType.Farm);
        while (!Core.CheckInventory("Diabolical Aura", quant))
        {
            PinpointthePieces(2183);
            Bot.Player.Pickup("Diabolical Aura");
            Core.Logger($"Completed x{i}");
        }
	}

	public void PinpointBow(int quantDSO, int quantCSO)
	{
        if (Core.CheckInventory("Dark Spirit Orb", quantDSO) && Core.CheckInventory("Corrupt Spirit Orb", quantDSO))
            return;
        if (!Core.CheckInventory("Necrotic Bow of the Shadow"))
            NecroticBow();

        int i = 1;
        Core.EquipClass(ClassType.Farm);
        while (!Core.CheckInventory("Dark Spirit Orb", quantDSO) && !Core.CheckInventory("Corrupt Spirit Orb", quantDSO))
        {
            PinpointthePieces(2186);
            Bot.Player.Pickup("Dark Spirit Orb", "Corrupt Spirit Orb");
            Core.Logger($"Completed x{i}");
        }
	}

	public void PinpointthePieces(int quest)
	{
        Core.EnsureAccept(quest);
        Core.HuntMonster("nulgath", "Dark Makai", "DoomKnight Armor Piece", 10);
        Core.EnsureComplete(quest);
    }
}