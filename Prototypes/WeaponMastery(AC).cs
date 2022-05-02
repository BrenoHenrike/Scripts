//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class WeaponMasteryAC
{
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core => CoreBots.Instance;
	public CoreNulgath Nulgath = new CoreNulgath();
	public CoreFarms Farm = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nulgath.bagDrops);
        Core.SetOptions();

        GetWM();

        Core.SetOptions(false);
    }

    public void GetWM()
    {
        Core.AddDrop(Nulgath.bagDrops);
        if(Core.CheckInventory("Evolved Warlord Orb")){
       	 	if(Core.CheckInventory("Evolved Warlord Hammer") && Core.CheckInventory("Evolved Warlord Axe")){
       		 	Core.Logger($"Weapon Mastery archieved!");
       		 	Bot.Sleep(5000);
        		return;
        	}
        	int i = 1;
			while(!Core.CheckInventory("Evolved Warlord Hammer") || !Core.CheckInventory("Evolved Warlord Axe"))
			{
				Core.EnsureAccept(4784);
				
				if(!Core.CheckInventory("Unidentified 13", 1)){
        			Nulgath.FarmUni13(1);}
        		
        		if(!Core.CheckInventory("TaintedGem", 10)){
        			Nulgath.SwindleBulk(10);}
        		
        		if(!Core.CheckInventory("Dark Crystal Shard", 10)){
      	  			Nulgath.FarmDarkCrystalShard(10);}
      	  		
      	  		if(!Core.CheckInventory("Totem of Nulgath", 1)){
      				Nulgath.FarmTotemofNulgath(1); }
      			
      			if(!Core.CheckInventory("Gem of Nulgath", 10)){
        			Nulgath.FarmGemofNulgath(10);}
        		
        		if(!Core.CheckInventory("Underfriend Blade of Nulgath")){
        			if(!Core.CheckInventory("Mirror Realm Token", 10)){
        				Core.HuntMonster("BattleOff", "Evil Moglin", "Mirror Realm Token", 10, false);}
        			if(Bot.Player.Gold <= 100000){	
        				Farm.Gold(100000);}
        			Core.BuyItem("mirrorportal" , 618, "Underfriend Blade of Nulgath");
        		}
        		
        	Core.EnsureComplete(4784);
        	Bot.Sleep(Core.ActionDelay);
        	Bot.Player.Pickup("Unidentified 10");
        	Bot.Player.Pickup("Evolved Warlord Hammer");
        	Bot.Player.Pickup("Evolved Warlord Axe");
        	Core.Logger($"Completed x{i++}");
        	}
        }
		else{
			Core.Logger($"Evolved Warlord Orb not found!");
			Bot.Sleep(5000);
		return;			
      	}
    }
}
//Made by ToxlcChain