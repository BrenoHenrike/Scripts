//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaExtra
{
	public CoreBots Core => CoreBots.Instance;

	public int questStart = 0;

	public string OptionsStorage = "SagaExtra";
	public bool DontPreconfigure = true;

	public List<IOption> Options = new List<IOption>()
	{
		new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
	};

	public static readonly int[] qIDs =
	{
		3812, /* 0  - */
		3813, /* 1  - */
		3814, /* 2  - */
		3815, /* 3  - */
		3816, /* 4  - */
		3817, /* 5  - */
		3818, /* 6  - */
		3819, /* 7  - */
		3820, /* 8  - */
		3821, /* 9  - */
		3822, /* 10 - */
		3823, /* 11 - */
		3824  /* 12 - */
	};

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		questStart = bot.Config.Get<int>("startQuest");

		for (int i = questStart; i < qIDs.Length; i++)
		{
			bot.Config.Set("startQuest", i);
			Core.Logger($"Starting {i}");
			Core.EnsureAccept(qIDs[i]);
			switch (i)
			{
				case 0: //
                    bot.Player.Join("dreadhaven");
                    break;
				case 1: //
                    bot.Player.Join("dreadhaven");
                    break;
				case 2: //
                    bot.Player.Join("dreadhaven");
                    break;
				case 3: //
                    Core.SmartKillMonster(qIDs[i], "falcontower", "Lady Knight|Sir Knight");
                    break;
				case 4: //
                    Core.SmartKillMonster(qIDs[i], "falcontower", "Lady Knight|Sir Knight");
                    break;
				case 5: //
                    Core.SmartKillMonster(qIDs[i], "falcontower", "Lady Knight|Sir Knight");					
					break;
				case 6: //
                    Core.SmartKillMonster(qIDs[i], "falcontower", "Lady Knight|Sir Knight");
					break;
				case 7: //
                    Core.SmartKillMonster(qIDs[i], "falcontower", "Lady Knight|Sir Knight");
					break;
				case 8: //
                    Core.SmartKillMonster(qIDs[i], "falcontower", "Lady Knight|Sir Knight");
					break;
				case 9: //
                    Core.SmartKillMonster(qIDs[i], "falcontower", "DragonLord");
                    break;
				case 10: //
                    Core.SmartKillMonster(qIDs[i], "falcontower", "Dragon Drakath");
                    break;
				case 11: //
                    Core.SmartKillMonster(qIDs[i], "falcontower", "Sepulchure");
                    break;
				case 12: //
                    Core.SmartKillMonster(qIDs[i], "falcontower", "Alteon");
                    break;
			}
			Core.EnsureComplete(qIDs[i]);
			Core.Logger($"Finished {i}");
			Core.Rest();
		}

		Core.SetOptions(false);
	}
}
