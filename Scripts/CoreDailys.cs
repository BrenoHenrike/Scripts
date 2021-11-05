using RBot;

public class CoreDailys
{
	public ScriptInterface Bot => ScriptInterface.Instance;

	public CoreBots Core = new CoreBots();

	public void DoAllDailys()
	{
		Core.Logger("Doing all dailys");
		Core.Logger("1");
		MadWeaponSmith();
		Core.Logger("2");
		CyserosSuperHammer();
		Core.Logger("3");
		BrightKnightArmor();
		Core.Logger("4");
		CollectorClass();
		Core.Logger("5");
		Cryomancer();
		Core.Logger("6");
		Pyromancer();
		Core.Logger("7");
		DeathKnightLord();
		Core.Logger("8");
		ShadowScytheClass();
		Core.Logger("9");
		GrumbleGrumble();
		Core.Logger("10");
		EldersBlood();
		Core.Logger("11");
		ShadowShroud();
		Core.Logger("12");
		DagesScrollFragment();
		Core.Logger("13");
		CryptoToken();
		Core.Logger("14");
		BeastMasterChallenge();
		Core.Logger("15");
		FungiforaFunGuy();
		Core.Logger("Dailys completed");
	}

	/// <summary>
	/// Accepts the quest and kills the monster to complete, if no cell/pad is given will hunt for the monster.
	/// </summary>
	/// <param name="quest">ID of the quest</param>
	/// <param name="map">Map where the monster is</param>
	/// <param name="monster">Name of the monster</param>
	/// <param name="item">Item to get</param>
	/// <param name="quant">Quantity of the item</param>
	/// <param name="isTemp">Whether it is temporary</param>
	/// <param name="cell">Cell where the monster is (optional)</param>
	/// <param name="pad">Pad where the monster is</param>
	public void DailyRoutine(int quest, string map, string monster, string item, int quant = 1, bool isTemp = true, string cell = null, string pad = null)
	{
		if (Bot.Quests.IsDailyComplete(quest))
			return;
		Bot.Player.Join(map);
		Core.EnsureAccept(quest);
		if (cell != null || pad != null)
			Core.KillMonster(map, cell, pad, monster, item, quant, isTemp);
		else
			Core.HuntMonster(map, monster, item, quant, isTemp);
		Core.EnsureComplete(quest);
		Bot.Player.Pickup(Bot.Drops.Pickup.ToArray());
	}
	/// <summary>
	/// Checks if the daily is complete, if not will add the specified drops and unbank if necessary
	/// </summary>
	/// <param name="quest">ID of the quest</param>
	/// <param name="items">Items to add to drop grabber and unbank</param>
	/// <returns></returns>
	public bool CheckDaily(int quest, params string[] items)
	{
		if (Bot.Quests.IsDailyComplete(quest))
			return false;
		Core.AddDrop(items);
		Core.Unbank(items);
		return true;
	}

	/// <summary>
	/// This is exclusive for Grumble, Grumble or any other quest without requirements
	/// </summary>
	/// <param name="quest"></param>
	public void DailyRoutine(int quest)
	{
		if (Bot.Quests.IsDailyComplete(quest))
			return;

		Core.EnsureAccept(quest);
		Core.EnsureComplete(quest);
	}

	public void FungiforaFunGuy()
	{
		if (!CheckDaily(4465) || Bot.Player.GetFactionRank("Brightoak") == 10 || !Core.IsMember)
			return;

		if (Bot.Quests.IsDailyComplete(4465))
			return;
		Core.EnsureAccept(4465);
		Core.HuntMonster("brightoak", "Grove Spore", "Colony Spore");
		Core.HuntMonster("brightoak", "Grove Spore", "Intact Spore");
		Core.EnsureComplete(4465);
	}

	public void BeastMasterChallenge()
	{
		if (!CheckDaily(3759) || Bot.Player.GetFactionRank("BeastMaster") == 10 || !Core.IsMember)
			return;

		DailyRoutine(3759, "swordhavenbridge", "Purple Slime", "Purple Slime", 10);
	}

	public void CyserosSuperHammer()
	{
		if (Core.CheckInventory("Cysero's SUPER Hammer", toInv: false))
			return;
		if(!Core.CheckInventory("Mad Weaponsmith"))
			return;
		if (!CheckDaily(4310, "C-Hammer Token") && !Core.IsMember)
			return;
		if (!CheckDaily(4311, "C-Hammer Token") && Core.IsMember)
			return;
		DailyRoutine(4310, "deadmoor", "Geist", "Geist's Chain Link");
		if (Core.IsMember)
			DailyRoutine(4311, "deadmoor", "Geist", "Geist's Pocket Lint");
		Core.ToBank("C-Hammer Token");
	}

	public void MadWeaponSmith()
	{
		if (Core.CheckInventory("Mad Weaponsmith", toInv: false))
			return;
		if (!CheckDaily(4308, "C-Armor Token") && !Core.IsMember)
			return;
		if (!CheckDaily(4309, "C-Armor Token") && Core.IsMember)
			return;
		DailyRoutine(4308, "deadmoor", "Nightmare", "Nightmare Fire");
		if (Core.IsMember)
			DailyRoutine(4309, "deadmoor", "Nightmare", "Unlucky Horseshoe");
		Core.ToBank("C-Armor Token");
	}

