//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class EnoughDOOMforanArchfiend
{
	public CoreBots Core = new CoreBots();
	public CoreNulgath Nulgath = new CoreNulgath();
	public CoreFarms Farms = new CoreFarms();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		if(!Core.CheckInventory("Doomblade of Destruction") || !Core.CheckInventory("DoomLord's War Mask")
			|| !Core.CheckInventory("ShadowFiend Cloak") || !Core.CheckInventory("Locks of the DoomLord")
			|| !Core.CheckInventory("Unidentified 35") || !Core.CheckInventory("Unidentified 34"))
		{
			Core.Logger("Missing one or more items from Nulgath Demands Work quest");
			return;
		}

		Core.Unbank("DoomLord's War Mask", "ShadowFiend Cloak", "Locks of the DoomLord", "Doomblade of Destruction");

		Core.AddDrop(Nulgath.bagDrops);
		Core.AddDrop("ArchFiend DoomLord", "Undead Essence", "Chaorruption Essence",
			"Essence Potion", "Essence of Klunk", "Living Star Essence", "Bone Dust", "Undead Energy");


		Nulgath.ContractExchange(ChooseReward.BloodGemoftheArchfiend);

		Nulgath.FarmUni13(1);

		Nulgath.ApprovalAndFavor(0, 5000);

		Nulgath.EssenceofNulgath(100);

		Core.HuntMonster("evilwardage", "Klunk", "Essence of Klunk", 1, false);

		Core.KillMonster("battleunderb", "Enter", "Spawn", "*", "Undead Essence", 1000, false);

		Nulgath.FarmVoucher(false);

		Nulgath.FarmBloodGem(2);

		if (!Core.CheckInventory("Aelita's Emerald"))
		{
			bot.Player.Join("yulgar");
			bot.Shops.Load(16);
			bot.Wait.ForActionCooldown(ScriptWait.GameActions.LoadShop);
			bot.SendPacket("%xt%zm%buyItem%150738%40660%16%23790%");
			bot.Sleep(1500);
		}

		while (!Core.CheckInventory("Essence Potion", 5))
		{
			Core.HuntMonster("orecavern", "Deathmole", "Arashtite Ore", 2, false);
			Core.HuntMonster("deathsrealm", "Skeleton Fighter", "Necrot", 2, false);

			bot.Player.Join("alchemy");
			bot.Sleep(2000);
			for (int i = 0; i < 2; i++)
			{
				bot.SendPacket("%xt%zm%crafting%1%getAlchWait%11480%11473%false%Ready to Mix%Necrot%Arashtite Ore%Uruz%Moose%");
				bot.Sleep(15000);
				bot.SendPacket("%xt%zm%crafting%1%checkAlchComplete%11480%11473%false%Mix Complete%Necrot%Arashtite Ore%Uruz%Moose%");
				bot.Sleep(1000);
				bot.Player.RejectExcept("Essence Potion");
				bot.Player.Pickup("Essence Potion");
				bot.Sleep(1000);
				if (bot.Inventory.Contains("Essence Potion", 5))
					break;
			}
		}

		Core.EnsureAccept(5260);

		Core.KillMonster("orecavern", "r3", "Up", "*", "Chaorruption Essence", 75, false);

		Core.HuntMonster("starsinc", "Living Star", "Living Star Essence", 100, false);

		Core.EnsureComplete(5260);
		bot.Player.Pickup("ArchFiend DoomLord");

		Core.SetOptions(false);
	}
}