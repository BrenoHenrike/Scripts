using RBot;
using System.Linq;

public class CoreNulgath
{
	public ScriptInterface Bot => ScriptInterface.Instance;

	public CoreBots Core = new CoreBots();
	public CoreFarms Farm = new CoreFarms();

	/// <summary>
	/// Crag & Bamboozle name in game
	/// </summary>
	public string CragName => "Crag &amp; Bamboozle";

	/// <summary>
	/// All principal drops from Nulgath
	/// </summary>
	public string[] bagDrops = 
	{
		"Blood Gem of the Archfiend",
		"Dark Crystal Shard",
		"Diamond of Nulgath",
		"Essence of Nulgath",
		"Fiend Token",
		"Totem of Nulgath",
		"Gem of Nulgath",
		"Tainted Gem",
		"Unidentified 10",
		"Unidentified 13",
		"Unidentified 34",
		"Voucher of Nulgath",
		"Voucher of Nulgath (non-mem)",
		"Emblem of Nulgath",
		"Receipt of Swindle",
		"Nulgath's Approval",
		"Archfiend's Favor"
	};

	/// <summary>
	/// Drops from the bosses that used to give acess to tercess
	/// </summary>
	public string[] tercessBags =
	{
		"Aracara's Fang",
		"Defeated Makai",
		"Escherion's Chain",
		"Hydra Scale",
		"O-dokuro's Tooth",
		"Strand of Vath's Hair"
	};

	/// <summary>
	/// Drops from the VHL Challenge quest
	/// </summary>
	public string[] VHLDrops =
	{
		"Hadean Onyx of Nulgath",
		"Black Knight Orb",
		"Dwakel Decoder",
		"Nulgath Shaped Chocolate",
		"The Secret 1",
		"Elder's Blood",
		"Aelita's Emerald",
		"Elemental Ink",
		"Bone Dust",
		"Emblem of Nulgath",
		"Void Highlord Armor",
		"Helm of the Highlord",
		"Roentgenium of Nulgath"
	};

	/// <summary>
	/// List of Betrayal Blades
	/// </summary>
	public string[] betrayalBlades =
	{
		"1st Betrayal Blade of Nulgath",
		"2nd Betrayal Blade of Nulgath",
		"3rd Betrayal Blade of Nulgath",
		"4th Betrayal Blade of Nulgath",
		"5th Betrayal Blade of Nulgath",
		"6th Betrayal Blade of Nulgath",
		"7th Betrayal Blade of Nulgath",
		"8th Betrayal Blade of Nulgath"
	};

	/// <summary>
	/// Shadow Blast Arena medals
	/// </summary>
	public string[] nationMedals =
	{
		"Nation Round 1 Medal",
		"Nation Round 2 Medal",
		"Nation Round 3 Medal",
		"Nation Round 4 Medal"
	};

	/// <summary>
	/// Joins Tercessuinotlim
	/// </summary>
	public void JoinTercessuinotlim()
	{
		if (Bot.Map.Name == "tercessuinotlim")
			return;
		Bot.Player.Join("citadel", "m22", "Left");
		if (Bot.Player.Cell != "m22")
			Bot.Player.Jump("m22", "Left");
		Bot.Player.Join("tercessuinotlim");
		Bot.Wait.ForMapLoad("tercessuinotlim");
	}

