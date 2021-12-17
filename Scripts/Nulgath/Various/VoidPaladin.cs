//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class VoidPaladin
{
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core => CoreBots.Instance;
	public CoreFarms Farm = new CoreFarms();
	public CoreNulgath Nulgath = new CoreNulgath();

	public readonly string[] ADKRewards =
	{
		"Void Paladin Helm",
		"Void Paladin Horns",
		"Void Paladin Katana Cape",
		"Void Paladin Cape",
		"Void Paladin Katana",
		"Void Paladin Katanas"
	};
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();
		Core.AddDrop(Nulgath.bagDrops);

		ADarkTemptation();

		DeeperandDeeperintoDarkness();

		Sacrifice();

		CyberSet();

		Core.SetOptions(false);
	}

	public void ADarkTemptation()
	{
		if(Core.CheckInventory(ADKRewards))
			return;
		Core.AddDrop(ADKRewards);
		Core.AddDrop("Scroll of Underworld", "Archmage Ink", "Mystic Shard");

		int i = 1;
		Core.Logger("Starting [A Dark Temptation] Quest");
		while(!Core.CheckInventory(ADKRewards, toInv: false))
		{
			Core.EnsureAccept(5826);

			Nulgath.FarmDarkCrystalShard(25);
			Nulgath.FarmDiamondofNulgath(13);
			Nulgath.EmblemofNulgath(2);
			Nulgath.SwindleBulk(35);
			Nulgath.FarmTotemofNulgath(1);
			Nulgath.FarmUni13();

			if (!Core.CheckInventory("Scroll of Underworld"))
			{
				if(Core.CheckInventory("Archmage Ink"))
				{
					Core.HuntMonster("underworld", "Skull Warrior", "Mystic Shard", 2, false);
					Core.BuyItem("dragonrune", 549, "Archmage Ink", 1, 5);
				}
				Bot.Player.Join("spellcraft");
				Bot.SendPacket("%xt%zm%crafting%1%spellOnStart%10%1555%Spell%");
				Bot.Sleep(Core.ExitCombatDelay);
				Bot.SendPacket("%xt%zm%crafting%1%spellComplete%10%2346%Underworld%");
				Bot.Player.Pickup("Scroll of Underworld");
			}

			Core.EnsureCompleteChoose(5826);
			Core.Logger($"Completed x{i++}");
		}
	}

	public void DeeperandDeeperintoDarkness()
	{
		if (Core.CheckInventory("Void Paladin"))
			return;

		Core.AddDrop("Void Paladin");
		Core.Logger("Starting [Deeper and Deeper into Darkness] Quest");

		Core.EnsureAccept(5827);

		Nulgath.FarmDiamondofNulgath(25);
		Nulgath.SwindleBulk(40);
		Nulgath.FarmVoucher(false);
		Nulgath.FarmGemofNulgath(25);
		Nulgath.FarmDarkCrystalShard(40);
		Nulgath.FarmUni13(2);

		if (!Core.CheckInventory("Nulgath Shaped Chocolate"))
		{
			Farm.Gold(2000000);
			Core.BuyItem("citadel", 44, "Nulgath Shaped Chocolate");
		}
		VoidAura(2);

		Core.EnsureComplete(5827);
		Bot.Player.Pickup("Void Paladin");
	}

	public void Sacrifice()
	{
		if(Core.CheckInventory("Void Light of Destiny"))
			return;
			
		Core.AddDrop("Void Light of Destiny");
		Core.Logger("Starting [Sacrifice] Quest");

		Nulgath.FarmDarkCrystalShard(40);
		Nulgath.FarmDiamondofNulgath(40);
		Nulgath.SwindleBulk(40);
		Nulgath.FarmGemofNulgath(20);
		Nulgath.EmblemofNulgath(3);
		Nulgath.FarmTotemofNulgath(1);
		VoidAura(6);

		if(Core.CheckInventory("Blinding Light of Destiny"))
			Core.ChainComplete(5828);
		else if(Core.CheckInventory("Ascended Light of Destiny"))
			Core.ChainComplete(5829);
		Bot.Player.Pickup("Void Light of Destiny");
	}

	public readonly string[] CyberVoidSet =
	{
		"Cyber Void Paladin",
		"Cyber Void Paladin Helm",
		"Cyber Void Cape",
		"Cyber Void Light of Destiny"
	};

	public void CyberSet()
	{
		if(Core.CheckInventory(CyberVoidSet, toInv: false))
			return;

		Core.AddDrop(CyberVoidSet);
		Core.CheckInventory(new[] { "Void Light of Destiny", "Void Paladin", "Void Paladin Helm", "Void Paladin Katana", "Void Paladin Katana Cape" });
		Core.EnsureAccept(6625);
		Core.HuntMonster("dreadspace", "Dread Space Warrior", "Powerpack", 5);
		Core.EnsureComplete(6625);
		Bot.Player.Pickup(CyberVoidSet);
	}

	private void VoidAura(int quant)
	{
		if(Core.CheckInventory("Void Aura", quant))
			return;

		Core.AddDrop("Void Aura");
		Core.Logger($"Farming {quant} Void Aura");
		while (!Core.CheckInventory("Void Aura", quant))
		{
			if (Core.CheckInventory("Sepulchure's DoomKnight Armor"))
			{
				Core.EquipClass(ClassType.Farm);
				Core.AddDrop("Malignant Essence", "Empowered Essence");

				Core.EnsureAccept(4439);

				Core.HuntMonster("shadowrealmpast", "Shadow Lord", "Malignant Essence", 3, false);
				Core.KillMonster("shadowrealmpast", "Enter", "Spawn", "*", "Empowered Essence", 50, false);

				Core.EnsureComplete(4439);
			}
			else if (Core.IsMember)
			{
				Core.EquipClass(ClassType.Farm);
				Core.AddDrop("Mirror Essence", "Twisted Essence", "Transposed Essence");

				Core.EnsureAccept(4438);

				Core.KillMonster("reddeath", "r2", "Left", "*", "Mirror Essence", 175, false);
				Core.HuntMonster("neverworldb", "*", "Twisted Essence", 25, false);
				Core.HuntMonster("doomwar", "Doom King Alteon", "Transposed Essence", 1, false);

				Core.EnsureComplete(4438);
			}
			else
			{
				Core.AddDrop("Dai Tengu Essence", "Unending Avatar Essence", "Void Dragon Essence", "Astral Ephemerite Essence", "Creature Creation Essence",
							 "Belrot the Fiend Essence", "Black Knight Essence", "Tiger Leech Essence", "Carnax Essence", "Chaos Vordred Essence");

				Core.EnsureAccept(4432);

				Core.EquipClass(ClassType.Farm);
				Core.KillMonster("timespace", "Frame1", "Left", "*", "Astral Ephemerite Essence", 20, false);

				Core.EquipClass(ClassType.Solo);
				Core.KillMonster("aqlesson", "Frame9", "Right", "Carnax", "Carnax Essence", 20, false);
				Core.HuntMonster("citadel", "Belrot the Fiend", "Belrot the Fiend Essence", 20, false);
				Core.HuntMonster("greenguardwest", "Black Knight", "Black Knight Essence", 20, false);
				Core.HuntMonster("mudluk", "Tiger Leech", "Tiger Leech Essence", 20, false);
				Core.HuntMonster("necrocavern", "Chaos Vordred", "Chaos Vordred Essence", 20, false);
				Core.HuntMonster("hachiko", "Dai Tengu", "Dai Tengu Essence", 20, false);
				Core.HuntMonster("timevoid", "Unending Avatar", "Unending Avatar Essence", 20, false);
				Core.HuntMonster("dragonchallenge", "Void Dragon", "Void Dragon Essence", 20, false);
				Core.HuntMonster("maul", "Creature Creation", "Creature Creation Essence", 20, false);

				Core.EnsureComplete(4432);

			}
			Bot.Player.Pickup("Void Aura");
		}
	}
}