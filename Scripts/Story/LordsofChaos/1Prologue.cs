//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaPrologue 
{
	public CoreBots Core => CoreBots.Instance;

	public int questStart = 0;

	public string OptionsStorage = "SagaPrologue";
	public bool DontPreconfigure = true;

	public List<IOption> Options = new List<IOption>()
	{
		new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
	};

	public static readonly int[] qIDs =
	{
		/* -- /Join PortalUndead -- 						 */
			183 , /*  0 - Enter the gates                    */
		/* -- /Join SwordhavenUndead -- 					 */
			176 , /*  1 - Undead Assault                     */
			177 , /*  2 - Skull Crusher Mountain             */
			178 , /*  3 - The Undead Giant                   */
		/* -- /Join CastleUndead -- 						 */
			179 , /*  4 - Talk to the Knights (Storyline)    */
			180 , /*  5 - Defend the Throne (Storyline)      */
			0   , /*  6 - The Arrival of Drakath cutscene    */
		/* -- /Join Shadowfall -- 							 */
			196 , /*  7 - Recover Sepulchure's Cursed Armor! */
			6216, /*  8 - Unlife Insurance                   */
		/* -- /Join Castle -- 								 */
			6217, /*  9 - Enter the Crypt                    */
			6218, /* 10 - Rescue the Knights                 */
			6219  /* 11 - Forest of Chaos                    */
	};
	
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		questStart = bot.Config.Get<int>("startQuest");
		
		for(int i = questStart; i < qIDs.Length; i++)
		{
			bot.Config.Set("startQuest", i);
			Core.Logger($"Starting {i}");
			Core.EnsureAccept(qIDs[i]);
			switch(i)
			{
				case 0: //Enter the gates
					Core.SmartKillMonster(qIDs[i], "portalundead", "Skeletal Fire Mage");
					break;
				case 1: //Undead Assault
					Core.SmartKillMonster(qIDs[i], "swordhavenundead", "Skeletal Soldier");
					break;
				case 2: //Skull Crusher Mountain
					Core.SmartKillMonster(qIDs[i], "swordhavenundead", "Skeletal Ice Mage");
					break;
				case 3: //The Undead Giant
					Core.SmartKillMonster(qIDs[i], "swordhavenundead", "Undead Giant");
					break;
				case 4: //Talk to the Knights (Storyline)
					Core.GetMapItem(38, 5, "castleundead");
					break;
				case 5: //Defend the Throne (Storyline)
					Core.SmartKillMonster(qIDs[i], "castleundead", "*");					
					break;
				case 6: //The Arrival of Drakath cutscene
					bot.Player.Join("castleundead");
					Core.Jump("King2", "Center");
					bot.SendPacket($"%xt%zm%updateQuest%188220%41%{(Core.HeroAlignment > 1 ? 1 : Core.HeroAlignment)}%");
					break;
				case 7: //Recover Sepulchure's Cursed Armor!
					Core.SmartKillMonster(qIDs[i], "chaoscrypt", "Chaorrupted Armor");
					break;
				case 8: //Unlife Insurance
					bot.Player.Join("castle");
					Core.Jump("King", "Center");
					bot.Sleep(2000);
					bot.Player.Join("prison");
					Core.Jump("Tax", "Right");
					bot.Player.Kill("Piggy Drake");
					bot.Sleep(2500);
					Core.Jump("Vault", "Right");
					Core.GetMapItem(39, 5, "prison");
					Core.Jump("Open", "Left");
					bot.Shops.Load(1559);
					bot.Sleep(2000);
					bot.Shops.BuyItem(1559, "Unlife Insurance Bond");
					bot.Sleep(2000);
					bot.Player.Join("shadowfall");
					break;
				case 9: //Enter the Crypt
					Core.GetMapItem(5662, 1, "chaoscrypt");
					break;
				case 10: //Rescue the Knights
					Core.SmartKillMonster(qIDs[i], "chaoscrypt", "Chaorrupted Knight");
					break;
				case 11: //Forest of Chaos
					Core.SmartKillMonster(qIDs[i], "forestchaos", new[] { "Chaorrupted Wolf", "Chaorrupted Bear" });
					break;
			}
			Core.EnsureComplete(qIDs[i]);
			Core.Logger($"Finished {i}");
			Core.Rest();
		}

		Core.SetOptions(false);
	}
}
