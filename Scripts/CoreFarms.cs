using RBot;
using RBot.Items;

public class CoreFarms
{
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core = new CoreBots();
	//CLASS Boost! (60 min) -- 27555
	//Doom CLASS Boost! (60 min) -- 19761 (can get this from /join Doom merge with Daily XP Boosts)
	//Daily Login Class Boost *20 min* -- 22447

	//REPUTATION Boost! (60 min) -- 27553
	//Doom REP Boost! (60 min) -- 19762 (can get this from /join Doom merge with Daily XP Boosts)
	//REPUTATION Boost! (20 min) -- 22449

	//GOLD Boost! (60 min) -- 27554
	//Doom GOLD Boost! (60 min) -- 19763 (can get this from /join Doom merge with Daily XP Boosts)
	//GOLD Boost! (20 min) -- 22450

	//XP Boost! (60 min) -- 27552
	//Daily XP Boost! (1 hr) -- 19189
	//XP Boost! (20 min) -- 22448

	/// <summary>
	/// Uses a boost with the given ID.
	/// </summary>
	/// <param name="boostID">ID of the Boost</param>
	/// <param name="type">Type of the Boost</param>
	/// <param name="useMultiple">Whether use more than one boost</param>
	public void UseBoost(int boostID, BoostType type, bool useMultiple = true)
	{
		if (useMultiple)
		{
			Bot.Player.UseBoost(boostID);
			Bot.RegisterHandler(30200, b =>
			{
				if (!b.Player.IsBoostActive(type))
					b.Player.UseBoost(boostID);
			}); 
		}
		else
		{
			Bot.Player.UseBoost(boostID);
		}
	}

	/// <summary>
	/// Uses a boost with one of the IDs present in <see cref="BoostIDs"/>
	/// </summary>
	/// <param name="boost">Desired Boost</param>
	/// <param name="type">Type of the Boost</param>
	/// <param name="useMultiple">Whether use more than one boost</param>
	public void UseBoost(BoostIDs boost, BoostType type, bool useMultiple = true) => UseBoost((int)boost, type, useMultiple);

	public void GetAllRanks()
	{
		AegisREP();
		AlchemyREP();
		ArcangroveREP();
		BaconCatREP();
		BeastMasterREP();
		BlacksmithingREP();
		BladeofAweREP();
		BrightoakREP();
		ChaosREP();
		DoomwoodREP();
		ElementalMasterREP();
		EmberseaREP();
		EternalREP();
		//EvilREP();
		GlaceraREP();
		//GoodREP();
		LycanREP();


		HollowbornREP();
		MysteriousDungeonREP();
		MythsongREP();
		RavenlossREP();
		SpellCraftingREP();
		VampireREP();
		YokaiREP();
	}
	
	/// <summary>
	/// Farms Gold in Battle Ground E with quests Level 46-60 and 61-75
	/// </summary>
	/// <param name="goldQuant">How much gold to farm</param>
	public void BattleGroundE(int goldQuant = 100000000)
	{
		if (Bot.Player.Gold >= goldQuant)
			return;
		Bot.Player.Join("battlegrounde");
		Core.Logger($"Farming {goldQuant}  gold");
		int i = 0;
		while (Bot.Player.Gold < goldQuant || Bot.Player.Gold <= 100000000)
		{
			Core.Logger($"Iteration {i}");
			Core.EnsureAccept(3991);
			Core.EnsureAccept(3992);
			Core.Jump("r2", "Center");
			Bot.Player.KillForItems("*", new[] { "Battleground D Opponent Defeated", "Battleground E Opponent Defeated" }, new[] { 10, 10 }, true);
			Core.EnsureComplete(3991);
			Core.EnsureComplete(3992);
			i++;
		}
		Core.Logger("Finished");
	}

