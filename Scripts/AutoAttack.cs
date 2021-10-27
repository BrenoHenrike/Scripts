using RBot;

public class Script
{
	public void ScriptMain(ScriptInterface bot)
	{
		bot.Options.InfiniteRange = true;
		bot.Lite.UntargetSelf = true;

		bot.Skills.StartTimer();

		while (!bot.ShouldExit())
			bot.Player.Kill("*");
	}
}