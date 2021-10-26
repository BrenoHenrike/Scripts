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

	public void UseGoldBoost(GoldBoost boost) => UseBoost((int)boost, BoostType.Gold);
	public void UseXPBoost(XpBoosts boost) => UseBoost((int)boost, BoostType.Experience);
	public void UseClassBoost(ClassBoost boost) => UseBoost((int)boost, BoostType.Class);
	public void UseREPBoost(REPBoost boost) => UseBoost((int)boost, BoostType.Reputation);

	private void UseBoost(int boostID, BoostType type)
	{
		if (!Core.CheckInventory(boostID))
			return;

		Bot.RegisterHandler(30200, b =>
		{
			if (!Bot.Player.IsBoostActive(type))
				Bot.Player.UseBoost(boostID);
		});
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