	public void AegisREP(int rank = 10)
	{
		if (FactionRank("Aegis") >= rank)
			return;
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("Aegis") < rank)
		{
			Core.EnsureAccept(4900, 4910, 4914);

			Core.HuntMonster("skytower", "Seraphic Assassin", "Seraphic Assassin Dueled", 10);
			Core.HuntMonster("skytower", "Virtuous Warrior", "Warriors Dueled", 10);
			Core.HuntMonster("skytower", "Seraphic Assassin", "Assassins Handed To Them", 6);
			Core.HuntMonster("skytower", "Virtuous Warrior|Seraphic Assassin", "Warrior Butt Beaten", 6);

			Core.EnsureComplete(4900, 4910, 4914);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}
	
	public void AlchemyREP(int rank = 10, bool goldMethod = true)
	{
		if (FactionRank("Alchemy") >= rank)
			return;
		Core.AddDrop("Dragon Scale", "Ice Vapor");
		Core.Logger($"Farming rank {rank} Alchemy");
		int i = 1;
		while (FactionRank("Alchemy") < rank)
		{
			if (goldMethod)
			{
				Bot.Player.Join("alchemyacademy");
				Bot.Shops.Load(395);
				Core.SendPackets("%xt%zm%buyItem%10124%61043%395%8421%", 2);
				Core.SendPackets("%xt%zm%buyItem%10124%7132%395%8845%");
				Bot.Shops.Load(397);
				Core.SendPackets("%xt%zm%buyItem%10124%11475%397%1232%", 5);
				Core.SendPackets("%xt%zm%buyItem%10124%11478%397%1235%", 5);
				AlchemyPacket();
			}
			else
			{
				Core.KillMonster("lair", "Enter", "Spawn", "*", "Dragon Scale", 10, false);
				Core.KillMonster("lair", "Enter", "Spawn", "*", "Ice Vapor", 10, false);
				AlchemyPacket();
			}
			Core.Logger($"Iteration {i} completed");
			i++;
		}
		Core.Logger("Finished");
	}

	public void ArcangroveREP(int rank = 10)
	{
		if (FactionRank("Arcangrove") >= rank)
			return;
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("Arcangrove") < rank)
		{
			Core.EnsureAccept(794, 795, 796, 797, 798, 799, 800, 801);

			Core.HuntMonster("arcangrove", "Seed Spitter", "Spool of Arcane Thread", 10);
			Core.HuntMonster("arcangrove", "Seed Spitter", "Defeated Seed Spitter", 10);
			Core.HuntMonster("arcangrove", "Seed Spitter", "Bundle of Thyme", 10);
			Core.HuntMonster("arcangrove", "Seed Spitter", "Thistle", 5);
			Core.HuntMonster("arcangrove", "Seed Spitter", "Pretzel Root", 4);
			Core.HuntMonster("arcangrove", "Gorillaphant", "Lore-Strip Gorillaphant Steak", 8);
			Core.HuntMonster("arcangrove", "Gorillaphant", "Slain Gorillaphant", 7);
			Core.HuntMonster("arcangrove", "Gorillaphant", "Gorillaphant Tusk", 6);
			Core.HuntMonster("arcangrove", "Gorillaphant", "Batch of Mustard Seeds", 3);

			Core.EnsureComplete(794, 795, 796, 797, 798, 799, 800, 801);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}

