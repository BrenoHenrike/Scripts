//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/SepulchureSaga.cs
using RBot;

public class FiendofLight
{

	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
	public CoreAdvanced Adv = new CoreAdvanced();
    public CoreDailies Daily = new();
	public CoreStory Story = new CoreStory();
    public SepulchureSaga SepulchureSaga = new();


    public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();
		GetFol();
     	Core.SetOptions(false);
	}
	
	private string[] Rewards = 
	{
		"Fiend of Light",
		"Fiend of Light Helm",
		"Fiend of Light Hair", 
		"Fiend of Light Winged Hair",
		"Fiend of Light Blinded Hair",
		"Fiend of Light Locks",
		"Fiend of Light Helm + Locks",
		"Fiend of Light Backblades",
		"Fiend of Light Wings",
		"Fiend of Light Wings + Tail",
		"Fiend of Light Tail",
		"Fiend of Light Blade",
		"Doomed Fiend of Light Blade",
		"Fiend of Light Blades"
	};
	
	public void GetFol()
	{
		////doing pre-story\\\\
		SepulchureSaga.DoALL();
	
		////Checking if all items are obtained\\\\
		if(Core.CheckInventory(Rewards,1,false,false)){ 						
			Core.Logger($"Fiend of Light archieved!");
       		Bot.Sleep(5000);
        	return;
       	}
        ////Farming rewards\\\\
        Core.AddDrop(Rewards);
        int i = 1;
        while(!Core.CheckInventory(Rewards)){
        	Core.EnsureAccept(6408);
        	Core.HuntMonster("darkplane", "*", "Crystallized Memory", 10, true, publicRoom: false);
        	Core.EnsureComplete(6408);
        	Bot.Sleep(Core.ActionDelay);
        	Bot.Player.Pickup(Rewards);
        	Core.Logger($"Completed x{i++}");
       	}
		////If you want the bot to bank the items after farming them all un "//" the following lines.\\\\
   		//Core.Logger($"banking Fiend of Light");
		//Core.ToBank(Rewards); 
    	
  	}
}
////Made by ToxlcChain\\\\