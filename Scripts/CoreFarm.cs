using RBot;

public class CoreFarm
{
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

	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core = new CoreBots();
	
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