	public void BaconCatREP(int rank = 10)
	{
		if (FactionRank("BaconCat") >= rank)
			return;
		if (Core.IsMember)
			Core.AddDrop("Wheel of Bacon Token");
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("BaconCat") < rank)
		{
			if (!Core.IsMember)
			{
				Core.EnsureAccept(5111, 5112, 5119, 5120);

				Core.HuntMonster("baconcatlair", "Cloud Shark", "Cloudy Hide", 6);
				Core.HuntMonster("baconcatlair", "Ice Cream Shark", "Moglinberry Ice Cream", 5);
				Core.HuntMonster("baconcatlair", "Ice Cream Shark", "Shark Sprinkles", 3);
				Core.HuntMonster("baconcatlair", "Cloud Shark", "Cloud Shark Farts", 3);
				Core.HuntMonster("baconcatlair", "Sketchy Shark", "College-Ruled Paper", 3);
				Core.HuntMonster("baconcatlair", "8-Bit Shark", "Great White DLC", 3);
				Core.HuntMonster("baconcatlair", "Cat Clothed Shark", "Kittarian Costumes", 3);
				Core.HuntMonster("baconcatlair", "Cat Clothed Shark", "Shark Teeth", 10);

				Core.EnsureComplete(5111, 5112, 5119, 5120); 
			}
			else
			{
				Core.EnsureAccept(5121, 5123, 5124, 5131);

				Core.HuntMonster("baconcatlair", "Robo Shark", "Wheel of Bacon Token", 5, false);
				Core.HuntMonster("baconcatlair", "Robo Shark", "Shark Legs Smashed", 10);
				Core.HuntMonster("baconcatlair", "Robo Shark", "Shark Quarters", 7);
				Core.HuntMonster("baconcatlair", "Robo Shark", "Walking Shark 1 Destroyed", 4);
				Core.HuntMonster("baconcatlair", "Robo Shark", "Walking Shark 2 Destroyed", 4);

				Core.EnsureComplete(5121, 5123, 5124, 5131);
			}
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}

	public void BeastMasterREP(int rank = 10)
	{
		if (FactionRank("BeastMaster") >= rank || !Core.IsMember)
			return;
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("BeastMaster") < rank)
		{
			Core.EnsureAccept(3757);

			Core.HuntMonster("pyramid", "Golden Scarab", "Gleaming Gems of Containment", 9);
			Core.HuntMonster("lair", "Golden Draconian", "Bright Binding of Submission", 8);

			Core.EnsureComplete(3757);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}