	/// <summary>
	/// Does Essence of Defeat Reagent quest for Dark Crystal Shards
	/// </summary>
	/// <param name="quant">Desired quantity, 1000 = max stack</param>
	public void EssenceofDefeatReagent(int quant = 1000)
	{
		if (Core.CheckInventory("Dark Crystal Shard", quant))
			return;
		Core.AddDrop(tercessBags);
		int i = 1;
		Core.Logger($"Farming {quant} Dark Crystal Shard");
		while (!Bot.Inventory.Contains("Dark Crystal Shard", quant))
		{
			Core.EnsureAccept(570);
			Core.HuntMonster("faerie", "Aracara", "Aracara's Fang", 1, false);
			Core.HuntMonster("hydra", "Hydra Head", "Hydra Scale", 1, false);
			if (!Core.CheckInventory("Strand of Vath's Hair"))
			{
				Bot.Player.Join("stalagbite");
				Core.Jump("r2", "Left");
				Bot.Player.Kill("Vath");
				Core.JumpWait();
				if (Bot.Player.DropExists("Strand of Vath's Hair"))
					Bot.Player.Pickup("Strand of Vath's Hair");
			}
			Core.HuntMonster("yokaiwar", "O-Dokuro's Head", "O-dokuro's Tooth", 1, false);
			Core.KillEscherion("Escherion's Chain", removeHandler: false);
			if (!Core.CheckInventory("Defeated Makai", 50))
			{
				JoinTercessuinotlim();
				Core.Jump("m2", "Bottom");
				Bot.Player.KillForItem("Dark Makai", "Defeated Makai", 50);
				Core.JumpWait();
			}
			Core.HuntMonster("djinn", "Tibicenas", "Tibicenas' Chain");
			Core.EnsureComplete(570);
			Bot.Wait.ForPickup("Dark Crystal Shard");
			Core.Logger($"Completed x{i}");
			i++;
		}
		Bot.Handlers.RemoveAll(h => h.Name == "escherion");
		Core.Logger("Finished");
	}