	public void BrightKnightArmor()
	{
		if (Core.CheckInventory("Bright Knight", toInv: false))
			return;
		if (!CheckDaily(3826, "Seal of Light") && !CheckDaily(3825, "Seal of Darkness"))
			return;
		DailyRoutine(3826, "alteonbattle", "Ultra Alteon", "Alteon Defeated");
		DailyRoutine(3825, "sepulchurebattle", "Ultra Sepulchure", "Sepulchure Defeated");
	}
	
	public void CollectorClass()
	{
		if (Core.CheckInventory("The Collector", toInv: false))
			return;
		if (!CheckDaily(1316, "This Might Be A Token", "Tokens of Collection") && !Core.IsMember)
			return;
		if (!CheckDaily(1331, "This Is Definitely A Token", "Tokens of Collection") && !CheckDaily(1332, "This Could Be A Token", "Tokens of Collection") && Core.IsMember)
			return;
		DailyRoutine(1316, "terrarium", "*", "This Might Be A Token", 2, false, "r2", "Right");
		if (Core.IsMember)
		{
			DailyRoutine(1331, "terrarium", "*", "This Is Definitely A Token", 2, false, "r2", "Right");
			DailyRoutine(1332, "terrarium", "*", "This Could Be A Token", 2, false, "r2", "Right"); 
		}
	}
	
	public void Cryomancer()
	{
		if (Core.CheckInventory("Cryomancer", toInv: false))
			return;
		if (!CheckDaily(3966, "Glacera Ice Token") && !Core.IsMember)
			return;
		if (!CheckDaily(3965, "Glacera Ice Token") && Core.IsMember)
			return;
		if (Core.IsMember)
			DailyRoutine(3965, "frozentower", "Frost Invader", "Dark Ice");
		else
			DailyRoutine(3966, "frozentower", "Frost Invader", "Dark Ice");
		Core.ToBank("Glacera Ice Token");
	}
	
	public void Pyromancer()
	{
		if (Core.CheckInventory("Pyromancer", toInv: false))
			return;
		if (!CheckDaily(2209, "Shurpu Blaze Token") && !Core.IsMember)
			return;
		if (!CheckDaily(2210, "Shurpu Blaze Token") && Core.IsMember)
			return;
		if (Core.IsMember)
			DailyRoutine(2210, "xancave", "Shurpu Ring Guardian", "Guardian Shale");
		else
			DailyRoutine(2209, "xancave", "Shurpu Ring Guardian", "Guardian Shale");
		Core.ToBank("Shurpu Blaze Token");
	}
	
	public void DeathKnightLord()
	{
		if (Core.CheckInventory("DeathKnight Lord", toInv: false))
			return;
		if (!CheckDaily(492, "Shadow Skull") || !Core.IsMember)
			return;
		DailyRoutine(492, "bludrut4", "Shadow Serpent", "Shadow Scales", 5);
		Core.ToBank("Shadow Skull");
	}
	
	public void ShadowScytheClass()
	{
		if (Core.CheckInventory("ShadowScythe General"))
			return;
		if (!CheckDaily(3828, "Shadow Shield") && !Core.IsMember)
			return;
		if (!CheckDaily(3827, "Shadow Shield") && Core.IsMember)
			return;
		DailyRoutine(3828, "lightguardwar", "Citadel Crusader", "Broken Blade");
		if (Core.IsMember)
			DailyRoutine(3827, "lightguardwar", "Citadel Crusader", "Broken Blade");
	}

	public void GrumbleGrumble()
	{
		if (!Core.CheckInventory("Crag & Bamboozle"))
			return;
		if (!CheckDaily(592, "Diamond of Nulgath", "Blood Gem of the Archfiend"))
			return;
		DailyRoutine(592);
	}

	public void EldersBlood()
	{
		if (Core.CheckInventory("Elders' Blood", 5))
			return;
		if (!CheckDaily(802, "Elders' Blood"))
			return;
		DailyRoutine(802, "arcangrove", "Gorillaphant", "Slain Gorillaphant", 50, cell: "Right", pad: "Left");
	}

	public void ShadowShroud()
	{
		if (!CheckDaily(486, "Shadow Shroud"))
			return;
		DailyRoutine(486, "bludrut2", "Shadow Creeper", "Shadow Canvas", 5, cell: "Enter", pad: "Down");
		Core.ToBank("Shadow Shroud");
	}

	public void DagesScrollFragment()
	{
		if (!CheckDaily(3596, "Dage Scroll Fragment"))
			return;
		DailyRoutine(3596, "mountdoomskull", "*", "Chaos Power Increased", 6, cell: "b1", pad: "Left");
		Core.ToBank("Dage's Scroll Fragment");
	}

	public void CryptoToken()
	{
		if (!CheckDaily(6187, "Crypto Token"))
			return;
		DailyRoutine(6187, "boxes", "Sneevil", "Metal Ore", cell: "Enter", pad: "Spawn");
		Core.ToBank("Crypto Token");
	}
}