	public void BlacksmithingREP(int rank = 4)
	{
		if (FactionRank("Blacksmithing") >= rank)
			return;
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("Blacksmithing") < rank)
		{
			Core.EnsureAccept(2777);

			Core.HuntMonster("greenguardeast", "Wolf", "Furry Lost Sock", 2);
			Core.HuntMonster("greenguardwest", "Slime", "Slimy Lost Sock", 5);

			Core.EnsureComplete(2777);
			Core.EnsureAccept(2777);

			Core.HuntMonster("greenguardwest", "Slime", "Slimy Lost Sock", 5);
			Core.HuntMonster("greenguardeast", "Wolf", "Furry Lost Sock", 2);

			Core.EnsureComplete(2777);
			Core.Logger($"Completed x{i*2}");
			i++;
		}
		Core.Logger("Finished");
	}

	public void BladeofAweREP(int rank = 10, bool farmBoA = true)
	{
		if (FactionRank("Blade of Awe") >= rank && !farmBoA)
			return;
		if (Core.CheckInventory("Blade of Awe", toInv: false) && farmBoA)
			farmBoA = false;
		if (farmBoA)
			Core.AddDrop("Legendary Stonewrit", "Legendary Handle", "Legendary Hilt", "Legendary Blade", "Legendary Runes");
		Core.AddDrop("Stonewrit Found!", "Handle Found!", "Hilt Found!", "Blade Found!", "Runes Found!");
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		if(!Core.CheckInventory("Legendary Stonewrit", toInv: false) && !Bot.Quests.IsAvailable(2935))
		{
			Core.EnsureAccept(2933);
			Core.HuntMonster("j6", "Sketchy Dragon", "Stonewrit Found!", 1, false);
			Core.EnsureComplete(2933);
			if(farmBoA)
				Bot.Player.Pickup("Legendary Stonewrit");
			Core.Logger("Find the Stonewrit! completed");
		}
		if (!Core.CheckInventory("Legendary Handle", toInv: false) && !Bot.Quests.IsAvailable(2935))
		{
			Core.EnsureAccept(2934);
			Core.HuntMonster("gilead", "Fire Elemental|Water Elemental|Wind Elemental|Earth Elemental", "Handle Found!", 1, false);
			Core.EnsureComplete(2934);
			if(farmBoA)
				Bot.Player.Pickup("Legendary Handle");
			Core.Logger("Find the Handle! completed");
		}
		while (FactionRank("Blade of Awe") < rank)
		{
			Core.EnsureAccept(2935);
			Core.HuntMonster("castleundead", "Skeletal Viking|Skeletal Warrior", "Hilt Found!", 1, false);
			Core.EnsureComplete(2935);
			if (farmBoA && !Core.CheckInventory("Legendary Hilt", toInv: false))
				Bot.Player.Pickup("Legendary Hilt");
			Core.Logger($"Completed Find the Hilt! x{i}");
			i++;
		}
		if (farmBoA)
		{
			if (!Core.CheckInventory("Legendary Blade", toInv: false))
			{
				Core.EnsureAccept(2933);
				Core.HuntMonster("hydra", "Hydra Head", "Blade Found!", 1, false);
				Core.EnsureComplete(2933);
				Bot.Player.Pickup("Legendary Blade");
				Core.Logger("Find the Blade! completed");
			}
			if (!Core.CheckInventory("Legendary Runes", toInv: false))
			{
				Core.EnsureAccept(2933);
				Core.KillEscherion("Runes Found!");
				Core.EnsureComplete(2933);
				Bot.Player.Pickup("Legendary Runes");
				Core.Logger("Find the Runes! completed");
			}
			Core.Unbank("Legendary Stonewrit", "Legendary Handle", "Legendary Hilt", "Legendary Blade", "Legendary Runes");
			Core.Logger("You can now merge the Blade of Awe at /join museum");
		}
		Core.Logger("Finished");
	}

	public void BrightoakREP(int rank = 10)
	{
		if (FactionRank("Brightoak") >= rank)
			return;
		if (!Bot.Quests.IsAvailable(4667))
		{
			Core.Logger("Quest not avaible for farm, do Brightoak saga till Elfhame [Unlocking the Guardian's Mouth]");
			return;
		}
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		Bot.Player.Join("elfhame");
		while (FactionRank("Brightoak") < rank)
		{
			Core.EnsureAccept(4667);
			Bot.Map.GetMapItem(3984);
			Core.EnsureComplete(4667);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}

	public void ChaosREP(int rank = 10)
	{
		if (FactionRank("Chaos") >= rank)
			return;
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("Chaos") < rank)
		{
			Core.EnsureAccept(3594);
			Core.KillMonster("mountdoomskull", "b1", "Left", "*", "Chaos Power Increased", 6);
			Core.EnsureComplete(3594);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}
	
	public void DoomwoodREP(int rank = 10)
	{
		if (FactionRank("Doomwood") >= rank)
			return;
		if (Core.IsMember)
			Core.AddDrop("Dark Tower Sword", "Light Tower Sword");
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("Doomwood") < rank)
		{
			if (!Core.IsMember)
			{
				Core.EnsureAccept(1151);
				Core.EnsureAccept(1152);
				Core.EnsureAccept(1153);
				Core.HuntMonster("shadowfallwar", "*", "To Do List of Doom");
				Core.HuntMonster("shadowfallwar", "*", "Skeleton Key");
				if (Bot.Quests.CanComplete(1151))
					Core.EnsureComplete(1151);
				Core.EnsureComplete(1152);
				Core.EnsureComplete(1153); 
			}
			else
			{
				if(Core.CheckInventory("Light Tower Sword"))
				{
					Core.EnsureAccept(2100);
					Core.HuntMonster("battleunderb", "Skeleton Warrior", "Battered Dark Tower Sword");
					Core.EnsureComplete(2100);
					Bot.Player.Pickup("Dark Tower Sword");
					Core.EnsureAccept(2101);
					Core.HuntMonster("doomwar", "Bronze DracoZombie", "Dracozombies' Spirits", 13);
					Core.EnsureComplete(2101);
					Bot.Player.Pickup("Dark Tower Sword");
				}
				Core.EnsureAccept(2102);
				Core.HuntMonster("doomwar", "Dark DracoZombie", "Bones of the Dracozombie");
				Core.EnsureComplete(2102);
			}
			Core.Logger($"Completed x{i}");
			i++;
		}
		if (Core.IsMember)
			Bot.Shops.SellItem("Light Tower Sword");
		Core.Logger("Finished");
	}

	public void ElementalMasterREP(int rank = 10)
	{
		if (FactionRank("Elemental Master") >= rank)
			return;
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("Elemental Master") < rank)
		{
			Core.EnsureAccept(3050);
			Core.EnsureAccept(3298);
			Core.HuntMonster("gilead", "Water Elemental", "Water Core");
			Core.HuntMonster("gilead", "Fire Elemental", "Fire Core");
			Core.HuntMonster("gilead", "Wind Elemental", "Air Core");
			Core.HuntMonster("gilead", "Earth Elemental", "Earth Core");
			Core.HuntMonster("gilead", "Mana Elemental", "Mana Core");
			Core.EnsureComplete(3050);
			if(Bot.Quests.CanComplete(3298))
				Core.EnsureComplete(3298);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}

	public void EmberseaREP(int rank = 10)
	{
		if (FactionRank("Embersea") >= rank)
			return;
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("Embersea") < rank)
		{
			Core.EnsureAccept(4228);
			//Core.EnsureAccept(4224);
			Core.HuntMonster("fireforge", "Blazebinder", "Defeated Blazebinder", 5);
			//Core.HuntMonster("fireforge", "Blazebinder", "Blazebinder defeated", 2);
			//Core.EnsureComplete(4224);
			Core.EnsureComplete(4228);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}

	public void EternalREP(int rank = 10)
	{
		if (FactionRank("Eternal") >= rank)
			return;
		if (!Bot.Quests.IsAvailable(5198))
		{
			Core.Logger("Can't do farming quest [Sphynxes are Riddled with Gems] (/fourdpyramid)");
			return;
		}
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("Eternal") < rank)
		{
			Core.EnsureAccept(5198);
			Core.KillMonster("fourdpyramid", "r11", "Right", 2908, "White Gem", 2);
			Core.KillMonster("fourdpyramid", "r11", "Right", 2909, "Black Gem", 2);
			Core.EnsureComplete(5198);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}
	
	public void EvilREP(int rank = 10)
	{
		if (FactionRank("Evil") >= rank)
			return;
		Core.Logger("This needs the player to be aligned to evil");
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while(FactionRank("Evil") < 4)
		{
			Core.EnsureAccept(364);
			Core.HuntMonster("newbie", "Slime", "Youthanize");
			Core.EnsureComplete(364);
			Core.Logger($"Completed x{i}");
			i++;
		}
		while (FactionRank("Evil") < rank)
		{
			if (!Core.IsMember)
			{
				Core.EnsureAccept(367);
				Core.HuntMonster("castleundead", "*", "Replacement Tibia", 6);
				Core.HuntMonster("castleundead", "*", "Phalanges", 3);
				Core.EnsureComplete(367); 
			}
			else
			{
				Core.EnsureAccept(366);
				Core.HuntMonster("sleuthhound", "Chair", "Chair", 4);
				Core.HuntMonster("sleuthhound", "Table", "Table", 2);
				Core.HuntMonster("sleuthhound", "Bookcase", "Bookcase");
				Core.EnsureComplete(366);
			}
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}

	public void GlaceraREP(int rank = 10)
	{
		if (FactionRank("Glacera") >= rank)
			return;
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("Glacera") < rank)
		{
			Core.EnsureAccept(5597);
			Core.EnsureAccept(5598);
			Core.EnsureAccept(5599);
			Core.EnsureAccept(5600);
			Core.KillMonster("icewindwar", "r2", "Left", "*", "World Ender Medal", 10);
			Core.EnsureComplete(5599);
			if (Bot.Quests.CanComplete(5600))
				Core.EnsureComplete(5600);
			if (Bot.Quests.CanComplete(5598))
				Core.EnsureComplete(5598);
			if(Bot.Quests.CanComplete(5597))
				Core.EnsureComplete(5597);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}
	
	public void GoodREP(int rank = 10)
	{
		if (FactionRank("Good") >= rank)
			return;
		Core.Logger("This needs the player to be aligned to good");
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while(FactionRank("Good") < 4)
		{
			Core.EnsureAccept(369);
			Core.HuntMonster("swordhavenbridge", "Slime", "Slime in a Jar", 6);
			Core.EnsureComplete(369);
			Core.Logger($"Completed x{i}");
			i++;
		}
		while (FactionRank("Good") < rank)
		{
			if (!Core.IsMember)
			{
				Core.EnsureAccept(372);
				Core.HuntMonster("castleundead", "*", "Chaorrupted Skull", 5);
				Core.EnsureComplete(372); 
			}
			else
			{
				Core.EnsureAccept(371);
				Core.HuntMonster("sewer", "Grumble", "Grumble's Fang", 5);
				Core.EnsureComplete(371);
			}
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}

	public void LycanREP(int rank = 10)
	{
		if (FactionRank("Lycan") >= rank)
			return;
		if (!Bot.Quests.IsAvailable(537))
		{
			Core.Logger("Can't do farming quest [Sanguine] (/lycan)");
			return;
		}
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("Lycan") < rank)
		{
			Core.EnsureAccept(537);
			Core.HuntMonster("sanguine", "Sanguine", "Sanguine Mask");
			Core.EnsureComplete(537);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}
	
	public void HollowbornREP(int rank = 10)
	{
		if (FactionRank("Hollowborn") >= rank)
			return;
		Core.AddDrop("Hollow Soul");
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("Hollowborn") < rank)
		{
			Core.EnsureAccept(7553);
			Core.EnsureAccept(7555);
			Core.KillMonster("shadowrealm", "r2", "Down", "*", "Darkseed", 8);
			Core.KillMonster("shadowrealm", "r2", "Down", "*", "Shadow Medallion", 5);
			Core.EnsureComplete(7553);
			Core.EnsureComplete(7555);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}

	public void MysteriousDungeonREP(int rank = 10)
	{
		if (FactionRank("Mysterious Dungeon") >= rank)
			return;
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		if (!Bot.Quests.IsAvailable(5429))
		{
			Bot.Player.Join("cursedshop");
			Core.EnsureAccept(5428);
			Bot.Map.GetMapItem(4803);
			Bot.Sleep(2500);
			if(Bot.Quests.CanComplete(5428))
				Core.EnsureComplete(5428);
			Bot.Player.Jump("Enter", "Spawn");
		}
		while (FactionRank("Mysterious Dungeon") < rank)
		{
			Core.EnsureAccept(5429);
			Core.HuntMonster("cursedshop", "Antique Chair", "Antique Chair Defeated");
			Core.EnsureComplete(5429);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}

	public void MythsongREP(int rank = 10)
	{
		if (FactionRank("Mythsong") >= rank)
			return;
		if (!Bot.Quests.IsAvailable(710))
		{
			Core.Logger("Can't do farming quest [Kimberly] (/palooza)");
			return;
		}
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("Mythsong") < rank)
		{
			Core.EnsureAccept(710);
			Core.HuntMonster("palooza", "Kimberly", "Kimberly Defeated");
			Core.EnsureComplete(710);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}

	public void RavenlossREP(int rank = 10)
	{
		if (FactionRank("Ravenloss") >= rank)
			return;
		if (!Bot.Quests.IsAvailable(3445))
		{
			Core.Logger("Can't do farming quest [Slay the Spiderkin] (/twilightedge)");
			return;
		}
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("Ravenloss") < rank)
		{
			Core.EnsureAccept(3445);
			Core.HuntMonster("twilightedge", "ChaosWeaver Mage", "ChaosWeaver Slain", 10);
			Core.EnsureComplete(3445);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}
	
	public void SpellCraftingREP(int rank = 10)
	{
		if (FactionRank("SpellCrafting") >= rank)
			return;
		Core.AddDrop("Mystic Quills", "Mystic Parchment");
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		if (Bot.Quests.IsAvailable(2260))
		{
			Core.EnsureAccept(2260);
			Bot.Player.Join("dragonrune");
			Bot.Map.GetMapItem(1920);
			Core.HuntMonster("castleundead", "Skeletal Warrior", "Arcane Parchment", 13);
			Core.EnsureComplete(2260);
			Core.Logger("SpellCrafting now unlocked");
		}
		if(FactionRank("SpellCrafting") < 4)
		{
			Core.CheckInventory("Mystic Quills");
			Core.HuntMonster("mobius", "Slugfit", "Mystic Quills", 10, false);
			Bot.Shops.Load(549);
			Core.SendPackets("%xt%zm%buyItem%30190%13280%549%1633%", 10);
			Bot.Player.Join("spellcraft");
			while (FactionRank("SpellCrafting") < 4)
			{
				Bot.SendPacket("%xt%zm%crafting%1%spellOnStart%1%1555%Spell%");
				Bot.Sleep(3000);
				Bot.SendPacket("%xt%zm%crafting%1%spellComplete%1%2299%Ssikari's Breath%");
				Core.Logger($"Completed x{i}");
				Bot.Sleep(3000);
			}
		}
		while (FactionRank("SpellCrafting") < rank)
		{
			if(!Core.CheckInventory("Mystic Parchment"))
				Core.HuntMonster("underworld", "Skull Warrior", "Mystic Parchment", 10, false);
			Bot.Player.Join("dragonrune");
			Bot.Shops.Load(549);
			Core.SendPackets("%xt%zm%buyItem%30932%13285%549%1638%", 10);
			Bot.Player.Join("spellcraft");
			while(Core.CheckInventory("Hallow Ink"))
			{
				Bot.SendClientPacket("%xt%zm%crafting%1%spellOnStart%6%1555%Spell%");
				Bot.Sleep(3000);
				Bot.SendPacket("%xt%zm%crafting%1%spellComplete%6%2322%Plague Flare%");
				Bot.Sleep(3000);
				Core.Logger($"Completed x{i}");
				i++;
			}
		}
		Core.Logger("Finished");
	}

	public void VampireREP(int rank = 10)
	{
		if (FactionRank("Vampire") >= rank)
			return;
		if (!Bot.Quests.IsAvailable(522))
		{
			Core.Logger("Can't do farming quest [Twisted Paw] (/safiria)");
			return;
		}
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("Vampire") < rank)
		{
			Core.EnsureAccept(522);
			Core.HuntMonster("safiria", "Twisted Paw", "Twisted Paw's Head");
			Core.EnsureComplete(522);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}
	
	public void YokaiREP(int rank = 10)
	{
		if (FactionRank("Yokai") >= rank)
			return;
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("Yokai") < rank)
		{
			Core.EnsureAccept(383);
			Core.KillMonster("dragonkoiz-111111", "t1", "Left", "Pockey Chew", "Piece of Pockey", 3);
			Core.EnsureComplete(383);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}

	private void AlchemyPacket()
	{
		int i = 1;
		Bot.Player.Join("alchemy");
		while (Core.CheckInventory("Dragon Scale") && Core.CheckInventory("Ice Vapor"))
		{
			Bot.SendPacket("%xt%zm%crafting%1%getAlchWait%11475%11478%false%Ready to Mix%Dragon Scale%Ice Vapor%Gebo%Moose%");
			Bot.Sleep(15000);
			Bot.SendPacket("%xt%zm%crafting%1%checkAlchComplete%11475%11478%false%Mix Complete%Dragon Scale%Ice Vapor%Gebo%Moose%");
			Bot.Sleep(700);
			Core.Logger($"Completed alchemy x{i}");
			i++;
		}
	}

	public int FactionRank(string faction) => Bot.Player.GetFactionRank(faction);
}

public enum BoostIDs
{
	DailyXP60 = 19189,
	XP20 = 22448,
	XP60 = 27552,
	DoomClass60 = 19761,
	Class20 = 22447,
	Class60 = 27555,
	DoomREP60 = 19762,
	REP20 = 22449,
	REP60 = 27553,
	DoomGold60 = 19763,
	Gold20 = 22450,
	Gold60 = 27554

}