	/// <summary>
	/// Does NWNO from Nulgath's Birthday Gift/Bounty Hunter's Drone Pet
	/// </summary>
	/// <param name="item">Desired item to get</param>
	/// <param name="quant">Desired quantity to get</param>
	public void NewWorldsNewOpportunities(string item = "Any", int quant = 1)
	{
		if (Core.CheckInventory(item, quant) || (!Core.CheckInventory("Nulgath's Birthday Gift") && !Core.CheckInventory("Bounty Hunter's Drone Pet")))
			return;
		Core.Logger($"Farming for {item}({quant})");
		int i = 1;
		while (!Bot.Inventory.Contains(item, quant))
		{
			if (Bot.Inventory.Contains(item, quant))
				break;
			if (Bot.Inventory.Contains("Bounty Hunter's Drone Pet"))
				Core.EnsureAccept(6183);
			else
				Core.EnsureAccept(6697);
			Core.HuntMonster("mobius", "Slugfit", "Slugfit Horn", 5);
			JoinTercessuinotlim();
			Core.HuntMonster("tercessuinotlim", "Dark Makai", "Makai Fang", 5);
			Core.HuntMonster("hydra", "Fire Imp", "Imp Flame", 3);
			Core.HuntMonster("faerie", "Cyclops Warlord", "Cyclops Horn", 3);
			Core.HuntMonster("greenguardwest", "Big Bad Boar", "Wereboar Tusk", 2);
			if (Bot.Inventory.Contains("Bounty Hunter's Drone Pet"))
				Core.EnsureComplete(6183);
			else
				Core.EnsureComplete(6697);
			Bot.Wait.ForPickup("Diamond of Nulgath");
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}

	/// <summary>
	/// Farm Diamonds from Evil War Nul quests (does Member one if possible)
	/// </summary>
	/// <param name="quant">Desired quantity, 1000 = max stack</param>
	public void DiamondEvilWar(int quant = 1000)
	{
		if (Core.CheckInventory("Diamond of Nulgath", quant))
			return;
		Core.AddDrop("Legion Blade", "Dessicated Heart");
		Core.Logger($"Farming {quant} Diamonds");
		int i = 1;
		Bot.Player.Join("evilwarnul");
		while (!Bot.Inventory.Contains("Diamond of Nulgath", quant))
		{
			if (Core.IsMember)
				Core.EnsureAccept(2221);
			else
				Core.EnsureAccept(2219);
			Core.Jump("r2", "Down");
			Bot.Player.KillForItems("*", new[] { "Legion Blade", "Dessicated Heart" }, new[] { 1, 22 });
			Bot.Player.KillForItems("*", new[] { "Legion Helm", "Undead Skull", "Legion Champion Medal" }, new[] { 5, 3, 5 }, true);
			if (Core.IsMember)
				Core.EnsureComplete(2221);
			else
				Core.EnsureComplete(2219);
			Bot.Wait.ForPickup("Diamond of Nulgath");
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}

	/// <summary>
	/// Farms Approvals and Favors in Evil War Nul
	/// </summary>
	/// <param name="quantApproval">Desired quantity for Approvals, 5000 = max stack</param>
	/// <param name="quantFavor">Desired quantity for Favors, 5000 = max stack</param>
	public void ApprovalAndFavor(int quantApproval = 5000, int quantFavor = 5000)
	{
		if (Core.CheckInventory("Nulgath's Approval", quantApproval) && Core.CheckInventory("Archfiend's Favor", quantFavor))
			return;
		Core.Logger($"Farming {quantApproval} Nulgath's Approval and {quantFavor} Archfiend's Favor");
		if (quantApproval > 0)
			Core.KillMonster("evilwarnul", "r2", "Down", "*", "Nulgath's Approval", quantApproval, false);
		if (quantFavor > 0)
			Core.KillMonster("evilwarnul", "r2", "Down", "*", "Archfiend's Favor", quantFavor, false);
		Core.Logger($"Finished");
	}

	/// <summary>
	/// Farms Tainted Gem with Swindle Bulk quest
	/// </summary>
	/// <param name="quant">Desired quantity, 1000 = max stack</param>
	public void SwindleBulk(int quant = 1000)
	{
		if(Core.CheckInventory("Tainted Gem", quant))
			return;
		Core.Logger($"Farming {quant} Tainted Gems");
		int i = 1;
		Core.AddDrop("Cubes");
		while (!Bot.Inventory.Contains("Tainted Gem", quant))
		{
			Core.EnsureAccept(7817);
			Core.KillMonster("boxes-111111", "Fort2", "Left", "*", "Cubes", 500, false);
			Core.KillMonster("mountfrost-111111", "War", "Left", "Snow Golem", "Ice Cubes", 6);
			Core.EnsureComplete(7817);
			Bot.Wait.ForPickup("Tainted Gem");
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger("Finished");
	}

	/// <summary>
	/// Farms Emblem of Nulgath in Shadow Blast Arena
	/// </summary>
	/// <param name="quant">Desired quantity, 500 = max stack</param>
	public void EmblemofNulgath(int quant = 500)
	{
		if (Core.CheckInventory("Emblem of Nulgath", quant))
			return;
		if (!Core.CheckInventory("Nation Round 4 Medal"))
			NationRound4Medal();

		Core.AddDrop("Fiend Seal", "Gem of Domination");
		Core.CheckInventory("Field Seal");
		Core.CheckInventory("Gem of Domination");
		Core.Logger($"Farming {quant} Emblems");
		Bot.Player.Join("shadowblast");
		int i = 1;
		while (!Bot.Inventory.Contains("Emblem of Nulgath", quant))
		{
			Core.EnsureAccept(4748);
			Core.Jump("r13", "Left");
			Bot.Player.KillForItem("*", "Gem of Domination", 1);
			Bot.Player.KillForItem("*", "Fiend Seal", 27);
			Core.EnsureComplete(4748);
			Bot.Wait.ForPickup("Emblem of Nulgath");
			Core.Logger($"Completed x{i}");
			i++;
		}
		Core.Logger($"Finished");
	}

	/// <summary>
	/// Farms Nation Round 4 Medal in Shadow Blast Arena
	/// </summary>
	public void NationRound4Medal()
	{
		Core.AddDrop(nationMedals);
		Core.Logger("Farming Nation Round 4 Medal");
		Bot.Player.Join("shadowblast");
		while (!Bot.Inventory.Contains("Nation Round 4 Medal"))
		{
			if (!Core.CheckInventory("Nation Round 1 Medal") &&
				!Core.CheckInventory("Nation Round 2 Medal") &&
				!Core.CheckInventory("Nation Round 3 Medal"))
			{
				Core.EnsureAccept(4744);
				Bot.Player.HuntForItem("Legion Airstrike", "Legion Rookie Defeated", 5, true);
				Bot.Player.HuntForItem("Shadowrise Guard", "Shadowscythe Rookie Defeated", 5, true);
				Core.EnsureComplete(4744);
				Bot.Wait.ForDrop("Nation Round 1 Medal");
				Core.Logger("Medal 1 acquired");
			}

			if (Core.CheckInventory("Nation Round 1 Medal"))
			{
				Core.EnsureAccept(4745);
				Bot.Player.HuntForItem("Legion Fenrir", "Legion Veteran Defeated", 7, true);
				Bot.Player.HuntForItem("Doombringer", "Shadowscythe Veteran Defeated", 7, true);
				Core.EnsureComplete(4745);
				Bot.Wait.ForDrop("Nation Round 2 Medal");
				Core.Logger("Medal 2 acquired");
			}

			if (Core.CheckInventory("Nation Round 2 Medal"))
			{
				Core.EnsureAccept(4746);
				Bot.Player.HuntForItem("Legion Cannon", "Legion Elite Defeated", 10, true);
				Bot.Player.HuntForItem("Draconic Doomknight", "Shadowscythe Elite Defeated", 10, true);
				Core.EnsureComplete(4746);
				Bot.Wait.ForDrop("Nation Round 3 Medal");
				Core.Logger("Medal 3 acquired");
			}

			if (Core.CheckInventory("Nation Round 3 Medal"))
			{
				Core.EnsureAccept(4747);
				Bot.Player.HuntForItem("Grimlord Boss", "Grimlord Vanquished", 1, true);
				Core.EnsureComplete(4747);
				Bot.Wait.ForDrop("Nation Round 4 Medal");
				Core.Logger("Medal 4 acquired");
			}
		}
	}

	/// <summary>
	/// Farms Totem of Nulgath/Gem of Nulgath with Voucher Item: Totem of Nulgath quest
	/// </summary>
	/// <param name="reward">Which reward to pick (totem or gem)</param>
	public void VoucherItemTotemofNulgath(ChooseReward reward)
	{
		Core.Logger($"Reward selected: {reward}");
		Core.EnsureAccept(4778);
		EssenceofNulgath();
		if (!Bot.Quests.CanComplete(4778))
			EssenceofNulgath(65);
		switch (reward)
		{
			case ChooseReward.GemofNulgath:
				Core.EnsureComplete(4778, 6136);
				break;
			default:
				Core.EnsureComplete(4778, 5357);
				break;
		}
		Core.Logger("Finished");
	}

	/// <summary>
	/// Farms Essences of Nulgath from Dark Makais in Tercessuinotlim
	/// </summary>
	/// <param name="quant">Desired quantity, 100 = max stack</param>
	public void EssenceofNulgath(int quant = 60)
	{
		if (Core.CheckInventory("Essence of Nulgath", quant))
			return;
		JoinTercessuinotlim();
		Core.Jump("m2", "Bottom");
		Core.Logger($"Farming {quant} Essences");
		Bot.Player.KillForItem("Dark Makai", "Essence of Nulgath", quant);
		Core.Logger("Finished");
	}

	/// <summary>
	/// Does Nulgath Larvae quest for the desired item
	/// </summary>
	/// <param name="item">Desired item name</param>
	/// <param name="quant">Desired item quantity</param>
	public void NulgathLarvae(string item = "Any", int quant = 1)
	{
		if (Core.CheckInventory(item, quant))
			return;
		Core.AddDrop("Mana Energy for Nulgath");
		int i = 1;
		Core.Logger($"Farming {quant} {item}");
		while(!Bot.Inventory.Contains(item, quant))
		{
			Core.EnsureAccept(2566);
			Core.HuntMonster("elemental", "Mana Golem", "Mana Energy for Nulgath", 10, false);
			while(Bot.Inventory.Contains("Mana Energy for Nulgath"))
			{
				Core.EnsureAccept(2566);
				Core.HuntMonster("elemental", "Mana Imp|Mana Falcon", "Charged Mana Energy for Nulgath", 5);
				Core.EnsureComplete(2566);
				Bot.Sleep(Core.ActionDelay);
				Core.Logger($"Completed x{i}");
				i++;
			}
		}
		Core.Logger("Finished");
	}

	/// <summary>
	/// Does Supplies to Spin the Whell of Chance for the desired item
	/// </summary>
	/// <param name="item">Desired item name</param>
	/// <param name="quant">Desired item quantity</param>
	public void Supplies(string item = "Any", int quant = 1)
	{
		if (Core.CheckInventory(item, quant))
			return;
		Core.AddDrop("Escherion's Helm");
		Core.CheckInventory("Escherion's Helm");
		Core.Logger($"Farming {quant} {item}");
		int i = 1;
		while(!Bot.Inventory.Contains(item, quant))
		{
			Core.EnsureAccept(2857);
			Core.KillEscherion("Escherion's Helm", removeHandler: false);
			Core.EnsureComplete(2857);
			Core.Logger($"Completed x{i}");
			i++;
		}
		Bot.Handlers.RemoveAll(h => h.Name == "escherion");
	}

	/// <summary>
	/// Does The Assistant quest for the desired item
	/// </summary>
	/// <param name="item">Desired item name</param>
	/// <param name="quant">Desired item quantity</param>
	/// <param name="farmGold"></param>
	public void TheAssistant(string item = "Any", int quant = 1, bool farmGold = true)
	{
		if (Core.CheckInventory(item, quant))
			return;
		int i = 1;
		Core.Logger($"Farming {quant} {item}");
		if (farmGold)
		{
			while (!Bot.Inventory.Contains(item, quant))
			{
				TheAssistantLoop(item, ref i, quant);
				if(Bot.Player.Gold < 100000)
					Farm.BattleGroundE(10000000);
			}
		}
		else
		{
			while (Bot.Player.Gold > 100000)
				TheAssistantLoop(item, ref i, quant);

			if (Bot.Inventory.Contains(item, quant))
				Core.Logger($"Couldn't get {item}({quant})");
		}
		Core.Logger("Finished");
	}

	private void TheAssistantLoop(string item, ref int i, int quant = 1)
	{
		if (!Core.CheckInventory("War-Torn Memorabilia"))
		{
			Bot.Player.Join("yulgar");
			while (Bot.Player.Gold >= 100000 && !Bot.Inventory.Contains("War-Torn Memorabilia", 5))
			{
				Bot.Shops.BuyItem(41, "War-Torn Memorabilia");
				Bot.Sleep(Core.ActionDelay);
			}
		}
		Core.EnsureAccept(2859);
		while (Bot.Inventory.Contains("War-Torn Memorabilia") && !Bot.Inventory.Contains(item, quant))
		{
			Core.ChainComplete(2859);
			Bot.Player.Pickup(bagDrops);
			Core.Logger($"Completed x{i}");
			i++;
		}
	}

	/// <summary>
	/// Does Bamblooze vs Drudgen quest for the desired item
	/// </summary>
	/// <param name="item">Desired item name</param>
	/// <param name="quant">Desired item quantity</param>
	public void BambloozevsDrudgen(string item = "Any", int quant = 1)
	{
		if (!Core.CheckInventory(CragName) || Core.CheckInventory(item, quant))
			return;
		Core.AddDrop("Escherion's Helm", "Tainted Core");
		int i = 1;
		Core.Logger($"Farming {quant} {item}");
		while (!Bot.Inventory.Contains(item, quant))
		{
			Core.EnsureAccept(2857);
			Core.EnsureAccept(609);
			Core.KillMonster("evilmarsh", "End", "Left", "Tainted Elemental", "Tainted Core", 10, false);
			while (Bot.Inventory.Contains("Tainted Core"))
			{
				Core.EnsureComplete(609);
				Bot.Wait.ForDrop("Escherion's Helm");
				Bot.Player.Pickup("Escherion's Helm");
				Core.EnsureComplete(2857);
				Bot.Sleep(Core.ActionDelay);
				Core.EnsureAccept(2857);
				Bot.Sleep(Core.ActionDelay);
				Core.EnsureAccept(609);
				Bot.Sleep(Core.ActionDelay);
				Core.Logger($"Completed x{i}");
				i++;
			}
		}
		Core.Logger("Finished");
	}

	/// <summary>
	/// Do Diamond Exchange quest 1 time, if farmDiamond is true, will farm 15 Diamonds before if needed
	/// </summary>
	/// <param name="farmDiamond">Whether or not farm Diamonds</param>
	public void DiamondExchange(bool farmDiamond = true)
	{
		if ((!Core.CheckInventory("Diamond of Nulgath", 15) && !farmDiamond) || !Core.CheckInventory(CragName))
			return;
		if (farmDiamond)
			BambloozevsDrudgen("Diamond of Nulgath", 15);
		Core.EnsureAccept(869);
		Core.KillMonster("evilmarsh", "Field1", "Left", "Dark Makai");
		Core.EnsureComplete(869);
		Core.Logger("Completed");
	}

	/// <summary>
	/// Do Contract Exchange quest 1 time, if <paramref name="farmUni13"/> is true, will farm Uni 13 before if needed
	/// </summary>
	/// <param name="reward">Desired reward</param>
	/// <param name="farmUni13">Whether or not farm Uni 13</param>
	public void ContractExchange(ChooseReward reward, bool farmUni13 = true)
	{
		if ((!Core.CheckInventory("Unidentified 13") && !farmUni13) || !Core.CheckInventory("Drudgen the Assistant"))
			return;
		if (farmUni13 && !Bot.Inventory.Contains("Unidentified 13"))
			FarmUni13();
		Core.EnsureAccept(870);
		JoinTercessuinotlim();
		Core.Jump("m4", "Right");
		Bot.Player.KillForItem("Shadow of Nulgath", "Blade Master Rune", 1, true);
		switch (reward)
		{
			case ChooseReward.TaintedGem:
				Core.EnsureComplete(870, 4769);
				Bot.Player.Pickup("Tainted Gem");
				break;
			case ChooseReward.DarkCrystalShard:
				Core.EnsureComplete(870, 4770);
				Bot.Player.Pickup("Dark Crystal Shard");
				break;
			case ChooseReward.DiamondofNulgath:
				Core.EnsureComplete(870, 4771);
				Bot.Player.Pickup("Diamond of Nulgath");
				break;
			case ChooseReward.GemofNulgath:
				Core.EnsureComplete(870, 6136);
				Bot.Player.Pickup("Gem of Nulgath");
				break;
			case ChooseReward.BloodGemoftheArchfiend:
				Core.EnsureComplete(870, 22332);
				Bot.Player.Pickup("Blood Gem of The Archfiend");
				break;
			default:
				Core.EnsureComplete(870, 4771);
				break;
		}
		Core.Logger($"Exchanged for {reward}");
	}
	
	/// <summary>
	/// Farms Unidentified 13 with the best method avaible
	/// </summary>
	/// <param name="quant">Desired quantity, 13 = max stack</param>
	public void FarmUni13(int quant = 1)
	{
		if (Core.CheckInventory("Unidentified 13", quant))
			return;
		if (Core.CheckInventory(CragName))
			while (!Bot.Inventory.Contains("Unidentified 13", quant > 13 ? 13 : quant))
				DiamondExchange();
		else if (Core.CheckInventory("Bounty Hunter's Drone Pet") || Core.CheckInventory("Nulgath's Birthday Gift"))
			NewWorldsNewOpportunities("Unidentified 13", quant > 13 ? 13 : quant);
		else
			Supplies("Unidentified 13", quant > 13 ? 13 : quant);
	}

	/// <summary>
	/// Do Kiss the Void quest for Blood Gems
	/// </summary>
	/// <param name="quant">Desired quantity, 100 = max stack</param>
	public void KisstheVoid(int quant = 100)
	{
		if (Core.CheckInventory("Blood Gem of the Archfiend", quant))
			return;
		Core.AddDrop("Tendurrr The Assistant", "Fragment of Chaos");
		Core.Logger($"Farming {quant} Blood Gems");
		int i = 1;
		while(!Bot.Inventory.Contains("Blood Gem of the Archfiend", quant))
		{
			Core.EnsureAccept(3743);
			if(!Core.CheckInventory("Tendurrr The Assistant"))
			{
				JoinTercessuinotlim();
				Core.Jump("m2", "Bottom");
				Bot.Player.KillForItem("Dark Makai", "Tendurrr The Assistant", 1);
				Core.JumpWait();
			}
			if (!Core.CheckInventory("Fragment of Chaos", 80))
				Core.HuntMonster("blindingsnow", "Chaos Gemrald", "Fragment of Chaos", 80, false);
			if (!Bot.Inventory.ContainsTempItem("Broken Betrayal Blade", 8))
				Core.KillMonster("evilwarnul", "r13", "Left", "Legion Fenrir", "Broken Betrayal Blade", 8);
			Core.EnsureComplete(3743);
			Bot.Wait.ForPickup("Blood Gem of the Archfiend");
			Core.Logger($"Completed x{i}");
		}
	}
}
public enum ChooseReward
{
	TaintedGem,
	DarkCrystalShard,
	DiamondofNulgath,
	GemofNulgath,
	BloodGemoftheArchfiend,
	TotemofNulgath
}