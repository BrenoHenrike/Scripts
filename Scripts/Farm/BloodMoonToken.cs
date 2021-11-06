//cs_include Script/CoreBots.cs
using RBot;

public class BloodMoonToken
{
	public CoreBots Core = new CoreBots();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Core.AddDrop("Blood Moon Token", "Black Blood Vial", "Moon Stone");

		while (!bot.ShouldExit() || !Core.CheckInventory("Blood Moon Token", 300))
		{
			Core.EnsureAccept(6059);

			Core.HuntMonster("bloodmoon", "Black Unicorn", "Black Blood Vial", 1, false);
			Core.HuntMonster("bloodmoon", "Lycan Guard", "Moon Stone", 1, false);

			Core.EnsureComplete(6059);
			bot.Wait.ForPickup("Blood Moon Token");
		}

		Core.SetOptions(false);
	}
}