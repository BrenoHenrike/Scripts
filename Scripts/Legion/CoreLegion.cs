using RBot;

public class CoreLegion
{
	public ScriptInterface Bot => ScriptInterface.Instance;

	public CoreBots Core => CoreBots.Instance;

	public void FarmLegionToken(int quant = 25000)
	{
		if (Core.CheckInventory("Legion Token", quant))
			return;
		LTBrightParagon(quant);
		LTShogunParagon(quant);
		LTFirstClassEntertainment(quant, true, 3);
		LTDreadrock(quant);
	}

	public void LTBrightParagon(int quant = 25000)
	{
		if (Core.CheckInventory("Legion Token", quant) || !Core.CheckInventory("Bright Paragon Pet"))
			return;
		Core.Logger($"Farming {quant} Legion Tokens");
		int i = 1;
		while (!Core.CheckInventory("Legion Token", quant))
		{
			Core.EnsureAccept(4704);
			Core.KillMonster("brightfortress", "r3", "Right", "*", "Badge of Loyalty", 10);
			Core.KillMonster("brightfortress", "r3", "Right", "*", "Badge of Corruption", 8);
			Core.KillMonster("brightfortress", "r3", "Right", "*", "Twisted Light Token", 6);
			Core.EnsureComplete(4704);
			Bot.Player.Pickup("Legion Token");
			Core.Logger($"Completed x{i}");
			i++;
		}
	}
	public void LTShogunParagon(int quant = 25000)
	{
		if (Core.CheckInventory("Legion Token", quant)
			|| (!Core.CheckInventory("Shogun Paragon Pet") && !Core.CheckInventory("Paragon Fiend Quest Pet") && !Core.CheckInventory("Paragon Ringbearer") && !Core.CheckInventory("Shogun Dage Pet")))
			return;
		Core.Logger($"Farming {quant} Legion Tokens");
		int i = 1;
		while (!Core.CheckInventory("Legion Token", quant))
		{
			if (Core.CheckInventory("Shogun Paragon Pet"))
				Core.EnsureAccept(5755);
			else if (Core.CheckInventory("Shogun Dage Pet"))
				Core.EnsureAccept(5756);
			else if (Core.CheckInventory("Paragon Fiend Quest Pet"))
				Core.EnsureAccept(6750);
			else if (Core.CheckInventory("Paragon Ringbearer"))
				Core.EnsureAccept(7073);
			Core.KillMonster("fotia", "r5", "Left", "*", "Nothing Heard", 10);
			Core.KillMonster("fotia", "r5", "Left", "*", "Nothing To See", 10);
			Core.KillMonster("fotia", "r5", "Left", "*", "Area Secured and Quiet", 10);
			Core.EnsureComplete(5755, 5756, 6750, 7073);
			Bot.Player.Pickup("Legion Token");
			Core.Logger($"Completed x{i}");
			i++;
		}
	}

	public void LTFirstClassEntertainment(int quant = 25000, bool onlyWithParty = false, int partySize = 4)
	{
		if (Core.CheckInventory("Legion Token", quant))
			return;
		bool privateRoomSwitch = Bot.Options.PrivateRooms;
		if (Bot.Options.PrivateRooms)
			Bot.Options.PrivateRooms = false;
		Bot.Player.Join("legionarena");
		if (Bot.Map.PlayerCount < partySize && onlyWithParty)
		{
			Bot.Player.Join("legionarena", ignoreCheck: true);
			if(Bot.Map.PlayerCount < partySize)
			{
				Bot.Options.PrivateRooms = privateRoomSwitch;
				return;
			}
		}
		Bot.Player.Jump("Boss", "Left");
		int i = 1;
		while (!Core.CheckInventory("Legion Token", quant))
		{
			Core.EnsureAccept(6743);
			Core.HuntMonster("legionarena", "Legion Fiend Rider", "Axeros' Brooch");
			Core.EnsureComplete(6743);
			Bot.Player.Pickup("Legion Token");
			Core.Logger($"Completed x{i}");
			i++;
		}
	}

	public void LTDreadrock(int quant = 25000)
	{
		if (Core.CheckInventory("Legion Token", quant) || !Core.CheckInventory("Legion Champion"))
			return;
		Core.Logger($"Farming {quant} Legion Tokens");
		Bot.Player.Join("dreadrock");
		int i = 1;
		while(!Core.CheckInventory("Legion Token", quant))
		{
			Core.EnsureAccept(4849);
			Core.KillMonster("dreadrock", "r3", "Bottom", "*", "Dreadrock Enemy Recruited", 6);
			Core.EnsureComplete(4849);
			Bot.Player.Pickup("Legion Token");
			Core.Logger($"Completed x{i}");
			i++;
		}
	}


}