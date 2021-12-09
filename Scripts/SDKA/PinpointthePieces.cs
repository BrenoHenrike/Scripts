//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/SDKA/CoreSDKA.cs
using System.Collections.Generic;
using RBot;
using RBot.Options;

public class PinpointthePieces_Any
{
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core => CoreBots.Instance;
	public CoreSDKA SDKA = new CoreSDKA();

	public string OptionStorage = "Pinpoint_the_Pieces";

	public List<IOption> Options = new List<IOption>()
	{
		new Option<PinpointIDs>("questID", "Quest ID", "ID of the desired Pinpoint quest to do.", PinpointIDs.Dagger)
	};
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();
		Core.AddDrop("Dark Energy", "Dark Spirit Orb", "Corrupt Spirit Orb", "Ominous Aura", "Diabolical Aura", "Doom Aura");

		Core.EquipClass(ClassType.Farm);        
		int i = 1;
		int questID = (int)bot.Config.Get<PinpointIDs>("questID");
		while(!bot.ShouldExit())
		{
			SDKA.PinpointthePieces(questID);
			Core.Logger($"Completed x{i++}");
		}

		Core.SetOptions(false);
	}
}

public enum PinpointIDs
{
	Dagger = 2181,
	Blade = 2182,
	Broadsword = 2183,
	Scythe = 2184,
	Mace = 2185,
	Bow = 2186
}