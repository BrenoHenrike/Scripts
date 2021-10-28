using RBot;
using RBot.Items;
using System.Collections.Generic;
using System.Linq;

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

	public void UseGoldBoost(GoldBoost boost, bool useMultiple = true) => UseBoost((int)boost, BoostType.Gold, useMultiple);
	public void UseXPBoost(XpBoosts boost, bool useMultiple = true) => UseBoost((int)boost, BoostType.Experience, useMultiple);
	public void UseClassBoost(ClassBoost boost, bool useMultiple = true) => UseBoost((int)boost, BoostType.Class, useMultiple);
	public void UseREPBoost(REPBoost boost, bool useMultiple = true) => UseBoost((int)boost, BoostType.Reputation, useMultiple);

	private void UseBoost(int boostID, BoostType type, bool useMultiple = true)
	{
		if (!Core.CheckInventory(boostID))
			return;

		if (useMultiple)
		{
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

	public void GetAllRanks()
	{
		AegisREP();
		AlchemyREP();
		ArcangroveREP();
		BaconCatREP();
		BeastMasterREP();
		BlacksmithingREP();
		BladeofAweREP();

		HollowbornREP();
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
		if (Bot.Player.IsMember)
			Core.AddDrop("Wheel of Bacon Token");
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("BaconCat") < rank)
		{
			if (!Bot.Player.IsMember)
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
		if (FactionRank("BeastMaster") >= rank || !Bot.Player.IsMember)
			return;
		Core.Logger($"Farming rank {rank}");
		int i = 1;
		while (FactionRank("BeastMaster") < rank)
		{
			Core.EnsureAccept(3757);

			Core.HuntMonster("pyramids", "Golden Scarab", "Gleaming Gems of Containment", 9);
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

public enum XpBoosts
{
	DailyXP60 = 19189,
	XP20 = 22448,
	XP60 = 27552
}

public enum ClassBoost
{
	DoomClass60 = 19761,
	Class20 = 22447,
	Class60 = 27555
}

public enum REPBoost
{

	DoomREP60 = 19762,
	REP20 = 22449,
	REP60 = 27553
}

public enum GoldBoost
{ 
	DoomGold60 = 19763,
	Gold20 = 22450,
	Gold60 = 27554
}