//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaDarkovia
{
	public CoreBots Core => CoreBots.Instance;

	public int questStart = 0;

	public string OptionsStorage = "SagaDarkovia";
	public bool DontPreconfigure = true;

	public List<IOption> Options = new List<IOption>()
	{
		new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
	};

	public static readonly int[] qIDs = 
	{
		/* -- /Join DarkoviaGrave --	                   */
			494, /*  0 - Grave Mission                     */
			495, /*  1 - Lending a Helping Hand            */
			496, /*  2 - Bone Appetit                      */
			497, /*  3 - Batting Cage                      */
			498, /*  4 - His Bark is worse than his Blight */
		/* -- /Join DarkoviaForest --                      */
			514, /*  5 - Lil' Red                          */
			516, /*  6 - A Dire Situation                  */
			517, /*  7 - Blood, Sweat, and Tears           */
			518, /*  8 - What a Lich!                      */
		/* -- /Join Safiria --                             */
			519, /*  9 - Feeding Grounds                   */
			520, /* 10 - Going Batty                       */
			521, /* 11 - Lycan Knights                     */
			522, /* 12 - Twisted Paw                       */
		/* -- /Join Lycan --                               */
			534, /* 13 - A Gift Of Meat                    */
			535, /* 14 - No Respect                        */
			536, /* 15 - Vampire Knights                   */
			537, /* 16 - Sanguine                          */
		/* -- /Join LycanWar --                            */
			0  , /* 17 - Lycan War                         */
		/* -- /Join ChaosCave --                           */
			564, /* 18 - Search and Report                 */
			565, /* 19 - The Key is the Key                */
			566, /* 20 - Secret Words                      */
			567, /* 21 - Dracowerepyre                     */
		/* -- /Join Wolfwing --                            */
			0    /* 22 - Wolfwing                          */
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
			switch(i)
			{
				case 0: //Grave Mission
					Core.GetMapItem(97, map: "darkoviagrave");
					break;
				case 1: //Lending a Helping Hand
					Core.SmartKillMonster(qIDs[i], "darkoviagrave", "Skeletal Fire Mage");
					break;
				case 2: //Bone Appetit
					Core.SmartKillMonster(qIDs[i], "darkoviagrave", "Rattlebones");
					break;
				case 3: //Batting Cage
					Core.SmartKillMonster(qIDs[i], "darkoviagrave", "Albino Bat");
					break;
				case 4: //His Bark is worse than his Blight
					Core.SmartKillMonster(qIDs[i], "darkoviagrave", "Blightfang");
					break;
				case 5: //Lil' Red
					Core.SmartKillMonster(515, "greenguardeast", new[] { "Wolf", "Spider" });
					Core.SmartKillMonster(515, "greenguardwest", new[] { "Frogzard", "Slime", "Big Bad Boar" }, completeQuest: true);
					break;
				case 6: //A Dire Situation
					Core.SmartKillMonster(qIDs[i], "darkoviaforest", "Dire Wolf");
					break;
				case 7: //Blood, Sweat, and Tears
					Core.SmartKillMonster(qIDs[i], "darkoviaforest", new[] { "Blood Maggot", "Blood Maggot", "Blood Maggot" });
					break;
				case 8: //What a Lich!
					Core.SmartKillMonster(qIDs[i], "darkoviaforest", "Lich of the Stone");
					break;
				case 9: //Feeding Grounds
					Core.SmartKillMonster(qIDs[i], "safiria", "Blood Maggot");
					break;
				case 10: //Going Batty
					Core.SmartKillMonster(qIDs[i], "safiria", "Albino Bat");
					break;
				case 11: //Lycan Knights
					Core.SmartKillMonster(qIDs[i], "safiria", "Chaos Lycan");
					break;
				case 12: //Twisted Paw
					Core.SmartKillMonster(qIDs[i], "safiria", "Twisted Paw");
					break;
				case 13: //A Gift Of Meat
					bot.Player.Join("lycan");
					Core.Jump("r3", "Right");
					bot.Sleep(3000);
					Core.SmartKillMonster(qIDs[i], "lycan", "Dire Wolf");
					break;
				case 14: //No Respect
					Core.SmartKillMonster(qIDs[i], "lycan", new[] { "Lycan", "Lycan Knight" });
					break;
				case 15: //Vampire Knights
					Core.SmartKillMonster(qIDs[i], "lycan", "Chaos Vampire Knight");
					break;
				case 16: //Sanguine
					Core.SmartKillMonster(qIDs[i], "lycan", "Sanguine");
					break;
				case 17: //Lycan War
					bot.Player.Join("lycanwar");
					Core.Jump("Boss", "Right");
					bot.Sleep(5000);
					Core.Jump("Boss", "Left");
					bot.Player.Kill("Edvard");
					bot.Sleep(7000);
					break;
				case 18: //Search and Report
					Core.GetMapItem(107, map: "chaoscave");
					break;
				case 19: //The Key is the Key
					Core.SmartKillMonster(qIDs[i], "chaoscave", "Werepyre", 50);
					break;
				case 20: //Secret Words
					Core.SmartKillMonster(qIDs[i], "chaoscave", "Werepyre", 50);
					break;
				case 21: //Dracowerepyre
					bot.Player.Join("chaoscave");
					Core.Jump("r5", "Left");
					bot.Sleep(5000);
					Core.SmartKillMonster(qIDs[i], "chaoscave", "Dracowerepyre");
					break;
				case 22: //Wolfwing
					bot.Player.Join("wolfwing");
					Core.Jump("Cut1", "Left");
					bot.Sleep(5000);
					Core.KillMonster("wolfwing", "Boss", "Left", "Wolfwing");
					bot.Sleep(3000);
					switch(Core.HeroAlignment)
					{
						case 0:
							bot.SendPacket("%xt%zm%tryQuestComplete%9860%597%-1%false%wvz%");
							break;
						default:
							bot.SendPacket("%xt%zm%tryQuestComplete%10853%598%-1%false%wvz%");
							break;
					}
					break;
			}
			Core.EnsureComplete(qIDs[i]);
			Core.Logger($"Finished {i}");
			Core.Rest();
		}

		Core.SetOptions(false);
	}
}
