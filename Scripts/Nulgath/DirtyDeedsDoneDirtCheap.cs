//cs_include Scripts/CoreBots.cs
using RBot;
using System.Linq;

public class DirtyDeedsDoneDirtCheap
{
	public CoreBots Core = new CoreBots();
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Core.AddDrop("Emerald Pickaxe", "Seraphic Grave Digger Spade", "Unidentified 10", "Receipt of Swindle", "Blood Gem of the Archfiend");
		Core.CheckInventory("Unidentified 10");
		Core.CheckInventory("Receipt of Swindle");
		Core.CheckInventory("Blood Gem of the Archfiend");

		if(!Core.CheckInventory("Emerald Pickaxe"))
		{
			bot.RegisterHandler(5, b =>
			{
				if (b.Monsters.CurrentMonsters.Where(m => m.Alive).Count() > 1)
					b.Player.Kill("Staff of Inversion");
			}, "escherion");
			Core.KillMonster("escherion", "Boss", "Left", "Escherion", "Emerald Pickaxe", 1, false);
			bot.Handlers.RemoveAll(h => h.Name == "escherion");
		}

		if(!Core.CheckInventory("Seraphic Grave Digger Spade"))
			Core.KillMonster("legioncrypt", "r1", "Top", "Gravedigger", "Seraphic Grave Digger Spade", 1, false);

		int i = 1;
		while(!Core.CheckInventory("Unidentified 10", 1000))
		{
			Core.EnsureAccept(7818);
			Core.HuntMonster("towerofdoom10", "Slugbutter", "Slugbutter Digging Advice");
			Core.HuntMonster("crownsreach", "Chaos Tunneler", "Chaotic Tunneling Techniques", 2);
			Core.HuntMonster("downward", "Crystal Mana Construct", "Crystalized Corporate Digging Secrets", 3);
			Core.EnsureComplete(7818);
			Core.Logger($"Completed {i}");
			i++;
		}

		Core.SetOptions(false);
	}
}